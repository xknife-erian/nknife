using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltGotoCss : BaseTmpltTreeMenuItem
    {
        public TmpltGotoCss(TmpltTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.TmpltTreeMenu.gotoCSS");
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