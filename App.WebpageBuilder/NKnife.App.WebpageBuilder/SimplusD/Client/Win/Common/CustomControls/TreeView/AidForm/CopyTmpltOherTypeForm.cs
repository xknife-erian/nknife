using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class CopyTmpltOherTypeForm : Form
    {
        TmpltType tranTmpltType = TmpltType.None;
        MyTreeView m_tree = null;
        TmpltSimpleExXmlElement copyEle = null;

        public TmpltType TranTmpltType
        {
            get 
            {
                if (GeneralRadioButton.Checked) tranTmpltType = TmpltType.General;
                else if (HomeRadioButton.Checked) tranTmpltType = TmpltType.Home;
                else if (ProductRadioButton.Checked) tranTmpltType = TmpltType.Product;
                else if (HrRadioButton.Checked) tranTmpltType = TmpltType.Hr;
                else if (KnowledgeRadioButton.Checked) tranTmpltType = TmpltType.Knowledge;
                else if (ProjectRadioButton.Checked) tranTmpltType = TmpltType.Project;
                else if (InvitebiddingRadioButton.Checked) tranTmpltType = TmpltType.InviteBidding;
                return tranTmpltType; 
            }
            set { tranTmpltType = value; }
        }

        public CopyTmpltOherTypeForm(TmpltSimpleExXmlElement tmpltEle)
        {
            InitializeComponent();
            copyEle = tmpltEle;
            m_tree = new MyTreeView();
            m_tree.TreeMode = TreeMode.SelectTmpltFolder;
            m_tree.LoadTreeData();
            m_tree.HideSelection = false;

            m_tree.Dock = DockStyle.Fill;
            TreePanel.Controls.Add(m_tree);

            NameLab.Text = "命名为:";
            m_tree.AfterSelect += new TreeViewEventHandler(m_tree_AfterSelect);
        }

        protected override void OnLoad(EventArgs e)
        {
            ///根据原Element，为选中饿类型赋初值
            switch (copyEle.TmpltType)
            {
                case TmpltType.General:
                    GeneralRadioButton.Checked = true;
                    break;
                case TmpltType.Product:
                    ProductRadioButton.Checked = true;
                    break;
                case TmpltType.Project:
                    ProjectRadioButton.Checked = true;
                    break;
                case TmpltType.InviteBidding:
                    InvitebiddingRadioButton.Checked = true;
                    break;
                case TmpltType.Knowledge:
                    KnowledgeRadioButton.Checked = true;
                    break;
                case TmpltType.Hr:
                    HrRadioButton.Checked = true;
                    break;
                case TmpltType.Home:
                    HomeRadioButton.Checked = true;
                    break;
                default:
                    Debug.Fail("未处理的TmpltType:" + copyEle.TmpltType);
                    break;
            }

            base.OnLoad(e);
        }

        void m_tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            BaseTreeNode myNode = e.Node as BaseTreeNode;
            if (myNode != null && (myNode.NodeType == TreeNodeType.TmpltFolder || myNode.NodeType == TreeNodeType.TmpltRootFolder))
            {
                OKBtn.Enabled = true;
                nameTextBox.Text = XmlUtilService.CreateIncreaseTmpltTitle(((ElementNode)m_tree.CurrentNode).Element as FolderXmlElement, TranTmpltType);
            }
            else
                OKBtn.Enabled = false;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            doc.CopyTmpltToOtherType(copyEle.Id, ((BaseFolderElementNode)m_tree.CurrentNode).Element.Id, TranTmpltType, nameTextBox.Text);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
