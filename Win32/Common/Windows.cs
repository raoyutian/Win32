using System;
using Win32.API;

namespace Win32
{
    public  static  class Windows
    {
        /// <summary>
        /// 窗体阴影特效
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static void SetWindowShadow(IntPtr hwnd)
        {
            Win32Api.SetClassLong(hwnd, Win32Api.GCL_STYLE, Win32Api.GetClassLong(hwnd, Win32Api.GCL_STYLE) | Win32Api.CS_DropSHADOW);
        }
       
    }
}
