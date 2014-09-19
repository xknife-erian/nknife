using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class BaseSelectForm : BaseForm
    {
        public BaseSelectForm(PageType type)
        {
            this._pageType = type;
            InitializeMyContorls();
            InitializeMyTree(this._pageType);

            this._btnOK.Click += new EventHandler(_btnOK_Click);
            this._btnCancel.Click += new EventHandler(_btnCancel_Click);
            this._tree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(Tree_NodeMouseDoubleClick);
        }
        void Tree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OkMethod();
        }

        void _btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _btnOK_Click(object sender, EventArgs e)
        {
            TreeNode node = (TreeNode)this._tree.SelectedNode;
            OkMethod();
        }

        private void OkMethod()
        {
            if ( this._tree.SelectedNode == null ) {
                this.DialogResult = DialogResult.Cancel;
            }
            else {
                PageSimpleExXmlElement ele = Service.Sdsite.CurrentDocument.GetPageElementById(this._tree.SelectedNode.Name);
                //ChannelSimpleExXmlElement ele =
                //    Service.Sdsite.CurrentDocument.GetChannelElementById(this._tree.SelectedNode.Name);
                //int m = (int)ele.ChannelType;
                int m = (int)ele.PageType;
                int n = (int)this._pageType;
                bool typeBool = (m == n);
                if ( !typeBool ) {
                    DialogResult result;
                    result = MessageBox.Show(
                        this,
                        "choseChannel",//this.GetTextResource("choseChannel"),
                        "NotChoseChannel",//this.GetTextResource("NotChoseChannel"),
                        MessageBoxButtons.OKCancel, 
                        MessageBoxIcon.Exclamation);
                    if ( result == DialogResult.Cancel ) {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else {
                    this.DialogResult = DialogResult.OK;
                    this._channelId = this._tree.SelectedNode.Name;
                    this.Close();
                }
            }
        }

        private PageType _pageType;
        public PageType PageTypeBySelectForm
        {
            get { return _pageType; }
            set { _pageType = value; }
        }

        private void InitializeMyTree(PageType type)
        {
            ChannelSimpleExXmlElement rootChannel
                = Service.Sdsite.CurrentDocument.GetChannelElementById(Utility.Const.ChannelRootId);
            TreeNode treeNode = new TreeNode();
            treeNode.Name = rootChannel.Id;
            treeNode.Text = rootChannel.Title;
            this._tree.Nodes.Add(treeNode);
            CreatTreeNode(rootChannel, treeNode);
            treeNode.Expand();
        }

        private void CreatTreeNode(AnyXmlElement channelEle, TreeNode treeNode)
        {
            foreach ( AnyXmlElement simEle in channelEle.ChildNodes ) {
                if ( simEle.NodeType != XmlNodeType.Element ) {
                    break;
                }
                TreeNode subTreeNode = null;
                if ( simEle.Name == "channel" )//一定要是频道
                {
                    PageSimpleExXmlElement currEle = (PageSimpleExXmlElement)simEle;

                   // ChannelSimpleExXmlElement currEle = (ChannelSimpleExXmlElement)simEle;
                    if ( currEle.IsDeleted ) {
                        break;
                    }
                    subTreeNode = new TreeNode();
                    int m = (int)currEle.PageType;
                   // int m = (int)currEle.ChannelType;
                    int n = (int)this._pageType;
                    bool typeBool = (m == n);
                    if ( !typeBool ) {
                        subTreeNode.ForeColor = System.Drawing.Color.Silver;
                        subTreeNode.Tag = false;
                    }

                    subTreeNode.Name = currEle.Id;
                    subTreeNode.Text = currEle.Title;
                    subTreeNode.Tag = true;
                    if ( currEle.HasChildNodes ) {
                        CreatTreeNode(currEle, subTreeNode);//递归
                    }
                }
                if ( subTreeNode != null ) {
                    treeNode.Nodes.Add(subTreeNode);
                }
            }
        }

        private IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if ( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected TreeView _tree;
        protected Button _btnOK;
        protected Button _btnCancel;

        private void InitializeMyContorls()
        {

            this._tree = new TreeView();
            this._btnOK = new Button();
            this._btnCancel = new Button();
            this.SuspendLayout();

            this.Size = new Size(312, 420);

            this._btnOK.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this._btnOK.Location = new Point(110, 340);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new Size(84, 28);
            this._btnOK.TabIndex = 1;
            this._btnOK.Text = "_btnOK";

            this._btnCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this._btnCancel.Location = new Point(200, 340);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new Size(84, 28);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "_btnCancel";

            this._tree.Dock = DockStyle.Top;
            this._tree.Name = "_tree";
            this._tree.TabIndex = 0;
            this._tree.Size = new Size(272, 315);
            // 
            // Form1
            // 
            ///设置一些Form的基本属性
            this.Padding = new Padding(20);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.AcceptButton = this._btnOK;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._tree);
            this.Name = "BaseSelectForm";
            this.ResumeLayout(false);
        }

        private string _channelId;
        public string ChannelId
        {
            get { return this._channelId; }
        }

    }
}
