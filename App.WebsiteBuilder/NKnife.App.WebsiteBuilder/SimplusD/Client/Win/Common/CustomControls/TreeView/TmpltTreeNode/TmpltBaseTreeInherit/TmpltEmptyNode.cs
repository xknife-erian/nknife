using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace Jeelu.SimplusD.Client.Win
{
    //Part
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, CanDelete = false)]
    class TmpltEmptyNode : TmpltBaseTreeNode
    {
        public TmpltEmptyNode(SnipPartXmlElement element)
            : base(element)
        {
            this.Text = "没有页面片";
        }
        #region 字段与属性

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.None;
            }
        }

        #endregion

        #region 方法和重写基类的方法

        public override void LoadData(TmpltTreeNodeType NodeFilterType)
        {
        }

        protected override void LoadChildNodes(TmpltTreeNodeType NodeFilterType)
        {
        }

        public override void RenewNodeText(TmpltBaseTreeNode node)
        {
        }

        public override void SetNoChildNodesText()
        {
        }

        public override void LoadData()
        {
        }

        protected override void LoadChildNodes()
        {
        }

        public override string CollapseImageKey
        {
            get
            {
                return "";
            }
        }

        #endregion
    }

}
