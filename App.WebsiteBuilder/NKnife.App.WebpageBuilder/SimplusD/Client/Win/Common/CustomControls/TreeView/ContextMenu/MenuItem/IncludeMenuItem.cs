using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    class IncludeMenuItem : BaseTreeMenuItem
    {
        public IncludeMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = "包含在项目中";
        }

        public override void MenuOpening()
        {
            if (TreeView.CurrentNode is OutsideNode)
                Visible = true;
            else
                Visible = false;
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.IncludeItem();
            base.OnClick(e);
        }
    }
}
