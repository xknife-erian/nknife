using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    /// <summary>
    /// 合并数量词规则
    /// </summary>
    class MergeNumRule : IRule
    {
        WordPosBuilder _POS;

        public MergeNumRule(WordPosBuilder pos)
        {
            _POS = pos;
        }

        #region IRule 成员

        public int ProcessRule(List<string> preWords, int index, List<string> retWords)
        {
            String word = (String)preWords[index];
            bool isReg;
            int pos = WordPosBuilder.GetPosFromInnerPosList(_POS.GetPos(word, out isReg));
            String num ;

            if ((pos & (int)PosEnum.POS_A_M) == (int)PosEnum.POS_A_M)
            {
                num = word;
                int i = 0;

                for (i = index + 1; i < preWords.Count; i++)
                {
                    String next = (String)preWords[i];
                    int nextPos = WordPosBuilder.GetPosFromInnerPosList(_POS.GetPos(next, out isReg));
                    if ((nextPos & (int)PosEnum.POS_A_M) == (int)PosEnum.POS_A_M)
                    {
                        num += next;
                    }
                    else
                    {
                        break;
                    }
                }

                if (num == word)
                {
                    return -1;
                }
                else
                {
                    retWords.Add(num);

                    return i;
                }
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
