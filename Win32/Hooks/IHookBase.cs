using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Win32.Hooks
{
  /// <summary>
  /// 钩子接口
  /// </summary>
    public interface IHookBase
    {
      
        void Start();

         void Stop();
        int HookProcEvent(int nCode, Int32 wParam, IntPtr lParam);
    }

}
