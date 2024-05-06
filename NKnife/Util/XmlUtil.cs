using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using NKnife.Base;

namespace NKnife.Util
{
    /// <summary>
    ///     针对XmlDocument的一些帮助方法。静态类。
    /// </summary>
    public static class XmlUtil
    {
        /// <summary>
        ///     创建一个新的Xml文件，如文件存在，将覆盖。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <param name="rootNodeName">根节点的LocalName</param>
        /// <param name="encoding">编码的字符串表示</param>
        /// <returns></returns>
        public static XmlDocument CreateNewDocument(string file, string rootNodeName, string encoding)
        {
            var doc = new XmlDocument();
            var declaration = doc.CreateXmlDeclaration("1.0", encoding, null);
            var root = doc.CreateElement(rootNodeName);
            doc.AppendChild(declaration);
            doc.AppendChild(root);

            if (!File.Exists(file))
            {
                var spe = Path.DirectorySeparatorChar.ToString();
                var index = file.LastIndexOf(spe, StringComparison.Ordinal);

                if (index >= 0)
                {
                    var dir = file.Substring(0, index);
                    FileUtil.CreateDirectory(dir);
                }
            }
            doc.Save(file);

            return doc;
        }

        /// <summary>
        ///     创建一个新的Xml文件，如文件存在，将覆盖。默认为utf-8编码模式。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <param name="rootNodeName">根节点的LocalName</param>
        /// <returns></returns>
        public static XmlDocument CreateNewDocument(string file, string rootNodeName)
        {
            return CreateNewDocument(file, rootNodeName, "utf-8");
        }

        /// <summary>
        ///     创建一个新的Xml文件，如文件存在，将覆盖。默认为utf-8编码模式。默认根节点名root。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <returns></returns>
        public static XmlDocument CreateNewDocument(string file)
        {
            return CreateNewDocument(file, "root");
        }
    }
}