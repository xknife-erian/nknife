using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    //页面片
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, CanDelete = false)]
    public class SnipElementNode : TmpltBaseTreeNode
    {
        public SnipElementNode(SnipXmlElement element)
            :base(element)
        {
        }

        #region 字段与属性

        public new AnyXmlElement Element
        {
            get
            {
                return base.Element as SnipXmlElement;
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
                return (base.Element as SnipXmlElement).Id;
            }
        }
        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.Snip;
            }
        }
        /// <summary>
        /// 返回当前页面片节点的类型
        /// </summary>
        public override TmpltTreeNodeType CurrentNodeType
        {
            get
            {
                TmpltTreeNodeType retNodeType = TmpltTreeNodeType.none;
                switch (((SnipXmlElement)Element).SnipType)
                {
                    case PageSnipType.General:
                        retNodeType = TmpltTreeNodeType.snipGeneral;
                        break;
                    case PageSnipType.Content:
                        retNodeType = TmpltTreeNodeType.snipContent;
                        break;
                }
                return retNodeType;
            }
        }

        #endregion

        #region 方法和重写基类的方法

        public override void LoadData(TmpltTreeNodeType NodeFilterType)
        {
            this.Text = ((SnipXmlElement)Element).SnipName;
            this.ToolTipText = ((SnipXmlElement)Element).SnipType.ToString();
            LoadChildNodes(NodeFilterType);
        }

        protected override void LoadChildNodes(TmpltTreeNodeType NodeFilterType)
        {
            if (((SnipXmlElement)Element).GetPartsElement() == null) return;

            foreach (SnipPartXmlElement snipPart in ((SnipXmlElement)Element).GetPartsElement().ChildNodes)
            {
                AddElementNode(snipPart,NodeFilterType);

            }
        }

        public override void SetNoChildNodesText( )
        {
        }

        public override void RenewNodeText(TmpltBaseTreeNode node)
        {
        }

        public override string CollapseImageKey
        {
            get
            {
                return "";
            }
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
