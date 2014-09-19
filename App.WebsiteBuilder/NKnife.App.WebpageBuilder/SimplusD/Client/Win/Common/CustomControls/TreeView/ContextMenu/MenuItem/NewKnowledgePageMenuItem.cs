using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class NewKnowledgePageMenuItem : BaseTreeMenuItem
    {
        public NewKnowledgePageMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.knowledgePage");
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
            TreeView.NewPage(((BaseFolderElementNode)TreeView.CurrentNode).Element,PageType.Knowledge);
            base.OnClick(e);

        }
    }
}
