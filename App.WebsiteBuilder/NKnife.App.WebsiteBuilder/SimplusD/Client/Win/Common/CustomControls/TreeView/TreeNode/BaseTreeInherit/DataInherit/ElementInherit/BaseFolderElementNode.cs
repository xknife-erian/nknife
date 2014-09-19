using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class BaseFolderElementNode : ElementNode
    {
        public BaseFolderElementNode(SimpleExIndexXmlElement element)
            :base(element,true)
        {
        }

        public new FolderXmlElement Element
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

        /// <summary>
        /// 通过element添加ElementNode节点，并添加到树中
        /// </summary>
        public virtual ElementNode AddElementNode(SimpleExIndexXmlElement element)
        {
            ///IsDeleted为true的，表示已经被删除，不显示
            if (element.IsDeleted)
            {
                return null;
            }

            ElementNode elementNode = null;
            switch (element.DataType)
            {
                case DataType.Channel:
                    //右侧树或选择频道才加载频道
                    if (TreeView.TreeMode == TreeMode.General || TreeView.TreeMode == TreeMode.SelectChannel)
                    {
                        elementNode = new ChannelNode(element as ChannelSimpleExXmlElement);
                    }
                    break;
                case DataType.Tmplt:
                    //选择模板文件夹的树不加载模板文件
                    if (TreeView.TreeMode == TreeMode.SelectTmplt || TreeView.TreeMode == TreeMode.General)
                    {
                        elementNode = new TmpltNode(element as TmpltSimpleExXmlElement);
                    }
                    break;
                case DataType.Page:
                    //只有在右侧树中才显示页面
                    if (TreeView.TreeMode == TreeMode.General)
                    {
                        elementNode = new PageNode(element as PageSimpleExXmlElement);
                    }
                    break;
                case DataType.Resources:
                    //只有在右侧树中才显示资源文件夹
                    if (TreeView.TreeMode == TreeMode.General)
                    {
                        elementNode = new ResourceRootNode(element as ResourcesXmlElement);
                    }
                    break;
                case DataType.Folder:
                    {
                        switch (((FolderXmlElement)element).FolderType)
                        {
                            case FolderType.ChannelFolder:
                                //选择模板和选择模板文件夹的树不加载子频道
                                if (TreeView.TreeMode != TreeMode.SelectTmplt && TreeView.TreeMode != TreeMode.SelectTmpltFolder)
                                {
                                    elementNode = new ChannelFolderNode(element as FolderXmlElement);
                                }
                                break;
                            case FolderType.TmpltFolder:
                                elementNode = new TmpltFolderNode(element as FolderXmlElement);
                                break;
                            case FolderType.ResourcesFolder:
                                elementNode = new ResourceFolderNode(element as FolderXmlElement);
                                break;
                            default:
                                Debug.Fail("未处理的FolderType类型:" + ((FolderXmlElement)element).FolderType);
                                break;
                        }
                        
                        break;
                    }
                case DataType.File:
                    elementNode = new ResourceFileNode(element as FileSimpleExXmlElement);
                    break;
                case DataType.TmpltFolder:
                    //选择频道的树不加载模板
                    if (TreeView.TreeMode != TreeMode.SelectChannel)
                    {
                        elementNode = new TmpltRootNode(element as TmpltFolderXmlElement);
                    }
                    break;
                default:
                    Debug.Fail("未处理的Element的DataType:" + element.DataType);
                    break;
            }

            if (elementNode == null)
            {
                return null;
            }

            ///添加到树中
            if (elementNode is ResourceRootNode)
            {
                ///资源文件夹的根，添加到第一个
                this.AddChildNode(0, elementNode);

                ((RootChannelNode)this).ResourceRootNode = (ResourceRootNode)elementNode;
            }
            else if (elementNode is TmpltRootNode)
            {
                ///模板文件夹的根，添加到第二个
                this.AddChildNode(1, elementNode);

                ((RootChannelNode)this).TmpltRootNode = (TmpltRootNode)elementNode;
            }
            else
            {
                this.AddChildNode(elementNode);
            }

            ///调用LoadData()加载本身的数据
            elementNode.LoadData();

            ///添加到收藏夹
            if (elementNode.Element.IsFavorite)
            {
                if (TreeView.TreeMode == TreeMode.General)
                {
                    TreeView.AddLinkNodeToFavorite(elementNode);
                }
            }

            return elementNode;
        }
    }
}
