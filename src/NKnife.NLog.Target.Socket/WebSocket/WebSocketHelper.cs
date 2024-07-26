using System.Security.Cryptography;
using System.Text;

namespace NKnife.NLog.Target.Socket.WebSocket
{
    /// <summary>
    ///     WebSocket帮助类
    /// </summary>
    internal static class WebSocketHelper
    {
        private static string MagicKey => Guid.NewGuid().ToString("N").ToUpper();

        /// <summary>
        ///     协议处理-http协议握手
        /// </summary>
        public static byte[] HandshakeMessage(string data)
        {
            var key  = string.Empty;
            var info = data;
            //一步一步来
            var list = info.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in list.Reverse())
            {
                if(item.IndexOf("Sec-WebSocket-Key", StringComparison.Ordinal) > -1)
                {
                    key = item.Split(new[] { ": " }, StringSplitOptions.None)[1];
                    break;
                }
            }

            //获取标准的key
            key = GetResponseKey(key);
            //拼装返回的协议内容
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + "\r\n");
            responseBuilder.Append("Upgrade: websocket" + "\r\n");
            responseBuilder.Append("Connection: Upgrade" + "\r\n");
            responseBuilder.Append("Sec-WebSocket-Accept: " + key + "\r\n\r\n");

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }

        /// <summary>
        ///     获取返回验证的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetResponseKey(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            key += MagicKey;
            key =  Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key)));

            return key;
        }
    }
}