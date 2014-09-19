using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.IO;
using System.Net;
using System.Threading;
using System.IO.Compression;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Web
        {

            /// <summary>
            /// 攫取一个网页
            /// </summary>
            /// <param name="url">网页地址</param>
            /// <param name="timeoutMaxCount">最大尝试次数</param>
            /// <param name="connectSleepTime">每次尝试等待时间(毫秒)</param>
            public static WebPage GetHtmlCodeFromUrl(string url, int timeoutMaxCount, int connectSleepTime)
            {
                HttpWebRequest request = null;

                for (int i = 0; i < timeoutMaxCount; i++)
                {
                    try
                    {
                        request = WebRequest.Create(url) as HttpWebRequest;
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(connectSleepTime);
                    }
                }
                if (request == null)
                {
                    return null;
                }
                WebPage wp = new WebPage();
                HttpWebResponse response = null;
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch(WebException e)
                {
                    wp.LastBadStatusCode = (int)e.Status;
                }
                if (wp.LastBadStatusCode == -1)/// 当接收网页代码未发生任何错误时
                {
                    MemoryStream memStream = GetHttpMemStream(response);
                    StreamReader reader = null;
                    Encoding encoding = GetEncoding(response);
                    memStream.Seek(0, SeekOrigin.Begin);

                    #region 核心部份，获得网页代码的StreamReader
                    if (encoding != null)
                    {
                        reader = new StreamReader(memStream, encoding);
                    }
                    else/// 如果从头信息中始终未能获取Encoding信息，只能从源码中获取
                    {
                        reader = new StreamReader(memStream, Encoding.ASCII);/// 用ASCII码方式读取流，以获取英文的charset字符串
                        string code = reader.ReadToEnd();
                        string charset = GetCharsetString(code);

                        if (string.IsNullOrEmpty(charset))
                        {
                            encoding = Encoding.Default;
                        }
                        else
                        {
                            try
                            {
                                encoding = Encoding.GetEncoding(charset);
                            }
                            catch//如果编码不存在，使用默认编码  
                            {
                                encoding = Encoding.Default;
                            }
                        }

                        memStream.Seek(0, SeekOrigin.Begin);
                        reader = new StreamReader(memStream, encoding);/// 再次根据编码重新读流
                    }
                    #endregion

                    wp.Encoding = encoding;
                    wp.HtmlCode = reader.ReadToEnd();

                    memStream.Close();
                    memStream.Dispose();
                    reader.Close();
                    reader.Dispose();
                }
                return wp;
            }

            /// <summary>
            /// 从网页代码中获取编码(charset)字符串
            /// </summary>
            private static string GetCharsetString(string code)
            {
                string charset = string.Empty;
                int begin = code.IndexOf("charset=");
                int end = -1;
                if (begin != -1)
                {
                    end = code.IndexOf("\"", begin);
                    if (end != -1)
                    {
                        int start = begin + 8;
                        charset = code.Substring(start, end - start + 1);
                        charset = charset.TrimEnd(new Char[] { '>', '"', '\'' });
                    }
                }
                return charset;
            }

            /// <summary>
            /// 从HttpWebResponse中获取System.IO.Stream,如用Gzip压缩，将其解压
            /// </summary>
            private static MemoryStream GetHttpMemStream(HttpWebResponse response)
            {
                MemoryStream memStream = new MemoryStream();     
                byte[] buffer = new byte[1024];
                System.IO.Stream stream = response.GetResponseStream();
                if (response.ContentEncoding.ToLower().Equals("gzip"))
                {
                    /// 如果页面采用了GZip进行压缩，调用.net的GZip类进行解压
                    stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                }
                else
                {
                    stream = response.GetResponseStream();
                }
                int i = stream.Read(buffer, 0, buffer.Length);
                while (i > 0)
                {
                    memStream.Write(buffer, 0, i);
                    i = stream.Read(buffer, 0, buffer.Length);
                }
                return memStream;
            }

            /// <summary>
            /// 从HttpWebResponse中尽最大的可能获取Encoding信息
            /// </summary>
            private static Encoding GetEncoding(HttpWebResponse response)
            {
                if (!string.IsNullOrEmpty(response.ContentEncoding))
                {
                    try
                    {
                        return Encoding.GetEncoding(response.ContentEncoding);
                    }
                    catch
                    {
                        return null;
                    }
                }

                /// 一种很特殊的情况 http://www.mom-baby.com.cn
                if (response.CharacterSet.ToLower() == "utf-8")
                {
                    return Encoding.UTF8;
                }

                /// 多数时候ContentEncoding都是空，
                /// 这时候我们通过从content-type头中去寻找编码信息，
                /// 比如163解析出来就是GBK
                string ctype = response.Headers["content-type"];
                if (ctype != null)
                {
                    int begin = ctype.IndexOf("charset=");
                    int end = -1;
                    if (begin != -1)
                    {
                        end = ctype.IndexOf("\"", begin);
                        if (end != -1)
                        {
                            int start = begin + 8;

                            string charset = string.Empty;
                            charset = ctype.Substring(start, end - start + 1);
                            charset = charset.TrimEnd(new Char[] { '>', '"' });
                            try
                            {
                                return Encoding.GetEncoding(charset);
                            }
                            catch
                            {
                                return null;
                            }
                        }
                    }
                }

                /// 什么信息也没有找到，只好返回null喽
                return null;
            }


#if DEBUG
            /// <summary>
            /// 抄来的代码，待验证
            /// </summary>
            static string GetPageContent(string url, string postdata, string encode, string method, CookieContainer cc)
            {
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
                objRequest.KeepAlive = true;
                objRequest.AllowAutoRedirect = true;
                objRequest.UserAgent = "Mozilla/4.0+(compatible;+MSIE+6.0;+Windows+NT+5.2;+SV1;+.NET+CLR+1.1.4322;+.NET+CLR+2.0.50727;+Alexa+Toolbar)";
                objRequest.Method = method;
                objRequest.ProtocolVersion = System.Net.HttpVersion.Version11;
                objRequest.CookieContainer = cc;
                objRequest.Referer = url;

                if (method.ToLower() == "post")
                {
                    objRequest.ContentType = "application/x-www-form-urlencoded";
                    byte[] bytes = Encoding.GetEncoding(encode).GetBytes(postdata);
                    objRequest.ContentLength = bytes.Length;
                    System.IO.Stream outStream = objRequest.GetRequestStream();
                    outStream.Write(bytes, 0, bytes.Length);
                    outStream.Close();
                }

                try
                {
                    HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                    System.IO.Stream objStream = objResponse.GetResponseStream();
                    StreamReader sr = new StreamReader(objStream, System.Text.Encoding.GetEncoding(encode));
                    return sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
#endif
        }
    }
}