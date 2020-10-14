using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win32.API;

namespace Win32
{
    /// <summary>
    /// Keyboard class to simulate key input.
    /// </summary>
    public static class Keyboard
    {
        /// <summary>
        ///   KeyDown the  key.
        /// </summary>
        /// <param name="virtualKey">Keys</param>
        public static void KeyDown( Keys virtualKey)
        {
            Win32Api.keybd_event((int)virtualKey, 0, 0, 0);
        }
        /// <summary>
        ///   KeyUp the  key.
        /// </summary>
        /// <param name="virtualKey">Keys</param>
        public static void KeyUp(Keys virtualKey)
        {
            Win32Api.keybd_event((int)virtualKey, 0, 2, 0);
        } 
    }
}
