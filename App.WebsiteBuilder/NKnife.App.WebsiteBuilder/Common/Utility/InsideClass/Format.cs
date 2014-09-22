using System;
using System.Text;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Format
        {
            /// <summary>
            /// 以普通中文形式显示字节数
            /// </summary>
            /// <param name="count">字节数</param>
            /// <param name="kAfterPointCount">小数位</param>
            static public string FormatBytes(int count, int kAfterPointCount)
            {
                float k = ((float)count) / 1024;
                if (k < 1)
                {
                    return count + "字节";
                }

                float m = ((float)k) / 1024;
                if (m < 1)
                {
                    string format = "0";
                    if (kAfterPointCount > 0)
                    {
                        format = "0.";
                        for (int i = 0; i < kAfterPointCount; i++)
                        {
                            format += "0";
                        }
                    }
                    return k.ToString(format) + "K字节";
                }

                return m.ToString("0.00") + "M字节";
            }

            /// <summary>
            /// 以普通的中文格式显示时间间隔
            /// </summary>
            static public string FormatTimes(TimeSpan ts)
            {
                StringBuilder sb = new StringBuilder();
                if (ts.TotalHours >= 1.0)
                {
                    sb.Append((int)ts.TotalHours).Append("小时");
                }
                if (ts.Minutes != 0)
                {
                    sb.Append(ts.Minutes).Append("分");
                }
                if (ts.Seconds != 0 || sb.Length == 0)
                {
                    sb.Append(ts.Seconds).Append("秒");
                }

                return sb.ToString();
            }
        }
    }
}