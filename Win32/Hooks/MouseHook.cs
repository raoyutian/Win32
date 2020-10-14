using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Win32.API;

namespace Win32.Hooks
{
 /// <summary>
 /// 鼠标钩子
 /// </summary>
    public class MouseHook: HookBase
    {

        #region 常量
        //声明鼠标钩子的封送结构类型  
        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public Win32Api.POINT pt;
            public int hWnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        #endregion

        //全局的事件  
        public event MouseEventHandler LeftDown;
        public event MouseEventHandler LeftUp;
        public event MouseEventHandler LeftDBClick;

        public event MouseEventHandler MiddleDown;
        public event MouseEventHandler MiddleUp;
        public event MouseEventHandler MiddleDBClick;


        public event MouseEventHandler RightDown;
        public event MouseEventHandler RightUp;
        public event MouseEventHandler RightDBClick;

        public event MouseEventHandler MouseWheel;
        public event MouseEventHandler MouseMove;

        #region 重写基类
        internal override int HookID
        {
            get
            {
                return WH_MOUSE_LL;
            }
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
                switch (wParam)
                {
                    case Win32Api.WM_LBUTTONDBLCLK:
                        if (LeftDBClick != null)
                        {
                            button = MouseButtons.Left;
                            clickCount = 2;
                             e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            LeftDBClick.Invoke(this,e);
                         }
                        break;
                    case Win32Api. WM_LBUTTONDOWN:
                      
                        if (LeftDown != null)
                        {
                            button = MouseButtons.Left;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            LeftDown.Invoke(this, e);
                        }

                        break;
                    case Win32Api.WM_LBUTTONUP:
                        if (LeftUp != null)
                        {
                            button = MouseButtons.Left;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            LeftUp.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_MBUTTONDBLCLK:
                        if (MiddleDBClick != null)
                        {
                            button = MouseButtons.Middle;
                            clickCount = 2;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MiddleDBClick.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_MBUTTONDOWN:
                        if (MiddleDown != null)
                        {
                            button = MouseButtons.Middle;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MiddleDown.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_MBUTTONUP:
                        if (MiddleUp != null)
                        {
                            button = MouseButtons.Middle;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MiddleUp.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_RBUTTONDBLCLK:
                        if (RightDBClick != null)
                        {
                            button = MouseButtons.Right;
                            clickCount = 2;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            RightDBClick.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_RBUTTONDOWN:
                        if (RightDown != null)
                        {
                            button = MouseButtons.Right;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            RightDown.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_RBUTTONUP:
                        if (RightUp != null)
                        {
                            button = MouseButtons.Right;
                            clickCount = 1;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            RightUp.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_MOUSEWHEEL:
                        delta = Marshal.ReadInt32(lParam);
                        if (MouseWheel != null)
                        {
                            button = MouseButtons.Middle;
                            clickCount = 0;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MouseWheel.Invoke(this, e);
                        }
                        break;
                    case Win32Api.WM_MOUSEMOVE:
                        
                        if (MouseMove != null)
                        {
                            button = MouseButtons.None;
                            clickCount = 0;
                            e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, delta);
                            MouseMove.Invoke(this, e);
                        }
                        break;
                }
            }
            return CallNextHookEx(hwndHook, nCode, wParam, lParam);
        }
        #endregion

    }

}
