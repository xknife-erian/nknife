using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    //文件夹,在项目树删除更新模板视图树时用到,（暂时）
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, CanDelete = false)]
    class FolderElementNode : TmpltBaseTreeNode
    {
        public FolderElementNode(FolderXmlElement element)
            :base(element)
        {
        }

        #region 字段与属性

        public new AnyXmlElement Element
        {
            get
            {
                return base.Element as FolderXmlElement;
            }
            internal set
            {
                base.Element = value;
            }
        }

        public string Id
        {
            get
            {
                return (base.Element as FolderXmlElement).Id;
            }
        }

        public override string CollapseImageKey
        {
            get
            {
                return "";
            }
        }

        #endregion

        #region 方法和重写基类的方法

        public override void LoadData(TmpltTreeNodeType NodeFilterType)
        {
        }

        protected override void LoadChildNodes(TmpltTreeNodeType NodeFilterType)
        {
        }

        public override void SetNoChildNodesText()
        { 
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.TmpltFolder;
            }
        }

        public override void RenewNodeText(TmpltBaseTreeNode node)
        {
        }

        public override void LoadData( )
        {
        }

        protected override void LoadChildNodes( )
        {
        }

        #endregion

    }
}