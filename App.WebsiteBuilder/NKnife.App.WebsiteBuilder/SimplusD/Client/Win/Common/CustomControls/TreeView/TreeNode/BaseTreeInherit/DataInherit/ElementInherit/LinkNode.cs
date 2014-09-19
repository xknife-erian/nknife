using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 收藏夹里的节点。(它只能算是一个链接节点，它的真实数据在TargetNode里)
    /// </summary>
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = false,CanDelete=true)]
    public class LinkNode : ElementNode
    {
        public LinkNode(ElementNode targetNode)
            :base(targetNode.Element,targetNode.IsFolder)
        {
            this.TargetNode = targetNode;
        }

        private ElementNode _targetNode;
        /// <summary>
        /// 收藏夹里的节点所对应的真实节点。
        /// </summary>
        public ElementNode TargetNode
        {
            get
            {
                return _targetNode;
            }
            protected set
            {
                _targetNode = value;
            }
        }

        public override string CollapseImageKey
        {
            get
            {
                return TargetNode.CollapseImageKey;
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return TargetNode.ExpandImageKey;
            }
        }

        public override void LoadData()
        {
            this.Text = this.TargetNode.Text;
        }

        protected override void LoadChildNodes()
        {
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.Link;
            }
        }
    }
}
