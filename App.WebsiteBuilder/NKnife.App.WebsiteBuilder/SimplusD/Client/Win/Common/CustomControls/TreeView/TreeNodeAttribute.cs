using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TreeNodeAttribute : Attribute
    {
        /// <summary>
        /// 树的节点的一些附加数据
        /// </summary>
        public TreeNodeAttribute()
        {
        }

        private bool _isBranch = false;
        /// <summary>
        /// 是否枝节点(即不是叶节点)
        /// </summary>
        public bool IsBranch
        {
            get { return _isBranch; }
            set { _isBranch = value; }
        }

        private bool _canDragDrop = true;
        /// <summary>
        /// 是否可以拖拽此节点
        /// </summary>
        public bool CanDragDrop
        {
            get { return _canDragDrop; }
            set { _canDragDrop = value; }
        }

        private TreeNodeType _acceptDragDropType;
        /// <summary>
        /// 此节点接受的拖拽类型
        /// </summary>
        public TreeNodeType AcceptDragDropType
        {
            get { return _acceptDragDropType; }
            set { _acceptDragDropType = value; }
        }

        private bool _canRename = true;
        /// <summary>
        /// 是否可以重命名
        /// </summary>
        public bool CanRename
        {
            get { return _canRename; }
            set { _canRename = value; }
        }

        private bool _isDockExpand = true;
        /// <summary>
        /// 是否在停靠时展开节点
        /// </summary>
        public bool IsDockExpand
        {
            get { return _isDockExpand; }
            set { _isDockExpand = value; }
        }

        private DragDropEffects _acceptEffects = DragDropEffects.Move | DragDropEffects.Copy;
        /// <summary>
        /// 接受的拖放类型
        /// </summary>
        public DragDropEffects AcceptEffects
        {
            get { return _acceptEffects; }
            set { _acceptEffects = value; }
        }

        private bool _isGrandchildNoOrder = false;
        /// <summary>
        /// 是否孙节点不排序(孙节点指当前节点下的所有级节点，当然包括子节点)
        /// </summary>
        public bool IsGrandchildNoOrder
        {
            get { return _isGrandchildNoOrder; }
            set { _isGrandchildNoOrder = value; }
        }

        private bool _canDelete = false;
        /// <summary>
        /// 是否可以删除此节点
        /// </summary>
        public bool CanDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; }
        }
    }
}
