using System;
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
        private static KeyNameBinderBase keyNameBinder = new KeyNameBinderEN();
        /// <summary>
        /// Gets the key-to-name binder used throughout this library to display keys or key
        /// sequences as user-friendly text.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static KeyNameBinderBase KeyNameBinder
        {
            get { return keyNameBinder; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Value cannot be null.");

                keyNameBinder = value;
            }
        }

        /// <summary>
        /// Converts the specified <see cref="Keys"/> to a shortcut string.
        /// </summary>
        public static string KeyDataToString(this Keys keyData)
        {
            var wordList = new List<string>();

            if (keyData.HasFlag(Keys.Alt))
                wordList.Add(KeyNameBinder.GetFriendlyName(Keys.Alt) ?? Keys.Alt.ToString());

            if (keyData.HasFlag(Keys.Shift))
                wordList.Add(KeyNameBinder.GetFriendlyName(Keys.Shift) ?? Keys.Shift.ToString());

            if (keyData.HasFlag(Keys.Control))
                wordList.Add(KeyNameBinder.GetFriendlyName(Keys.Control) ?? Keys.Control.ToString());

            if (keyData.HasFlag(ExtraKeys.WinKeyModifier))
                wordList.Add(KeyNameBinder.GetFriendlyName(ExtraKeys.WinKeyModifier));

            Keys keyCode = keyData.GetKeyCode();

            string keyName = KeyNameBinder.GetFriendlyName(keyCode);

            if (keyName != null)
            {
                wordList.Add(keyName);
            }
            else if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                wordList.Add(keyCode.ToString().Substring(1, 1));
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
                        string word = keyCode.ToString();

                        if (word.StartsWith("Oem"))
                            word = word.Remove(0, 3);

                        TextInfo textInfo = KeyNameBinder.Culture.TextInfo;
                        word = textInfo.ToTitleCase(word);
                        wordList.Add(word);
                        break;
                }
            }

            var SB = new StringBuilder();

            for (int i = 0; i < wordList.Count; i++)
            {
                SB.Append(wordList[i]);

                if (i != wordList.Count - 1)
                    SB.Append(keyNameBinder.GetSeperator());
            }

            return SB.ToString();
        }
    }
}
