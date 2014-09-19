using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Jeelu.WordSegmentor;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 中文人名统计
    /// </summary>
    [Serializable]
    class T_ChsNameWordTraffic
    {
        /// <summary>
        /// 单词
        /// </summary>
        public String Word;

        /// <summary>
        /// 单词在人名前出现的次数
        /// </summary>
        public long Before;

        /// <summary>
        /// 单词在人名后出现的次数
        /// </summary>
        public long After;
    }


    /// <summary>
    /// 人名统计
    /// </summary>
    [Serializable]
    class T_ChsNameTraffic
    {
        /// <summary>
        /// 出现在人名前的单词总数
        /// </summary>
        public int BeforeWordCount;

        /// <summary>
        /// 人名前单词出现次数总和
        /// </summary>
        public long BeforeCount;

        /// <summary>
        /// 出现在人名后的单词总数
        /// </summary>
        public int AfterWordCount;

        /// <summary>
        /// 人名后单词出现次数总和
        /// </summary>
        public long AfterCount;

        public List<T_ChsNameWordTraffic> Words = new List<T_ChsNameWordTraffic>();

    }

    /// <summary>
    /// 中文人名统计实现
    /// </summary>
    class CChsNameTraffic
    {
        T_ChsNameTraffic _Traffic;
        Hashtable _Table = new Hashtable();

        public void Clear()
        {
            _Table = new Hashtable();
            _Traffic = new T_ChsNameTraffic();
        }

        public void Save(String fileName)
        {
            Stream s = CSerialization.SerializeBinary(_Traffic);
            s.Position = 0;
            CFile.WriteStream(fileName, (MemoryStream)s);
        }

        public void Load(String fileName)
        {
            try
            {
                List<T_ChsNameWordTraffic> newWords = new List<T_ChsNameWordTraffic>();

                if (!File.Exists(fileName))
                {
                    //字典文件不存在 
                    return;
                }

                MemoryStream s = CFile.ReadFileToStream(fileName);
                s.Position = 0;
                object obj;
                CSerialization.DeserializeBinary(s, out obj);
                _Traffic = (T_ChsNameTraffic)obj;

                _Traffic.AfterCount = 0;
                _Traffic.AfterWordCount = 0;
                _Traffic.BeforeCount = 0;
                _Traffic.BeforeWordCount = 0;

                //整理姓名前缀后缀表
                foreach (T_ChsNameWordTraffic wordTraffic in _Traffic.Words)
                {
                    if (wordTraffic.Word.Length > 1)
                    {
                        if (wordTraffic.Word[0] >= 0x4e00 && wordTraffic.Word[0] <= 0x9fa5)
                        {
                            //是汉字，且长度大于2，才作为有效前后缀
                            newWords.Add(wordTraffic);

                            if (wordTraffic.After > 0)
                            {
                                _Traffic.AfterCount += wordTraffic.After;
                                _Traffic.AfterWordCount++;
                            }

                            if (wordTraffic.Before > 0)
                            {
                                _Traffic.BeforeCount += wordTraffic.After;
                                _Traffic.BeforeWordCount++;
                            }

                        }
                    }
                }

                _Traffic.Words = newWords;
            }
            catch
            {
                _Traffic = new T_ChsNameTraffic();
            }

            foreach (T_ChsNameWordTraffic wordTraffic in _Traffic.Words)
            {
                _Table[wordTraffic.Word] = wordTraffic;
            }
        }

        public void AddBefore(String word)
        {
            if (word.Length <= 1)
            {
                //长度必须大于1
                return;
            }

            if (word[0] < 0x4e00 || word[0] > 0x9fa5)
            {
                //必须是汉字
                return;
            }

            T_ChsNameWordTraffic wordTraffic = (T_ChsNameWordTraffic)_Table[word];
            if (wordTraffic == null)
            {
                wordTraffic = new T_ChsNameWordTraffic();
                wordTraffic.Word = word;
                wordTraffic.Before = 1;
                wordTraffic.After = 0;
                _Traffic.Words.Add(wordTraffic);
                _Table[wordTraffic.Word] = wordTraffic;
                _Traffic.BeforeWordCount++;
                _Traffic.BeforeCount++;
                return;
            }
            else
            {
                wordTraffic.Before++;
                _Traffic.BeforeCount++;
                return;
            }
        }

        public void AddAfter(String word)
        {
            if (word.Length <= 1)
            {
                //长度必须大于1
                return;
            }

            if (word[0] < 0x4e00 || word[0] > 0x9fa5)
            {
                //必须是汉字
                return;
            }


            T_ChsNameWordTraffic wordTraffic = (T_ChsNameWordTraffic)_Table[word];
            if (wordTraffic == null)
            {
                wordTraffic = new T_ChsNameWordTraffic();
                wordTraffic.Word = word;
                wordTraffic.Before = 0;
                wordTraffic.After = 1;
                _Traffic.Words.Add(wordTraffic);
                _Table[wordTraffic.Word] = wordTraffic;
                _Traffic.AfterWordCount++;
                _Traffic.AfterCount++;
                return;
            }
            else
            {
                wordTraffic.After++;
                _Traffic.AfterCount++;
                return;
            }
        }

        /// <summary>
        /// 判断两个词的统计值，第二个比第一个大返回true
        /// </summary>
        /// <param name="fst"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public bool CompareTwoWords(String fst, String sec)
        {
            T_ChsNameWordTraffic cw1 = GetWordTraffic(fst);
            T_ChsNameWordTraffic cw2 = GetWordTraffic(sec);

            if (cw2 != null)
            {
                long after1;

                if (cw1 == null)
                {
                    after1 = 0;
                }
                else
                {
                    after1 = cw1.After;
                }

                if (after1 < cw2.After)
                {
                    return true;
                }
            }

            return false;
        }


        public T_ChsNameWordTraffic GetWordTraffic(String word)
        {
            return (T_ChsNameWordTraffic)_Table[word]; ;
        }

        public bool MaybeNameByAfter(String afterWord)
        {
            T_ChsNameWordTraffic wordTraffic = GetWordTraffic(afterWord);
            if (wordTraffic == null)
            {
                return false;
            }
            if (_Traffic.AfterWordCount == 0)
            {
                return false;
            }
            if (wordTraffic.After >= _Traffic.AfterCount * 5/ _Traffic.AfterWordCount)
            {
                //出现概率大于平均值5倍
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据名字前的单词判断该名字是否可能是汉字名
        /// </summary>
        /// <param name="beforeWord"></param>
        /// <returns></returns>
        public bool MaybeNameByBefore(String beforeWord)
        {
            T_ChsNameWordTraffic wordTraffic = GetWordTraffic(beforeWord);
            if (wordTraffic == null)
            {
                return false;
            }

            if (wordTraffic.Before >= _Traffic.BeforeCount * 5 / _Traffic.BeforeWordCount)
            {
                //出现概率大于平均值5倍

                return true;
            }
            else
            {
                return false;
            }
        }


    }


    /// <summary>
    /// 匹配姓名
    /// </summary>
    class MatchName : IRule
    {
        public delegate void TrafficUnknownWordFunc(String word, T_POS Pos);

        CChsNameTraffic _ChsNameTraffic;
        PosBinRule _PosBinRule;
        CPOS _Pos;
        TrafficUnknownWordFunc _TrafficUnknownWordHandle;

        /// <summary>
        /// 没有明显歧异的姓氏
        /// </summary>
        static string[] FAMILY_NAMES = {
            //有明显歧异的姓氏
            "王","张","黄","周","徐",
            "胡","高","林","马","于",
            "程","傅","曾","叶","余",
            "夏","钟","田","任","方",
            "石","熊","白","毛","江",
            "史","候","龙","万","段",
            "雷","钱","汤","易","常",
            "武","赖","文", "查",

            //没有明显歧异的姓氏
            "赵", "肖", "孙", "李",
            "吴", "郑", "冯", "陈", 
            "褚", "卫", "蒋", "沈", 
            "韩", "杨", "朱", "秦", 
            "尤", "许", "何", "吕", 
            "施", "桓", "孔", "曹",
            "严", "华", "金", "魏",
            "陶", "姜", "戚", "谢",
            "邹", "喻", "柏", "窦",
            "苏", "潘", "葛", "奚",
            "范", "彭", "鲁", "韦",
            "昌", "俞", "袁", "酆", 
            "鲍", "唐", "费", "廉",
            "岑", "薛", "贺", "倪",
            "滕", "殷", "罗", "毕",
            "郝", "邬", "卞", "康",
            "卜", "顾", "孟", "穆",
            "萧", "尹", "姚", "邵",
            "湛", "汪", "祁", "禹",
            "狄", "贝", "臧", "伏",
            "戴", "宋", "茅", "庞",
            "纪", "舒", "屈", "祝",
            "董", "梁", "杜", "阮",
            "闵", "贾", "娄", "颜",
            "郭", "邱", "骆", "蔡",
            "樊", "凌", "霍", "虞",
            "柯", "昝", "卢", "柯",
            "缪", "宗", "丁", "贲",
            "邓", "郁", "杭", "洪",
            "崔", "龚", "嵇", "邢",
            "滑", "裴", "陆", "荣",
            "荀", "惠", "甄", "芮",
            "羿", "储", "靳", "汲", 
            "邴", "糜", "隗", "侯",
            "宓", "蓬", "郗", "仲",
            "栾", "钭", "历", "戎",
            "刘", "詹", "幸", "韶",
            "郜", "黎", "蓟", "溥",
            "蒲", "邰", "鄂", "咸",
            "卓", "蔺", "屠", "乔",
            "郁", "胥", "苍", "莘",
            "翟", "谭", "贡", "劳",
            "冉", "郦", "雍", "璩",
            "桑", "桂", "濮", "扈",
            "冀", "浦", "庄", "晏",
            "瞿", "阎", "慕", "茹",
            "习", "宦", "艾", "容",
            "慎", "戈", "廖", "庾",
            "衡", "耿", "弘", "匡",
            "阙", "殳", "沃", "蔚",
            "夔", "隆", "巩", "聂",
            "晁", "敖", "融", "訾",
            "辛", "阚", "毋", "乜",
            "鞠", "丰", "蒯", "荆",
            "竺", "盍", "单", "欧",
            "司马", "上官", "欧阳",
            "夏侯", "诸葛", "闻人",
            "东方", "赫连", "皇甫",
            "尉迟", "公羊", "澹台",
            "公冶", "宗政", "濮阳",
            "淳于", "单于", "太叔",
            "申屠", "公孙", "仲孙",
            "轩辕", "令狐", "徐离",
            "宇文", "长孙", "慕容",
            "司徒", "司空", "万俟"};

        static Hashtable _FamilyNameTbl;

        bool _AutoStudy = false;

        /// <summary>
        /// 是否自动学习
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
            }
        }

        public TrafficUnknownWordFunc TrafficUnknownWordHandle
        {
            get
            {
                return _TrafficUnknownWordHandle;
            }

            set
            {
                _TrafficUnknownWordHandle = value;
            }
        }

        public MatchName(CPOS pos)
        {
            _PosBinRule = new PosBinRule(pos);
            _Pos = pos;
            _ChsNameTraffic = new CChsNameTraffic();

            _FamilyNameTbl = new Hashtable();
            foreach (String familyName in FAMILY_NAMES)
            {
                _FamilyNameTbl[familyName] = true;
            }
        }

        #region ChsNameTraffic 相关函数

        /// <summary>
        /// 清除姓名统计文件
        /// </summary>
        public void ClearNameTraffic()
        {
            _ChsNameTraffic.Clear();
        }

        /// <summary>
        /// 加入姓名后缀
        /// </summary>
        /// <param name="word"></param>
        public void AddAfter(String word)
        {
            _ChsNameTraffic.AddAfter(word);
        }

        /// <summary>
        /// 加入姓名前缀
        /// </summary>
        /// <param name="word"></param>
        public void AddBefore(String word)
        {
            _ChsNameTraffic.AddBefore(word);
        }


        /// <summary>
        /// 加载姓名统计文件
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadNameTraffic(String fileName)
        {
            _ChsNameTraffic.Load(fileName);
        }

        /// <summary>
        /// 保存姓名统计文件
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveNameTraffic(String fileName)
        {
            _ChsNameTraffic.Save(fileName);
        }

        /// <summary>
        /// 是否是中文名字
        /// </summary>
        /// <param name="familyName">姓</param>
        /// <param name="firstName">名</param>
        /// <returns>是返回true</returns>
        static public bool IsChineseName(String familyName, String firstName)
        {
            if (firstName.Length > 2 || familyName.Length > 2)
            {
                return false;
            }

            return _FamilyNameTbl[familyName] != null;
        }



        #endregion

        #region IRule 成员

        private void Traffic(String beforeWord, String afterWord)
        {
            if (beforeWord != null)
            {
                if (beforeWord.Trim() != "")
                {
                    _ChsNameTraffic.AddBefore(beforeWord);
                }
            }

            if (afterWord != null)
            {
                if (afterWord.Trim() != "")
                {
                    _ChsNameTraffic.AddAfter(afterWord);
                }
            }
        }

        /// <summary>
        /// 匹配姓位于单词首部的情况
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int MatchFamilyNameInHead(List<String> preWords, int index, List<String> retWords)
        {
            String curWord = (String)preWords[index];

            if (index >= preWords.Count - 1)
            {
                return -2;
            }

            if (curWord.Length > 2)
            {
                return -1;
            }

            String nextWord = (String)preWords[index + 1];

            if (curWord[0] < 0x4e00 || curWord[0] > 0x9fa5)
            {
                //不是汉字
                return -2;
            }

            if (nextWord[0] < 0x4e00 || nextWord[0] > 0x9fa5)
            {
                //不是汉字
                return -2;
            }

/*
            if (_PosBinRule.Match(curWord, nextWord))
            {
                return -2;
            }
*/

            String familyName;

            if (curWord.Length == 1)
            {
                if (_FamilyNameTbl[curWord] == null)
                {
                    return -1;
                }
                else
                {
                    familyName = curWord;
                }
            }
            else
            {
                if (_FamilyNameTbl[curWord] == null)
                {
                    if (_FamilyNameTbl[curWord[0].ToString()] == null)
                    {
                        return -1;
                    }
                    else
                    {
                        familyName = curWord[0].ToString();
                    }
                }
                else
                {
                    familyName = curWord;
                }
            }

            String name = curWord + nextWord;

            if (name.Length - familyName.Length == 1)
            {
                //单字名 还要尝试是否是双字名

                if (index < preWords.Count - 2)
                {
                    String nnext = (String)preWords[index + 2];

                    if (nnext.Length >= 2)
                    {
                        if (!_ChsNameTraffic.MaybeNameByAfter(nnext))
                        {
                            String after = nnext.Substring(1, nnext.Length - 1);
                            nnext = nnext[0].ToString();

                            if (_ChsNameTraffic.CompareTwoWords(nnext, after))
                            {
                                name += nnext;
                                retWords.Add(name);
                                retWords.Add(after);

                                //统计
                                if (_AutoStudy)
                                {
                                    String afterWord = after;
                                    String beforeWord = null;
                                    if (index > 0)
                                    {
                                        beforeWord = preWords[index - 1];
                                    }

                                    Traffic(beforeWord, afterWord);
                                }

                                return index + 3;
                            }
                            else
                            {
                                if (index + 3 < preWords.Count)
                                {
                                    if (preWords[index + 3].Length == 1)
                                    {
                                        after += preWords[index + 3];
                                        if (_ChsNameTraffic.CompareTwoWords(nnext, after))
                                        {
                                            //统计
                                            if (_AutoStudy)
                                            {
                                                String afterWord = after;
                                                String beforeWord = null;
                                                if (index > 0)
                                                {
                                                    beforeWord = preWords[index - 1];
                                                }

                                                Traffic(beforeWord, afterWord);
                                            }

                                            name += nnext;
                                            retWords.Add(name);
                                            retWords.Add(after);
                                            return index + 4;

                                        }
                                    }
                                }

                            }
                        }
                    }
                    else if (nnext.Length == 1 &&
                        nnext[0] >= 0x4e00 && nnext[0] <= 0x9fa5)
                    {
                        bool merge = false;

                        if (index + 3 < preWords.Count)
                        {
                            merge = _ChsNameTraffic.CompareTwoWords(preWords[index + 2], preWords[index + 3]);
                        }

                        if (!merge)
                        {
                            merge = !_PosBinRule.MatchNameInHead(nnext);
                        }

                        if (merge)
                        {
                            //统计
                            if (_AutoStudy)
                            {
                                String afterWord = null;
                                String beforeWord = null;
                                if (index > 0)
                                {
                                    beforeWord = preWords[index - 1];
                                }

                                if (index + 3 < preWords.Count)
                                {
                                    afterWord = preWords[index + 3];
                                }

                                Traffic(beforeWord, afterWord);
                            }

                            name += nnext;
                            retWords.Add(name);
                            return index + 3;
                        }
                    }
                }
            }
            else if (name.Length - familyName.Length > 2)
            {
                String nnext = nextWord;
                if (nnext.Length > 1)
                {
                    if (_PosBinRule.MatchNameInHead(nnext.Substring(1, nnext.Length-1)))
                    {
                        //统计
                        if (_AutoStudy)
                        {
                            String afterWord = null;
                            String beforeWord = null;
                            if (index > 0)
                            {
                                beforeWord = preWords[index - 1];
                            }

                            if (index + 2 < preWords.Count)
                            {
                                afterWord = preWords[index + 2];
                            }

                            Traffic(beforeWord, afterWord);
                        }

                        name = curWord + nnext[0].ToString();
                        preWords.Insert(index +2, nnext.Substring(1, nnext.Length - 1));
                        retWords.Add(name);
                        return index + 2;
                    }
                }
            }

            //统计
            if (_AutoStudy)
            {
                String afterWord = null;
                String beforeWord = null;
                if (index > 0)
                {
                    beforeWord = preWords[index - 1];
                }

                if (index + 2 < preWords.Count)
                {
                    afterWord = preWords[index + 2];
                }

                Traffic(beforeWord, afterWord);
            }

            retWords.Add(name);
            return index + 2;
        }


        /// <summary>
        /// 根据统计结果匹配尾部是姓的情况
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int MatchFamilyNameInTailByTraffic(List<String> preWords, int index, List<String> retWords)
        {
            if (retWords.Count < 1)
            {
                return -1;
            }

            String curWord = (String)retWords[retWords.Count - 1];

            if (curWord.Length < 2)
            {
                return -1;
            }

            String nextWord = (String)preWords[index];

            if (nextWord.Length > 2)
            {
                return -1;
            }

            String familyName;

            //单姓
            familyName = curWord[curWord.Length - 1].ToString();

            if (_FamilyNameTbl[familyName] == null)
            {
                //双姓
                familyName = curWord.Substring(curWord.Length - 2, 2);
                if (_FamilyNameTbl[familyName] == null)
                {
                    return -1;
                }
            }

            String remain = curWord.Substring(0, curWord.Length - familyName.Length);

            if (retWords.Count > 0)
            {
                //重新组合前面的词，并判断词性匹配
                String newWord = null;
                bool isReg;

                if (retWords.Count > 1)
                {
                    newWord = retWords[retWords.Count - 2] + remain;
                    if (!_ChsNameTraffic.MaybeNameByBefore(newWord))
                    {
                        newWord = null;
                    }

                    if (newWord != null)
                    {
                        retWords.RemoveAt(retWords.Count - 1);
                        retWords.RemoveAt(retWords.Count - 1);
                    }

                }

                if (newWord != null)
                {
                    retWords.Add(newWord);
                }
                else
                {
                    return -1;
                }
            }

            String name = familyName + nextWord;

            if (name.Length - familyName.Length == 1)
            {
                //单字名 还要尝试是否是双字名

                if (index < preWords.Count - 1)
                {
                    String nnext = name + (String)preWords[index + 1];
                    nnext = nnext.Substring(familyName.Length, nnext.Length - familyName.Length);

                    if (nnext.Length <= 2)
                    {
                        bool merge = false;

                        if (index + 2 < preWords.Count)
                        {
                            merge = _ChsNameTraffic.CompareTwoWords(preWords[index + 1], preWords[index + 2]);
                        }

                        if (!merge)
                        {
                            merge = !_PosBinRule.MatchNameInHead(nnext);
                        }

                        if (merge)
                        {
                            //统计
                            if (_AutoStudy)
                            {
                                String afterWord = null;
                                String beforeWord = null;
                                if (retWords.Count > 0)
                                {
                                    beforeWord = retWords[retWords.Count - 1];
                                }

                                if (index + 2 < preWords.Count)
                                {
                                    afterWord = preWords[index + 2];
                                }

                                Traffic(beforeWord, afterWord);
                            }


                            name = name + (String)preWords[index + 1];
                            retWords.Add(name);
                            return index + 2;
                        }
                    }
                }
            }

            //统计
            if (_AutoStudy)
            {
                String afterWord = null;
                String beforeWord = null;
                if (retWords.Count > 0)
                {
                    beforeWord = retWords[retWords.Count-1];
                }

                if (index + 2 < preWords.Count)
                {
                    afterWord = preWords[index + 1];
                }

                Traffic(beforeWord, afterWord);
            }

            retWords.Add(name);
            return index + 1;
        }




        /// <summary>
        /// 匹配姓位于单词尾部的情况
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int MatchFamilyNameInTail(List<String> preWords, int index, List<String> retWords)
        {
            if (retWords.Count < 1)
            {
                return -1;
            }

            String curWord = (String)retWords[retWords.Count-1];

            if (curWord.Length < 2)
            {
                return -1;
            }

            String nextWord = (String)preWords[index];

            if (nextWord.Length > 2)
            {
                return -1;
            }

            String familyName;

            //单姓
            familyName = curWord[curWord.Length - 1].ToString();

            if (_FamilyNameTbl[familyName] == null)
            {
                familyName = curWord.Substring(curWord.Length-2, 2);
                if (_FamilyNameTbl[familyName] == null)
                {
                    return -1;
                }
            }

            String remain = curWord.Substring(0, curWord.Length - familyName.Length);

            if (retWords.Count > 0)
            {
                //重新组合前面的词，并判断词性匹配
                String newWord = null;
                bool isReg;

                if (retWords.Count > 1)
                {
                    newWord = retWords[retWords.Count - 2] + remain;
                    _Pos.GetPos(newWord, out isReg);
                    if (!isReg)
                    {
                        newWord = null;
                    }
                    else
                    {
                        if (!_PosBinRule.MatchNameInTail(newWord))
                        {
                            newWord = null;
                        }
                    }

                    if (newWord != null)
                    {
                        retWords.RemoveAt(retWords.Count - 1);
                        retWords.RemoveAt(retWords.Count - 1);
                    }

                }

                if (newWord == null)
                {
                    newWord = remain;
                    _Pos.GetPos(newWord, out isReg);
                    if (!isReg)
                    {
                        if (retWords.Count > 1)
                        {
                            newWord = retWords[retWords.Count - 2] + remain;
                            retWords.RemoveAt(retWords.Count - 1);
                        }
                    }
                    else
                    {
                        if (!_PosBinRule.MatchNameInTail(newWord))
                        {
                            newWord = null;
                        }
                    }

                    if (newWord != null)
                    {
                        retWords.RemoveAt(retWords.Count - 1);
                    }
                }

                if (newWord != null)
                {
                    retWords.Add(newWord);
                }
                else
                {
                    return -1;
                }
            }

            String name = familyName + nextWord;

            if (name.Length - familyName.Length == 1)
            {
                //单字名 还要尝试是否是双字名

                if (index < preWords.Count - 1)
                {
                    String nnext = name + (String)preWords[index + 1];
                    nnext = nnext.Substring(familyName.Length, nnext.Length - familyName.Length);

                    if (nnext.Length <= 2)
                    {
                        if (!_PosBinRule.MatchNameInHead(nnext))
                        {
                            name = name + (String)preWords[index + 1];
                            retWords.Add(name);
                            return index + 2;
                        }
                    }
                }
            }

            retWords.Add(name);
            return index + 1;
        }

        /// <summary>
        /// 简单匹配。
        /// 判断第一个单词是不是姓，如果是，
        /// 如果是单字姓，再看第二个单词是不是单子，
        /// 如果是，看两个词是否可以组成双字姓。
        /// 不是也一样按上面逻辑判断是否是双字姓。
        /// 确认姓后，看前面的单词是否是高频姓名前缀，
        /// 如果是，确认为姓名，看后面的单词是否是高频
        /// 姓名后缀，如果是，直接返回姓，如果不是，后面
        /// 单词为两个单字，则判断第二个单字是否是高频后缀
        /// 不是，合并为一个名字，是只取第一个单子。
        /// 如果前面不是高频姓名前缀，判断后面如果两个字之内
        /// 是否有高频姓名后缀，有则确认为姓名。
        /// 确认姓名后，对前后单词加入单词前后缀，并将姓名进行
        /// 记录，等到一定出现频率后，将新的姓名加入词库。
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int SimpleMatch(List<String> preWords, int index, List<String> retWords)
        {
            String prefix = null;
            int curIndex = index;

            //获取前缀
            if (retWords.Count >= 1)
            {
                prefix = (String)retWords[retWords.Count - 1];
            }

            //如果后面没有词了，则不再匹配
            if (preWords.Count - index < 2)
            {
                return -1;
            }

            //第一个单词
            String fstWord = (String)preWords[index];

            //如果第一个单词长度大于2，通常不是中文姓氏，退出姓名匹配
            if (fstWord.Length > 2)
            {
                return -1;
            }

            String famlilyName = null;

            if (_FamilyNameTbl[fstWord] == null)
            {
                //长度为2的单词，如果不是中文姓氏，则直接退出姓名匹配
                if (fstWord.Length == 2)
                {
                    return -1;
                }
            }
            else
            {
                //此时匹配出来的姓氏如果是单字，还需要继续匹配，看是不是双字姓
                famlilyName = fstWord;
            }

            if (fstWord.Length == 1)
            {
                if (preWords[index + 1].Length == 1)
                {
                    if (_FamilyNameTbl[fstWord + preWords[index + 1]] != null)
                    {
                        famlilyName = fstWord + preWords[index + 1];
                        curIndex++;
                    }
                }
            }

            if (famlilyName == null)
            {
                return -1;
            }

            //通过前缀判断是否是姓名
            bool isNameByPrefix = false;

            //通过后缀判断是否是姓名
            bool isNameByPostfix = false;

            //先判断有没有姓名前缀
            if (prefix != null)
            {
                if (_ChsNameTraffic.MaybeNameByBefore(prefix))
                {
                    isNameByPrefix = true;
                }
            }

            int postfixPosition = curIndex + 1;

            //如果没有前缀，又到了最后
            //只有在合并双字姓氏时会发生
            if (preWords.Count - curIndex < 2)
            {
                isNameByPostfix = false;
            }
            else if (preWords[postfixPosition].Length > 2)
            {
                //如果姓氏后面的词长度大于2，通常不是人名
                isNameByPostfix = false;
            }
            else
            {
                if (_ChsNameTraffic.MaybeNameByAfter(preWords[postfixPosition]))
                {
                    //如果姓后面直接跟后缀，直接返回姓

                    isNameByPostfix = true;
                }
                else
                {
                    if (preWords[postfixPosition].Length == 1)
                    {
                        //如果姓后面跟一个单字，还要判断单字后面是后缀还是单字
                        postfixPosition++;

                        if (preWords.Count - postfixPosition < 1)
                        {
                            //已经到最后
                            isNameByPostfix = false;
                        }
                        else if (preWords[postfixPosition][0] < 0x4e00 || preWords[postfixPosition][0] > 0x9fa5)
                        {
                            //如果后面不是汉字，则结束
                            isNameByPostfix = false;
                        }
                        else if (_ChsNameTraffic.MaybeNameByAfter(preWords[postfixPosition]))
                        {
                            //是后缀
                            isNameByPostfix = true;
                        }
                        else
                        {
                            //不是后缀
                            if (preWords[postfixPosition].Length == 1)
                            {
                                //单字名后面还有一个单字，可能是双字名
                                postfixPosition++;

                                if (preWords.Count - postfixPosition < 1)
                                {
                                    //已经到最后
                                    isNameByPostfix = false;
                                }
                                else if (preWords[postfixPosition][0] < 0x4e00 || preWords[postfixPosition][0] > 0x9fa5)
                                {
                                    //如果后面不是汉字，则结束
                                    isNameByPostfix = false;
                                }
                                else if (_ChsNameTraffic.MaybeNameByAfter(preWords[postfixPosition]))
                                {
                                    //是后缀
                                    isNameByPostfix = true;
                                }
                                else
                                {
                                    //不是后缀
                                    isNameByPostfix = false;
                                }
                            }
                            else
                            {
                                //单字后面跟多字，不可能合并为一个名字
                                isNameByPostfix = false;
                            }
                        }
                    }
                    else
                    {
                        //如果姓后面跟一个单字，还要判断单字后面是后缀还是单字
                        postfixPosition++;

                        if (preWords.Count - postfixPosition < 1)
                        {
                            //已经到最后
                            isNameByPostfix = false;
                        }
                        else if (preWords[postfixPosition][0] < 0x4e00 || preWords[postfixPosition][0] > 0x9fa5)
                        {
                            //如果后面不是汉字，则结束
                            isNameByPostfix = false;
                        }
                        else if (_ChsNameTraffic.MaybeNameByAfter(preWords[postfixPosition]))
                        {
                            //是后缀
                            isNameByPostfix = true;
                        }
                        else
                        {
                            isNameByPostfix = true;
                        }
                    }
                }
            }

            if (isNameByPostfix || isNameByPrefix)
            {
                //如果有前缀或后缀确认，则认为是人名
                String name = famlilyName;
                for (int i = curIndex + 1; i < postfixPosition; i++)
                {
                    name += preWords[i];
                }

                retWords.Add(name);

                if (AutoStudy)
                {
                    if (TrafficUnknownWordHandle != null)
                    {
                        TrafficUnknownWordHandle(name, T_POS.POS_A_NR);
                    }
                }

                return postfixPosition;
            }
            else
            {
                return -1;
            }
        }


        public int ProcRule(List<String> preWords, int index, List<String> retWords)
        {

            return SimpleMatch(preWords, index, retWords);

/*
            int idx;
            idx = MatchFamilyNameInTailByTraffic(preWords, index, retWords);

            if (idx < -1)
            {
                return -1;
            }

            if (idx < 0)
            {
                idx = MatchFamilyNameInHead(preWords, index, retWords);
                return idx;
            }
            else
            {
                return idx;
            }

            if (idx < -1)
            {
                return -1;
            }

            if (idx < 0)
            {
                return MatchFamilyNameInTail(preWords, index, retWords);
            }
            else
            {
                return idx;
            }
 */ 
        }

        #endregion
    }
}
