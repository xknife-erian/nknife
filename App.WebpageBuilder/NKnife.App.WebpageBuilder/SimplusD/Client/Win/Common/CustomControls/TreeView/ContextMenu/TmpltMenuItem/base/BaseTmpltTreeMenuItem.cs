using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseTmpltTreeMenuItem : ToolStripMenuItem
    {
        public BaseTmpltTreeMenuItem(TmpltTreeView treeView)
            :base()
        {
            this.TreeView = treeView;
        }

        public abstract void MenuOpening();

        public TmpltTreeView TreeView { get; private set; }
    }
}
