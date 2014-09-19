using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using CustomControls;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MyTreeView : TreeViewEx
    {
        /// <summary>
        /// 最上面的“解决方案”节点，也是所有节点的根节点
        /// </summary>
        public SiteManagerNode SiteManagerNode { get; private set; }

        /// <summary>
        /// 不要“解决方案”节点时,取得根频道节点
        /// </summary>
        public RootChannelNode SelectTreeRootChanNode { get; private set; }

        /// <summary>
        /// .sdsite的文件路径
        /// </summary>
        public string SdsiteFilePath
        {
            get
            {
                if (Service.Sdsite.IsOpened)
                {
                    return Service.Sdsite.CurrentDocument.AbsoluteFilePath;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取是否显示所有文件
        /// </summary>
        public bool ShowAllFiles { get; set; }

        /// <summary>
        /// 树的应用,比如选择模板,选择频道时的筛选属性
        /// </summary>
        public override TreeMode TreeMode {get; set; }

        /// <summary>
        /// 新建页面时,选择模板,筛选模板类型
        /// </summary>
        public override PageType SelectTmpltType { get; set; }

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

        #region _dicNodeIndexs:通过id缓存的所有ElementNode节点

        /// <summary>
        /// 对所有ElementNode节点通过ID的一个索引  //by zhucai:是否需要把除ElementNode外的节点也放进来呢....
        /// </summary>
        private Dictionary<string, ElementNode> _dicNodeIndexs = new Dictionary<string,ElementNode>();

        public ElementNode GetElementNode(string id)
        {
            ElementNode elementNode = null;
            _dicNodeIndexs.TryGetValue(id,out elementNode);
            return elementNode;
        }

        public override void SetElementNode(BaseTreeNode elementNode)
        {
            _dicNodeIndexs[((ElementNode)elementNode).Element.Id] = (ElementNode)elementNode;
        }

        public override void RemoveElementNode(string id)
        {
            _dicNodeIndexs.Remove(id);
        }

        #endregion

        public MyTreeView()
        {
            MultiSelect = true;    //允许多选
            base.ShowRootLines = false;
            this.ShowNodeToolTips = true;
            this.ImageList = new ImageList();//ResourceService.MainImageList;
            this.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.ImageList.ImageSize = new Size(22, 16);

            //sdsite项目的打开和关闭事件
            Service.Sdsite.SdsiteOpened += new EventHandler(SdsiteXmlDocument_SdsiteOpened);
            Service.Sdsite.SdsiteClosing += new EventHandler(SdsiteXmlDocument_SdsiteClosing);

            this.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(MyTreeView_NodeMouseDoubleClick);
        }

        /// <summary>
        /// 双击打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (TreeMode == TreeMode.General)
            {
                OpenSubItem();
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            e.Node.Text = e.Label.Replace(" ", "");
            SdsiteXmlDocument sdsiteDoc = Service.Sdsite.CurrentDocument;

            if (((ElementNode)e.Node).Element.CanNewFileName(e.Label))
            {
                sdsiteDoc.ChangeTitle(((ElementNode)e.Node).Element.Id, e.Label);
            }
            else
            {
                e.CancelEdit = true;
                MessageService.Show("文件名重复或不合法！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RenameNode();
                return;
            }

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

        #region 文档事件加载与卸载

        void SdsiteXmlDocument_SdsiteOpened(object sender, EventArgs e)
        {
            ///右侧树才需要监听这些事件
            if (TreeMode == TreeMode.General)
            {
                SdsiteXmlDocument sdsite = Service.Sdsite.CurrentDocument;
                sdsite.ElementAdded += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAdded);
                sdsite.ElementDeleted += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementDeleted);
                sdsite.ElementAddedFavorite += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAddedFavorite);
                sdsite.ElementRemovedFavorite += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementRemovedFavorite);
                sdsite.ElementTitleChanged += new EventHandler<ChangeTitleEventArgs>(sdsite_ElementTitleChanged);
                sdsite.ElementMoved += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementMoved);
                sdsite.ElementExclude += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementExclude);
                sdsite.ElementInclude += new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementInclude);
            }
        }

        void SdsiteXmlDocument_SdsiteClosing(object sender, EventArgs e)
        {
            ///右侧树才需要监听这些事件
            if (TreeMode == TreeMode.General)
            {
                SdsiteXmlDocument sdsite = Service.Sdsite.CurrentDocument;
                if (sdsite != null)
                {
                    sdsite.ElementAdded -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAdded);
                    sdsite.ElementDeleted -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementDeleted);
                    sdsite.ElementAddedFavorite -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementAddedFavorite);
                    sdsite.ElementRemovedFavorite -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementRemovedFavorite);
                    sdsite.ElementTitleChanged -= new EventHandler<ChangeTitleEventArgs>(sdsite_ElementTitleChanged);
                    sdsite.ElementMoved -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementMoved);
                    sdsite.ElementExclude -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementExclude);
                    sdsite.ElementInclude -= new EventHandler<EventArgs<SimpleExIndexXmlElement>>(sdsite_ElementInclude);
                }
            }
        }

        #endregion

        #region 处理拖拽
        /// <summary>
        /// 在这里根据e的值判定是否执行拖拽的最终放置
        /// </summary>
        protected override void OnNodeDragDroping(DragDropNodeEventArgs e)
        {
            foreach (BaseTreeNode node in e.DragDropResult.DragDropNodes)
            {
                if (ProcessDragDropingIsCancel(node, e))
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnNodeDragDroping(e);
        }

        /// <summary>
        /// 此方法执行时已经完成树中的TreeNode的拖拽，应该在这里写真正的数据的移动
        /// </summary>
        protected override void OnNodeDragDroped(DragDropNodeEventArgs e)
        {
            foreach (BaseTreeNode node in e.DragDropResult.DragDropNodes)
            {
                ProcessDragDropedNode(node, e);
            }

            base.OnNodeDragDroped(e);
        }

        /// <summary>
        /// 拖拽结束，将要移动节点的预处理。返回值：是否取消拖拽
        /// </summary>
        /// <param name="srcNode"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        void ProcessDragDropedNode(BaseTreeNode srcNode, DragDropNodeEventArgs e)
        {
            BaseTreeNode putNode = e.DragDropResult.DropPutNode as BaseTreeNode;
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            SimpleExIndexXmlElement dragEle = ((ElementNode)srcNode).Element;
            SimpleExIndexXmlElement targetEle = null;
            if (e.DragDropResult.DropPutNode is DataNode)
            {
                targetEle = ((ElementNode)e.DragDropResult.DropPutNode).Element;
            }

            ///将要拖放到此节点内
            BaseTreeNode parentNode = null;
            if (e.DragDropResult.DropResultType == DragDropResultType.Into)
            {
                parentNode = (BaseTreeNode)e.DragDropResult.DropPutNode;
            }
            else
            {
                parentNode = (BaseTreeNode)e.DragDropResult.DropPutNode.Parent;
            }

            ///收藏夹的处理方式
            if (parentNode.NodeType == TreeNodeType.Favorite)
            {
                doc.AddFavorite(dragEle);
            }
            ///
            else
            {
                ///移动
                if ((e.DrgEvent.Effect & DragDropEffects.Move) != 0)
                {
                    ///处理XmlElement
                    bool result = doc.MoveNode(dragEle, targetEle, e.DragDropResult.DropResultType);

                    if (result)
                    {
                        ///处理树节点
                        srcNode.Parent.RemoveChildNodeTemp(srcNode);
                        switch (e.DragDropResult.DropResultType)
                        {
                            case DragDropResultType.Before:
                                putNode.Parent.AddChildNodeTemp(putNode.Index, srcNode);
                                break;
                            case DragDropResultType.After:
                                putNode.Parent.AddChildNodeTemp(putNode.Index + 1, srcNode);
                                break;
                            case DragDropResultType.Into:
                                putNode.AddChildNodeTemp(srcNode);
                                break;
                            default:
                                Debug.Assert(false);
                                break;
                        }
                    }
                }
                ///拷贝
                else if ((e.DrgEvent.Effect & DragDropEffects.Copy) != 0)
                {
                    doc.CopyNode(dragEle.Id, ((ElementNode)parentNode).Element.Id);
                }
            }
        }

        bool ProcessDragDropingIsCancel(BaseTreeNode srcNode, DragDropNodeEventArgs e)
        {
            BaseTreeNode putNode = e.DragDropResult.DropPutNode;
            ///若是拖拽到收藏夹中
            if (putNode is FavoriteRootNode)
            {
                return false;
            }

            //if (putNode is BaseFileElementNode)
            //{
            //    return true;
            //}


            ///判断重名或者生成文件名用的类型
            if (srcNode.Parent == putNode
                && e.DragDropResult.DropResultType == DragDropResultType.Into)//如果是拖动到自己父下面,则不执行操作
                return true;
            //检查在目标文件夹是否会重名 
           /* if (e.DragDropResult.DropResultType == DragDropResultType.Into
                && XmlUtilService.ExistsSameFileName(((ElementNode)putNode).Element as FolderXmlElement, ((ElementNode)srcNode).Element.DataType, srcNode.Text))
            {
                MessageService.Show("${res:Tree.msg.isExistsSameFile}");
                return true;
            }*/


            return false;
        }

        /// <summary>
        /// 通过此方法判定是否允许将srcNode放入targetNode节点，作为targetNode的子节点
        /// </summary>
        public override bool CanInto(BaseTreeNode srcNode, BaseTreeNode targetNode)
        {
            if (this.TreeMode != TreeMode.General)
            {
                return false;
            }

            if ((srcNode.NodeType == TreeNodeType.Tmplt || srcNode.NodeType == TreeNodeType.TmpltFolder)
                && (targetNode.NodeType == TreeNodeType.ChannelFolder || targetNode.NodeType == TreeNodeType.Channel)
                || (srcNode.NodeType == TreeNodeType.TmpltFolder && targetNode.NodeType == TreeNodeType.RootChannel)
                || (srcNode.NodeType == TreeNodeType.ResourceFolder && targetNode.NodeType == TreeNodeType.RootChannel))
            {
                return false;
            }

           /* if (!((baseSrcNode.NodeType == TreeNodeType.ResourceFolder || baseSrcNode.NodeType == TreeNodeType.ResourceFile)
                && (baseTargetNode.NodeType == TreeNodeType.ResourceFolder || baseTargetNode.NodeType == TreeNodeType.ResourceRoot)))
            {
                return false;
            }*/

            return base.CanInto(srcNode, targetNode);
        }

        #endregion

        #region 图标处理

        /// <summary>
        /// 存储扩展名对应的图标
        /// </summary>
        Dictionary<string, Image> _dicSystemFileIcon = new Dictionary<string, Image>();
        /// <summary>
        /// 通过扩展名获得系统的图标
        /// </summary>
        Image GetImageIndex(string extensionName)
        {
            string extension = extensionName;

            //无扩展名时特殊处理
            if (extension == "")
            {
                string strSign = "?noextension";

                if (_dicSystemFileIcon.ContainsKey(strSign))
                {
                    return _dicSystemFileIcon[strSign];
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(strSign, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(strSign, img);
                        return img;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            ///有扩展名
            else
            {
                ///在Dictionary里已经存在的就直接取Dictionary的
                if (_dicSystemFileIcon.ContainsKey(extension))
                {
                    return _dicSystemFileIcon[extension];
                }
                ///Dictionary里不存在，则调用api方法获得Icon并存入Dictionary中
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(extension, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(extension, img);
                        return img;
                    }
                    else
                    {
                        Debug.Fail(string.Format("获取指定文件类型:{0}的图标失败！",extension));
                        return null;
                    }
                }
            }
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            BaseTreeNode myNode = e.Node as BaseTreeNode;

            ///若ImageKey不等于CollapseImageKey，则认为它的图标正在表示特殊用途，那么不更换图标
            if (myNode.ImageKey == myNode.CollapseImageKey)
            {
                myNode.ImageKey = myNode.FactImageKey;
                myNode.SelectedImageKey = myNode.ImageKey;
            }
            base.OnAfterExpand(e);
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            BaseTreeNode myNode = e.Node as BaseTreeNode;

            ///若是SiteManagerNode(即根节点)折叠，则再次展开，即不允许其折叠
            if (myNode == SiteManagerNode)
            {
                SiteManagerNode.Expand();
                return;
            }

            ///若ImageKey不等于ExpandImageKey，则认为它的图标正在表示特殊用途，那么不更换图标
            if (myNode.ImageKey == myNode.ExpandImageKey)
            {
                myNode.ImageKey = myNode.FactImageKey;
                myNode.SelectedImageKey = myNode.ImageKey;
            }
            base.OnAfterCollapse(e);
        }

        #endregion

        #region 树的加载与释放

        /// <summary>
        /// 初始化树数据
        /// </summary>
        public void LoadTreeData()
        {
            this.BeginUpdate();

            ///只有右侧树才会加载_dicNodeIndexs
            if (TreeMode == TreeMode.General)
            {
                _dicNodeIndexs.Clear();
            }

            SdsiteXmlDocument sdsiteDoc = Service.Sdsite.CurrentDocument;

            ///添加TreeView的根节点:SiteManagerNode
            if (this.TreeMode == TreeMode.General)
            {
                SiteManagerNode = new SiteManagerNode(Path.GetDirectoryName(SdsiteFilePath));
                AddRootNodeToTree(SiteManagerNode);
                SiteManagerNode.LoadData();
                SiteManagerNode.Expand();

                this.ContextMenuStrip = TreeContextMenuStrip.CreateForTreeView(this);
            }
            else
            {
                SelectTreeRootChanNode = new RootChannelNode(Service.Sdsite.CurrentDocument.RootChannel);
                this.Nodes.Clear();
                this.Nodes.Add(SelectTreeRootChanNode);
                SelectTreeRootChanNode.LoadData();
            }
            this.EndUpdate();
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
        /// 添加根节点的内部方法
        /// </summary>
        private void AddRootNodeToTree(SiteManagerNode node)
        {
            ///清空节点
            this.Nodes.Clear();

            ///设置图标
            //node.SelectedImageKey = node.ImageKey = node.CollapseImageKey;

            ///添加到树中
            this.Nodes.Add(node);
        }

        #endregion

        #region 保存文件后对应树节点变化
        /// <summary>
        /// 文件元素设为移除收藏夹后对应树节点变化
        /// </summary>
        void sdsite_ElementRemovedFavorite(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            SiteManagerNode.FavoriteRootNode.RemoveFavoriteFile(e.Item.Id);
        }
        /// <summary>
        /// 文件元素设为到收藏夹后对应树节点变化
        /// </summary>
        void sdsite_ElementAddedFavorite(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ElementNode elementNode = GetElementNode(e.Item.Id);
            AddLinkNodeToFavorite(elementNode);
        }

        public override void AddLinkNodeToFavorite(BaseTreeNode elementNode)
        {
            LinkNode linkNode = new LinkNode((ElementNode)elementNode);
            linkNode.LoadData();
            SiteManagerNode.FavoriteRootNode.AddChildNode(linkNode);
            SiteManagerNode.FavoriteRootNode.Expand();
        }

        /// <summary>
        /// 更名
        /// </summary>
        void sdsite_ElementTitleChanged(object sender, ChangeTitleEventArgs e)
        {
            ElementNode elementNode = GetElementNode(e.Item.Id);
            elementNode.Text = e.Item.Title;
            elementNode.FilePath = e.Item.AbsoluteFilePath;

            //改名后页面标题栏需要同步改名
        }
        /// <summary>
        /// 元素的移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementMoved(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ///移动的节点
            ElementNode node = GetElementNode(e.Item.Id);

            ///通过Element的OwnerFolderElement找到更改后的父节点
            ElementNode nowParentNode = GetElementNode(e.Item.OwnerFolderElement.Id);

            ///在原位置删除节点
            node.Parent.RemoveChildNode(node);

            //todo:这里似乎还要考虑位置，而不应该全是放在最后
            ///在新位置添加节点
            nowParentNode.AddChildNode(node);
            node.FilePath = e.Item.AbsoluteFilePath;
        }
        /// <summary>
        /// 删除文件元素后对应树节点变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementDeleted(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ///先找原节点并删除
            BaseTreeNode node = this.GetElementNode(e.Item.Id);
            node.Parent.RemoveChildNode(node);
        }
        /// <summary>
        /// 添加文件元素后对应树节点变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementAdded(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ElementNode newNode = AddElementNode(e.Item);
            if (newNode != null)
            {
                this.CurrentNode = newNode;

                switch (newNode.Element.DataType)
                {
                    case DataType.Channel:
                    case DataType.Folder:
                        this.RenameNode();
                        break;
                }
            }
            //CurrentNode = newNode;
            //if (newNode is BaseFileElementNode)
            //{
            //    if (newNode.NodeType == TreeNodeType.Page)
            //    {
            //        switch (((PageNode)newNode).Element.PageType)
            //        {
            //            case PageType.General:
            //                Service.Workbench.OpenWorkDocument(WorkDocumentType.HtmlDesigner, newNode.Element.Id);
            //                break;
            //            case PageType.Product:
            //            case PageType.Project:
            //            case PageType.InviteBidding:
            //            case PageType.Knowledge:
            //            case PageType.Hr:
            //                Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit,e.Item.Id);// Service.Sdsite.CurrentDocument.RootChannel.Id);
            //                break;
            //            case PageType.Home:
            //                Service.Workbench.OpenWorkDocument(WorkDocumentType.HomePage, newNode.Element.Id);
            //                break;
            //        }
            //    }
            //    //todo:新增的页面,模板,资源文件后执行对应操作
            //}
            //else
            //{
            //    this.LabelEdit = true;
            //    RenameNode();
            //}
        }
        /// <summary>
        /// 节点包含到项目中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementInclude(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            BaseTreeNode eleNode = null;
            eleNode = this.GetNode(CurrentNode.FullPath);
            int eleIndex = eleNode.Index;

            BaseTreeNode parentNode = eleNode.Parent as BaseTreeNode;
            parentNode.RemoveChildNode(eleNode as BaseTreeNode);
            BaseTreeNode newNode = null;
            switch (e.Item.DataType)
            {
                case DataType.Channel:
                    newNode = new ChannelNode(e.Item as ChannelSimpleExXmlElement);
                    break;
                case DataType.Tmplt:
                    newNode = new TmpltNode(e.Item as TmpltSimpleExXmlElement);
                    break;
                case DataType.Page:
                    newNode =new PageNode(e.Item as PageSimpleExXmlElement);
                    break;
                case DataType.Folder:
                    {
                        switch (((FolderXmlElement)e.Item).FolderType)
                        {
                            case FolderType.ChannelFolder:
                                newNode = new ChannelFolderNode(e.Item as FolderXmlElement);
                                break;
                            case FolderType.TmpltFolder:
                                newNode = new TmpltFolderNode(e.Item as TmpltFolderXmlElement);
                                break;
                            case FolderType.ResourcesFolder:
                                newNode = new ResourceFolderNode(e.Item as FolderXmlElement);
                                break;
                        }
                        break;
                    }
                case DataType.File:
                    break;
            }
            //FileOutsideNode fileOutSideNode = new FileOutsideNode(e.Item.AbsoluteFilePath);
            parentNode.AddChildNode(newNode);
        }
        /// <summary>
        /// 节点排除出项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sdsite_ElementExclude(object sender, EventArgs<SimpleExIndexXmlElement> e)
        {
            ElementNode eleNode = GetElementNode(e.Item.Id);
            int eleIndex = eleNode.Index;

            BaseTreeNode parentNode = eleNode.Parent;
            parentNode.RemoveChildNode(eleNode);
            if (ShowAllFiles)
            {
                OutsideNode outSideNode = null;
                if (e.Item is FolderXmlElement)
                    outSideNode = new FileOutsideNode(e.Item.AbsoluteFilePath);
                else
                    outSideNode = new FolderOutsideNode(e.Item.AbsoluteFilePath);
                outSideNode.Text = e.Item.FileName;
                parentNode.Nodes.Insert(eleIndex, outSideNode);
            }
        }

        #endregion

        private ElementNode AddElementNode(SimpleExIndexXmlElement element)
        {
            ///找到新Element的父Element在树中的节点
            BaseFolderElementNode folderNode = (BaseFolderElementNode)GetElementNode(element.OwnerFolderElement.Id);

            ///添加此SimpleExIndexXmlElement
            return folderNode.AddElementNode(element);
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
                        DeleteSelectNodes();
                    }
                    else if (keyData == Keys.Enter)
                    {
                        OpenSubItem();
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void DeleteSelectNodes()
        {
            if (SelectedNodes.Count > 0)
            {
                ///先判断是否所有节点可以删除.有任何不可以删除的节点则不执行任何删除操作
                foreach (BaseTreeNode node in this.SelectedNodes)
                {
                    if (!node.CanDelete)
                    {
                        return;
                    }
                }

                ///所有节点都可以删除,那么真正执行删除命令
                if (MessageService.Show("${res:Tree.msg.delete}", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BaseTreeNode[] treeNodes = this.SelectedNodes.ToArray();
                    BaseTreeNode nextSelectNode = (BaseTreeNode)treeNodes[0].PrevNode;
                    
                    foreach (ElementNode node in treeNodes)
                    {
                        bool result = Service.Sdsite.CurrentDocument.DeleteItem(node.Element);

                        ///为false则表示删除失败。
                        if (!result)
                        {
                            nextSelectNode = node;
                            break;
                        }
                    }
                    this.CurrentNode = (BaseTreeNode)nextSelectNode;
                    this.Select();
                    this.Focus();
                }
            }
        }

        #region 获取或设置树中项目的打开状态

        /// <summary>
        /// 获取或设置树中项目的打开状态
        /// </summary>
        public string[] OpenItems
        {
            get
            {
                List<string> _openItems = new List<string>();
                foreach (BaseTreeNode node in this.Nodes)
                {
                    if (node.IsExpanded && node is ElementNode)
                        _openItems.Add(((ElementNode)node).Element.Id);
                    GetOpenItems(node, _openItems);
                }
                return _openItems.ToArray();
            }
            set
            {
                foreach (string id in value)
                {
                    ElementNode elementNode;
                    if (_dicNodeIndexs.TryGetValue(id, out elementNode))
                    {
                        elementNode.Expand();
                    }
                }
            }
        }

        void GetOpenItems(BaseTreeNode treeNode, List<string> _openItems)
        {
            foreach (BaseTreeNode node in treeNode.Nodes)
            {
                if (node.IsExpanded && node is ElementNode)
                    _openItems.Add(((ElementNode)node).Element.Id);
                GetOpenItems(node, _openItems);
            }
        }
        #endregion

        #region 包含,排除项目
        public void IncludeItem()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            foreach (BaseTreeNode node in SelectedNodes)
            {
                if (node is FileOutsideNode)
                {
                    doc.IncludeItem(Utility.File.GetXmlDocumentId(((OutsideNode)node).FilePath));
                }
                else if (node is FolderOutsideNode)
                {
                    doc.IncludeItem(Utility.File.GetXmlDocumentId(((OutsideNode)node).FilePath));
                }

            }
        }

        public void ExcludeItem()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            foreach (BaseTreeNode node in SelectedNodes)
            {
                if (node is ElementNode && !((ElementNode)node).Element.IsExclude)
                {
                    ElementNode dataNode = node as ElementNode;
                    doc.ExcludeItem(dataNode.Element.Id);
                }
            }
        }

        #endregion

        #region 新增逻辑
        /// <summary>
        /// 新增频道
        /// </summary>
        /// <param name="m_tree"></param>
        public void NewChannel()
        {
            string title = XmlUtilService.CreateIncreaseChannelTitle(((BaseFolderElementNode)CurrentNode).Element as FolderXmlElement);

            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            BaseFolderElementNode parentNode = (BaseFolderElementNode)this.CurrentNode;
            doc.CreateChannel(parentNode.Element.Id, title);
        }

        /// <summary>
        /// 新建页面
        /// </summary>
        /// <param name="m_tree"></param>
        /// <param name="myType"></param>
        public void NewPage(FolderXmlElement parentEle, PageType myType)
        {
            if (myType != PageType.Home)
            {
                NewPageForm newPage = new NewPageForm(parentEle, myType);
                if (newPage.ShowDialog() == DialogResult.OK)
                {
                    ///建完页面后打开页面
                    WorkDocumentType workDocumentType = WorkDocumentType.None;
                    if (myType == PageType.General)
                    {
                        workDocumentType = WorkDocumentType.HtmlDesigner;
                    }
                    else
                    {
                        workDocumentType = WorkDocumentType.Edit;
                    }
                    Service.Workbench.OpenWorkDocument(workDocumentType, newPage.NewPageId);
                }
            }
            else
            {
                NewHomePageForm newPage = new NewHomePageForm(parentEle, myType);
                if (newPage.ShowDialog() == DialogResult.OK)
                {
                    ///建完页面后打开页面
                    Service.Workbench.OpenWorkDocument(WorkDocumentType.HomePage, newPage.NewPageId);
                }
            }
        }

        /// <summary>
        /// 新建模板
        /// </summary>
        /// <param name="m_tree"></param>
        /// <param name="myType"></param>
        public void NewTmplt(FolderXmlElement parentEle, TmpltType myType)
        {
            //获得父频道节点,父模板文件夹节点
            //((BaseFolderElementNode)CurrentNode).Element,
            NewTmpltSetupForm tmpltForm = new NewTmpltSetupForm(parentEle, myType);
            if (tmpltForm.ShowDialog() == DialogResult.OK)
            {
                TmpltSimpleExXmlElement tmpltEle = Service.Sdsite.CurrentDocument.CreateTmplt(parentEle, myType, tmpltForm.TmpltTitle,
                    false, tmpltForm.TmpltWidth, tmpltForm.TmpltHeight, tmpltForm.BackImage);
                Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, tmpltEle.Id);
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="m_tree"></param>
        internal void NewFolder()
        {
            string title = XmlUtilService.CreateIncreaseFolderTitle(((BaseFolderElementNode)CurrentNode).Element as FolderXmlElement); 
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            BaseFolderElementNode parentNode = (BaseFolderElementNode)CurrentNode;
            doc.CreateFolder(parentNode.Element.Id, title);
        }

        /// <summary>
        /// 新建主页
        /// </summary>
        /// <param name="m_tree"></param>
        internal void NewHome()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            BaseFolderElementNode parentNode = (BaseFolderElementNode)CurrentNode;
            KeyValuePair<string, string> myPair = doc.CreateHome(parentNode.Element, XmlUtilService.CreateIncreasePageTitle(((BaseFolderElementNode)CurrentNode).Element as FolderXmlElement,PageType.Home));
            Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, myPair.Key);
        }
        #endregion

        #region 收藏夹逻辑
        /// <summary>
        /// 清空收藏夹
        /// </summary>
        public void ClearFavorite()
        {
            if (MessageService.Show("您确定清空收藏夹吗？", MessageBoxButtons.OKCancel)
                == DialogResult.Cancel)
            {
                return;
            }
            this.BeginUpdate();

            TreeNode[] linkNodes = new TreeNode[this.SiteManagerNode.FavoriteRootNode.Nodes.Count];
            this.SiteManagerNode.FavoriteRootNode.Nodes.CopyTo(linkNodes,0);
            foreach (LinkNode linkNode in linkNodes)
            {
                Service.Sdsite.CurrentDocument.RemoveFavorite(linkNode.TargetNode.Element);
            }
            this.EndUpdate();
        }

        /// <summary>
        /// 加入收藏夹
        /// </summary>
        /// <param name="m_tree"></param>
        public void AddToFavorite()
        {
            this.BeginUpdate();
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            foreach (BaseTreeNode node in SelectedNodes)
            {
                if (!IsInFavorite(node))
                {
                    ElementNode dataNode = node as ElementNode;
                    doc.AddFavorite(dataNode.Element.Id);
                }
            }
            this.EndUpdate();
        }

        /// <summary>
        /// 移出收藏夹
        /// </summary>
        /// <param name="addTreeNodeAdvs">待移出的节点</param>
        public void RemoveFromFavorite()
        {
            this.BeginUpdate();
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            BaseTreeNode favoriteNode = SiteManagerNode.FavoriteRootNode;//收藏夹根节点
            BaseTreeNode[] arrSelectedNode = this.SelectedNodes.ToArray();
            foreach (BaseTreeNode node in arrSelectedNode)
            {
                doc.RemoveFavorite(((ElementNode)node).Element.Id);
            }
            this.EndUpdate();
        }

        /// <summary>
        /// 检验是否已经加入收藏夹
        /// </summary>
        /// <param name="addNode"></param>
        /// <param name="favoriteNodeList"></param>
        /// <returns></returns>
        public bool IsInFavorite(BaseTreeNode addNode)
        {
            foreach (BaseTreeNode node in SiteManagerNode.FavoriteRootNode.Nodes)
            {
                if (((ElementNode)node).Element.Id != ((ElementNode)addNode).Element.Id)
                    continue;
                else
                    return true;
            }
            return false;
        }
        #endregion

        #region 其他处理
        /// <summary>
        /// 转到文件
        /// </summary>
        public void GotoFile()
        {
            ElementNode currentNode = CurrentNode as ElementNode;
            if (currentNode != null)
            {
                string filePath = currentNode.Element.AbsoluteFilePath;
                Process.Start("explorer", "/select," + filePath);
            }
        }
        /// <summary>
        /// 页面选择模板
        /// </summary>
        public void SelectTmpltForm(PageType pageType)
        {
            SelectTmpltForm form = new SelectTmpltForm(pageType);

            ///找到原本的模板Id
            string tmpltId = ((PageNode)this.CurrentNode).Element.TmpltId;
            foreach (PageNode node in this.SelectedNodes)
            {
                if (node.Element.TmpltId != tmpltId)
                {
                    tmpltId = null;
                }
            }
            form.SelectTmpltId = tmpltId;

            ///显示Form，选择模板id
            if (form.ShowDialog() == DialogResult.OK)
            {
                ///Form选择完成，关联所有选中的节点
                foreach (BaseTreeNode node in this.SelectedNodes)
	            {
                    if (node.NodeType == TreeNodeType.Page)
                    {
                        ((PageNode)node).Element.TmpltId = form.SelectTmpltId;
                    }
                }
            }
        }

        /// <summary>
        /// 导入资源文件
        /// </summary>
        public  void ImportResource()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FormOpenFileDialog OpenFile = new FormOpenFileDialog();
            OpenFile.OpenDialog.Multiselect = true;
            OpenFile.OpenDialog.Filter = "PIC files (*.jpg,*.png,*.gif)|*.jpg;*.png;*.gif;|Flash files (.swf)|*.swf|Media files (*.rmvb,*.rm,*.avi,*.wmv)|*.rmvb;*.rm;*.avi;*.wmv|Audio files (*.mp3,*.mdi,*.wma,*.wav)|*.mp3;*.mid;*.wma;*.wav";
            
            BaseFolderElementNode pathNode = CurrentNode as BaseFolderElementNode;
            string parentFolderID = ((ElementNode)CurrentNode).Element.Id;
            
            if (OpenFile.ShowDialog(this) == DialogResult.OK)
            {
                string[] fileNames = OpenFile.OpenDialog.FileNames;
                foreach (string srcFilePath in fileNames)
                {
                    FileSimpleExXmlElement fileEle = doc.CreateFileElement(parentFolderID, srcFilePath);
                }
            }
        }
        /// <summary>
        /// 将模板复制为其他类型的模板
        /// </summary>
        /// <param name="copyEle"></param>
        public void CopyTmpltToOtherType(TmpltSimpleExXmlElement copyEle)
        {
            CopyTmpltOherTypeForm copyTmplt = new CopyTmpltOherTypeForm(copyEle);
            copyTmplt.ShowDialog();
        }
        /// <summary>
        /// 设为首页
        /// </summary>
        /// <param name="myTree"></param>
        internal void SetAsIndexPage()
        {
            ///先将之前的默认页的Node的字体恢复普通
            PageNode defaultPageNode = ((ChannelFolderNode)this.CurrentNode.Parent).GetDefaultPageNode();
            if (defaultPageNode != null)
            {
                defaultPageNode.NodeFont = null;
            }

            ///将当前选择的页面的Node加粗
            CurrentNode.BoldFont();

            ///设置默认页
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;
            FolderXmlElement folderEle = ((BaseFolderElementNode)CurrentNode.Parent).Element;
            string pageID = ((PageNode)CurrentNode).Element.Id;
            folderEle.DefaultPageId = pageID;
            doc.Save();
        }
        #endregion

        #region 打开
        public void OpenSubItem()
        {
            ElementNode eleNode = CurrentNode as ElementNode;
            if (CurrentNode.NodeType == TreeNodeType.Link)
            {
                eleNode = ((LinkNode)CurrentNode).TargetNode;
            }
            if (eleNode != null)
            {
                switch (eleNode.NodeType)
                {
                    case TreeNodeType.Page:
                        {
                            switch (((PageNode)eleNode).Element.PageType)
                            {
                                case PageType.General:
                                    Service.Workbench.OpenWorkDocument(WorkDocumentType.HtmlDesigner, eleNode.Element.Id);
                                    break;
                                case PageType.Product:
                                case PageType.Project:
                                case PageType.InviteBidding:
                                case PageType.Knowledge:
                                case PageType.Hr:
                                    Service.Workbench.OpenWorkDocument(WorkDocumentType.Edit, eleNode.Element.Id);
                                    break;
                                case PageType.Home:
                                    Service.Workbench.OpenWorkDocument(WorkDocumentType.HomePage, eleNode.Element.Id);
                                    break;
                                default:
                                    Debug.Fail("");
                                    break;
                            }
                            break;
                        }
                    case TreeNodeType.Tmplt:
                        {
                            Service.Workbench.OpenWorkDocument(WorkDocumentType.TmpltDesigner, eleNode.Element.Id);
                            break;
                        }
                    case TreeNodeType.ResourceFile:
                        {
                            string fullPath = ((ElementNode)eleNode).Element.AbsoluteFilePath;
                            Process p = Process.Start(fullPath);
                            if (p != null)
                                p.Dispose();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 复制,剪切,粘贴
        DataNode cNode = null;
        int cutCopyType = 0;
        /// <summary>
        /// 1复制,2剪切,0不做
        /// </summary>
        public int CutCopyType
        {
            get { return cutCopyType; }
            set { cutCopyType = value; }
        }

        public DataNode CNode
        {
            get { return cNode; }
            set { cNode = value; }
        }

        public void CopyNode()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;

            if (CanInto(CNode, CurrentNode))
            {
                if (CNode is ElementNode)
                {
                    doc.CopyNode(((ElementNode)CNode).Element.Id, ((ElementNode)CurrentNode).Element.Id);
                }
            }
        }

        public void CutNode()
        {
            SdsiteXmlDocument doc = Service.Sdsite.CurrentDocument;

            if (CanInto(CNode, CurrentNode))
            {
                if (CNode is ElementNode)
                {
                    doc.CutNode(((ElementNode)CNode).Element.Id, ((ElementNode)CurrentNode).Element.Id);
                }
            }
        }
        #endregion 
    }
}
