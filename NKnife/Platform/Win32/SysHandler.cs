using System.Runtime.InteropServices;

namespace NKnife.Platform.Win32
{
    public class SysHandler
    {
        //struct for date/time apis 
        public struct SystemTime
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

        //imports SetLocalTime function from kernel32.dll 
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int SetLocalTime(ref SystemTime lpSystemTime);

        public static void SetSysTime(short day, short month, short year, short hour, short minute, short second)
        {
            try
            {
                SystemTime systNew = new SystemTime();

                // 设置属性 
                systNew.wDay = day;
                systNew.wMonth = month;
                systNew.wYear = year;
                systNew.wHour = hour;
                systNew.wMinute = minute;
                systNew.wSecond = second;

                // 调用API，更新系统时间 
                SetLocalTime(ref systNew);
            }
            catch
            {
            }
        }
    }
}
