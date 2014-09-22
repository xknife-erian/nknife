using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// Ê×Ò³ÐÍÒ³Ãæ¡£
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
