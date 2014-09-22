/***************************************************************************************
 * Jeelu.WordSegmentor 简介: Jeelu.WordSegmentor 是由KaiToo搜索开发的一款基于字典的简单中英文分词算法
 * 主要功能: 中英文分词，未登录词识别,多元歧义自动识别,全角字符识别能力
 * 主要性能指标:
 * 分词准确度:90%以上(有待专家的权威评测)
 * 处理速度: 600KBytes/s
 * 
 * 版本: V1.2.02 
 * Copyright(c) 2007 http://www.kaitoo.com 
 * 作者:肖波
 * 授权: 开源GPL
 * 公司网站: http://www.kaitoo.com
 * 个人博客: http://blog.csdn.net/eaglet; http://www.cnblogs.com/eaglet
 * 联系方式: blog.eaglet@gmail.com
 * ***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Diagnostics;


namespace Jeelu.WordSegmentor
{
    public enum T_Direction
    {
        /// <summary>
        /// 从左到右
        /// </summary>
        LeftToRight = 0,

        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft = 1,
    }

    /// <summary>
    /// 单词信息
    /// </summary>
    public class T_WordInfo
    {
        /// <summary>
        /// 单词
        /// </summary>
        public String Word;

        /// <summary>
        /// 单词首字符在全文中的位置
        /// </summary>
        public int Position;

        /// <summary>
        /// 单词的权重级别
        /// </summary>
        public int Rank;

        /// <summary>
        /// 单词对应的标记
        /// </summary>
        public object Tag;
    }

    public delegate bool CompareByPosFunc(List<T_WordInfo> words, List<int> pre, List<int> cur);
    public delegate bool SelectByFreqFunc(List<T_WordInfo> words, List<int> pre, List<int> cur);

    /// <summary>
    /// 从全文中提取指定的单词，及其位置
    /// </summary>
    public class CExtractWords
    {
        CWordDfa _WordDfa;
        List<int> _GameNodes;
        int _MinSpace;
        int _MinDeep;

        T_Direction _MatchDirection;
        CompareByPosFunc _CompareByPos;
        SelectByFreqFunc _SelectByFreq;

        public CompareByPosFunc CompareByPosEvent
        {
            get { return _CompareByPos; }
            set { _CompareByPos = value; }
        }

        public SelectByFreqFunc SelectByFreqEvent
        {
            get { return _SelectByFreq; }
            set { _SelectByFreq = value; }
        }

        /// <summary>
        /// 匹配方向
        /// </summary>
        public T_Direction MatchDirection
        {
            get { return _MatchDirection; }
            set { _MatchDirection = value; }
        }

        public CExtractWords()
        {
            _MatchDirection = T_Direction.LeftToRight;
            _WordDfa = new CWordDfa();
        }

        public object GetTag(String word)
        {
            return _WordDfa.GetTag(word);
        }

        public void InsertWordToDfa(String word, object tag)
        {
            _WordDfa.InsertWordToDfa(word, tag);
        }


        private bool CompareGroup(List<T_WordInfo> words, List<int> pre, List<int> cur, T_Direction direction)
        {
            int i ;

            if (direction == T_Direction.LeftToRight)
            {
                i = 0;
            }
            else
            {
                i = cur.Count - 1;
            }


            while ((direction == T_Direction.LeftToRight && i < cur.Count) ||
                (direction == T_Direction.RightToLeft && i >= 0))

            {
                if (i >= pre.Count)
                {
                    break;
                }

                int preId = (int)pre[i];
                int curId = (int)cur[i];

                if (((T_WordInfo)words[curId]).Word.Length > ((T_WordInfo)words[preId]).Word.Length)
                {
                    return true;
                }
                else if (((T_WordInfo)words[curId]).Word.Length < ((T_WordInfo)words[preId]).Word.Length)
                {
                    return false;
                }

                if (direction == T_Direction.LeftToRight)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            }

            return false;
        }

        /// <summary>
        /// 博弈树
        /// </summary>
        /// <param name="words"></param>
        /// <param name="nodes"></param>
        /// <param name="init"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="spaceNum"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        private List<int> GameTree(List<T_WordInfo> words, List<int> nodes, bool init, int begin, int end, ref int spaceNum, ref int deep)
        {
            if (init)
            {
                int startPos = ((T_WordInfo)words[begin]).Position;
                for (int i = begin; i <= end ; i++) 
                {
                    T_WordInfo wordInfo = (T_WordInfo)words[i];
                    spaceNum = wordInfo.Position - startPos;
                    deep = 0;
                    List<int> oneNodes;

                    if (i == end)
                    {
                        oneNodes = new List<int>();
                        oneNodes.Add(i);
                        deep++;
                    }
                    else
                    {
                        oneNodes = GameTree(words, nodes, false, i, end, ref spaceNum, ref deep);
                    }

                    if (oneNodes != null)
                    {
                        bool select = false;

                        if (_MinSpace > spaceNum ||
                            (_MinSpace == spaceNum && deep < _MinDeep))
                        {
                            select = true;

                            if (_MinSpace == 0)
                            {
                                if (SelectByFreqEvent != null)
                                {
                                    select = SelectByFreqEvent(words, _GameNodes, oneNodes);
                                }
                            }

                        }
                        else if (_MinDeep == deep && _MinSpace == spaceNum)
                        {
                            if (_CompareByPos != null && _MinSpace == 0)
                            {
                                select = _CompareByPos(words, _GameNodes, oneNodes);
                            }
                            else
                            {
                                select = CompareGroup(words, _GameNodes, oneNodes, MatchDirection);
                            }
                        }


                        if (select)
                        {
                            _MinDeep = deep;
                            _MinSpace = spaceNum;
                            _GameNodes.Clear();
                            foreach (int obj in oneNodes)
                            {
                                _GameNodes.Add(obj);
                            }
                        }
                    }
                    deep = 0;
                    nodes.Clear();
                }
            }
            else
            {
                nodes.Add(begin);
                deep++;

                T_WordInfo last = (T_WordInfo)words[begin];

                bool nextStep = false;
                bool reach = false;
                int endPos = last.Position + last.Word.Length - 1;

                int oldDeep = deep;
                int oldSpace = spaceNum;

                for (int i = begin + 1; i <= end; i++)
                {
                    T_WordInfo cur = (T_WordInfo)words[i];

                    if (endPos < cur.Position + cur.Word.Length - 1)
                    {
                        endPos = cur.Position + cur.Word.Length - 1;
                    }


                    if (last.Position + last.Word.Length <= cur.Position)
                    {

                        nextStep = true;

                        if (reach)
                        {
                            reach = false;
                            spaceNum = oldSpace;
                            deep = oldDeep;
                            nodes.RemoveAt(nodes.Count - 1);
                        }

                        spaceNum += cur.Position - (last.Position + last.Word.Length);
                        List<int> oneNodes;
                        oneNodes = GameTree(words, nodes, false, i, end, ref spaceNum, ref deep);

                        if (oneNodes != null)
                        {
                            bool select = false;

                            if (_MinSpace > spaceNum ||
                                (_MinSpace == spaceNum && deep < _MinDeep))
                            {
                                select = true;
                            }
                            else if (_MinDeep == deep && _MinSpace == spaceNum)
                            {
                                if (_CompareByPos != null && _MinSpace == 0)
                                {
                                    select = _CompareByPos(words, _GameNodes, oneNodes);
                                }
                                else
                                {
                                    select = CompareGroup(words, _GameNodes, oneNodes, MatchDirection);
                                }
                            }


                            if (select)
                            {
                                reach = true;
                                nextStep = false;
                                _MinDeep = deep;
                                _MinSpace = spaceNum;
                                _GameNodes.Clear();
                                foreach (int obj in oneNodes)
                                {
                                    _GameNodes.Add(obj);
                                }
                            }
                            else
                            {
                                spaceNum = oldSpace;
                                deep = oldDeep;
                                nodes.RemoveRange(deep, nodes.Count - deep);
                            }
                        }
                        else
                        {
                            spaceNum = oldSpace;
                            deep = oldDeep;
                            nodes.RemoveRange(deep , nodes.Count - deep);
                        }
                    }
                }

                if (!nextStep)
                {
                    spaceNum += endPos - (last.Position + last.Word.Length-1);

                    List<int> ret = new List<int>();

                    foreach (int obj in nodes)
                    {
                        ret.Add(obj);
                    }

                    return ret;
                }


            }

            return null;
        }

        /// <summary>
        /// 最大匹配提取全文中所有匹配的单词
        /// </summary>
        /// <param name="fullText">全文</param>
        /// <returns>返回T_WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public List<T_WordInfo> ExtractFullTextMaxMatch(String fullText)
        {
            List<T_WordInfo> retWords = new List<T_WordInfo>();
            List<T_WordInfo> words = ExtractFullText(fullText);

            int i = 0;

            while (i < words.Count)
            {
                T_WordInfo wordInfo = (T_WordInfo)words[i];

                int j;

                int rangeEndPos = 0;

                for (j = i; j < words.Count-1; j++)
                {
                    if (j - i > 16)
                    {
                        //嵌套太多的情况一般很少发生，如果发生，强行中断，以免造成博弈树遍历层次过多
                        //降低系统效率
                        break;
                    }

                    if (rangeEndPos < ((T_WordInfo)words[j]).Position + ((T_WordInfo)words[j]).Word.Length -1)
                    {
                        rangeEndPos = ((T_WordInfo)words[j]).Position + ((T_WordInfo)words[j]).Word.Length - 1;
                    }

                    if (rangeEndPos <
                        ((T_WordInfo)words[j + 1]).Position)  
                    {
                        break;
                    }
                }

                if (j > i)
                {
                    int spaceNum = 0;
                    int deep = 0;
                    _GameNodes = new List<int>();
                    _MinDeep = 65535;
                    _MinSpace = 65535 * 256;

                    GameTree(words, new List<int>(), true, i, j, ref spaceNum, ref deep);

                    foreach (int index in _GameNodes)
                    {
                        T_WordInfo info = (T_WordInfo)words[index];
                        retWords.Add(info);
                    }

                    i = j + 1;
                    continue;
                }
                else
                {
                    retWords.Add(wordInfo);
                    i++;
                }

                
            }

            return retWords;
        }


        /// <summary>
        /// 提取全文
        /// </summary>
        /// <param name="fullText">全文</param>
        /// <returns>返回T_WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public List<T_WordInfo> ExtractFullText(String fullText)
        {
            List<T_WordInfo> words = new List<T_WordInfo>();

            if (fullText == null || fullText == "")
            {
                return words;
            }

            T_DfaUnit cur = null;
            bool find = false;
            int pos = 0;
            int i = 0;

            while (i < fullText.Length)
            {
                cur = _WordDfa.Next(cur, fullText[i]);
                if (cur != null && !find)
                {
                    pos = i;
                    find = true;
                }

                if (find)
                {
                    if (cur == null)
                    {
                        find = false;
                        i = pos + 1; //有可能存在包含关系的词汇，所以需要回溯
                        continue;
                    }
                    else if (cur.QuitWord != null)
                    {
                        T_WordInfo wordInfo = new T_WordInfo();
                        wordInfo.Word = cur.QuitWord;
                        wordInfo.Position = pos;
                        wordInfo.Rank = _WordDfa.GetRank(wordInfo.Word);
                        wordInfo.Tag = cur.Tag;
                        words.Add(wordInfo);

                        if (cur.Childs == null)
                        {
                            find = false;
                            cur = null;
                            i = pos + 1; //有可能存在包含关系的词汇，所以需要回溯
                            continue;
                        }
                    }
                }

                i++;
            }

            return words;
        }



    }
}
