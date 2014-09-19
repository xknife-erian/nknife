using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SoftwareOptionForm : Form
    {
        ///记录当前选中的节点名称,目的保存本次选项的操作状态
        public static string TreeNodeName; 

        public SoftwareOptionForm()
        {
            _optionData.Load();
            InitializeControl();
        }

        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private OptionData _optionData = new OptionData();
        private SplitContainer _splitPanel;
        private Button _okButton;
        private Button _cancelButton;
        private TreeView _treeview;
        /// <summary>
        /// 控件排版
        /// </summary>
        private void InitializeControl()
        {
            this._splitPanel = new SplitContainer();
            this._okButton = new Button();
            this._cancelButton = new Button();

            this._splitPanel.SuspendLayout();
            this.SuspendLayout();

            #region 一般的控件定义

            // 
            // splitCtr
            // 
            this._splitPanel.Dock = DockStyle.Top;
            this._splitPanel.Location = new Point(10, 11);
            this._splitPanel.Name = "splitCtr";
            this._splitPanel.Size = new Size(645, 385);
            this._splitPanel.SplitterDistance = 170;
            this._splitPanel.TabIndex = 1;
            // 
            // btnOK
            // 
            this._okButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this._okButton.Location = new Point(490, 405);
            this._okButton.Name = "btnOK";
            this._okButton.Size = new Size(85, 30);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "btnOK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this._cancelButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this._cancelButton.DialogResult = DialogResult.Cancel;
            this._cancelButton.Location = new Point(580, 405);
            this._cancelButton.Name = "btnCancel";
            this._cancelButton.Size = new Size(85, 30);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "btnCancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // SoftOption
            // 
            ///设置一些Form的基本属性
            this.Font = new Font("Tahoma", 8.25F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.AcceptButton = this._okButton;
            this.ClientSize = new Size(680, 450);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._splitPanel);
            this.Name = "SoftwareOptionForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(10, 10, 10, 15);
            this.Text = "SoftwareOptionForm";

            #endregion

            this.AcceptButton = _okButton;
            this.CancelButton = _cancelButton;

            this._treeview = new TreeView();
            this._treeview.HideSelection = false;
            this._treeview.Dock = DockStyle.Fill;
            this._splitPanel.Panel1.Controls.Add(this._treeview);
            this._treeview.Dock = DockStyle.Fill;
            this._treeview.Nodes.AddRange(_optionData.ToTreeNode());

            this.FillControlText(this);

            this._splitPanel.IsSplitterFixed = true;
            this._splitPanel.ResumeLayout(false);
            this.ResumeLayout(false);

            this._treeview.AfterSelect += new TreeViewEventHandler(Treeview_AfterSelect);
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadPanels();
            //默认显示的树节点
            TreeNode[] nodes = this._treeview.Nodes.Find(TreeNodeName, true);
            switch (nodes.Length)
            {
                case 0:
                    this._treeview.SelectedNode = this._treeview.Nodes[0].Nodes[0];
                    break;
                case 1:
                    this._treeview.SelectedNode = nodes[0];
                    break;
                case 2:
                    this._treeview.SelectedNode = nodes[1];
                    break;
                default:
                    break;
            }

            base.OnLoad(e);
        }

        private void LoadPanels()
        {
            foreach (var panel in _optionData.OptionPanelDic.Values)
            {
                _splitPanel.Panel2.Controls.Add(panel);
                panel.PerformLayout();
                panel.FillValue();
                panel.Visible = false;
            }
        }

        AutoLayoutPanel prePanel;
        void Treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AutoLayoutPanel panel = (AutoLayoutPanel)e.Node.Tag;
            panel.Dock = DockStyle.Fill;

            if (prePanel != null)
            {
                if (panel != prePanel)
                {
                    prePanel.Visible = false;
                    panel.Visible = true;
                }
            }
            else
            {
                panel.Visible = true; 
            }
            
            
            prePanel = panel;
        }


        private void BtnOK_Click(object sender, EventArgs e)
        {
            foreach (var panel in _optionData.OptionPanelDic.Values)
            {
                panel.Save();
            }

            this._optionData.SaveTime = DateTime.Now;
            this._optionData.Save();
            ///记录当前选中节点的名称
            TreeNodeName = _treeview.SelectedNode.Name;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SoftwareOption.ReLoad();
            _optionData.Load();
            this.Close();
        }
        
        /// <summary>
        /// 显示窗体控件文本，除SplitContainer容器外
        /// </summary>
        private void FillControlText(Control ctr)
        {
            string currText;
            if (!OptionData.TextDic.TryGetValue(ctr.Text, out currText))
            {
                currText = ctr.Text;
            }
            ctr.Text = currText;
            ///对窗体的控件文本进行设置，除了splitcontainer的容器控件外
            if (ctr.HasChildren && !(ctr is SplitContainer))
            {
                foreach (Control subCtr in ctr.Controls)
                {
                    FillControlText(subCtr);
                }
            }
        }
        
    }
}