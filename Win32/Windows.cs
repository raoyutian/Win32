using System;
using System.Diagnostics;
using System.Text;
using Win32;
using System.Collections;

namespace Win32
{
    /// <summary>
    ///Windows常用API方法封装
    /// </summary>
    public static  class Windows
    {
        /// <summary>
        /// 窗口阴影特效
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static void SetWindowShadow(IntPtr hwnd)
        {
            User32.SetClassLong(hwnd, Const.GCL_STYLE, User32.GetClassLong(hwnd, Const.GCL_STYLE) | Const.CS_DropSHADOW);
        }
        /// <summary>
        /// 锁屏
        /// </summary>
        /// <param name="aLock">true:锁屏+关屏;false:禁止鼠标键盘动作+关屏</param>
        public static void LockWorkStation(bool aLock)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            if (aLock)
            {
                // 锁屏+关屏
                User32.LockWorkStation();
                User32.SendMessage(form.Handle, Const.WM_SYSCOMMAND, Const.SC_MONITORPOWER, Const.MonitorPowerOff);
            }
            else
            {
                // 禁止鼠标键盘动作+关屏
                User32.BlockInput(true);
                System.Threading.Thread.Sleep(10);
                User32.SendMessage(form.Handle, Const.WM_SYSCOMMAND, Const.SC_MONITORPOWER, Const.MonitorPowerOff);
                User32. BlockInput(false);
            }
            form.Dispose();
        }

        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static string GetWindowText(IntPtr hwnd)
        {
            StringBuilder stringBuilder=new StringBuilder(1024);
            User32.GetWindowText(hwnd, stringBuilder, 1024);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 判断窗口举行范围是否存在
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static bool IsEmptyWindowRect(IntPtr hwnd)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle();
            User32.GetWindowRect(hwnd, out rectangle);
            return rectangle.IsEmpty;

        }
        /// <summary>
        /// 获取窗口句柄类名
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static string GetClassName(IntPtr hwnd)
        {
            StringBuilder classname = new StringBuilder(125);
            User32.GetClassName(hwnd, classname, classname.Capacity);
            return classname.ToString();
        }

        /// <summary>
        /// 获取窗口的进程名称
        /// </summary>
        /// <param name="hwnd">窗口句柄，如果hwnd等于IntPtr.Zero，则返回当前组件窗口的进程名称</param>
        /// <returns>进程名称</returns>
        public static string GetWindowProcessName(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            { 
                hwnd = Win32.User32.GetForegroundWindow();
            }
            hwnd = GetTopWindow(hwnd);
            int pid = 0;
            string ProcessName = "";
            Win32.User32.GetWindowThreadProcessId(hwnd, out pid);
            if (pid > 0)
            {
                Process process = Process.GetProcessById(pid);
                if (process != null)
                { ProcessName = process.ProcessName; }
            }
            return ProcessName;
        }
        /// <summary>
        /// 获取顶层窗口
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <returns>窗口句柄</returns>
        public static IntPtr GetTopWindow(IntPtr hwnd)
        {
            IntPtr phwnd = User32.GetParent(hwnd);
            if (phwnd == IntPtr.Zero) return hwnd;
            return GetTopWindow(phwnd);
        }
        
        /// <summary>
        /// 置顶窗口并设置焦点
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        public static void SetTopFocus(IntPtr hwnd)
        {
            Win32.User32.SetForegroundWindow(hwnd);
            Win32.User32.SetFocus(hwnd);
        }
        private static Hashtable ProcessWnd = null;

        public static IntPtr GetWindowHandleByProcessId(uint processId)
        {
            if (ProcessWnd == null)
            {
                ProcessWnd = new Hashtable();
            }
            IntPtr winHandle = IntPtr.Zero;
            object objWnd = ProcessWnd[processId];
            if (objWnd != null)
            {
                winHandle = (IntPtr)objWnd;
                if (winHandle != IntPtr.Zero && Win32.User32.IsWindow(winHandle))
                {
                    return winHandle;
                }
                else
                    winHandle = IntPtr.Zero;
            }
            bool result = Win32.User32.EnumWindows(new User32.WNDENUMPROC(EnumWindowsProc), processId);
            if (!result)
            {
                objWnd = ProcessWnd[processId];
                if (objWnd != null)
                {
                    winHandle = (IntPtr)objWnd;
                }
            }
            return winHandle;

        }

        public static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        {
            uint uiPid = 0;
            if (Win32.User32.GetParent(hwnd) == IntPtr.Zero)
            {
                Win32.User32.GetWindowThreadProcessId(hwnd, out uiPid);
                if (uiPid == lParam)
                {
                    ProcessWnd[uiPid] = hwnd;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 利用Accessible获取对应坐标的窗口句柄
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IntPtr GetWindowByPoint(int x ,int y)
        {
            IntPtr hwnd = IntPtr.Zero;
            Accessibility.IAccessible accessible;
            object childid;
            Win32.Oleacc.AccessibleObjectFromPoint(new POINT(x, y), out accessible, out childid);
            if (accessible != null)
            {
                Win32.Oleacc.WindowFromAccessibleObject(accessible, ref hwnd);
            }
            return hwnd;
        }

       


    #region 窗口操作
    /// <summary>
    /// 窗口最大化
    /// </summary>
    /// <param name="hwnd"></param>
    public static void Maximize(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_MAXIMIZE);
        }
        /// <summary>
        /// 窗口最小化
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Minimize(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_MINIMIZE);
        }
        /// <summary>
        /// 还原窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Normal(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_NORMAL);
        }
        /// <summary>
        /// 恢复窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Restore(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_RESTORE);
        }
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Show(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_SHOW);
        }
        /// <summary>
        /// 隐藏窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Hide(IntPtr hwnd)
        {
            User32.ShowWindow(hwnd, Win32.Const.SW_HIDE);
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void Close(IntPtr hwnd)
        {
            User32.PostMessage((int)hwnd, Win32.Const.WM_CLOSE,IntPtr.Zero, IntPtr.Zero);
        }
        #endregion
    }
}
