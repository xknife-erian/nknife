using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// һ��ҳ�档
    /// </summary>
    [PageCustom(true)]
    public class GeneralPageXmlDocument : PageXmlDocument
    {
        public GeneralPageXmlDocument(string relativeFilePath, SimpleExIndexXmlElement sdsiteElement)
            : base(relativeFilePath, sdsiteElement)
        {
        }
    }
   
}
