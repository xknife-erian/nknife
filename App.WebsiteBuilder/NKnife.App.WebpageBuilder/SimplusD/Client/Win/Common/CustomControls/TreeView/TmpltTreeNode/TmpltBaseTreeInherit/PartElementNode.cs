using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Jeelu.SimplusD.Client.Win
{
    //Part
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, CanDelete = false)]
    class PartElementNode : TmpltBaseTreeNode
    {
        public PartElementNode(SnipPartXmlElement element)
            : base(element)
        {
        }

        #region 字段与属性

        public new AnyXmlElement Element
        {
            get
            {
                return base.Element as SnipPartXmlElement;
            }
            internal set
            {
                base.Element = value;
            }
        }

        public override string ID
        {
            get
            {
                return (base.Element as SnipPartXmlElement).SnipPartId;
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.SnipPart;
            }
        }

        /// <summary>
        /// 返回当前PART节点的类型
        /// </summary>
        public override TmpltTreeNodeType CurrentNodeType
        {
            get
            {
                TmpltTreeNodeType retNodeType = TmpltTreeNodeType.none;
                switch (((SnipPartXmlElement)Element).SnipPartType)
                {
                    case SnipPartType.Static:
                        retNodeType = TmpltTreeNodeType.partStatic;
                        break;
                    case SnipPartType.Navigation:
                        retNodeType = TmpltTreeNodeType.partNavigation;
                        break;
                }
                return retNodeType;
            }
        }

        public override string CollapseImageKey
        {
            get
            {
                return "";
            }
        }
        #endregion

        #region 方法和重写基类的方法

        public override void LoadData(TmpltTreeNodeType NodeFilterType)
        {
            this.Text = ((SnipPartXmlElement)Element).SnipPartId;
            this.ToolTipText = ((SnipPartXmlElement)Element).SnipPartType.ToString();
            LoadChildNodes(NodeFilterType);
        }

        protected override void LoadChildNodes(TmpltTreeNodeType NodeFilterType)
        {//Part
            if (((SnipPartXmlElement)Element).ChildNodes == null) return;
            foreach (XmlNode node in ((SnipPartXmlElement)Element).ChildNodes)
            {
                if (node.Name == "part")
                {
                    SnipPartXmlElement snipPart = node as SnipPartXmlElement;
                    AddElementNode(snipPart,NodeFilterType);
                }
            }
        }

        public override void SetNoChildNodesText()
        {
        }
        
        public override void RenewNodeText(TmpltBaseTreeNode node)
        {
        }

        public override void LoadData()
        {
        }

        protected override void LoadChildNodes()
        {
        }

        #endregion

        
    }

}
