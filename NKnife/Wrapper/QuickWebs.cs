using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using NKnife.ShareResources;

namespace NKnife.Wrapper
{
    /// <summary>
    ///     面向Java的Servlet的Web请求操作
    /// </summary>
    public class QuickWebs
    {
        private JWebClient _WebClient;

        /// <summary>
        ///     上传文件
        /// </summary>
        /// <param name="file">指定的文件</param>
        /// <param name="servlet">指定的Servlet地址</param>
        public static void Upload(FileInfo file, string servlet)
        {
            var webrequest = (HttpWebRequest) WebRequest.Create(servlet);
            webrequest.Method = "POST";
            var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            webrequest.ContentLength = fileStream.Length;
            Stream requestStream = webrequest.GetRequestStream();

            var buffer = new Byte[(int) fileStream.Length];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }
            requestStream.Close();
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
            _WebClient = new JWebClient();
            _WebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            _WebClient.Timeout = timeout;

            byte[] data = Encoding.UTF8.GetBytes(ConvertNameValueToString(postVars));
            byte[] replay = _WebClient.UploadData(url, "POST", data);

            return encoding.GetString(replay);
        }

        private string ConvertNameValueToString(NameValueCollection src)
        {
            var result = new StringBuilder();
            foreach (string key in src.AllKeys)
            {
                result.Append(key);
                result.Append("=");
                result.Append(HttpUtility.UrlEncode(src[key], Encoding.GetEncoding("GBK")));
                result.Append("&");
            }
            string test = result.ToString().TrimEnd('&');
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
                WebRequest request = base.GetWebRequest(address);
                RefreshUserAgent();

                if (request != null)
                {
                    if (request.GetType() == typeof (HttpWebRequest))
                    {
                        ((HttpWebRequest) request).CookieContainer = CookieContainer;
                        ((HttpWebRequest) request).UserAgent = UserAgent;
                        (request).Timeout = Timeout;
                    }
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