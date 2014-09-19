using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltDeleteMenuItem : BaseTmpltTreeMenuItem
    {
        public TmpltDeleteMenuItem(TmpltTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.delete");
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
        public override void MenuOpening()
        {
        }
    }
}