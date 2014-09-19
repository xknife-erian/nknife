using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Jeelu.WordSeg
{
    internal class PosBinRule : IRule
    {
        WordPosBuilder _POS;
        Hashtable _PosBinTbl;

        DualityWordBin[] _PosBins = {
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_N), //  478 , 动词 动语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_V), //  469 , 名词 名语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_N), //  414 , 名词 名语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_V), //  360 , 动词 动语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_U , InnerPos.POS_D_N), //  317 , 助词 助语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_U), //  226 , 名词 名语素-助词 助语素
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_V), //  221 , 副词 副语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_U), //  213 , 动词 动语素-助词 助语素
            new DualityWordBin(InnerPos.POS_D_U , InnerPos.POS_D_V), //  147 , 助词 助语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_D), //  127 , 名词 名语素-副词 副语素
            new DualityWordBin(InnerPos.POS_D_P , InnerPos.POS_D_N), //  118 , 介词-名词 名语素
            new DualityWordBin(InnerPos.POS_D_C , InnerPos.POS_D_V), //  84  , 连词 连语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_P), //  83  , 动词 动语素-介词
            new DualityWordBin(InnerPos.POS_D_A , InnerPos.POS_D_N), //  81  , 形容词 形语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_C), //  76  , 名词 名语素-连词 连语素
            new DualityWordBin(InnerPos.POS_D_C , InnerPos.POS_D_N), //  75  , 连词 连语素-名词 名语素
            new DualityWordBin(InnerPos.POS_A_NS, InnerPos.POS_D_N), //  75  , 地名-名词 名语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_A), //  74  , 名词 名语素-形容词 形语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_A), //  72  , 动词 动语素-形容词 形语素
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_P), //  71  , 名词 名语素-介词
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_D_F), //  70  , 名词 名语素-方位词 方位语素
            new DualityWordBin(InnerPos.POS_D_A , InnerPos.POS_D_V), //  67  , 形容词 形语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_R , InnerPos.POS_D_N), //  65  , 代词 代语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_R , InnerPos.POS_D_V), //  58  , 代词 代语素-动词 动语素
            new DualityWordBin(InnerPos.POS_A_M , InnerPos.POS_D_V), //  58  , 数词 数语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_B , InnerPos.POS_D_N), //  57  , 区别词 区别语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_P , InnerPos.POS_D_V), //  56  , 介词-动词 动语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_D), //  54  , 动词 动语素-副词 副语素
            new DualityWordBin(InnerPos.POS_D_A , InnerPos.POS_D_U), //  50  , 形容词 形语素-助词 助语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_R), //  48  , 动词 动语素-代词 代语素
            new DualityWordBin(InnerPos.POS_D_P , InnerPos.POS_A_NS), // 45  , 介词-地名
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_N ), // 44  , 副词 副语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_N ), // 44  , 副词 副语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_P ), // 43  , 副词 副语素-介词
            new DualityWordBin(InnerPos.POS_A_M , InnerPos.POS_D_N ), // 43  , 数词 数语素-名词 名语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_A_M ), // 40  , 动词 动语素-数词 数语素
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_D ), // 36  , 副词 副语素-副词 副语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_C ), // 36  , 动词 动语素-连词 连语素
            new DualityWordBin(InnerPos.POS_D_D , InnerPos.POS_D_A ), // 34  , 副词 副语素-形容词 形语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_A_NS), // 34  , 动词 动语素-地名
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_F ), // 34  , 动词 动语素-方位词 方位语素
            new DualityWordBin(InnerPos.POS_D_T , InnerPos.POS_D_V ), // 31  , 时间词-动词 动语素
            new DualityWordBin(InnerPos.POS_D_U , InnerPos.POS_D_A ), // 30  , 助词 助语素-形容词 形语素
            new DualityWordBin(InnerPos.POS_A_NS, InnerPos.POS_D_V ), // 30  , 地名-动词 动语素
            new DualityWordBin(InnerPos.POS_D_U , InnerPos.POS_A_M ), // 29  , 助词 助语素-数词 数语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_B  ), // 29  , 动词 动语素-区别词 区别语素
            new DualityWordBin(InnerPos.POS_D_F , InnerPos.POS_D_V  ), // 26  , 方位词 方位语素-动词 动语素
            new DualityWordBin(InnerPos.POS_D_C , InnerPos.POS_D_P  ), // 24  , 连词 连语素-介词
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_A_NZ ), // 23  , 动词 动语素-其他专名
            new DualityWordBin(InnerPos.POS_D_N , InnerPos.POS_A_M  ), // 23  , 名词 名语素-数词 数语素
            new DualityWordBin(InnerPos.POS_A_Q , InnerPos.POS_D_N  ), // 22  , 量词 量语素-名词 名语素
            new DualityWordBin(InnerPos.POS_A_NZ, InnerPos.POS_D_V  ), // 22  , 其他专名-动词 动语素
            new DualityWordBin(InnerPos.POS_D_R , InnerPos.POS_D_D  ), // 21  , 代词 代语素-副词 副语素
            new DualityWordBin(InnerPos.POS_A_NZ, InnerPos.POS_D_N  ), // 21  , 其他专名-名词 名语素
            new DualityWordBin(InnerPos.POS_D_V , InnerPos.POS_D_E  ), //20  , 动词 动语素-叹词 叹语素        
        };


        public PosBinRule(WordPosBuilder pos)
        {
            _POS = pos;
            _PosBinTbl = new Hashtable();

            foreach (DualityWordBin bin in _PosBins)
            {
                _PosBinTbl[bin.HashCode] = true;
            }
        }

        /// <summary>
        /// 人名和前面的词词性匹配
        /// </summary>
        /// <param name="nextStr"></param>
        /// <returns></returns>
        public bool MatchNameInTail(String preStr)
        {
            bool isReg;
            InnerPos[] preList = _POS.GetPos(preStr, out isReg);

            if (preList.Length == 0)
            {
                return false;
            }

            foreach (InnerPos pre in preList)
            {
                if (pre == InnerPos.POS_UNK)
                {
                    continue;
                }

                DualityWordBin posBin = new DualityWordBin(pre, InnerPos.POS_D_N);
                if (_PosBinTbl[posBin.HashCode] != null)
                {
                    return true;
                }

                posBin = new DualityWordBin(pre, InnerPos.POS_A_NR);
                if (_PosBinTbl[posBin.HashCode] != null)
                {
                    return true;
                }

            }

            return false;
        }


        /// <summary>
        /// 人名和后面的词词性匹配
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public bool MatchNameInHead(String nextStr)
        {
            bool isReg;
            InnerPos[] nextList = _POS.GetPos(nextStr, out isReg);

            if (nextList.Length == 0)
            {
                return false;
            }

            foreach (InnerPos next in nextList)
            {
                if (next == InnerPos.POS_UNK)
                {
                    continue;
                }

                DualityWordBin posBin = new DualityWordBin(InnerPos.POS_D_N, next);
                if (_PosBinTbl[posBin.HashCode] != null)
                {
                    return true;
                }

                posBin = new DualityWordBin(InnerPos.POS_A_NR, next);
                if (_PosBinTbl[posBin.HashCode] != null)
                {
                    return true;
                }

            }

            return false;
        }



        public bool Match(String str1, String str2)
        {
            bool isReg;

            InnerPos[] posList1 = _POS.GetPos(str1, out isReg);
            if (!isReg)
            {
                return false;
            }

            InnerPos[] posList2 = _POS.GetPos(str2, out isReg);
            if (!isReg)
            {
                return false;
            }

            if (posList1.Length == 0 || posList2.Length == 0)
            {
                return false;
            }

            foreach (InnerPos pos1 in posList1)
            {
                if (pos1 == InnerPos.POS_UNK)
                {
                    continue;
                }

                foreach (InnerPos pos2 in posList2)
                {
                    if (pos2 == InnerPos.POS_UNK)
                    {
                        continue;
                    }

                    DualityWordBin posBin = new DualityWordBin(pos1, pos2);
                    if (_PosBinTbl[posBin.HashCode] != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        #region IRule 成员

        public int ProcessRule(List<string> preWords, int index, List<string> retWords)
        {
            if (retWords.Count == 0)
            {
                return -1;
            }

            if (Match((String)retWords[retWords.Count - 1], (String)preWords[index]))
            {
                retWords.Add(preWords[index]);
                return index + 1;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
