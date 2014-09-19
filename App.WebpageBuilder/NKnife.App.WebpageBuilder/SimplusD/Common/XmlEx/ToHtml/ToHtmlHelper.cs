using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 这是一个用来ToHtml的实例化类型，通过构造函数传入Sdsite的文件路径，
    /// 在实例化对象中实例一个SdsiteXmlDocument对象，以及该Sdsite的相关参数，如生成路径等。
    /// design by lukan, 2008-7-1 03:37:39
    /// </summary>
    public class ToHtmlHelper
    {
        /// <summary>
        /// 构造函数。构造一个包含SdsiteXmlDocument对象，及生成的相关参数的实例。
        /// </summary>
        /// <param name="sdsitePath">SdsiteXmlDocument的文件路径。</param>
        /// <param name="buildPath">生成html源码的目录</param>
        public ToHtmlHelper(string sdsitePath, string buildPath)
        {
            ///初始化一个SdsiteXmlDocument
            Debug.Assert(File.Exists(sdsitePath), sdsitePath + " isn't Exist!");
            SdsiteXmlDocument doc = new SdsiteXmlDocument(sdsitePath);
            doc.Load();
            this.SdsiteXmlDocument = doc;

            ///初始化生成路径
            if (!Directory.Exists(buildPath))
            {
                ///TODO:还没有想好如何处理这个问题。
                ///主要是传入的是一个错误的字符串(根本与目录无关)怎么办？
                Directory.CreateDirectory(buildPath);
                Debug.Assert(Directory.Exists(GetDir(buildPath)), "网站初始根路径不存在，请检查。\r\n" + buildPath);
            }
            _BuildPath = GetDir(buildPath);
        }

        /// <summary>
        /// 一个已打开的SdsiteXmlDocument对象。是一个XmlDocument，由Jeelu强类型化。
        /// </summary>
        public SdsiteXmlDocument SdsiteXmlDocument { get; set; }

        /// <summary>
        /// CSS文件存放的路径的目录名(考虑到Linux的原因，采用小写)
        /// </summary>
        private string _CSSPath = @"css\";
        /// <summary>
        /// Resources文件存放的路径的目录名(考虑到Linux的原因，采用小写)
        /// </summary>
        private string _ResourcesPath = @"resources\";
        /// <summary>
        /// tmplt文件存放的路径的目录名(考虑到Linux的原因，采用小写)
        /// </summary>
        private string _TmpltPath = @"templates\";

        /// <summary>
        /// 网站初始根路径(目录).
        /// </summary>
        internal string BuildPath
        {
            get
            {
                Debug.Assert(!string.IsNullOrEmpty(_BuildPath), "请先调用InitializePath()方法");
                return _BuildPath;
            }
            set { _BuildPath = value; }
        }
        private string _BuildPath;

        /// <summary>
        /// 网站初始CSS文件存放路径(考虑到Linux的原因，采用小写)
        /// </summary>
        internal string CSSPath
        {
            get
            {
                string path = Path.Combine(BuildPath, _CSSPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>
        /// 网站初始Resources文件存放路径(考虑到Linux的原因，采用小写)
        /// </summary>
        internal string ResourcesPath
        {
            get
            {
                string path = Path.Combine(BuildPath, _ResourcesPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>
        /// 网站初始模板存放路径(考虑到Linux的原因，采用小写)
        /// </summary>
        internal string TmpltPath
        {
            get
            {
                string path = Path.Combine(BuildPath, _TmpltPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        /// <summary>
        /// 模板的相对URL
        /// </summary>
        internal string TmpltRelativeUrl
        {
            get
            {
                return "/" + _TmpltPath.Replace('\\', '/');
            }
        }

        /// <summary>
        /// 把最后不带\的形式统一成带\。如d:\test转成d:\test\
        /// </summary>
        /// <param name="path">目录名</param>
        static string GetDir(string path)
        {
            if (!path.EndsWith(@"\"))
            {
                path = path + @"\";
            }
            return path;
        }
        /// <summary>
        /// 生成的文件的保存方法
        /// </summary>
        internal static bool FileSave(string fileFullName, string fileText)
        {
            if (!File.Exists(fileFullName))///如果文件不存在，那么有可能目录也不存在
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileFullName));///创建目录
            }
            else if (File.GetAttributes(fileFullName) != FileAttributes.Normal)
            {
                File.SetAttributes(fileFullName, FileAttributes.Normal);
            }

            FileStream fs = null;
            const int maxCount = 100;///重试创建文件的最大次数
            for (int i = 0; i < maxCount; i++)
            {
                try///考虑文件有可能被频繁访问，程序可能无法访问
                {
                    ///因只要是调用.SaveXhtml方法，故肯定是重新生成，所以使用FileMode.Create
                    fs = File.Open(fileFullName, FileMode.Create, FileAccess.ReadWrite);
                    break;
                }
                catch
                {
                    if (i == (maxCount - 1))
                    {
                        throw;
                    }
                    Thread.Sleep(15);
                }
            }
            byte[] buffer = Encoding.UTF8.GetBytes(fileText);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
            fs.Dispose();
            return true;
        }
    }
}
