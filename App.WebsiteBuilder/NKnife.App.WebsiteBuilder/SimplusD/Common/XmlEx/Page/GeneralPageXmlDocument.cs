using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// “ª∞„“≥√Ê°£
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
