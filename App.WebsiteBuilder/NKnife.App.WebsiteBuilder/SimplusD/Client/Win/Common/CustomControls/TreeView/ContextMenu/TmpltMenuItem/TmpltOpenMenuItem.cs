using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltOpenMenuItem : BaseTmpltTreeMenuItem
    {
        public TmpltOpenMenuItem(TmpltTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.open");
            //this.Image = ResourceService.GetResourceImage("MainMenu.file.open");
            this.Font = new Font(this.Font, FontStyle.Bold);
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
