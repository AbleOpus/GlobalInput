using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GlobalInput.Keyboard
{
    /// <summary>
    /// Exception thrown to indicate that the specified <see cref="Keys"/> cannot 
    /// be unbound because it has not previously been bound by this application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This exception normally occurs when you attempt to unbind 
    /// <see cref="Keys"/> that was not previously bound by this application.
    /// </para>
    /// <para>
    /// You cannot unbind <see cref="Keys"/> registered by another application.
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class HotkeyNotBoundException : Win32Exception
    {
        internal HotkeyNotBoundException(int errorCode) : base(errorCode)
        {
        }
    }
}