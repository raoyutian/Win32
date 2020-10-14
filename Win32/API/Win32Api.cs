using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Win32.Enums;

namespace Win32.API
{
    public   static partial class Win32Api
    {

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event( 
            int bVk,    //虚拟键值 
            byte bScan,// 一般为0   
            int dwFlags,  //这里是整数类型  0 为按下，2为释放 
            int dwExtraInfo  //这里是整数类型 一般情况下设成为 0

        );
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int CreatePen(int nPenStyle, int nWidth, int crColor);
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int DeleteObject(int hObject);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetCursorPos(ref POINT lpPoint);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(int hdc, int nIndex);
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int Rectangle(int hdc, int X1, int Y1, int X2, int Y2);
        

        /// <summary>
        /// 如果函数执行成功，返回值不为0。
        /// 如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// </summary>
        /// <param name="hWnd">要定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复）</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);



        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int ReleaseCapture();
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int ReleaseDC(int hwnd, int hdc);
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SelectObject(int hdc, int hObject);
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetROP2(int hdc, int nDrawMode);


        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]//获取桌面的句柄
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]//在桌面找寻子窗体
        public static extern IntPtr ChildWindowFromPointEx(IntPtr pHwnd, POINT pt, uint uFlgs);
      

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
       
        [DllImport("user32.dll")]//进行坐标转换 （再窗体内部进行查找）
        public static extern bool ScreenToClient(IntPtr hWnd, out POINT lpPoint);


        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hObject, int nXDest, int nYDest, int nWidth,
           int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc,
            TernaryRasterOperations dwRop);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(string lpFileName);



        #region API相关函数 

        /// <summary> 
        /// 回调函数代理 
        /// </summary> 
        public delegate bool CallBack(IntPtr hwnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImportAttribute("user32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "SwitchToThisWindow", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(System.IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetPrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDefault, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpReturnedString, int nSize, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int WritePrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);


      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"> 窗口句柄ID</param>
        /// <param name="ProcessID">窗口所属进程句柄ID</param>
        /// <returns>窗口所属线程句柄ID</returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ProcessID);

        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(int Description, int ReservedValue);

       
        


        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
    (
        int nLeftRect, // x-coordinate of upper-left corner
        int nTopRect, // y-coordinate of upper-left corner
        int nRightRect, // x-coordinate of lower-right corner
        int nBottomRect, // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
     );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

       
        public const int CS_DROPSHADOW = 0x00020000;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }



               // variables for dragging the form
        public const int HTCLIENT = 0x1;

       

   

        [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "FlashWindow")]
        public static extern void FlashWindow(IntPtr hwnd, bool bInvert);
        #endregion

        #region Windows自定义相关
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }


        /// <summary>  
        /// 带有外边框和标题的windows的样式  
        /// </summary>  
        public const long WS_CAPTION = 0X00C0000L;

        /// <summary>  
        /// window的基本样式  
        /// </summary>  
        public const int GWL_STYLE = -16;
        /// <summary>  
        /// window的扩展样式  
        /// </summary>  
        public const int GWL_EXSTYLE = -20;

        public const Int32 WS_EX_LAYERED = 0x00080000;
        public const Int32 WS_EX_TRANSPARENT = 0x00000020;



        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, int newVal);

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr obj);

        #region 设置窗体阴影
        public const int CS_DropSHADOW = 0x20000;
        public const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion


        #endregion
    }
}
