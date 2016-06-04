using System.Globalization;
using System.Windows.Forms;

namespace GlobalInput.Keyboard.KeyNaming
{
    /// <summary>
    /// Represents a key name binder with the names being in culture-neutral English.
    /// </summary>
    public class KeyNameBinderEN : KeyNameBinderBase
    {
        public override CultureInfo Culture => CultureInfo.GetCultureInfo("en");

        public override string GetFriendlyName(Keys key)
        {
            switch (key)
            {
                // I decided to not space the words, as it makes the sequence as a whole, more readable.
                case Keys.Oem1: return "Semicolon";
                case Keys.OemOpenBrackets: return"Open Bracket";
                case Keys.Oem6: return"Close Bracket";
                case Keys.Oem7: return"Quote";
                case Keys.Enter: return"Enter";
                case Keys.PageDown:  return "Page Down";
                case Keys.PageUp: return"Page Up"; // Otherwise "5"
                case Keys.Oem5: return"Back Slash"; // Otherwise "5"
                case Keys.LWin: return"Left Win"; // Otherwise "LWin"
                case Keys.RWin: return "Right Win"; // Otherwise "RWin"
                case Keys.CapsLock: return"Caps Lock"; // Otherwise "Capital"
                case Keys.OemQuestion: return "Forward Slash"; // Otherwise "Question"
                case Keys.Scroll: return "Scroll Lock"; // Otherwise "Scroll"
                case Keys.NumLock: return "Number Lock"; // Otherwise "Scroll"
                case Keys.PrintScreen:
                case Keys.F13: return "Print Screen"; // Otherwise "F13"

                default: return null;
            }
        }
    }
}