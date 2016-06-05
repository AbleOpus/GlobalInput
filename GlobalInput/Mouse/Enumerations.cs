using System;

namespace GlobalInput.Mouse
{
    /// <summary>
    /// Represents the tertiary mouse buttons. Typically these are the two tabs
    /// placed on the left of the mouse.
    /// </summary>
    internal enum MouseXButtons : uint
    {
        X1 = 65536,
        X2 = 131072
    }

    /// <summary>
    /// Specifies messages, which indicate what mouse operation was performed.
    /// </summary>
    public enum MouseMessages
    {
        /// <summary>
        /// The mouse pointer has moved.
        /// </summary>
        MouseMove = 0x0200,
        /// <summary>
        /// The left mouse button was depressed.
        /// </summary>
        LeftButtonDown = 0x0201,
        /// <summary>
        /// The left mouse button was unpressed.
        /// </summary>
        LeftButtonUp = 0x0202,
        /// <summary>
        /// The right mouse button was depressed.
        /// </summary>
        RightButtonDown = 0x0204,
        /// <summary>
        /// The right mouse button was unpressed.
        /// </summary>
        RightButtonUp = 0x0205,
        /// <summary>
        /// The mouse wheel has scrolled.
        /// </summary>
        WheelScrolled = 0x020A,
        /// <summary>
        /// The mouse wheel has been depressed.
        /// </summary>
        WheelDown = 0x207,
        /// <summary>
        /// The mouse wheel has been unpressed.
        /// </summary>
        WheelUp = 0x208,
        /// <summary>
        /// The x button has been pressed.
        /// </summary>
        XButtonDown = 523,
        /// <summary>
        /// The x button has been unpressed.
        /// </summary>
        XButtonUp = 524
    }

    /// <summary>
    /// Specifies categorizations of mouse messages.
    /// </summary>
    [Flags]
    public enum MouseMessageTypes
    {
        /// <summary>
        /// No mouse message types.
        /// </summary>
        None = 0,
        /// <summary>
        /// Button down mouse message types.
        /// </summary>
        ButtonDown = 1,
        /// <summary>
        /// Button up mouse message types.
        /// </summary>
        ButtonUp = 2,
        /// <summary>
        /// Mouse move message types.
        /// </summary>
        Move = 4,
        /// <summary>
        /// Mouse wheel message types.
        /// </summary>
        Wheel = 8,
        /// <summary>
        /// All message types.
        /// </summary>
        All = ButtonDown | ButtonUp | Move | Wheel
    }
}
