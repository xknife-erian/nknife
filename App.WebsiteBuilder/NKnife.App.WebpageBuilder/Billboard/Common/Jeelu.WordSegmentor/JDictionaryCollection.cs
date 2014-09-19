using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 词典的集合
    /// </summary>
    public class JDictionaryCollection : CollectionBase
    {
        /// <summary>
        /// 构造函数。词典的集合。
        /// </summary>
        /// <param name="dirPath">词典所在的目录</param>
        /// <param name="type">词典的类型</param>
        private JDictionaryCollection(string dirPath, JDictionaryTypeEnum type)
        {

            string path = Path.GetFullPath(dirPath);
            string[] files = Directory.GetFiles(path, "*." + type.ToString(), SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                this.List.Add(item);
            }
        }

        /// <summary>
        /// 词典集合创建器
        /// </summary>
        /// <param name="dirPath">词典所在的目录</param>
        /// <param name="type">词典的类型</param>
        internal static JDictionaryCollection Creator(string dirPath, JDictionaryTypeEnum type)
        {
            Debug.Assert(Directory.Exists(dirPath), dirPath + " isn't Exists!");
            return new JDictionaryCollection(dirPath, type);
        }

        /// <summary>
        /// 获取Jeelu分词组件的词库类型，枚举名将做为该类型的词库文件的后缀名
        /// </summary>
        internal JDictionaryTypeEnum JDictionaryType { get; private set; }

        /// <summary>
        /// 获取本集合的关联的所有网站的停止词的集合。
        /// </summary>
        internal Dictionary<string, JDictionaryStop> StopKeywordsDictionary { get; private set; }

        /// <summary>
        /// 载入所有的词库到词典中
        /// </summary>
        internal void LoadAllSiteStop()
        {
            foreach (string file in this.List)
            {
                JDictionaryStop sitestopDic = new JDictionaryStop(file, new Dictionary<string, int>());
                this.StopKeywordsDictionary.Add(file, sitestopDic);
            }
        }
    }
}
