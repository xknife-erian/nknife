using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MyExTmpltTreeViewPad : UserControl
    {
        TmpltTreeView _myTree = null;//树
        TmpltTreeToolStrip _toolStrip = null;//工具栏
        TmpltTreeStatusStrip _statusStrip = null; //状态栏

        public TmpltTreeToolStrip ToolStrip
        {
            get { return _toolStrip; }
            set { _toolStrip = value; }
        }

        public TmpltTreeView MyTree
        {
            get { return _myTree; }
            set { _myTree = value; }
        }
        public TmpltTreeStatusStrip StatusStrip
        {
            get { return _statusStrip; }
            set { _statusStrip = value; }
        }

        public MyExTmpltTreeViewPad()
        {
            InitializeComponent();
            InitMy();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitMy()
        {
            _myTree = new TmpltTreeView();
            _statusStrip = new TmpltTreeStatusStrip(_myTree);
            _toolStrip = new TmpltTreeToolStrip(_myTree, _statusStrip);
            this.Controls.Add(_toolStrip);
            this.Controls.Add(_statusStrip);
            this.Controls.Add(_myTree);

            _toolStrip.Dock = DockStyle.Top;
            _statusStrip.Dock = DockStyle.Bottom;
            _myTree.Dock = DockStyle.Fill;

            _toolStrip.BringToFront();
            _myTree.BringToFront();
        }

        /// <summary>
        /// 初始化网站数据，置入Tree中展示
        /// </summary>
        public void LoadTreeData()
        {
            _myTree.LoadTreeData();
            _toolStrip.SetVisualForInitTree();
            _statusStrip.UpdateStatusStripInfo();
        }

        public void UnloadTreeData()
        {
            _myTree.UnloadTreeData();
            _toolStrip.SetVisualForDisposeTree();
            _statusStrip.DestoryStatusStripInfo();
        }

        public void RefreshSiteTreeData()
        {
            UnloadTreeData();
            LoadTreeData();
        }
    }
}
