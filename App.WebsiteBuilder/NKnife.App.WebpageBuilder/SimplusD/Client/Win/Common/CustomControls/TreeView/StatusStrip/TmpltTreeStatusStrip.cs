using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltTreeStatusStrip : StatusStrip
    {
        TmpltTreeView _myTree = null;
        public TmpltTreeStatusStrip(TmpltTreeView myTree)
        {
            _myTree = myTree;
            InitMy();
        }
        private void InitMy()
        {
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("", null, null, "tmpltViewInfo");
            this.Items.AddRange(
                new ToolStripItem[]{
                statusLabel
                });
        }
        public void UpdateStatusStripInfo()
        {
            this.Items["tmpltViewInfo"].Text = string.Format("模板类型:{0} 页面片类型:{1}", _myTree.strTmpltNodeType, _myTree.strSnipNodeType);
        }

        public void DestoryStatusStripInfo()
        {
            foreach (ToolStripItem item in this.Items)
            {
                item.Text = String.Empty;
            }
        }
    }
}
