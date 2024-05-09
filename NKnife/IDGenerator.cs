using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NKnife.Interface;
using NKnife.Maths;

namespace NKnife
{
    /// <summary>
    ///     ID生成器
    /// </summary>
    public class IdGenerator
    {
        private const string CHAR_SET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int MAX_PER_MILLISECOND = 99; // 每毫秒最多生成的ID数  
        private static readonly int s_charSetLength = CHAR_SET.Length;
        private static int s_sequenceNumber;
        private static string s_lastIdPrefix = string.Empty;

        private static DateTime s_lastTimestamp = DateTime.UtcNow;

        public static string Generate()
        {
            lock (s_lastIdPrefix)
            {
                var now = DateTime.UtcNow;
                var year = now.Year % 100;
                var yearChar = CHAR_SET[year % s_charSetLength];
                var monthChar = CHAR_SET[now.Month % s_charSetLength];
                var dayChar = CHAR_SET[now.Day % s_charSetLength];
                var hourChar = CHAR_SET[now.Hour % s_charSetLength];
                var minuteChar = CHAR_SET[now.Minute % s_charSetLength];
                var secondChar = CHAR_SET[now.Second % s_charSetLength];
                var idBase = $"{yearChar}{monthChar}{dayChar}{hourChar}{minuteChar}{secondChar}";

                var millisecondString = MillisecondString(idBase, now);

                s_lastIdPrefix = $"{idBase}{millisecondString}";
                s_sequenceNumber++;

                return $"{s_lastIdPrefix}{s_sequenceNumber:D2}";
            }
        }

        private static string MillisecondString(string idBase, DateTime now)
        {
            var millisecondString = now.Millisecond.ToString("D3");

            if (s_lastIdPrefix == $"{idBase}{millisecondString}"
               && s_sequenceNumber >= MAX_PER_MILLISECOND)
            {
                WaitForNextMillisecond();
                millisecondString = MillisecondString(idBase, DateTime.UtcNow);
            }

            return millisecondString;
        }

        private static void WaitForNextMillisecond()
        {
            Thread.Sleep(1);
            // 更新上一个时间戳    
            s_lastTimestamp = DateTime.UtcNow;
            // 重置ID计数器和顺序号计数器    
            s_sequenceNumber = 0;
        }
    }
}