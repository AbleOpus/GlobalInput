using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GlobalInput.Mouse
{
    /// <summary>
    /// Represents a system-wide mouse hooker.
    /// </summary>
    [ToolboxItem("sdsdssdsds. eddf.")]
    public sealed class MouseHooker : LowLevelHooker
    {
        /// <summary>
        /// Gets or sets an inclusive message type filter, to disable and enable the invocation
        ///  of certain events, without unsubscribing to them, or unhooking the low-level hooker.
        /// </summary>
        [Description("An inclusive message type filter, to disable and enable the invocation of" + 
            " certain events, without unsubscribing to them, or unhooking the low-level hooker.")]
        [DefaultValue(MouseMessageTypes.All), Category("Behavior")]
        public MouseMessageTypes MessageFilter { get; set; }

        #region Events
        /// <summary>
        /// Occurs when the mouse wheel scrolls.
        /// </summary>
        public event MouseEventHandler MouseWheel;

        /// <summary>
        /// Occurs when the mouse has moved.
        /// </summary>
        public event MouseEventHandler MouseMoved;

        /// <summary>
        /// Occurs when a mouse button has been released.
        /// </summary>
        public event MouseEventHandler MouseUp;

        /// <summary>
        /// Occurs when a mouse button has been depressed.
        /// </summary>
        public event MouseEventHandler MouseDown;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseHooker"/> class.
        /// </summary>
        public MouseHooker()
        {
            MessageFilter = MouseMessageTypes.All;
        }

        /// <summary>
        /// Implements logic to process data passed through the hook call-back.
        /// </summary>
        protected override void ProcHookCallback(IntPtr wParam, IntPtr lParam)
        {
            var hookStruct = (MousehookStruct)Marshal.PtrToStructure(lParam, typeof(MousehookStruct));

            // Process message and raise the appropriate event
            switch ((MouseMessages)wParam)
            {
                case MouseMessages.MouseMove:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Move)) break;
                    var mouseButtons = (MouseButtons)hookStruct.Flags;
                    var args = new MouseEventArgs(mouseButtons, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseMoved?.Invoke(this, args);
                    break;

                case MouseMessages.RightButtonDown:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonDown)) break;
                    args = new MouseEventArgs(MouseButtons.Right, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.LeftButtonDown:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonDown)) break;
                    args = new MouseEventArgs(MouseButtons.Left, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.RightButtonUp:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonUp)) break;
                    args = new MouseEventArgs(MouseButtons.Right, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
                    break;

                case MouseMessages.LeftButtonUp:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.ButtonUp)) break;
                    args = new MouseEventArgs(MouseButtons.Left, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
                    break;

                case MouseMessages.WheelScrolled:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    var mouseDelta = (short)((hookStruct.MouseData >> 16) & 0xffff);
                    args = new MouseEventArgs(MouseButtons.Middle, 0, hookStruct.Point.X, hookStruct.Point.Y, mouseDelta);
                    MouseWheel?.Invoke(this, args);
                    break;

                case MouseMessages.WheelDown:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    args = new MouseEventArgs(MouseButtons.Middle, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseDown?.Invoke(this, args);
                    break;

                case MouseMessages.WheelUp:
                    if (!MessageFilter.HasFlag(MouseMessageTypes.Wheel)) break;
                    args = new MouseEventArgs(MouseButtons.Middle, 0, hookStruct.Point.X, hookStruct.Point.Y, 0);
                    MouseUp?.Invoke(this, args);
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
