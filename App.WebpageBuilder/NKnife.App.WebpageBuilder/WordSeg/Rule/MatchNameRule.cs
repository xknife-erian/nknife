using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 匹配姓名
    /// </summary>
    class MatchName : IRule
    {
        PosBinRule _PosBinRule;
        WordPosBuilder _Pos;

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
            "竺", "盍", "万俟",
            "司马", "上官", "欧阳",
            "夏侯", "诸葛", "闻人",
            "东方", "赫连", "皇甫",
            "尉迟", "公羊", "澹台",
            "公冶", "宗政", "濮阳",
            "淳于", "单于", "太叔",
            "申屠", "公孙", "仲孙",
            "轩辕", "令狐", "徐离",
            "宇文", "长孙", "慕容",
            "司徒", "司空"};

        static Hashtable _FamilyNameTbl;

        public MatchName(WordPosBuilder pos)
        {
            _PosBinRule = new PosBinRule(pos);
            _Pos = pos;

            _FamilyNameTbl = new Hashtable();
            foreach (String familyName in FAMILY_NAMES)
            {
                _FamilyNameTbl[familyName] = true;
            }
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

        #region IRule 成员

        /// <summary>
        /// 匹配姓位于单词首部的情况
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int MatchFamilyNameInHead(List<string> preWords, int index, List<string> retWords)
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

            if (_PosBinRule.Match(curWord, nextWord))
            {
                return -2;
            }

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

                    if (nnext.Length == 1)
                    {
                        if (!_PosBinRule.MatchNameInHead(nnext))
                        {
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
                        name = curWord + nnext[0].ToString();
                        preWords.Insert(index +2, nnext.Substring(1, nnext.Length - 1));
                        retWords.Add(name);
                        return index + 2;
                    }
                }
            }


            retWords.Add(name);
            return index + 2;
        }

        /// <summary>
        /// 匹配姓位于单词尾部的情况
        /// </summary>
        /// <param name="preWords"></param>
        /// <param name="index"></param>
        /// <param name="retWords"></param>
        /// <returns></returns>
        private int MatchFamilyNameInTail(List<string> preWords, int index, List<string> retWords)
        {
            if (retWords.Count < 1)
            {
                return -1;
            }

            String curWord = retWords[retWords.Count-1];

            if (curWord.Length < 2)
            {
                return -1;
            }

            String nextWord = preWords[index];

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


        public int ProcessRule(List<string> preWords, int index, List<string> retWords)
        {
            int idx = MatchFamilyNameInHead(preWords, index, retWords);
            return idx;
/*
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
