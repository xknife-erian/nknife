using System.Runtime.InteropServices;

namespace NKnife.Platform.Win32
{
    /// <summary>
    /// 该类积累了常见的win32宏，定义，结构，函数等，供调用windows api时使用
    /// </summary>
    public class Win32Defination
    {
        #region WINUSER.H

        public static int WM_USER = 0x0400;

        #endregion

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int SetLocalTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetLocalTime(ref SYSTEMTIME lpSystemTime);


    }

    #region WINBASE.H
    public struct SYSTEMTIME
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }

    //public struct SYSTEMTIME
    //{
    //    public int wYear;
    //    public int wMonth;
    //    public int wDayOfWeek;
    //    public int wDay;
    //    public int wHour;
    //    public int wMinute;
    //    public int wSecond;
    //    public int wMilliseconds;
    //}
    #endregion

    #region WINDEF.H
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    } 
    #endregion
}
