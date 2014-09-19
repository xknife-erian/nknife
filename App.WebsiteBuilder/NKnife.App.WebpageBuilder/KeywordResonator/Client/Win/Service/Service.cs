using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.WordSegmentor;
using Jeelu.Billboard;
using System.IO;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 应用程序的Service静态类，大多数的静态类都被设为他的静态类
    /// </summary>
    internal static partial class Service
    {
        /// <summary>
        /// 交互时，验证号
        /// </summary>
        public static string SessionId { get; set; }
        private static string _dictVersion;
        /// <summary>
        /// 词库的版本
        /// </summary>
        public static string DictVersion 
        {
            get
            {
                if (string.IsNullOrEmpty(_dictVersion))
                {
                    //首次从文件里读取版本
                    _dictVersion = GetVersionFromTxt();
                }
                return _dictVersion;
            }
            set 
            {
                //保存到文件
                SetVersionToTxt(value);
            }
        }

        /// <summary>
        /// 计算机的MAC
        /// </summary>
        public static string ComputerMac 
        {
            get
            {
                if (string.IsNullOrEmpty(_computerMac))
                {
                    _computerMac = Jeelu.Utility.Management.GetMacAddress();
                }
                return _computerMac;
            }
        }
        private static string _computerMac;

        /// <summary>
        /// 返回验证条件的字节数组
        /// </summary>
        /// <param name="passport"></param>
        /// <param name="version"></param>
        /// <param name="mac"></param>
        /// <returns></returns>
        public static byte[] GetBodyBytes(string passport, string version, string mac)
        {
            string all = passport + "&" + version + "&" + mac;
            return Encoding.UTF8.GetBytes(all);
        }

        /// <summary>
        /// 从文件里读取此值
        /// </summary>
        /// <returns></returns>
        private static string GetVersionFromTxt()
        {
            string path = PathService.File.VersionFile;
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
            string path = PathService.File.VersionFile;
            File.SetAttributes(path, FileAttributes.Normal);
            StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.WriteLine(version);
            writer.Close();
        }


        /// <summary>
        /// 全局共享的一个分词对象
        /// </summary>
        public static JWordSegmentor JWordSegmentor { get; set; }

        public static DbHelper DbHelper { get; set; }
        /// <summary>
        /// 全局的针对词库的操作的动作集合的XML文件，文件的每一个节点表示一个动作。
        /// </summary>
        public static WordSqlXml WordSqlXml
        {
            get { return Service.JWordSegmentor.OwnerWordSqlXml; }
        }

        /// <summary>
        /// 更新本地词库
        /// </summary>
        /// <returns></returns>
        public static void UpdateLocalDict(string updateCmd)
        {
            WordSqlXml sql = new WordSqlXml();
            sql.LoadXml(updateCmd);

            foreach (var item in sql)
            {
                WordSqlXmlAction word = (WordSqlXmlAction)item;
                word.Run();
            }
        }
    }
}
