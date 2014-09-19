using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace Jeelu.Billboard
{
    public class KwHelper
    {
        /// <summary>
        /// 根据关键词匹配广告。核心算法，2008-7-9命名为共振算法。
        /// </summary>
        /// <param name="pageKeywords">网页关键词</param>
        /// <param name="adInvertedDic">广告与广告关键词的倒排索引字典</param>
        /// <returns>匹配的广告ID</returns>
        public static long[] Match(Dictionary<string, ulong> pageKeywords, Dictionary<string, List<long>> adInvertedDic)
        {
            Dictionary<long, ulong> adIdMap = new Dictionary<long, ulong>();
            //依次取关键词
            foreach (KeyValuePair<string, ulong> keyword in pageKeywords)
            {
                List<long> adIds;
                if (adInvertedDic.TryGetValue(keyword.Key, out adIds))
                {
                    foreach (long adId in adIds)
                    {
                        ulong weight = 0;
                        adIdMap.TryGetValue(adId, out weight);
                        weight++;
                        adIdMap[adId] = weight;
                    }
                }
            }
            //TODO: 依据权重排序
            List<long> list = new List<long>(adIdMap.Keys);
            return list.ToArray();
        }


        internal static Dictionary<string, ulong> KeywordSegmentor(HtmlHelper htmlhelper, bool isFilterStopWords, JWordSegmentor jWordSegmentor)
        {
            string[] keywords;
            Dictionary<string, ulong> kwDic = new Dictionary<string, ulong>();
            jWordSegmentor.FilterStopWords = isFilterStopWords;

            keywords = jWordSegmentor.Segment(htmlhelper.Content).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 1);

            keywords = jWordSegmentor.Segment(htmlhelper.Title).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 10);

            keywords = jWordSegmentor.Segment(htmlhelper.Keyword).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 3);

            keywords = jWordSegmentor.Segment(htmlhelper.Description).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 2);

            StringBuilder sb = new StringBuilder();
            foreach (var item in htmlhelper.BCollection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 8);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H1Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 7);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H2Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 7);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H3Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 5);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H4Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 4);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H5Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 3);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.H6Collection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 2);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.ImageAltCollection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 4);

            sb = new StringBuilder();
            foreach (var item in htmlhelper.LinkAltCollection)
            {
                sb.AppendLine(item);
            }
            keywords = jWordSegmentor.Segment(sb.ToString()).ToArray();
            kwDic = SetKeywordDic(keywords, kwDic, 6);

            return kwDic;
        }

        private static Dictionary<string, ulong> SetKeywordDic(string[] keywords, Dictionary<string, ulong> kwDic, uint i)
        {
            foreach (string str in keywords)
            {
                if (Regex.IsMatch(str, @"^(\d+.)?\d+\%?$"))///通过正则删除整数，实数，百分比
                {
                    continue;
                }
                if (kwDic.ContainsKey(str))
                {
                    kwDic[str] = kwDic[str] + i;
                }
                else
                {
                    kwDic.Add(str, i);
                }
            }
            return kwDic;
        }

        /// <summary>
        /// 对取出的关键词进行词频统计
        /// </summary>
        /// <param name="keywords">关键词数组</param>
        /// <param name="isSingleWord">过滤单字词，客户端不应过滤,true过滤,false不过滤</param>
        /// <returns>Key为关键词，Value是词频的一个字典</returns>
        public static Dictionary<string, ulong> KeywordSortor(string[] keywords, bool isSingleWord, uint i)
        {
            Dictionary<string, ulong> kwDic = new Dictionary<string, ulong>();
            /// 统计
            foreach (string keyword in keywords)
            {
                if (isSingleWord && keyword.Length <= 1)
                {
                    continue;
                }
                if (Regex.IsMatch(keyword, @"^(\d+.)?\d+\%?$"))
                {
                    continue;
                }
                if (kwDic.ContainsKey(keyword))
                {
                    kwDic[keyword]++;
                }
                else
                {
                    kwDic.Add(keyword, 1);
                }
            }

            SortedDictionary<ulong, List<string>> sortedDic = new SortedDictionary<ulong, List<string>>();
            foreach (var item in kwDic)
            {
                if (sortedDic.ContainsKey(item.Value))
                {
                    sortedDic[item.Value].Add(item.Key);
                }
                else
                {
                    List<string> t = new List<string>();
                    t.Add(item.Key);
                    sortedDic.Add(item.Value, t);
                }
            }

            // 输出
            kwDic = new Dictionary<string, ulong>();
            foreach (var item in sortedDic)
            {
                foreach (var subitem in item.Value)
                {
                    kwDic.Add(subitem, (ulong)(item.Key * i));
                }
            }
            return kwDic;
        }
    }
}


//List<KeyValuePair<string, ulong>> sortList = new List<KeyValuePair<string, ulong>>();
//foreach (KeyValuePair<string, ulong> pair in kwDic)
//{
//    sortList.Add(pair);
//}
//Array.Sort(sortList.ToArray(), new Comparison<KeyValuePair<string, ulong>>(
//       delegate(KeyValuePair<string, ulong> p1, KeyValuePair<string, ulong> p2)
//       {
//           return (int)(p1.Value - p2.Value);
//       })
//       );