using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Reflection;
using Jeelu.WordSegmentor;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 简单字典分词
    /// </summary>
    public class WordSeg
    {
        #region const
        const string CHS_STOP_WORD_FILENAME = "chsstopwords.txt";
        const string ENG_STOP_WORD_FILENAME = "engstopwords.txt";
        const string PATTERNS =
            @"[０-９\d]+\%|[０-９\d]{1,2}月|[０-９\d]{1,2}日|[０-９\d]{1,4}年|" +
            @"[０-９\d]{1,4}-[０-９\d]{1,2}-[０-９\d]{1,2}|" +
            @"[０-９\d]+|[^ａ-ｚＡ-Ｚa-zA-Z0-9０-９\u4e00-\u9fa5]|[ａ-ｚＡ-Ｚa-zA-Z]+|[\u4e00-\u9fa5]+";
        #endregion

        protected IRule[] _Rules;

        //中文停用词哈希表
        protected Hashtable _ChsStopwordTbl = new Hashtable();

        //英文停用词哈希表
        protected Hashtable _EngStopwordTbl = new Hashtable();

        protected CExtractWords _ExtractWords;

        private PosBinRule _PosBinRule;
        private MatchName _MatchNameRule;

        /// <summary>
        /// 字典
        /// </summary>
        protected T_DictFile _Dict;

        /// <summary>
        /// 字典管理
        /// </summary>
        protected DictMgr _DictMgr = new DictMgr();


        /// <summary>
        /// 未登录词统计字典
        /// 用于统计未登录词的出现频率和词性。
        /// 目前主要统计未知词性的未登录词和
        /// 未知姓名
        /// </summary>
        protected T_DictFile _UnknownWordsDict;

        /// <summary>
        /// 未登录词字典管理
        /// </summary>
        protected DictMgr _UnknownWordsDictMgr = new DictMgr();

        DateTime _LastSaveTime; //上一次保存字典和统计信息的时间

        #region Public property

        /// <summary>
        /// 未登录词阈值，当统计超过这个值时，自动将未登录词加入到字典中
        /// </summary>
        public int UnknownWordsThreshold
        {
            get { return _UnknownWordsThreshold; }
            set
            {
                if (value < 1)
                {
                    _UnknownWordsThreshold = 1;
                }
                else
                {
                    _UnknownWordsThreshold = value;
                }
            }
        }
        private int _UnknownWordsThreshold = 100;

        /// <summary>
        /// 自动插入超过统计阈值的未登录词
        /// </summary>
        public bool AutoInsertUnknownWords
        {
            get { return _AutoInsertUnknownWords; }
            set { _AutoInsertUnknownWords = value; }
        }
        private bool _AutoInsertUnknownWords = false;

        /// <summary>
        /// 优先判断词频，如果一个长的单词由多个短的单词组成，而长的单词词频较低则忽略长的单词。
        /// 如"中央酒店"的词频比"中央"和"酒店"的词频都要低，则忽略"中央酒店"。
        /// </summary>
        public bool FreqFirst
        {
            get { return _FreqFirst; }
            set
            {
                _FreqFirst = value;
                if (_FreqFirst)
                {
                    _ExtractWords.SelectByFreqEvent = SelectByFreq;
                }
                else
                {
                    _ExtractWords.SelectByFreqEvent = null;
                }
            }
        }
        private bool _FreqFirst = true;

        /// <summary>
        /// 自动学习
        /// </summary>
        public bool AutoStudy
        {
            get
            {
                return _AutoStudy;
            }
            set
            {
                _AutoStudy = value;
                _MatchNameRule.AutoStudy = value;
            }
        }
        private bool _AutoStudy = false;

        /// <summary>
        /// 间隔多少秒自动保存最新的字典和统计信息，AutoStudy = true时有效
        /// </summary>
        public int AutoSaveInterval
        {
            get { return _AutoSaveInterval; }
            set
            {
                if (value <= 1)
                {
                    _AutoSaveInterval = 1;
                }
                else
                {
                    _AutoSaveInterval = value;
                }
            }
        }
        private int _AutoSaveInterval = 24 * 3600; //间隔多少秒自动保存最新的字典和统计信息，AutoStudy = true时有效

        /// <summary>
        /// 字典文件所在路径
        /// </summary>
        public string DictPath
        {
            get { return _DictPath; }
            set { _DictPath = value; }
        }
        private string _DictPath;

        /// <summary>
        /// 日志文件名
        /// </summary>
        public string LogFileName
        {
            get { return _LogFileName; }
            set { _LogFileName = value; }
        }
        private string _LogFileName = "Jeelu.WordSegmentor.log";

        /// <summary>
        /// 词性
        /// </summary>
        public CPOS Pos
        {
            get { return _POS; }
        }
        protected CPOS _POS;

        #endregion

        #region 配置文件

        private object Convert(String In, Type destType)
        {
            if (destType.Equals(typeof(bool)))
            {
                return System.Convert.ToBoolean(In);
            }
            else if (destType.Equals(typeof(byte)))
            {
                return System.Convert.ToByte(In);
            }
            else if (destType.Equals(typeof(char)))
            {
                return System.Convert.ToChar(In);
            }
            else if (destType.Equals(typeof(DateTime)))
            {
                return System.Convert.ToDateTime(In);
            }
            else if (destType.Equals(typeof(decimal)))
            {
                return System.Convert.ToDecimal(In);
            }
            else if (destType.Equals(typeof(double)))
            {
                return System.Convert.ToDouble(In);
            }
            else if (destType.Equals(typeof(Int16)))
            {
                return System.Convert.ToInt16(In);
            }
            else if (destType.Equals(typeof(Int32)))
            {
                return System.Convert.ToInt32(In);
            }
            else if (destType.Equals(typeof(Int64)))
            {
                return System.Convert.ToInt64(In);
            }
            else if (destType.Equals(typeof(SByte)))
            {
                return System.Convert.ToSByte(In);
            }
            else if (destType.Equals(typeof(Single)))
            {
                return System.Convert.ToSingle(In);
            }
            else if (destType.Equals(typeof(String)))
            {
                return In;
            }
            else if (destType.Equals(typeof(UInt16)))
            {
                return System.Convert.ToUInt16(In);
            }
            else if (destType.Equals(typeof(UInt32)))
            {
                return System.Convert.ToUInt32(In);
            }
            else if (destType.Equals(typeof(UInt64)))
            {
                return System.Convert.ToUInt64(In);
            }
            else
            {
                throw new Exception(String.Format("Unknown type:{0}", destType.Name));
            }
        }

        class CfgItem
        {
            public PropertyInfo Pi;
            public String Comment;

            public CfgItem(PropertyInfo pi, String comment)
            {
                Pi = pi;
                Comment = comment;
            }
        }

        CfgItem[] GetCfgItems()
        {
            CfgItem[] items = new CfgItem[9];
            items[0] = new CfgItem(this.GetType().GetProperty("UnknownWordsThreshold"), "未登录词阈值，当统计超过这个值时，自动将未登录词加入到字典中");
            items[1] = new CfgItem(this.GetType().GetProperty("AutoInsertUnknownWords"), "自动插入超过统计阈值的未登录词");
            items[2] = new CfgItem(this.GetType().GetProperty("FreqFirst"), "优先判断词频，如果一个长的单词由多个短的单词组成，而长的单词词频较低则忽略长的单词。如 中央酒店的词频比中央和酒店的词频都要低，则忽略中央酒店。");
            items[3] = new CfgItem(this.GetType().GetProperty("AutoStudy"), "自动统计姓名前后缀，自动统计未登录词，自动统计词频");
            items[4] = new CfgItem(this.GetType().GetProperty("AutoSaveInterval"), "间隔多少秒自动保存最新的字典和统计信息，AutoStudy = true时有效");
            items[5] = new CfgItem(this.GetType().GetProperty("DictPath"), "字典文件所在路径");
            items[6] = new CfgItem(this.GetType().GetProperty("LogFileName"), "日志文件名");
            items[7] = new CfgItem(this.GetType().GetProperty("MatchName"), "是否匹配汉语人名");
            items[8] = new CfgItem(this.GetType().GetProperty("FilterStopWords"), "是否过滤停用词");

            return items;
        }

        /// <summary>
        /// 从配置文件加载配置
        /// </summary>
        /// <param name="fileName">配置文件名</param>
        public virtual void LoadConfig(String fileName)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                doc.Load(fileName);

                System.Xml.XmlNodeList list = doc.GetElementsByTagName("Item");

                System.Xml.XmlAttribute itemName = null;

                foreach (System.Xml.XmlNode node in list)
                {
                    try
                    {
                        itemName = node.Attributes["Name"];
                        System.Xml.XmlAttribute value = node.Attributes["Value"];
                        if (itemName == null || value == null)
                        {
                            continue;
                        }

                        PropertyInfo pi = GetType().GetProperty(itemName.Value);
                        pi.SetValue(this, Convert(value.Value, pi.PropertyType), null);
                    }
                    catch (Exception e1)
                    {
                        WriteLog(String.Format("Load Item={0} fail, errmsg:{1}",
                            itemName.Value, e1.Message));
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(String.Format("Load config fail, errmsg:{0}", e.Message));
            }

        }

        /// <summary>
        /// 保存配置到配置文件
        /// </summary>
        /// <param name="fileName">配置文件名</param>
        public virtual void SaveConfig(String fileName)
        {
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(fileName, Encoding.UTF8);

            try
            {
                writer.Formatting = System.Xml.Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Jeelu.WordSegmentor");


                foreach (CfgItem item in GetCfgItems())
                {
                    writer.WriteComment(item.Comment);
                    writer.WriteStartElement("Item");
                    writer.WriteAttributeString("Name", item.Pi.Name);
                    writer.WriteAttributeString("Value", item.Pi.GetValue(this, null).ToString());
                    writer.WriteEndElement(); //Item
                }

                writer.WriteEndElement(); //Jeelu.WordSegmentor
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                WriteLog(String.Format("Save config fail, errmsg:{0}", e.Message));
            }
        }

        #endregion

        protected virtual void WriteLog(string log)
        {
            try
            {
                CFile.WriteLine(LogFileName,
                    string.Format("{0} {1}", DateTime.Now, log), "utf-8");
            }
            catch
            {
            }
        }

        protected virtual double GetFreqWeight(List<T_WordInfo> words, List<int> list)
        {
            double weight = 0;

            for (int i = 0; i < list.Count; i++)
            {
                T_WordInfo w = (T_WordInfo)words[(int)list[i]];
                T_DictStruct dict = (T_DictStruct)w.Tag;
                weight += dict.Frequency;
            }

            return weight;
        }

        protected virtual int GetPosWeight(List<T_WordInfo> words, List<int> list)
        {
            int weight = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                T_WordInfo w1 = (T_WordInfo)words[(int)list[i]];
                T_WordInfo w2 = (T_WordInfo)words[(int)list[i + 1]];
                if (_PosBinRule.Match(w1.Word, w2.Word))
                {
                    weight++;
                }
            }

            return weight;
        }

        protected virtual bool CompareByPos(List<T_WordInfo> words, List<int> pre, List<int> cur)
        {
            int posWeightPre = GetPosWeight(words, pre);
            int posWeightCur = GetPosWeight(words, cur);

            if (posWeightPre < posWeightCur)
            {
                return true;
            }

            if (posWeightPre > posWeightCur)
            {
                return false;
            }

            //词性比较相同的情况下比较词频
            return GetFreqWeight(words, pre) < GetFreqWeight(words, cur);
        }

        /// <summary>
        /// 按词频优先进行选择
        /// </summary>
        /// <param name="words"></param>
        /// <param name="pre"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        protected virtual bool SelectByFreq(List<T_WordInfo> words, List<int> pre, List<int> cur)
        {
            double minPreFreq = 1000000000;
            double minCurFreq = 1000000000;
            int maxPreLength = 0; //Pre中所有词的最大
            int maxCurLength = 0; //Cur中所有词的最大

            foreach (int index in pre)
            {
                double freq = ((T_DictStruct)words[index].Tag).Frequency;
                if (freq < minPreFreq)
                {
                    minPreFreq = freq;
                }

                if (words[index].Word.Length > maxPreLength)
                {
                    maxPreLength = words[index].Word.Length;
                }
            }

            foreach (int index in cur)
            {
                double freq = ((T_DictStruct)words[index].Tag).Frequency;
                if (freq < minCurFreq)
                {
                    minCurFreq = freq;
                }

                if (words[index].Word.Length > maxCurLength)
                {
                    maxCurLength = words[index].Word.Length;
                }

            }

            //对于全部由单个字组成的词，不进行词频优先统计
            if (maxPreLength <= 1 && maxCurLength > 1)
            {
                return true;
            }
            else if (maxPreLength > 1 && maxCurLength <= 1)
            {
                return false;
            }

            return minCurFreq > minPreFreq;
        }

        protected virtual void InitRules()
        {
            _Rules = new IRule[3];
            _PosBinRule = new PosBinRule(_POS);
            _Rules[0] = new MergeNumRule(_POS);
            _Rules[1] = _PosBinRule;
            _MatchNameRule = new MatchName(_POS);
            _Rules[2] = _MatchNameRule;
        }

        protected WordSeg()
        {
            _MatchName = false;
            _FilterStopWords = false;
            _MatchDirection = T_Direction.LeftToRight;
            _ExtractWords = new CExtractWords();
            _ExtractWords.CompareByPosEvent = CompareByPos;
            _POS = new CPOS();
            _LastSaveTime = DateTime.Now;
            InitRules();
        }

        /// <summary>
        /// 合并浮点数
        /// </summary>
        protected virtual string MergeFloat(ArrayList words, int start, ref int end)
        {
            StringBuilder str = new StringBuilder();

            int dotCount = 0;
            end = start;
            int i;

            for (i = start; i < words.Count; i++)
            {
                string word = (string)words[i];

                if (word == "")
                {
                    break;
                }

                if ((word[0] >= '0' && word[0] <= '9')
                    || (word[0] >= '０' && word[0] <= '９'))
                {
                }
                else if (word[0] == '.' && dotCount == 0)
                {
                    dotCount++;
                }
                else
                {
                    break;
                }

                str.Append(word);
            }

            end = i;

            return str.ToString();
        }

        /// <summary>
        /// 合并Email
        /// </summary>
        protected virtual string MergeEmail(ArrayList words, int start, ref int end)
        {
            StringBuilder str = new StringBuilder();

            int dotCount = 0;
            int atCount = 0;
            end = start;
            int i;

            for (i = start; i < words.Count; i++)
            {
                string word = (string)words[i];

                if (word == "")
                {
                    break;
                }

                if ((word[0] >= 'a' && word[0] <= 'z') ||
                    (word[0] >= 'A' && word[0] <= 'Z') ||
                    word[0] >= '0' && word[0] <= '9')
                {
                    dotCount = 0;
                }
                else if (word[0] == '@' && atCount == 0)
                {
                    atCount++;
                }
                else if (word[0] == '.' && dotCount == 0)
                {
                    dotCount++;
                }
                else
                {
                    break;
                }

                str.Append(word);

            }

            end = i;

            return str.ToString();
        }

        /// <summary>
        /// 合并英文专用词。
        /// 如果字典中有英文专用词如U.S.A, C++.C#等
        /// 需要对初步分词后的英文和字母进行合并
        /// </summary>
        protected virtual string MergeEnglishSpecialWord(CExtractWords extractWords, ArrayList words, int start, ref int end)
        {
            StringBuilder str = new StringBuilder();

            int i;

            for (i = start; i < words.Count; i++)
            {
                string word = (string)words[i];

                //word 为空或者为空格回车换行等分割符号，中断扫描
                if (word.Trim() == "")
                {
                    break;
                }

                //如果遇到中文，中断扫描
                if (word[0] >= 0x4e00 && word[0] <= 0x9fa5)
                {
                    break;
                }

                str.Append(word);
            }

            String mergeString = str.ToString();
            List<T_WordInfo> exWords = extractWords.ExtractFullText(mergeString);

            if (exWords.Count == 1)
            {
                T_WordInfo info = (T_WordInfo)exWords[0];
                if (info.Word.Length == mergeString.Length)
                {
                    end = i;
                    return mergeString;
                }
            }

            return null;

        }

        #region 维护停用词

        /// <summary>
        /// 从停用词字典中加载停用词
        /// 停用词字典的格式：
        /// 文本文件格式，一个词占一行
        /// </summary>
        /// <param name="chsFileName">中文停用词</param>
        /// <param name="engFileName">英文停用词</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public virtual void LoadStopwordsDict(String chsFileName, String engFileName)
        {
            int numChrStop = 0;//统计中文停用词数目，并作为Value值插入哈希表
            int numEngStop = 0;//统计英文停用词数目，并作为Value值插入哈希表

            try
            {
                StreamReader swChrFile = new StreamReader(chsFileName, Encoding.GetEncoding("UTF-8"));
                StreamReader swEngFile = new StreamReader(engFileName, Encoding.GetEncoding("UTF-8"));

                //加载中文停用词
                while (!swChrFile.EndOfStream)
                {
                    //按行读取中文停用词
                    string strChrStop = swChrFile.ReadLine();

                    //如果哈希表中不包括该停用词则添加到哈希表中
                    if (!_ChsStopwordTbl.Contains(strChrStop))
                    {
                        _ChsStopwordTbl.Add(strChrStop, numChrStop);
                        numChrStop++;
                    }
                }

                //加载英文停用词
                while (!swEngFile.EndOfStream)
                {
                    //按行读取中文停用词
                    string strEngStop = swEngFile.ReadLine();

                    //如果哈希表中不包括该停用词则添加到哈希表中
                    if (!_EngStopwordTbl.Contains(strEngStop))
                    {
                        _EngStopwordTbl.Add(strEngStop, numEngStop);
                        numEngStop++;
                    }
                }

                swChrFile.Close();
                swEngFile.Close();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 将中文停用词保存到文件中 
        /// </summary>
        /// <param name="fileName">要保存文件名</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public virtual void SaveChsStopwordDict(String fileName)
        {
            try
            {
                //创建一个新的存储中文停用词的文本文件，若该文件存在则覆盖
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));


                //遍历中文停用词表，写入文件
                foreach (DictionaryEntry i in _ChsStopwordTbl)
                {
                    sw.WriteLine(i.Key.ToString());
                }

                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 将英文停用词保存到文件中 
        /// </summary>
        /// <param name="fileName">要保存文件名</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public virtual void SaveEngStopwordDict(String fileName)
        {
            try
            {
                //创建一个新的存储英文停用词的文本文件，若该文件存在则覆盖
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));


                //遍历英文停用词表，写入文件
                foreach (DictionaryEntry i in _EngStopwordTbl)
                {
                    sw.WriteLine(i.Key.ToString());
                }
                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 增加一个中文停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void AddChsStopword(String word)
        {
            //如果原来词库中已存在，则不做任何操作
            if (_ChsStopwordTbl.Contains(word))
            {
                return;
            }
            else
            {
                _ChsStopwordTbl.Add(word, _ChsStopwordTbl.Count);

            }

        }

        /// <summary>
        /// 删除一个中文停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void DelChsStopword(String word)
        {
            //如果原来词库中不存在，则不做任何操作
            _ChsStopwordTbl.Remove(word);
        }

        /// <summary>
        /// 增加一个英文停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void AddEngStopword(String word)
        {
            //如果原来词库中已存在，则不做任何操作
            if (_EngStopwordTbl.Contains(word))
            {
                return;
            }
            else
            {
                _EngStopwordTbl.Add(word, _EngStopwordTbl.Count);
            }
        }

        /// <summary>
        /// 删除一个英文停用词
        /// </summary>
        /// <param name="word"></param>
        public virtual void DelEngStopword(String word)
        {
            //如果原来词库中不存在，则不做任何操作
            _EngStopwordTbl.Remove(word);
        }

        #endregion

        #region 加载字典

        public void LoadDict()
        {
            LoadDict(false);
        }

        /// <summary>
        /// 加载字典
        /// </summary>
        /// <param name="clear">是否清除词频</param>
        public virtual void LoadDict(bool clear)
        {

            //加载字典
            _Dict = Dict.LoadFromBinFileEx(_DictPath + "Dict.dct");
            _DictMgr.Dict = _Dict;

            foreach (T_DictStruct word in _Dict.Dicts)
            {
                if (clear)
                {
                    word.Frequency = 0;
                }
                _ExtractWords.InsertWordToDfa(word.Word, word);
                _POS.AddWordPos(word.Word, word.Pos);
            }

            //加载未登录词统计字典
            if (File.Exists(_DictPath + "UnknownWords.dct"))
            {
                _UnknownWordsDict = Dict.LoadFromBinFileEx(_DictPath + "UnknownWords.dct");
            }
            else
            {
                _UnknownWordsDict = new T_DictFile();
            }
            _UnknownWordsDictMgr.Dict = _UnknownWordsDict;

            //加载姓名前缀后缀统计表
            _MatchNameRule.LoadNameTraffic(_DictPath + "Name.dct");
            if (clear)
            {
                _MatchNameRule.ClearNameTraffic();
            }
            _MatchNameRule.TrafficUnknownWordHandle = TrafficUnknownWord;
        }

        public virtual void SaveDict()
        {
            _MatchNameRule.SaveNameTraffic(_DictPath + "Name.dct");

            foreach (T_DictStruct word in _Dict.Dicts)
            {
                T_DictStruct dict = (T_DictStruct)_ExtractWords.GetTag(word.Word);
                if (dict != null)
                {
                    word.Frequency = dict.Frequency;
                }
            }

            Dict.SaveToBinFileEx(_DictPath + "Dict.dct", _Dict);

            Dict.SaveToBinFileEx(_DictPath + "UnknownWords.dct", _UnknownWordsDict);

        }

        #endregion

        #region 分词属性

        /// <summary>
        /// 是否匹配汉语人名
        /// </summary>
        public bool MatchName
        {
            get
            {
                return _MatchName;
            }

            set
            {
                _MatchName = value;
            }
        }
        private bool _MatchName;

        /// <summary>
        /// 匹配方向
        /// 默认为从左至右匹配,即正向匹配
        /// </summary>
        public T_Direction MatchDirection
        {
            get
            {
                return _MatchDirection;
            }

            set
            {
                _MatchDirection = value;
            }
        }
        private T_Direction _MatchDirection;

        /// <summary>
        /// 是否过滤停用词
        /// </summary>
        public bool FilterStopWords
        {
            get
            {
                return _FilterStopWords;
            }
            set
            {
                if (value)
                {
                    if (_ChsStopwordTbl.Count == 0 || _EngStopwordTbl.Count == 0)
                    {
                        LoadStopwordsDict(_DictPath + CHS_STOP_WORD_FILENAME, _DictPath + ENG_STOP_WORD_FILENAME);
                    }
                }
                _FilterStopWords = value;
            }
        }
        private bool _FilterStopWords;

        #endregion

        #region 分词

        private void InsertWordToArray(String word, List<String> arr)
        {
            arr.Add(word);
        }

        /// <summary>
        /// 预分词
        /// </summary>
        /// <param name="str">要分词的句子</param>
        /// <returns>预分词后的字符串输出</returns>
        private List<String> PreSegment(String str)
        {
            ArrayList initSeg = new ArrayList();


            if (!CRegex.GetSingleMatchStrings(str, PATTERNS, true, ref initSeg))
            {
                return new List<String>();
            }

            List<String> retWords = new List<String>();

            int i = 0;

            _ExtractWords.MatchDirection = MatchDirection;

            while (i < initSeg.Count)
            {
                String word = (String)initSeg[i];
                if (word == "")
                {
                    word = " ";
                }

                if (i < initSeg.Count - 1)
                {
                    bool mergeOk = false;
                    if (((word[0] >= '0' && word[0] <= '9') || (word[0] >= '０' && word[0] <= '９')) &&
                        ((word[word.Length - 1] >= '0' && word[word.Length - 1] <= '9') ||
                         (word[word.Length - 1] >= '０' && word[word.Length - 1] <= '９'))
                        )
                    {
                        //合并浮点数
                        word = MergeFloat(initSeg, i, ref i);
                        mergeOk = true;
                    }
                    else if ((word[0] >= 'a' && word[0] <= 'z') ||
                             (word[0] >= 'A' && word[0] <= 'Z')
                             )
                    {
                        //合并成英文专业名词
                        String specialEnglish = MergeEnglishSpecialWord(_ExtractWords, initSeg, i, ref i);

                        if (specialEnglish != null)
                        {
                            InsertWordToArray(specialEnglish, retWords);
                            continue;
                        }

                        //合并邮件地址
                        if ((String)initSeg[i + 1] != "")
                        {
                            if (((String)initSeg[i + 1])[0] == '@')
                            {
                                word = MergeEmail(initSeg, i, ref i);
                                mergeOk = true;
                            }
                        }
                    }

                    if (mergeOk)
                    {
                        InsertWordToArray(word, retWords);
                        continue;
                    }
                }


                if (word[0] < 0x4e00 || word[0] > 0x9fa5)
                {
                    //英文或符号，直接加入
                    InsertWordToArray(word, retWords);
                }
                else
                {
                    List<T_WordInfo> words = _ExtractWords.ExtractFullTextMaxMatch(word);
                    int lastPos = 0;
                    bool lstIsName = false; //前一个词是人名

                    foreach (T_WordInfo wordInfo in words)
                    {

                        if (lastPos < wordInfo.Position)
                        {
                            /*
                                                        String unMatchWord = word.Substring(lastPos, wordInfo.Position - lastPos);

                                                        InsertWordToArray(unMatchWord, retWords);
                            */
                            //中间有未匹配词，将单个字逐个加入
                            for (int j = lastPos; j < wordInfo.Position; j++)
                            {
                                InsertWordToArray(word[j].ToString(), retWords);
                            }

                        }


                        lastPos = wordInfo.Position + wordInfo.Word.Length;

                        //统计中文姓名的后缀
                        if (AutoStudy && lstIsName)
                        {
                            T_DictStruct wordDict = (T_DictStruct)wordInfo.Tag;
                            if ((wordDict.Pos & (int)T_POS.POS_A_NR) == 0)
                            {
                                _MatchNameRule.AddBefore(wordInfo.Word);
                            }

                            lstIsName = false;
                        }

                        //统计中文姓名的前缀
                        //如总统，主席等
                        if ((((T_DictStruct)wordInfo.Tag).Pos & (int)T_POS.POS_A_NR) != 0)
                        {
                            if (wordInfo.Word.Length > 1 && wordInfo.Word.Length <= 4 && retWords.Count > 0 && AutoStudy && !lstIsName)
                            {
                                T_DictStruct wordDict = (T_DictStruct)wordInfo.Tag;
                                _MatchNameRule.AddBefore(retWords[retWords.Count - 1]);
                            }

                            lstIsName = true;
                        }


                        InsertWordToArray(wordInfo.Word, retWords);


                    }

                    if (lastPos < word.Length)
                    {
                        //尾部有未匹配词，将单个字逐个加入
                        for (int j = lastPos; j < word.Length; j++)
                        {
                            InsertWordToArray(word[j].ToString(), retWords);
                        }

                        //InsertWordToArray(word.Substring(lastPos, word.Length - lastPos), retWords);
                    }
                }

                i++;
            }

            return retWords;
        }

        private void TrafficUnknownWord(String word, T_POS Pos)
        {
            if (word.Length <= 1 || word.Length > 3)
            {
                return;
            }

            T_DictStruct unknownWord = _UnknownWordsDictMgr.GetWord(word);


            if (unknownWord == null)
            {
                _UnknownWordsDictMgr.InsertWord(word, 1, (int)Pos);
                return;
            }

            //如果是屏蔽的未登录词，则不加入
            //屏蔽的未登录词用词性等于0来表示
            if (unknownWord.Pos == 0)
            {
                return;
            }

            unknownWord.Pos |= (int)Pos;
            unknownWord.Frequency++;

            if (unknownWord.Frequency > UnknownWordsThreshold && AutoInsertUnknownWords)
            {
                T_DictStruct w = _DictMgr.GetWord(word);
                if (w == null)
                {
                    _DictMgr.InsertWord(word, unknownWord.Frequency, unknownWord.Pos);

                    _ExtractWords.InsertWordToDfa(word, unknownWord);
                    _POS.AddWordPos(word, unknownWord.Pos);

                }
                else
                {
                    w.Pos |= unknownWord.Pos;
                    w.Frequency += unknownWord.Frequency;
                }

                unknownWord.Frequency = 0;
            }
        }

        /// <summary>
        /// 召回未登录词
        /// </summary>
        /// <returns></returns>
        private List<String> RecoverUnknowWord(List<String> words)
        {
            List<String> retWords = new List<String>();

            int i = 0;
            int j = 0;

            while (i < words.Count)
            {
                String w = (String)words[i];

                if (i == words.Count - 1)
                {
                    retWords.Add(w);
                    break;
                }

                if (_POS.IsUnknowOneCharWord(w))
                {
                    String word = w;
                    i++;

                    while (_POS.IsUnknowOneCharWord(words[i]))
                    {
                        word += (String)words[i];
                        i++;
                        if (i >= words.Count)
                        {
                            break;
                        }
                    }

                    if (AutoStudy)
                    {
                        TrafficUnknownWord(word, T_POS.POS_A_NZ);

                        //将所有连续单字组成一个词，假设其为未登录词，进行统计
                        if (j < i && w[0] >= 0x4e00 && w[0] <= 0x9fa5)
                        {
                            j = i;

                            if (j < words.Count)
                            {
                                String longWord = word;

                                while (words[j].Length == 1 && words[j][0] >= 0x4e00 && words[j][0] <= 0x9fa5)
                                {
                                    longWord += words[j];
                                    j++;

                                    if (j >= words.Count)
                                    {
                                        break;
                                    }
                                }

                                TrafficUnknownWord(longWord, T_POS.POS_A_NZ);
                            }
                        }
                    }

                    retWords.Add(word);
                    continue;
                }
                else
                {
                    if (AutoStudy)
                    {
                        //将所有连续单字组成一个词，假设其为未登录词，进行统计
                        if (j <= i && w.Length == 1 && w[0] >= 0x4e00 && w[0] <= 0x9fa5)
                        {
                            j = i + 1;
                            String word = w;

                            if (j < words.Count)
                            {
                                while (words[j].Length == 1 && words[j][0] >= 0x4e00 && words[j][0] <= 0x9fa5)
                                {
                                    word += words[j];
                                    j++;

                                    if (j >= words.Count)
                                    {
                                        break;
                                    }
                                }

                                TrafficUnknownWord(word, T_POS.POS_A_NZ);
                            }
                        }
                    }

                    retWords.Add(w);
                }

                i++;
            }

            return retWords;

        }


        /// <summary>
        /// 分词,不屏蔽停用词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<String> SegmentNoStopWord(String str)
        {
            List<String> preWords = PreSegment(str);
            List<String> retWords = new List<String>();

            int index = 0;
            while (index < preWords.Count)
            {
                int next = -1;
                foreach (IRule rule in _Rules)
                {
                    if (!_MatchName && rule is MatchName)
                    {
                        continue;
                    }

                    next = rule.ProcRule(preWords, index, retWords);
                    if (next > 0)
                    {
                        index = next;
                        break;
                    }
                }

                if (next > 0)
                {
                    continue;
                }

                retWords.Add(preWords[index]);
                index++;
            }

            //return retWords;
            List<String> retStrings = RecoverUnknowWord(retWords);

            if (AutoStudy)
            {
                foreach (String word in retStrings)
                {
                    T_DictStruct dict = (T_DictStruct)_ExtractWords.GetTag(word);

                    if (dict != null)
                    {
                        dict.Frequency++;
                    }

                }
            }

            return retStrings;
        }

        /// <summary>
        /// 定期保存最新的字典和统计信息
        /// </summary>
        private void SaveDictOnTime()
        {
            if (!AutoStudy)
            {
                return;
            }

            TimeSpan s = DateTime.Now - _LastSaveTime;

            if (s.TotalSeconds > AutoSaveInterval)
            {
                _LastSaveTime = DateTime.Now;
                SaveDict();
            }
        }

        /// <summary>
        /// 分词并输出单词信息列表 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<T_WordInfo> SegmentToWordInfos(String str)
        {
            //定时保存字典
            SaveDictOnTime();

            List<String> words = SegmentNoStopWord(str);

            List<T_WordInfo> retWords = new List<T_WordInfo>();
            int position = 0;

            foreach (String word in words)
            {
                if (_FilterStopWords)
                {
                    if (_ChsStopwordTbl[word] != null || _EngStopwordTbl[word] != null)
                    {
                        position += word.Length;
                        continue;
                    }
                }

                T_WordInfo wordInfo = new T_WordInfo();
                wordInfo.Word = word;
                wordInfo.Position = position;
                retWords.Add(wordInfo);
                position += word.Length;
            }

            return retWords;
        }

        /// <summary>
        /// 分词只输出单词列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<String> Segment(String str)
        {
            //定时保存字典
            SaveDictOnTime();
            return ToOperateStopWord(SegmentNoStopWord(str));
        }

        /// <summary>
        /// Jeelu提取有关Stop词的操作方法。
        /// </summary>
        /// <param name="words">已分好的所有词</param>
        /// <returns>抛弃了停止词的词的集合</returns>
        protected virtual List<string> ToOperateStopWord(List<String> words)
        {
            if (!_FilterStopWords)
            {
                return words;
            }
            else
            {
                List<String> retWords = new List<String>();
                foreach (String word in words)
                {
                    if (_ChsStopwordTbl[word] != null || _EngStopwordTbl[word] != null)
                    {
                        continue;
                    }
                    retWords.Add(word);
                }
                return retWords;
            }
        }

        #endregion
    }

}
