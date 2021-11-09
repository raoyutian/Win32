using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win32.Hooks
{
    /// <summary>
    /// 鼠标钩子
    /// </summary>
    public class MouseHook : HookBase
    {
        #region 常量
        /// <summary>
        /// 鼠标双击时间间隔
        /// </summary>
        private uint DoubleClickTime = 500;

        /// <summary>
        /// 鼠标左键是否按下
        /// </summary>
        private bool mouseLeftDown = false;

        /// <summary>
        /// 最后按下鼠标键时间
        /// </summary>
        private int LastMouseDownTimeSpan = 0;

        //声明鼠标钩子的封送结构类型  
        [StructLayout(LayoutKind.Sequential)]
        private struct MouseHookStruct
        {
            public Win32.POINT pt;
            /// <summary>
            /// 如果是鼠标滚动事件，则hWndde 高位是滚动的wheel delta的值，低位保留
            /// </summary>
            public int hWnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }
       
        #endregion
        #region 鼠标公开全局事件
        /// <summary>
        /// 鼠标左键按下
        /// </summary>
        public event MouseEventHandler LeftDown;
        /// <summary>
        /// 鼠标左键弹起
        /// </summary>
        public event MouseEventHandler LeftUp;
        ///// <summary>
        ///// 鼠标左键单击
        ///// </summary>
        //public event MouseEventHandler LeftClick;
        ///// <summary>
        ///// 鼠标左键双击
        ///// </summary>
        //public event MouseEventHandler LeftDBClick;
        /// <summary>
        /// 鼠标中键按下
        /// </summary>
        public event MouseEventHandler MiddleDown;
        /// <summary>
        /// 鼠标中键弹起
        /// </summary>
        public event MouseEventHandler MiddleUp;
        ///// <summary>
        ///// 鼠标中键点击
        ///// </summary>
        //public event MouseEventHandler MiddleClick;
        /// <summary>
        /// 鼠标右键按下
        /// </summary>
        public event MouseEventHandler RightDown;
        /// <summary>
        /// 鼠标右键弹起
        /// </summary>
        public event MouseEventHandler RightUp;
        ///// <summary>
        ///// 鼠标右键单击
        ///// </summary>
        //public event MouseEventHandler RightClick;
        /// <summary>
        /// 鼠标滚动
        /// </summary>
        public event MouseEventHandler MouseWheel;
        /// <summary>
        /// 鼠标移动
        /// </summary>
        public event MouseEventHandler MouseMove;
        #endregion

        #region 重写基类
        protected override int HookID
        {
            get
            {
                return WH_MOUSE_LL;
            }
        }
        /// <summary>  
        /// 墨认的构造函数构造当前类的实例.  
        /// </summary>  
        public MouseHook() : base()
        {
            DoubleClickTime = User32.GetDoubleClickTime();

        }

        public override void Stop()
        {
            base.Stop();
        }

        public override int HookProcEvent(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果正常运行并且用户要监听鼠标的消息  
            if (nCode >= 0)
            {
                MouseButtons button = MouseButtons.None;
                int clickCount = 0;
                int delta = 0;
                MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                MouseEventArgs e;
                switch ((int)wParam)
                {
                    case Const.WM_LBUTTONDOWN:
                        LastMouseDownTimeSpan = Environment.TickCount;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        LeftDown?.Invoke(this, e);
                        break;
                    case Const.WM_LBUTTONUP:
                        LastMouseDownTimeSpan = Environment.TickCount;
                        button = MouseButtons.Left;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        LeftUp?.Invoke(this, e);
                        break;
                    case Const.WM_MBUTTONDOWN:
                        button = MouseButtons.Middle;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        MiddleDown?.Invoke(this, e);
                        break;
                    case Const.WM_MBUTTONUP:
                        button = MouseButtons.Middle;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        MiddleUp?.Invoke(this, e);
                        break;
                    case Const.WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        RightDown?.Invoke(this, e);
                        break;
                    case Const.WM_RBUTTONUP:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        RightUp?.Invoke(this, e);
                        break;
                    case Const.WM_MOUSEWHEEL:
                        if (!mouseLeftDown)
                        {
                            button = MouseButtons.Middle;
                            clickCount = 0;
                            delta = MyMouseHookStruct.hWnd >> 16;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MouseWheel?.Invoke(this, e);
                        }
                        break;
                    case Const.WM_MOUSEMOVE:
                        button = MouseButtons.None;
                        clickCount = 0;
                        e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                        MouseMove?.Invoke(this, e);
                        break;
                }
            }

            return CallNextHookEx(hwndHook, nCode, wParam, lParam);
        }

        public static uint HiWord(IntPtr ptr)
        {
            if (((uint)ptr & 0x80000000)== 0x80000000)
             return ((uint)ptr >> 16);
             else
                return ((uint)ptr >> 16) & 0xFFFF;
        }


        #endregion

    }
}
