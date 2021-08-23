using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Microsoft.International.Converters.PinYinConverter;
using NKnife.Chinese;

namespace NKnife.App.TouchInputKnife.Commons.Pinyin
{
    public class Pinyin
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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
            _logger.Trace(string.Format("拼音分割完成:{0},{1}", input, charArray.Count));
            if (sortByFreq)
            {
                return ListFrequencySort(charArray);
            }
            return charArray.ToArray();
        }

        /// <summary>
        /// 按汉字的使用频率进行重新排序
        /// </summary>
        /// <param name="charArray">需要排序的汉字集合</param>
        /// <returns></returns>
        private static char[] ListFrequencySort(List<char> charArray)
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
            _logger.Trace("ListFrequencySort完成");
            return charArray.ToArray();
        }

    }
}