using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSegmentor
{
    public class PosTraffic
    {
        Hashtable _PosBinTbl = new Hashtable();
        ArrayList _PosBinList = new ArrayList();

        CPOS _POS;

        public CPOS POS
        {
            get
            {
                return _POS;
            }

            set
            {
                _POS = value;
            }
        }

        private void Hit(T_POSBin posBin)
        {
            T_POSBin bin = (T_POSBin)_PosBinTbl[posBin.HashCode];
            if (bin == null)
            {
                bin = new T_POSBin(posBin._Pos1, posBin._Pos2);
                bin._Count = 1;
                _PosBinTbl[bin.HashCode] = bin;
                _PosBinList.Add(bin);
            }
            else
            {
                bin._Count++;
            }
        }

        public ArrayList GetPosBinGroup()
        {
            _PosBinList.Sort();
            return _PosBinList;
        }

        public void Traffic(List<String> words)
        {
            for (int i = 0; i < words.Count-1; i++)
            {
                bool isReg;
                T_INNER_POS[] curPos = _POS.GetPos((String)words[i], out isReg);
                T_INNER_POS[] nextPos = _POS.GetPos((String)words[i + 1], out isReg);


                //ArrayList curList = _POS.GetPosList(curPos);

                if (curPos.Length != 1)
                {
                    continue;
                }

                T_INNER_POS pos1 = curPos[0];

                if (pos1 == T_INNER_POS.POS_UNK)
                {
                    continue;
                }


                //ArrayList nextList = _POS.GetPosList(nextPos);

                if (nextPos.Length != 1)
                {
                    continue;
                }

                T_INNER_POS pos2 = (T_INNER_POS)nextPos[0];

                if (pos2 == T_INNER_POS.POS_UNK)
                {
                    continue;
                }

                T_POSBin bin = new T_POSBin(pos1, pos2);

                Hit(bin);
            }
        }

    }
}
