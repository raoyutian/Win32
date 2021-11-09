using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Win32
{
    public static class Delegates
    {
        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);
    }
}
