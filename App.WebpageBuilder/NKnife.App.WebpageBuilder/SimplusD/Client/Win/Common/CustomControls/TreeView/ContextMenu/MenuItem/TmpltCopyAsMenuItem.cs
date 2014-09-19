using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class TmpltCopyAsMenuItem : BaseTreeMenuItem
    {
        public TmpltCopyAsMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.tmpltCopyAs");
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
            TreeView.CopyTmpltToOtherType(((TmpltNode)TreeView.CurrentNode).Element);
            base.OnClick(e);
        }
    }
}
