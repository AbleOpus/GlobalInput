using System;
using System.Windows.Forms;
using GlobalInput.Keyboard.KeyNaming;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Binds <see cref="Keys"/> to an <see cref="Action"/>.
    /// </summary>
    public class HotkeyBinding : IFormattable
    {
        /// <summary>
        /// Gets or sets the hotkey for this binding.
        /// </summary>
        public Keys Hotkey { get; }

        /// <summary>
        /// Gets the <see cref="Action"/> for this binding.
        /// </summary>
        public Action Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HotkeyBinding"/> class with the
        /// specified arguments.
        /// </summary>
        /// <param name="hotkey">The hotkey for this binding.</param>
        /// <param name="action">The <see cref="Action"/> for this binding.</param>
        /// <exception cref="InvalidKeysValueException"></exception>
        public HotkeyBinding(Keys hotkey, Action action = null)
        {
            if (hotkey == Keys.None)
            {
                throw new InvalidKeysValueException(
                    $@"the value of ""{hotkey}"" cannot be ""None"".",
                    hotkey);
            }

            if ((hotkey & Keys.KeyCode) == Keys.Packet)
            {
                throw new InvalidKeysValueException(
                    $@"the value of ""{hotkey}"" cannot be ""Packet"". Packet is not a real keyboard key.",
                    hotkey);
            }

            Hotkey = hotkey;
            Action = action;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            string methodName = Action?.Method.Name ?? "N/A";
            return $"Hotkey: {Hotkey.KeyDataToString() }, Action: {methodName}";
        }

        /// <summary>
        /// Formats the value of the current instance using the specified format.
        /// </summary>
        /// <returns>
        /// The value of the current instance in the specified format.
        /// </returns>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use 
        /// the default format defined for the type of the <see cref="T:System.IFormattable"/> implementation. 
        /// </param><param name="formatProvider">The provider to use to format the value.-or- A null reference 
        /// (Nothing in Visual Basic) to obtain the numeric format information from the current locale 
        /// setting of the operating system. </param><filterpriority>2</filterpriority>
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (format == null)
                return ToString();

            string methodName = Action?.Method.Name ?? "N/A";
            return String.Format(formatProvider, format, Hotkey.KeyDataToString(), methodName);
        }

        /// <summary>
        /// Determines whether the specified HotkeyBinding is equal to the current HotkeyBinding.
        /// </summary>
        /// <returns>
        /// true if the specified HotkeyBinding is equal to the current HotkeyBinding; otherwise, false.
        /// </returns>
        protected bool Equals(HotkeyBinding other)
        {
            return Hotkey == other.Hotkey;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object.</param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HotkeyBinding)obj);
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return (int)Hotkey;
        }

        /// <summary> </summary>
        public static bool operator ==(HotkeyBinding left, HotkeyBinding right)
        {
            return Equals(left, right);
        }

        /// <summary> </summary>
        public static bool operator !=(HotkeyBinding left, HotkeyBinding right)
        {
            return !Equals(left, right);
        }
    }
}
