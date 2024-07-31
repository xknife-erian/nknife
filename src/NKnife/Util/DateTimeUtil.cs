using System;
using System.Diagnostics;
using System.Text;

namespace NKnife.Util
{
    /// <summary>
    /// 有关DateTime的扩展方法
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>月份名称的字符串数组(英语)
        /// </summary>	
        public static string[] Monthes
        {
            get { return new[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"}; }
        }

        /// <summary>获取一个时间的当天的开始时间
        /// </summary>
        /// <param name="time">The begin time. eg:20110303</param>
        /// <returns></returns>
        public static DateTime GetDateTime(string time)
        {
            var sb = new StringBuilder(time); //20110303
            sb.Insert(6, '-').Insert(4, '-');
            sb.Append(" 00:00:00");
            return DateTime.Parse(sb.ToString());
        }

        /// <summary>返回两个日期之间的时间间隔（y：年份间隔、M：月份间隔、d：天数间隔、h：小时间隔、m：分钟间隔、s：秒钟间隔、ms：微秒间隔）     
        /// </summary>     
        /// <param name="date1">开始日期</param>     
        /// <param name="date2">结束日期</param>     
        /// <param name="interval">间隔标志</param>     
        /// <returns>返回间隔标志指定的时间间隔</returns>     
        public static int DateDiff(DateTime date1, DateTime date2, string interval)
        {
            const double DBL_YEAR_LEN = 365; //年的长度，365天     
            const double DBL_MONTH_LEN = (365/12); //每个月平均的天数     
            TimeSpan objT = date2.Subtract(date1);
            switch (interval)
            {
                case "y": //返回日期的年份间隔     
                    return Convert.ToInt32(objT.Days/DBL_YEAR_LEN);
                case "M": //返回日期的月份间隔     
                    return Convert.ToInt32(objT.Days/DBL_MONTH_LEN);
                case "d": //返回日期的天数间隔     
                    return objT.Days;
                case "h": //返回日期的小时间隔     
                    return objT.Hours;
                case "m": //返回日期的分钟间隔     
                    return objT.Minutes;
                case "s": //返回日期的秒钟间隔     
                    return objT.Seconds;
                case "ms": //返回时间的微秒间隔     
                    return objT.Milliseconds;
                default:
                    break;
            }
            return 0;
        }
        
        /// <summary>返回与当前时间相差的秒数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string time, int sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddSeconds(sec);
            if (ts.TotalSeconds > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalSeconds < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalSeconds;
        }

        /// <summary>返回与当前时间相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalMinutes < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalMinutes;
        }

        /// <summary>返回与当前时间相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (string.IsNullOrEmpty(time))
                return 1;
            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
            {
                return int.MaxValue;
            }
            else if (ts.TotalHours < int.MinValue)
            {
                return int.MinValue;
            }
            return (int) ts.TotalHours;
        }

        /// <summary>计算本周的起止日期
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void ThisWeekRange(ref DateTime start, ref DateTime end)
        {
            DateTime dt = DateTime.Today;
            int weekNow = Convert.ToInt32(dt.DayOfWeek);
            int dayDiff = (-1)*weekNow;
            int dayAdd = 6 - weekNow;
            start = DateTime.Now.AddDays(dayDiff).Date;
            end = DateTime.Now.AddDays(dayAdd).Date;
        }

        /// <summary>根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void WeekRange(int year, int weekOrder, ref DateTime start, ref DateTime end)
        {
            //非法参数
            if (year <= 0 || weekOrder <= 0 || weekOrder > 53)
            {
                return;
            }

            //当年的第一天
            var firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1)*firstOfWeek;
            int dayAdd = 6 - firstOfWeek;
            start = firstDay.AddDays(dayDiff).Date;
            end = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1)*7;
                start = start.AddDays(addDays);
                end = end.AddDays(addDays);
            }
        }

        /// <summary>返回今天(零时)
        /// </summary>
        /// <returns></returns>
        public static DateTime Today()
        {
            string date = DateTime.Now.ToLongDateString();
            const string time = "00:00:00";
            return DateTime.Parse($"{date} {time}");
        }

        /// <summary>指定两个时间值是否是一天的时间，不考虑该值所含的“小时，分，秒”等
        /// </summary>
        /// <param name="leftDay">The left day.</param>
        /// <param name="rightDay">The right day.</param>
        /// <returns>
        ///   <c>true</c> if [is same day] [the specified left day]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSameDay(DateTime leftDay, DateTime rightDay)
        {
            return (leftDay.Year == rightDay.Year && leftDay.Month == rightDay.Month && leftDay.Day == rightDay.Day);
        }
    }
}