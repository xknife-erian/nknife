using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 关键词管理类
    /// </summary>
    class WordsManager
    {
        #region 属性及字段
        private Dictionary<string, ulong> _newWords;
        private Dictionary<string, ulong> _oldWords;
        static private int _todayWeight = 2;
        private Dictionary<string, ulong> _existWords;
        private UrlManager _urls;

        /// <summary>
        /// Url管理器
        /// </summary>
        internal UrlManager Urls
        {
            get { return _urls; }
            set { _urls = value; }
        }

        /// <summary>
        /// 词库已有关键词
        /// </summary>
        public System.Collections.Generic.Dictionary<string, ulong> ExistWords
        {
            get { return _existWords; }
            set { _existWords = value; }
        }

        /// <summary>
        /// 今天出现词的权重比
        /// </summary>
        public static int TodayWeight
        {
            get { return WordsManager._todayWeight; }
            set { WordsManager._todayWeight = value; }
        }

        /// <summary>
        /// 词库关键词
        /// </summary>
        public Dictionary<string, ulong> OldWords
        {
            get { return _oldWords; }
            set { _oldWords = value; }
        }

        /// <summary>
        /// 新出现关键词
        /// </summary>
        public Dictionary<string, ulong> NewWords
        {
            get { return _newWords; }
            set { _newWords = value; }
        }
        #endregion

        #region 构造函数
        public WordsManager()
        {
            NewWords = new Dictionary<string, ulong>();
            OldWords = new Dictionary<string, ulong>();
            ExistWords = new Dictionary<string, ulong>();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 载入已有词库
        /// </summary>
        public void LoadOldWords(string wordsFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(wordsFile);
            XmlNodeList nodes = doc.SelectNodes(@"//item");
            
            foreach (XmlNode node in nodes)
            {
                OldWords.Add(node.Attributes[0].Value, Convert.ToUInt64(node.Attributes[1].Value));
            }
        }

        /// <summary>
        /// 获取新的词列表
        /// </summary>
        public void FetchNewWords(string urlsFile)
        {
            string[] urls = GetUrls(urlsFile);
            string[] words = new string[100];
            foreach (string url in urls)
            {
                //抓取网页并分词
            }
            //统计词频
            foreach (string word in words)
            {
                if (NewWords.ContainsKey(word))
                {
                    NewWords[word]++;
                }
                else
                {
                    NewWords.Add(word, 1);
                }
            }
        }


        /// <summary>
        /// 合并词库中已有词汇
        /// </summary>
        public void MergeExistWords()
        {
            foreach (KeyValuePair<string,ulong> word in NewWords)
            {
                if (OldWords.ContainsKey(word.Key))
                {
                    ExistWords.Add(word.Key, OldWords[word.Key] + word.Value);
                }
            }
            foreach (KeyValuePair<string,ulong> word in ExistWords)
            {
                if (NewWords.ContainsKey(word.Key))
                {
                    NewWords.Remove(word.Key);
                }
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取URL列表
        /// </summary>
        /// <param name="urlsFile"></param>
        /// <returns></returns>
        private string[] GetUrls(string urlsFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(urlsFile);
            XmlNodeList nodes = doc.SelectNodes(@"//url");
            string[] urls = new string[nodes.Count];
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                urls[i] = node.Value;
                i++;
            }
            return urls;
        }
        #endregion

        /// <summary>
        /// 返回更新字符串
        /// </summary>
        public string GetUpdateCmd()
        {
            return "";
        }

    }
}
