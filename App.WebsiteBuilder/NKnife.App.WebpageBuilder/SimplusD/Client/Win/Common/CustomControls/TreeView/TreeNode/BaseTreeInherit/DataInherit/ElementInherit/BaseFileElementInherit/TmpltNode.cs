using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = false,
    CanDelete = true)]
    public class TmpltNode : BaseFileElementNode
    {
        public TmpltNode(TmpltSimpleExXmlElement element)
            :base(element)
        {
            this.Text = element.Title;
        }

        public new TmpltSimpleExXmlElement Element
        {
            get
            {
                return base.Element as TmpltSimpleExXmlElement;
            }
        }

        protected override void LoadChildNodes()
        {
            
        }

        public override string CollapseImageKey
        {
            get 
            {
                return "tree.img.templet";
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.templet2";
            }
        }


        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.Tmplt;
            }
        }
    }
}
