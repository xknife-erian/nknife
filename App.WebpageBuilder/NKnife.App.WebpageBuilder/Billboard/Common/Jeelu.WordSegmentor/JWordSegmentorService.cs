using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Jeelu.WordSegmentor;
using Jeelu.Billboard;

namespace Jeelu.Billboard
{
    public static class JWordSegmentorService
    {
        /// <summary>
        /// 全局的词库管理器
        /// </summary>
        internal static JDictionaryManager JWordDictManager { get { return _JWordSegmentor.MainDictManager; } }

        #region 分词组件主对象管理

        /// <summary>
        /// 初始化WordSegmentor的工作环境，返回一个全局的分词对象(WordSeg)。这个对象是否能够单建的？
        /// </summary>
        /// <param name="path">配置文件所在路径</param>
        /// <returns>Jeelu继承重写的WordSeg对象</returns>
        public static JWordSegmentor Creator(string configPath, string dataFullPath)
        {
            if (_JWordSegmentor == null)
            {
                _JWordSegmentor = new JWordSegmentor();
                _JWordSegmentor.LoadConfig(Path.Combine(configPath, "Jeelu.WordSegmentor.segcfg"));
                _JWordSegmentor.DictPath = dataFullPath;
                _JWordSegmentor.DictionaryCollection = JDictionaryCollection.Creator(
                                    Path.Combine(dataFullPath, "channel"), JDictionaryTypeEnum.SiteDictionary);
                _JWordSegmentor.DictionaryStopCollection = JDictionaryCollection.Creator(
                                    Path.Combine(dataFullPath, "sitestop"), JDictionaryTypeEnum.SiteStopDictionary);
                _JWordSegmentor.LoadDict();
                return _JWordSegmentor;
            }
            return _JWordSegmentor;
        }
        private static JWordSegmentor _JWordSegmentor = null;

        #endregion

        #region 词典管理

        /// <summary>
        /// 保存词典
        /// </summary>
        public static void Save()
        {
            _JWordSegmentor.SaveDict();
        }

        public static bool IsExist(string keyword)
        {
            T_DictStruct tds = JWordDictManager.GetWord(keyword);
            if (tds != null)
            {
                return true;
            }
            return false;
        }
        public static bool IsExist(string keyword, string ciku)
        {
            return IsExist(keyword, null);
        }

        /// <summary>
        /// 词库里面的词的数量
        /// </summary>
        public static int Count
        {
            get { return JWordDictManager.Dict.Dicts.Count; }
        }

        /// <summary>
        /// 获得一个词的权重
        /// </summary>
        public static ulong GetFrequency(string keyword)
        {
            T_DictStruct tds = JWordDictManager.GetWord(keyword);
            if (tds != null)
            {
                return ulong.Parse(tds.Frequency.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 从词库中搜索，支持模糊查找
        /// </summary>
        /// <param name="keyword">要搜索的詞</param>
        /// <param name="isApproximate">是否模糊查找</param>
        public static string[] Search(string keyword, bool isApproximate)
        {
            JWordDictManager.Approximate = false;
            List<SearchWordResult> wordList = JWordDictManager.Search(keyword);
            List<string> kwList = new List<string>();
            foreach (SearchWordResult item in wordList)
            {
                kwList.Add(item.ToString());
            }
            return kwList.ToArray();
        }

        #endregion
    }
}
