using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class SelectTmpltMenuItem : BaseTreeMenuItem
    {
        public SelectTmpltMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.selectTmplt");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 0)
            {
                this.Visible = false;
                return;
            }

            foreach (BaseTreeNode node in TreeView.SelectedNodes)
            {
                if (!(node is PageNode))
                {
                    this.Visible = false;
                    return;
                }
            }

            this.Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.SelectTmpltForm(((PageNode)TreeView.CurrentNode).Element.PageType);
            base.OnClick(e);
        }
    }
}
