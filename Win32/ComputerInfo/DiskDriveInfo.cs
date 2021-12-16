namespace Win32.ComputerInfo
{
    /// <summary>
    /// 硬盘 
    /// </summary>
    public class DiskDriveInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; } = "";
        /// <summary>
        /// 容量
        /// </summary>
        public ulong Size { get; set; } = 0;
    }
}
