using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��ҳ��ҳ�档
    /// </summary>
    [PageCustom(true)]
    public class HomeXmlDocument : PageXmlDocument
    {
        public HomeXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }
    }
}
