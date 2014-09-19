using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Jeelu.Billboard
{

    /// <summary>
    /// Html源码抽取后的各类字符串的组合类型
    /// 含有一个抓取单个页面的静态方法，两个抽取正文的静态方法
    /// design by lukan, 2008-7-9 09:20:42
    /// 
    /// 　　2008-7-18 01:14:14
    ///     用正则抽取正文还没有写；
    /// </summary>
    public class HtmlHelper
    {
        public HtmlHelper(string Url)
        {
            this.Uri = new Uri(Url);
        }

        public string Title { get; private set; }
        public string[] H1Collection { get; private set; }
        public string[] H2Collection { get; private set; }
        public string[] H3Collection { get; private set; }
        public string[] H4Collection { get; private set; }
        public string[] H5Collection { get; private set; }
        public string[] H6Collection { get; private set; }
        public string[] BCollection { get; private set; }
        public string[] ImageAltCollection { get; private set; }
        public string[] LinkAltCollection { get; private set; }
        public string Keyword { get; private set; }
        public string Description { get; private set; }
        public string[] HrefCollection { get; private set; }
        public string Content { get; private set; }

        public Uri Uri { get; private set; }

        public Dictionary<string, ulong> SortedKeywords { get; private set; }

        /// <summary>
        /// 抽取正文的静态方法
        /// 运行较慢，但是从某种意义上来讲开发较快。
        /// </summary>
        /// <param name="webrule">網站的一些規則</param>
        private string GetHtml(WebRule webrule)
        {
            ///抓取網頁
            WebPage webpage = Jeelu.Utility.Web.GetHtmlCodeFromUrl(this.Uri.AbsoluteUri, 5, 30 * 1000);
            webpage.HtmlCode = ClearTag(webpage.HtmlCode, "style");
            webpage.HtmlCode = ClearTag(webpage.HtmlCode, "script");

            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.OptionOutputAsXml = true;

            htmlDoc.LoadHtml(webpage.HtmlCode);

            HtmlNode headElement = htmlDoc.DocumentNode.SelectSingleNode("//head");
            HtmlNode bodyElement = htmlDoc.DocumentNode.SelectSingleNode("//body");

            /////标题
            this.Title = GetTagElementString(headElement, "title");

            ///从Head中获得Keyword节点，Desciption节点
            HtmlNodeCollection metaNodes = headElement.SelectNodes("meta");
            if (metaNodes != null)
            {
                foreach (HtmlNode node in metaNodes)
                {
                    switch (node.GetAttributeValue("name", "").ToLower())
                    {
                        #region case
                        case "keywords":
                            {
                                string content = node.GetAttributeValue("content", "");
                                if (string.IsNullOrEmpty(content))
                                {
                                    break;
                                }
                                this.Keyword += content + ',';
                            }
                            break;
                        case "description":
                            {
                                string content = node.GetAttributeValue("content", "");
                                if (string.IsNullOrEmpty(content))
                                {
                                    break;
                                }
                                this.Description += content + ',';
                            }
                            break;
                        default:
                            break;
                        #endregion
                    }//switch
                }//foreach
            }//if

            ///获得页面的所有链接，目前在Jeelu.Billboard项目中用处不大
            this.HrefCollection = GetTagElementValue(bodyElement, "a", "href").ToArray();

            ///获得页面的所有图片的Alt属性
            this.ImageAltCollection = GetTagElementValue(bodyElement, "img", "alt").ToArray();
            ///获得页面的所有链接的Alt属性
            this.LinkAltCollection = GetTagElementValue(bodyElement, "a", "alt").ToArray();

            ///获得页面的所有加粗设为重点的字符串
            List<string> bList = GetTagElementValue(bodyElement, "strong", "");
            bList.AddRange(GetTagElementValue(bodyElement, "b", "").ToArray());
            this.BCollection = bList.ToArray();

            ///各级标题
            this.H1Collection = GetTagElementValue(bodyElement, "h1", "").ToArray();
            this.H2Collection = GetTagElementValue(bodyElement, "h2", "").ToArray();
            this.H3Collection = GetTagElementValue(bodyElement, "h3", "").ToArray();
            this.H4Collection = GetTagElementValue(bodyElement, "h4", "").ToArray();
            this.H5Collection = GetTagElementValue(bodyElement, "h5", "").ToArray();
            this.H6Collection = GetTagElementValue(bodyElement, "h6", "").ToArray();

            ///整体的字符串
            this.Content = this.GetContent(bodyElement.InnerText, webrule);

            return this.Content;
        }

        /// <summary>
        /// 对一个URL进行完整的处理，抓取->抽取正文->分词->关键词整理(词频统计)
        /// 默认抓取尝试5次，每次等待50毫秒
        /// </summary>
        /// <param name="url">将要访问的Url, 类似"http://Myserver/Mypath/Myfile.asp".</param>
        /// <param name="isFilterStopWords">是否启用“过滤停止词”，客户端使用时应该不过滤停止词而让所有词汇显示</param>
        /// <param name="jWordSegmentor">分詞對象，一般會有一個全局的靜態對象</param>
        /// <param name="wrList">URL規則及正文抽取規則</param>
        /// <returns>经过整理的关键词数组，按权重排序输出</returns>
        public static Dictionary<string, ulong> SetupSingleUrl(
            string url, 
            bool isFilterStopWords, 
            JWordSegmentor jWordSegmentor,
            Dictionary<string, WebRuleCollection> wrcDic)
        {
            Uri uri = new Uri(url);

            string ruleName = WebRule.GetRuleFormUrl(uri);
            WebRuleCollection wrc;
            WebRule webrule = null;
            if (wrcDic.TryGetValue(uri.Host, out wrc))
            {
                webrule = wrc[ruleName];
            }

            HtmlHelper htmlhelper = new HtmlHelper(url);
            foreach (var item in wrcDic)
            {
                if (item.Value.TryGetValue(uri.Host, out webrule))
                {
                    break;
                }
            }
            /// 抓取->抽取正文
            htmlhelper.GetHtml(webrule);
            return KwHelper.KeywordSegmentor(htmlhelper, isFilterStopWords, jWordSegmentor);
        }

        ///// <summary>
        ///// 分词
        ///// </summary>
        ///// <returns>经过整理的关键词数组，按权重排序输出</returns>
        //private Dictionary<string, ulong> KeywordSegmentor(bool isFilterStopWords, JWordSegmentor jWordSegmentor)
        //{
        //    jWordSegmentor.FilterStopWords = isFilterStopWords;
        //    string[] keywords = jWordSegmentor.Segment(this.Content).ToArray();
        //    this.SortedKeywords = KwHelper.KeywordSortor(this, keywords, false, 1);

        //    keywords = jWordSegmentor.Segment(this.Title).ToArray();

        //    return this.SortedKeywords;
        //}

        private static List<string> GetTagElementValue(HtmlNode tagetEle, string nodeName, string attName)
        {
            List<string> srcList = new List<string>();
            HtmlNodeCollection aNodes = tagetEle.SelectNodes("//" + nodeName);
            if (aNodes == null)
            {
                return srcList;
            }
            foreach (HtmlNode node in aNodes)
            {
                string str = "";
                if (string.IsNullOrEmpty(attName))
                {
                    str = node.InnerText.Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        continue;
                    }
                }
                else
                {
                    str = node.GetAttributeValue(attName, "").Trim();
                    if (string.IsNullOrEmpty(str))
                    {
                        continue;
                    }
                }
                srcList.Add(str);
            }
            return srcList;
        }
        private static string GetTagElementString(HtmlNode node, string nodeName)
        {
            StringBuilder sb = new StringBuilder();

            HtmlNodeCollection nodes = node.SelectNodes(nodeName);
            foreach (HtmlNode htmlnode in nodes)
            {
                string str = htmlnode.InnerText;
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                sb.Append(str).Append("_");
            }
            return sb.ToString(0, sb.Length - 1);
        }

        /// <summary>
        /// 无意义的节点，提取之前先用正则删除
        /// </summary>
        private static string ClearTag(string str, string tag)
        {
            string begin = string.Format(@"\<{0}", tag);
            string end = string.Format(@"{0}\>", tag);

            string strReg = string.Format(@"{0}((?!{1}).)*{1}", begin, end);
            Regex regex = new Regex(strReg, RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            string outstr = regex.Replace(str, ReplaceCallback);
            return outstr;
        }
        private static string ReplaceCallback(Match m)
        {
            return "";
        }

        protected virtual string GetContent(string code, WebRule webrule)
        {
            if (webrule == null)
            {
                return code;
            }
            StringBuilder sb = new StringBuilder();
            foreach (ExtractRule item in webrule.ExtractRuleCollection)
            {
                sb.AppendLine(item.Extract(code));
            }
            return sb.ToString();
        }

        #region 暂时一段时间内可能用不到的方法

#if DEBUG
        /// <summary>
        /// 抽取正文的静态方法（正则）
        /// </summary>
        internal static HtmlHelper ExtractContent(string htmlcode)
        {
            return null;
        }
#endif
        #endregion

    }
}
