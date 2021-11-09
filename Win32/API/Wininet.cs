using System.Runtime.InteropServices;

namespace Win32
{
    /// <summary>
    /// Wininet相关API
    /// </summary>
    public static partial class Wininet
    {
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(int Description, int ReservedValue);
    }
}
