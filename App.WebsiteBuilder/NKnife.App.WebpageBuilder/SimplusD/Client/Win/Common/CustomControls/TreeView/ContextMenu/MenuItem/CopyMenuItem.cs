using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class CopyMenuItem : BaseTreeMenuItem
    {
        public CopyMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.copy");
            this.Image = ResourceService.GetResourceImage("MainMenu.edit.copy");
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

            //foreach (BaseTreeNode node in TreeView.SelectedNodes)
            //{
            //    if (node.NodeType == TreeNodeType.SiteManager
            //        || node.NodeType == TreeNodeType.Favorite
            //        || node.NodeType == TreeNodeType.RootChannel)
            //    {
            //        Visible = false;
            //        return;
            //    }

            //    if (!(node is TmpltNode
            //        || node is ChannelNode
            //        || node is PageNode
            //        || node is ChannelFolderNode
            //        || node is TmpltFolderNode
            //        || node is ResourceFolderNode))
            //    {
            //        Visible = false;
            //        return;
            //    }
            //}

            //Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.CNode = TreeView.CurrentNode as DataNode;
            TreeView.CutCopyType = 1;
            base.OnClick(e);
        }
    }
}
