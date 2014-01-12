using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Text
{
    public static class ConvertUtil
    {
        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        public static string BytesToString(byte[] data, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                if (data[0] == 239 && data[1] == 187 && data[2] == 191)
                {
                    return encoding.GetString(data, 3, data.Length - 3);
                }
            }
            return encoding.GetString(data);
        }

        public static DateTime StringToDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now;
            }
            return DateTime.Parse(str);
        }

        public static DateTime StringToDateTime(string str, string format)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now;
            }
            return DateTime.ParseExact(str, format, null);
        }

        public static Decimal StringToDecimal(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 1;
            }
            return Decimal.Parse(str);
        }

        public static bool StringToBool(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return bool.Parse(str);
        }

        public static int StringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return int.Parse(str);
        }

        public static long StringToLong(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return long.Parse(str);
        }

        public static float StringToFloat(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return float.Parse(str);
        }

        /// <summary>
        /// 处理带px的字符串，转换成int
        /// </summary>
        public static int PxStringToInt(string str)
        {
            throw new NotImplementedException();
            //string strTemp = str;
            //if (Regex.Number.IsMatch(str))
            //{
            //    strTemp = str;
            //}
            //else if (str.EndsWith("px", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    strTemp = str.Substring(0, str.Length - 2);
            //}
            //else
            //{
            //    return -1;
            //}

            //int n;
            //if (int.TryParse(strTemp, out n))
            //{
            //    return n;
            //}
            //return -1;
        }

        /// <summary>
        /// 用来处理null的情况。若输入的str为null，则返回一个空字符串
        /// </summary>
        public static string StringToString(string str)
        {
            if (str == null)
            {
                return "";
            }
            return str;
        }

        /// <summary>
        /// 把代有单位的计量数据转化为以单位为Key、以数值为value的键值对
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> SizeToUintAndInt(string size)
        {
            string _key = "px";
            string _value = "";
            int i = 0;
            if (string.IsNullOrEmpty(size))
            {
                return new KeyValuePair<string, string>(_key, _value);
            }
            if (size.Length == 1 && int.TryParse(size, out i))
            {
                return new KeyValuePair<string, string>(_key, i.ToString());
            }
            if (size[size.Length - 1] == '%')
            {
                _key = "%";
                _value = size.Remove(size.Length - 1);
                return new KeyValuePair<string, string>(_key, _value);
            }
            else
            {
                string _unit = size.Substring(size.Length - 2, 2);
                _value = size.Remove(size.Length - 2);
                if (int.TryParse(_value, out i))
                {
                    return new KeyValuePair<string, string>(_key, _value);
                }
                else
                {
                    return new KeyValuePair<string, string>("px", "");
                }
            }
        }

        /// <summary>
        /// 将字符串数组转换按某种符号分开的字符串
        /// </summary>
        public static string StringArrayToString(string[] stringArray, char c)
            //static public string ConvertStringArrayToString(string[] stringArray, string spaceString)
        {
            var sb = new StringBuilder();
            foreach (string str in stringArray)
            {
                sb.Append(str).Append(c);
            }
            return sb.ToString().TrimEnd(c);

            //if (stringArray == null) return "";
            //string finalString = string.Empty;
            //int arrayLength = stringArray.Length;
            //if (arrayLength == 0) return "";
            //for (int i = 0; i < arrayLength - 1; i++)
            //{
            //    finalString += stringArray[i] + spaceString;
            //}
            //return finalString + stringArray[arrayLength - 1];
        }

        #region Image和base64之间的转换

        public static string ImageToBase64(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            ///jpg格式，则直接读内存。否则先读成Image，再转成jpg格式
            if (ext != ".jpg" && ext != ".jpeg")
            {
                Image image = Image.FromFile(filePath);
                return ImageToBase64(image);
            }
            else
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                return Convert.ToBase64String(bytes);
            }
        }

        public static string ImageToBase64(Image image)
        {
            var memory = new MemoryStream();
            image.Save(memory, ImageFormat.Jpeg);
            byte[] bytes = memory.ToArray();
            return Convert.ToBase64String(bytes);
        }

        public static Image Base64ToImage(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return Image.FromStream(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static void Base64ToImage(string base64String, string filePath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, bytes);
        }

        #endregion

        #region object和base64之间的转换, DesignBy:lukan

        public static string FileToBase64(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static Icon Base64ToIcon(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Icon(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static Cursor Base64ToCursor(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Cursor(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        #endregion
    }
}