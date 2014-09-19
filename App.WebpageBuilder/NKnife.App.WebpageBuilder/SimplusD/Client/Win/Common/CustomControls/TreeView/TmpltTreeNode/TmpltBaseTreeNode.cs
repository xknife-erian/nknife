using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    [ResReader(false)]
    public abstract class TmpltBaseTreeNode : BaseTreeNode
    {

        /// <summary>
        /// 
        /// </summary>
        public TmpltBaseTreeNode(AnyXmlElement element)
        {
            Element = element;

            /// 调用Jeelu.ResourcesReader管理控件所使用的资源
            ResourcesReader.SetObjectResourcesHelper(this);
            this.SetImageList();
            this._imageList.ColorDepth = ColorDepth.Depth32Bit;
            this.SetImage();
        }    
        
        #region 字段和属性

        protected bool _NodeExpand;

        /// <summary>
        /// 该节点是否被展开过
        /// </summary>
        public virtual bool NodeExpand
        {
            get { return _NodeExpand; }
            set { _NodeExpand = value; }
        }

        //public abstract TreeNodeType NodeType { get; }

        public  TmpltTreeView TreeView
        {
            get
            {
                return (TmpltTreeView)base.TreeView;
                
            }
        }

        private ImageList _imageList = new ImageList();

        public AnyXmlElement Element { get;  set; }

        /// <summary>
        /// 当前节点显示的类型
        /// </summary>
        /// <param name="node"></param>
        public virtual TmpltTreeNodeType CurrentNodeType { get; set; }

        /// <summary>
        /// 节点在XML所对应的ID,因为模板/页面片/PART的ID不统一,所以增加个属性
        /// </summary>
        public virtual string ID { get; set; }

        public override string CollapseImageKey
        {
            get
            {
                return "";
            }
        }

        public override bool CanRename
        {
            get { return true; }
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 添加Node到Nodes集合的方法。为使我们能在添加时做一些统一的处理，
        /// 所有的节点添加必须使用此方法，若派生类重写此方法，必须最终调用base.AddChildNode
        /// </summary>
        public int AddChildNode(TmpltBaseTreeNode node)
        {
            return AddChildNode(-1, node);
        }

        public virtual int AddChildNode(int index, TmpltBaseTreeNode node)
        {
            int result = index;
            ///先添加到节点中
            if (index == -1)
            {
                result = this.Nodes.Add(node);
            }
            else
            {
                this.Nodes.Insert(index, node);
            }
            ///设置图标
            // node.SelectedImageKey = node.ImageKey = node.FactImageKey;

            //if()//什么样的节点需要添加
            {
                TreeView.SetElementNode(node);
            }
            return result;
        }

        public virtual void AddElementNode(AnyXmlElement Element, TmpltTreeNodeType NodeFilterType)
        {
            TmpltBaseTreeNode tmpltBase = null;
            if (Element is SnipXmlElement)
            {
                tmpltBase = new SnipElementNode(Element as SnipXmlElement);
                if ((NodeFilterType != TmpltTreeNodeType.none) && NodeFilterType != tmpltBase.CurrentNodeType)
                {
                    return;
                }
            }
            else if (Element is SnipPartXmlElement)
            {
                tmpltBase = new PartElementNode(Element as SnipPartXmlElement);
            }
            AddChildNode(tmpltBase);
            tmpltBase.LoadData(NodeFilterType);
        }

        /// <summary>
        /// 加载节点本身的数据。若需要加载子节点，则在派生类的实现里需要调用LoadChildNodes()
        /// </summary>
        public abstract void LoadData(TmpltTreeNodeType NodeFilterType);

        /// <summary>
        /// 加载子节点
        /// </summary>
        protected abstract void LoadChildNodes(TmpltTreeNodeType NodeFilterType);

        /// <summary>
        /// 重新加载单个节点数据
        /// </summary>
        /// <param name="NodeFilterType"></param>
        public abstract void SetNoChildNodesText();

        public virtual void RemoveChildNode(TmpltBaseTreeNode node)
        {
            while (node.Nodes.Count > 0)
            {
                if (node.Nodes.Count == 1 && !(node.FirstNode is TmpltBaseTreeNode)) break;

                if (node.FirstNode is TmpltBaseTreeNode)
                {
                    this.RemoveChildNode((TmpltBaseTreeNode)node.FirstNode);
                }
            }
            string Id = "";
            //模板文件 （如果删除的是模板文件,且在模板视图中未展开过,即没有子节点,则该节点直接删除）
            if (node is TmpltElementNode)
            {
                Id = ((TmpltSimpleExXmlElement)node.Element).Id;
            }
            else if (node is SnipElementNode)
            {//页面片
                Id = ((SnipXmlElement)node.Element).Id;
            }
            else if (node is PartElementNode)
            {//PART
                Id = ((SnipPartXmlElement)node.Element).SnipPartId;
            }

            /////从ElementNode的容器中删除
            TreeView.RemoveElementNode(Id);
            ((TreeNode)node).Remove();

            ///关闭当前节点打开的Form 在模板视图中,不能删除和重命名 所以不用关闭对应FORM
        }

        /// <summary>
        /// 对节点的TITLE重新赋值
        /// </summary>
        /// <param name="node"></param>
        public abstract void RenewNodeText(TmpltBaseTreeNode node);


        /// <summary>
        /// 在兄弟节点这一级上是否有重名,有返回TRUE,反之FALSE
        /// </summary>
        public bool BrotherNodeRepeatName(string strNodeText)
        {
            foreach (TreeNode treeNode in this.Parent.Nodes)
            {
                if (treeNode.Text.Equals(strNodeText, StringComparison.CurrentCultureIgnoreCase))
                    return true;//同级上有重复的
            }
            return false;
        }

        /// <summary>
        /// 修改NODE显示的TEXT
        /// </summary>
        /// <param name="strNewText"></param>
        public virtual void ChangeNodeText(string strNewText)
        {//对节点重命名暂时仅支持页面片的重命名！

            TmpltBaseTreeNode tmpltBaseTreeNode = this;
            //找到对应的模板1
            while (!(tmpltBaseTreeNode is TmpltElementNode))
            {
                tmpltBaseTreeNode = (TmpltBaseTreeNode)tmpltBaseTreeNode.Parent;
            }

            TmpltSimpleExXmlElement tmpltElement = tmpltBaseTreeNode.Element as TmpltSimpleExXmlElement;

            //得到对应的模板文件DOC
            TmpltXmlDocument tmpltDocument = tmpltElement.GetIndexXmlDocument();

            if (this is SnipElementNode)
            {
                SnipXmlElement snipElement = ((this as SnipElementNode).Element) as SnipXmlElement;
                snipElement.SnipName = strNewText;

                //通知页面设计器,更改FORM的TEXT
                string[] strArray = new string[2];
                strArray[0] = snipElement.Id;
                strArray[1] = strNewText;
                SdsiteXmlDocument.OnSnipDesignerFormTextChange(new EventArgs<string[]>(strArray));
            }
        }

        private void SetImageList()
        {
            Dictionary<string, Image> dic = ResourcesReader.ImageReader(this);
            foreach (var item in dic)
            {
                this._imageList.Images.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 设置TreeNode的三种状态的Image
        /// </summary>
        private void SetImage()
        {
            switch (NodeType)
            {
                case TreeNodeType.Tmplt:

                    break;
                case TreeNodeType.Snip:

                    break;
                case TreeNodeType.SnipPart:

                    break;
            }
        }
        #endregion
    }

    /// <summary>
    /// 模板视图树中节点类型
    /// </summary>
    public enum TmpltTreeNodeType
    {
        none = 0,

        //模板过滤
        generalTmplt = 1,//
        homeTmplt = 2,
        productTmplt = 3,
        knowledgeTmplt = 4,
        hrTmplt = 5,
        inviteBidTmplt = 6,
        projectTmplt = 7,

        //页面片过滤
        snipGeneral = 8,//普通型
        snipContent = 9,//正文型

        //PART过滤
        partStatic = 10,//静态PART
        partNavigation = 11,//导航
        //....
    }
}
