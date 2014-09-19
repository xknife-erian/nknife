using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// 表示一个文件夹(会在资源文件、模板、页面下出现)
    /// </summary>
    public class FolderXmlElement : SimpleExIndexXmlElement
    {
        public FolderXmlElement(XmlDocument doc)
            :base("folder",doc)
        {
        }

        protected FolderXmlElement(string localName,XmlDocument doc)
            :base(localName,doc)
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
                return OwnerFolderElement.RelativeFilePath + FileName + @"\";
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
                return OwnerFolderElement.RelativeUrl + FileName + @"/";
            }
        }

        public override DataType DataType
        {
            get { return DataType.Folder; }
        }


        public string DefaultPageId
        {
            get
            {
               return this.GetAttribute("defaultPageId").ToString();
            }
            set
            {
                this.SetAttribute("defaultPageId", value);
            }
        }

        /// <summary>
        /// 是否拥有有效的DefaultPage
        /// </summary>
        public bool HasEffectiveDefaultPage
        {
            get
            {
                return !string.IsNullOrEmpty(DefaultPageId);
            }
        }

        public FolderType FolderType
        {
            get
            {
                string strType = this.GetAttribute("folderType");
                if (string.IsNullOrEmpty(strType))
                    return FolderType.None;
                else
                    return (FolderType)Enum.Parse(typeof(FolderType), strType);
            }
            set
            {
                this.SetAttribute("folderType", value.ToString());
            }
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
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + @"\";
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
                return oldOwnerFolder.OldRelativeUrl + oldFileName + @"/";
            }
        }
    }
}
