using System.Collections.Generic;
using Jeelu.WordSegmentor;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 原开源分词组件虽然写的还好，但是还是有很大的问题。比如没有考虑如何访问词库管理器，没有考虑多线程等。
    /// 故继承出改写一部东西，修改了他的源代码，在本类中加以说明。
    /// </summary>
    public class JWordSegmentor : WordSeg
    {
        public JWordSegmentor()
        {
            this._DictMgr = new JDictionaryManager();
            this._UnknownWordsDictMgr = new JDictionaryManager();
        }
        /// <summary>
        /// 主词典文件。
        /// 更改了原开源分词组件，将他的这个成员变量的访问修饰符修改为protected
        /// </summary>
        public T_DictFile MainDictFile { get { return this._Dict; } }
        /// <summary>
        /// 主词典文件管理器。
        /// 更改了原开源分词组件，将他的这个成员变量的访问修饰符修改为protected
        /// </summary>
        public JDictionaryManager MainDictManager { get { return (JDictionaryManager)this._DictMgr; } }
        /// <summary>
        /// 主未登录词典文件。
        /// 更改了原开源分词组件，将他的这个成员变量的访问修饰符修改为protected
        /// </summary>
        public T_DictFile UnknownWordsDict { get { return this._UnknownWordsDict; } }
        /// <summary>
        /// 主未登录词典文件管理器。
        /// 更改了原开源分词组件，将他的这个成员变量的访问修饰符修改为protected
        /// </summary>
        public JDictionaryManager UnknownWordManager { get { return (JDictionaryManager)this._UnknownWordsDictMgr; } }
        /// <summary>
        /// 返加是否阻拦网站特许停止词
        /// </summary>
        public bool IsFilterStopSiteWords { get; set; }

        /// <summary>
        /// 全局的针对词库的操作的动作集合的XML文件，文件的每一个节点表示一个动作。
        /// </summary>
        public WordSqlXml OwnerWordSqlXml 
        {
            get { return _WordSqlXml; }
        }
        private WordSqlXml _WordSqlXml = new WordSqlXml();

        /// <summary>
        /// 主词库集合
        /// </summary>
        public JDictionaryCollection DictionaryCollection
        {
            get { return this._DictionaryCollection; }
            set { this._DictionaryCollection = value; }
        }
        private JDictionaryCollection _DictionaryCollection;

        /// <summary>
        /// 停止词库集合
        /// </summary>
        public JDictionaryCollection DictionaryStopCollection
        {
            get { return this._DictionaryStopCollection; }
            set { this._DictionaryStopCollection = value; }
        }
        private JDictionaryCollection _DictionaryStopCollection;

        public override void LoadDict(bool clear)
        {
            base.LoadDict(clear);
            this.LoadJDictionary(clear);/// Jeelu的频道词库
        }
        /// <summary>
        /// 加载频道（分类）字典
        /// </summary>
        /// <param name="clear">是否清除词频信息</param>
        protected virtual void LoadJDictionary(bool clear)
        {
            foreach (string dictfile in _DictionaryCollection)
            {
                JDictionary jdict = (JDictionary)Dict.LoadFromBinFileEx(dictfile);
                foreach (T_DictStruct dictStruct in jdict.Dicts)
                {
                    if (clear)
                    {
                        dictStruct.Frequency = 0;
                    }
                    this._ExtractWords.InsertWordToDfa(dictStruct.Word, dictStruct);
                    this._POS.AddWordPos(dictStruct.Word, dictStruct.Pos);
                }//foreach
            }//foreach
        }

        /// <summary>
        /// Jeelu重载核心方法：分词。
        /// </summary>
        /// <param name="str">需分词的字符串</param>
        /// <param name="sitename">网站名字,类似"www.sina.com.cn"</param>
        /// <returns>排除停止词后的分词集合</returns>
        public List<string> Segment(string str, string sitename)
        {
            this.SiteName = sitename;
            return this.Segment(str);
        }
        /// <summary>
        /// 网站名字,类似"www.sina.com.cn"
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 将已划分词的集合排除停止词，如不带停止词库的类型，仅阻拦公共的停止词
        /// </summary>
        /// <param name="words">已划分词的集合</param>
        /// <returns>排除停止词后的分词集合</returns>
        protected override List<string> ToOperateStopWord(List<string> kwList)
        {
            List<string> mkwList = base.ToOperateStopWord(kwList);
            return this.ToOperateStopWord(mkwList, this.SiteName);
        }
        private List<string> ToOperateStopWord(List<string> kwList, string sitename)
        {
            if (string.IsNullOrEmpty(this.SiteName))
            {
                return kwList;///如果网站名字为空，不操作
            }
            JDictionaryStop siteJDict;
            if (!this.DictionaryStopCollection.StopKeywordsDictionary.TryGetValue(sitename, out siteJDict))
            {
                return kwList;///如果网站字典不存在，不操作
            }
            this.SiteName = null;
            List<string> returnKwList = new List<string>();
            foreach (string kw in kwList)
            {
                if (siteJDict.StopWordDic.ContainsKey(kw))
                {
                    continue;
                }
                returnKwList.Add(kw);
            }
            return returnKwList;
        }
    }
}
