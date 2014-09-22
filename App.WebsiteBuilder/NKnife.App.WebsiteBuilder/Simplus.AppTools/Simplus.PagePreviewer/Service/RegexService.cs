using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusPagePreviewer
{
    class RegexService
    {
        static readonly string StrShtmlInclude = "<!--#include virtual=\".*\" -->";//找出shtml中include所有内容
        static readonly string StrIncludeRegex = @"\<\? +include\(\'(?<src>[^']*)\'\) +\?\>";
        static readonly string StrKeyListRegex = @"\<\? +include\(\$_GET\[\'keywordList\'\]\.\'\.html\'\) +\?\>";
        static readonly string StrPhpTagTitle = @"<\? echo \$_GET\['title'\] \?>";
        static readonly string StrPhpTagKeywords = @"<\? echo \$_GET\['keywords'\] \?>";
        static readonly string StrPhpTagDescription = @"<\? echo \$_GET\['description'\] \?>";
        static readonly string StrPhpTagAuthor = @"<\? echo \$_GET\['author'\] \?>";
        static readonly string StrPhpContent = @"\<\? +include\(\$_GET\[\'content\'\]\.\'\.html\'\) +\?\>";
        //static readonly string StrContentId = @"content\=.*/(?<contentId>\w+)\.html&title";//找到content=
        static readonly string StrContentId = @"content\=.*/(?<contentId>\w+)\&title";//找到content=
        static readonly Regex IncludeRegex = new Regex(StrIncludeRegex, RegexOptions.ExplicitCapture
            | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static readonly string StrShtmlTitle = @"<title>.*</title>";

        //by zhucai:之前没有加空格的时候若名字里有空格则无法预览，可能还要考虑其他情况
        //static readonly string StrShtmlTagTitle = @"title=[\w ]+&";
        static readonly string StrShtmlTagTitle = @"title=(?<title>[^&]*)&";

        static readonly string StrShtmlTagKeywords = @"keywords=.*&d";
        static readonly string StrShtmlTagDescription = @"description=.*&a";
        static readonly string StrShtmlTagAuthor = @"author=.*";
        /// <summary>
        /// 得到模板中所有[? include(*)?]
        /// </summary>
        static public List<string> GetIncloudeList(string phpstring)
        {
            List<string> incloudeList = new List<string>();
            MatchCollection ms = IncludeRegex.Matches(phpstring);
            foreach (Match m in ms)
            {
                incloudeList.Add(m.Value);
            }

            return incloudeList;
        }

        /// <summary>
        /// 得到模板中[? include("*")?] 的*
        /// </summary>
        static public string GetFileName(string name)
        {
            Match m = IncludeRegex.Match(name);
            string fileName = m.Groups["src"].Value;
            return fileName;
        }

        /// <summary>
        /// 在单独预览php时将不用的部分替换
        /// </summary>
        static public string ChangePhpTag(string phpContent)
        {
            Regex regTag = new Regex(StrPhpTagTitle);
            Match m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, "title(正文页面片的内容不进行显示)");
            }
            regTag = new Regex(StrPhpTagKeywords);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, "keywords");
            }
            regTag = new Regex(StrPhpTagDescription);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, "description");
            }
            regTag = new Regex(StrPhpTagAuthor);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, "author");
            }
            Regex regContent = new Regex(StrPhpContent);
            Match mc = regContent.Match(phpContent);
            if (!string.IsNullOrEmpty(mc.Value))
            {
                phpContent = phpContent.Replace(mc.Value, "");
            }
            return phpContent;
        }

        /// <summary>
        /// 在单独预览shtml时将部分替换
        /// </summary>
        static public string ChangePhpTag(string phpContent, string title, string keywords, string description, string author)
        {
            Regex regTag = new Regex(StrPhpTagTitle);
            Match m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, title);
            }
            regTag = new Regex(StrPhpTagKeywords);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, keywords);
            }
            regTag = new Regex(StrPhpTagDescription);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, description);
            }
            regTag = new Regex(StrPhpTagAuthor);
            m = regTag.Match(phpContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                phpContent = phpContent.Replace(m.Value, author);
            }
            return phpContent;
        }

        /// <summary>
        /// 获取shtml中include所有内容
        /// </summary>
        static public string GetShtmlInclude(string shtmlContent)
        {
            Regex regInclude = new Regex(StrShtmlInclude);
            return regInclude.Match(shtmlContent).Value;
        }

        /// <summary>
        /// 获取ContentId
        /// </summary>
        static public string GetContentId(string strVirtual)
        {
            Regex regx = new Regex(StrContentId);
            string contentId = regx.Match(strVirtual).Groups["contentId"].Value;
            return contentId;
        }

        /// <summary>
        /// 替换 include Get
        /// </summary>
        static public string ChangeContent(string strContent, string strChange)
        {
            Regex contentRegex = new Regex(StrPhpContent, RegexOptions.ExplicitCapture
            | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Match m = contentRegex.Match(strContent);

            try
            {
                if (!string.IsNullOrEmpty(m.Value))
                {
                    strContent = strContent.Replace(m.Value, strChange);
                }
                else
                {
                    strContent = RegexService.ChangeNoContentPage(strContent, strChange);
                }
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
            }
            return strContent;
        }

        private static string ChangeNoContentPage(string strContent, string strchange)
        {
            Regex contentRegex = new Regex(StrShtmlTitle, RegexOptions.ExplicitCapture
            | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Match m = contentRegex.Match(strContent);
            try
            {
                if (!string.IsNullOrEmpty(m.Value))
                {
                    strContent = strContent.Replace(m.Value, m.Value.Substring(0, m.Value.Length - 8) + "(" + strchange + ")" + "</title>");
                }
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
            }
            return strContent;
        }

        /// <summary>
        /// 将页面中的Title，author，keywords，description，进行相应的替换
        /// </summary>
        internal static string ChangeShtmlTag(string content, string quoMarkContent)
        {
            string title = GetPageTitle(quoMarkContent);
            string description = GetDescription(quoMarkContent);
            string author = GetAuthor(quoMarkContent);
            string keywords = GetKeyWords(quoMarkContent);
            return ChangePhpTag(content, title, keywords, description, author);
        }

        /// <summary>
        /// 获取文章描述部分
        /// </summary>
        private static string GetDescription(string quoMarkContent)
        {
            Regex regx = new Regex(StrShtmlTagDescription);
            Match m = regx.Match(quoMarkContent);
            string strmid = m.Value.Substring(12);
            return strmid.Substring(0, strmid.Length - 2);
        }

        /// <summary>
        /// 获取文章作者部分
        /// </summary>
        private static string GetAuthor(string quoMarkContent)
        {
            Regex regx = new Regex(StrShtmlTagAuthor);
            Match m = regx.Match(quoMarkContent);
            string strmid = m.Value.Substring(7);
            return strmid;
        }

        /// <summary>
        /// 获得文章关键字
        /// </summary>
        private static string GetKeyWords(string quoMarkContent)
        {
            Regex regx = new Regex(StrShtmlTagKeywords);
            Match m = regx.Match(quoMarkContent);
            if (!string.IsNullOrEmpty(m.Value))
            {
                string strmid = m.Value.Substring(9);
                return strmid.Substring(0, strmid.Length - 2);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取正文标题
        /// </summary>
        private static string GetPageTitle(string quoMarkContent)
        {
            Regex regx = new Regex(StrShtmlTagTitle);
            Match m = regx.Match(quoMarkContent);
            return m.Groups["title"].Value;
            //string strmid = m.Value.Substring(6);
            //return strmid.Substring(0, strmid.Length - 1);
        }

        /// <summary>
        /// 将关键字列表添加到页面中
        /// </summary>
        public static string ChangeKeyList(string phpContent, string KeyListSnip)
        {
            Regex regx = new Regex(StrKeyListRegex);
            Match m = regx.Match(phpContent);
            return phpContent.Replace(m.Value, KeyListSnip);
        }
    }
}