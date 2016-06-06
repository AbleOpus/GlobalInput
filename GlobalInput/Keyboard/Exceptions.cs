using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Th exception that is thrown to indicate that the specified <see cref="Keys"/> cannot be 
    /// bound because it has been previously bound either by this application or 
    /// another running application.
    /// </summary>
    public sealed class HotkeyAlreadyBoundException : Exception
    {
        /// <summary>
        /// Gets the binding that is already bound.
        /// </summary>
        public HotkeyBinding ExistingBinding { get; }

        /// <summary>
        /// Gets the new binding that conflicts with the already bound binding.
        /// </summary>
        public HotkeyBinding RedundantBinding { get; }

        public bool IsLocalBindingConflict => ExistingBinding != null;

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyAlreadyBoundException"/> class
        /// with the specified arguments.
        /// </summary>
        /// <param name="message">The message to summarize this exception.</param>
        /// <param name="existingBinding">The binding that is already bound.</param>
        /// <param name="redundantBinding">The new binding that conflicts with the already bound binding.</param>
        internal HotkeyAlreadyBoundException(string message, HotkeyBinding existingBinding,
            HotkeyBinding redundantBinding) : base(message)
        {
            ExistingBinding = existingBinding;
            RedundantBinding = redundantBinding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyAlreadyBoundException"/> class
        /// with the specified arguments.
        /// </summary>
        /// <param name="message">The message to summarize this exception.</param>
        internal HotkeyAlreadyBoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Exception thrown to indicate that the specified <see cref="Keys"/> value 
    /// is an invalid representation of key input.
    /// </summary>
    public sealed class InvalidKeysValueException : Exception
    {
        /// <summary>
        /// Gets the invalid Keys value.
        /// </summary>
        public Keys Keys { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidKeysValueException"/> class
        /// with the specified arguments.
        /// </summary>
        /// <param name="message">A message that describes the current exception.</param>
        /// <param name="keys"> the invalid Keys value.</param>
        public InvalidKeysValueException(string message, Keys keys) : base(message)
        {
            Keys = keys;
        }
    }

    /// <summary>
    /// Exception thrown to indicate that the specified <see cref="Keys"/> cannot 
    /// be unbound because it has not previously been bound by this application.
    /// </summary>
    public sealed class HotkeyNotBoundException : Exception
    {
        /// <summary>
        /// Gets the hotkey that is already bound.
        /// </summary>
        public Keys Hotkey { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyNotBoundException"/> class
        /// with the specified arguments.
        /// </summary>
        /// <param name="message">A message that describes the current exception.</param>
        /// <param name="hotkey">The hotkey that is already bound.</param>
        public HotkeyNotBoundException(string message, Keys hotkey) : base(message)
        {
            Hotkey = hotkey;
        }
    }
}