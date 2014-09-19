using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewProductTmpltMenuItem : BaseTreeMenuItem
    {
        public NewProductTmpltMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.productTmplt");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
     && (TreeView.CurrentNode is TmpltRootNode || TreeView.CurrentNode is TmpltFolderNode))
                Visible = true;
            else
                Visible = false;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewTmplt(((BaseFolderElementNode)TreeView.CurrentNode).Element, TmpltType.Product);
            base.OnClick(e);
        }
    }
}
