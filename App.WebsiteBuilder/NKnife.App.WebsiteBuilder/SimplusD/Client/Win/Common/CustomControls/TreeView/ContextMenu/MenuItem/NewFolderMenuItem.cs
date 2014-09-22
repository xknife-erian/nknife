using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewFolderMenuItem : BaseTreeMenuItem
    {
        public NewFolderMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.addFolder");
        }
        public override void MenuOpening()
        {
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewFolder();
            base.OnClick(e);
        }
    }
}
