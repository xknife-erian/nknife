using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, IsGrandchildNoOrder = true,
       // AcceptEffects = DragDropEffects.Link,
        AcceptDragDropType = TreeNodeType.Page | TreeNodeType.Tmplt | TreeNodeType.Channel
        | TreeNodeType.ChannelFolder | TreeNodeType.TmpltFolder | TreeNodeType.ResourceFolder)]
    public class FavoriteRootNode : BaseTreeNode
    {
        public override void LoadData()
        {
            this.Text = "收藏夹";
            LoadChildNodes();
        }

        protected override void LoadChildNodes()
        {
            
        }

        public override string CollapseImageKey
        {
            get
            {
                return "tree.img.favorite2";
            }
        }

        public override string ExpandImageKey
        {
            get
            {
                return "tree.img.favorite";
            }
        }

        public void RemoveFavoriteFile(string id)
        {
            foreach (LinkNode node in Nodes)
            {
                if (node.TargetNode.Element.Id == id)
                {
                    RemoveChildNode(node);
                    return;
                }
            }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.Favorite;
            }
        }
    }
}
