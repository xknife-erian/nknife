using System;
using System.Text;
using System.Windows.Forms;

namespace NKnife.SerialBox
{
    internal class Utils
    {
        public static long GetMmSystemTime()
        {
            return DateTime.Now.Ticks;
        }

        public static string GetStrSystemTime()
        {
            return DateTime.Now.ToString("[yyyy-MM-dd hh:mm:ss.fff]\r");
        }

        public static string HexToStr(string s)
        {
            s = s.Replace(" ", "");
            var bytes = new byte[s.Length / 2];
            var num = 0;
            for (var i = 0; i < s.Length; i += 2)
                try
                {
                    bytes[num++] = Convert.ToByte(s.Substring(i, 2), 0x10);
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.ToString(), "错误提示");
                }

            return Encoding.Default.GetString(bytes).Replace("\0", "");
        }

        public static byte[] StrHexArrToByte(string s)
        {
            s = s.Replace(" ", "");
            var buffer = new byte[s.Length / 2];
            var num = 0;
            for (var i = 0; i < s.Length; i += 2)
                try
                {
                    buffer[num++] = Convert.ToByte(s.Substring(i, 2), 0x10);
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.ToString(), "错误提示");
                }

            return buffer;
        }

        public static byte StrHexToByte(string str)
        {
            byte num = 0;
            try
            {
                num = Convert.ToByte(str, 0x10);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.ToString(), "错误提示");
            }

            return num;
        }

        public static string StrToHex(string s)
        {
            var builder = new StringBuilder();
            var bytes = Encoding.GetEncoding("GB18030").GetBytes(s);
            foreach (var t in bytes)
            {
                builder.Append(Convert.ToString(t, 0x10).ToUpper().PadLeft(2, '0'));
                builder.Append(" ");
            }

            return builder.ToString();
        }
    }
}