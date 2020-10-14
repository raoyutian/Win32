using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Win32.API;

namespace Win32.Hooks
{
   /// <summary>
   /// 键盘钩子
   /// </summary>
    public class KeyboardHook:HookBase
    {
        //键盘事件  
        public event KeyEventHandler OnKeyUp;
        public event KeyEventHandler OnKeyDown;
       
        #region 重写基类
        internal override int HookID
        {
            get
            {
                return WH_KEYBOARD_LL;
            }
        }
        public override int HookProcEvent(int nCode, Int32 wParam, IntPtr lParam)
        {
            //如果正常运行并且用户要监听键盘的消息  
            if (nCode >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                KeyEventArgs keyEventArgs = new KeyEventArgs(vkCode.ToString().ToEnum<Keys>());
                switch (wParam)
                {
                    case Win32Api.WM_KEYDOWN:
                        OnKeyDown?.Invoke(this, keyEventArgs);
                        break;
                    case Win32Api.WM_KEYUP:
                        OnKeyUp?.Invoke(this, keyEventArgs);
                        break;
                }
            }
            return CallNextHookEx(hwndHook, nCode, wParam, lParam);
        }
        #endregion

    }
}
