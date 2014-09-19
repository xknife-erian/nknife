using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseTreeMenuItem : ToolStripMenuItem
    {
        public BaseTreeMenuItem(MyTreeView treeView)
            :base()
        {
            this.TreeView = treeView;
        }

        public abstract void MenuOpening();

        public MyTreeView TreeView { get; private set; }
    }
}
