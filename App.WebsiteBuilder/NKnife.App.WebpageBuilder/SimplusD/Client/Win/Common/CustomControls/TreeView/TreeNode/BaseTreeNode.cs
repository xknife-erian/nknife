using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseTreeNode : TreeNode
    {
        /// <summary>
        /// TreeNode的扩展。在TreeViewEx里必须使用BaseTreeNode或其派生类。
        /// </summary>
        public BaseTreeNode()
        {
        }

        public abstract TreeNodeType NodeType { get; }

        public new TreeViewEx TreeView
        {
            get
            {
                return (TreeViewEx)base.TreeView;
            }
        }

        public new BaseTreeNode Parent
        {
            get
            {
                return (BaseTreeNode)base.Parent;
            }
        }

        private bool _enabled = true;
        /// <summary>
        /// 获取或设置是否可用.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;

                if (TreeView != null)
                {
                    TreeView.InitNode(this);
                }
            }
        }

        /// <summary>
        /// 加载节点本身的数据。若需要加载子节点，则在派生类的实现里需要调用LoadChildNodes()
        /// </summary>
        public abstract void LoadData();

        /// <summary>
        /// 加载子节点
        /// </summary>
        protected abstract void LoadChildNodes();

        public string FactImageKey
        {
            get
            {
                string strKey = null;
                if (this.IsExpanded)
                {
                    strKey = ExpandImageKey;
                }
                else
                {
                    strKey = CollapseImageKey;
                }

                ///读取ServerState状态
                ServerState state = ServerState.None;
                if (this is ElementNode)
                {
                    state = ((ElementNode)this).ServerState;
                }

                string newKey = Service.Draw.GetServerSignKey(strKey, state);
                if (!TreeView.ImageList.Images.ContainsKey(newKey))
                {
                    ///若是资源文件节点，则先确定图标已经存在
                    if (this is ResourceFileNode)
                    {
                        ResourceFileNode fileNode = (ResourceFileNode)this;
                        fileNode.InsureIcon();
                    }

                    Image oldImg = ResourceService.MainImageList.Images[strKey];
                    Image newImg = Service.Draw.DrawServerSign(oldImg, state);

                    TreeView.ImageList.Images.Add(newKey, newImg);
                }
                return newKey;
            }
        }

        /// <summary>
        /// 折叠时的图标(ImageKey)
        /// </summary>
        public abstract string CollapseImageKey { get; }

        /// <summary>
        /// 展开时的图标(ImageKey)
        /// </summary>
        public virtual string ExpandImageKey
        {
            get
            {
                return CollapseImageKey;
            }
        }

        /// <summary>
        /// 添加Node到Nodes集合的方法。为使我们能在添加时做一些统一的处理，
        /// 所有的节点添加必须使用此方法，若派生类重写此方法，必须最终调用base.AddChildNode
        /// </summary>
        public int AddChildNode(BaseTreeNode node)
        {
            return AddChildNode(-1, node);
        }

        public virtual int AddChildNode(int index, BaseTreeNode node)
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
            node.SelectedImageKey = node.ImageKey = node.FactImageKey;

            ///初始化节点状态
            TreeView.InitNode(node);

            ///检查文件是否正常
            if (node is ElementNode)
            {
                ElementNode elementNode = (ElementNode)node;

                ///添加到TreeNode的dic容器里去
                if (TreeView.TreeMode == TreeMode.General && elementNode.NodeType != TreeNodeType.Link)
                {
                    TreeView.SetElementNode(elementNode);
                }

                ///检查文件是否存在：不存在，则在图标上画一个感叹号
                if (!Utility.File.Exists(elementNode.Element.AbsoluteFilePath))
                {
                    string signKey = Service.Draw.GetSignKey(node.FactImageKey, SignType.ExcalmatoryPoint);
                    if (!this.TreeView.ImageList.Images.ContainsKey(signKey))
                    {
                        ///若是资源文件节点，则先确定图标已经存在
                        if (elementNode is ResourceFileNode)
                        {
                            ResourceFileNode fileNode = (ResourceFileNode)elementNode;
                            fileNode.InsureIcon();
                        }

                        ///画标记
                        Image signImage = this.TreeView.ImageList.Images[node.FactImageKey];
                        Service.Draw.DrawSign(signImage, SignType.ExcalmatoryPoint);
                        this.TreeView.ImageList.Images.Add(signKey, signImage);
                    }

                    node.SelectedImageKey = node.ImageKey = signKey;

                    if (elementNode.IsFolder)
                    {
                        node.ToolTipText = "此文件夹不存在！";
                    }
                    else
                    {
                        node.ToolTipText = "此文件不存在！";
                    }
                }
                else
                {
                    bool isHealthy = true;
                    switch (elementNode.Element.DataType)
                    {
                        ///检查非首页型模板是否有正文型页面片
                        case DataType.Tmplt:
                            {
                                TmpltSimpleExXmlElement tmpltEle = elementNode.Element as TmpltSimpleExXmlElement;
                                if (tmpltEle.TmpltType != TmpltType.Home && !tmpltEle.HasContentSnip)
                                {
                                    isHealthy = false;
                                    elementNode.ToolTipText = "此模板没有正文型页面片。属于不完整模板。\r\n请为此模板创建正文型页面片，否则关联到它的页面无法正常生成。";
                                }
                                break;
                            }
                        ///检查页面是否正确关联了模板
                        case DataType.Page:
                            {
                                PageSimpleExXmlElement pageEle = elementNode.Element as PageSimpleExXmlElement;
                                if (string.IsNullOrEmpty(pageEle.TmpltId))
                                {
                                    isHealthy = false;
                                    elementNode.ToolTipText = "此页面没有设置关联模板，属于不完整页面。\r\n请重新选择关联模板，否则将无法生成最终页面。";
                                }
                                else
                                {
                                    TmpltSimpleExXmlElement tmpltEle = Service.Sdsite.CurrentDocument.GetTmpltElementById(pageEle.TmpltId);
                                    if (tmpltEle == null ||
                                        !File.Exists(tmpltEle.AbsoluteFilePath))
                                    {
                                        isHealthy = false;
                                        elementNode.ToolTipText = "此页面关联的模板未能找到，属于不完整页面。\r\n请重新选择关联模板，否则无法生成最终页面。";
                                    }
                                    else
                                    {
                                        isHealthy = true;
                                        //ElementNode tmpltNode = TreeView.GetElementNode(tmpltEle.Id);
                                        //if (tmpltNode != null)
                                        //{
                                        //    elementNode.ToolTipText = "此页面完整,关联的模板是:" + tmpltNode.FullPath + "。\r\n可生成最终页面。";
                                        //}
                                    }
                                }
                                break;
                            }
                    }

                    ///不健康节点，在图标上添加标记
                    if (!isHealthy)
                    {
                        string signKey = Service.Draw.GetSignKey(node.FactImageKey, SignType.QuestionPoint);
                        if (!this.TreeView.ImageList.Images.ContainsKey(signKey))
                        {
                            Image signImage = this.TreeView.ImageList.Images[node.FactImageKey];
                            Service.Draw.DrawSign(signImage, SignType.QuestionPoint);
                            this.TreeView.ImageList.Images.Add(signKey, signImage);
                        }

                        node.SelectedImageKey = node.ImageKey = signKey;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 与RemoveChildNodeTemp成对出现
        /// </summary>
        public int AddChildNodeTemp(BaseTreeNode node)
        {
            return AddChildNodeTemp(-1, node);
        }
        /// <summary>
        /// 与RemoveChildNodeTemp成对出现
        /// </summary>
        public int AddChildNodeTemp(int index, BaseTreeNode node)
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

            return result;
        }

        /// <summary>
        /// 从Nodes集合移除指定Node。为使我们能在删除时做一些统一的处理，
        /// 所有的节点删除必须使用此方法，若派生类重写此方法，必须最终调用base.RemoveChildNode
        /// </summary>
        public virtual void RemoveChildNode(BaseTreeNode node)
        {
            ///若是ElementNode，且不是链接节点，则删除ElementNode的容器中的缓存
            if (node is ElementNode && node.NodeType != TreeNodeType.Link)
            {
                ElementNode elementNode = (ElementNode)node;

                ///先删除其子节点
                while (node.Nodes.Count > 0)
                {
                    this.RemoveChildNode((BaseTreeNode)node.FirstNode);
                }

                ///若在收藏夹中，则先移出来
                if (elementNode.Element.IsFavorite)
                {
                    Service.Sdsite.CurrentDocument.RemoveFavorite(elementNode.Element);
                }

                ///从ElementNode的容器中删除
                TreeView.RemoveElementNode(elementNode.Element.Id);

                ///关闭当前节点打开的Form
                Service.Workbench.CloseWorkDocumentWithoutSave(elementNode.Element.Id,
                    Service.Workbench.GetWorkDocumentType(elementNode.Element));
            }

            ///删除的节点需要去除其选择
            if (TreeView.SelectedNodes.Contains(node))
            {
                TreeView.SelectedNodes.Remove(node);
            }

            ((TreeNode)node).Remove();
        }

        /// <summary>
        /// 临时删除，一般供移动时使用
        /// </summary>
        public void RemoveChildNodeTemp(BaseTreeNode node)
        {
            ((TreeNode)node).Remove();
        }

        /// <summary>
        /// 此方法最终调用RemoveChildNode方法
        /// </summary>
        public void RemoveChildNodeAt(int index)
        {
            BaseTreeNode node = (BaseTreeNode)this.Nodes[index];
            RemoveChildNode(node);
        }

        /// <summary>
        /// 否决的。隐藏此方法，推荐使用RemoveChildNode方法
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Bindable(false), Browsable(false)]
        public new void Remove()
        {
            Debug.Fail("否决的。隐藏此方法，推荐使用RemoveChildNode方法");

            if (Parent != null)
            {
                Parent.RemoveChildNode(this);
            }
            else
            {
                base.Remove();
            }
        }

        /// <summary>
        /// 将当前节点的文本加粗显示
        /// </summary>
        public void BoldFont()
        {
            this.NodeFont = new Font(this.TreeView.Font, FontStyle.Bold);
        }

        #region 由定制特性完成的一系列判断

        protected TreeNodeAttribute GetTreeNodeAttribute()
        {
            object[] atts = this.GetType().GetCustomAttributes(typeof(TreeNodeAttribute), false);
            if (atts.Length > 0)
            {
                return (TreeNodeAttribute)atts[0];
            }
            return null;
        }

        public virtual bool CanRename
        {
            get { return GetTreeNodeAttribute().CanRename; }
        }

        public virtual bool CanDragDrop
        {
            get { return GetTreeNodeAttribute().CanDragDrop; }
        }

        public virtual bool CanDelete
        {
            get { return GetTreeNodeAttribute().CanDelete; }
        }

        public virtual bool IsBranch
        {
            get { return GetTreeNodeAttribute().IsBranch; }
        }

        public virtual bool IsDockExpand
        {
            get { return GetTreeNodeAttribute().IsDockExpand; }
        }

        public virtual bool IsGrandchildNoOrder
        {
            get { return GetTreeNodeAttribute().IsGrandchildNoOrder; }
        }

        public virtual TreeNodeType AcceptDragDropType
        {
            get { return GetTreeNodeAttribute().AcceptDragDropType; }
        }

        public virtual DragDropEffects AcceptEffects
        {
            get { return GetTreeNodeAttribute().AcceptEffects; }
        }

        #endregion
    }
}
