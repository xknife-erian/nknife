using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class CutMenuItem : BaseTreeMenuItem
    {
        public CutMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.cut");
            this.Image = ResourceService.GetResourceImage("MainMenu.edit.cut");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 0)
            {
                Visible = false;
                return;
            }

            foreach (BaseTreeNode node in TreeView.SelectedNodes)
            {
                if (!node.CanDelete)
                {
                    Visible = false;
                    return;
                }
            }

            Visible = true;
            return;
            //if (TreeView.CurrentNode.NodeType == TreeNodeType.SiteManager
            //    || TreeView.CurrentNode.NodeType == TreeNodeType.Favorite)
            //{
            //    Visible = false;
            //    return;
            //}

            //if ((TreeView.CurrentNode is TmpltNode
            //    || TreeView.CurrentNode is ChannelNode
            //    || TreeView.CurrentNode is PageNode
            //    || TreeView.CurrentNode is ChannelFolderNode
            //    || TreeView.CurrentNode is TmpltFolderNode
            //    || TreeView.CurrentNode is ResourceFolderNode)
            //    && (TreeView.CurrentNode.NodeType != TreeNodeType.RootChannel))
            //{
            //    Enabled = true;
            //}
            //else
            //    Enabled = false;
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.CNode = TreeView.CurrentNode as DataNode;
            TreeView.CutCopyType = 2;
            base.OnClick(e);
        }
    }
}
