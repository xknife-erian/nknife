using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NKnife.Text
{
    public class StringHandler
    {
        /// <summary>
        /// 根据行数，字数算出Lcd显示屏显示内容的数据包List中一个item表示一行的数据内容，
        /// 考虑行数限制，超过部分舍弃
        /// </summary>
        /// <param name="content"></param>
        /// <param name="lineCount"></param>
        /// <param name="wordCount"></param>
        /// <returns></returns>
        public static List<byte[]> GetLcdDisplayDataArraysByLineAndWordCount(string content, int lineCount, int wordCount)
        {
            var result = new List<byte[]>();
            var list = GetLcdDisplayDataArraysByWordCount(content, wordCount);
            var min = Math.Min(list.Count, lineCount);
            for (int i = 0; i < min; i++)
            {
                result.Add(list[i]);
            }
            return result;
        }

        /// <summary>
        /// 根据字数算出Lcd显示屏显示内容的数据包List中一个item表示一行的数据内容，不考虑行数限制
        /// </summary>
        /// <param name="content"></param>
        /// <param name="wordCount"></param>
        /// <returns></returns>
        public static List<byte[]> GetLcdDisplayDataArraysByWordCount(string content, int wordCount)
        {
            if (wordCount < 4) throw new ArgumentOutOfRangeException("字数不能小于4");

            var result = new List<byte[]>();
            int strLen = content.Length;
            var tempLineList = new List<byte>();
            int currentLineHalfWordCount = 0;
            for (int i = 0; i < strLen; i++)
            {
                string tempWord = content.Substring(i, 1);
                var tempData = Encoding.GetEncoding("GB2312").GetBytes(tempWord);
                if (currentLineHalfWordCount + tempData.Count() > wordCount * 2) //加上当前字后超出了行能显示的字
                {
                    result.Add(tempLineList.ToArray()); //完成一行
                    tempLineList.Clear();
                    currentLineHalfWordCount = 0;
                }
                else
                {
                    tempLineList.AddRange(tempData);
                    currentLineHalfWordCount += tempData.Count();
                }
            }
            if(tempLineList.Count>0) result.Add(tempLineList.ToArray());
            return result;
        } 

        /// <summary>
        /// 由地址（0－255）生成瑞泽通讯查询地址 DL DH（10xxxxxx 110000xx）
        /// </summary>
        /// <param name="cmdAddress"></param>
        /// <returns></returns>
        public static string ConvertIntAddressToAddressStr(int cmdAddress)
        {
            byte mybyte = Convert.ToByte(cmdAddress);
            string tempStr = "0000000" + Convert.ToString(mybyte, 2);
            string binaryStr = (tempStr).Substring(tempStr.Length - 8, 8);
            string binaryDL = "10" + binaryStr.Substring(2, 6);
            string binaryDH = "110000" + binaryStr.Substring(0, 2);
            string DL = BinaryByteToHexStr(binaryDL);
            string DH = BinaryByteToHexStr(binaryDH);

            return DL + " " + DH;
        }

        public static string PanDataToNumber(string data1, string data2, string data3, string data4)
        {
            string result = string.Empty;
            try
            {
                result = (Convert.ToInt32(data3, 16)*100 + Convert.ToInt32(data4, 16)).ToString();
            }
            catch
            {
            }
            return result;
        }

        public static string PanDataToNumberEx(string data1, string data2, string data3, string data4, string data5)
        {
            string result = string.Empty;
            try
            {
                if (!data1.Equals("00") && !data1.Equals("20"))
                {
                    result += Chr(Convert.ToInt32(data1, 16));
                }
                if (!data2.Equals("00") && !data2.Equals("20"))
                {
                    result += Chr(Convert.ToInt32(data2, 16));
                }
                if (!data3.Equals("00") && !data3.Equals("20"))
                {
                    result += Chr(Convert.ToInt32(data3, 16));
                }
                if (!data4.Equals("00") && !data4.Equals("20"))
                {
                    result += Chr(Convert.ToInt32(data4, 16));
                }
                if (!data5.Equals("00") && !data5.Equals("20"))
                {
                    result += Chr(Convert.ToInt32(data5, 16));
                }
            }
            catch
            {
            }
            return result;
        }

        public static string PanGetChkString(int count)
        {
            int localCount = count%256;
            localCount = localCount%100;
            string chkStr = Convert.ToString((localCount), 16);
            chkStr = chkStr.PadLeft(2, '0');
            return chkStr;
        }

        public static string PanGetAddrFormat(int addr)
        {
            if (addr > 255)
            {
                addr = addr%255;
            }
            string addrHex = Convert.ToString(addr, 16).PadLeft(2, '0');
            return addrHex;
        }

        public static int PanGetNumberChk(int number)
        {
            if (number > 9999)
            {
                number = number%10000;
            }
            int NumberHigh = 0;
            int NumberLow = 0;
            if (number > 99)
            {
                NumberHigh = number/100;
                NumberLow = number%100;
            }
            else
            {
                NumberLow = number;
            }
            return NumberHigh + NumberLow;
        }

        public static string PanGetDataFormat(int numberData)
        {
            if (numberData > 9999)
            {
                numberData = numberData%10000;
            }
            string WaitStrHigh = "00";
            string WaitStrLow = "00";
            if (numberData > 99)
            {
                int highCount = numberData/100;
                int lowCount = numberData%100;
                WaitStrHigh = Convert.ToString(highCount, 16);
                WaitStrLow = Convert.ToString(lowCount, 16);
            }
            else
            {
                WaitStrLow = Convert.ToString(numberData, 16);
            }
            WaitStrHigh = WaitStrHigh.PadLeft(2, '0');
            WaitStrLow = WaitStrLow.PadLeft(2, '0');

            return WaitStrHigh + " " + WaitStrLow;
        }

        /// <summary>2进制字符串转换成16进制字符串
        /// </summary>
        /// <param name="binaryByte"></param>
        /// <returns></returns>
        private static string BinaryByteToHexStr(string binaryByte)
        {
            return FourBinaryBitToOneHexBit(binaryByte.Substring(0, 4)) + FourBinaryBitToOneHexBit(binaryByte.Substring(4, 4));
        }

        /// <summary>4bit 2进制数转换成 1bit 16进制数
        /// </summary>
        /// <param name="binaryBit"></param>
        /// <returns></returns>
        private static string FourBinaryBitToOneHexBit(string binaryBit)
        {
            string ResultHexBit = "";
            switch (binaryBit)
            {
                case "0000":
                    ResultHexBit = "0";
                    break;
                case "0001":
                    ResultHexBit = "1";
                    break;
                case "0010":
                    ResultHexBit = "2";
                    break;
                case "0011":
                    ResultHexBit = "3";
                    break;
                case "0100":
                    ResultHexBit = "4";
                    break;
                case "0101":
                    ResultHexBit = "5";
                    break;
                case "0110":
                    ResultHexBit = "6";
                    break;
                case "0111":
                    ResultHexBit = "7";
                    break;
                case "1000":
                    ResultHexBit = "8";
                    break;
                case "1001":
                    ResultHexBit = "9";
                    break;
                case "1010":
                    ResultHexBit = "A";
                    break;
                case "1011":
                    ResultHexBit = "B";
                    break;
                case "1100":
                    ResultHexBit = "C";
                    break;
                case "1101":
                    ResultHexBit = "D";
                    break;
                case "1110":
                    ResultHexBit = "E";
                    break;
                case "1111":
                    ResultHexBit = "F";
                    break;
            }
            return ResultHexBit;
        }

        /// <summary>
        /// 返回１６进制字符
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string GetHexChar(string value)
        {
            string sReturn = string.Empty;
            switch (value)
            {
                case "10":
                    sReturn = "A";
                    break;
                case "11":
                    sReturn = "B";
                    break;
                case "12":
                    sReturn = "C";
                    break;
                case "13":
                    sReturn = "D";
                    break;
                case "14":
                    sReturn = "E";
                    break;
                case "15":
                    sReturn = "F";
                    break;
                default:
                    sReturn = value;
                    break;
            }
            return sReturn;
        }

        /// <summary>
        /// 返回１６进制
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string ConvertHex(string value)
        {
            string sReturn = string.Empty;
            try
            {
                while (int.Parse(value) > 16)
                {
                    int v = int.Parse(value);
                    sReturn = GetHexChar((v%16).ToString()) + sReturn;
                    value = Math.Floor(Convert.ToDouble(v/16)).ToString();
                }
                sReturn = GetHexChar(value) + sReturn;
            }
            catch
            {
                sReturn = "###Valid Value!###";
            }
            return sReturn;
        }

        public static string ConvertHex(int value)
        {
            string result = string.Empty;
            try
            {
                while (value > 16)
                {
                    result = GetHexChar((value % 16).ToString()) + result;
                    value = (int)Math.Floor(Convert.ToDouble(value / 16));
                }
                result = GetHexChar(value.ToString()) + result;
                return result;
            }
            catch
            {
                return result;
            }
        }

        /// <summary>返回数字对应的asc指令
        /// </summary>
        /// <param name="intToBeConverted"></param>
        /// <param name="lengthToBeConverted"></param>
        /// <returns></returns>
        public static string ConvertHexCmdFromInt(string intToBeConverted, int lengthToBeConverted)
        {
            string StrCmd = "";
            if (intToBeConverted.Length > lengthToBeConverted)
            {
                for (int i = intToBeConverted.Length - lengthToBeConverted; i < lengthToBeConverted; i++)
                {
                    StrCmd += (int.Parse(intToBeConverted[i].ToString()) + 30).ToString() + " ";
                }
            }
            if (intToBeConverted.Length == lengthToBeConverted)
            {
                for (int i = 0; i < lengthToBeConverted; i++)
                {
                    StrCmd += (int.Parse(intToBeConverted[i].ToString()) + 30).ToString() + " ";
                }
            }
            if (intToBeConverted.Length < lengthToBeConverted)
            {
                for (int i = 0; i < lengthToBeConverted - intToBeConverted.Length; i++)
                {
                    StrCmd += "30 ";
                }
                for (int i = 0; i < intToBeConverted.Length; i++)
                {
                    StrCmd += (int.Parse(intToBeConverted[i].ToString()) + 30).ToString() + " ";
                }
            }
            return StrCmd;
        }

        /// <summary>
        /// 判断是否数字
        /// </summary>
        /// <param name="str">指定的字符串</param>
        /// <returns>bool</returns>
        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            int n = str.Length;
            string mStr = "[0-9]{" + n.ToString() + ",}";
            return Regex.Match(str, mStr).Success;
        }

        public static bool IsComposedByZero(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            int n = str.Length;
            string mStr = "[0]{" + n.ToString() + ",}";
            return Regex.Match(str, mStr).Success;
        }

        /// <summary>
        /// 指定字符是否数字类别
        /// </summary>
        /// <param name="cr">指定的字符</param>
        /// <returns>bool</returns>
        public static bool IsNumber(char cr)
        {
            return char.IsNumber(cr);
        }

        /// <summary>
        /// 指定字符是否字符类别
        /// </summary>
        /// <param name="cr">指定的字符</param>
        /// <returns>bool</returns>
        public static bool IsLetter(char cr)
        {
            return char.IsLetter(cr);
        }

        /// <summary>
        /// 判断是否手机号
        /// </summary>
        /// <param name="phone">指定字符串</param>
        /// <returns>bool</returns>
        public static bool IsPhoneNumber(string phone)
        {
            string nr = Regex.Match(phone, "^(130|131|133|135|135|137|138|139)(\\d){8}$").ToString();
            if (nr != "")
            {
                //合法
                return true;
            }
            //不合法
            return false;
        }

        /// <summary>获得字符串（包括汉字）assci码
        /// </summary>
        /// <param name="sSou"></param>
        /// <returns></returns>
        public static string GetAssciHexCode(string sSou)
        {
            byte[] b = Encoding.Default.GetBytes(sSou.ToCharArray());
            return b.Aggregate("", (current, t) => current + Convert.ToString(t, 16));
        }

        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                var asciiEncoding = new ASCIIEncoding();
                int intAsciiCode = asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            throw new Exception("Character is not valid.");
        }

        /// <summary>ASCII码转字符：
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                var asciiEncoding = new ASCIIEncoding();
                var byteArray = new[] {(byte) asciiCode};
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }

        /// <summary>将字符串转换
        /// </summary>
        /// <param name="displayContent"></param>
        /// <returns></returns>
        public static string GenerateDisplayStr(string displayContent)
        {
            char[] CharArray = displayContent.ToCharArray();
            string HexString;
            string ResultString = "";
            for (int i = 0; i < CharArray.Length; i++)
            {
                HexString = GetAssciHexCode(CharArray[i].ToString());
                if (IsChinese(CharArray[i])) //汉字字符
                {
                    ResultString += HexString.Substring(0, 2) + " " + HexString.Substring(2, 2) + " ";
                }
                else //非汉字字符
                {
                    ResultString += HexString + " ";
                }
            }
            return ResultString.Trim();
        }

        public static int GetHexStringChk(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str.ToCharArray());
            return b.Aggregate(0, (current, bt) => current + bt);
        }

        /// <summary>判断字符是否汉字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsChinese(char c)
        {
            return c >= 0x4E00 && c <= 0x9FA5;
        }

        public static int GetByteLength(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str.ToCharArray());
            return b.Length;
        }

        public static bool IsIP(string ipAddr)
        {
            const string regexIPAddress = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";
            return Regex.IsMatch(ipAddr, regexIPAddress);
        }
    }
}