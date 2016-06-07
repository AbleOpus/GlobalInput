using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace GlobalInput.Keyboard.KeyNaming
{
    /// <summary>
    /// Represents a key name binder with the names being in culture-neutral English.
    /// </summary>
    public class KeyNameBinderEN : KeyNameBinderBase
    {
        /// <summary>
        /// Gets the culture that indicates how the names will appear.
        /// </summary>
        public override CultureInfo Culture => CultureInfo.GetCultureInfo("en");

        /// <summary>
        /// Gets a friendly name for the specified key code.
        /// </summary>
        /// <param name="key">The key to convert to a user-friendly string.</param>
        /// <returns>A user-friendly string representing the name of the key.</returns>
        public override string GetFriendlyName(Keys key)
        {
            switch (key)
            {
                // Mods
                case ExtraKeys.WinKeyModifier: return "Windows";
                // Key code Mods.
                case Keys.RShiftKey: return "Right Shift";
                case Keys.LShiftKey: return "Left Shift";
                case Keys.LControlKey: return "Left Ctrl";
                case Keys.RControlKey: return "Right Ctrl";
                case Keys.LMenu: return "Left Alt";
                case Keys.RMenu: return "Right Alt";
                // Non-mods.
                case Keys.Back: return "Backspace";
                case Keys.Oem1: return "Semicolon";
                case Keys.OemOpenBrackets: return"Open Bracket";
                case Keys.Oem6: return"Close Bracket";
                case Keys.Oem7: return"Quote";
                case Keys.Enter: return"Enter";
                case Keys.PageDown:  return "Page Down";
                case Keys.PageUp: return"Page Up";
                case Keys.Oem5: return"Back Slash";
                case Keys.LWin: return"Left Win";
                case Keys.RWin: return "Right Win";
                case Keys.CapsLock: return"Caps Lock";
                case Keys.OemQuestion: return "Forward Slash";
                case Keys.Scroll: return "Scroll Lock"; 
                case Keys.NumLock: return "Number Lock"; 
                case Keys.PrintScreen:
                case Keys.F13: return "Print Screen";
                case Keys.VolumeDown: return "Volume Down";
                case Keys.VolumeUp: return "Volume Up";
                case Keys.VolumeMute: return "Mute Volume";
                case Keys.MediaNextTrack: return "Play Next";
                case Keys.MediaPreviousTrack: return "Play Next";
                case Keys.MediaPlayPause: return "Play/Pause";
                case Keys.BrowserRefresh: return "Browser Refresh";
                case Keys.BrowserStop: return "Browser Stop";
                case Keys.BrowserBack: return "Browser Back";
                case Keys.BrowserForward: return "Browser Forward";
                case Keys.BrowserFavorites: return "Browser Favorites";
                case Keys.LaunchMail: return "Launch Mail";
                case Keys.SelectMedia: return "Select Media";
                case Keys.LaunchApplication1: return "Launch App 1";
                case Keys.LaunchApplication2: return "Launch App 2";
                case Keys.LineFeed: return "Line Feed";
                case Keys.Zoom: return "Zoom";
                default: return null;
            }
        }

        /// <summary>
        /// Gets the text that separates the modifiers from themselves, and the modifiers from
        /// the key code. For instance: The separator value " + " will result in something like
        /// "Keys.Control + Keys.Alt + Keys.C".
        /// </summary>
        public override string GetSeperator()
        {
            return " + ";
        }
    }
}