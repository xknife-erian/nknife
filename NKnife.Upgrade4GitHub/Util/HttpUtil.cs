using System;
using System.IO;
using System.Net;
using System.Text;

namespace NKnife.Upgrade4Github.Util
{
    static class HttpUtil
    {
        /// <summary>
        /// 在本项目只有这样一个GitHub访问，为了更轻量级，没有使用通常的RestSharp库，而是进行了简单的封装。
        /// </summary>
        /// <param name="baseUrl">详细的GitHub路径</param>
        public static string GetRestResult(string baseUrl)
        {
            //定义安全传输协议（TLS1.2=3702, TLS1.1=765, TLS1.0=192, SSL3=48）
            ServicePointManager.SecurityProtocol = (SecurityProtocolType) 3072; //TLS1.2=3702

            string result = "";
            if (WebRequest.Create(baseUrl) is HttpWebRequest req)
            {
                req.Method = "get";
                req.ContentType = @"application/octet-stream";
                req.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";
                try
                {
                    var res = (HttpWebResponse) req.GetResponse();
                    var inputStream = res.GetResponseStream();
                    var sr = new StreamReader(inputStream ?? throw new InvalidOperationException(), Encoding.UTF8);
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }

            return result;
        }
    }
}
