using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Gean.Net.Webs
{
    public class UtilityWebs
    {
        #region 单例

        private static UtilityWebs _Instance;
        private static readonly object _LockObj = new object();

        private UtilityWebs()
        {
        }

        public static UtilityWebs Instance()
        {
            lock (_LockObj)
            {
                if (_Instance == null) _Instance = new UtilityWebs();
            }
            return _Instance;
        }

        #endregion

        private WebClientEx _WebClient;

        /// <summary>通过WebClient的扩展类型(增加随机的UserArgent，Cookie容器)进Post操作
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="endcoding"></param>
        /// <param name="postVars">The post vars.</param>
        /// <returns></returns>
        public string WebPost(string url, Encoding endcoding, NameValueCollection postVars)
        {
            return WebPost(url, endcoding, 4000, postVars);
        }

        /// <summary>通过WebClient的扩展类型(增加随机的UserArgent，Cookie容器)进Post操作
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="encoding"></param>
        /// <param name="timeout">超时时长</param>
        /// <param name="postVars">The post vars.</param>
        /// <returns></returns>
        public string WebPost(string url, Encoding encoding, int timeout, NameValueCollection postVars)
        {
            _WebClient = new WebClientEx();
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
    }
}