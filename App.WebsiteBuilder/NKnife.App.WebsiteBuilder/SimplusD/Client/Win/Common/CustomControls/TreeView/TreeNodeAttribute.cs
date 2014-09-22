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
        /// ���Ľڵ��һЩ��������
        /// </summary>
        public TreeNodeAttribute()
        {
        }

        private bool _isBranch = false;
        /// <summary>
        /// �Ƿ�֦�ڵ�(������Ҷ�ڵ�)
        /// </summary>
        public bool IsBranch
        {
            get { return _isBranch; }
            set { _isBranch = value; }
        }

        private bool _canDragDrop = true;
        /// <summary>
        /// �Ƿ������ק�˽ڵ�
        /// </summary>
        public bool CanDragDrop
        {
            get { return _canDragDrop; }
            set { _canDragDrop = value; }
        }

        private TreeNodeType _acceptDragDropType;
        /// <summary>
        /// �˽ڵ���ܵ���ק����
        /// </summary>
        public TreeNodeType AcceptDragDropType
        {
            get { return _acceptDragDropType; }
            set { _acceptDragDropType = value; }
        }

        private bool _canRename = true;
        /// <summary>
        /// �Ƿ����������
        /// </summary>
        public bool CanRename
        {
            get { return _canRename; }
            set { _canRename = value; }
        }

        private bool _isDockExpand = true;
        /// <summary>
        /// �Ƿ���ͣ��ʱչ���ڵ�
        /// </summary>
        public bool IsDockExpand
        {
            get { return _isDockExpand; }
            set { _isDockExpand = value; }
        }

        private DragDropEffects _acceptEffects = DragDropEffects.Move | DragDropEffects.Copy;
        /// <summary>
        /// ���ܵ��Ϸ�����
        /// </summary>
        public DragDropEffects AcceptEffects
        {
            get { return _acceptEffects; }
            set { _acceptEffects = value; }
        }

        private bool _isGrandchildNoOrder = false;
        /// <summary>
        /// �Ƿ���ڵ㲻����(��ڵ�ָ��ǰ�ڵ��µ����м��ڵ㣬��Ȼ�����ӽڵ�)
        /// </summary>
        public bool IsGrandchildNoOrder
        {
            get { return _isGrandchildNoOrder; }
            set { _isGrandchildNoOrder = value; }
        }

        private bool _canDelete = false;
        /// <summary>
        /// �Ƿ����ɾ���˽ڵ�
        /// </summary>
        public bool CanDelete
        {
            get { return _canDelete; }
            set { _canDelete = value; }
        }
    }
}
