using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = true,
     CanDelete = true,
     AcceptDragDropType = TreeNodeType.Page | TreeNodeType.ChannelFolder)]
    public class ChannelFolderNode : BaseFolderElementNode
    {
        public ChannelFolderNode(FolderXmlElement element)
            :base(element)
        {
        }

        public new FolderXmlElement Element
        {
            get
            {
                return base.Element as FolderXmlElement;
            }
            protected set
            {
                base.Element = value;
            }
        }

        protected override void LoadChildNodes()
        {
            LoadChildChannelNode();
            LoadChildFolderNode();
            LoadChildPageNode();
            if (!string.IsNullOrEmpty(Element.DefaultPageId))
            {
                PageNode defaultPageNode = GetDefaultPageNode();
                if (defaultPageNode != null)
                {
                    defaultPageNode.BoldFont();
                }
            }
        }

        public PageNode GetDefaultPageNode()
        {
            if (!string.IsNullOrEmpty(Element.DefaultPageId))
            {
                foreach (BaseTreeNode node in this.Nodes)
                {
                    if (node.NodeType == TreeNodeType.Page)
                    {
                        if (((PageNode)node).Element.Id == this.Element.DefaultPageId)
                        {
                            return (PageNode)node;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 加载文件夹
        /// </summary>
        private void LoadChildFolderNode()
        {
            foreach (XmlNode node in Element.ChildNodes)
            {
                if (node is FolderXmlElement)
                {
                    FolderXmlElement folderEle = (FolderXmlElement)node;

                    ///若是频道或模板文件夹，则跳过
                    if (folderEle is ChannelSimpleExXmlElement)
                    {
                        continue;
                    }

                    AddElementNode(folderEle);
                }
            }
        }

        /// <summary>
        /// 加载频道
        /// </summary>
        public virtual void LoadChildChannelNode()
        {
            foreach (XmlNode node in Element.ChildNodes)
            {
                if (node is ChannelSimpleExXmlElement)
                {
                    AddElementNode((SimpleExIndexXmlElement)node);
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        public virtual void LoadChildPageNode()
        {
            foreach (XmlNode node in Element.ChildNodes)
            {
                if (node is PageSimpleExXmlElement)
                {
                    AddElementNode((SimpleExIndexXmlElement)node);
                }
            }
        }

        public override string CollapseImageKey
        {
            get { return "tree.img.favorite"; }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.ChannelFolder;
            }
        }
    }
}
