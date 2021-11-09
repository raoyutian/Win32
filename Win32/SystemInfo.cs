using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32
{  /// <summary>
      ///系统信息封装
      /// </summary>
        public static class SystemInfo
        {
            #region DeviceCaps常量
            const int HORZRES = 8;
            const int VERTRES = 10;
            const int LOGPIXELSX = 88;
            const int LOGPIXELSY = 90;
            const int DESKTOPVERTRES = 117;
            const int DESKTOPHORZRES = 118;
            #endregion

            /// <summary>
            /// 获取系统显示宽度缩放百分比
            /// </summary>
            public static float ScaleX
            {
                get
                {
                    IntPtr hdc = User32.GetDC(IntPtr.Zero);
                    int t = Gdi32.GetDeviceCaps(hdc.ToInt32(), DESKTOPHORZRES);
                    int d = Gdi32.GetDeviceCaps(hdc.ToInt32(), HORZRES);
                    float ScaleX = (float)t / (float)d;
                    User32.ReleaseDC(IntPtr.Zero, hdc);
                    return ScaleX;
                }
            }

        }
 }
 
