using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class NewHomePageForm : Form
    {
        MyTreeView m_tree = null;//本窗口的树
        FolderXmlElement _parentEle = null;//欲添加到的频道(文件夹)元素

        public string NewPageId { get; private set; }

        public NewHomePageForm(FolderXmlElement parentEle, PageType pageType)
        {
            InitializeComponent();

            this.Text = "新建索引页面";
            // 初始化树:只加入频道
            _parentEle = parentEle;
            m_tree = new MyTreeView();
            m_tree.TreeMode = TreeMode.SelectTmplt;
            m_tree.SelectTmpltType = pageType;
            m_tree.LoadTreeData();

            m_tree.HideSelection = false;

            m_tree.Dock = DockStyle.Fill;
            TreePanel.Controls.Add(m_tree);
            if (parentEle == null)
                parentEle = (FolderXmlElement)((ElementNode)m_tree.SelectTreeRootChanNode).Element;

            NameTextBox.Text = XmlUtilService.CreateIncreasePageTitle(parentEle, PageType.Home);
            OKBtn.Enabled = false;
            m_tree.AfterSelect += new TreeViewEventHandler(m_tree_AfterSelect);
        }

        protected override void OnLoad(EventArgs e)
        {
            newTmpltCheckBox.Checked = true;

            base.OnLoad(e);
        }

        void m_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (m_tree.CurrentNode is TmpltNode && ((TmpltNode)m_tree.CurrentNode).Element.TmpltType==TmpltType.Home)
                OKBtn.Enabled = true;
            else
                OKBtn.Enabled = false;
        }

        #region 获得父节点
        TreeNode GetFirstTmpltNode()
        {
            foreach (TreeNode node in m_tree.Nodes)
            {
                if ((node is ChannelNode) && ((ChannelNode)node).Element.Id == _parentEle.Id)
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
        private void CannelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            ///不创建模板
            if (!newTmpltCheckBox.Checked)
            {
                if ((m_tree.CurrentNode is DataNode) && ((DataNode)m_tree.CurrentNode).NodeType == TreeNodeType.Tmplt)
                {
                    if (_parentEle == null)//如果传入的父元素为空,则父频道为网站根节点
                        _parentEle = ((ElementNode)m_tree.SelectTreeRootChanNode).Element as FolderXmlElement;

                    PageSimpleExXmlElement pageEle = doc.CreatePage(_parentEle, PageType.Home, NameTextBox.Text, ((TmpltNode)m_tree.CurrentNode).Element.Id);
                    NewPageId = pageEle.Id;
                }
                else
                {
                    MessageService.Show("请选择模板!");
                    return;
                }
            }
            ///创建页面同时创建模板
            else
            {
                KeyValuePair<string,string> keyvalue = doc.CreateHome(_parentEle, NameTextBox.Text);
                NewPageId = keyvalue.Value;
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NameTextBox.Text.Trim().Length > 0)
                OKBtn.Enabled = true;
            else
                OKBtn.Enabled = false;
        }

        private void newTmpltCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (newTmpltCheckBox.Checked)
            {
                TreePanel.Enabled = false;
                OKBtn.Enabled = true;
            }
            else
            {
                TreePanel.Enabled =OKBtn.Enabled= true;
                OKBtn.Enabled = false;
            }
            m_tree.CurrentNode = null;
        }
    }
}
