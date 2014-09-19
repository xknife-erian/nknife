using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true,
    AcceptDragDropType = TreeNodeType.ResourceFolder | TreeNodeType.ResourceFile)]
    public class ResourceRootNode : ResourceFolderNode
    {
        public ResourceRootNode(ResourcesXmlElement element)
            :base(element)
        {
            Element = element;
        }

        public new ResourcesXmlElement Element
        {
            get
            {
                return base.Element as ResourcesXmlElement;
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
                return "tree.img.resources"; 
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.resources2";
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.ResourceRoot;
            }
        }
    }
}
