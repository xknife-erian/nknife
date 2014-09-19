using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
namespace Jeelu.SimplusD.Client.Win
{
    [ResReader(false)]
    public class TmpltTreeView : TreeViewEx
    {
        #region 字段与属性

        //private TmpltBaseTreeNode CurrentNode
        //{
        //    get
        //    {
        //        if (this.SelectedNode is TmpltBaseTreeNode)
        //        {
        //            return ((TmpltBaseTreeNode)this.SelectedNode) as TmpltBaseTreeNode;
        //        }
        //        return null;
        //    }
        //}
       
        //用来处理重命名
        private string viewedLabel;

        //与模板视图设置 相关
        private TmpltTreeNodeType _tmpltFilter = TmpltTreeNodeType.none;
        public TmpltTreeNodeType TmpltFilter
        {
            get { return _tmpltFilter; }
            set { _tmpltFilter = value; }
        }
        private TmpltTreeNodeType _snipFilter = TmpltTreeNodeType.none;
        public TmpltTreeNodeType SnipFilter
        {
            get { return _snipFilter; }
            set { _snipFilter = value; }
        }
        private TmpltTreeNodeType _partFilter = TmpltTreeNodeType.none;
        public TmpltTreeNodeType PartFilter
        {
            get { return _partFilter; }
            set { _partFilter = value; }
        }

        #region 节点类型的字符串表示

        //模板节点类型的字符串表示
        public string strTmpltNodeType
        {
            get
            {
                string strType = String.Empty;
                switch (TmpltFilter)
                {
                    case TmpltTreeNodeType.none:
                        strType = "所有模板";
                        break;
                    case TmpltTreeNodeType.generalTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.newTmplt");
                        break;
                    case TmpltTreeNodeType.homeTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.homeTmplt");
                        break;
                    case TmpltTreeNodeType.productTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.productTmplt");
                        break;
                    case TmpltTreeNodeType.knowledgeTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.knowledgeTmplt");
                        break;
                    case TmpltTreeNodeType.hrTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.HRTmplt");
                        break;
                    case TmpltTreeNodeType.inviteBidTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.inviteBiddingTmplt");
                        break;
                    case TmpltTreeNodeType.projectTmplt:
                        strType = ResourceService.GetResourceText("Tree.MyTreeMenu.projectTmplt");
                        break;
                }
                return strType;
            }
        }

        //页面片节点类型的字符串表示
        public string strSnipNodeType
        {
            get
            {
                string strType = String.Empty;
                switch (SnipFilter)
                {
                    case TmpltTreeNodeType.none:
                        strType = "所有页面片";
                        break;
                    case TmpltTreeNodeType.snipGeneral:
                        strType = "普通页面片";
                        break;
                    case TmpltTreeNodeType.snipContent:
                        strType = "正文页面片";
                        break;
                }
                return strType;
            }
        }

        //PART节点类型的字符串表示
        public string strPartNodeType { get; set; }

        #endregion

        new public BaseTreeNode CurrentNode
        {
            get
            {
                return base.CurrentNode as BaseTreeNode;
            }
            set
            {
                base.CurrentNode = value;
            }
        }

        #endregion

