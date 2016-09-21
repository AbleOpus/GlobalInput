using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GlobalInput.Mouse
{
    /// <summary>
    /// Represents a system-wide mouse hooker.
    /// </summary>
    public sealed class MouseHooker : LowLevelHooker
    {
        /// <summary>
        /// Represents the tertiary mouse buttons. Typically these are the two tabs
        /// placed on the left of the mouse.
        /// </summary>
        private enum MouseXButtons : uint
        {
            X1 = 65536,
            X2 = 131072
        }

        /// <summary>
        /// Gets the buttons that are currently depressed.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MouseButtons ButtonsDown { get; private set; }

        /// <summary>
        /// Gets or sets an inclusive message type filter, to disable and enable the invocation
        ///  of certain events, without unsubscribing to them, or unhooking the low-level hooker.
        /// </summary>
        [Description("An inclusive message type filter, to disable and enable the invocation of" + 
            " certain events, without unsubscribing to them, or unhooking the low-level hooker.")]
        [DefaultValue(MouseMessageTypes.All), Category("Behavior")]
        public MouseMessageTypes MessageFilter { get; set; } = MouseMessageTypes.All;

        #region Events
        /// <summary>
        /// Occurs when the mouse wheel scrolls.
        /// </summary>
        [Description("Occurs when the mouse wheel scrolls."), Category("Mouse")]
        public event MouseEventHandler MouseWheel;

        /// <summary>
        /// Occurs when the mouse has moved.
        /// </summary>
        [Description("Occurs when the mouse has moved."), Category("Mouse")]
        public event MouseEventHandler MouseMoved;

        /// <summary>
        /// Occurs when a mouse button has been released.
        /// </summary>
        [Description("Occurs when a mouse button has been released."), Category("Mouse")]
        public event MouseEventHandler MouseUp;

        /// <summary>
        /// Occurs when a mouse button has been depressed.
        /// </summary>
        [Description("Occurs when a mouse button has been depressed."), Category("Mouse")]
        public event MouseEventHandler MouseDown;
        #endregion

        /// <summary>
        /// Implements logic to process data passed through the hook call-back.
        /// </summary>
        protected override void ProcHookCallback(IntPtr wParam, IntPtr lParam)
        {
            var mouseData = (MousehookStruct)Marshal.PtrToStructure(lParam, typeof(MousehookStruct));

            switch ((MouseMessages)wParam)
            {
                case MouseMessages.MouseMove:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Move)) break;
                    var mouseButtons = (MouseButtons)mouseData.Flags;
                    var args = new MouseEventArgs(mouseButtons, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseMoved?.Invoke(this, args);
                    return; // For easier debugging. Data may be written to output window below.

                case MouseMessages.RightButtonDown:
                    ButtonsDown |= MouseButtons.Right;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonDown)) break;
                    args = new MouseEventArgs(MouseButtons.Right, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.LeftButtonDown:
                    ButtonsDown |= MouseButtons.Left;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonDown)) break;
                    args = new MouseEventArgs(MouseButtons.Left, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.RightButtonUp:
                    ButtonsDown &= ~MouseButtons.Right;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonUp)) break;
                    args = new MouseEventArgs(MouseButtons.Right, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
                    break;

                case MouseMessages.LeftButtonUp:
                    ButtonsDown &= ~MouseButtons.Left;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonUp)) break;
                    args = new MouseEventArgs(MouseButtons.Left, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
                    break;

                case MouseMessages.WheelScrolled:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    var mouseDelta = (short)((mouseData.MouseData >> 16) & 0xffff);
                    args = new MouseEventArgs(MouseButtons.Middle, 0, mouseData.Point.X, mouseData.Point.Y, mouseDelta);
                    MouseWheel?.Invoke(this, args);
                    break;

                case MouseMessages.WheelDown:
                    ButtonsDown |= MouseButtons.Middle;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    args = new MouseEventArgs(MouseButtons.Middle, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.WheelUp:
                    ButtonsDown &= ~MouseButtons.Middle;
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    args = new MouseEventArgs(MouseButtons.Middle, 0, mouseData.Point.X, mouseData.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
                    break;

                case MouseMessages.XButtonDown:
                    bool isX1 = (MouseXButtons)mouseData.MouseData == MouseXButtons.X1;
                    var mouseButton = isX1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    if (isX1) ButtonsDown |= MouseButtons.XButton1;
                    else ButtonsDown |= MouseButtons.XButton2;
                    MouseDown?.Invoke(this, new MouseEventArgs(mouseButton, 0, mouseData.Point.X, mouseData.Point.Y, 0));
                    break;

                case MouseMessages.XButtonUp:
                    isX1 = (MouseXButtons)mouseData.MouseData == MouseXButtons.X1;
                    mouseButton = isX1 ? MouseButtons.XButton1 : MouseButtons.XButton2;
                    if (isX1) ButtonsDown &= ~MouseButtons.XButton1;
                    else ButtonsDown &= ~MouseButtons.XButton2;
                    MouseUp?.Invoke(this, new MouseEventArgs(mouseButton, 0, mouseData.Point.X, mouseData.Point.Y, 0));
                    break;
            }
        }

        /// <summary>
        /// Gets the windows hook type numerical ID.
        /// </summary>
        protected override int GetWindowsHookType()
        {
            return 14;
        }
    }
}
