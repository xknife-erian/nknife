using System.Collections.Generic;
using NKnife.Base;

namespace NKnife.App.TouchInputKnife.Commons.Pinyin
{
    /// <summary>
    ///     采用动态规划的算法将拼音序列分隔为音节
    ///     参考中科院关于中文语言分割的多篇文献。
    ///     lukan@xknfie.net
    ///     2014-7-4
    /// </summary>
    public class PinyinSeparator : IPinyinSeparator
    {
        /// <summary>
        ///     合法音节库
        /// </summary>
        private readonly ISyllableCollection _SyllableCollection;

        public PinyinSeparator(ISyllableCollection dict)
        {
            _SyllableCollection = dict;
        }

        public List<string> Separate(string pinyin)
        {
            int size = pinyin.Length;

            var result = new List<string>();

            if (_SyllableCollection.Check(pinyin) != 2)
            {
                result.Add(pinyin);
                return result;
            }

            /* ======== 以下采用动态规划进行搜索 ========= */

            // M矩阵（保存子串的最优M值）
            var mMatrix = new int[size, size];
            // P矩阵（保存最优分割的位置）
            var pMatrix = new int[size, size];
            // -|- 以上矩阵行数表示子串开始的位置，列数表示子串结束的位置

            for (int i = 0; i < size; i++)
            {
                int ceil = size - i;
                int column = i;
                int row = 0;
                for (int j = 0; j < ceil; j++)
                {
                    if (row == column)
                    {
                        mMatrix[row, column] = _SyllableCollection.Check("" + pinyin[row]);
                        pMatrix[row, column] = row;
                    }
                    else
                    {
                        int min = int.MaxValue;
                        int pos = row;
                        for (int t = row; t < column; t++)
                        {
                            string subPinyin = pinyin.Substring(row, (column + 1 - row));
                            int check = _SyllableCollection.Check(subPinyin);

                            int value = mMatrix[row, t] + mMatrix[t + 1, column] + check;
                            if (value <= min)
                            {
                                min = value;
                                pos = t;
                            }
                        }
                        mMatrix[row, column] = min;
                        pMatrix[row, column] = pos;
                    }
                    column++;
                    row++;
                }
            }

            var splits = new List<Pair<int, int>>();
            splits.Add(Pair<int, int>.Build(0, pinyin.Length - 1));
            while (!(splits.Count <= 0))
            {
                Pair<int, int> span = splits[0];
                splits.RemoveAt(0);

                int start = span.First;
                int end = span.Second;

                string current = pinyin.Substring(start, end + 1 - start);

                if (_SyllableCollection.Check(current) != 2)
                {
                    result.Add(current);
                    continue;
                }
                int sp = pMatrix[start, end];
                if (sp + 1 < end)
                    splits.Insert(0, Pair<int, int>.Build(sp + 1, end));
                if (start < sp)
                    splits.Insert(0, Pair<int, int>.Build(start, sp));
            }

            return result;
        }
    }
}