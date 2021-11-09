using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Win32
{
    /// <summary>
    /// GDI相关API
    /// </summary>
    public  static partial class Gdi32
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(int hdc, int nIndex);
       
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int Rectangle(int hdc, int X1, int Y1, int X2, int Y2);
       
       
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SelectObject(int hdc, int hObject);
       
        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int SetROP2(int hdc, int nDrawMode);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hObject, int nXDest, int nYDest, int nWidth,
           int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc,
            TernaryRasterOperations dwRop);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn ( int nLeftRect,  int nTopRect,  int nRightRect,  int nBottomRect,int nWidthEllipse,  int nHeightEllipse   );

        [DllImport("gdi32")]
        public static extern int DeleteObject(IntPtr obj);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int CreatePen(int nPenStyle, int nWidth, int crColor);

 
 

		[DllImport("Gdi32.dll", EntryPoint = "GetPixel")]
		public static extern int Win32GetPixel(IntPtr hdc, int x, int y);

		 

		/// <summary>
		/// Compose a RGB value
		/// </summary>
		/// <param name="r">The red component</param>
		/// <param name="g">The green component</param>
		/// <param name="b">The blue component</param>
		/// <returns>Return the composed RGB value</returns>
		public static int RGB(byte r, byte g, byte b)
		{
			return ((int)r << 16) | ((short)g << 8) | b; // It's C# .net System.Drawing.Color way
		}

		/// <summary>
		/// Extract the red component of a RGB value
		/// </summary>
		/// <param name="color">The RGB value</param>
		/// <returns>Return the red component</returns>
		public static byte GetRValue(int color)
		{
			return (byte)(color >> 16);
		}

		/// <summary>
		/// Extract the green component of a RGB value
		/// </summary>
		/// <param name="color">The RGB value</param>
		/// <returns>Return the green component</returns>
		public static byte GetGValue(int color)
		{
			return (byte)(((short)color) >> 8);
		}

		/// <summary>
		/// Extract the blue component of a RGB value
		/// </summary>
		/// <param name="color">The RGB value</param>
		/// <returns>Return the blue component</returns>
		public static byte GetBValue(int color)
		{
			return (byte)color;
		}

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);

        [DllImport("gdi32.dll")]
        public static extern bool SetStretchBltMode(IntPtr hdc, StretchMode iStretchMode);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(
            IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
            IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hDc);

      


    }
}
