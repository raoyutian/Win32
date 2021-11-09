using System.Windows.Forms;


namespace Win32.Input
{
    /// <summary>
    /// Mouse
    /// </summary>
    public static  partial class Mouse
    {
        #region Mouse

        /// <summary>
        ///   MoveTo CursorPos To ,x ,y 
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        public static void MoveTo(int x, int y)
        {
            User32.SetCursorPos(x, y);
            User32.mouse_event(Const.MOUSEEVENTF_MOVE , 5, 0, 0, 0);
            User32.mouse_event(Const.MOUSEEVENTF_MOVE, -5, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  LeftClick 
        /// </summary>
        public static void LeftClick()
        {
            Wait.Delay(50);
            User32.mouse_event(Const.MOUSEEVENTF_ABSOLUTE | Const.MOUSEEVENTF_LEFTDOWN | Const.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  LeftClick 
        /// </summary>
        public static void LeftClick(int x, int y)
        {
            MoveTo(x, y);
            LeftClick();
        }
        /// <summary>
        ///  Mouse LeftDBClick 
        /// </summary>
        public static void LeftDBClick()
        {
            LeftClick();
         
            LeftClick();
        }
        /// <summary>
        ///  Mouse LeftDBClick 
        /// </summary>
        public static void LeftDBClick(int x, int y)
        {
            MoveTo(x, y);
            LeftClick();
          
            LeftClick();
        }

        /// <summary>
        ///  Mouse  RightClick 
        /// </summary>
        public static void RightClick()
        {
            User32.mouse_event(Const.MOUSEEVENTF_ABSOLUTE | Const.MOUSEEVENTF_RIGHTDOWN | Const.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  RightClick 
        /// </summary>
        public static void RightClick(int x, int y)
        {
            MoveTo(x, y);
            RightClick();
        }

        /// <summary>
        ///  Mouse  RightClick 
        /// </summary>
        public static void RightDBClick()
        {
            RightClick();
          
            RightClick();
        }
        /// <summary>
        ///  Mouse  RightClick 
        /// </summary>
        public static void RightDBClick(int x, int y)
        {
            MoveTo(x, y);
            RightClick();
        
            RightClick();
        }

        /// <summary>
        ///  Mouse  MiddleClick 
        /// </summary>
        public static void MiddleClick()
        {
            User32.mouse_event(Const.MOUSEEVENTF_ABSOLUTE | Const.MOUSEEVENTF_MIDDLEDOWN | Const.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
        }
        /// <summary>
        ///  Mouse  MiddleClick 
        /// </summary>
        public static void MiddleClick(int x, int y)
        {
            MoveTo(x, y);
            MiddleClick();
        }

        /// <summary>
		///移动鼠标位置（相对）
		/// </summary>
		/// <param name="dx"></param>
		/// <param name="dy"></param>
		public static void MouseMove(int dx, int dy)
        {
            POINT point = new POINT();
            User32.GetCursorPos(ref point);
            MoveTo(point.X + dx, point.Y + dy);
        }

        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="x1">X coords of the start position</param>
        /// <param name="y1">Y coords of the start position</param>
        /// <param name="x2">X coords of the end position</param>
        /// <param name="y2">Y coords of the end position</param>
        /// <param name="button">The button to be held down</param>
        public static void MouseDrag(int x1, int y1, int x2, int y2, MouseButtons button = MouseButtons.Left)
        {
            MoveTo(x1, y1);
            MouseDown(button);
            MoveTo(x2, y2);
            MouseUp(button);
        }

        /// <summary>
        /// Press down a mouse button
        /// </summary>
        /// <param name="button">The button to be pressed</param>
        public static void MouseDown(MouseButtons button)
        {
            if ((button & MouseButtons.Left) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            }

            if ((button & MouseButtons.Right) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            }

            if ((button & MouseButtons.Middle) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            }
        }


        /// <summary>
        /// Release a mouse button
        /// </summary>
        /// <param name="button">The button to be released</param>
        public static void MouseUp(MouseButtons button)
        {
            if ((button & MouseButtons.Left) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }

            if ((button & MouseButtons.Right) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }

            if ((button & MouseButtons.Middle) != 0)
            {
                User32.mouse_event(Const.MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Click a mouse button
        /// </summary>
        /// <param name="button">The button to be clicked</param>
        public static void MouseClick(MouseButtons button)
        {
            MouseDown(button);
            MouseUp(button);
        }

        /// <summary>
        /// Double-click a mouse button
        /// </summary>
        /// <param name="button">The button to be clicked</param>
        public static void MouseDblClick(MouseButtons button)
        {
            MouseClick(button);
           
            MouseClick(button);
        }
        /// <summary>
        /// Scroll the mouse wheel
        /// </summary>
        /// <param name="scrollUp">Scroll direction</param>
        public static void MouseWheel(bool scrollUp)
        {
            User32.mouse_event(Const.MOUSEEVENTF_WHEEL, 0, 0, scrollUp ? 120 : -120, 0);
        }
        /// <summary>
        /// 滚动鼠标
        /// </summary>
        /// <param name="delta">滚动的数值</param>
        public static void MouseWheel(int delta)
        {
            User32.mouse_event(Const.MOUSEEVENTF_WHEEL, 0, 0, delta, 0);
        }
 
        /// <summary>
        ///获取当前鼠标点的绝对位置（屏幕坐标）
        /// </summary>
        /// <returns></returns>
        public static System.Drawing.Point GetCursorPos()
        {
            POINT point = new POINT();
            User32.GetCursorPos(ref point);
            return new System.Drawing.Point(point.X, point.Y);
        }
        #endregion
    }
}
