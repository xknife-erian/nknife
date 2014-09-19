using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewMenuItem : BaseTreeMenuItem
    {
        public NewMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.new");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
                && TreeView.CurrentNode is BaseFolderElementNode)
                this.Visible = true;
            else
                this.Visible = false;
        }
    }
}
