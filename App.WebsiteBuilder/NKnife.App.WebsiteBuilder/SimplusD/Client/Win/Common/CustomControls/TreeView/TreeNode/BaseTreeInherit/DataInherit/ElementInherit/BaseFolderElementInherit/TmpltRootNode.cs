using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true,
     AcceptDragDropType = TreeNodeType.Tmplt | TreeNodeType.TmpltFolder)]
    public class TmpltRootNode : TmpltFolderNode
    {
        public TmpltRootNode(TmpltFolderXmlElement element)
            :base(element)
        {
            Element = element;
        }

        public new TmpltFolderXmlElement Element
        {
            get
            {
                return base.Element as TmpltFolderXmlElement;
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
                return "tree.img.tmpltfolder";
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.tmpltfolder2";
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.TmpltRootFolder;
            }
        }
    }
}
