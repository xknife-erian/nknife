using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    //模板文件
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true, CanDelete = false)]
    public class TmpltElementNode : TmpltBaseTreeNode
    {
        public TmpltElementNode(TmpltSimpleExXmlElement element)
            :base(element)
        {
            this.Text = AbsoluteFileName;
            this.Nodes.Add("没有页面片");
            this.ToolTipText = ((TmpltSimpleExXmlElement)Element).TmpltType.ToString();

            _NodeExpand = false; //初始化时没打开过的状态
        }

       #region 字段与属性

       public new AnyXmlElement Element
        {
            get
            {
                return base.Element as TmpltSimpleExXmlElement;
            }
            internal set
            {
                base.Element = value;
            }
        }

       public override string ID
       {
            get
            {
                return (base.Element as TmpltSimpleExXmlElement).Id;
            }
       }

       private string AbsoluteFileName
       {//FolderXmlElement
           get
           {
               SimpleExIndexXmlElement indexElement = (SimpleExIndexXmlElement)Element;
               string FullName = indexElement.Title;
               while (indexElement.ParentNode is FolderXmlElement && !(indexElement.ParentNode is TmpltFolderXmlElement))
               {
                   indexElement = (SimpleExIndexXmlElement)indexElement.ParentNode;
                   FullName = indexElement.Title +"\\" + FullName;
               }
               return FullName;
           }
       }

       public override TreeNodeType NodeType
       {
           get
           {
               return TreeNodeType.Tmplt;
           }
       }

       /// <summary>
       /// 返回当前模板节点的类型
       /// </summary>
       public override TmpltTreeNodeType CurrentNodeType
       {
           get
           {
               TmpltTreeNodeType retNodeType = TmpltTreeNodeType.none;
               switch (((TmpltSimpleExXmlElement)Element).TmpltType)
               {
                   case TmpltType.General:
                       retNodeType = TmpltTreeNodeType.generalTmplt;
                       break;
                   case TmpltType.Home:
                       retNodeType = TmpltTreeNodeType.homeTmplt;
                       break;
                   case TmpltType.Product:
                       retNodeType = TmpltTreeNodeType.productTmplt;
                       break;
                   case TmpltType.Knowledge:
                       retNodeType = TmpltTreeNodeType.knowledgeTmplt;
                       break;
                   case TmpltType.Hr:
                       retNodeType = TmpltTreeNodeType.hrTmplt;
                       break;
                   case TmpltType.InviteBidding:
                       retNodeType = TmpltTreeNodeType.inviteBidTmplt;
                       break;
                   case TmpltType.Project:
                       retNodeType = TmpltTreeNodeType.projectTmplt;
                       break;
               }
               return retNodeType;
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
           LoadChildNodes(NodeFilterType);
       }

       protected override void LoadChildNodes(TmpltTreeNodeType NodeFilterType)
       {
           this.Nodes.Clear();
           TmpltXmlDocument tmpltDoc = Service.Sdsite.CurrentDocument.GetTmpltDocumentById(ID);
           foreach (SnipXmlElement snipEle in tmpltDoc.GetSnipElementList())
           {
               AddElementNode(snipEle,NodeFilterType);
           }
           SetNoChildNodesText();
       }

       public override void SetNoChildNodesText()
       {
           if (this.Nodes.Count <= 0)
               ClearTmpltNode();
       }

       public void ClearTmpltNode()
       {
           this.Nodes.Clear();
           TmpltEmptyNode tmpltEmptyNode = new TmpltEmptyNode(null);
           this.Nodes.Add(tmpltEmptyNode);
       }

       public override void RenewNodeText(TmpltBaseTreeNode node)
       {
           this.Text = AbsoluteFileName;
       }

       public override void LoadData()
       {
       }

       protected override void LoadChildNodes()
       {
       }

       #endregion       
    }

    
}
