using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    internal static class ExtraKeys
    {
        /// <summary>
        /// Gets the Windows key modifier.
        /// </summary>
        /// <remarks>This flag comes immediately after the Alt flag. This flag is not used
        /// by anything but <see cref="GlobalInput"/>.
        /// </remarks>
        internal const Keys WinKeyModifier = (Keys)524288;
    }
}
