using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class ImportResourceMenuItem : BaseTreeMenuItem
    {
        public ImportResourceMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.importResource");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
                && (TreeView.CurrentNode is ResourceRootNode ||
                   TreeView.CurrentNode is ResourceFolderNode))
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.ImportResource();
            base.OnClick(e);
        }
    }
}
