using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// Document的代理类。抽象类。
    /// </summary>
    public abstract class DocumentProxy
    {
        /// <summary>
        /// 获得当前代理对应的Document对象
        /// </summary>
        public abstract object GetDocument();
        protected string _id;
        protected SdsiteXmlDocument _sdsiteDocument;
    }
}
