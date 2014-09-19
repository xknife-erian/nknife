using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class SetAsIndexPageMenuItem : BaseTreeMenuItem
    {
        public SetAsIndexPageMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.setAsIndexPage");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1 && TreeView.CurrentNode is PageNode)
                this.Visible = true;
            else
                this.Visible = false;
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.SetAsIndexPage();
            base.OnClick(e);
        }
    }
}
