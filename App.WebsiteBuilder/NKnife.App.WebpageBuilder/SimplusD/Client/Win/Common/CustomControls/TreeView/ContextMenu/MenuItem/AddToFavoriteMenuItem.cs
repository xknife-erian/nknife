using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class AddToFavoriteMenuItem : BaseTreeMenuItem
    {
        public AddToFavoriteMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.addToFavorite");
        }

        public override void MenuOpening()
        {
            if (TreeView.CurrentNode is LinkNode 
                || !(TreeView.CurrentNode is ElementNode)
                || TreeView.CurrentNode is RootChannelNode
                || TreeView.CurrentNode is TmpltRootNode
                || TreeView.CurrentNode is ResourceRootNode)
            {
                Visible = false;
            }
            else
                Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.AddToFavorite();
            base.OnClick(e);
        }
    }
}