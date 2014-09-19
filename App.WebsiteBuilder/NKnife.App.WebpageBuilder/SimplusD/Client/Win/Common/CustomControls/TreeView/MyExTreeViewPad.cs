using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MyExTreeViewPad : UserControl
    {
        MyTreeView _myTree = null;//树
        TreeToolStrip _toolStrip = null;//工具栏

        public TreeToolStrip ToolStrip
        {
            get { return _toolStrip; }
            set { _toolStrip = value; }
        }

        public MyTreeView MyTree
        {
            get { return _myTree; }
            set { _myTree = value; }
        }
       
        /// <summary>
        /// 构造函数
        /// </summary>
        public MyExTreeViewPad()
        {
            InitializeComponent();
            InitMy();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitMy()
        {
            _myTree = new MyTreeView();
            _toolStrip=new TreeToolStrip(_myTree);
            this.Controls.Add(_toolStrip);
            this.Controls.Add(_myTree);

            _toolStrip.Dock = DockStyle.Top;
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
        }

        public void UnloadTreeData()
        {
            _myTree.UnloadTreeData();
            _toolStrip.SetVisualForDisposeTree();
        }

        public void RefreshSiteTreeData()
        {
            UnloadTreeData();
            LoadTreeData();
        }
    }
}
