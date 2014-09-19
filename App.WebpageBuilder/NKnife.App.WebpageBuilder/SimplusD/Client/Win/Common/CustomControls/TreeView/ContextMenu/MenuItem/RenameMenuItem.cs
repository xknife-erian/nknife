using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class RenameMenuItem : BaseTreeMenuItem
    {
        public RenameMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.rename");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            Visible = TreeView.CurrentNode.CanRename;
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.LabelEdit = true;
            TreeView.RenameNode();
            base.OnClick(e);
        }
    }
}
