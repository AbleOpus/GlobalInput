using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Provides additional keys that are not already within the Keys enumeration.
    /// </summary>
    public static class ExtraKeys
    {
        /// <summary>
        /// Gets the flaggable Windows key modifier.
        /// </summary>
        /// <remarks>This flag comes immediately after the Alt flag. This flag is not used
        /// by anything but <see cref="GlobalInput"/>.
        /// </remarks>
        public const Keys WinKeyModifier = (Keys)524288;
    }
}
