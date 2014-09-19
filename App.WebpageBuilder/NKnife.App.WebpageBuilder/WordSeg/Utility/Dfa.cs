using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 有穷自动机
    /// 单元结构
    /// </summary>
    class T_DfaUnit
    {
        public T_DfaUnit Childs; //该节点的后趋节点
        public T_DfaUnit NextFriend; //该节点的下一个伙伴节点
        public String QuitWord; //结束时应返回的字符串,如果为null，表示没有结束
        public object Tag; //对于技术字符串的标签
        public Char Char; //当前字符
        public bool NeedTrans; //是否需要转义
    }

    /// <summary>
    /// 单词有穷自动机
    /// </summary>
    class CWordDfa
    {
        bool _UseRank ;
        Hashtable _WordsTbl; //存储需要提取的单词的表
        Hashtable _FstCharTbl; //首字Hash表,作为有穷自动机的入口

        private T_DfaUnit AddChar(T_DfaUnit cur, Char c, String quitWord, bool needTrans, object tag)
        {
            T_DfaUnit unit = new T_DfaUnit();
            unit.Char = c;
            unit.NeedTrans = needTrans;
            unit.Childs = null;
            unit.QuitWord = quitWord;
            if (quitWord != null)
            {
                unit.Tag = tag;
            }

            unit.NextFriend = null;

            if (cur == null)
            {
                Debug.Assert(_FstCharTbl[c] == null);
                _FstCharTbl[c] = unit;
            }
            else
            {
                if (cur.Childs == null)
                {
                    cur.Childs = unit;
                }
                else
                {
                    T_DfaUnit friend = cur.Childs;
                    T_DfaUnit oldFriend = friend;
                    while (friend != null)
                    {
                        oldFriend = friend;
                        friend = friend.NextFriend;
                    }

                    oldFriend.NextFriend = unit;
                }
            }

            return unit;
        }

        /// <summary>
        /// 转义符号比较
        /// </summary>
        /// <param name="trans">转义符号</param>
        /// <param name="c">实际字符</param>
        /// <returns>相等返回true</returns>
        private bool TransCharEqual(Char trans, Char c)
        {
            return false;
        }

        /// <summary>
        /// 获取单词对应的权重级别
        /// </summary>
        /// <param name="word">单词</param>
        /// <returns>级别,未找到单词，返回0</returns>
        public int GetRank(String word)
        {
            if (!_UseRank)
            {
                return 0;
            }

            object obj = _WordsTbl[word];
            if (obj != null)
            {
                return (int)obj;
            }
            else
            {
                return 0;
            }
        }

        public T_DfaUnit Next(T_DfaUnit cur, Char c)
        {
            if (cur == null)
            {
                T_DfaUnit unit = (T_DfaUnit)_FstCharTbl[c];
                if (unit == null)
                {
                    return null;
                }
                else
                {
                    return unit;
                }
            }
            else
            {
                T_DfaUnit unit = cur.Childs;
                while (unit != null)
                {
                    if (unit.NeedTrans)
                    {
                        if (TransCharEqual(unit.Char, c))
                        {
                            return cur;
                        }
                    }

                    if (unit.Char == c)
                    {
                        return unit;
                    }

                    unit = unit.NextFriend;
                }
            }


            return null;
        }


        /// <summary>
        /// 遍历有穷自动机，获取最后一个和输入单词匹配的单元
        /// </summary>
        /// <param name="word">单词</param>
        /// <param name="pos">输出位置</param>
        /// <returns>最后一个匹配单元，如果第一个字符就不能匹配，返回null</returns>
        private T_DfaUnit GetLastMatchUnit(String word, out int pos)
        {
            pos = 0;
            T_DfaUnit cur = null;
            T_DfaUnit last = null;

            while (pos < word.Length)
            {
                last = cur;
                cur = Next(cur, word[pos]);
                if (cur == null)
                {
                    return last;
                }

                pos++;
            }

            cur.QuitWord = word;
            return cur;
        }


        /// <summary>
        /// 向有穷自动机输入单词
        /// </summary>
        /// <param name="word">单词</param>
        /// <param name="rank">单词的权重</param>
        public void InsertWordToDfa(String word)
        {
            InsertWordToDfa(word, 0, null);
        }

        /// <summary>
        /// 向有穷自动机输入单词
        /// </summary>
        /// <param name="word">单词</param>
        /// <param name="tag">标签</param>
        public void InsertWordToDfa(String word, object tag)
        {
            InsertWordToDfa(word, 0, tag);
        }

        public void Clear()
        {
            this._WordsTbl.Clear();
            this._FstCharTbl.Clear();
        }

        /// <summary>
        /// 向有穷自动机输入单词
        /// </summary>
        /// <param name="word">单词</param>
        /// <param name="rank">单词的权重</param>
        public void InsertWordToDfa(String word, int rank, object tag)
        {
            if (word == null || word == "")
            {
                return;
            }

            if (rank != 0)
            {
                _UseRank = true;
            }

            if (_WordsTbl[word] != null)
            {
                return;
            }

            _WordsTbl[word] = rank;

            int pos;
            T_DfaUnit unit = GetLastMatchUnit(word, out pos);

            bool needTrans = false;
            for (int i = pos; i < word.Length; i++)
            {
                if (!needTrans && word[i] == '\\')
                {
                    if (i == word.Length - 1)
                    {
                        //最后一个字符是转义符号
                        throw (new Exception("Last char is trans char!"));
                    }
                    //转义
                    needTrans = true;
                    continue;
                }

                if (i == word.Length - 1)
                {
                    unit = AddChar(unit, word[i], word, needTrans, tag);
                }
                else
                {
                    unit = AddChar(unit, word[i], null, needTrans, tag);
                }

                needTrans = false;
            }
        }

        public CWordDfa()
        {
            _WordsTbl = new Hashtable();
            _FstCharTbl = new Hashtable();
            _UseRank = false;
        }


    }


}
