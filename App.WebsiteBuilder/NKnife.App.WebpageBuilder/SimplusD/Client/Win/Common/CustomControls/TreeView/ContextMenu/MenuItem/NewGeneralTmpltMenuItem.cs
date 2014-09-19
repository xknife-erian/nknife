using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    class NewGeneralTmpltMenuItem : BaseTreeMenuItem
    {
        public NewGeneralTmpltMenuItem(MyTreeView treeView)
            : base(treeView)
        {
            this.Text = ResourceService.GetResourceText("Tree.MyTreeMenu.newTmplt");
            this.Image = ResourceService.GetResourceImage("MainMenu.tmplt.addTmplt");
        }

        public override void MenuOpening()
        {
            if (TreeView.CurrentNode is TmpltFolderNode || TreeView.CurrentNode is TmpltRootNode)
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            TreeView.NewTmplt(((BaseFolderElementNode)TreeView.CurrentNode).Element, TmpltType.General);
            base.OnClick(e);
        }
    }
}
