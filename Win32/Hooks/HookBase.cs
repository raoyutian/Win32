using System;
using System.Runtime.InteropServices;

namespace Win32.Hooks
{
    /// <summary>
    /// 钩子基类
    /// </summary>
    public abstract class HookBase
    {
        #region const
        public const int WH_JOURNALRECORD = 0;      //监视和记录输入事件。安装一个挂钩处理过程,对寄送至系统消息队列的输入消息进行纪录
        public const int WH_JOURNALPLAYBACK = 1;    //回放用WH_JOURNALRECORD记录事件
        public const int WH_KEYBOARD = 2;           //键盘钩子，键盘触发消息。WM_KEYUP或WM_KEYDOWN消息
        public const int WH_GETMESSAGE = 3;         //发送到窗口的消息。GetMessage或PeekMessage触发
        public const int WH_CALLWNDPROC = 4;        //发送到窗口的消息。由SendMessage触发
        public const int WH_CBT = 5;                //当基于计算机的训练(CBT)事件发生时 
        public const int WH_SYSMSGFILTER = 6;       //同WH_MSGFILTER一样，系统范围的。 
        public const int WH_MOUSE = 7;              //鼠标钩子,查询鼠标事件消息
        public const int WH_HARDWARE = 8;           //非鼠标、键盘消息时
        public const int WH_DEBUG = 9;              //调试钩子，用来给钩子函数除错
        public const int WH_SHELL = 10;             //外壳钩子，当关于WINDOWS外壳事件发生时触发. 
        public const int WH_FOREGROUNDIDLE = 11;    //前台应用程序线程变成空闲时候，钩子激活。
        public const int WH_CALLWNDPROCRET = 12;    //发送到窗口的消息。由SendMessage处理完成返回时触发
        public const int WH_KEYBOARD_LL = 13;       //此挂钩只能在Windows NT中被安装,用来对底层的键盘输入事件进行监视
        public const int WH_MOUSE_LL = 14;          //此挂钩只能在Windows NT中被安装,用来对底层的鼠标输入事件进行监视
        #endregion

        #region API
        //装置钩子的函数  
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸下钩子的函数  
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern bool UnhookWindowsHookEx(int idHook);
        //下一个钩挂的函数  
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        protected delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        protected delegate IntPtr HookWndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);
        #endregion

        #region 变量
        protected  int hwndHook = 0; //钩子句柄  
        protected HookProc HookProcedure; //声明钩子事件类型.  
        #endregion
        /// <summary>  
        /// 墨认的构造函数构造当前类的实例.  
        /// </summary>  
        public HookBase()
        {
        }

        //析构函数.  
        ~HookBase()
        {
            Stop();
        }
        private int _HookID=14;
        protected  virtual int HookID
        {
            get
            {
                return _HookID;
            }
        }

        public  virtual void Start()
        {   //安装鼠标钩子  
            if (hwndHook == 0)
            {
                //生成一个HookProc的实例.  
                HookProcedure = new HookProc(HookProcEvent);

                hwndHook = SetWindowsHookEx(HookID, HookProcedure, Marshal.GetHINSTANCE(System.Reflection.Assembly.GetEntryAssembly().GetModules()[0]), 0);

                //如果装置失败停止钩子  
                if (hwndHook == 0)
                {
                    Stop();
                   // throw new Exception("SetWindowsHookEx failed.");  //Max中会抛出异常
                }
            }

        }

        public virtual void Stop()
        {
            bool retMouse = true;
            if (hwndHook != 0)
            {
                retMouse = UnhookWindowsHookEx(hwndHook);
                hwndHook = 0;
            }

            //如果卸下钩子失败  
            if (!(retMouse))
            {
                //throw new Exception("UnhookWindowsHookEx failed.");
            }
        }

        public virtual int HookProcEvent(int nCode, int wParam, IntPtr lParam)
        {
           throw new Exception("Please  overwrite HookProcEvent");  
        }
    }

}
