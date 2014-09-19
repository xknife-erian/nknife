using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.SimplusD;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusPagePreviewer
{
    static public class TmpltAndPageService
    {
        /// <summary>
        /// 通过pageID获取shtml文件的那句话
        /// </summary>
        static public string GetPageContent(string pageId)
        {
            try
            {
                PageSimpleExXmlElement pageEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetPageElementById(pageId);
                return "";// pageEle.ToHtml();
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断SDsite上是否存在该节点，避免发生错误请求
        /// </summary>
        static public bool IsContentElement(string EleId)
        {
            AnyXmlElement xmlEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetElementById(EleId);
            if (xmlEle == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 通过模板ID获得模板的TOHTML，并将该TmpltDoc实例加入到字典中，为Tocss做准备
        /// </summary>
        static public string GetTmpltContent(string tmpltId)
        {
            try
            {
                TmpltXmlDocument tmpltDoc = ToHtmlSdsiteService.SdsiteXmlDocument.GetTmpltDocumentById(tmpltId);
                ActiveTmpltAndSnip.AddTmpltDocIntoDictionary(tmpltDoc);

                return tmpltDoc.ToHtml();
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return string.Empty;
            }
        }

        /// <summary>
        /// 生成正文页面片内容
        /// </summary>
        static public string GetContentPageContent(string contentId)
        {
            try
            {
                PageSimpleExXmlElement pageEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetPageElementById(contentId);
                TmpltXmlDocument tmpltDoc = ToHtmlSdsiteService.SdsiteXmlDocument.GetTmpltDocumentById(pageEle.TmpltId);
                SnipXmlElement snipEle = tmpltDoc.GetContentSnipEle();
                string contentHtml = null;
                if (snipEle != null)
                {
                    // lukan,　2008-6-23 10:38:45，　重构
                    //contentHtml = snipEle.ToRealContentHtml(contentId);
                    //if (snipEle.IsHaveContent == false && tmpltDoc.TmpltType == TmpltType.General)
                    //{
                    //    contentHtml += "<span>您在正文页面片中还没有添加正文,正文部分将无法显示</span>";
                    //}
                    //if (!string.IsNullOrEmpty(snipEle.ToRealCss()))
                    //{
                    //    ActiveTmpltAndSnip.AddSnipElementIntoDictionary(snipEle);
                    //}
                }
                else
                {
                    return "您还没有添加正文页面片！";
                }
                return contentHtml;
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return string.Empty;
            }

        }

        /// <summary>
        /// 通过模板及snipID获得页面片的HTml，并将该SnipEle实例加入到字典中，为Tocss做准备
        /// </summary>
        static public string GetSnipContent(string tmpltId, string snipId)
        {
            try
            {
                TmpltXmlDocument tmpltDoc = ToHtmlSdsiteService.SdsiteXmlDocument.GetTmpltDocumentById(tmpltId);
                SnipXmlElement snipEle = tmpltDoc.GetSnipElementById(snipId);
                string snipHtml = "";// lukan,　2008-6-23 10:38:45，　重构             snipEle.ToRealHtml();
                //if (!string.IsNullOrEmpty(snipEle.ToRealCss()))
                //{
                //    ActiveTmpltAndSnip.AddSnipElementIntoDictionary(snipEle);
                //}
                return snipHtml;
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断该CSS为页面片，或模板CSS，找到字典中存储的实例，进行Tocss
        /// </summary>
        internal static byte[] GetCssByte(string index)
        {
            string ElementId = PathService.GetCSSGuid(index);
            string strCss = null;
            if (ActiveTmpltAndSnip.ActiveTmpltDictionary.ContainsKey(ElementId))
            {
                strCss = "";// ActiveTmpltAndSnip.ActiveTmpltDictionary[ElementId].ToCss();
                ActiveTmpltAndSnip.DeleteTmpltDocmentDictionary(ElementId);
                return Encoding.UTF8.GetBytes(strCss);
            }
            else if (ActiveTmpltAndSnip.ActiveSnipDictionary.ContainsKey(ElementId))
            {
                strCss = "";//ActiveTmpltAndSnip.ActiveSnipDictionary[ElementId].ToRealCss();
                ActiveTmpltAndSnip.DeleteSnipElementDictionary(ElementId);
                return Encoding.UTF8.GetBytes(strCss);

            }
            else
            {
                return Encoding.UTF8.GetBytes(ConstService.BAD);
            }
        }

        /// <summary>
        /// 当发送频道请求情况下，产生该频道下的列表（文件夹，频道，页面）
        /// </summary>
        internal static string GetChannelContent(string channelGuid)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<body></body>");
                FolderXmlElement channeEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetFolderElementById(channelGuid);
                if (string.IsNullOrEmpty(channeEle.DefaultPageId))
                {
                    foreach (AnyXmlElement anyEle in channeEle.ChildNodes)
                    {
                        if (anyEle.Name != "siteShowItem")
                        {
                            SimpleExIndexXmlElement pageEle = anyEle as SimpleExIndexXmlElement;
                            if (pageEle.Name != "resources" && pageEle.Name != "tmpltRootFolder" && pageEle.IsDeleted == false)
                            {
                                //如果包含主页，则跳转到主页
                                XmlElement xmlEle = xmlDoc.CreateElement("div");
                                XmlElement linkEle = xmlDoc.CreateElement("a");
                                if (pageEle.Name == "channel" || pageEle.Name == "folder")
                                {
                                    linkEle.SetAttribute("href", pageEle.OwnerFolderElement.RelativeUrl);
                                }
                                else
                                {
                                    linkEle.SetAttribute("href", pageEle.RelativeUrl);
                                }
                                linkEle.InnerXml += pageEle.FileName;
                                xmlEle.AppendChild(linkEle);
                                xmlDoc.DocumentElement.AppendChild(xmlEle);
                                return string.Format(ConstService.HTMLUTF8Head, channeEle.Title) + xmlDoc.InnerXml + ConstService.HTMLUTF8END;
                            }
                        }
                    }
                }
                else
                {
                    return "<html><head>" + ConstService.UTF8 + "<meta http-equiv=\"refresh\" content=\"0;URL=http://127.0.0.1:" + Perviewer.Port + channeEle.RelativeUrl + "index.shtml\"></head></html>";
                }

            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
                return ConstService.BAD;
            }
            return "该频道（文件夹）下无内容显示！";

        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetKeywordListSnip(string tmpltId, string pageId)
        {
            PageSimpleExXmlElement pageEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetPageElementById(pageId);
            TmpltXmlDocument tmpltDoc = ToHtmlSdsiteService.SdsiteXmlDocument.GetTmpltDocumentById(tmpltId);
            SnipXmlElement snipXmlEle = tmpltDoc.GetKeyListSnip();
            return "";// snipXmlEle.ToKeywordHtml(pageEle);
        }

        public static string GetIndexPath(string channleGuid)
        {
            FolderXmlElement channleEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetFolderElementById(channleGuid);
            PageSimpleExXmlElement pageEle = ToHtmlSdsiteService.SdsiteXmlDocument.GetPageElementById(channleEle.DefaultPageId);
            if (pageEle != null)
            {
                return pageEle.OwnerFolderElement.RelativeUrl + pageEle.FileName + Utility.Const.ShtmlFileExt;
            }
            return string.Empty;
        }
    }
}