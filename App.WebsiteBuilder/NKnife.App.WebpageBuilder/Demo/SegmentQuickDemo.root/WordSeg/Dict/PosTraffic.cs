using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    public class PosTraffic
    {
        Hashtable _PosBinTbl = new Hashtable();
        List<DualityWordBin> _PosBinList = new List<DualityWordBin>();

        public WordPosBuilder POS { get; set; }

        private void Hit(DualityWordBin posBin)
        {
            DualityWordBin bin = (DualityWordBin)_PosBinTbl[posBin.HashCode];
            if (bin == null)
            {
                bin = new DualityWordBin(posBin._Pos1, posBin._Pos2);
                bin._Count = 1;
                _PosBinTbl[bin.HashCode] = bin;
                _PosBinList.Add(bin);
            }
            else
            {
                bin._Count++;
            }
        }

        public List<DualityWordBin> GetPosBinGroup()
        {
            _PosBinList.Sort();
            return _PosBinList;
        }

        public void Traffic(List<string> words)
        {
            foreach (string word in words)
            {
                bool isReg;
                InnerPos[] curPos = this.POS.GetPos(word, out isReg);
                InnerPos[] nextPos = this.POS.GetPos(word, out isReg);

                if (curPos.Length != 1)
                {
                    continue;
                }

                InnerPos pos1 = curPos[0];
                if (pos1 == InnerPos.POS_UNK)
                {
                    continue;
                }

                if (nextPos.Length != 1)
                {
                    continue;
                }

                InnerPos pos2 = (InnerPos)nextPos[0];
                if (pos2 == InnerPos.POS_UNK)
                {
                    continue;
                }

                DualityWordBin bin = new DualityWordBin(pos1, pos2);
                Hit(bin);
            }
        }

    }
}
