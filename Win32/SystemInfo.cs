using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

using Win32.ComputerInfo;
using Win32.Input;

namespace Win32
{  /// <summary>
   ///系统信息封装
   /// </summary>
    public class SystemInfo
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

        #region 计算机硬件信息
        /// <summary>
        /// 内存
        /// </summary>
        public MemoryInfo memory;
        /// <summary>
        /// 处理器
        /// </summary>
        public ProcessorInfo processor;
        /// <summary>
        /// 主板
        /// </summary>
        public BaseBoard baseBoard;
        /// <summary>
        /// BIOS
        /// </summary>
        public BIOS bios;
        /// <summary>
        /// 操作系统
        /// </summary>
        public OperatingSystemInfo operatingSystem;
        /// <summary>
        /// 硬盘列表
        /// </summary>
        public List<DiskDriveInfo> diskDrivelist;
        /// <summary>
        /// 网卡列表
        /// </summary>
        public List<NetworkAdapterInfo> NetworkAdapterlist;
        /// <summary>
        /// 计算机名
        /// </summary>
        public string ComputerName => System.Environment.MachineName;


        public SystemInfo()
        {
            var osQuery = new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
            var osSearcher = new ManagementObjectSearcher(osQuery);
            foreach (var os in osSearcher.Get())
            {
                memory.TotalPhysicalMemory = Convert.ToUInt64(os["TotalVisibleMemorySize"]) / 1024 / 1024;
                memory.AvailablePhysicalMemory = Convert.ToUInt64(os["FreePhysicalMemory"]) / 1024 / 1024;
                memory.TotalVirtualMemory = Convert.ToUInt64(os["TotalVirtualMemorySize"]) / 1024 / 1024;
                memory.AvailableVirtualMemory = Convert.ToUInt64(os["FreeVirtualMemory"]) / 1024 / 1024;
            }

            //处理器
            ManagementClass cimobjectProcessor = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mocProcessor = cimobjectProcessor.GetInstances();
            foreach (ManagementObject mo in mocProcessor)
            {
                if (mo["ProcessorId"] != null) processor.SerialNumber = mo.Properties["ProcessorId"].Value.ToString().Trim();
                if (mo["Name"] != null) processor.Name = mo.Properties["Name"].Value.ToString().Trim();
                if (mo["Manufacturer"] != null) processor.Manufacturer = mo.Properties["Manufacturer"].Value.ToString().Trim();

            }

            //操作系统
            ManagementClass cimobjectOperatingSystem = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection mocOperatingSystem = cimobjectOperatingSystem.GetInstances();
            if (System.Environment.Is64BitOperatingSystem) operatingSystem.OSLevel = "64";
            foreach (ManagementObject mo in mocOperatingSystem)
            {
                if (mo["SerialNumber"] != null) operatingSystem.SerialNumber = mo.Properties["SerialNumber"].Value.ToString().Trim();
                if (mo["Caption"] != null) operatingSystem.Caption = mo.Properties["Caption"].Value.ToString().Trim();

                if (mo["InstallDate"] != null) operatingSystem.InstallDate = mo.Properties["InstallDate"].Value.ToString().Trim();
                if (mo["LastBootUpTime"] != null) operatingSystem.LastBootUpTime = mo.Properties["LastBootUpTime"].Value.ToString().Trim();
                if (mo["LocalDateTime"] != null) operatingSystem.LocalDateTime = mo.Properties["LocalDateTime"].Value.ToString().Trim();

            }
            //获取硬盘
            ManagementClass cimobjectDiskDrive = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection mocDiskDrive = cimobjectDiskDrive.GetInstances();
            diskDrivelist = new List<DiskDriveInfo>();
            foreach (ManagementObject mo in mocDiskDrive)
            {
                DiskDriveInfo diskDrive = new DiskDriveInfo();
                if (mo["Caption"] != null) diskDrive.Name = mo.Properties["Caption"].Value.ToString().Trim();
                if (mo["SerialNumber"] != null) diskDrive.SerialNumber = mo.Properties["SerialNumber"].Value.ToString().Trim();
                if (mo["Size"] != null) diskDrive.Size = (ulong)mo.Properties["Size"].Value / 1024 / 1024 / 1024;
                if (!diskDrive.Name.StartsWith("USBKey")) diskDrivelist.Add(diskDrive);

            }
            //获取网卡地址
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection mocNetworkAdapter = mc.GetInstances();
            NetworkAdapterlist = new List<NetworkAdapterInfo>();
            foreach (ManagementObject mo in mocNetworkAdapter)
            {
                NetworkAdapterInfo networkAdapterInfo = new NetworkAdapterInfo();
                if (mo["Caption"] != null) networkAdapterInfo.Name = mo["Caption"].ToString().Trim();
                if (mo["MacAddress"] != null) networkAdapterInfo.MacAddress = mo["MacAddress"].ToString().Trim();
                if (!networkAdapterInfo.Name.Contains("VMware")
                    && !networkAdapterInfo.Name.Contains("Microsoft")
                    && !networkAdapterInfo.Name.Contains("Virtual")
                    && !networkAdapterInfo.Name.Contains("Miniport")
                    && networkAdapterInfo.MacAddress.Contains(":")
                    )
                {
                    NetworkAdapterlist.Add(networkAdapterInfo);
                }
            }
            //主板 
            ManagementClass cimobjectBaseBoard = new ManagementClass("WIN32_BaseBoard");
            ManagementObjectCollection mocBaseBoard = cimobjectBaseBoard.GetInstances();
            foreach (ManagementObject mo in mocBaseBoard)
            {
                if (mo["SerialNumber"] != null) baseBoard.SerialNumber = mo.Properties["SerialNumber"].Value.ToString().Trim();
                if (mo["Name"] != null) baseBoard.Name = mo.Properties["Name"].Value.ToString().Trim();
                if (mo["Manufacturer"] != null) baseBoard.Manufacturer = mo.Properties["Manufacturer"].Value.ToString().Trim();
                if (mo["Product"] != null) baseBoard.Product = mo.Properties["Product"].Value.ToString().Trim();
            }

            ManagementClass cimobjectBIOS = new ManagementClass("WIN32_BIOS");
            ManagementObjectCollection mocBIOS = cimobjectBIOS.GetInstances();

            foreach (ManagementObject mo in mocBIOS)
            {
                if (mo["SerialNumber"] != null) bios.SerialNumber = mo.Properties["SerialNumber"].Value.ToString().Trim();
                if (mo["Name"] != null) bios.Name = mo.Properties["Name"].Value.ToString().Trim();
                if (mo["Manufacturer"] != null) bios.Manufacturer = mo.Properties["Manufacturer"].Value.ToString().Trim();
                if (mo["ReleaseDate"] != null) bios.ReleaseDate = mo.Properties["ReleaseDate"].Value.ToString().Trim();
            }
        }
        #endregion

