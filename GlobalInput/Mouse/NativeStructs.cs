using System;
using System.Runtime.InteropServices;

namespace GlobalInput.Mouse
{
    /// <summary>
    /// Defines the x- and y- coordinates of a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        /// <summary>
        /// The x-coordinate of the point.
        /// </summary>
        public readonly int X;
        /// <summary>
        /// The y-coordinate of the point.
        /// </summary>
        public readonly int Y;
    }

    /// <summary>
    /// Contains information about a low-level mouse input event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MousehookStruct
    {
        /// <summary>
        /// The x- and y-coordinates of the cursor, in screen coordinates.
        /// </summary>
        public POINT Point;
        /// <summary>
        /// If the message is WM_MOUSEWHEEL, the high-order word of this 
        /// member is the wheel delta. The low-order word is reserved. 
        /// A positive value indicates that the wheel was rotated forward, 
        /// away from the user; a negative value indicates that the wheel 
        /// was rotated backward, toward the user. One wheel click is defined 
        /// as WHEEL_DELTA, which is 120.
        /// </summary>
        public readonly uint MouseData;
        /// <summary>
        /// The event-injected flags. An application can use the following values to test the flags.
        /// Testing LLMHF_INJECTED (bit 0) will tell you whether the event was injected. If it was, 
        /// then testing LLMHF_LOWER_IL_INJECTED (bit 1) will tell you whether or not the event was 
        /// injected from a process running at lower integrity level.
        /// </summary>
        public readonly uint Flags;
        /// <summary>
        /// The time stamp for this message.
        /// </summary>
        public readonly uint Time;
        /// <summary>
        /// Additional information associated with the message.
        /// </summary>
        public IntPtr ExtraInfo;
    }
}
