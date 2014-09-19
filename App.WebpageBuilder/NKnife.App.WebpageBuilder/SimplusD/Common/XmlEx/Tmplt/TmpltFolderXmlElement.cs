using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// "ģ����ļ���"��Ӧ��XmlElmeent��
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
