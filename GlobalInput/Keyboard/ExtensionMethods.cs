using System.Linq;
using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Extension methods for keyboard related functionality.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extracts <see cref="Modifiers"/> from the specified <see cref="Keys"/>.
        /// </summary>
        internal static Modifiers ExtractModifiers(this Keys keys)
        {
            var modifiers = Modifiers.None;

            if (keys.HasFlag(Keys.Alt))
                modifiers |= Modifiers.Alt;

            if (keys.HasFlag(Keys.Control))
                modifiers |= Modifiers.Control;

            if (keys.HasFlag(Keys.Shift))
                modifiers |= Modifiers.Shift;

            return modifiers;
        }

        /// <summary>
        /// Gets whether the specified <see cref="Keys"/> has a toggle key.
        /// </summary>
        /// <returns>True, if a toggle key is found, otherwise false.</returns>
        public static bool HasToggleKey(this Keys keyData)
        {
            Keys[] toggleKeys = {Keys.Tab, Keys.NumLock, Keys.Capital, Keys.Scroll};
            return toggleKeys.Any(key => (keyData & Keys.KeyCode) == key);
        }

        /// <summary>
        /// Gets whether the specified key contains only modifiers.
        /// </summary>
        /// <remarks>This method ignores the non-flaggable modifiers.</remarks>
        /// <param name="keyData">The key to evaluate.</param>
        /// <returns>true, if only modifiers, otherwise false.</returns>
        public static bool IsOnlyModifiers(this Keys keyData)
        {
            if (keyData == Keys.None) return false;

            Keys keyCode = keyData & Keys.KeyCode;

            bool hasModifiers = (keyData & Keys.Modifiers) != Keys.None;
            bool hasNonMod =
                keyCode != Keys.None &&
                keyCode != Keys.Menu &&
                keyCode != Keys.ShiftKey &&
                keyCode != Keys.ControlKey;

            return !hasNonMod && hasModifiers;
        }

        /// <summary>
        /// Gets whether the specified key is being pressed.
        /// </summary>
        public static bool IsKeyPressed(this Keys key)
        {
            short result = NativeMethods.GetKeyState(key);

            switch (result)
            {
                case 0: return false;
                case 1: return false;
                default: return true;
            }
        }

        /// <summary>
        /// Converts <see cref="Modifiers"/> to <see cref="Keys"/>.
        /// </summary>
        /// <param name="modifiers">The <see cref="Modifiers"/> to convert.</param>
        internal static Keys ToKeys(this Modifiers modifiers)
        {
            var keys = Keys.None;

            if (modifiers.HasFlag(Modifiers.Alt))
                keys |= Keys.Alt;

            if (modifiers.HasFlag(Modifiers.Control))
                keys |= Keys.Control;

            if (modifiers.HasFlag(Modifiers.Shift))
                keys |= Keys.Shift;

            return keys;
        }
    }
}
