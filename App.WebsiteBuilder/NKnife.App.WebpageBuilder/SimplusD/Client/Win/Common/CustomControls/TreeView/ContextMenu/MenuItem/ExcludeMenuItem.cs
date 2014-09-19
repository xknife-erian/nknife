using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class ExcludeMenuItem : BaseTreeMenuItem
    {
        public ExcludeMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = "从项目中排除";
        }

        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count == 0)
            {
                Visible = false;
                return;
            }

            foreach (BaseTreeNode node in TreeView.SelectedNodes)
            {
                if (node.NodeType == TreeNodeType.RootChannel
                    || !(node is BaseFileElementNode
                    || node is ChannelFolderNode
                    || node is TmpltFolderNode
                    || node is ResourceFolderNode))
                {
                    Visible = false;
                    return;
                }
            }

            Visible = true;
        }


        protected override void OnClick(EventArgs e)
        {
            TreeView.ExcludeItem();
            base.OnClick(e);
        }
    }
}
