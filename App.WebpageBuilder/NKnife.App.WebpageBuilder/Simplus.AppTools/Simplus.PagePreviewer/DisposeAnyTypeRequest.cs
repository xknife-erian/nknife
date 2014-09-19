using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusPagePreviewer
{
    class DisposeAnyTypeRequest
    {
        /// <summary>
        /// 封装shtml请求，如果是主页，找到其频道Guid，找到其主页，否则直接调用请求
        /// </summary>
        public static void AnyShtmlHeadBody(string index, Stream ns)
        {
            if (Path.GetFileName(index) != ConstService.INDEXSHTML)
            {
                ShtmlHeadBody(index, ns);
            }
            else
            {
                string channelStr = PathService.GetUrlPath(index); ;
                string channelGuid = PathService.GetChannelGuid(channelStr);
                if (!string.IsNullOrEmpty(channelGuid))
                {
                    string shtmlIndex = TmpltAndPageService.GetIndexPath(channelGuid);
                    if (!string.IsNullOrEmpty(shtmlIndex))
                    {
                        shtmlIndex = EncodingService.EncodingUTF8(shtmlIndex.Substring(1));
                        ShtmlHeadBody(shtmlIndex, ns);
                    }
                    else
                    {
                        WrongHeadBody(ns);
                    }
                }
                else
                {
                    WrongHeadBody(ns);
                }
            }
        }

        /// <summary>
        /// 处理shtml请求
        /// </summary>
        public static void ShtmlHeadBody(string index, Stream ns)
        {
            try
            {
                string pageId = PathService.GetPageGuid(index);
                if (!string.IsNullOrEmpty(pageId))
                {
                    string shtmlContent = TmpltAndPageService.GetPageContent(pageId);
                    //获取shtml中include所有内容
                    string strInclude = RegexService.GetShtmlInclude(shtmlContent);
                    //获取引号中所有内容
                    string quoMarkContent = StringService.GetQuotationMarkComment(strInclude);
                    HttpHeadInfo ht = new HttpHeadInfo();
                    string content = ConstService.PUBlISHDTD;
                    ht.Content_Type = "text/html";
                    string path = StringService.DeleteQuest(quoMarkContent);
                    string tmpltId = PathService.GetAnyUrlGuid(path);
                    content += TmpltAndPageService.GetTmpltContent(tmpltId);
                    content = RegexService.ChangeShtmlTag(content, quoMarkContent);
                    if (quoMarkContent.Contains("keywordList="))
                    {
                        content = ChangeKeyList(content, tmpltId, pageId);
                    }
                    //判断其页面是否含有正文
                    if (quoMarkContent.Contains("content="))
                    {
                        string contentId = RegexService.GetContentId(quoMarkContent);
                        //string contentId = PathService.GetContentId(contentPath);
                        //将正文型内容插到模板中
                        if (TmpltAndPageService.IsContentElement(contentId))
                        {
                            string strChange = TmpltAndPageService.GetContentPageContent(contentId);
                            //先运行将正文部分替换
                            content = RegexService.ChangeContent(content, strChange);
                        }
                        //再将其他页面片替换
                        content = PhpBody(content, tmpltId);
                    }
                    else
                    {
                        content = PhpBody(content, tmpltId);
                        content += ConstService.HTMLUTF8END;
                    }
                    int leng = Encoding.UTF8.GetByteCount(content);
                    ht.Content_Length = leng.ToString();

                    string shtmlHeadBody = ConstService.OK + ht.GetHttpHead() + content;
                    byte[] shtmlBytes = Encoding.UTF8.GetBytes(shtmlHeadBody);
                    ns.Write(shtmlBytes, 0, shtmlBytes.Length);
                    ns.Flush();
                    ns.Close();
                }
                else
                {
                    WrongHeadBody(ns);
                }
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
            }
        }

        public static string ChangeKeyList(string phpContent, string tmpltId, string pageId)
        {
            string snipList = TmpltAndPageService.GetKeywordListSnip(tmpltId, pageId);
            return RegexService.ChangeKeyList(phpContent, snipList);
        }

        /// <summary>
        /// 处理php请求
        /// </summary>
        public static void PhpHeadBody(string index, Stream ns)
        {
            string tmpltId = PathService.GetAnyUrlGuid(index);
            if (TmpltAndPageService.IsContentElement(tmpltId))
            {
                string phpContent = TmpltAndPageService.GetTmpltContent(tmpltId);
                //利用正则表达式替换php中的标题等
                phpContent = RegexService.ChangePhpTag(phpContent);
                string content = PhpBody(phpContent, tmpltId);
                if (!string.IsNullOrEmpty(content))
                {
                    HttpHeadInfo ht = new HttpHeadInfo();
                    content = ConstService.PUBlISHDTD + content;
                    int length = Encoding.UTF8.GetByteCount(content);
                    ht.Content_Length = length.ToString();
                    ht.Content_Type = "text/html";
                    string strMsgHead = ConstService.OK + ht.GetHttpHead() + content;
                    byte[] bytes = Encoding.UTF8.GetBytes(strMsgHead);
                    ns.Write(bytes, 0, bytes.Length);
                    ns.Flush();
                    ns.Close();
                }
                else
                {
                    WrongHeadBody(ns);

                }
            }
            else
            {
                WrongHeadBody(ns);

            }
        }

        /// <summary>
        /// 替换模板中的所有页面片
        /// </summary>
        private static string PhpBody(string phpString, string tmpltId)
        {
            List<string> listIncloude = RegexService.GetIncloudeList(phpString);
            foreach (string incloude in listIncloude)
            {

                string snipId = PathService.GetIncludeHtmlGuid(incloude);
                string replaceHtml = TmpltAndPageService.GetSnipContent(tmpltId, snipId);
                try
                {
                    phpString = phpString.Replace(incloude, replaceHtml);
                }
                catch (Exception e)
                {
                    ExceptionService.CreateException(e);
                }
            }
            return phpString;
        }

        /// <summary>
        /// 处理除shtml、php外的其他所有类型
        /// </summary>
        public static void OtherHeadBody(string index, string content_Type, Stream putoutStream)
        {
            try
            {
                string fileName = Path.GetFileName(index);
                string fileType = Path.GetExtension(index).Substring(1);
                //判断是否为CSS请求有责单独处理
                if (content_Type == HttpRequestType.Css.MimeType)
                {

                    CssRequestHeadBody(index, putoutStream);
                }
                else
                {
                    //判断是否为资源请求，有责找到资源路径（此时请求应均为资源）
                    if (index.Substring(0, 2).Contains("Re"))
                    {
                        index = PathService.GetResourcesFilePath(index);
                    }
                    else
                    {
                        WrongHeadBody(putoutStream);
                    }
                    //先进行UTF8解码，再进行Gb2312转吗
                    index = EncodingService.EncodingUTF8Change(index);
                    if (File.Exists(index))
                    {
                        LongFileHeadBody(index, putoutStream, content_Type);

                    }
                    else
                    {
                        index = EncodingService.EncodingGB2312Change(index);
                        if (File.Exists(index))
                        {
                            LongFileHeadBody(index, putoutStream, content_Type);

                        }
                        else
                        {
                            WrongHeadBody(putoutStream);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WrongHeadBody(putoutStream);
            }
        }

        /// <summary>
        /// 获取Channel下子文件的内容
        /// </summary>
        internal static void ChannelHeadBody(string index, Stream ns)
        {
            string channelGuid = PathService.GetChannelGuid(index);
            if (string.IsNullOrEmpty(channelGuid))
            {
                WrongHeadBody(ns);
            }
            else
            {
                HttpHeadInfo ht = new HttpHeadInfo();
                string shtml = TmpltAndPageService.GetChannelContent(channelGuid);
                ht.Content_Type = "text/html";
                byte[] bytes = Encoding.UTF8.GetBytes(shtml);
                ht.Content_Length = bytes.Length.ToString();
                shtml = ConstService.OK + ht.GetHttpHead() + shtml;
                bytes = Encoding.UTF8.GetBytes(shtml);
                //string respose = ConstService.Redirect +"Location:" + index + "index.shtml\r\n"+ ht.GetHttpHead();
                //byte[] bytes = Encoding.ASCII.GetBytes(respose);
                ns.Write(bytes, 0, bytes.Length);
                ns.Flush();
                ns.Close();
            }
        }

        private static void WrongHeadBody(Stream ns)
        {
            byte[] wrongBytes = Encoding.UTF8.GetBytes(ConstService.BAD);
            ns.Write(wrongBytes, 0, wrongBytes.Length);
            ns.Flush();
            ns.Close();
        }

        private static void LongFileHeadBody(string filePath, Stream putoutStream, string requestType)
        {
            FileInfo file = new FileInfo(filePath);
            HttpHeadInfo ht = new HttpHeadInfo();
            ht.Content_Length = file.Length.ToString();
            ht.Content_Type = requestType;
            string strMsgHead = ConstService.OK + ht.GetHttpHead();
            byte[] bytesMsgHead = Encoding.UTF8.GetBytes(strMsgHead);
            if (file.Length > ConstService.MaxCache)
            {
                putoutStream.Write(bytesMsgHead, 0, bytesMsgHead.Length);
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                byte[] buffer = new byte[1024 * 1024 * 10];
                long iOffset = 0;
                try
                {
                    while (iOffset < file.Length)
                    {
                        int m = fileStream.Read(buffer, 0, buffer.Length);
                        if (m <= 0)
                        {
                            break;
                        }
                        putoutStream.Write(buffer, 0, m);
                        iOffset += m;
                        Console.WriteLine("iOffset" + iOffset);
                    }
                }
                catch (Exception e)
                {
                    ExceptionService.CreateException(e);
                }
                fileStream.Close();

            }
            else
            {
                byte[] bytesMsgBody = File.ReadAllBytes(filePath);
                byte[] bytesMsgAll = new byte[bytesMsgHead.Length + bytesMsgBody.Length];
                Array.Copy(bytesMsgHead, 0, bytesMsgAll, 0, bytesMsgHead.Length);
                Array.Copy(bytesMsgBody, 0, bytesMsgAll, bytesMsgHead.Length, bytesMsgBody.Length);
                putoutStream.Write(bytesMsgAll, 0, bytesMsgAll.Length);
            }
            putoutStream.Flush();
            putoutStream.Close();
        }

        private static void CssRequestHeadBody(string index, Stream putoutStream)
        {
            byte[] bytesMsgBody = TmpltAndPageService.GetCssByte(index);
            HttpHeadInfo htx = new HttpHeadInfo();
            htx.Content_Length = bytesMsgBody.Length.ToString();
            htx.Content_Type = HttpRequestType.Css.MimeType;
            string strMsgHeadx = ConstService.OK + htx.GetHttpHead();
            byte[] bytesMsgHeadx = Encoding.UTF8.GetBytes(strMsgHeadx);
            byte[] bytesMsgAll = new byte[bytesMsgHeadx.Length + bytesMsgBody.Length];
            Array.Copy(bytesMsgHeadx, 0, bytesMsgAll, 0, bytesMsgHeadx.Length);
            Array.Copy(bytesMsgBody, 0, bytesMsgAll, bytesMsgHeadx.Length, bytesMsgBody.Length);
            try
            {
                putoutStream.Write(bytesMsgAll, 0, bytesMsgAll.Length);
                putoutStream.Flush();
            }
            catch (Exception e)
            {
                ExceptionService.CreateException(e);
            }
            putoutStream.Close();
        }
    }
}