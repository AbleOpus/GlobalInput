using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Exception thrown to indicate that specified <see cref="Keys"/> cannot be 
    /// bound because it has been previously bound either by this application or 
    /// another running application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This exception normally occurs when you attempt to bind 
    /// <see cref="Keys"/> that has previously been bound by this application. 
    /// </para>
    /// <para>
    /// This exception can also occur when another running application has already 
    /// bound the specified <see cref="Keys"/>.  
    /// </para>
    /// <para>
    /// Use the <see cref="HotkeyHooker.Unhook"/> method to unbind 
    /// <see cref="Keys"/> previously bound by this application.
    /// </para>
    /// <para>
    /// Use the <see cref="HotkeyHooker.IsHotKeyHooked"/> function to 
    /// determine whether the <see cref="Keys"/> in question has already been 
    /// bound either by this application or another running application.
    /// </para>
    /// </remarks>
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

    public sealed class InvalidKeysValueException : Exception
    {
        public Keys Keys { get; }

        public InvalidKeysValueException(string message, Keys keys) : base(message)
        {
            Keys = keys;
        }
    }
}