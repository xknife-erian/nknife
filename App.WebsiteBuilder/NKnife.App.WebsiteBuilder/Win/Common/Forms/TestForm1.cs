using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.Win
{
    public partial class TestForm1 : Form
    {
        public TestForm1()
        {
            InitializeComponent();
            this.Text = Environment.CurrentDirectory;
        }

        #region tab1

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            CssPage page = CssPage.Parse(textBox1.Text);

            foreach (CssSection section in page.Sections)
            {
                TreeNode node = new TreeNode();
                treeView1.Nodes.Add(node);
                if (section is CssOtherSection)
                {
                    node.Text = section.ToString();
                    node.ForeColor = Color.Red;
                }
                else
                {
                    node.Text = section.Name;
                    BindingSection(node, section);
                    node.Expand();
                }
            }
            treeView1.EndUpdate();
        }

        void BindingSection(TreeNode parentNode, CssSection section)
        {
            foreach (CssProperty property in section.Properties)
            {
                TreeNode node = new TreeNode(property.ToString());
                parentNode.Nodes.Add(node);
            }
        }

        #endregion


        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        void LoadNode(TreeNode parentNode, string path)
        {
            string[] dirs = Directory.GetDirectories(path,"*",SearchOption.TopDirectoryOnly);

            foreach (string dir in dirs)
            {
                TestNode dirNode = new TestNode(Path.GetFileName(dir));
                parentNode.Nodes.Add(dirNode);

                LoadNode(dirNode, dir);
            }

            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                TestNode fileNode = new TestNode(Path.GetFileName(file));
                parentNode.Nodes.Add(fileNode);
            }
        }

        private void TestForm1_Load(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabPage2;

            this.testTree1.BeginUpdate();

            string testDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            TreeNode rootNode = new TestNode(testDir);
            this.testTree1.Nodes.Add(rootNode);
            LoadNode(rootNode, testDir);
            this.testTree1.EndUpdate();
        }
    }

    public class TestTree: TreeView,INavigatable
    {
        public TestTree()
        {
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            OnLevelNodesChanged(EventArgs.Empty);

            base.OnAfterSelect(e);
        }

        #region INavigatable 成员

        public void Select(INavigateNode node)
        {
            TestNode treeNode = node as TestNode;
            this.SelectedNode = treeNode;
        }

        public IEnumerable<INavigateNode> GetLevelNodes()
        {
            List<INavigateNode> list = new List<INavigateNode>();
            TestNode tempNode = this.SelectedNode as TestNode;
            while (tempNode != null)
            {
                list.Insert(0, tempNode);

                tempNode = tempNode.Parent as TestNode;
            }
            return list;
        }

        public event EventHandler LevelNodesChanged;

        protected void OnLevelNodesChanged(EventArgs e)
        {
            if (LevelNodesChanged != null)
            {
                LevelNodesChanged(this, e);
            }
        }

        #endregion
    }

    public class TestNode : TreeNode,INavigateNode
    {
        public TestNode()
        {
        }
        public TestNode(string text)
            : base(text)
        {
        }

        #region INavigateNode 成员

        public string Title
        {
            get { return this.Text; }
        }

        public string Message
        {
            get { return this.ToolTipText; }
        }

        #endregion
    }
}
