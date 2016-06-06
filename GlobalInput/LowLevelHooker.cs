using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GlobalInput
{
    /// <summary>
    /// Points to a method to process hook information.
    /// </summary>
    internal delegate IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// Provides base functionality for a low-level hooker.
    /// </summary>
    public abstract partial class LowLevelHooker : Component
    {
        private LowLevelHookProc proc;
        private IntPtr hookId = IntPtr.Zero;

        private bool hooked;
        /// <summary>
        /// Gets or sets whether the low-level hook is installed.
        /// </summary>
        [Description("Determines whether the low-level hook is installed."), Category("Behavior"), DefaultValue(false)]
        public bool Hooked
        {
            get { return hooked; }
            set
            {
                if (value != hooked)
                {
                    hooked = value;

                    if (hooked) Hook();
                    else Unhook();

                    OnPropertyChanged();
                }
            }
        }

        private bool callNextHook = true;
        /// <summary>
        /// Gets or sets whether to call the next hook in the chain.
        /// </summary>
        [Description("Determines whether to call the next hook in the chain."), Category("Behavior"), DefaultValue(true)]
        public bool CallNextHook
        {
            get { return callNextHook; }
            set
            {
                if (value != callNextHook)
                {
                    callNextHook = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="LowLevelHooker"/> class.
        /// </summary>
        protected LowLevelHooker()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Provides a constructor for the designer.
        /// </summary>
        protected LowLevelHooker(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// Implements logic to process data passed through the hook call-back.
        /// </summary>
        protected abstract void ProcHookCallback(IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Gets the windows hook type numerical ID.
        /// </summary>
        protected abstract int GetWindowsHookType();

        /// <summary>
        /// Implements the retrieval of the thread ID to use with this hooker.
        /// </summary>
        protected virtual uint GetThreadId()
        {
            return 0;
        }

        /// <summary>
        /// Installs the mouse move hook.
        /// </summary>
        /// <returns>True, if function succeeds, otherwise false.</returns>
        /// <exception cref="Win32Exception"></exception>
        public void Hook()
        {
            proc = HookCallback;

            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                int hookTypeId = GetWindowsHookType();
                IntPtr moduleHandle = NativeMethods.GetModuleHandle(currentModule.ModuleName);
                hookId = NativeMethods.SetWindowsHookEx(hookTypeId, proc, moduleHandle, GetThreadId());
            }

            if (hookId == IntPtr.Zero)
            {
                int lastError = Marshal.GetLastWin32Error();

                if (lastError == 1429)
                {
                    throw new Win32Exception(lastError, "This hook procedure can only be set globally.");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        /// <summary>
        /// Uninstalls the mouse move hook.
        /// </summary>
        /// <returns>True, if function succeeds, otherwise false.</returns>
        public bool Unhook()
        {
            return NativeMethods.UnhookWindowsHookEx(hookId);
        }

        /// <summary>
        /// The call-back method to process hook messages.
        /// </summary>
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                ProcHookCallback(wParam, lParam);
            }

            if (CallNextHook)
            {
                return NativeMethods.CallNextHookEx(hookId, nCode, wParam, lParam);
            }
            else
            {
                return IntPtr.Zero;
            }
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
