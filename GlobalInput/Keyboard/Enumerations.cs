using System;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Specifies the unmanaged representation of modifier keys.
    /// </summary>
    [Flags]
    public enum Modifiers
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
}