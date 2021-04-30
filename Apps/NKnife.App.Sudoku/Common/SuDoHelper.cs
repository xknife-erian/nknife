using System;

namespace NKnife.App.Sudoku.Common
{
    public static class SuDoHelper
    {
        public static SuDoXml DoXml { get; private set; }
        public static void Initialize(string doXmlFile)
        {
            DoXml = new SuDoXml(doXmlFile);
        }

        /// <summary>
        /// 获得一个数独题目的字符串转换成的int的数组
        /// </summary>
        /// <param name="doString">数独题目的字符串</param>
        /// <returns></returns>
        public static int[] GetDoIntArrary(string doString)
        {
            int[] intArr = new int[81];
            if (doString.Length != 81 && doString.Length != 161)
            {
                return null;
            }
            else if (doString.Length == 81)
            {
                int i = 0;
                foreach (char var in doString)
                {
                    DoStringToIntArrary(intArr, i, var);
                    i++;
                }
            }
            else if (doString.Length == 161)
            {
                for (int i = 0; i < doString.Length; i = i + 2)
                {
                    DoStringToIntArrary(intArr, i / 2, doString[i]);
                }
            }

            return intArr;
        }

        private static void DoStringToIntArrary(int[] intArr, int i, char var)
        {
            if (var != '1' && var != '2' && var != '3' && var != '4' && var != '5' &&
                var != '6' && var != '7' && var != '8' && var != '9')
            {
                intArr[i] = 0;
            }
            else
            {
                intArr[i] = Convert.ToUInt16(var.ToString());
            }
        }
    }
}
