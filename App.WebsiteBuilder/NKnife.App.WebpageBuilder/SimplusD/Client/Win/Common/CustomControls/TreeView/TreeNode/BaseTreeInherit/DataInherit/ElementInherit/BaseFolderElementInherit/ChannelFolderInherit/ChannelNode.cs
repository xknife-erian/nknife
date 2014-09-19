using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = true, CanDelete = true,
        AcceptDragDropType = TreeNodeType.Page |  TreeNodeType.Channel | TreeNodeType.ChannelFolder)]
    public class ChannelNode : ChannelFolderNode
    {
        public ChannelNode(ChannelSimpleExXmlElement element)
            :base(element)
        {
        }

        public new ChannelSimpleExXmlElement Element
        {
            get
            {
                return base.Element as ChannelSimpleExXmlElement;
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
                return TreeNodeType.Channel;
            }
        }
    }
}
