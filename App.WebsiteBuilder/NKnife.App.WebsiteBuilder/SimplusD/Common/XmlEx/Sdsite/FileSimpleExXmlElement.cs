using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 资源文件里的每一个文件对应的XmlElement
    /// </summary>
    public class FileSimpleExXmlElement : SimpleExIndexXmlElement
    {
        public FileSimpleExXmlElement(XmlDocument doc)
            :base("file",doc)
        {
        }

        public override string RelativeFilePath
        {
            get
            {
                if (OwnerFolderElement == null)
                {
                    return null;
                }
                return OwnerFolderElement.RelativeFilePath + FileName;
            }
        }

        public override string RelativeUrl
        {
            get
            {
                if (OwnerFolderElement == null)
                {
                    return null;
                }
                return OwnerFolderElement.RelativeUrl + FileName;
            }
        }

        public override DataType DataType
        {
            get { return DataType.File; }
        }

        public override string OldRelativeFilePath
        {
            get
            {
                ///获得原父节点
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///获得原文件名
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///通过原父节点和原文件名即可得到原路径
                return oldOwnerFolder.OldRelativeFilePath + oldFileName;
            }
        }

        public override string OldRelativeUrl
        {
            get
            {
                ///获得原父节点
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///获得原文件名
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///通过原父节点和原文件名即可得到原路径
                return oldOwnerFolder.OldRelativeUrl + oldFileName;
            }
        }
    }
}
