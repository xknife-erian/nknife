using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
///by zhucai:建议把操作的代码放到此Form之外，外面检测到DialogResult.OK则执行，
///且须考虑选择的多个Page
namespace Jeelu.SimplusD.Client.Win
{
    public partial class SelectTmpltForm : Form
    {
        MyTreeView newTree = null;

        public string SelectTmpltId { get; set; }

        public SelectTmpltForm(PageType pageType)
        {
            InitializeComponent();
            newTree = new MyTreeView();
            newTree.TreeMode = TreeMode.SelectTmplt;
            newTree.SelectTmpltType = pageType;
            newTree.LoadTreeData();
            newTree.Dock = DockStyle.Fill;
            tableLayoutPanel.Controls.Add(newTree);

            newTree.HideSelection = false;

            newTree.AfterSelect += new TreeViewEventHandler(newTree_AfterSelect);
            newTree.DoubleClick += new EventHandler(newTree_DoubleClick);
        }

        protected override void OnLoad(EventArgs e)
        {
            //定位到模板
            if (!string.IsNullOrEmpty(SelectTmpltId))
            {
                newTree.CurrentNode = newTree.GetElementNode(SelectTmpltId);
            }

            if (newTree.CurrentNode == null)
            {
                OKBtn.Enabled = false;
            }

            base.OnLoad(e);
        }

        void newTree_DoubleClick(object sender, EventArgs e)
        {
            if (OKBtn.Enabled)
                OKBtn.PerformClick();
        }

        /// <summary>
        /// 选择节点的变化而变化Ok按钮的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void newTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is TmpltNode)
            {
                //if (IshasContentSnip(((TmpltNode)e.Node).Element.Id))
                if (((ElementNode)e.Node).Enabled)
                    OKBtn.Enabled = true;
                else
                    OKBtn.Enabled = false;
            }
            else
                OKBtn.Enabled = false;
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            if (newTree.CurrentNode != null
                && newTree.CurrentNode is DataNode
                && ((DataNode)newTree.CurrentNode).NodeType == TreeNodeType.Tmplt)
            {
                //deleted by zhucai:不在这里设置，而是在外面设置
                //Service.Sdsite.CurrentDocument.GetPageElementById(_pageId).TmpltId
                //    = ((ElementNode)newTree.CurrentNode).Element.Id;
                //Service.Sdsite.CurrentDocument.Save();
                //MessageBox.Show("设置成功,您选择的模板是:" + newTree.CurrentNode.Text);

                this.SelectTmpltId = ((ElementNode)newTree.CurrentNode).Element.Id;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageService.Show("请选择模板!");
            }
        }

        /// <summary>
        /// 判断模板是否存在正文型面片
        /// </summary>
        /// <param name="tmpltID"></param>
        /// <returns></returns>
        private bool IshasContentSnip(string tmpltID)
        {
            TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpltID);
            XmlNodeList snips = tmpltDoc.GetElementsByTagName("snip");
            foreach (XmlNode snip in snips)
            {
                if (((SnipXmlElement)snip).SnipType == PageSnipType.Content)
                {
                    return true;
                }
            }
            return false;
        }

        #region 获得父节点
        //TreeNode GetFirstTmpltNode()
        //{
        //    foreach (TreeNode node in newTree.Nodes)
        //    {
        //        if ((node is ChannelNode) && ((ChannelNode)node).Element.Id == _parentEle.Id)
        //        {
        //            return node;
        //        }
        //        return GetFirstTmpltNode(node);
        //    }
        //    return null;
        //}

        //TreeNode GetFirstTmpltNode(TreeNode folder)
        //{
        //    foreach (TreeNode node in folder.Nodes)
        //    {
        //        if ((node is ChannelNode) && ((ChannelNode)node).Element.Id == _parentEle.Id)
        //        {
        //            return node;
        //        }
        //        GetFirstTmpltNode(node);
        //    }
        //    return null;
        //}
        #endregion
    }
}