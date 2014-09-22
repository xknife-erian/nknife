using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// "资源文件夹"对应的XmlElmeent。
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
        /// Image存到System下
        /// </summary>
        /// <param name="img"></param>
        /// <param name="fileName">直接的文件名,非全路径</param>
        /// <returns></returns>
        public string ImageSaveAsResources(Image img, string fileName)
        {
            fileName += ".jpg";

            ///将文件名翻译成拼音，生成一个不重名的文件名
            string pinyinFileName = Utility.File.FormatFileName(fileName, false);
            string formatFileName = Utility.File.BuildFileName(OwnerDocument.Resources.SystemFolder.AbsoluteFilePath, pinyinFileName, false, true);

            img.Save(Path.Combine(OwnerDocument.Resources.SystemFolder.AbsoluteFilePath, formatFileName));

            FileSimpleExXmlElement fileEle = OwnerDocument.CreateFileElementNoCreateFile(OwnerDocument.Resources.SystemFolder, formatFileName, fileName);
            return fileEle.Id;
        }
    }
}
