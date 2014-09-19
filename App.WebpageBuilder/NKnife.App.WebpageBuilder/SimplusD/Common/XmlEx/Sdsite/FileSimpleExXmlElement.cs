using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD
{
    /// <summary>
    /// ��Դ�ļ����ÿһ���ļ���Ӧ��XmlElement
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
                return oldOwnerFolder.OldRelativeFilePath + oldFileName;
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
                return oldOwnerFolder.OldRelativeUrl + oldFileName;
            }
        }
    }
}
