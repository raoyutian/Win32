namespace Win32.ComputerInfo
{
    /// <summary>
    /// 网卡 
    /// </summary>
    public class NetworkAdapterInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }="";
        /// <summary>
        /// 网卡地址
        /// </summary>
        public string MacAddress { get; set; } = "";
    }
}
