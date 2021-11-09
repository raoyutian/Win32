using System;
using System.Windows.Forms;
namespace Win32.Input
{
    /// <summary>
    /// 时间相关类
    /// </summary>
    public static partial class Wait
    {
     /// <summary>
     /// 延时函数
     /// </summary>
     /// <param name="milliSecond">延时毫秒数</param>
        public static void Delay(uint milliSecond = 1000)
        {
            if (milliSecond <= 0) milliSecond = 0;
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                  Application.DoEvents();
                #region 当没有引用System.Windows.Forms 时，纯WPF可用下面代码
                //Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,new Action(delegate { }));
                #endregion
            }
        }
    }
}
namespace Win32
{
    public  class App
    {/// <summary>
     /// 响应CPU消息处理，避免UI线程假死
     /// </summary>
        public static void DoEvents()
        {
           Application.DoEvents();
            #region 当没有引用System.Windows.Forms 时，纯WPF可用下面代码
            //Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,new Action(delegate { }));
            #endregion
        }
    }
}



