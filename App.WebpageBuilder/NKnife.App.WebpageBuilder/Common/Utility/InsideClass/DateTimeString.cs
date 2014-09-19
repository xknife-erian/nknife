using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class DateTimeString
        {
            static public string GetDateTimeFormat(string intDateTime)
            {
                switch (intDateTime)
                {
                    case "1":
                        return "yy/M/d";
                    case "2":
                        return "yyyy年M月d日";
                    case "3":
                        return "MM/dd/yyyy";
                    case "4":
                        return "MM/d/yy";
                    case "5":
                        return "yyyy-MM-dd";
                    case "6":
                        return "d/M/yy";
                    case "7":
                        return "d/MM/yy";
                    case "8":
                        return "dd.MM.yyyy";
                    case "9":
                        return "dd.MM.yy";
                    case "10":
                        return "d-MM-yyyy";
                    default:
                        return "yyyy-MM-dd";
                }
            }
            static public string GetDateTimeWeekFormat(string intDateTime, DayOfWeek dateWeek)
            {
                switch (intDateTime)
                {
                    case "1":
                        string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                        return Day[System.Convert.ToInt32(dateWeek)];
                    case "2":
                        string[] EnglishWeek = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                        return EnglishWeek[System.Convert.ToInt32(dateWeek)];
                    case "3":
                        string[] SubEnglishWeek = new string[] { "Sun", "Mon", "Tus", "Wed", "Thur", "Fri", "Sat" };
                        return SubEnglishWeek[System.Convert.ToInt32(dateWeek)];
                    default:
                        return string.Empty;
                }
            }
            static public string GetDateTimeTimeFormat(string intDateTime, DateTime dateTime)
            {
                switch (intDateTime)
                {
                    case "1":
                        string tempDateTime = dateTime.ToString("hh:mm");
                        if (dateTime.Hour > 12)
                        {
                            tempDateTime += " PM";
                        }
                        else
                        {
                            tempDateTime += " AM";
                        }
                        return tempDateTime;
                    case "2":
                        return dateTime.ToString("HH::mm");
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
