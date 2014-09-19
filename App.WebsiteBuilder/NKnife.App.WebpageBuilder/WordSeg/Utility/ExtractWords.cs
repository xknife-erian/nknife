using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Diagnostics;


namespace Jeelu.WordSeg
{
    /// <summary>
    /// 从全文中提取指定的单词，及其位置
    /// </summary>
    public class ExtractInfo
    {
        CWordDfa _WordDfa;
        List<int> _GameNodes;
        int _MinSpace;
        int _MinDeep;

        public CompareByPosFunc CompareByPosEvent { get; set; }

        /// <summary>
        /// 匹配方向
        /// </summary>
        public Direction MatchDirection { get; set; }

        public ExtractInfo()
        {
            this.MatchDirection = Direction.LeftToRight;
            _WordDfa = new CWordDfa();
        }

        public void InsertWordToDfa(String word)
        {
            _WordDfa.InsertWordToDfa(word);
        }

        public void Clear()
        {
            _WordDfa.Clear();
        }

        private bool CompareGroup(List<WordInfo> words, List<int> pre, List<int> cur, Direction direction)
        {
            int i ;

            if (direction == Direction.LeftToRight)
            {
                i = 0;
            }
            else
            {
                i = cur.Count - 1;
            }


            while ((direction == Direction.LeftToRight && i < cur.Count) ||
                (direction == Direction.RightToLeft && i >= 0))

            {
                if (i >= pre.Count)
                {
                    break;
                }

                int preId = (int)pre[i];
                int curId = (int)cur[i];

                if (words[curId].Word.Length > words[preId].Word.Length)
                {
                    return true;
                }
                else if (((WordInfo)words[curId]).Word.Length < ((WordInfo)words[preId]).Word.Length)
                {
                    return false;
                }

                if (direction == Direction.LeftToRight)
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
        private List<int> GameTree(List<WordInfo> words, List<int> nodes, bool init, int begin, int end, ref int spaceNum, ref int deep)
        {
            if (init)
            {
                int startPos = ((WordInfo)words[begin]).Position;
                for (int i = begin; i <= end ; i++) 
                {
                    WordInfo wordInfo = (WordInfo)words[i];
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
                        }
                        else if (_MinDeep == deep && _MinSpace == spaceNum)
                        {
                            if (this.CompareByPosEvent != null && _MinSpace == 0)
                            {
                                select = this.CompareByPosEvent(words, _GameNodes, oneNodes);
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

                WordInfo last = (WordInfo)words[begin];

                bool nextStep = false;
                bool reach = false;
                int endPos = last.Position + last.Word.Length - 1;

                int oldDeep = deep;
                int oldSpace = spaceNum;

                for (int i = begin + 1; i <= end; i++)
                {
                    WordInfo cur = (WordInfo)words[i];

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
                                if (this.CompareByPosEvent != null && _MinSpace == 0)
                                {
                                    select = this.CompareByPosEvent(words, _GameNodes, oneNodes);
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
        /// <returns>返回WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public List<WordInfo> ExtractFullTextMaxMatch(String fullText)
        {
            List<WordInfo> retWords = new List<WordInfo>();
            List<WordInfo> words = ExtractFullText(fullText);

            int i = 0;

            while (i < words.Count)
            {
                WordInfo wordInfo = words[i];

                int j;

                int rangeEndPos = 0;

                for (j = i; j < words.Count-1; j++)
                {
                    if (j - i > 16)
                    {
                        //嵌套太多的情况一般很少发生，如果发生，强行中断，以免造成博弈树遍历层次过多降低系统效率
                        break;
                    }

                    if (rangeEndPos < (words[j].Position + words[j].Word.Length -1))
                    {
                        rangeEndPos = words[j].Position + words[j].Word.Length - 1;
                    }

                    if (rangeEndPos <
                        ((WordInfo)words[j + 1]).Position)  
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

                    GameTree(words,new List<int>(),true, i, j, ref spaceNum, ref deep);

                    foreach (int index in _GameNodes)
                    {
                        WordInfo info = words[index];
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
        /// <returns>返回WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public List<WordInfo> ExtractFullText(String fullText)
        {
            List<WordInfo> words = new List<WordInfo>();

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
                        WordInfo wordInfo = new WordInfo();
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

    /// <summary>
    /// 方向枚举
    /// </summary>
    public enum Direction
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

    public delegate bool CompareByPosFunc(List<WordInfo> words, List<int> pre, List<int> cur);


}
