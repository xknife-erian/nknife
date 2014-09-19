using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// "��Դ�ļ���"��Ӧ��XmlElmeent��
    /// </summary>
    public class ResourcesXmlElement : FolderXmlElement
    {
        public ResourcesXmlElement(XmlDocument doc)
            :base("resources",doc)
        {
        }

        public FolderXmlElement SystemFolder
        {
            get
            {
                FolderXmlElement systemNode = this.SelectSingleNode("folder[@fileName='System']") as FolderXmlElement;
                if (systemNode == null)
                {
                    systemNode = OwnerDocument.CreateFolder(this, "System");
                }
                return systemNode;
            }
        }

        public override DataType DataType
        {
            get
            {
                return DataType.Resources;
            }
        }

        //private Dictionary<string, FileSimpleExXmlElement> _dicPathAndFiles = new Dictionary<string, FileSimpleExXmlElement>();

        /// <summary>
        /// Image�浽System��
        /// </summary>
        /// <param name="img"></param>
        /// <param name="fileName">ֱ�ӵ��ļ���,��ȫ·��</param>
        /// <returns></returns>
        public string ImageSaveAsResources(Image img, string fileName)
        {
            fileName += ".jpg";

            ///���ļ��������ƴ��������һ�����������ļ���
            string pinyinFileName = Utility.File.FormatFileName(fileName, false);
            string formatFileName = Utility.File.BuildFileName(OwnerDocument.Resources.SystemFolder.AbsoluteFilePath, pinyinFileName, false, true);

            img.Save(Path.Combine(OwnerDocument.Resources.SystemFolder.AbsoluteFilePath, formatFileName));

            FileSimpleExXmlElement fileEle = OwnerDocument.CreateFileElementNoCreateFile(OwnerDocument.Resources.SystemFolder, formatFileName, fileName);
            return fileEle.Id;
        }
    }
}
