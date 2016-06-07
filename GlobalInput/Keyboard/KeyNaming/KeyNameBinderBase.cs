using System.Globalization;
using System.Windows.Forms;

namespace GlobalInput.Keyboard.KeyNaming
{
    /// <summary>
    /// An abstract representation of key-to-name binder. Derived classes will implements
    /// a culture and a method to return names for various keys.
    /// </summary>
    public abstract class KeyNameBinderBase
    {
        /// <summary>
        /// Gets the culture that indicates how the names will appear.
        /// </summary>
        public abstract CultureInfo Culture { get; }

        /// <summary>
        /// Gets a friendly name for the specified key code, or individual modifier value.
        /// </summary>
        /// <param name="key">The key to convert to a user-friendly string.</param>
        /// <returns>A user-friendly string representing the name of the key.</returns>
        public abstract string GetFriendlyName(Keys key);

        /// <summary>
        /// Gets the text that separates the modifiers from themselves, and the modifiers from
        /// the key code. For instance: The separator value " + " will result in something like
        /// "Keys.Control + Keys.Alt + Keys.C".
        /// </summary>
        public abstract string GetSeperator();
    }
}
