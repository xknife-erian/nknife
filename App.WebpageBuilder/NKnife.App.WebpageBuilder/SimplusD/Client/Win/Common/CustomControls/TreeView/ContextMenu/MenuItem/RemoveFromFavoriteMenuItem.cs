using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class RemoveFromFavoriteMenuItem : BaseTreeMenuItem
    {
        public RemoveFromFavoriteMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.removeFormFavorite");
        }

        public override void MenuOpening()
        {
            if (TreeView.CurrentNode is LinkNode)
                Visible = true;
            else
                Visible = false;
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.RemoveFromFavorite();
            base.OnClick(e);
        }
    }
}
