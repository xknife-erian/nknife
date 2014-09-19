using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class GotoCorelationTmpltMenuItem : BaseTreeMenuItem
    {
        public GotoCorelationTmpltMenuItem(MyTreeView treeView)
            :base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.gotoCorelationTmplt");
        }

        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            if (TreeView.CurrentNode.NodeType != TreeNodeType.Page)
            {
                Visible = false;
                return;
            }

            Visible = true;

            if (string.IsNullOrEmpty(((PageNode)TreeView.CurrentNode).Element.TmpltId))
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            ///找到关联的模板id
            string tmpltId = ((PageNode)TreeView.CurrentNode).Element.TmpltId;
            if (string.IsNullOrEmpty(tmpltId))
            {
                return;
            }

            ///设置找到的模板节点为当前节点
            ElementNode tmpltNode = TreeView.GetElementNode(tmpltId);
            if (tmpltNode == null)
            {
                MessageService.Show("没有找到此页面关联的模板！");
            }
            else
            {
                TreeView.CurrentNode = tmpltNode;
            }

            base.OnClick(e);
        }
    }
}
