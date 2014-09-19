using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class OpenMenuItem : BaseTreeMenuItem
    {
        public OpenMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.open");
            this.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            this.Font = new Font(this.Font, FontStyle.Bold);
        }

        protected override void OnClick(EventArgs e)
        {
            ///先判断选择的数量，等于1则继续处理
            if (TreeView.SelectedNodes.Count == 1)
            {
                TreeView.OpenSubItem();
            }

            base.OnClick(e);
        }

        public override void MenuOpening()
        {
            ///先判断选择的数量，不等于1则不显示
            if (TreeView.SelectedNodes.Count == 1)
            {
                BaseTreeNode node = TreeView.CurrentNode;
                if (node.NodeType == TreeNodeType.Link)
                {
                    node = ((LinkNode)node).TargetNode;
                }

                if (node.NodeType == TreeNodeType.Link
                    || node.NodeType == TreeNodeType.Page
                    || node.NodeType == TreeNodeType.ResourceFile
                    || node.NodeType == TreeNodeType.Tmplt)
                {
                    this.Visible = true;
                    return;
                }
            }
            this.Visible = false;
        }
    }
}
