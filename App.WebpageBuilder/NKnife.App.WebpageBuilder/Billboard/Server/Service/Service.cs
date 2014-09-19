using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Jeelu.WordSegmentor;
using System.IO;

namespace Jeelu.Billboard.Server
{
    /// <summary>
    /// 应用程序的Service静态类，大多数的静态类都被设为他的静态类
    /// </summary>
    internal static partial class Service
    {
        public static JWordSegmentor JWordSegmentor { get; set; }
        public static DbHelper DbHelper { get; set; }

        /// <summary>
        /// 用户的登录密码
        /// </summary>
        public static string UserLoginPassword { get { return "1234";}}

        private static string _dictVersion;
        /// <summary>
        /// 词库的版本号
        /// </summary>
        public static string DictVersion 
        {
            get 
            {
                if (string.IsNullOrEmpty (_dictVersion ))
                {
                    _dictVersion = GetVersionFromTxt();
                }
                return _dictVersion;
            }
            set 
            {
                _dictVersion = value;
                SetVersionToTxt(value);
            }
        }

        public static Dictionary<string, string> dic = new Dictionary<string, string>();
        /// <summary>
        /// 得到一个新的GUID
        /// </summary>
        /// <returns></returns>
        public static string GetSessionId()
        {
            return Guid.NewGuid().ToString("N");
        }


        public static void SetMac(string mac)
        {
            if (!dic.ContainsKey (mac))
            {
                dic.Add(mac, "");
            }
        }

        public static void SetSessionId(string mac, string sessionId)
        {
            if (dic.ContainsKey(mac))
            {
                dic.Remove(mac);
            }
            dic.Add(mac, sessionId);
        }

        /// <summary>
        /// 从文件里读取此值
        /// </summary>
        /// <returns></returns>
        private static string GetVersionFromTxt()
        {
            string path = Path.File.VersionFile;
            if (!File.Exists(path))
            {
                StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
                writer.WriteLine("100000");
                writer.Close();
                return "100000";
            }
            else
            {
                StreamReader reader = new StreamReader(path);
                string version = reader.ReadLine();
                reader.Close();
                return version;
            }
        }
        /// <summary>
        /// 写入版本号
        /// </summary>
        /// <param name="version"></param>
        private static void SetVersionToTxt(string version)
        {
            string path = Path.File.VersionFile;
            File.SetAttributes(Path.File.VersionFile, FileAttributes.Normal);
            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine(version);
            writer.Close();
        }
    }
}
