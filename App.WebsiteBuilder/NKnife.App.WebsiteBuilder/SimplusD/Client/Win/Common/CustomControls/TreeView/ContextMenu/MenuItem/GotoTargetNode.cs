using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class GotoTargetNode : BaseTreeMenuItem
    {
        public GotoTargetNode(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.gotoTarget");
        }

        public override void MenuOpening()
        {
            ///若选中节点数不是1，不显示
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            ///若当前节点不是Link型，不显示
            if (TreeView.CurrentNode.NodeType != TreeNodeType.Link)
            {
                this.Visible = false;
                return;
            }

            ///否则，显示
            this.Visible = true;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.CurrentNode = ((LinkNode)TreeView.CurrentNode).TargetNode;

            base.OnClick(e);
        }
    }
}
