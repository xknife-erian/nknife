using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class PasteMenuItem : BaseTreeMenuItem
    {
        public PasteMenuItem(MyTreeView treeView)
            : base(treeView)
        {
          this.Text=  ResourceService.GetResourceText("Tree.MyTreeMenu.paste");
          this.Image= ResourceService.GetResourceImage("MainMenu.edit.MainMenu.edit.paste");
        }

        public override void MenuOpening()
        {
            ///先判断选择的数量，不等于1则不显示
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            ///判断是否枝节点，判断是否允许接收拖放
            if ((!TreeView.CurrentNode.IsBranch)
                || (TreeView.CurrentNode.AcceptDragDropType == TreeNodeType.None))
            {
                Visible = false;
                return;
            }

            ///收藏夹不接收粘贴(但接收拖拽，因为是链接形式)
            if (TreeView.CurrentNode.NodeType == TreeNodeType.Favorite)
            {
                Visible = false;
                return;
            }

            Visible = true;

            if (TreeView.CNode != null
                && TreeView.CanInto(TreeView.CNode, TreeView.CurrentNode))
            {
                Enabled = true;
            }
            else
            {
                Enabled = false;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            switch (TreeView.CutCopyType)
            {
                case 1:TreeView.CopyNode();break;
                case 2:TreeView.CutNode();break;      
            }
            TreeView.CutCopyType = 0;
            base.OnClick(e);
        }
    }
}
