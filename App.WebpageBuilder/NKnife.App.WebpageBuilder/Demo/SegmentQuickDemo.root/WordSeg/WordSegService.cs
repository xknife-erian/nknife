using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.WordSeg
{
    public static class WordSegService
    {
        /// <summary>
        /// 初始化分词组件的工作环境。文件读取异常应在调用时进行处理。
        /// </summary>
        /// <param name="segwords">主词典的Xml文件</param>
        /// <param name="segchsstopwords">中文停止词词典的Xml文件</param>
        /// <param name="segchssymbol">中文标点符号的Xml文件</param>
        /// <param name="segengstopwords">英文停止词词典的Xml文件</param>
        /// <param name="segengsymbol">英文标点符号的Xml文件</param>
        public static void Initialize(
            string segwords, 
            string segchsstopwords, 
            string segchssymbol, 
            string segengstopwords, 
            string segengsymbol)
        {
            SegWords = LoadSegWords(segwords);
            SegChsStopwordDic = LoadStopwords(segchsstopwords, SegChsStopwordDic);
            SegChsStopwordDic = LoadStopwords(segchssymbol, SegChsStopwordDic);
            SegEngStopwordDic = LoadStopwords(segengstopwords, SegEngStopwordDic);
            SegEngStopwordDic = LoadStopwords(segengsymbol, SegEngStopwordDic);

            ExtractInfo = new ExtractInfo();
            _WordPosBuilder = new WordPosBuilder();
            ExtractInfo.CompareByPosEvent = CompareByPos;
            foreach (WordPos item in SegWords.WordPosList)
            {
                ExtractInfo.InsertWordToDfa(item.Word);
                _WordPosBuilder.AddWordPos(item.Word, item.Pos);
            }

            SetOwnerRules();
        }

        /// <summary>
        /// 从关键词主词典加载数据。
        /// </summary>
        /// <param name="filename">关键词主词典的Xml文件</param>
        private static WordPosCollection LoadSegWords(string filename)
        {
            WordPosCollection wpList = new WordPosCollection();
            try///用XmlTextReader的方法快速读取字典文件
            {
                XmlTextReader reader = new XmlTextReader(filename);
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (!reader.IsEmptyElement && reader.Name == "kw")
                        {
                            reader.MoveToAttribute("w");///将节点移动“权重”属性上，“权重”属性名为："w"
                            int i = int.Parse(reader.Value);
                            string kwStr = reader.ReadElementString();
                            WordPos wp = new WordPos(kwStr, i);
                            wpList.WordPosList.Add(wp);
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return wpList;
        }

        /// <summary>
        /// 从停用词和标点符号字典中加载数据
        /// </summary>
        /// <param name="filename">停用词或标点符号字典的Xml文件</param>
        private static Dictionary<string, int> LoadStopwords(string filename, Dictionary<string, int> dic)
        {
            int i = 0;
            if (dic == null)
            {
                dic = new Dictionary<string, int>();
            }
            else
            {
                i = dic.Count;
            }
            try
            {
                XmlTextReader reader = new XmlTextReader(filename);
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (!reader.IsEmptyElement && reader.Name == "kw")
                        {
                            string kwStr = reader.ReadElementString();
                            if (!dic.ContainsKey(kwStr))
                            {
                                dic.Add(kwStr, i);
                                i++;
                            }
                        }
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
                throw;
            }
            return dic;
        }


        /// <summary>
        /// Jeelu.WordSeg用到的主词库
        /// </summary>
        internal static WordPosCollection SegWords { get; set; }
        /// <summary>
        /// Jeelu.WordSeg用到的中文停用词字典
        /// </summary>
        internal static Dictionary<string, int> SegChsStopwordDic { get; set; }
        /// <summary>
        /// Jeelu.WordSeg用到的英文停用词字典
        /// </summary>
        internal static Dictionary<string, int> SegEngStopwordDic { get; set; }

        internal static IRule[] Rules { get; private set; }
        internal static ExtractInfo ExtractInfo { get; private set; }

        private static WordPosBuilder _WordPosBuilder { get; set; }
        private static PosBinRule _PosBinRule { get; set; }

        /// <summary>
        /// 获取词语的权重
        /// </summary>
        private static int GetPosWeight(List<WordInfo> words, List<int> list)
        {
            int weight = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                WordInfo w1 = (WordInfo)words[(int)list[i]];
                WordInfo w2 = (WordInfo)words[(int)list[i + 1]];
                if (_PosBinRule.Match(w1.Word, w2.Word))
                {
                    weight++;
                }
            }
            return weight;
        }

        /// <summary>
        /// 比较词性
        /// </summary>
        private static bool CompareByPos(List<WordInfo> words, List<int> pre, List<int> cur)
        {
            return GetPosWeight(words, pre) < GetPosWeight(words, cur);
        }

        /// <summary>
        /// 初始化规则
        /// </summary>
        private static void SetOwnerRules()
        {
            Rules = new IRule[3];
            _PosBinRule = new PosBinRule(_WordPosBuilder);
            Rules[0] = new MergeNumRule(_WordPosBuilder);
            Rules[1] = _PosBinRule;
            Rules[2] = new MatchName(_WordPosBuilder);
        }


    }
}
