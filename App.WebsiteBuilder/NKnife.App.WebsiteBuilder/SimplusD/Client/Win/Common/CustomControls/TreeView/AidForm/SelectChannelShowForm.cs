using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SelectChannelShowForm : Form
    {
        #region
        private List<string> selectesChannelIDList;
        public List<string> SelectesChannelIDList
        {
            set { selectesChannelIDList = value; }
            get { return selectesChannelIDList; }
        }
        public List<PageSimpleExXmlElement> pageList;
        public List<PageSimpleExXmlElement> PageList
        {
            set { pageList = value; }
            get { return pageList; }
        }
        public List<PageType> pageTypeList;
        public List<PageType> PageTypeList
        {
            set { pageTypeList = value; }
            get { return pageTypeList; }
        }
        #endregion
        MyTreeView chanTree = null;
        List<BaseFolderElementNode> folderList =new List<BaseFolderElementNode>();

        public SelectChannelShowForm()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        #region 选中频道后的事件
        /// <summary>
        /// 选中窗体的时候判断该频道所含有的页面 Lisuye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chanTree_SelectedNodesChanged(object sender, EventArgs e)
        {
            if (this.conShowChildNode.Checked == false)
            {
                TreeNodeSelected();
            }
            else
                this.ShowPageForAll();
        }

        private void TreeNodeSelected()
        {
            ClearCheackValue();
            if (chanTree.CurrentNode != null)
            {
                if (((ElementNode)chanTree.CurrentNode).Element.DataType == DataType.Channel)
                {
                    FolderXmlElement ele = ((BaseFolderElementNode)chanTree.CurrentNode).Element;
                    foreach (PageSimpleExXmlElement pageEle in ele.SelectNodes("child::page"))
                    {
                        IsChecked(pageEle);
                    }
                }
            }
        }
        private void IsChecked(PageSimpleExXmlElement pageEle)
        {
                if (pageEle.IsDeleted != true)
                {
                    switch (pageEle.PageType)
                    {
                        case PageType.Hr:
                            this.conHr.Checked = true;
                            break;
                        case PageType.Knowledge:
                            this.conKnowledge.Checked = true;
                            break;
                        case PageType.Product:
                            this.conProduct.Checked = true;
                            break;
                        case PageType.Project:
                            this.conProject.Checked = true;
                            break;
                        case PageType.General:
                            this.conGeneral.Checked = true;
                            break;
                        case PageType.InviteBidding:
                            this.conInviteBidding.Checked = true;
                            break;
                    }
                }
        }
        #endregion
        #region 
        /// <summary>
        /// 清空选中的页面值 Lisuye
        /// </summary>
        private void ClearCheackValue()
        {
            this.conGeneral.Checked = false;
            this.conHr.Checked = false;
            this.conInviteBidding.Checked = false;
            this.conKnowledge.Checked = false;
            this.conProduct.Checked = false;
            this.conProject.Checked=false;
        }
        #endregion
        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ShowForm();
            this.ShowTreeView();
            this.MadeChannelSelect(chanTree);
            ShowFormValue();
            this.chanTree.SelectedNodesChanged += new EventHandler(chanTree_SelectedNodesChanged);
            this.conShowChildNode.CheckedChanged += new EventHandler(conShowChildNode_CheckedChanged);
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (chanTree.CurrentNode != null)
            {
                this.OKBtn.Enabled = true;
            }
            else
                this.OKBtn.Enabled = false;
        }
   
        #endregion
        #region conShowChildNode_CheckedChanged
        /// <summary>
        /// 选中显示子级节点的事件 Lisuye
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void conShowChildNode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.conShowChildNode.Checked == false)
            {
               TreeNodeSelected();
            }
            else 
                this.ShowPageForAll();
        }
        //显示所有包括子节点
        private void ShowPageForAll()
        {         
            if (this.conShowChildNode.Checked)
            { 
                ClearCheackValue();
                string channelID = string.Empty;
                if ((ElementNode)chanTree.CurrentNode == null)
                    return;
                if (((ElementNode)chanTree.CurrentNode).Element.DataType == DataType.Channel)
                {
                    channelID = ((BaseFolderElementNode)this.chanTree.CurrentNode).Element.Id;
                    FolderXmlElement folderEle = Service.Sdsite.CurrentDocument.GetFolderElementById(channelID);
                    foreach (PageSimpleExXmlElement pageEle in folderEle.SelectNodes("//channel[@id='" + channelID + "']//page"))
                    {
                        IsChecked(pageEle);
                    }
                }
            }

        }
        #endregion
        #region 显示界面的初始状态
        /// <summary>
        /// 显示界面的初始状态
        /// </summary>
        private void ShowForm()
        {
            this.Text = "选择频道";
            this.conShowChildNode.Text = "显示包括所有子频道的页面信息";
            this.label1.Text = "1.在选择频道的同时选中页面筛选结果为";
            this.label2.Text = "显示选中频道下的所有选中页面";
            this.label3.Text = "2.在选中频道根节点并选中“显示包括所有";
            this.label4.Text = "子频道的页面信息”的同时选中页面筛选";
            this.label5.Text = "3.上面对应的两种方情况在不选择“显示";
            this.label6.Text = "包括所有子频道的页面信息”筛选结果";
            this.label7.Text = "为不显示子级的页面";
            this.label8.Text = "的结果为显示所有选中页面";
            
        }
        #endregion
        #region 初始状态下的TreeView
        /// <summary>
        /// 初始状态下的TreeView Lisuye
        /// </summary>
        private void ShowTreeView()
        {
            chanTree = new MyTreeView();
            chanTree.TreeMode = TreeMode.SelectChannel;
            chanTree.LoadTreeData();
            chanTree.CurrentNode = chanTree.SelectTreeRootChanNode;
            chanTree.Dock = DockStyle.Fill;
            this.TreePanel.Controls.Add(chanTree);
        }
        #endregion
        #region
        private void MadeChannelSelect(MyTreeView myTree)
        {
            folderList.Clear();
            ProcessSelectFolder(chanTree.SelectTreeRootChanNode);
        }
        private void ProcessSelectFolder(TreeNode BaseFolderElementNode)
        {
            foreach (TreeNode node in BaseFolderElementNode.Nodes)
            {
                if (node is BaseFolderElementNode)
                {
                    FolderXmlElement ele = ((BaseFolderElementNode)node).Element;

                    foreach (string id in SelectesChannelIDList)
                    {
                        if (id == ele.Id)
                        {
                            //node.ForeColor = Color.Green;
                            folderList.Add(node as BaseFolderElementNode);
                        }

                    }
                    ProcessSelectFolder(node);
                }
            }
        }
        #endregion
        #region OKBtn_Click,CannelBtn_Click
        /// <summary>
        /// 确定，取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
         private void OKBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ShowResult();
            SaveFormValue();
        }
         private void CannelBtn_Click(object sender, EventArgs e)
         {
             this.Close();
         }
         public void ShowResult()
         {
             PageTypeList = new List<PageType>();
             PageList = new List<PageSimpleExXmlElement>();
             if(this.conGeneral.Checked)
                     PageTypeList.Add(PageType.General);
             if(this.conHr.Checked)
                     PageTypeList.Add(PageType.Hr);
             if(this.conInviteBidding.Checked)
                     PageTypeList.Add(PageType.InviteBidding);
             if(this.conKnowledge.Checked)
                     PageTypeList.Add(PageType.Knowledge);
             if(this.conProduct.Checked)
                     PageTypeList.Add(PageType.Product);
             if(this.conProject.Checked)
                     PageTypeList.Add(PageType.Project);

             string channelID = string.Empty;
             if (((ElementNode)chanTree.CurrentNode).Element.DataType == DataType.Channel)
             {
                 channelID = ((BaseFolderElementNode)this.chanTree.CurrentNode).Element.Id;
                 FolderXmlElement folderEle = Service.Sdsite.CurrentDocument.GetFolderElementById(channelID);
                 foreach (PageSimpleExXmlElement pageEle in folderEle.SelectNodes("//channel[@id='" + channelID + "']//page"))
                 {
                     foreach (PageType pagetype in PageTypeList)
                     {
                         if (pagetype == pageEle.PageType)
                         {
                             PageList.Add(pageEle);
                             break;
                         }
                     }
                 }
             }
             State.ChannelId = channelID;
             State.PageTypeListAll = PageTypeList;
         }
        #endregion
         private void SaveFormValue()
         {
           State.Gen=this.conGeneral.Checked;
           State.Hr=this.conHr.Checked ;
           State.Bid=this.conInviteBidding.Checked ;
           State.Know=this.conKnowledge.Checked ;
           State.Prd=this.conProduct.Checked ;
           State.Prj=this.conProject.Checked;
           State.SelectChildNode = this.conShowChildNode.Checked;
           State.SelectedTreeNode = ((ElementNode)chanTree.CurrentNode).Element.Id;
         }
         private void ShowFormValue()
         {
             this.conGeneral.Checked = State.Gen;
             this.conHr.Checked = State.Hr;
             this.conInviteBidding.Checked = State.Bid;
             this.conKnowledge.Checked = State.Know;
             this.conProduct.Checked = State.Prd;
             this.conProject.Checked = State.Prj;
             this.conShowChildNode.Checked = State.SelectChildNode;
             if (State.SelectedTreeNode!=null)
                 chanTree.CurrentNode = this.chanTree.GetElementNode(State.SelectedTreeNode);
         }

         private void splitContainer3_Panel2_Paint(object sender, PaintEventArgs e)
         {

         }
    }
    public static class State
    {
        public static bool SelectChildNode{ get; set; }
        public static bool Hr { get; set; }
        public static bool Bid{ get; set; }
        public static bool Know{ get; set; }
        public static bool Prj{ get; set; }
        public static bool Prd{ get; set; }
        public static bool Gen { get; set; }
        public static string SelectedTreeNode { get; set; }
        public static string ChannelId { get; set; }
        public static List<PageType> PageTypeListAll { get; set; }

    }

}
