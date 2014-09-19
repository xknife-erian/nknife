using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class TreeViewEx : BaseTreeView
    {
        #region 字段与属性

        /// <summary>
        /// 拖拽中，悬停导致展开节点的时间
        /// </summary>
        const int DropBranchTick = 1000;
        const int WM_PAINT = 0x000F;
        const int InvalidateOffset = 5;
        const int LineLength = 20;
        const int RectHeight = 8;
        const int ScrollOffset = 10;

        /// <summary>
        /// 拖拽中，悬停导致展开的计时器
        /// </summary>
        Timer _branchNodeBerth = new Timer();

        //用来处理重命名
        private const int WM_TIMER = 0x0113;
        private bool _triggerLabelEdit = false;
        private bool _preLabelEditForMouseUp = false;
        private bool _wasDoubleClick = false;
        private string viewedLabel;
        private string editedLabel;

        //用来处理拖拽
        bool _isMouseDown;
        bool _isDragDrop;
        BaseTreeNode[] _dropNodes;
        Point _beginPoint;
        Point _lineBeginPoint;
        Point _lineEndPoint;
        bool _canOrder = true;
        int _insertLineWidth = 100;
        BaseTreeNode _dropResultNode;
        DragDropResultType _dropResultType;
        BaseTreeNode _tempSelectNode;
        public bool CanOrder
        {
            get { return _canOrder; }
            set { _canOrder = value; }
        }

        /// <summary>
        /// 否决的。隐藏SelectedNode属性。
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Bindable(false), Browsable(false)]
        new public TreeNode SelectedNode
        {
            get
            {
                return base.SelectedNode;
            }
            set
            {
                base.SelectedNode = value;
            }
        }

        //用来处理可支持多选
        private SelectedNodeCollection _SelectedNodes = new SelectedNodeCollection();
        /// <summary>
        /// 选择的节点集
        /// </summary>
        public SelectedNodeCollection SelectedNodes
        {
            get { return _SelectedNodes; }
        }

        public event EventHandler SelectedNodesChanged;
        protected virtual void OnSelectedNodesChanged(EventArgs e)
        {
            if (SelectedNodesChanged != null)
            {
                SelectedNodesChanged(this, e);
            }
        }

        private bool _multiSelect;
        /// <summary>
        /// 获取或设置是否多选
        /// </summary>
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }
        private BaseTreeNode _currentNode;
        public BaseTreeNode CurrentNode
        {
            get
            {
                if (MultiSelect)
                {
                    return _currentNode;
                }
                else
                {
                   //return (BaseTreeNode)this.SelectedNode;
                   return _currentNode;

                }
            }
            set
            {
                if (MultiSelect)
                {
                    SetCurrentNode(value);

                    if (MultiSelect)
                    {
                        if (value != null)
                        {
                            if (!this.SelectedNodes.Contains(value))
                            {
                                ///添加
                                SelectedNodes.Add(value);
                            }

                            ///清除其他
                            this.SelectedNodes.ClearWithout(value);

                            value.EnsureVisible();
                        }
                        else
                        {
                            this.SelectedNodes.Clear();
                        }
                    }
                }
                else
                {
                    SetCurrentNode(value);

                    this.SelectedNodes.ClearWithout(value);
                }
            }
        }

        #region 虚属性

        public virtual TreeMode TreeMode { get; set; }
        public virtual PageType SelectTmpltType { get; set; }

        #endregion

        #endregion

        public TreeViewEx()
        {
            this.ShowLines = true;
            this.AllowDrop = true;
            this.LabelEdit = false;
            this.HideSelection = false;
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.OptimizedDoubleBuffer, false);

            _branchNodeBerth.Interval = DropBranchTick;
            _branchNodeBerth.Tick += new EventHandler(_branchNodeBerth_Tick);

            ///处理可多选
            SelectedNodes.Inserted += new EventHandler<EventArgs<BaseTreeNode>>(SelectedNodes_Inserted);
            SelectedNodes.Removed += new EventHandler<EventArgs<BaseTreeNode>>(SelectedNodes_Removed);
        }

        #region 虚方法
        public virtual void SetElementNode(BaseTreeNode elementNode)
        {
        }
        public virtual void RemoveElementNode(string id)
        {
        }
        public virtual void AddLinkNodeToFavorite(BaseTreeNode elementNode)
        {
        }
        #endregion

        #region 内部方法

        /// <summary>
        /// 拖拽中，悬停导致展开的处理函数
        /// </summary>
        void _branchNodeBerth_Tick(object sender, EventArgs e)
        {
            //将此分枝节点展开
            Point mousePoint = PointToClient(Control.MousePosition);
            BaseTreeNode node = (BaseTreeNode)this.GetNodeAt(mousePoint);
            if (_branchNodeBerth.Tag == node && GetDragDropResult() == DragDropResultType.Into)
            {
                InvalidatePrevLine(false);
                node.Expand();
                RefreshDragState();
            }

            _branchNodeBerth.Stop();
            _branchNodeBerth.Tag = null;
        }

        void SelectedNodes_Removed(object sender, EventArgs<BaseTreeNode> e)
        {
            LowlightNode(e.Item);

            ///如果删除的是CurrentNode，则改变CurrentNode节点
            if (CurrentNode == e.Item)
            {
                if (SelectedNodes.Count > 0)
                {
                    SetCurrentNode(SelectedNodes[0]);
                }
                else
                {
                    SetCurrentNode(null);
                }
            }

            OnSelectedNodesChanged(EventArgs.Empty);
        }

        void SelectedNodes_Inserted(object sender, EventArgs<BaseTreeNode> e)
        {
            HighlightNode(e.Item);

            ///如果添加的时候CurrentNode不存在，则为其赋值
            if (CurrentNode == null)
            {
                SetCurrentNode(e.Item);
            }

            OnSelectedNodesChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 使上一次画的线无效
        /// </summary>
        void InvalidatePrevLine(bool isCancelSelected)
        {
            ///停止悬停导致展开的计时器
            _branchNodeBerth.Stop();
            _branchNodeBerth.Tag = null;

            ///取消临时选择
            if (isCancelSelected && _tempSelectNode != null)
            {
                this.SelectedNodes.Remove(_tempSelectNode);
                _tempSelectNode = null;
            }

            ///使上次的画线区域无效
            if (_isDragDrop && _canOrder)
            {
                Invalidate(new Rectangle(_lineBeginPoint.X - InvalidateOffset, _lineBeginPoint.Y - InvalidateOffset,
                    _lineEndPoint.X + InvalidateOffset * 2, InvalidateOffset * 2));
            }
        }

        /// <summary>
        /// Hit检测结果是否在TreeNode上(目前只有在图片或文字上才会返回true)
        /// </summary>
        bool HitIsOnNode(TreeViewHitTestLocations location)
        {
            return (location & TreeViewHitTestLocations.Image) == TreeViewHitTestLocations.Image
                || (location & TreeViewHitTestLocations.Label) == TreeViewHitTestLocations.Label;
        }

        /// <summary>
        /// 画拖拽过程中的插入线
        /// </summary>
        void DrawInsertLine(Graphics g, int x, int y, int width)
        {
            g.DrawLine(Pens.Black, new Point(x, y), new Point(x + LineLength, y));
            g.DrawRectangle(Pens.Black, new Rectangle(x + LineLength, y - (RectHeight / 2), width, RectHeight));
            g.FillRectangle(new SolidBrush(Color.FromArgb(40, 100, 0, 255)), new Rectangle(x + LineLength, y - (RectHeight / 2), width, RectHeight));
        }

        /// <summary>
        /// 获取鼠标当前位置的拖拽结果
        /// </summary>
        DragDropResultType GetDragDropResult()
        {
            ///找到鼠标当前位置的TreeNode
            Point mousePosition = PointToClient(Control.MousePosition);
            BaseTreeNode mouseNode = (BaseTreeNode)this.GetNodeAt(mousePosition);

            ///鼠标当前位置没有TreeNode，不允许拖拽
            if (mouseNode == null)
            {
                return DragDropResultType.None;
            }
            else if (Array.IndexOf<BaseTreeNode>(_dropNodes, mouseNode) >= 0)
            {
                ///当前位置的TreeNode就是本身，那么当成Into
                return DragDropResultType.Self;
            }
            else
            {
                ///不可以拖拽到其子节点
                if (CheckNodeParent(mouseNode, _dropNodes))
                {
                    return DragDropResultType.None;
                }
            }

            ///找到鼠标位置节点的父节点
            BaseTreeNode mouseParentNode = (BaseTreeNode)mouseNode.Parent;

            ///为空表示是顶级节点，不允许拖拽
            if (mouseParentNode == null)
            {
                return DragDropResultType.None;
            }

            ///检查是否在不允许排序的节点内
            bool canNotDragPut = NodeIsInNoOrder(mouseNode);
            if (canNotDragPut)
            {
                return DragDropResultType.None;
            }

            ///获取是否可以Into
            bool canInto = CanInto(_dropNodes, mouseNode);
            bool canIntoParent = CanInto(_dropNodes, mouseParentNode);

            ///不能Into到父
            if (!canIntoParent)
            {
                ///检查是否可以Into到子
                if (canInto)
                {
                    return DragDropResultType.Into;
                }
                return DragDropResultType.None;
            }
            ///可以Into到子
            else if (canInto)
            {
                if (mouseNode.Bounds.Y + (mouseNode.Bounds.Height / 4) > mousePosition.Y)
                {
                    return DragDropResultType.Before;
                }
                else if (mouseNode.Bounds.Y + (mouseNode.Bounds.Height / 4) * 3 > mousePosition.Y)
                {
                    return DragDropResultType.Into;
                }
                else
                {
                    return DragDropResultType.After;
                }
            }
            ///不可以Into到子
            else
            {
                bool isBefore = mouseNode.Bounds.Y + (mouseNode.Bounds.Height / 2) > mousePosition.Y;
                DragDropResultType dropType = isBefore ? DragDropResultType.Before : DragDropResultType.After;
                return dropType;
            }
        }

        /// <summary>
        /// 刷新拖拽过程中的状态(主要是画分隔符或选中节点)
        /// </summary>
        void RefreshDragState()
        {
            try
            {
                ///找到鼠标当前位置的TreeNode
                Point mousePosition = PointToClient(Control.MousePosition);
                BaseTreeNode mouseNode = (BaseTreeNode)this.GetNodeAt(mousePosition);

                ///鼠标当前位置没有TreeNode，那么认为在最后一个TreeNode之后
                if (mouseNode == null)
                {
                    InvalidatePrevLine(true);
                    return;
                }

                if (CanOrder)
                {
                    DragDropResultType dropType = GetDragDropResult();

                    ///根据鼠标位置是否到达边界，来判断是否需要滚动
                    if ((mousePosition.Y >= ClientRectangle.Bottom - ScrollOffset) && (mousePosition.Y <= ClientRectangle.Bottom))
                    {
                        InvalidatePrevLine(false);
                        Utility.DllImport.ScrollTreeViewLineDown(this);
                    }
                    else if ((mousePosition.Y >= ClientRectangle.Top) && (mousePosition.Y <= ClientRectangle.Top + ScrollOffset))
                    {
                        InvalidatePrevLine(false);
                        Utility.DllImport.ScrollTreeViewLineUp(this);
                    }
                    else
                    {
                        ///如果处于Into状态，则根据停放时间将其展开
                        if ((!mouseNode.IsExpanded) &&
                            dropType == DragDropResultType.Into && mouseNode.IsDockExpand)
                        {
                            if (_branchNodeBerth.Tag != mouseNode)
                            {
                                _branchNodeBerth.Stop();

                                _branchNodeBerth.Tag = mouseNode;
                                _branchNodeBerth.Start();
                            }
                        }

                        ///如果获取结果跟上次不一样，则重绘
                        if (mouseNode != _dropResultNode || dropType != _dropResultType
                            || (_tempSelectNode == null && dropType == DragDropResultType.Into))
                        {
                            //使鼠标附近的区域无效
                            InvalidatePrevLine(true);

                            ///如果这次是Into，则选择
                            if (dropType == DragDropResultType.Into)
                            {
                                _tempSelectNode = mouseNode;
                                SelectedNodes.Add(mouseNode);
                            }
                        }
                    }
                }
                else
                {
                    #region 不排序的情况

                    this.SelectedNode = mouseNode;

                    ///如果是枝节点，则根据停放时间将其展开
                    if ((!mouseNode.IsExpanded) && mouseNode.IsBranch)
                    {
                        if (_branchNodeBerth.Tag != mouseNode)
                        {
                            _branchNodeBerth.Stop();

                            _branchNodeBerth.Tag = mouseNode;
                            _branchNodeBerth.Start();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Service.Exception.ShowException(ex);
            }
        }

        /// <summary>
        /// 判定指定节点是否在不排序的节点内（不排序则也表示不允许在里面拖拽）
        /// </summary>
        bool NodeIsInNoOrder(BaseTreeNode node)
        {
            BaseTreeNode parentNode = node;
            while (parentNode.Parent != null)
            {
                parentNode = parentNode.Parent;
                if (parentNode.IsGrandchildNoOrder)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region override重写的方法

        protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //检查鼠标是否在TreeNode上
                TreeViewHitTestInfo testInfo = this.HitTest(e.Location);
                if ((testInfo.Node == this.CurrentNode)
                    && (testInfo.Location == TreeViewHitTestLocations.Label
                    || testInfo.Location == TreeViewHitTestLocations.Image))
                {
                    base.OnNodeMouseDoubleClick(e);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                OnPaint(new PaintEventArgs(this.CreateGraphics(), this.RectangleToScreen(this.ClientRectangle)));
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            try
            {
                //拖拽结束
                InvalidatePrevLine(true);

                ///获取拖拽结果
                _dropResultNode = (BaseTreeNode)this.GetNodeAt(PointToClient(Control.MousePosition));

                ///鼠标当前位置没有TreeNode，那么认为在最后一个之后
                if (_dropResultNode == null)
                {
                    _dropResultNode = (BaseTreeNode)this.Nodes[this.Nodes.Count - 1];
                }

                ///恢复一些辅助变量为初始化状态
                _isDragDrop = false;
                _beginPoint = Point.Empty;

                ///判断目标节点是否不是选择节点；或者不在选择节点的子节点内；
                /// 
                ///注：(by zhucai)判断一个一个节点是否是另一节点的孙节点不能通过node.FullPath.StartWith的方式，
                ///因为可能出现上级节点名刚好等于当前节点祖节点的前面部分。
                if (Array.IndexOf(_dropNodes, _dropResultNode) == -1 && !CheckNodeParent(_dropResultNode, _dropNodes))
                {
                    //触发NodeDragDroping事件
                    DragDropNodeEventArgs ddn = new DragDropNodeEventArgs(drgevent, new DragDropResult(_dropNodes, _dropResultNode, GetDragDropResult()));

                    OnNodeDragDroping(ddn);

                    if (!ddn.Cancel)
                    {
                        /*
                        ///先从树里删除
                        _dropNode.Remove();

                        ///计算该插入的位置
                        int insertIndex = 0;
                        TreeNode parentNode = null;
                        switch (ddn.DragDropResult.DropType)
                        {
                            case DragDropResultType.Before:
                                parentNode = ddn.DragDropResult.DropPutNode.Parent;
                                insertIndex = ddn.DragDropResult.DropPutNode.Index;
                                break;
                            case DragDropResultType.After:
                                parentNode = ddn.DragDropResult.DropPutNode.Parent;
                                insertIndex = ddn.DragDropResult.DropPutNode.Index + 1;
                                break;
                            case DragDropResultType.Into:
                                parentNode = ddn.DragDropResult.DropPutNode;
                                insertIndex = parentNode.Nodes.Count;
                                break;
                            default:
                                Debug.Assert(false);
                                break;
                        }

                        ///插入树中
                        if (parentNode != null)
                        {
                            parentNode.Nodes.Insert(insertIndex, (BaseTreeNode)_dropNode);
                        }
                        else
                        {
                            this.Nodes.Insert(insertIndex, (BaseTreeNode)_dropNode);
                        }
                        */
                        ///触发NodeDragDroped事件
                        OnNodeDragDroped(ddn);
                    }
                }

                //this.SelectedNodes.AddRange(_dropNodes);
            }
            catch (Exception ex)
            {
                Service.Exception.ShowException(ex);
            }
            finally
            {
            }

            base.OnDragDrop(drgevent);
        }

        protected override void OnDragLeave(EventArgs e)
        {
            //拖拽出去
            InvalidatePrevLine(true);
            _isDragDrop = false;

            base.OnDragLeave(e);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            //拖拽进来
            _isDragDrop = true;

            if (_dropNodes != null && _dropNodes.Length > 0)
            {
                if (drgevent.Data.GetDataPresent(_dropNodes.GetType()))
                {
                    drgevent.Effect = DragDropEffects.All;
                }
            }

            base.OnDragEnter(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (GetDragDropResult() == DragDropResultType.None)
            {
                drgevent.Effect = DragDropEffects.None;
            }
            else
            {
                ///找到鼠标当前位置的TreeNode
                Point mousePosition = PointToClient(Control.MousePosition);
                BaseTreeNode mouseNode = (BaseTreeNode)this.GetNodeAt(mousePosition);

                ///鼠标当前位置没有TreeNode，那么认为在最后一个TreeNode之后
                if (mouseNode != null)
                {
                    DragDropEffects effects = mouseNode.AcceptEffects;
                    drgevent.Effect = effects & drgevent.AllowedEffect;
                }
            }

            base.OnDragOver(drgevent);
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            RefreshDragState();

            base.OnGiveFeedback(gfbevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //如果其顺序有效，且在拖动，则在拖拽处画条横线，指示插入点
            if (CanOrder && _isDragDrop)
            {
                Point mousePosition = PointToClient(Control.MousePosition);
                BaseTreeNode mouseNode = (BaseTreeNode)this.GetNodeAt(mousePosition);
                bool mouseNodeIsNull = false;

                ///鼠标当前位置没有TreeNode，那么认为鼠标在最后一个TreeNode之后
                if (mouseNode == null)
                {
                    mouseNodeIsNull = true;
                    mouseNode = (BaseTreeNode)this.Nodes[this.Nodes.Count - 1];
                }

                if (!mouseNodeIsNull)
                {
                    ///检测拖拽结果
                    DragDropResultType dropResultType = GetDragDropResult();

                    if (mouseNode != _dropResultNode || dropResultType != _dropResultType)
                    {
                        switch (dropResultType)
                        {
                            ///鼠标在目标节点之前
                            case DragDropResultType.Before:
                                {
                                    _lineBeginPoint = new Point(mouseNode.Bounds.X - 20, mouseNode.Bounds.Y);
                                    _lineEndPoint = new Point(mouseNode.Bounds.X + _insertLineWidth, mouseNode.Bounds.Y);

                                    DrawInsertLine(e.Graphics, _lineBeginPoint.X, _lineBeginPoint.Y, CurrentNode.Bounds.Width);
                                    break;
                                }
                            ///鼠标在目标节点之后
                            case DragDropResultType.After:
                                {
                                    _lineBeginPoint = new Point(mouseNode.Bounds.X - 20, mouseNode.Bounds.Y + mouseNode.Bounds.Height);
                                    _lineEndPoint = new Point(mouseNode.Bounds.X + _insertLineWidth, mouseNode.Bounds.Y + mouseNode.Bounds.Height);

                                    DrawInsertLine(e.Graphics, _lineBeginPoint.X, _lineBeginPoint.Y, CurrentNode.Bounds.Width);
                                    break;
                                }
                        }

                        ///只有在线画完后才将当前值存储起来
                        _dropResultType = dropResultType;
                        _dropResultNode = mouseNode;
                    }
                }
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// 通过此方法判定是否允许将srcNode放入targetNode节点，作为targetNode的子节点
        /// </summary>
        public virtual bool CanInto(BaseTreeNode srcNode, BaseTreeNode targetNode)
        {
            ///判断目标节点是枝节点
            if (!targetNode.IsBranch)
            {
                return false;
            }

            ///根据接收类型判定
            TreeNodeType acceptType = targetNode.AcceptDragDropType;
            TreeNodeType srcType = srcNode.NodeType;

            if (srcType == TreeNodeType.None || acceptType == TreeNodeType.None)
            {
                return false;
            }

            if ((acceptType & srcType) == 0)
            {
                return false;
            }

            return true;
        }

        protected bool CanInto(BaseTreeNode[] srcNodes, BaseTreeNode targetNode)
        {
            foreach (BaseTreeNode node in srcNodes)
            {
                ///任何一次返回false则整体返回false
                if (!CanInto(node, targetNode))
                {
                    return false;
                }
            }

            return true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            TreeViewHitTestInfo testInfo = this.HitTest(e.Location);
            if (!(testInfo.Node is BaseTreeNode)) return;

            _preLabelEditForMouseUp = false;

            //当鼠标按下后，准备拖拽
            if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                && (testInfo.Location == TreeViewHitTestLocations.Label
                || testInfo.Location == TreeViewHitTestLocations.Image))
            {
                if (testInfo.Node != null && HitIsOnNode(testInfo.Location))
                {
                    ///处理可支持多选
                    BaseTreeNode node = (BaseTreeNode)testInfo.Node;
                    if (MultiSelect)
                    {
                        if (SelectedNodes.Count == 0)
                        {
                            ///单选
                            SingleSelectNode(node, e.Button == MouseButtons.Right);
                        }
                        else if (SelectedNodes.Count == 1 && SelectedNodes[0] == node)
                        {
                            ///重命名
                            if (e.Button == MouseButtons.Left)
                            {
                                if (testInfo.Location == TreeViewHitTestLocations.Label)
                                {
                                    _preLabelEditForMouseUp = true;
                                }
                            }
                        }
                        else
                        {
                            if ((Control.ModifierKeys & Keys.Shift) != 0)
                            {
                                ///选择连续节点
                                ContinueSelectNode(node, e.Button == MouseButtons.Right);
                            }
                            else if ((Control.ModifierKeys & Keys.Control) != 0)
                            {
                                ///复选
                                MultiSelectNode(node, e.Button == MouseButtons.Right);
                            }
                            else
                            {
                                ///拖拽或者选中
                                if (!this.SelectedNodes.Contains(node))
                                {
                                    SingleSelectNode(node, e.Button == MouseButtons.Right);
                                }
                            }
                        }
                    }
                    else
                    {
                        NoMultiSelectNode(node as BaseTreeNode);
                    }
                }

                ///准备拖拽(必须在多选逻辑后面，不然this.SelectedNodes不准确)
                _isMouseDown = true;
                _beginPoint = e.Location;
                _dropNodes = this.SelectedNodes.ToArray();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isMouseDown && CurrentNode != null)
            {
                ///通过TreeNodeAttribute获取其是否允许被拖拽
                bool canDragDrop = CurrentNode.CanDragDrop;

                ///判断SelectedNodes是否是同一类型，若类型不一样，不允许拖拽
                Type nodeType = CurrentNode.GetType();
                foreach (BaseTreeNode node in this.SelectedNodes)
                {
                    if (node.GetType() != nodeType)
                    {
                        canDragDrop = false;
                        break;
                    }
                }

                if (canDragDrop)
                {
                    //将要拖动
                    if (Math.Abs(e.Location.X - _beginPoint.X) > 5 || Math.Abs(e.Location.Y - _beginPoint.Y) > 5)
                    {
                        _isMouseDown = false;
                        DoDragDropNode();
                    }
                }
            }

            base.OnMouseMove(e);
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            SetCurrentNode(_currentNode);

            base.OnAfterSelect(e);
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            ///取消选择被折叠而不可见的TreeNode
            List<BaseTreeNode> willUnSelectNodes = new List<BaseTreeNode>();
            foreach (BaseTreeNode node in SelectedNodes)
            {
                ///检查node是否是折叠节点的孙节点
                if (CheckNodeParent(node, (BaseTreeNode)e.Node))
                {
                    willUnSelectNodes.Add(node);
                }
            }

            ///执行取消选择
            foreach (BaseTreeNode node in willUnSelectNodes)
            {
                SelectedNodes.Remove(node);
            }

            base.OnAfterCollapse(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                RenameNode();
            }

            base.OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.CurrentNode == null || !this.CurrentNode.IsEditing)
            {
                #region 实现基本的按键选择控制键：上、下、左、右、PageUp、PageDown、Home、End

                switch (keyData)
                {
                    case Keys.Up:
                        {
                            ///先找到当前节点的上一个节点
                            BaseTreeNode node = null;
                            if (this.CurrentNode == null)
                            {
                                if (this.Nodes.Count > 0)
                                {
                                    node = (BaseTreeNode)this.Nodes[0];
                                }
                            }
                            else
                            {
                                node = (BaseTreeNode)this.CurrentNode.PrevVisibleNode;
                            }

                            ///设置
                            if (node != null)
                            {
                                this.CurrentNode = node;
                            }
                            return true;
                        }
                    case Keys.Down:
                        {
                            ///先找到当前节点的下一个节点
                            BaseTreeNode node = null;
                            if (this.CurrentNode == null)
                            {
                                if (this.Nodes.Count > 0)
                                {
                                    node = (BaseTreeNode)this.Nodes[0];
                                }
                            }
                            else
                            {
                                node = (BaseTreeNode)this.CurrentNode.NextVisibleNode;
                            }

                            ///设置
                            if (node != null)
                            {
                                this.CurrentNode = node;
                            }
                            return true;
                        }
                    case Keys.Left:
                        {
                            if (this.CurrentNode != null)
                            {
                                ///若有子节点，且处于展开状态：那么折叠
                                if (this.CurrentNode.Nodes.Count > 0 && this.CurrentNode.IsExpanded)
                                {
                                    this.CurrentNode.Collapse();
                                }
                                ///否则：讲CurrentNode设为其父节点
                                else
                                {
                                    BaseTreeNode nodeParent = (BaseTreeNode)this.CurrentNode.Parent;
                                    if (nodeParent != null)
                                    {
                                        this.CurrentNode = nodeParent;
                                    }
                                }
                            }
                            return true;
                        }
                    case Keys.Right:
                        {
                            if (this.CurrentNode != null)
                            {
                                if (this.CurrentNode.Nodes.Count > 0)
                                {
                                    ///若节点处于展开状态，则讲CurrentNode设为第一个子节点
                                    if (this.CurrentNode.IsExpanded)
                                    {
                                        this.CurrentNode = (BaseTreeNode)this.CurrentNode.Nodes[0];
                                    }
                                    ///若节点处于折叠状态，则展开
                                    else
                                    {
                                        this.CurrentNode.Expand();
                                    }
                                }
                            }
                            return true;
                        }
                    case Keys.Home:
                        {
                            if (this.Nodes.Count > 0)
                            {
                                this.CurrentNode = (BaseTreeNode)this.Nodes[0];
                                this.CurrentNode.EnsureVisible();
                            }
                            return true;
                        }
                    case Keys.End:
                        {
                            if (this.Nodes.Count > 0)
                            {
                                ///最后一个可见节点
                                BaseTreeNode lastNode = (BaseTreeNode)this.Nodes[0];
                                while (lastNode.Nodes.Count > 0 && lastNode.IsExpanded)
                                {
                                    lastNode = (BaseTreeNode)lastNode.LastNode;
                                }
                                this.CurrentNode = lastNode;
                                this.CurrentNode.EnsureVisible();
                            }
                            return true;
                        }
                    case Keys.PageUp:
                        {
                            if (this.Nodes.Count > 0)
                            {
                                ///根据屏幕显示的可见节点数，遍历找到上面该选中的节点
                                int offset = this.VisibleCount - 1;
                                BaseTreeNode tempNode = this.CurrentNode;
                                for (int i = 0; i < offset; i++)
                                {
                                    if (tempNode.PrevVisibleNode == null)
                                    {
                                        break;
                                    }
                                    tempNode = (BaseTreeNode)tempNode.PrevVisibleNode;
                                }

                                this.CurrentNode = tempNode;
                            }
                            return true;
                        }
                    case Keys.PageDown:
                        {
                            if (this.Nodes.Count > 0)
                            {
                                ///根据屏幕显示的可见节点数，遍历找到上面该选中的节点
                                int offset = this.VisibleCount - 1;
                                BaseTreeNode tempNode = this.CurrentNode;
                                for (int i = 0; i < offset; i++)
                                {
                                    if (tempNode.NextVisibleNode == null)
                                    {
                                        break;
                                    }
                                    tempNode = (BaseTreeNode)tempNode.NextVisibleNode;
                                }

                                this.CurrentNode = tempNode;
                            }
                            return true;
                        }
                }

                #endregion
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region 公共方法，供外部使用

        public virtual void DoDragDropNode()
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                this.DoDragDrop(this.SelectedNodes, DragDropEffects.Copy | DragDropEffects.Link);
            }
            else
            {
                this.DoDragDrop(this.SelectedNodes, DragDropEffects.Link | DragDropEffects.Move);
            }
        }

        /// <summary>
        /// 将要拖拽TreeNode
        /// </summary>
        public event EventHandler<DragDropNodeEventArgs> NodeDragDroping;
        protected virtual void OnNodeDragDroping(DragDropNodeEventArgs e)
        {
            if (NodeDragDroping != null)
            {
                NodeDragDroping(this, e);
            }
        }

        /// <summary>
        /// 已经拖拽TreeNode
        /// </summary>
        public event EventHandler<DragDropNodeEventArgs> NodeDragDroped;
        protected virtual void OnNodeDragDroped(DragDropNodeEventArgs e)
        {
            if (NodeDragDroped != null)
            {
                NodeDragDroped(this, e);
            }
        }

        /// <summary>
        /// 通过fullPath获取TreeNode
        /// </summary>
        public BaseTreeNode GetNode(string fullPath)
        {
            string[] arrStr = fullPath.Split(new string[] { this.PathSeparator }, StringSplitOptions.None);

            BaseTreeNode node = null;
            foreach (BaseTreeNode n in this.Nodes)
            {
                if (n.Text == arrStr[0])
                {
                    node = n;
                    break;
                }
            }
            for (int i = 1; i < arrStr.Length; i++)
            {
                if (node == null)
                {
                    return null;
                }
                foreach (BaseTreeNode n in node.Nodes)
                {
                    if (n.Text == arrStr[i])
                    {
                        node = n;
                        break;
                    }
                }
                //node = node.Nodes[];
            }
            return node;
        }

        /// <summary>
        /// 检查node节点是否是grandfatherNode节点的孙节点
        /// (即node通过其Parent属性的迭代调用一定能找到grandfatherNode)
        /// </summary>
        /// <returns></returns>
        public bool CheckNodeParent(BaseTreeNode node, BaseTreeNode grandfatherNode)
        {
            ///若是node比grandfatherNode的Leve小，则肯定不是其孙节点
            if (node.Level <= grandfatherNode.Level)
            {
                return false;
            }

            BaseTreeNode agentNode = node;
            do
            {
                agentNode = agentNode.Parent;
            }
            while (agentNode.Level != grandfatherNode.Level);

            return agentNode == grandfatherNode;
        }

        /// <summary>
        /// 检查node节点是否是grandfatherNode节点的孙节点
        /// (只需是grandfatherNodes中任何一个节点的孙节点就返回true)
        /// (即node通过其Parent属性的迭代调用一定能找到grandfatherNode)
        /// </summary>
        public bool CheckNodeParent(BaseTreeNode node, BaseTreeNode[] grandfatherNodes)
        {
            foreach (BaseTreeNode grandfatherNode in grandfatherNodes)
            {
                if (CheckNodeParent(node, grandfatherNode))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 处理可支持多选的内部方法

        private void SetCurrentNode(BaseTreeNode node)
        {
            _currentNode = node;
            SelectedNode = null;
            if (node != null && !SelectedNodes.Contains(node))
            {
                SelectedNodes.Add(node);
            }
        }

        private BaseTreeNode GetTopNode(BaseTreeNode node1, BaseTreeNode node2)
        {
            Debug.Assert(node1.TreeView == node2.TreeView);

            ///先判断是否同一个对象
            if (object.ReferenceEquals(node1,node2))
            {
                return node1;
            }

            ///找到两个节点的较浅的深度值
            int minLevel = Math.Min(node1.Level, node2.Level);

            ///向上遍历，以其祖节点作为代理
            TreeNode agentNode1 = node1;
            while (agentNode1.Level != minLevel)
            {
                agentNode1 = agentNode1.Parent;
            }
            BaseTreeNode agentNode2 = node2;
            while (agentNode2.Level != minLevel)
            {
                agentNode2 = agentNode2.Parent;
            }

            ///比较两个代理哪个大
            while (true)
            {
                ///在同一个父节点的情况下比较Index值
                if (agentNode1.Parent == agentNode2.Parent)
                {
                    int result = agentNode1.Index - agentNode2.Index;

                    ///为0则表示两个代理是同一个节点。这时哪个节点Level越小越靠前
                    if (result == 0)
                    {
                        return node1.Level < node2.Level ? node1 : node2;
                    }
                    else if (result > 0)
                    {
                        return node2;
                    }
                    else
                    {
                        return node1;
                    }
                }
                else
                {
                    agentNode1 = agentNode1.Parent;
                    agentNode2 = agentNode2.Parent;
                }
            }
        }

        /// <summary>
        /// 连选
        /// </summary>
        private void ContinueSelectNode(BaseTreeNode node, bool isRightDown)
        {
            if (_currentNode == node)
            {
                SingleSelectNode(node, isRightDown);
                return;
            }

            ///找到这两个节点哪个是开头，哪个是结尾(即前后顺序)
            BaseTreeNode topNode = (BaseTreeNode)GetTopNode(_currentNode, node);
            BaseTreeNode bottomNode = (node == topNode ? _currentNode : node);

            ///先清空
            SelectedNodes.Clear();
            //SelectedNodes.ClearWithout(_currentNode);

            BaseTreeNode nextNode = topNode;
            while (nextNode != bottomNode)
            {
                SelectedNodes.Add(nextNode);
                nextNode = (BaseTreeNode)nextNode.NextVisibleNode;
            }

            SelectedNodes.Add(bottomNode);
        }

        /// <summary>   
        /// 复选   
        /// </summary>
        private void MultiSelectNode(BaseTreeNode node, bool isRightDown)
        {
            ///若是Enabled==false,则不处理
            BaseTreeNode baseNode = node as BaseTreeNode;
            if (baseNode != null && !baseNode.Enabled)
            {
                return;
            }

            ///先检查是否已经被选
            if (SelectedNodes.Contains(node))
            {
                ///已被选择。则检查是否是右键按下
                if (!isRightDown)
                {
                    ///不是右键按下，那么从选择集合里删除
                    SelectedNodes.Remove(node);
                }
            }
            else
            {
                ///未被选择。添加到选择集合
                SelectedNodes.Add(node);

                ///设置_currentNode节点
                SetCurrentNode(node);
            }
        }

        /// <summary>   
        /// 单选   
        /// </summary>
        private void SingleSelectNode(BaseTreeNode node, bool isRightDown)
        {
            bool isSelectContained = SelectedNodes.Contains(node);

            ///若是右键，则先看是否在已经选择的节点上按下，若是，则基本不做处理
            if (isRightDown && isSelectContained)
            {
                ///设置_currentNode节点
                SetCurrentNode(node);
                return;
            }

            ///若是Enabled==false,则取消所有选择
            BaseTreeNode baseNode = node as BaseTreeNode;
            if (baseNode != null && !baseNode.Enabled)
            {
                ///清空
                SelectedNodes.Clear();
            }
            else
            {
                if (!isSelectContained)
                {
                    ///添加
                    SelectedNodes.Add(node);
                }

                ///清空其他
                SelectedNodes.ClearWithout(node);
            }
        }

        /// <summary>
        /// 单选模式时的选择
        /// </summary>
        private void NoMultiSelectNode(BaseTreeNode node)
        {
            this.CurrentNode = node;
        }

        public void InitNode(BaseTreeNode node)
        {
            ///处理Enabled为false的情况
            BaseTreeNode baseNode = node as BaseTreeNode;
            if (baseNode != null && !baseNode.Enabled)
            {
                node.BackColor = BackColor;
                node.ForeColor = SystemColors.GrayText;
                return;
            }

            node.BackColor = BackColor;
            node.ForeColor = SystemColors.ControlText;
        }

        /// <summary>   
        /// 取消节点的高亮显示   
        /// </summary>
        private void LowlightNode(BaseTreeNode node)
        {
            ///处理Enabled为false的情况
            BaseTreeNode baseNode = node as BaseTreeNode;
            if (baseNode != null && !baseNode.Enabled)
            {
                node.BackColor = BackColor;
                node.ForeColor = SystemColors.GrayText;
                return;
            }

            node.BackColor = BackColor;
            node.ForeColor = SystemColors.ControlText;
        }

        /// <summary>   
        /// 高亮显示节点   
        /// </summary>
        private void HighlightNode(BaseTreeNode node)
        {
            ///处理Enabled为false的情况
            BaseTreeNode baseNode = node as BaseTreeNode;
            if (baseNode != null && !baseNode.Enabled)
            {
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.GrayText;
                return;
            }

            node.BackColor = SystemColors.Highlight;
            node.ForeColor = SystemColors.HighlightText;
        }

        #endregion

        #region 处理重命名

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMouseDown = false;

            if (e.Button == MouseButtons.Left)
            {
                if (_preLabelEditForMouseUp)
                {
                    if (_wasDoubleClick)
                    {
                        _wasDoubleClick = false;
                    }
                    else
                    {
                        _triggerLabelEdit = true;
                    }
                }
            }
            base.OnMouseUp(e);
        }

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            ///通过检查TreeNode的TreeNodeAttribute属性来判定是否允许重命名
            bool canRename = ((BaseTreeNode)e.Node).CanRename;
            if (!canRename)
            {
                e.CancelEdit = true;
                return;
            }

            // put node label to initial state
            // to ensure that in case of label editing cancelled
            // the initial state of label is preserved
            this.CurrentNode.Text = viewedLabel;
            // base.OnBeforeLabelEdit is not called here
            // it is called only from StartLabelEdit
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            this.LabelEdit = false;
            if (e.Label == null)
            {
                return;
            }

            ValidateLabelEditEventArgs ea = new ValidateLabelEditEventArgs(e.Label);
            OnValidateLabelEdit(ea);
            if (ea.Cancel == true)
            {
                e.Node.Text = editedLabel;
                this.LabelEdit = true;
                e.Node.BeginEdit();
            }
            else
            {
                base.OnAfterLabelEdit(e);
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        public virtual void RenameNode()
        {
            BaseTreeNode tn = this.CurrentNode;

            if (tn != null)
            {
                if (!tn.CanRename)
                {
                    return;
                }

                viewedLabel = tn.Text;

                NodeLabelEditEventArgs e = new NodeLabelEditEventArgs(tn);
                base.OnBeforeLabelEdit(e);

                if (!e.CancelEdit)
                {
                    editedLabel = tn.Text;

                    this.LabelEdit = true;
                    tn.BeginEdit();
                }
            }
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (_triggerLabelEdit
             && m.Msg == WM_TIMER)
            {
                _triggerLabelEdit = false;
                RenameNode();
            }

            base.OnNotifyMessage(m);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            _wasDoubleClick = true;
            _triggerLabelEdit = false;

            base.OnDoubleClick(e);
        }

        public event EventHandler<ValidateLabelEditEventArgs> ValidateLabelEdit;
        protected virtual void OnValidateLabelEdit(ValidateLabelEditEventArgs e)
        {
            if (ValidateLabelEdit != null)
            {
                ValidateLabelEdit(this, e);
            }
        }

        #endregion
    }

    #region 一些类型

    public class DragDropNodeEventArgs : CancelEventArgs
    {
        public DragDropNodeEventArgs(DragEventArgs drgevent,DragDropResult ddr)
        {
            this._drgevent = drgevent;
            this._ddr = ddr;
            this.Cancel = false;
        }

        DragEventArgs _drgevent;
        DragDropResult _ddr;

        /// <summary>
        /// 从DragDrop事件转发过来的原参数
        /// </summary>
        public DragEventArgs DrgEvent
        {
            get { return _drgevent; }
        }
        /// <summary>
        /// 拖拽的结果数据
        /// </summary>
        public DragDropResult DragDropResult
        {
            get { return _ddr; }
        }
    }

    public class NodeEventArgs : EventArgs
    {
        private BaseTreeNode _treeNode;
        public BaseTreeNode TreeNode
        {
            get { return _treeNode; }
        }

        public NodeEventArgs(BaseTreeNode node)
        {
            this._treeNode = node;
        }
    }

    public class ValidateLabelEditEventArgs : CancelEventArgs
    {
        public ValidateLabelEditEventArgs(string label)
        {
            this.label = label;
            this.Cancel = false;
        }

        private string label;
        /// <summary>
        /// 节点编辑后的新值
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }
    }

    /// <summary>
    /// 拖拽结果
    /// </summary>
    public class DragDropResult
    {
        BaseTreeNode _dropPutNode;
        BaseTreeNode[] _dragdropNodes;
        DragDropResultType _dropResultType;

        /// <summary>
        /// 拖拽的放开时鼠标处的节点
        /// </summary>
        public BaseTreeNode DropPutNode
        {
            get { return _dropPutNode; }
        }
        /// <summary>
        /// 被拖拽的节点
        /// </summary>
        public BaseTreeNode[] DragDropNodes
        {
            get { return _dragdropNodes; }
        }
        /// <summary>
        /// 拖拽放开时处于鼠标处节点(DropPutNode)的前面还是后面
        /// </summary>
        public DragDropResultType DropResultType
        {
            get { return _dropResultType; }
        }
        public DragDropResult(BaseTreeNode[] dragdropNodes, BaseTreeNode dropNode,DragDropResultType dropResultType)
        {
            _dragdropNodes = dragdropNodes;
            _dropPutNode = dropNode;
            _dropResultType = dropResultType;
        }
    }

    #endregion
}
