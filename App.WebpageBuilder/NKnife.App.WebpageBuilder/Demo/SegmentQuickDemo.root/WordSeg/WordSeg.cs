using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 基于字典的简单中文分词组件。
    /// design by Lukan, 2008-7-7 02:30:11（算法：基于网络上一些共享算法；流程设计：基于Jeelu的约定设计模式）
    /// </summary>
    public class WordSegmentor
    {
        public WordSegmentor()
        {
            this.IsMatchName = false;
            this.IsFilterStopWords = false;
            this.MatchDirection = Direction.LeftToRight;
        }

        #region 分词.核心公共方法

        /// <summary>
        /// 分词。Jeelu.WordSeg的核心公共方法
        /// </summary>
        /// <param name="str">将要进行分词的字符串</param>
        public List<string> Segmentor(String str)
        {
            List<string> preWords = PreSegment(str);
            List<string> retWords = new List<string>();

            int index = 0;
            while (index < preWords.Count)
            {
                int next = -1;
                foreach (IRule rule in WordSegService.Rules)
                {
                    if (!this.IsMatchName && rule is MatchName)
                    {
                        continue;
                    }
                    next = rule.ProcessRule(preWords, index, retWords);
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
            return retWords;
            //return RecoverUnknowWord(retWords).ToArray();//返回前召回停用词
        }

        /// <summary>
        /// 预分词
        /// </summary>
        /// <param name="str">要分词的句子</param>
        /// <returns>预分词后的字符串输出</returns>
        private List<string> PreSegment(String str)
        {
            List<string> initSeg = new List<string>();

            //如果不包括数字串、日期、英文字母、汉字就返回，有就分成多个块
            if (!CRegex.GetSingleMatchStrings(str, PATTERNS, true, ref initSeg))
            {
                return new List<string>();
            }
            List<string> retWords = new List<string>();
            int i = 0;
            WordSegService.ExtractInfo.MatchDirection = MatchDirection;

            while (i < initSeg.Count)
            {
                String word = initSeg[i];
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
                        )//如果word是数字开头和数字结尾
                    {
                        word = MergeFloat(initSeg, i, ref i);
                        mergeOk = true;
                    }
                    else if ((word[0] >= 'a' && word[0] <= 'z') ||
                             (word[0] >= 'A' && word[0] <= 'Z'))
                    {
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
                    InsertWordToArray(word, retWords);
                }
                else
                {
                    List<WordInfo> words = WordSegService.ExtractInfo.ExtractFullTextMaxMatch(word);
                    int lastPos = 0;
                    foreach (WordInfo wordInfo in words)
                    {
                        if (lastPos < wordInfo.Position)
                        {/*
                            String unMatchWord = word.Substring(lastPos, wordInfo.Position - lastPos);

                            InsertWordToArray(unMatchWord, retWords);        */
                            //将没有匹配的词元进行一个字一词划分
                            for (int j = lastPos; j < wordInfo.Position; j++)
                            {
                                InsertWordToArray(word[j].ToString(), retWords);
                            }
                        }

                        lastPos = wordInfo.Position + wordInfo.Word.Length;
                        InsertWordToArray(wordInfo.Word, retWords);
                    }
                    //将剩下的字做为一个整词划分
                    if (lastPos < word.Length)
                    {
                        InsertWordToArray(word.Substring(lastPos, word.Length - lastPos), retWords);
                    }
                }
                i++;
            }
            return retWords;
        }

        /// <summary>
        /// 分词接口
        /// </summary>
        private List<MatchWord> GetMatchWord(string text)
        {
            List<string> words = Segmentor(text);
            List<MatchWord> ret = new List<MatchWord>();
            int start = 0;
            foreach (string word in words)
            {
                MatchWord m = new MatchWord(word, text.IndexOf(word, start, StringComparison.OrdinalIgnoreCase), word.Length);
                ret.Add(m);
                start = m.Position + m.Length;
            }
            return ret;
        }

        /// <summary>
        /// 召回停用词
        /// </summary>
        private List<string> RecoverUnknowWord(List<string> words)
        {
            List<string> retWords = new List<string>();
            int i = 0;
            while (i < words.Count)
            {
                String w = (String)words[i];
                if (i == words.Count - 1)
                {
                    retWords.Add(w);
                    break;
                }
                if (this.WordPosBuilder.IsUnknowOneCharWord(w))
                {
                    String word = w;
                    i++;
                    while (this.WordPosBuilder.IsUnknowOneCharWord((String)words[i]))
                    {
                        word += (String)words[i];
                        i++;
                        if (i >= words.Count)
                        {
                            break;
                        }
                    }
                    retWords.Add(word);
                    continue;
                }
                else
                {
                    retWords.Add(w);
                }
                i++;
            }
            return retWords;
        }

        #endregion

        #region 成员变量
        
        private const string PATTERNS = @"[０-９\d]+\%|[０-９\d]{1,2}月|[０-９\d]{1,2}日|[０-９\d]{1,4}年|" +
                                        @"[０-９\d]{1,4}-[０-９\d]{1,2}-[０-９\d]{1,2}|" +
                                        @"[０-９\d]+|[^ａ-ｚＡ-Ｚa-zA-Z0-9０-９\u4e00-\u9fa5]|[ａ-ｚＡ-Ｚa-zA-Z]+|[\u4e00-\u9fa5]+";
        #endregion

        #region 属性

        /// <summary>
        /// 是否匹配汉语人名
        /// </summary>
        public bool IsMatchName { get; set; }


        /// <summary>
        /// 是否过滤停用词
        /// </summary>
        public bool IsFilterStopWords { get; set; }


        /// <summary>
        /// 词性
        /// </summary>
        public WordPosBuilder WordPosBuilder { get; private set; }


        /// <summary>
        /// 匹配方向。默认为从左至右匹配,即正向匹配。
        /// </summary>
        public Direction MatchDirection { get; set; }


        #endregion

        #region 私有方法

        /// <summary>
        /// 合并浮点数
        /// </summary>
        /// <param name="words"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private String MergeFloat(List<string> words, int start, ref int end)
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
        private String MergeEmail(List<string> words, int start, ref int end)
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
        /// 视停用词的情况，将一个单词插入到一个List中去
        /// </summary>
        private void InsertWordToArray(String word, List<string> arr)
        {
            if (this.IsFilterStopWords)
            {
                if (WordSegService.SegChsStopwordDic.ContainsKey(word) || WordSegService.SegEngStopwordDic.ContainsKey(word))
                {
                    return;
                }
            }
            arr.Add(word);
        }

        #endregion

    }
}
