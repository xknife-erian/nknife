using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Jeelu.SimplusD.Server
{
    public class CommonService
    {
        
        static private Dictionary<string, string> _recordUserDic;
        /// <summary>
        /// 存储用户帐户验证信息,用来判定当前的用户是否为有效用户
        /// 参数1:用户名;参数2:新产生的guid
        /// </summary>
        static public Dictionary<string, string> RecordUserDic
        {
            get
            {
                if (_recordUserDic == null)
                {
                    _recordUserDic = new Dictionary<string, string>();
                }
                return _recordUserDic;
            }
        }

        static public readonly string Sdsites = "sdsites";
        
        static public readonly string Temp = "temp";
        
        /// <summary>
        /// 某些文件扩展名 .inc
        /// </summary>
        static public readonly string Inc = ".inc";

        /// <summary>
        /// Css文件夹名称 Css
        /// </summary>
        static public readonly string Css = "Css";

        


        /// <summary>
        /// 将一段字符串用MD5编码(字符串会用ascii码编码)
        /// </summary>
        static public string EncodingMd5(string inputText)
        {
            string str;
            //创建md5对象
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //使用ascii编码方式把字符串转化为字节数组．
            byte[] inputBye = Encoding.ASCII.GetBytes(inputText);
            byte[] outputBye = md5.ComputeHash(inputBye);

            str = BitConverter.ToString(outputBye);
            str = str.Replace("-", "").ToLower();
            return (str);
        }
        /// <summary>
        /// 得到路径  如:zha\zhangling\HeNanGov
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="projectName">项目名</param>
        /// <returns></returns>
        static public string GetUserRelativePath(string userName, string projectName)
        {
            string  getStr = userName.Substring(0, 3);
            return getStr + @"\" + userName + @"\" + projectName;
        }

        /// <summary>
        /// 得到路径 如:zha\zhangling 主要是用于创建user.xml文件路径
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        static public string GetUserPath(string userName)
        {
            return Path.Combine(userName.Substring(0, 3), userName);
        }

        /// <summary>
        /// 返回给 CGI 一个新的GUID号,作为通告证号
        /// </summary>
        /// <returns></returns>
        static public string CreateNewGuid()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }
    }
}