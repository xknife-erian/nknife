using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TreeToolStrip : ToolStrip
    {
        #region 声明
        ToolStripMenuItem propertyToolStripButton = null;
        ToolStripMenuItem refreshToolStripButton = null;
        ToolStripMenuItem showAllToolStripButton = null;
        ToolStripMenuItem collapseAllToolStripButton = null;
        ToolStripMenuItem expandAllToolStripButton = null;
        ToolStripSeparator ts1 = null;
        MyTreeView _myTree = null;

        public ToolStripMenuItem ShowAllToolStripButton
        {
            get { return showAllToolStripButton; }
            set { showAllToolStripButton = value; }
        }
        public ToolStripMenuItem RefreshToolStripButton
        {
            get { return refreshToolStripButton; }
            set { refreshToolStripButton = value; }
        }
        public ToolStripMenuItem PropertyToolStripButton
        {
            get { return propertyToolStripButton; }
            set { propertyToolStripButton = value; }
        }
        #endregion

        #region 初始化
        public TreeToolStrip(MyTreeView myTree)
        {
            _myTree = myTree;
            InitMy();
        }

        void InitMy()
        {
            propertyToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("MainMenu.view.property"), null, "ChildNodes");
            refreshToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.Reflash"), null, "Refresh");
            //showAllToolStripButton = new ToolStripMenuItem("所有文件", null, null, "ShowAll");
            collapseAllToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.CollapseAll"), null, "CollapseAll");
            expandAllToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.ExpandAll"), null, "ExpandAll");
            ts1 = new ToolStripSeparator();

            propertyToolStripButton.ToolTipText = "属性";
            refreshToolStripButton.ToolTipText = "刷新";
            //showAllToolStripButton.ToolTipText = "显示所有文件";
            collapseAllToolStripButton.ToolTipText = "全折叠";
            expandAllToolStripButton.ToolTipText = "全展开";

            this.Items.AddRange(new ToolStripItem[] {
                propertyToolStripButton,refreshToolStripButton,//showAllToolStripButton,
                ts1,
                expandAllToolStripButton,collapseAllToolStripButton
                });
            SetVisual(false);
            this.ItemClicked += new ToolStripItemClickedEventHandler(MyTreeToolStrip_ItemClicked);
        }
        #endregion

        #region Click事件
        void MyTreeToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "ChildNodes":
                    {
                        break;
                    }
                case "ShowAll":
                    {
                        ToolStripMenuItem tsm = ((ToolStripMenuItem)e.ClickedItem);
                        if (tsm.CheckState == CheckState.Checked)
                        {
                            tsm.CheckState = CheckState.Unchecked;
                            _myTree.ShowAllFiles = false;
                            UnShowAllFile();
                        }
                        else if (tsm.CheckState == CheckState.Unchecked)
                        {
                            tsm.CheckState = CheckState.Checked;
                            _myTree.ShowAllFiles = true;
                            ShowAllFile();
                        }
                        break;
                    }
                case "Refresh":
                    {
                        string[] openItem = _myTree.OpenItems;
                        _myTree.BeginUpdate();

                        _myTree.UnloadTreeData();
                        _myTree.LoadTreeData();
                        _myTree.OpenItems = openItem;

                        _myTree.EndUpdate();
                        break;
                    }
                case "CollapseAll":
                    {
                        _myTree.CollapseAll();
                        break;
                    }
                case "ExpandAll":
                    {
                        TreeNode nextVisibleNode = _myTree.Nodes[0];
                        while (nextVisibleNode != null)
                        {
                            TreeNode tempNode = nextVisibleNode;
                            nextVisibleNode = nextVisibleNode.NextVisibleNode;

                            tempNode.Expand();
                        }
                        break;
                    }
            }
        }
        #endregion

        #region 显示所有
        private void UnShowAllFile()
        {
            ProssUnShowAll(_myTree.SiteManagerNode.RootChannelNode);
        }

        private void ProssUnShowAll(BaseFolderElementNode folderNode)
        {
            foreach (BaseTreeNode node in folderNode.Nodes)
            {
                if (node is OutsideNode)
                {
                    node.Parent.RemoveChildNode(node);
                }
                else if (node is BaseFolderElementNode)
                {
                    ProssUnShowAll(node as BaseFolderElementNode);
                }
            }
        }

        private void ShowAllFile()
        {
            ProssShowAll(_myTree.SiteManagerNode.RootChannelNode);
        }

        private void ProssShowAll(BaseFolderElementNode folderNode)
        {
            //try
            //{
            //    string[] files = Directory.GetFiles(folderNode.Element.AbsoluteFilePath);
            //    foreach (string file in files)
            //    {
            //        string id = Utility.File.GetXmlDocumentId(file);
            //        string exName = Path.GetExtension(file);
            //        SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            //        AnyXmlElement ele = doc.GetElementById(id);
            //        if (ele == null || ((SimpleExIndexXmlElement)ele).IsExclude)
            //        {
            //            FileOutsideNode outNode = new FileOutsideNode(file);
            //            outNode.Text = Path.GetFileNameWithoutExtension(file);
            //            folderNode.Nodes.Add(outNode);

            //            SimpleExIndexXmlElement newEle = null;
            //            if (ele == null)
            //            {
            //                switch (exName)
            //                {
            //                    case Utility.Const.PageFileExt:
            //                        {
            //                            //Service.Sdsite.CurrentDocument.CreatePage()
            //                            //newEle = new PageSimpleExXmlElement();
            //                            //newEle.Id = Guid.NewGuid().ToString("N");
            //                            break;
            //                        }
            //                }
            //                folderNode.Element.AppendChild(newEle);
            //            }
            //        }
            //    }


            //    string[] Directories = Directory.GetDirectories(folderNode.Element.AbsoluteFilePath);
            //    foreach (SimpleExIndexXmlElement Dir in folderNode.Element.ChildNodes)
            //    {
            //        if (Dir is FolderXmlElement && Dir.IsExclude)
            //        {
            //            FolderOutsideNode outNode = new FolderOutsideNode(Dir.AbsoluteFilePath);
            //            outNode.Text = Path.GetFileName(Path.GetDirectoryName(Dir.AbsoluteFilePath));
            //            folderNode.Nodes.Add(outNode);
            //        }
            //    }

            //    TreeNodeCollection childFolderNodes = folderNode.Nodes;
            //    foreach (TreeNode childFolder in childFolderNodes)
            //    {
            //        if (childFolder is BaseFolderElementNode)
            //            ProssShowAll(childFolder as BaseFolderElementNode);
            //    }
            //}
            //catch
            //{
 
            //}
        }
        #endregion

        #region 可见性设置
        internal void SetVisualForInitTree()
        {
            SetVisual(true);
        }

        internal void SetVisualForDisposeTree()
        {
            SetVisual(false);
        }

        private void SetVisual(bool visual)
        {
            propertyToolStripButton.Visible = false;
            refreshToolStripButton.Visible = visual;
            //showAllToolStripButton.Visible = visual;
            collapseAllToolStripButton.Visible = visual;
            expandAllToolStripButton.Visible = visual;
            ts1.Visible = visual;
        }
        #endregion
    }
}