        #region CPU和内存使用率
        static PerformanceCounter[] counters = new PerformanceCounter[System.Environment.ProcessorCount];
        /// <summary>
        /// CPU使用率
        /// </summary>
        public static void CpuUsageOnChanged(Action<double> action, ref bool cancel)
        {
            int count = counters.Length * 2;
            for (int i = 0; i < counters.Length; i++)
            {
                counters[i] = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
            }
            while (!cancel)
            {
                float ddd = 0;
                for (int i = 0; i < counters.Length; i++)
                {
                    float f = counters[i].NextValue();
                    ddd += f;
                }
                Wait.Delay(1000);
                action(Math.Round(ddd / count, 0));
            }

        }

        /// <summary>
        /// 内存使用率
        /// </summary>
        public static double MemoryUsage
        {
            get
            {
                var osQuery = new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
                var osSearcher = new ManagementObjectSearcher(osQuery);
                ulong PhysicalMemoryTotal = 0u;
                ulong PhysicalMemoryFree = 0u;
                foreach (var os in osSearcher.Get())
                {
                    PhysicalMemoryTotal = Convert.ToUInt64(os["TotalVisibleMemorySize"]);
                    PhysicalMemoryFree = Convert.ToUInt64(os["FreePhysicalMemory"]);
                }
                return Math.Round((double)(PhysicalMemoryTotal - PhysicalMemoryFree) / PhysicalMemoryTotal * 100, 0);
            }
        }

        #endregion

    }
}

