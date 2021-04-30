using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NKnife.Chesses.Common;
using NKnife.Chesses.Common.Base;
using NKnife.Chesses.Common.Interface;
using NKnife.Chesses.Common.Record;

namespace Gean.Module.Chess.Demo
{
    partial class MyDemoDialog : Form
    {

        #region static property

        private static MyDemoDialog _form = null;

        public static string Title
        {
            get { return _form.Text; }
            set { _form.Text = value; }
        }
        public static string StatusText1
        {
            get { return _form._statusLabel1.Text; }
            set { _form._statusLabel1.Text = value; }
        }
        public static string StatusText2
        {
            get { return _form._statusLabel2.Text; }
            set { _form._statusLabel2.Text = value; }
        }
        public static string TextBox1
        {
            get { return _form._textBox1.Text; }
            set { _form._textBox1.Text = value; }
        }
        public static string TextBox2
        {
            get { return _form._textBox2.Text; }
            set { _form._textBox2.Text = value; }
        }
        public static string TextBox3
        {
            get { return _form._textBox3.Text; }
            set { _form._textBox3.Text = value; }
        }
        public static ProgressBar ProgressBar
        {
            get { return _form._progressBar; }
        }
        public static TreeView RecordTree
        {
            get { return _form._RecordTree; }
        }
        public static TreeNode RootNode
        {
            get { return _form._TreeNode; }
        }
        public static Cursor FormCursor
        {
            get { return _form.Cursor; }
            set { _form.Cursor = value; }
        }

        #endregion

        #region any Form method...

        public BaseDemoMethod Method { get; set; }
        public int DemoCount
        {
            get { return _DemoCount++; }
        }
        private int _DemoCount = 1;

        public string InitString
        {
            get { return DateTime.Now.ToLongTimeString() + " | DemoApplication1 Initialized!"; }
        }

        public MyDemoDialog()
        {
            InitializeComponent();
            _form = this;
            StatusText2 = "";
            this.Method = new BaseDemoMethod();
            this.UpdateListBox();
            this._RecordTree.NodeMouseClick += new TreeNodeMouseClickEventHandler(_RecordTree_NodeMouseClick);
        }

        void _RecordTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!(e.Node.Tag is IStepTree))
                return;
            _StepTree.Nodes.Clear();
            Record record = e.Node.Tag as Record;
            TreeNode node = new TreeNode("Game");

            this.MarkNode(record, node, new StringBuilder());

            _StepTree.BeginUpdate();
            _StepTree.Nodes.Add(node);
            _StepTree.ShowLines = true;
            _StepTree.ExpandAll();
            _StepTree.EndUpdate();
            _textBox2.Text = record.ToString();
        }

        private void MarkNode(IStepTree tree, TreeNode node, StringBuilder text)
        {
            foreach (IChessItem item in tree.Items)
            {
                TreeNode subnode = new TreeNode();
                if (item is Step)
                {
                    Step step = (Step)item;
                    if (step.GameSide == Enums.GameSide.White)
                    {
                        text = new StringBuilder();
                        text.Append(item.ItemType).Append(": ").Append(step.Generator());
                    }
                    if (step.GameSide == Enums.GameSide.Black)
                    {
                        text.Append(' ').Append(step.Generator());
                        subnode.Text = text.ToString();
                        node.Nodes.Add(subnode);
                    }
                    if (item is IStepTree)
                    {
                        IStepTree subtree = (IStepTree)item;
                        if (subtree.HasChildren)
                        {
                            MarkNode(subtree, subnode, new StringBuilder());
                        }
                    }
                }
                else
                {
                    text = new StringBuilder();
                    text.Append(item.ItemType).Append(": ").Append(item.Value);
                    subnode.Text = text.ToString();
                    node.Nodes.Add(subnode);
                }
            }
        }


        private void _OKButton_Click(object sender, EventArgs e)
        {
            this.MainDemoApplication();
        }
        private void _CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static void Clear()
        {
            _form.Clear(null, null);
        }
        private void Clear(object sender, EventArgs e)
        {
            this._textBox1.Clear();
            this._textBox2.Clear();
            this._textBox3.Clear();
            this._statusLabel1.Text = "";
            this._statusLabel2.Text = "";
            this._progressBar.Value = 0;
            this._RecordTree.Nodes.Clear();
            this._StepTree.Nodes.Clear();
            this.UpdateListBox();
            this.Update();
        }

        /// <summary>
        /// 每次运行Demo的记数更新
        /// </summary>
        private void UpdateListBox()
        {
            this.Text = Application.ProductName + " | DemoCount: " + this.DemoCount.ToString();
        }

        private void _TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.TreeNodeClick(e.Node);
        }

        #endregion

        #region call

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Method.OnLoadMethod();
        }

        /// <summary>
        /// 主Demo应用方法
        /// </summary>
        private void MainDemoApplication()
        {
            this.Method.MainClick();
        }

        /// <summary>
        /// 程序界面中TreeView中的TreeNode的点击事件的方法
        /// </summary>
        /// <param name="treeNode">被点击的TreeNode</param>
        private void TreeNodeClick(TreeNode treeNode)
        {
            this.Method.NodeClick(treeNode);
        }

        #endregion
    }
}
