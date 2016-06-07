using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlobalInput.Keyboard.KeyNaming;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Used to install system-wide hotkeys.
    /// </summary>
    public class HotkeyHooker : IDisposable, INotifyPropertyChanged
    {
        /// <summary>
        /// Specifies the unmanaged representation of modifier keys.
        /// </summary>
        [Flags]
        private enum Modifiers
        {
            /// <summary>
            /// No modifiers specified.
            /// </summary>
            None = 0x0000,
            /// <summary>
            /// The alt modifier key.
            /// </summary>
            Alt = 0x0001,
            /// <summary>
            /// The control modifier key.
            /// </summary>
            Control = 0x0002,
            /// <summary>
            /// The shift modifier key.
            /// </summary>
            Shift = 0x0004,
            /// <summary>
            /// The Windows key.
            /// </summary>
            Win = 0x0008
        }

        /// <summary>
        /// Represents a window for receiving hotkey messages.
        /// </summary>
        private sealed class HotkeyBindingWindow : NativeWindow, IDisposable
        {
            /// <summary>
            /// Occurs when a registered hotkey is pressed.
            /// </summary>
            internal EventHandler<KeyEventArgs> HotkeyPressed;

            internal HotkeyBindingWindow()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                const int WM_HOTKEY = 0x0312;

                if (m.Msg == WM_HOTKEY)
                    HotkeyPressed?.Invoke(this, new KeyEventArgs(ExtractKeys(m)));

                base.WndProc(ref m);
            }

            /// <summary>
            /// Extract key data from the specified message.
            /// </summary>
            private static Keys ExtractKeys(Message m)
            {
                var modifiers = (Modifiers)((int)m.LParam & 0xFFFF);
                var keyCode = (Keys)((int)m.LParam >> 16);
                keyCode |= ModifiersToKeys(modifiers);
                return keyCode;
            }

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        #region Static Members
        /// <summary>
        /// Indicates whether a <see cref="Keys"/> has been bound already 
        /// by this application or another application.
        /// </summary>
        /// <param name="hotkey">The key to test.</param>
        /// <exception cref="InvalidKeysValueException"></exception>
        public static bool IsHotKeyHooked(Keys hotkey)
        {
            HotkeyHooker hooker = new HotkeyHooker();

            try
            {
                hooker.Hook(hotkey);
            }
            catch (HotkeyAlreadyBoundException)
            {
                return true;
            }
            finally
            {
                hooker.Dispose();
            }

            return false;
        }

        /// <summary>
        /// Gets all of the hotkeys that cannot be hooked asynchronously.
        /// </summary>
        public static Task<Keys[]> GetOccupiedHotkeysTaskAsync()
        {
            return Task.Run(() => GetOccupiedHotkeys());
        }

        /// <summary>
        /// Gets all of the hotkeys that cannot be hooked.
        /// </summary>
        public static Keys[] GetOccupiedHotkeys()
        {
            List<Keys> boundKeys = new List<Keys>();
            HotkeyHooker hooker = new HotkeyHooker();
            var values = Enum.GetValues(typeof(Keys));

            Keys[] possibleModifierValues =
            {
                Keys.None,
                Keys.Shift,
                Keys.Alt,
                ExtraKeys.WinKeyModifier,
                Keys.Control,
                Keys.Control | ExtraKeys.WinKeyModifier,
                Keys.Control | Keys.Alt,
                Keys.Control | Keys.Shift,
                Keys.Shift | Keys.Alt,
                Keys.Shift | Keys.Alt | Keys.Control
            };

            foreach (Keys modifiers in possibleModifierValues)
            {
                foreach (Keys keyCode in values)
                {
                    switch (keyCode)
                    {
                        case Keys.Packet:
                        case Keys.Control:
                        case Keys.Shift:
                        case Keys.Alt:
                            continue;
                    }

                    Keys keyData = keyCode | modifiers;
                    if (keyData == Keys.None) continue;
 
                    try
                    {
                        hooker.Hook(keyData);
                        hooker.Unhook(keyData);
                    }
                    catch (HotkeyAlreadyBoundException)
                    {
                        boundKeys.Add(keyData);
                    }
                }
            }

            hooker.Dispose();
            return boundKeys.ToArray();
        }
        #endregion

        private readonly HotkeyBindingWindow bindingWindow = new HotkeyBindingWindow();
        private readonly BindingList<HotkeyBinding> hotkeyBindings = new BindingList<HotkeyBinding>();

        /// <summary>
        /// Gets a bindable, read-only list of installed hotkeys.
        /// </summary>
        public IReadOnlyList<HotkeyBinding> HotkeyBindings => hotkeyBindings;

        /// <summary>
        /// Gets whether this instance has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        private bool invokeEnabled = true;
        /// <summary>
        /// Gets or sets whether <see cref="Action"/>'s corresponding to a pressed hotkey, will be invoked.
        /// </summary>
        public bool InvokeEnabled
        {
            get { return invokeEnabled; }
            set
            {
                if (value != invokeEnabled)
                {
                    invokeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyHooker"/> class.
        /// </summary>
        public HotkeyHooker()
        {
            bindingWindow.HotkeyPressed = (s, e) =>
            {
                if (!IsDisposed && InvokeEnabled)
                {
                    foreach (var hk in HotkeyBindings)
                    {
                        if (hk.Hotkey == e.KeyData)
                        {
                            hk.Action?.Invoke();
                            break;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Extracts <see cref="Modifiers"/> from the specified <see cref="Keys"/>.
        /// </summary>
        private static Modifiers KeysToModifiers(Keys keys)
        {
            var modifiers = Modifiers.None;

            if (keys.HasFlag(Keys.Alt))
                modifiers |= Modifiers.Alt;

            if (keys.HasFlag(Keys.Control))
                modifiers |= Modifiers.Control;

            if (keys.HasFlag(Keys.Shift))
                modifiers |= Modifiers.Shift;

            if (keys.HasFlag(ExtraKeys.WinKeyModifier))
                modifiers |= Modifiers.Win;

            return modifiers;
        }

        /// <summary>
        /// Converts <see cref="Modifiers"/> to <see cref="Keys"/>.
        /// </summary>
        /// <param name="modifiers">The <see cref="Modifiers"/> to convert.</param>
        private static Keys ModifiersToKeys(Modifiers modifiers)
        {
            var keys = Keys.None;

            if (modifiers.HasFlag(Modifiers.Alt))
                keys |= Keys.Alt;

            if (modifiers.HasFlag(Modifiers.Control))
                keys |= Keys.Control;

            if (modifiers.HasFlag(Modifiers.Shift))
                keys |= Keys.Shift;

            if (modifiers.HasFlag(Modifiers.Win))
                keys |= ExtraKeys.WinKeyModifier;

            return keys;
        }

        /// <summary>
        /// Hooks a system-wide hotkey.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidKeysValueException"></exception>
        public void Hook(Keys hotKey, Action action = null)
        {
            Hook(new HotkeyBinding(hotKey, action));
        }

        /// <summary>
        /// Hooks a system-wide hotkey.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidKeysValueException"></exception>
        public void Hook(HotkeyBinding hotkeyBinding)
        {
            CheckDisposed();

            if (hotkeyBinding == null)
                throw new ArgumentNullException(nameof(hotkeyBinding));

            var existingBinding = hotkeyBindings.FirstOrDefault(b => b.Hotkey == hotkeyBinding.Hotkey);

            if (existingBinding != null)
            {
                throw new HotkeyAlreadyBoundException(
                    $@"The hotkey ""{hotkeyBinding.ToString("{0}")}"" is already hooked by this instance.",
                    existingBinding, hotkeyBinding);
            }

            Keys keyCode = hotkeyBinding.Hotkey & Keys.KeyCode;
            Modifiers mods = KeysToModifiers(hotkeyBinding.Hotkey);

            bool successful = NativeMethods.RegisterHotKey
                (bindingWindow.Handle, hotkeyBinding.Hotkey.GetHashCode(), (uint)mods, (uint)keyCode);

            if (!successful)
            {
                int errorCode = Marshal.GetLastWin32Error();

                switch (errorCode)
                {
                    case 1409:
                        throw new HotkeyAlreadyBoundException(
                            $@"The hotkey ""{hotkeyBinding.ToString("{0}")}"" is already hooked by another program.");

                    default:
                        throw new NotImplementedException("Unexpected Win32Error code.");
                }
            }

            hotkeyBindings.Add(hotkeyBinding);
        }

        /// <summary>
        /// Unhooks a hooked system-wide hotkey.
        /// </summary>
        /// <exception cref="HotkeyNotBoundException">Tried to unhook a hotkey which was not hooked.</exception>
        public void Unhook(Keys keys)
        {
            CheckDisposed();
            UnhookCore(keys);
            hotkeyBindings.Remove(new HotkeyBinding(keys));
        }


        /// <summary>
        /// Unhooks the specified hotkey.
        /// </summary>
        /// <exception cref="HotkeyNotBoundException"></exception>
        private void UnhookCore(Keys keys)
        {
            bool successful = NativeMethods.UnregisterHotKey
                (bindingWindow.Handle, keys.GetHashCode());

            if (!successful)
            {
                int errorCode = Marshal.GetLastWin32Error();
                Debug.WriteLine(errorCode);

                switch (errorCode)
                {
                    case 1419:
                        throw new HotkeyNotBoundException(
                            $@"Cannot unhook ""{keys.KeyDataToString()}"" as is not hooked by this hooker.", keys);

                    default: throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Checks to see if this instance has been disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(HotkeyHooker), "Cannot use object after it has been disposed.");
        }

        /// <summary>
        /// Unhooks all hooked system-wide hotkeys.
        /// </summary>
        /// <exception cref="HotkeyNotBoundException"></exception>
        public void UnhookAll()
        {
            CheckDisposed();

            foreach (var hook in HotkeyBindings)
            {
                UnhookCore(hook.Hotkey);
            }

            hotkeyBindings.Clear();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            UnhookAll();
            bindingWindow.Dispose();
            IsDisposed = true;
        }

        /// <summary>
        /// Occurs when the value of a bindable property has changed.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
