using System.IO;
using Jeelu.SimplusD;
using System;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusPagePreviewer
{
    class PathService
    {
        static private string _absolutePath;

        /// <summary>
        /// SDsite文件路径
        /// </summary>
        static public string AbsolutePath 
        {
            get
            {
                return _absolutePath;
            }
            set
            {
                _absolutePath = value;
            }
        }

        static public string ProjectName
        {
            get
            {
               return Path.GetFileNameWithoutExtension(_absolutePath);
            }
        }

        /// <summary>
        /// 获取绝对路径的方法
        /// </summary>
        static public string GetAbsoluteFilePath(string path)
        {
            return _absolutePath + path;
        }

        /// <summary>
        /// 删除最后一个/后的内容的剩余部分
        /// </summary>
        static string DelLastPathName(string path)
        {

            int x = path.LastIndexOf('/');
            if (x > 0)
            {

                return path.Substring(0, x);
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 获得资源文件的根目录
        /// </summary>
        static public string GetResourcesFilePath(string index)
        {
            string path = Path.GetDirectoryName(_absolutePath);
            return Path.Combine(Path.Combine(path, "Root"),index);
        }

        /// <summary>
        /// 获取相对工程路径(去掉/后内容)
        /// </summary>
        static public string GetDirectoryOfFile(string path)
        {    
            string filename = Path.GetFileName(path);
            return path.Substring(0, path.Length - filename.Length);
        }

        /// <summary>
        /// 通过两种编码方式获得对应的PageID，有则返回ID，否则反回空
        /// </summary>
        internal static string GetPageGuid(string index)
        {
            string url = EncodingService.EncodingUTF8Change(index);
            string guid=GetShtmlRequestGuid(url);
            if(!string.IsNullOrEmpty(guid))
            {
                return guid;
            }
            else
            {
                url = EncodingService.EncodingGB2312Change(index);
                guid = GetShtmlRequestGuid(url);
                if (!string.IsNullOrEmpty(guid))
                {
                    return guid;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取含有guid的url的guid
        /// </summary>
        internal static string GetAnyUrlGuid(string url)
        {
            string type = Path.GetExtension(url);
            switch (type)
            {
                case Utility.Const.ShtmlFileExt:
                    return url.Substring(url.Length - 38, 32);
                case Utility.Const.CssFileExt:
                    return url.Substring(url.Length - 36, 32);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 根据page请求的URL获得对应的pageID,无法找到返回null
        /// </summary>
        internal static string GetShtmlRequestGuid(string url)
        {
            //try
            //{
            //    string lastUrl = url;
            //    string fileName = Path.GetFileName(url);
            //    string fileType = Path.GetExtension(fileName);
            //    string fileTitle = fileName.Substring(0, fileName.Length - fileType.Length);
            //    string path = url;
            //    url = path.Substring(0, path.Length - fileName.Length);
            //    FolderXmlElement folderXmlEle = (FolderXmlElement)ToHtmlSdsiteService.SdsiteXmlDocument.GetElementById("00000000");
            //    //通过遍历url中的folder与channel，找到pageEle的父节点
            //    while (!string.IsNullOrEmpty(url))
            //    {
            //        string folder = GetFirstPath(url);
            //        folderXmlEle = (FolderXmlElement)folderXmlEle.SelectSingleNode(string.Format("child::*[@fileName='{0}']", folder));
            //        url = DelFirstPath(url);
            //    }

            //    //通过pageEle的父节点找到其本身
            //    SimpleExIndexXmlElement xmlEle = (SimpleExIndexXmlElement)folderXmlEle.SelectSingleNode(string.Format("child::page[@fileName='{0}']", fileTitle));
            //    if (xmlEle != null)
            //    {
            //        return xmlEle.Id;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //catch (Exception e)
            //{
            //    ExceptionService.CreateException(e);
            //    return null;
            //}
            return null;
        }

        /// <summary>
        /// 获取第一层目录名
        /// </summary>
        static private string GetFirstPath(string url)
        {
            int i = url.IndexOf("/");
            if (i > 0)
            {
                return url.Substring(0, i);
            }
            else
            {
                return url;
            }
        }

        /// <summary>
        /// 获取第一层目录名
        /// </summary>
        static private string GetLastPath(string url)
        {
            int i = url.LastIndexOf("/");
            if (i >= 0)
            {
                return url.Substring(i + 1);
            }
            else
            {
                return url;
            }

        }

        /// <summary>
        /// 删除最后一层目录
        /// </summary>
        static private string DelFirstPath(string url)
        {
            int i = url.IndexOf("/");
            if (i > 0)
            {
                return url.Substring(i+1);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获得[? include(*.html)?]的*
        /// </summary>
        public static string GetIncludeHtmlGuid(string strIncludeHtml)
        {
            //Match m = Regex.Match(strIncludeHtml, @"\(\'(?<id>\w+).html\'\)");
            //string url = m.Groups["id"].Value;

            string url = strIncludeHtml.Substring(11, 38);
            return GetAnyUrlGuid(url);
        }

        /// <summary>
        /// 通过各种编码，获得相应的ChannelGuid
        /// </summary>
        internal static string GetChannelGuid(string index)
        {
            string url = EncodingService.EncodingUTF8Change(index);
            string guid = GetChannelRequestGuid(url);
            if (!string.IsNullOrEmpty(guid))
            {
                return guid;
            }
            else
            {
                url = EncodingService.EncodingGB2312Change(index);
                guid = GetChannelRequestGuid(url);
                if (!string.IsNullOrEmpty(guid))
                {
                    return guid;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 通过频道的url返回频道的GUID,没有则返回空
        /// </summary>
        internal static string GetChannelRequestGuid(string index,ToHtmlHelper thh)
        {
            //如果为空返回根节点的Guid
            if (string.IsNullOrEmpty(index)||index=="//")
            {
                return "00000000";
            }

            try
            {
                FolderXmlElement folderXmlEle = (FolderXmlElement)thh.SdsiteXmlDocument.GetElementById("00000000");
                //url = DelFirstPath(url.Substring(1));
                index = index.Substring(0, index.Length - 1);
                string channelName = GetLastPath(index);
                string url = DelLastPathName(index);
                //遍历所有要得到频道的父频道，找到其真实的父频道
                while (!string.IsNullOrEmpty(url))
                {
                    string folder = GetFirstPath(url);
                    folderXmlEle = (FolderXmlElement)folderXmlEle.SelectSingleNode(string.Format("child::*[@fileName='{0}']", folder));
                    url = DelFirstPath(url);
                }
                //通过父频道返回所求频道的id
                SimpleExIndexXmlElement xmlEle = (SimpleExIndexXmlElement)folderXmlEle.SelectSingleNode(string.Format("child::channel[@fileName='{0}']|child::folder[@fileName='{0}']", channelName));
                if (xmlEle != null)
                {
                    return xmlEle.Id;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return null;
            }
        }

        /// <summary>
        /// 获取CSS请求的guid
        /// </summary>
        public static string GetCSSGuid(string index)
        {
            if (index.Contains("?temp="))
            {
                return index.Substring(index.Length-60, 32);
            }
            else
            {
                return GetAnyUrlGuid(index);
            }
        }

        public static string GetUrlPath(string path)
        {
           string fileName=  Path.GetFileName(path);

           return path.Substring(0, path.Length - fileName.Length);
           
        }
    }
}
