using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewHomePageMenuItem : BaseTreeMenuItem
    {
        public NewHomePageMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.homePage");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
                && (TreeView.CurrentNode is RootChannelNode || TreeView.CurrentNode is ChannelNode))
                Visible = true;
            else
                Visible = false;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewPage(((BaseFolderElementNode)TreeView.CurrentNode).Element, PageType.Home);
            base.OnClick(e);

        }
    }
}
