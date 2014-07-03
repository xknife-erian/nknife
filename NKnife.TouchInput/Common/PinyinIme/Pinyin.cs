using System.Collections.Generic;
using System.Linq;
using Microsoft.International.Converters.PinYinConverter;
using NKnife.Chinese;

namespace NKnife.TouchInput.Common.PinyinIme
{
    public class Pinyin
    {
        /// <summary>
        /// 获取指定拼音的汉字集合
        /// </summary>
        /// <param name="input">指定的拼音</param>
        /// <param name="sortByFreq">输入是否按使用频率进行排序</param>
        /// <returns></returns>
        public static char[] GetCharArrayOfPinyin(string input, bool sortByFreq = true)
        {
            var charArray = new List<char>();
            for (int i = 1; i <= 5; i++)
            {
                char[] chars = ChineseChar.GetChars(string.Format("{0}{1}", input, i));
                if (chars != null)
                {
                    charArray.AddRange(chars);
                }
            }
            if (sortByFreq)
            {
                return ListProcess(charArray);
            }
            return charArray.ToArray();
        }

        private static char[] ListProcess(List<char> charArray)
        {
            var ilist = new List<int>(charArray.Count);
            var map = new Dictionary<int, char>(charArray.Count);
            foreach (var c in charArray)
            {
                int i = SimplifyChineseChar.IndexOf(c);
                if (i <0 || ilist.Contains(i))
                {
                    continue;
                }
                ilist.Add(i);
                map.Add(i, c);
            }
            ilist.Sort();
            charArray.Clear();
            charArray.AddRange(ilist.Select(i => map[i]));
            return charArray.ToArray();
        }
    }
}