using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = false,
    CanDelete = true)]
    public class PageNode : BaseFileElementNode
    {
        public PageNode(PageSimpleExXmlElement element)
            : base(element)
        {
        }

        public new PageSimpleExXmlElement Element
        {
            get
            {
                return base.Element as PageSimpleExXmlElement;
            }
        }


        protected override void LoadChildNodes()
        {
            
        }

        public override string CollapseImageKey
        {
            get
            {
                switch (this.Element.PageType)
                {
                    case PageType.General:
                        return "tree.img.page";
                    case PageType.Product:
                        return "tree.img.product";
                    case PageType.Project:
                        return "tree.img.project";
                    case PageType.InviteBidding:
                        return "tree.img.invitebidd";
                    case PageType.Knowledge:
                        return "tree.img.knowledge";
                    case PageType.Hr:
                        return "tree.img.hr";
                    case PageType.Home:
                        return "tree.img.page";
                }
                return "tree.img.page";
            }
        }

        public override TreeNodeType NodeType
        {
            get 
            {
                return TreeNodeType.Page;
            }
        }
    }
}