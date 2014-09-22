using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewChannelMenuItem : BaseTreeMenuItem
    {
        public NewChannelMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.newChannel");
        }

        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            if (!(TreeView.CurrentNode is ChannelNode))
            {
                Visible = false;
                return;
            }

            Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewChannel();
            base.OnClick(e);
        }
    }
}
