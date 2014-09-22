using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.SimplusD;

namespace Jeelu.SimplusPagePreviewer
{
    public class GetResourceAbsolutePath
    {
        /// <summary>
        /// 获取绝对路径
        /// </summary>
        public static string GetResourceAbsPath(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)ToHtmlSdsiteService.SdsiteXmlDocument.GetElementById(id);
            return fileEle.AbsoluteFilePath;
        }

        /// <summary>
        /// 获取相对路径
        /// </summary>
        public static string GetResourcePath(string id)
        {
            FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)ToHtmlSdsiteService.SdsiteXmlDocument.GetElementById(id);
            return fileEle.RelativeFilePath;
        }

        public static string GetResourceUrl(string id)
        {
            try
            {
                FileSimpleExXmlElement fileEle = (FileSimpleExXmlElement)ToHtmlSdsiteService.SdsiteXmlDocument.GetElementById(id);
                return fileEle.RelativeUrl;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
