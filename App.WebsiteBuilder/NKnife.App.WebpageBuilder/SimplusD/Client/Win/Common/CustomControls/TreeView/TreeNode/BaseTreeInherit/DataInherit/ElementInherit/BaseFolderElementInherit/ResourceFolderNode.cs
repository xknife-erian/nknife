using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = true,
        CanDelete = true,AcceptDragDropType=TreeNodeType.ResourceFolder|TreeNodeType.ResourceFile)]
    public class ResourceFolderNode : BaseFolderElementNode
    {
        public ResourceFolderNode(FolderXmlElement element)
            :base(element)
        {
        }

        protected override void LoadChildNodes()
        {
            foreach (var node in Element.ChildNodes)
            {
                if (node is FolderXmlElement)
                {
                    AddElementNode((SimpleExIndexXmlElement)node);
                }
                else if (node is FileSimpleExXmlElement)
                {
                    AddElementNode((SimpleExIndexXmlElement)node);
                }
            }
        }

        public override string CollapseImageKey
        {
            get { return "tree.img.favorite2"; }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.favorite";
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
               return  TreeNodeType.ResourceFolder;
            }
        }
    }
}
