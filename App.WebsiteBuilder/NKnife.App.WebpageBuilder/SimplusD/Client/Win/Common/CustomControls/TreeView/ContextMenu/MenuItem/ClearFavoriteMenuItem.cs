using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class ClearFavoriteMenuItem : BaseTreeMenuItem
    {
        public ClearFavoriteMenuItem(MyTreeView treeView)
            :base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.clearFavorite");
        }

        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            if (TreeView.CurrentNode.NodeType != TreeNodeType.Favorite)
            {
                Visible = false;
                return;
            }

            Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.ClearFavorite();

            base.OnClick(e);
        }
    }
}
