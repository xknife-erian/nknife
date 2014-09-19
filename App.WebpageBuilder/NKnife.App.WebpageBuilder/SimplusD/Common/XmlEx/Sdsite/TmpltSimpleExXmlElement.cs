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
        /// 获取节点对应的文件对象
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

        #region IGetDocumentable 成员

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
                return oldOwnerFolder.OldRelativeFilePath + oldFileName + Utility.Const.TmpltFileExt;
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

                ///通过原父节点和原文件名即可得到原路径
                return oldOwnerFolder.OldRelativeUrl;
            }
        }

        #region CustomeId相关的

        /// <summary>
        /// 仅用key存储CustomId的集合
        /// </summary>
        Dictionary<string, SimpleExIndexXmlElement> _dicCustomIds = new Dictionary<string, SimpleExIndexXmlElement>();

        /// <summary>
        /// 创建不重复的CustomId
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
                    Debug.Fail("未处理的IdType:" + type);
                    break;
            }

            for (long i = 1; i < long.MaxValue; i++)
            {
                ///临时组成的CustomId
                string tempCustomId = profix + i;

                ///检查此CustomId是否已经存在
                if (!ExistsCustomId(tempCustomId))
                {
                    ///不存在此id，则使用此id
                    id = tempCustomId;
                    break;
                }
            }

            return id;
        }

        /// <summary>
        /// 检查指定CustomId是否已经存在
        /// </summary>
        public bool ExistsCustomId(string customId)
        {
            return _dicCustomIds.ContainsKey(customId);
        }

        #endregion
    }
}
