using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = false, IsBranch = true, CanDelete = false,
        AcceptDragDropType = TreeNodeType.Page | TreeNodeType.Channel
        | TreeNodeType.ChannelFolder | TreeNodeType.TmpltFolder | TreeNodeType.ResourceFolder )]
    public class RootChannelNode : ChannelNode
    {
        public RootChannelNode(RootChannelXmlElement element)
            :base(element)
        {
            Element = element;
        }

        public override void LoadData()
        {
            base.LoadData();

            this.Expand();
        }

        protected override void LoadChildNodes()
        {
            base.LoadChildNodes();

            if (TreeView.TreeMode != TreeMode.General)
            {
                TreeView.ExpandAll();
            }
        }

        public ResourceRootNode ResourceRootNode { get; internal set; }

        public TmpltRootNode TmpltRootNode { get; internal set; }

        public new RootChannelXmlElement Element
        {
            get
            {
                return base.Element as RootChannelXmlElement;
            }
            protected set
            {
                base.Element = value;
            }
        }

        public override string CollapseImageKey
        {
            get
            {
                return "tree.img.channel";
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.channel2";
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.RootChannel;
            }
        }
    }
}
