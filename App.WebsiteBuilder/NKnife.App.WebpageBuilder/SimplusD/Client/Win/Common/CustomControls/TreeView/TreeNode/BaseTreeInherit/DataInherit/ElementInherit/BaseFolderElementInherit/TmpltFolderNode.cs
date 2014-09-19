using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = true,
    CanDelete = true,
    AcceptDragDropType = TreeNodeType.Tmplt | TreeNodeType.TmpltFolder)]
    public class TmpltFolderNode : BaseFolderElementNode
    {
        public TmpltFolderNode(FolderXmlElement element)
            :base(element)
        {
        }

        public override void LoadData()
        {
            this.Text = Element.Title;
            if (TreeView.TreeMode != TreeMode.SelectTmpltFolder)
                LoadChildNodes();
        }

        protected override void LoadChildNodes()
        {
            foreach (var node in Element.ChildNodes)
            {
                if (node is TmpltSimpleExXmlElement)
                {
                    TmpltNode tnode = (TmpltNode)AddElementNode((SimpleExIndexXmlElement)node);
                    if (tnode == null)
                    {
                        continue;
                    }

                    ///如果树是选择模板模式，则设置其Enabled状态
                    if (TreeView.TreeMode == TreeMode.SelectTmplt)
                    {
                        TmpltType tmpltType = (TmpltType)(int)TreeView.SelectTmpltType;

                        ///TmpltType.None的时候，可以选择所有模板，创建页面的类型根据选择的模板类型决定
                        if (tmpltType != TmpltType.None)
                        {
                            if (tnode.Element.TmpltType == tmpltType)
                                tnode.Enabled = true;
                            else
                                tnode.Enabled = false;
                        }
                    }
                }
                else if (node is FolderXmlElement)
                {
                    AddElementNode((SimpleExIndexXmlElement)node);
                }
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
                return TreeNodeType.TmpltFolder;
            }
        }
    }
}
