using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��ʾһ���ļ���(������Դ�ļ���ģ�塢ҳ���³���)
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
        /// �Ƿ�ӵ����Ч��DefaultPage
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
                ///���ԭ���ڵ�
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///���ԭ�ļ���
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///ͨ��ԭ���ڵ��ԭ�ļ������ɵõ�ԭ·��
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + @"\";
            }
        }

        public override string OldRelativeUrl
        {
            get
            {
                ///���ԭ���ڵ�
                FolderXmlElement oldOwnerFolder = OwnerFolderElement;
                if (!string.IsNullOrEmpty(OldParentFolderId))
                {
                    oldOwnerFolder = OwnerDocument.GetFolderElementById(OldParentFolderId);
                }

                if (oldOwnerFolder == null)
                {
                    return null;
                }

                ///���ԭ�ļ���
                string oldFileName = FileName;
                if (!string.IsNullOrEmpty(OldFileName))
                {
                    oldFileName = OldFileName;
                }

                ///ͨ��ԭ���ڵ��ԭ�ļ������ɵõ�ԭ·��
                return oldOwnerFolder.OldRelativeUrl + oldFileName + @"/";
            }
        }
    }
}
