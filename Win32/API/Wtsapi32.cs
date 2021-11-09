using System;
using System.Runtime.InteropServices;

namespace Win32
{
    public static class Wtsapi32
    {
        public const uint INVALID_SESSION_ID = 0xFFFFFFFF;
        public static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        public enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WTS_SESSION_INFO
        {
            public readonly UInt32 SessionID;

            [MarshalAs(UnmanagedType.LPStr)]
            public readonly String pWinStationName;

            public readonly WTS_CONNECTSTATE_CLASS State;
        }

        [DllImport("Wtsapi32.dll")]
        public static extern uint WTSQueryUserToken(uint SessionId, ref IntPtr phToken);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern int WTSEnumerateSessions(
            IntPtr hServer,
            int Reserved,
            int Version,
            ref IntPtr ppSessionInfo,
            ref int pCount);

        [DllImport("userenv.dll", SetLastError = true)]
        public static extern bool CreateEnvironmentBlock(ref IntPtr lpEnvironment, IntPtr hToken, bool bInherit);

        [DllImport("userenv.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSIsChildSessionsEnabled(out bool isEnabled);

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSEnableChildSessions(bool enable);

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSGetChildSessionId(out uint SessionId);
      
        [DllImport("Wtsapi32.dll", SetLastError = true)]
        public static extern bool WTSLogoffSession(IntPtr hServer, uint SessionId, bool bWait);
        [DllImport("wtsapi32.dll")]
        public static extern void WTSCloseServer(IntPtr hServer);
        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern IntPtr WTSOpenServer(string pServerName);
 
    }
}
