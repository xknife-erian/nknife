using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class SetAsDefaultTmpltMenuItem : BaseTreeMenuItem
    {
        public SetAsDefaultTmpltMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.setAsDefaultTmplt");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1 && TreeView.CurrentNode is TmpltNode)
                this.Visible = true;
            else
                this.Visible = false;
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
    }
}
