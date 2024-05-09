﻿using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using NKnife.ShareResources;

namespace NKnife
{
    /// <summary>
    ///     面向Java的Servlet的Web请求操作
    /// </summary>
    public class JWebs
    {
        private const string Boundary = "~~~##^##~~~";
        private const string ContentType = "multipart/form-data; boundary=" + Boundary;

        private const string FilePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                                              "Content-Type: application/octet-stream\r\n\r\n";

        private const string BeginBoundary = "--" + Boundary + "\r\n";
        private const string EndBoundary = "\r\n--" + Boundary + "--\r\n";

        private JWebClient _webClient;

        /// <summary>
        ///     上传文件
        /// </summary>
        /// <param name="file">指定的文件</param>
        /// <param name="servlet">指定的Servlet地址</param>
        /// <param name="timeout">超时，默认30秒</param>
        public string UploadFile(FileInfo file, string servlet, int timeout = 1000 * 30)
        {
            var responseContent = string.Empty;

            var rootRequest = (HttpWebRequest) WebRequest.Create(servlet);
            rootRequest.Method = "POST";
            rootRequest.Timeout = timeout;
            rootRequest.ContentType = ContentType;

            var beginBoundary = Encoding.ASCII.GetBytes(BeginBoundary);
            var endBoundary = Encoding.ASCII.GetBytes(EndBoundary);

            var fileSimpleName = file.Name.Substring(0, file.Name.LastIndexOf('.'));
            var header = string.Format(FilePartHeader, fileSimpleName, file.Name);

            using (var memStream = new MemoryStream())
            {
                // 根据W3C的相关协议(http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4)进行组包，以下是文件块，
                // 用临时的内存流先进行临时组包
                // 1.写入文件块开始时的边界符
                memStream.Write(beginBoundary, 0, beginBoundary.Length);

                // 2.写入文件块的文件头（即文件描述信息）
                var headerbytes = Encoding.ASCII.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                // 3.读取实际的磁盘中的文件并写入
                var buffer = new byte[512];
                using (var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    var bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0) memStream.Write(buffer, 0, bytesRead);
                    fileStream.Close();
                }

                // 4.写入文件块最后的结束边界符  
                memStream.Write(endBoundary, 0, endBoundary.Length);

                // 设置发送信息的长度
                rootRequest.ContentLength = memStream.Length;

                // 将已组成的协议写入请求中
                var requestStream = rootRequest.GetRequestStream();
                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();

                memStream.Close();
            }

            // 获取回复
            using (var httpWebResponse = (HttpWebResponse) rootRequest.GetResponse())
            {
                var replayStream = httpWebResponse.GetResponseStream();
                if (replayStream != null)
                    using (var reader = new StreamReader(replayStream, Encoding.UTF8))
                    {
                        responseContent = reader.ReadToEnd();
                    }

                httpWebResponse.Close();
            }

            rootRequest.Abort();

            return responseContent;
        }

        /// <summary>
        ///     通过WebClient的扩展类型(增加随机的UserArgent，Cookie容器)进Post操作
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="endcoding"></param>
        /// <param name="postVars">The post vars.</param>
        /// <returns></returns>
        public string WebPost(string url, Encoding endcoding, NameValueCollection postVars)
        {
            return WebPost(url, endcoding, 4000, postVars);
        }

        /// <summary>
        ///     通过WebClient的扩展类型(增加随机的UserArgent，Cookie容器)进Post操作
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="encoding"></param>
        /// <param name="timeout">超时时长</param>
        /// <param name="postVars">The post vars.</param>
        /// <returns></returns>
        public string WebPost(string url, Encoding encoding, int timeout, NameValueCollection postVars)
        {
            _webClient = new JWebClient();
            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            _webClient.Timeout = timeout;

            var data = Encoding.UTF8.GetBytes(ConvertNameValueToString(postVars));
            var replay = _webClient.UploadData(url, "POST", data);


            return encoding.GetString(replay);
        }

        private string ConvertNameValueToString(NameValueCollection src)
        {
            var result = new StringBuilder();
            foreach (var key in src.AllKeys)
            {
                result.Append(key);
                result.Append("=");
                result.Append(HttpUtility.UrlEncode(src[key], Encoding.GetEncoding("GBK")));
                result.Append("&");
            }

            var test = result.ToString().TrimEnd('&');
            return test;
        }

        internal class JWebClient : WebClient
        {
            private static readonly string[] _MultiUserAgents = GeneralString.HttpUserAgents.Split('~');
            private static readonly Random _Random = new Random();

            public JWebClient()
            {
                Timeout = 800;
                UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727)";
                CookieContainer = new CookieContainer();
            }

            public CookieContainer CookieContainer { get; set; }

            public string UserAgent { get; set; }

            public int Timeout { get; set; }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                RefreshUserAgent();

                if (request != null)
                    if (request.GetType() == typeof(HttpWebRequest))
                    {
                        ((HttpWebRequest) request).CookieContainer = CookieContainer;
                        ((HttpWebRequest) request).UserAgent = UserAgent;
                        request.Timeout = Timeout;
                    }

                return request;
            }

            private void RefreshUserAgent()
            {
                UserAgent = _MultiUserAgents[_Random.Next(0, _MultiUserAgents.Length)];
            }
        }
    }
}