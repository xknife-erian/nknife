using System;
using System.Text;

namespace NKnife.Text
{
    public class CounterDisplay
    {
        //发送显示
        public static string GetSendDisplayStr(string displayContent,string addressStr)
        {
            string DisplayStr = GenerateDisplayStr(displayContent);
            string ColorStr = "";
            for (int i = 0; i < (DisplayStr.Length / 2); i++)
            {
                ColorStr += "01";
            }
            return (addressStr + "43" + ColorStr + "1A" + addressStr + "44" + DisplayStr + "1A");
        }

        //将字符串转换为数字屏显示的格式
        public static string GetSendNumberDisplayStr(string displayContent,string addressStr)
        {
            string DisplayStr = GenerateDisplayStr(displayContent);
            return (addressStr + "57" + DisplayStr);
        }

        //将字符串转换成LED显示屏能显示的格式
        public static string GenerateDisplayStr(string displayContent)
        {
            char[] CharArray = displayContent.ToCharArray();
            string HexString;
            string ResultString = "";
            for (int i = 0; i < CharArray.Length; i++)
            {
                HexString = GetAssciHexCode(CharArray[i].ToString());
                if (IsChinese(CharArray[i]))  //汉字字符
                {
                    for (int j = 0; j < 4; j++)
                    {
                        ResultString += "0" + HexString[j].ToString();
                    }
                }
                else  //非汉字字符
                {
                    ResultString += HexString;
                }
            }
            return ResultString;
        }

        

        //判断字符是否汉字
        public static bool IsChinese(char c)
        {
            return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
        }

        //获得字符串（包括汉字）assci码
        private static string GetAssciHexCode(string sSou)
        {
            byte[] b = Encoding.Default.GetBytes(sSou.ToCharArray());
            string s = "";
            for (int i = 0; i < b.Length; i++)
                s += Convert.ToString(b[i], 16);
            return s;
        }
    }
}
