using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewInviteBidPageMenuItem : BaseTreeMenuItem
    {
        public NewInviteBidPageMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.inviteBiddingPage");
        }
        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 1
                && (TreeView.CurrentNode is ChannelFolderNode || TreeView.CurrentNode is ChannelNode))
                Visible = true;
            else
                Visible = false;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewPage(((BaseFolderElementNode)TreeView.CurrentNode).Element, PageType.InviteBidding);
            base.OnClick(e);

        }
    }
}
