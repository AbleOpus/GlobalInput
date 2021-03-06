﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Represents a low-level, system wide keyboard hooker.
    /// </summary>
    public class KeyboardHooker : LowLevelHooker                                                
    {
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        /// <summary>
        /// Occurs when a key been depressed.
        /// </summary>
        [Description("Occurs when a key been depressed."), Category("Key")]
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Occurs when a key has been unpressed.
        /// </summary>
        [Description("Occurs when a key been unpressed."), Category("Key")]
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Implements logic to process data passed through the hook call-back.
        /// </summary>
        protected override void ProcHookCallback(IntPtr wParam, IntPtr lParam)
        {
            var hookInfo = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
            var keys = (Keys)hookInfo.VirtualKeyCode;
            var args = new KeyEventArgs(keys);

            switch (wParam.ToInt32())
            {
                case WM_KEYDOWN: KeyDown?.Invoke(this, args); break;
                case WM_KEYUP: KeyUp?.Invoke(this, args); break;
            }
        }

        /// <summary>
        /// Gets the Windows hook type identifier.
        /// </summary>
        protected override int GetWindowsHookType()
        {
            return 13;
        }
    }
}
