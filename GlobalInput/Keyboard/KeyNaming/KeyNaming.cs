using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace GlobalInput.Keyboard.KeyNaming
{
    /// <summary>
    /// Provides functionality for naming keys and key sequences.
    /// </summary>
    public static class KeyNaming
    {
        /// <summary>
        /// Gets the key-to-name binder used throughout this library to display keys or key
        /// sequences as user-friendly text.
        /// </summary>
        public static KeyNameBinderBase KeyNameBinder { get; } = new KeyNameBinderEN();

        /// <summary>
        /// Converts the specified <see cref="Keys"/> to a shortcut string.
        /// </summary>
        public static string KeyDataToString(this Keys keyData)
        {
            if (keyData == Keys.None) return "None";
            var wordList = new List<string>();
            if (keyData.HasFlag(Keys.Alt)) wordList.Add("Alt");
            if (keyData.HasFlag(Keys.Shift)) wordList.Add("Shift");
            if (keyData.HasFlag(Keys.Control)) wordList.Add("Control");
            if (keyData.HasFlag(ExtraKeys.WinKeyModifier)) wordList.Add("Windows");
            Keys keyCode = keyData & Keys.KeyCode;

            // Process numbers
            if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                wordList.Add(keyCode.ToString().Substring(1, 1));
            }
            else
            {
                string keyName = KeyNameBinder.GetFriendlyName(keyCode);

                if (keyName != null)
                {
                    wordList.Add(keyName);
                }
                else
                {
                    switch (keyCode)
                    {
                        case Keys.Menu:
                        case Keys.ShiftKey:
                        case Keys.ControlKey:
                            break;

                        default:
                            if (keyCode != Keys.None)
                            {
                                string word = keyCode.ToString();

                                if (word.StartsWith("Oem"))
                                    word = word.Remove(0, 3);

                                TextInfo textInfo = KeyNameBinder.Culture.TextInfo;
                                word = textInfo.ToTitleCase(word);
                                wordList.Add(word);
                            }
                            break;
                    }
                }
            }

            var SB = new StringBuilder();

            for (int i = 0; i < wordList.Count; i++)
            {
                SB.Append(wordList[i]);

                if (i != wordList.Count - 1)
                    SB.Append(" + ");
            }

            return SB.ToString();
        }
    }
}
