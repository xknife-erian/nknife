using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class ClearPublishInfoMenuItem : BaseTreeMenuItem
    {
        public ClearPublishInfoMenuItem(MyTreeView treeView)
            :base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.clearPublishInfo");
        }

        public override void MenuOpening()
        {
            if (TreeView.SelectedNodes.Count != 1)
            {
                Visible = false;
                return;
            }

            ///只有在网站管理器这层才会出现此菜单。
            if (TreeView.CurrentNode.NodeType != TreeNodeType.SiteManager)
            {
                Visible = false;
                return;
            }

            Visible = true;

            ///只有此项目发布过，此菜单才可用
            if (string.IsNullOrEmpty(Service.Sdsite.CurrentDocument.OwnerUser))
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
            Service.Sdsite.CurrentDocument.ClearPublishInfo();
            Service.Workbench.ReloadTree();
            //此时，服务器端应作出相应的处理??？

            base.OnClick(e);
        }
    }
}
