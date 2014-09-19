using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.SimplusD
{
    public class TmpltSimpleExXmlElement : SimpleExIndexXmlElement, IGetDocumentable
    {
        internal protected TmpltSimpleExXmlElement(XmlDocument doc)
            : base("tmplt", doc)
        {
        }

        TmpltXmlDocument _indexXmlDocument = null;
        /// <summary>
        /// ��ȡ�ڵ��Ӧ���ļ�����
        /// </summary>
        /// <returns></returns>
        public TmpltXmlDocument GetIndexXmlDocument()
        {
            if (_indexXmlDocument == null)
            {
                _indexXmlDocument = TmpltXmlDocument.CreateInstance(this.RelativeFilePath, this.Id, this);
                _indexXmlDocument.Load();
            }
            return _indexXmlDocument;
        }

        #region IGetDocumentable ��Ա

        IndexXmlDocument IGetDocumentable.GetIndexXmlDocument()
        {
            return GetIndexXmlDocument();
        }

        #endregion

        public TmpltType TmpltType
        {
            get
            {
                string strType = this.GetAttribute("type");
                return (TmpltType)Enum.Parse(typeof(TmpltType), strType);
            }
            set
            {
                SetAttribute("type", value.ToString());
            }
        }

        public bool HasContentSnip
        {
            get { return Utility.Convert.StringToBool(GetAttribute("hasContent")); }
            set
            {
                SetAttribute("hasContent", value.ToString());
            }
        }

        public override string RelativeFilePath
        {
            get
            {
                if (OwnerFolderElement == null)
                {
                    return null;
                }
                return OwnerFolderElement.RelativeFilePath + FileName + Utility.Const.TmpltFileExt;
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
                return OwnerFolderElement.RelativeUrl + Id + Utility.Const.ShtmlFileExt; //+ UrlTitle;   //todo:
            }
        }

        public override DataType DataType
        {
            get { return DataType.Tmplt; }
        }

        public override string FileExtension
        {
            get
            {
                return Utility.Const.TmpltFileExt;
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
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + Utility.Const.TmpltFileExt;
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

                ///ͨ��ԭ���ڵ��ԭ�ļ������ɵõ�ԭ·��
                return oldOwnerFolder.OldRelativeUrl;
            }
        }

        #region CustomeId��ص�

        /// <summary>
        /// ����key�洢CustomId�ļ���
        /// </summary>
        Dictionary<string, SimpleExIndexXmlElement> _dicCustomIds = new Dictionary<string, SimpleExIndexXmlElement>();

        /// <summary>
        /// �������ظ���CustomId
        /// </summary>
        public string CreateCustomId(IdType type)
        {
            string id = null;
            string profix = null;
            switch (type)
            {
                case IdType.Snip:
                    profix = "snip";
                    break;

                case IdType.Part:
                    profix = "part";
                    break;

                default:
                    Debug.Fail("δ�����IdType:" + type);
                    break;
            }

            for (long i = 1; i < long.MaxValue; i++)
            {
                ///��ʱ��ɵ�CustomId
                string tempCustomId = profix + i;

                ///����CustomId�Ƿ��Ѿ�����
                if (!ExistsCustomId(tempCustomId))
                {
                    ///�����ڴ�id����ʹ�ô�id
                    id = tempCustomId;
                    break;
                }
            }

            return id;
        }

        /// <summary>
        /// ���ָ��CustomId�Ƿ��Ѿ�����
        /// </summary>
        public bool ExistsCustomId(string customId)
        {
            return _dicCustomIds.ContainsKey(customId);
        }

        #endregion
    }
}
