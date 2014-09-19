using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    static public class DocumentProxyFactory
    {
        /// <summary>
        /// 通过传入document对象创建其代理对象。
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        static public DocumentProxy CreateProxy(object document)
        {
            DocumentProxy proxy = null;
            if (document is PageXmlDocument)
            {
                proxy = new PageDocumentProxy((PageXmlDocument)document);
            }
            return proxy;
        }
    }
}
