using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TmpltTreeToolStrip : ToolStrip
    {
        #region 声明
        ToolStripMenuItem refreshToolStripButton        = null;
        ToolStripMenuItem showAllToolStripButton        = null;
        ToolStripMenuItem collapseAllToolStripButton    = null;
        ToolStripMenuItem expandAllToolStripButton      = null;

        ToolStripSplitButton tmpltViewSet               = null; //模板视图设置 第一级

        ToolStripMenuItem tmpltFilter                   = null; //模板过滤 第二级
        ToolStripMenuItem snipFilter                    = null; //页面片过滤 第二级

        ToolStripMenuItem allTmplt                      = null; //第三级
        ToolStripMenuItem generalTmpltMenuItem          = null;
        ToolStripMenuItem homeTmpltMenuItem             = null;
        ToolStripMenuItem productTmpltMenuItem          = null;
        ToolStripMenuItem knowledgeTmpltMenuItem        = null;
        ToolStripMenuItem hrTmpltMenuItem               = null;
        ToolStripMenuItem inviteBidTmpltMenuItem        = null;
        ToolStripMenuItem projectTmpltMenuItem          = null;
        ToolStripMenuItem allSnip                       = null;//页面片
        ToolStripMenuItem generalSnip                   = null; 
        ToolStripMenuItem contentSnip                   = null;

        ToolStripSeparator ts1                          = null;
        ToolStripSeparator ts2                          = null;

        TmpltTreeView _myTree                           = null;
        TmpltTreeStatusStrip _myStatusStrip             = null; //在TOOLSTRIP上动作,StatusStrip要相应

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
        #endregion

        #region 初始化

        public TmpltTreeToolStrip(TmpltTreeView myTree,TmpltTreeStatusStrip myStatusStrip)
        {
            _myTree = myTree;
            _myStatusStrip = myStatusStrip;
            InitMy();
        }
        void InitMy()
        {
            refreshToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.Reflash"), null, "Refresh");
            collapseAllToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.CollapseAll"), null, "CollapseAll");
            expandAllToolStripButton = new ToolStripMenuItem("", ResourceService.GetResourceImage("Tree.ToolStrip.ExpandAll"), null, "ExpandAll");            
            ts1 = new ToolStripSeparator();
            ts2 = new ToolStripSeparator();

            allTmplt                = new ToolStripMenuItem("所有模板", null, FilterToolStripMenuItem_Click, "allTmplt");
            generalTmpltMenuItem    = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.newTmplt"), null, FilterToolStripMenuItem_Click, "generalTmpltMenuItem");
            homeTmpltMenuItem       = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.homeTmplt"), null, FilterToolStripMenuItem_Click, "homeTmpltMenuItem");
            productTmpltMenuItem    = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.productTmplt"), null, FilterToolStripMenuItem_Click, "productTmpltMenuItem");
            knowledgeTmpltMenuItem  = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.knowledgeTmplt"), null, FilterToolStripMenuItem_Click, "knowledgeTmpltMenuItem");
            hrTmpltMenuItem         = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.HRTmplt"), null, FilterToolStripMenuItem_Click, "hrTmpltMenuItem");
            inviteBidTmpltMenuItem  = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.inviteBiddingTmplt"), null, FilterToolStripMenuItem_Click, "inviteBidTmpltMenuItem");
            projectTmpltMenuItem    = new ToolStripMenuItem(ResourceService.GetResourceText("Tree.MyTreeMenu.projectTmplt"), null, FilterToolStripMenuItem_Click, "projectTmpltMenuItem");

            tmpltFilter = new ToolStripMenuItem("模板过滤");
            tmpltFilter.DropDownItems.AddRange(
                new ToolStripItem[]{
                    allTmplt,generalTmpltMenuItem,homeTmpltMenuItem,productTmpltMenuItem,
                    knowledgeTmpltMenuItem,hrTmpltMenuItem,inviteBidTmpltMenuItem,projectTmpltMenuItem
                });

            allSnip = new ToolStripMenuItem("所有页面片", null, FilterToolStripMenuItem_Click, "allSnip");
            generalSnip = new ToolStripMenuItem("普通页面片", null, FilterToolStripMenuItem_Click, "generalSnip");
            contentSnip = new ToolStripMenuItem("正文页面片", null, FilterToolStripMenuItem_Click, "contentSnip");

            snipFilter = new ToolStripMenuItem("页面片过滤");
            snipFilter.DropDownItems.AddRange(
                 new ToolStripItem[]{
                     allSnip,generalSnip,contentSnip
                }
                );
            tmpltViewSet = new ToolStripSplitButton("", ResourceService.GetResourceImage("Tree.ToolStrip.ExpandAll"), null, "tmpltViewSet");
            tmpltViewSet.DropDownItems.AddRange(
                new ToolStripItem[]{
                    tmpltFilter,snipFilter
                }
                );

            allTmplt.Checked = true;
            allSnip.Checked = true;


            refreshToolStripButton.ToolTipText = "刷新";
            collapseAllToolStripButton.ToolTipText = "全折叠";
            expandAllToolStripButton.ToolTipText = "全展开";
            tmpltViewSet.ToolTipText = "模板视图设置";

            this.Items.AddRange(new ToolStripItem[] {
                refreshToolStripButton,
                ts1,
                expandAllToolStripButton,collapseAllToolStripButton,ts2,tmpltViewSet
                });
            SetVisual(false);
            this.ItemClicked += new ToolStripItemClickedEventHandler(MyTreeToolStrip_ItemClicked);
        }

        #endregion

        
        #region Click事件

        private void MyTreeToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
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
                case "allTmplt":
                    int i = 0;
                    break;
            }
        }

        /// <summary>
        /// 过滤 事件的相应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)(sender);
            TmpltTreeNodeType TmpltFilter = _myTree.TmpltFilter;
            TmpltTreeNodeType SnipFilter = _myTree.SnipFilter;
            switch (menuItem.Name)
            {
                #region 模板过滤部分
                case "allTmplt":
                    TmpltFilter = TmpltTreeNodeType.none;
                    break;
                case "generalTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.generalTmplt;
                    break;
                case "homeTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.homeTmplt;
                    break;
                case "productTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.productTmplt;
                    break;
                case "knowledgeTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.knowledgeTmplt;
                    break;
                case "hrTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.hrTmplt;
                    break;
                case "inviteBidTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.inviteBidTmplt;
                    break;
                case "projectTmpltMenuItem":
                    TmpltFilter = TmpltTreeNodeType.projectTmplt;
                    break;
                #endregion

                #region 页面过滤部分
                case "allSnip":
                    SnipFilter = TmpltTreeNodeType.none;
                    break;
                case "generalSnip":
                    SnipFilter = TmpltTreeNodeType.snipGeneral;
                    break;
                case "contentSnip":
                    SnipFilter = TmpltTreeNodeType.snipContent;
                    break;
                #endregion
            }

            //按过滤的显示
            _myTree.TmpltFilter = TmpltFilter;
            _myTree.SnipFilter  = SnipFilter;
            string[] openItem = _myTree.OpenItems;
            _myTree.BeginUpdate();

            _myTree.UnloadTreeData();
            _myTree.LoadTreeData();
            _myTree.OpenItems = openItem;
            _myTree.EndUpdate();

            //toopstrip变化 相应到状态栏上
            _myStatusStrip.UpdateStatusStripInfo();

            SetMenuItemMutex(menuItem);
        }
        /// <summary>
        /// 设置菜单互斥
        /// </summary>
        private void SetMenuItemMutex(ToolStripMenuItem menuItem)
        {
            foreach (ToolStripItem item in ((ToolStripMenuItem)menuItem.OwnerItem).DropDownItems)
            {
                if (item is ToolStripMenuItem)
                {
                    (item as ToolStripMenuItem).Checked = false;
                }
            }
            menuItem.Checked = true;
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
            refreshToolStripButton.Visible = visual;
            collapseAllToolStripButton.Visible = visual;
            expandAllToolStripButton.Visible = visual;
            tmpltViewSet.Visible = visual;
            ts1.Visible = visual;
            ts2.Visible = visual;
        }
        #endregion
    }
}
