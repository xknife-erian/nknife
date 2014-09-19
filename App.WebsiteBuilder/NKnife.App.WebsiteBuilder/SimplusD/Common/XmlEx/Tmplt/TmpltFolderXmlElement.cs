using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// "模板根文件夹"对应的XmlElmeent。
    /// </summary>
    public class TmpltFolderXmlElement : FolderXmlElement
    {
        public TmpltFolderXmlElement(XmlDocument doc)
            : base("tmpltRootFolder", doc)
        {
        }

        public override DataType DataType
        {
            get
            {
                return DataType.TmpltFolder;
            }
        }
    }
}
