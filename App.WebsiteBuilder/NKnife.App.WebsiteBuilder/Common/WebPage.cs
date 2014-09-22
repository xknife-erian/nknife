using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public class WebPage
    {
        /// <summary>
        /// header内容:etag
        /// </summary>
        public string HeaderEtag { get; set; }
        /// <summary>
        /// header内容:content_length
        /// </summary>
        public string HeaderContentLength { get; set; }
        /// <summary>
        /// header内容:last_modified
        /// </summary>
        public string HeaderLastModified { get; set; }
        /// <summary>
        /// 最后一次失败请求状态
        /// </summary>
        public int LastBadStatusCode
        {
            get { return this._LastBadStatusCode; }
            set { this._LastBadStatusCode = value; }
        }
        private int _LastBadStatusCode = -1;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 网页源码
        /// </summary>
        public string HtmlCode { get; set; }

        public string Url { get; set; }
    }
}