        public TmpltTreeView()
        {
            base.ShowRootLines = true;
            this.ShowNodeToolTips = true;
            this.LabelEdit = true;
            this.ImageList = new ImageList();//ResourceService.MainImageList;
            this.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.ImageList.ImageSize = new Size(22, 16);

            //节点过滤
            TmpltFilter = TmpltTreeNodeType.none;
            SnipFilter = TmpltTreeNodeType.none;
            PartFilter = TmpltTreeNodeType.none;
            
            //树有关的事件
            this.AfterExpand += new TreeViewEventHandler(TmpltTreeView_AfterExpand);
            this.BeforeExpand += new TreeViewCancelEventHandler(TmpltTreeView_BeforeExpand);
            this.BeforeCollapse += new TreeViewCancelEventHandler(TmpltTreeView_BeforeCollapse);
            this.MouseDown += new MouseEventHandler(TmpltTreeView_MouseDown);    

            //sdsite项目的打开和关闭事件
            Service.Sdsite.SdsiteOpened += new EventHandler(SdsiteXmlDocument_SdsiteOpened);
            Service.Sdsite.SdsiteClosing += new EventHandler(SdsiteXmlDocument_SdsiteClosing);

            this.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(TmpltTreeView_NodeMouseDoubleClick);
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            e.Node.Text = e.Label.Replace(" ", "");
            base.OnAfterLabelEdit(e);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            if (this.TreeMode != TreeMode.General)//非右侧树不可更名
            {
                e.CancelEdit = true;
                return;
            }
            base.OnBeforeLabelEdit(e);
        }
        /// <summary>
        /// 重写键盘按键事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (TreeMode == TreeMode.General)
            {
                if (this.CurrentNode != null && !this.CurrentNode.IsEditing)
                {
                    if (keyData == Keys.Delete)
                    {
                        MessageBox.Show("fasdf");
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            BaseTreeNode myNode = e.Node as BaseTreeNode;

            ///若ImageKey不等于CollapseImageKey，则认为它的图标正在表示特殊用途，那么不更换图标
            if (myNode.ImageKey == myNode.CollapseImageKey)
            {
                //myNode.ImageKey = myNode.FactImageKey;
                //myNode.SelectedImageKey = myNode.ImageKey;
            }
            base.OnAfterExpand(e);
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            BaseTreeNode myNode = e.Node as BaseTreeNode;

            ///若ImageKey不等于ExpandImageKey，则认为它的图标正在表示特殊用途，那么不更换图标
            if (myNode.ImageKey == myNode.ExpandImageKey)
            {
                //myNode.ImageKey = myNode.FactImageKey;
                //myNode.SelectedImageKey = myNode.ImageKey;
            }
            base.OnAfterCollapse(e);
        }








        private void AddTmpltFolder(FolderXmlElement Folder)
        {
            foreach (var node in Folder.ChildNodes)
            {
                if (node is FolderXmlElement)
                {  
                    //有可能要加其ID  如果该文件夹有模板,并且删除该文件夹,要根据这个删模板树的内容
                    FolderElementNode FolderNode =  new FolderElementNode(node as FolderXmlElement);
                    SetElementNode(FolderNode);
                    AddTmpltFolder(node as FolderXmlElement);
                }
                else if (node is TmpltSimpleExXmlElement)
                {
                    TmpltElementNode tmpltNode = new TmpltElementNode(node as TmpltSimpleExXmlElement);
                    if ((TmpltFilter == TmpltTreeNodeType.none) || TmpltFilter == tmpltNode.CurrentNodeType)
                    {
                        SetElementNode(tmpltNode);
                        AddRootNodeToTree(tmpltNode);
                    }
                }
            }
        }
        /// <summary>
        /// 初始化树数据
        /// </summary>
        public void LoadTreeData()
        {
            this.BeginUpdate();

            this.Nodes.Clear();
            TmpltFolderXmlElement sdsiteDoc = Service.Sdsite.CurrentDocument.TmpltFolder;
            foreach (var node in sdsiteDoc.ChildNodes)
            {
                if (node is FolderXmlElement)
                {
                    FolderElementNode FolderNode = new FolderElementNode(node as FolderXmlElement);
                    SetElementNode(FolderNode);
                    AddTmpltFolder(node as FolderXmlElement);
                }
                else if (node is TmpltSimpleExXmlElement)
                {
                    TmpltElementNode tmpltNode = new TmpltElementNode(node as TmpltSimpleExXmlElement);

                    if ((TmpltFilter == TmpltTreeNodeType.none) || TmpltFilter == tmpltNode.CurrentNodeType)
                    {
                        SetElementNode(tmpltNode);
                        AddRootNodeToTree(tmpltNode);
                    }
                }
            }
            this.EndUpdate();
            this.ContextMenuStrip = TmpltTreeContextMenuStrip.CreateForTreeView(this);
        }

        /// <summary>
        /// 添加根节点的内部方法
        /// </summary>
        private void AddRootNodeToTree(TmpltElementNode node)
        {
            ///添加到树中
            this.Nodes.Add(node);
        }

        #region 树本身有关方法

        private bool expandCollapse;
        private void TmpltTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeViewHitTestInfo testInfo = this.HitTest(e.Location);
            expandCollapse = e.Clicks > 1 && (testInfo.Location == TreeViewHitTestLocations.Label
                || testInfo.Location == TreeViewHitTestLocations.Image);             
        }
        private void TmpltTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = expandCollapse; 
        }
        private void TmpltTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = expandCollapse;
            //展开前的判断
            if (!e.Cancel && e.Node is TmpltElementNode)
            {
                TmpltElementNode tmpltNode = e.Node as TmpltElementNode;
                TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpltNode.ID);
                if (tmpltDoc.GetSnipElementList().Count <= 0)
                {
                    tmpltNode.ClearTmpltNode();
                }
            }
        }
        private void TmpltTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //生成树
            if (e.Node is TmpltElementNode)
            {
                TmpltElementNode tmpltNode = e.Node as TmpltElementNode;
                TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(tmpltNode.ID);
                if (tmpltDoc.GetSnipElementList().Count <= 0 || tmpltNode.NodeExpand) return;

                tmpltNode.NodeExpand = true;
                tmpltNode.LoadData(SnipFilter);
                ResetDicNodeIndexs();
            }
        }

        /// <summary>
        /// 卸载树数据
        /// </summary>
        public void UnloadTreeData()
        {
            this.Nodes.Clear();
            this._dicNodeIndexs.Clear();
        }

        /// <summary>
        /// 定位到模板/页面片/PART
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmpltTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TmpltBaseTreeNode tmpltBaseNode = e.Node as TmpltBaseTreeNode;
            if (tmpltBaseNode == null) return;

            switch (tmpltBaseNode.NodeType)
            {
                case TreeNodeType.Tmplt:
                    //打开(或激活)模板
                    Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, ((TmpltSimpleExXmlElement)tmpltBaseNode.Element).Id);
                    break;
                case TreeNodeType.Snip:
                    {//打开页面片

                        //得到对应的页面片
                        SnipElementNode snipElementNode = tmpltBaseNode as SnipElementNode;
                        SnipXmlElement snipElement = snipElementNode.Element as SnipXmlElement;

                        //得到页面片所属的模板文件
                        TmpltElementNode tmpltElementNode = snipElementNode.Parent as TmpltElementNode;
                        TmpltSimpleExXmlElement tmpltElement = tmpltElementNode.Element as TmpltSimpleExXmlElement;

                        //先打开(或激活)对应的模板 如果该模板已经打开,不用打开也不用激活她,因为目的是为了打开页面片
                        //如果打开的话回出现令人烦的闪烁现象
                        if (!Service.Workbench.GetResultKeyID(WorkDocumentType.TmpltDesigner, tmpltElement.Id))
                        {
                            Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, tmpltElement.Id);
                        }
                        //打开（激活）页面片
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.SnipDesigner, snipElement.Id, tmpltElement.Id);


                        //因为如果添加一个页面片且没有保存,再重命名,新的名不能保存,模板里没有对应的内容
                        //所以的通知页面片设计器,将FORM的TEXT进行修改
                        snipElementNode.ChangeNodeText(e.Node.Text);
                    }

                    break;
                case TreeNodeType.SnipPart:
                    { //找到PART对应的页面片

                        PartElementNode partElemetNode = tmpltBaseNode as PartElementNode;
                        SnipPartXmlElement snipPartElement = partElemetNode.Element as SnipPartXmlElement;

                        TmpltBaseTreeNode tmpltNode = (TmpltBaseTreeNode)partElemetNode;
                        while (!(tmpltNode is SnipElementNode))
                        {
                            tmpltNode = (TmpltBaseTreeNode)(tmpltNode.Parent);
                        }

                        //得到对应的页面片
                        SnipElementNode snipElementNode = tmpltNode as SnipElementNode;
                        SnipXmlElement snipElement = snipElementNode.Element as SnipXmlElement;                     


                        ////得到页面片所属的模板文件
                        TmpltElementNode tmpltElementNode = snipElementNode.Parent as TmpltElementNode;
                        TmpltSimpleExXmlElement tmpltElement = tmpltElementNode.Element as TmpltSimpleExXmlElement;

                        //先打开(或激活)对应的模板
                        if (!Service.Workbench.GetResultKeyID(WorkDocumentType.TmpltDesigner, tmpltElement.Id))
                        {
                            Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, tmpltElement.Id);
                        }

                        //打开页面片！
                        Service.Workbench.OpenWorkDocument(WorkDocumentType.SnipDesigner, snipElement.Id, tmpltElement.Id);
                        string []strArray = new string[2];
                        strArray[0] = snipElement.Id;
                        strArray[1] = snipPartElement.SnipPartId;

                        SdsiteXmlDocument.OnOrientationPart(new EventArgs<string[]>(strArray));
                    }
                    break;
            }
        }

        //protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        //{
        //    TmpltBaseTreeNode tn = this.CurrentNode as TmpltBaseTreeNode;
        //    if (tn == null || !(tn is SnipElementNode))
        //    {
        //        e.CancelEdit = true;
        //    }
        //    else
        //    {
        //        viewedLabel = e.Node.Text;
        //    }
           
        //}

        //protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        //{//重命名得通知页面模板设计器的TITLE

        //    if (string.IsNullOrEmpty(e.Label))
        //    {
        //        e.CancelEdit = true;
        //        return;
        //    }
        //    if (viewedLabel.Equals(e.Label)) return; //如果和原来的相同,则不处理返回

        //    TmpltBaseTreeNode tmpltBaseTreeNode = e.Node as TmpltBaseTreeNode;
        //    if (!tmpltBaseTreeNode.BrotherNodeRepeatName(e.Label))
        //    {
        //        tmpltBaseTreeNode.ChangeNodeText(e.Label);
        //    }
        //    else
        //    {
        //        e.CancelEdit = true;
        //        MessageService.Show("文件名重复或不合法！", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //}
        /// <summary>
        /// 重命名
        /// </summary>
        public void RenameNode()
        {//现在仅支持页面片重命名

            TmpltBaseTreeNode tn = (TmpltBaseTreeNode)this.CurrentNode;
            if (tn != null && tn is SnipElementNode) //仅支持页面片重命名
            {
                NodeLabelEditEventArgs e = new NodeLabelEditEventArgs(tn);
                base.OnBeforeLabelEdit(e);

                if (!e.CancelEdit)
                {
                    this.LabelEdit = true;
                    tn.BeginEdit();
                }
            }
            
        }

        #endregion

        #region 获取或设置树中项目的打开状态

        /// <summary>
        /// 获取或设置树中项目的打开状态
        /// </summary>
        public string[] OpenItems
        {
            get
            {
                List<string> _openItems = new List<string>();
                foreach (TreeNode treenode in this.Nodes)
                {
                    TmpltBaseTreeNode node = treenode as TmpltBaseTreeNode;
                    if (node != null && node.IsExpanded)
                    {
                        if (node is PartElementNode)
                        {
                            SnipPartXmlElement snipPartElement = ((TmpltBaseTreeNode)node).Element as SnipPartXmlElement;
                            _openItems.Add(snipPartElement.SnipPartId);
                        }
                        else if(node is SnipElementNode)
                        {
                            SnipXmlElement snipElement = ((TmpltBaseTreeNode)node).Element as SnipXmlElement;
                            _openItems.Add(snipElement.Id);
                        }
                        else if (node is TmpltElementNode)
                        {
                            TmpltSimpleExXmlElement tmpltElement = ((TmpltBaseTreeNode)node).Element as TmpltSimpleExXmlElement;
                            _openItems.Add(tmpltElement.Id);
                        }
                    }

                    GetOpenItems(node, _openItems);
                }
                return _openItems.ToArray();
            }
            set
            {
                foreach (string id in value)
                {
                    TmpltBaseTreeNode elementNode;
                    if (_dicNodeIndexs.TryGetValue(id, out elementNode))
                    {
                        elementNode.Expand();
                    }
                }
            }
        }

        private void GetOpenItems(TmpltBaseTreeNode treeNode, List<string> _openItems)
        {
            if (treeNode == null) return;

            foreach (TreeNode treenode in treeNode.Nodes)
            {
                TmpltBaseTreeNode node = treenode as TmpltBaseTreeNode;
                if (node != null && node.IsExpanded)
                {
                    if (node is PartElementNode)
                    {
                        SnipPartXmlElement snipPartElement = ((TmpltBaseTreeNode)node).Element as SnipPartXmlElement;
                        _openItems.Add(snipPartElement.SnipPartId);
                    }
                    else if (node is SnipElementNode)
                    {
                        SnipXmlElement snipElement = ((TmpltBaseTreeNode)node).Element as SnipXmlElement;
                        _openItems.Add(snipElement.Id);
                    }
                    else if (node is TmpltElementNode)
                    {
                        TmpltSimpleExXmlElement tmpltElement = ((TmpltBaseTreeNode)node).Element as TmpltSimpleExXmlElement;
                        _openItems.Add(tmpltElement.Id);
                    }
                }
                GetOpenItems(node, _openItems);
            }
        }
        #endregion


        #region 模板文件节点要响应的事件有 添加/删除/移动/修改/重命名

        /// <summary>
        /// 对所有TmpltBaseTreeNode节点通过ID的一个索引
        /// </summary>
        private Dictionary<string, TmpltBaseTreeNode> _dicNodeIndexs = new Dictionary<string, TmpltBaseTreeNode>();

        private void SdsiteXmlDocument_SdsiteClosing(object sender, EventArgs e)
        {
            ///右侧树才需要监听这些事件
            //{
                SdsiteXmlDocument sdsite = Service.Sdsite.CurrentDocument;
                sdsite.ElementAdded -=new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAdded);
                sdsite.ElementDeleted -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementDeleted);
                sdsite.ElementTitleChanged -= new EventHandler<ChangeTitleEventArgs>(sdsite_ElementTitleChanged);
                sdsite.ElementMoved -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementMoved);
            //}
            //模板中添加了新的页面片
        }
        private void SdsiteXmlDocument_SdsiteOpened(object sender, EventArgs e)
        {
            ///右侧树才需要监听这些事件
            //{
                SdsiteXmlDocument sdsite = Service.Sdsite.CurrentDocument;
                sdsite.ElementAdded += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAdded);
                sdsite.ElementDeleted += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementDeleted);
                sdsite.ElementTitleChanged += new EventHandler<ChangeTitleEventArgs>(sdsite_ElementTitleChanged);
                sdsite.ElementMoved += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementMoved);
            //}
        }
        private void sdsite_ElementAdded(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
           
            if (e.Item is TmpltSimpleExXmlElement) //如果添加的是单个模板,则直接添加
            {
                TmpltElementNode tmpltNode = new TmpltElementNode(e.Item as TmpltSimpleExXmlElement);
                if ((TmpltFilter == TmpltTreeNodeType.none) || TmpltFilter == tmpltNode.CurrentNodeType)
                {
                    SetElementNode(tmpltNode);
                    AddRootNodeToTree(tmpltNode);
                }
            }
            else if (e.Item is FolderXmlElement) //添加整个文件夹(里面可能包括模板)
            {
                FolderElementNode folderNode = new FolderElementNode(e.Item as FolderXmlElement);
                SetElementNode(folderNode);
                //增加了模板过滤 如果和当前过滤选择不一样,则不显示
                AddTmpltFolder(e.Item as FolderXmlElement);
            }
            //else if() 参数 SimpleExIndexXmlElement 并不能包含添加的 页面片和PART 现在只做模板
            //{
            //}
        }
        private void sdsite_ElementDeleted(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ///先找原节点并删除
            ///比较麻烦 如果删除整个文件夹 或者该文件夹有一个模板,删了后 该节点也删除  elementNode 没有存文件夹的ID
            TmpltBaseTreeNode node = this.GetElementNode(e.Item.Id);
            if (node != null)
            { //_dicNodeIndexsz中的内容 只包含,模板文件夹下的问价夹,和模板文件,及其下的页面片和PART
              
                if (node is FolderElementNode) //是模板下的文件夹
                {
                    RemoveTmpltTreeNode(node.Element.ChildNodes);
                }
                else//模板文件,页面片,PART
                {
                    node.RemoveChildNode(node);
                }
            }
            
        }
        /// <summary>
        /// 删除的是整个文件夹
        /// </summary>
        /// <param name="anyNode"></param>
        private void RemoveTmpltTreeNode(XmlNodeList anyNode)
        {
            foreach (XmlNode node in anyNode)
            {
                if (node is FolderXmlElement)
                {
                    RemoveTmpltTreeNode(node.ChildNodes);
                    RemoveElementNode( (node as FolderXmlElement).Id);
                }
                else if (node is TmpltSimpleExXmlElement)
                {
                    TmpltSimpleExXmlElement tmpltElement = node as TmpltSimpleExXmlElement;
                    TmpltBaseTreeNode tmpltBaseNode = GetElementNode(tmpltElement.Id);
                    tmpltBaseNode.RemoveChildNode(tmpltBaseNode);
                }
            }
        }
        /// <summary>
        /// 重命名模板文件夹
        /// </summary>
        /// <param name="anyNode"></param>
        private void RenewFloderText(XmlNodeList anyNode)
        {
            foreach (XmlNode node in anyNode)
            {
                if (node is FolderXmlElement)
                {
                    RenewFloderText(node.ChildNodes);
                }
                else if (node is TmpltSimpleExXmlElement)
                {
                    TmpltSimpleExXmlElement tmpltElement = node as TmpltSimpleExXmlElement;
                    TmpltBaseTreeNode tmpltBaseNode = GetElementNode(tmpltElement.Id);
                    tmpltBaseNode.RenewNodeText(tmpltBaseNode);
                }
            }
        }
        private void sdsite_ElementTitleChanged(object sender, ChangeTitleEventArgs e)
        {
            //如果命名的对象为 模板 或者为模板文件夹
            TmpltBaseTreeNode node = this.GetElementNode(e.Item.Id);
            if (node != null)
            { //_dicNodeIndexsz中的内容 只包含,模板文件夹下的问价夹,和模板文件,及其下的页面片和PART

                if (node is FolderElementNode) //是模板下的文件夹
                {
                    RenewFloderText(node.Element.ChildNodes);
                }
                else if(node is TmpltElementNode)//模板文件 (现在 仅支持模板文件 重命名)
                {
                    node.RenewNodeText(node);
                }
            }
        }

        /// <summary>
        /// 元素的移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementMoved(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {//移动涉及到 Part的移动暂时不做

            //所有的模板都是一级显示,因此如果是模板的移动只是改下名，（但最好将其位置也改变一下,显示层次关系）
            //(该事件在剪切的时候触发,并且模板视图能很好相应 但拖动导致的移动尚未处理）

            TmpltBaseTreeNode node = this.GetElementNode(e.Item.Id);
            if (node != null)
            { //_dicNodeIndexsz中的内容 只包含,模板文件夹下的问价夹,和模板文件,及其下的页面片和PART

                if (node is FolderElementNode) //是模板下的文件夹
                {
                    RenewFloderText(node.Element.ChildNodes);
                }
                else if (node is TmpltElementNode)//模板文件 
                {
                    node.RenewNodeText(node);
                }
            }
        }
        /// <summary>
        /// 保存对应值
        /// </summary>
        /// <param name="elementNode"></param>
        public void SetElementNode(TmpltBaseTreeNode elementNode)
        {//因为基类没有ID属性,分开赋值

            string strNodeIndex = "";
            //模板文件
            if (elementNode is TmpltElementNode)
            {                
                strNodeIndex = ((TmpltSimpleExXmlElement)elementNode.Element).Id;
            }            
            else if (elementNode is SnipElementNode)
            {//页面片
                strNodeIndex = ((SnipXmlElement)elementNode.Element).Id;
            }             
            else if (elementNode is PartElementNode)
            {//PART
                strNodeIndex = ((SnipPartXmlElement)elementNode.Element).SnipPartId;
            }
            else if (elementNode is FolderElementNode)
            {//文件夹
                strNodeIndex = ((FolderXmlElement)elementNode.Element).Id;
            }
            if (strNodeIndex != "" && !_dicNodeIndexs.ContainsKey(strNodeIndex))
            {
                _dicNodeIndexs[strNodeIndex] = elementNode;
            }

        }

        /// <summary>
        /// 根据节点ID找到对应节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TmpltBaseTreeNode GetElementNode(string id)
        {
            TmpltBaseTreeNode elementNode = null;
            _dicNodeIndexs.TryGetValue(id, out elementNode);
            return elementNode;
        }

        public void RemoveElementNode(string id)
        {
            if (_dicNodeIndexs.ContainsKey(id))
            {
                _dicNodeIndexs.Remove(id);
            }
        }

        #endregion

        #region 页面片 节点要相应的事件有 添加/删除

        /// <summary>
        /// 在模板视图树上新添加一个页面片节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDocument_SnipAdd(object sender, EventArgs<SnipXmlElement> e)
        {
            //查找该页面所对应的模板的ID
            TmpltXmlDocument tmpltDoc = (TmpltXmlDocument)e.Item.OwnerDocument; //测试增加页面片是
            TmpltBaseTreeNode elementNode;
            if (_dicNodeIndexs.TryGetValue(tmpltDoc.Id, out elementNode))
            {
                //如果一开始没有页面片, 当添加时,要处理一下没有页面片的TEXT

                elementNode.AddElementNode(e.Item, SnipFilter);
            }
        }

        private void sdsite_SnipDel(object sender, EventArgs<SnipXmlElement> e)
        {
             TmpltBaseTreeNode tmpltNode;
             if (_dicNodeIndexs.TryGetValue(e.Item.Id, out tmpltNode))
             {
                 this.BeginUpdate();
                 TmpltBaseTreeNode tmpltParentNode = (TmpltBaseTreeNode)tmpltNode.Parent;
                 tmpltNode.RemoveChildNode(tmpltNode);

                 tmpltParentNode.SetNoChildNodesText();
                 this.EndUpdate();
                 //ResetDicNodeIndexs();

             }
        }

        private void sdsite_NoSaveMdiTmpltDesign(object sender, EventArgs<TmpltSimpleExXmlElement> e)
        {
            //应该考虑没有页面片时的显示
            //设计到_dicNodeIndexs 不存在项要删除

            //寻找该TmpltSimpleExXmlElement在树上所对应的节点
            //如果当前的模板过滤和目标模板的类型不一致也回找不到

            //TmpltXmlDocument f = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(e.Item.Id);
            //int i = f.GetSnipElementList().Count;

            TmpltBaseTreeNode tmpltNode;
            if (_dicNodeIndexs.TryGetValue(e.Item.Id, out tmpltNode))
            {
                string[] openItem = this.OpenItems;

                tmpltNode.Nodes.Clear();
                tmpltNode.LoadData(SnipFilter);

                ResetDicNodeIndexs();
                this.BeginUpdate();
                this.OpenItems = openItem;
                this.EndUpdate();
            }        



        }

        /// <summary>
        /// 当模板树被还原时,_dicNodeIndexs里的值也要变化,她要和模板树一致
        /// </summary>
        private void ResetDicNodeIndexs()
        {
            _dicNodeIndexs.Clear();
            foreach (TreeNode node in this.Nodes)
            {
                if (node is TmpltBaseTreeNode)
                {
                    SetElementNode(node as TmpltBaseTreeNode);
                    RepeatLoadTmpltTreeNodeIndex(node.Nodes);
                }
            }
        }
        private void RepeatLoadTmpltTreeNodeIndex(TreeNodeCollection nodeCollection)
        {
            foreach (TreeNode node in nodeCollection)
            {
                if (node is TmpltBaseTreeNode)
                {
                    SetElementNode(node as TmpltBaseTreeNode);
                    RepeatLoadTmpltTreeNodeIndex(node.Nodes);
                }
            }
        }

        #endregion

        /// <summary>
        /// 重新加载树上单个节点数据
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="anyXmlElement"></param>
        private void RepeatLoadSingleNodeData(TmpltBaseTreeNode treeNode,AnyXmlElement anyXmlElement)
        {//模板和页面片的单个节点刷新 即保存时调用,保存是才通知模板树

        }

    }
}


//没过滤后,树 清空,是不是索引也清空