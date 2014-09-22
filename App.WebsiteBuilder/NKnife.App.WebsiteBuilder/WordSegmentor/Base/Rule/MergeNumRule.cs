using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 合并数量词规则
    /// </summary>
    class MergeNumRule : IRule
    {
        CPOS _POS;

        public MergeNumRule(CPOS pos)
        {
            _POS = pos;
        }

        #region IRule 成员

        public int ProcRule(List<String> preWords, int index, List<String> retWords)
        {
            String word = (String)preWords[index];
            bool isReg;
            int pos = CPOS.GetPosFromInnerPosList(_POS.GetPos(word, out isReg));
            String num ;

            if ((pos & (int)T_POS.POS_A_M) == (int)T_POS.POS_A_M)
            {
                num = word;
                int i = 0;

                for (i = index + 1; i < preWords.Count; i++)
                {
                    String next = (String)preWords[i];
                    int nextPos = CPOS.GetPosFromInnerPosList(_POS.GetPos(next, out isReg));
                    if ((nextPos & (int)T_POS.POS_A_M) == (int)T_POS.POS_A_M)
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
