using System;

namespace Win32.Input
{
    public static class Screen
    {
        /// <summary>
        /// 当前屏幕的缩放比例
        /// </summary>
        public static double Scaling
        {
            get
            {
                SystemDpi(out int x, out int y);
                return GetScaling(x);
            }
        }

        ///获取当前系统的dpi数值
        private static void SystemDpi(out int x, out int y)
        {
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                x = (int)g.DpiX;
                y = (int)g.DpiY;
                g.Dispose();
            }
        }

        ///根据当前系统dpi数值匹配 当前系统的桌面缩放比例，x或y都一样
        private static double GetScaling(int dpiIndex)
        {
            switch (dpiIndex)
            {
                case 96:
                    return 1;
                case 120:
                    return 1.25;
                case 144:
                    return 1.5;
                case 168:
                    return 1.75;
                case 192:
                    return 2;
            }
            return 1;
        }
    }
}