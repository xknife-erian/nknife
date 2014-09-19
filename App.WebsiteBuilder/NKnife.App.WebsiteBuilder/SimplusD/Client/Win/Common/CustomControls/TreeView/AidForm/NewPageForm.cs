using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class NewPageForm : Form
    {
        MyTreeView m_tree = null;//本窗口的树
        FolderXmlElement _parentEle = null;//欲添加到的频道(文件夹)元素

        public PageType PageType { get; private set; }
        public string TmpltId { get; set; }
        public string NewPageId { get; private set; }

        public NewPageForm(FolderXmlElement parentEle, PageType pageType)
        {
            PageType = pageType;

            InitializeComponent();
            Label label1 = new Label();
            label1.AutoSize = true;
            label1.Location = new Point(24, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 12);
            label1.TabIndex = 0;

            panel3.Controls.Add(label1);

            // 初始化树:只加入频道
            _parentEle = parentEle;
            m_tree = new MyTreeView();
            m_tree.MultiSelect = true;
            m_tree.TreeMode = TreeMode.SelectTmplt;
            m_tree.SelectTmpltType = PageType;
            m_tree.LoadTreeData();

            m_tree.HideSelection = false;

            if (parentEle != null)
            {
                TreeNode parentNode = GetFirstTmpltNode();
                if (parentNode != null && parentNode.Nodes.Count > 0)
                    m_tree.CurrentNode = (BaseTreeNode)parentNode.Nodes[0];
            }
            m_tree.Dock = DockStyle.Fill;
            m_tree.ExpandAll();
            TreePanel.Controls.Add(m_tree);
            label1.Text = "新建一个页面,请选择一个将要关联的模板!!";
            if (parentEle == null)
            {
                parentEle = (FolderXmlElement)((ElementNode)m_tree.SelectTreeRootChanNode).Element;
            }

            NameTextBox.Text = XmlUtilService.CreateIncreasePageTitle(parentEle,pageType);
            OKBtn.Enabled = false;
            m_tree.SelectedNodesChanged += new EventHandler(m_tree_SelectedNodesChanged);
        }

        /// <summary>
        /// 选择模板后方激活确定按钮
        /// </summary>
        void m_tree_SelectedNodesChanged(object sender, EventArgs e)
        {
            if (m_tree.SelectedNodes.Count != 1)
            {
                OKBtn.Enabled = false;
                return;
            }
            if (m_tree.CurrentNode is TmpltNode)
            {
                if (((ElementNode)m_tree.CurrentNode).Enabled)
                    OKBtn.Enabled = true;
                else
                    OKBtn.Enabled = false;
            }
            else
                OKBtn.Enabled = false;
        }

        #region 获得父节点
        TreeNode GetFirstTmpltNode()
        {
            foreach (TreeNode node in m_tree.Nodes)
            {
                if ((node is TmpltFolderNode) && ((TmpltFolderNode)node).Element.Id == _parentEle.Id)
                {
                    return node;
                }
                return GetFirstTmpltNode(node);
            }
            return null;
        }

        TreeNode GetFirstTmpltNode(TreeNode folder)
        {
            foreach (TreeNode node in folder.Nodes)
            {
                if ((node is ChannelNode) && ((ChannelNode)node).Element.Id == _parentEle.Id)
                {
                    return node;
                }
                GetFirstTmpltNode(node);
            }
            return null;
        }
        #endregion

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if ((m_tree.CurrentNode is DataNode) && ((DataNode)m_tree.CurrentNode).NodeType == TreeNodeType.Tmplt)
            {
                this.TmpltId = ((TmpltNode)m_tree.CurrentNode).Element.Id;

                //如果传入的父元素为空,则父频道为网站根节点
                if (_parentEle == null)
                {
                    _parentEle = ((ElementNode)m_tree.SelectTreeRootChanNode).Element as FolderXmlElement;
                    PageType = (PageType)((int)((TmpltNode)m_tree.CurrentNode).Element.TmpltType);
                }

                ///检查是否有重名
                //if (File.Exists(Path.Combine(_parentEle.AbsoluteFilePath, NameTextBox.Text + Utility.Const.PageFileExt)))
                //{
                //    MessageService.Show("此文件已存在，请重新命名。");
                //    NameTextBox.SelectAll();
                //    NameTextBox.Focus();
                //    return;
                //}

                SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
                PageSimpleExXmlElement pageEle = doc.CreatePage(_parentEle.Id, PageType, NameTextBox.Text, TmpltId);

                this.NewPageId = pageEle.Id;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageService.Show("请选择模板!");
            }
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NameTextBox.Text.Trim().Length > 0)
                OKBtn.Enabled = true;
            else
                OKBtn.Enabled = false;
        }

        private void CannelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
