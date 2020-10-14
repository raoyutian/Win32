using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Win32.API;

namespace Win32
{
    /// <summary>
    /// Mouse class to   input.
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        ///   MoveTo CursorPos To ,x ,y 
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public static void MoveTo( int x ,int y)
        {
            Win32Api.SetCursorPos(x, y);
        }
        /// <summary>
        ///  Mouse  LeftClick 
        /// </summary>
        public static void LeftClick()
        {
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_ABSOLUTE | Win32Api.MOUSEEVENTF_LEFTDOWN | Win32Api.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  RightClick 
        /// </summary>
        public static void RightClick()
        {
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_ABSOLUTE | Win32Api.MOUSEEVENTF_RIGHTDOWN | Win32Api.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  MiddleClick 
        /// </summary>
        public static void MiddleClick()
        {
            Win32Api.mouse_event(Win32Api.MOUSEEVENTF_ABSOLUTE | Win32Api.MOUSEEVENTF_MIDDLEDOWN | Win32Api.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
        }
    }
}
