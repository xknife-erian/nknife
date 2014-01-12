using System;
using System.Runtime.InteropServices;

namespace NKnife.Platform.Win32
{
    /// <summary>
    /// 系统操作如重启，关机等
    /// </summary>
    public class SystemUtil
    {
        #region API
        [StructLayout(LayoutKind.Sequential, Pack = 1)]

        internal struct TokPriv1Luid
        {
            public int Count;

            public long Luid;

            public int Attr;
        }

        [DllImport("user32")]
        internal static extern long ExitWindowsEx(long uFlags, long dwReserved);


        [DllImport("kernel32.dll", ExactSpelling = true)]

        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]

        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,

            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool ExitWindowsEx(int flg, int rea);

        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;

        internal const int TOKEN_QUERY = 0x00000008;

        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        internal const int EWX_LOGOFF = 0x00000000;

        internal const int EWX_SHUTDOWN = 0x00000001;

        internal const int EWX_REBOOT = 0x00000002;

        internal const int EWX_FORCE = 0x00000004;

        internal const int EWX_POWEROFF = 0x00000008;

        private static void DoExitWin(int flg)
        {
            TokPriv1Luid tp;

            var hproc = GetCurrentProcess();

            var htok = IntPtr.Zero;

            OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);

            tp.Count = 1;

            tp.Luid = 0;

            tp.Attr = SE_PRIVILEGE_ENABLED;

            LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);

            AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

            ExitWindowsEx(flg, 0);
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 关机
        /// </summary>
        public static void CloseComputor()
        {
            DoExitWin(EWX_SHUTDOWN);
        }

        /// <summary>
        /// 重启
        /// </summary>
        public static  void RestartComputer()
        {
            DoExitWin(EWX_REBOOT);
        }
        #endregion
    }
}
