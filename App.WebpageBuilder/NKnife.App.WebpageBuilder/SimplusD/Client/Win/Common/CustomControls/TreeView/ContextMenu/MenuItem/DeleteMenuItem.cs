using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class DeleteMenuItem : BaseTreeMenuItem
    {
        public DeleteMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.delete");
            this.Image = ResourceService.GetResourceImage("MainMenu.edit.delete");
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
            //foreach (var node in TreeView.SelectedNodes)
            //{
            //    if (node is BaseFileElementNode ||
            //        node is OutsideNode ||
            //        node is ChannelFolderNode ||
            //        node is TmpltFolderNode ||
            //        node is ResourceFolderNode)
            //        Visible = true;
            //    else
            //    {
            //        Visible = false;
            //        break;
            //    }
            //}
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.DeleteSelectNodes();
            base.OnClick(e);
        }
    }
}
