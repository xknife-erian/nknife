using System.IO;
using System.Text;

namespace Jeelu.SimplusPagePreviewer
{
    public static class EncodingService
    {
        /// <summary>
        /// 该方法将浏览器发送的url按UTF-8，进行解码
        ///</summary>
        public static string EncodingUTF8Change(string index)
        {
            string utf8Index = System.Web.HttpUtility.UrlDecode(index, Encoding.UTF8);
            return utf8Index;
        }

        /// <summary>
        /// 该方法将浏览器发送的url按gb2312，进行解码
        /// </summary>
        public static string EncodingGB2312Change(string index)
        {
            string gb2312Index = System.Web.HttpUtility.UrlDecode(index, Encoding.GetEncoding("gb2312"));
            return gb2312Index;
        }

        public static string EncodingUTF8(string index)
        {
            string utf8Str = System.Web.HttpUtility.UrlEncode(index, Encoding.UTF8);
            return utf8Str;
        }

    }
}
