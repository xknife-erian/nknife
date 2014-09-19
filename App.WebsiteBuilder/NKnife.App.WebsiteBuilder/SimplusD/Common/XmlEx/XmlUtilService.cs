using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace Jeelu.SimplusD
{
    static public class XmlUtilService
    {
        /// <summary>
        /// 移除指定元素的所有子节点
        /// </summary>
        /// <param name="ele"></param>
        static public void RemoveAllChilds(XmlElement ele)
        {
            while (ele.HasChildNodes)
            {
                ele.RemoveChild(ele.FirstChild);
            }
        }

        /// <summary>
        /// 从XmlElement里获取一组值。如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">从此XmlElement里获取值。即上面的groups元素</param>
        /// <param name="itemNodeName">item的名字，即上面的"item"</param>
        /// <param name="dataMode">数据存储的类型。上面是ItemsDataMode.ValueProperty</param>
        /// <returns></returns>
        static public string[] GetGroupItems(XmlElement groupEle, string itemNodeName, ItemsDataMode dataMode)
        {
            XmlNodeList nodes = groupEle.SelectNodes(itemNodeName);
            string[] returnItems = new string[nodes.Count];

            switch (dataMode)
            {
                case ItemsDataMode.ValueProperty:
                    {
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.Attributes["value"].Value;
                            i++;
                        }
                        break;
                    }
                case ItemsDataMode.Text:
                    {
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.InnerText;
                            i++;
                        }
                        break;
                    }
                case ItemsDataMode.CData:
                    {
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.InnerText;
                            i++;
                        }
                        break;
                    }
                default:
                    throw new Exception("开发期错误。未知的ItemsDataMode:" + dataMode);
            }

            return returnItems;
        }

        static public void SetGroupItems(XmlElement groupEle, string itemNodeName, ItemsDataMode dataMode,IEnumerable<string> values)
        {
            ///先清空
            RemoveAllChilds(groupEle);

            switch (dataMode)
            {
                case ItemsDataMode.ValueProperty:
                    foreach (string value in values)
                    {
                        XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                        item.SetAttribute("value", value);
                        groupEle.AppendChild(item);
                    }
                    break;
                case ItemsDataMode.Text:
                    foreach (string value in values)
                    {
                        XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                        item.InnerText = value;
                        groupEle.AppendChild(item);
                    }
                    break;
                case ItemsDataMode.CData:
                    foreach (string value in values)
                    {
                        XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                        XmlCDataSection cdata = groupEle.OwnerDocument.CreateCDataSection(value);
                        item.AppendChild(cdata);
                        groupEle.AppendChild(item);
                    }
                    break;
                default:
                    throw new Exception("开发期错误。未知的ItemsDataMode:" + dataMode);
            }
        }

        /// <summary>
        /// 通过父节点获取子节点的值。如：＜option＞＜partIdSeed＞1＜/partIdSeed＞＜/option＞
        /// </summary>
        /// <param name="parentEle">父节点。即例子里的：option节点</param>
        /// <param name="itemNodeName">子节点名。即例子里的"partIdSeed"</param>
        /// <param name="dataMode">数据存储模式。例子里是ItemsDataMode.Text</param>
        /// <returns></returns>
        static public string GetValue(XmlElement parentEle, string itemNodeName, ItemsDataMode dataMode)
        {
            XmlElement node = (XmlElement)parentEle.SelectSingleNode(itemNodeName);
            if (node == null)
            {
                return "";
            }
            switch (dataMode)
            {
                case ItemsDataMode.ValueProperty:
                    return node.GetAttribute("value");

                case ItemsDataMode.Text:
                    return node.InnerText;

                case ItemsDataMode.CData:
                    return node.InnerText;

                default:
                    throw new Exception("开发期错误。未知的ItemsDataMode:" + dataMode);
            }
        }
        static public void SetValue(XmlElement parentEle, string itemNodeName, ItemsDataMode dataMode,string value)
        {
            XmlElement node = (XmlElement)parentEle.SelectSingleNode(itemNodeName);
            if (node == null)
            {
                node = parentEle.OwnerDocument.CreateElement(itemNodeName);
                parentEle.AppendChild(node);
            }

            switch (dataMode)
            {
                case ItemsDataMode.ValueProperty:
                    node.SetAttribute("value",value);
                    break;

                case ItemsDataMode.Text:
                    node.InnerText = value;
                    break;

                case ItemsDataMode.CData:
                    RemoveAllChilds(node);
                    XmlCDataSection cdata = parentEle.OwnerDocument.CreateCDataSection(value);
                    node.AppendChild(cdata);
                    break;

                default:
                    throw new Exception("开发期错误。未知的ItemsDataMode:" + dataMode);
            }
        }

        /// <summary>
        /// 节点数据的深拷贝
        /// </summary>
        /// <param name="srcEle">源节点</param>
        /// <param name="targetEle">目标节点</param>
        static public void CopyXmlElement(XmlElement srcEle, XmlElement targetEle)
        {
            Debug.Assert(srcEle != null);
            Debug.Assert(targetEle != null);

            ///清空目标节点的内容
            targetEle.RemoveAll();

            ///拷贝属性(XmlAttribute)
            foreach (XmlAttribute srcAtt in srcEle.Attributes)
            {
                XmlAttribute targetAtt = targetEle.OwnerDocument.CreateAttribute(srcAtt.Name);
                targetAtt.Value = srcAtt.Value;

                targetEle.Attributes.Append(targetAtt);
            }

            ///拷贝内容
            targetEle.InnerXml = srcEle.InnerXml;
        }

        static public void CopyXmlDocument(XmlDocument srcDoc, XmlDocument targetDoc)
        {
            Debug.Assert(srcDoc != null);
            Debug.Assert(targetDoc != null);

            targetDoc.LoadXml(srcDoc.OuterXml);
        }

        /// <summary>
        /// 创建ID
        /// </summary>
        /// <returns></returns>
        static public string CreateIncreaseId()
        {
            //todo:
            //lock (typeof(XmlUtilService))
            //{
            //    string strSeed = GetValue(Service.Sdsite.CurrentDocument.HelperData, "partIdSeed", ItemsDataMode.Text);
            //    strSeed = string.IsNullOrEmpty(strSeed) ? "0" : strSeed;
            //    ///在节点中获取种子值
            //    long seed = long.Parse(strSeed);

            //    ///递增，且保存到文件中
            //    seed++;
            //    SetValue(Service.Sdsite.CurrentDocument.HelperData, "partIdSeed", ItemsDataMode.Text, seed.ToString());

            //    Service.Sdsite.CurrentDocument.Save();

            //    ///返回
            //    return "ee_" + seed;
            //}
            return null;
        }

        /// <summary>
        /// 判断是否存在同名
        /// </summary>
        /// <param name="parentFolder">父Element</param>
        static public bool ExistsSameFileName(FolderXmlElement parentFolder, DataType dataType, string newName)
        {
            string newFilePath = Path.Combine(parentFolder.AbsoluteFilePath, newName);

            switch (dataType)
            {
                case DataType.Tmplt:
                    return File.Exists(newFilePath + Utility.Const.TmpltFileExt);

                case DataType.Page:
                    return File.Exists(newFilePath + Utility.Const.PageFileExt);

                case DataType.File:
                    return File.Exists(newFilePath);

                case DataType.Channel:
                case DataType.Folder:
                    return Directory.Exists(newFilePath);

                default:
                    Debug.Fail("");
                    break;
            }

            return true;
        }

        #region 生成自动递增的Title

        static public string CreateIncreaseChannelTitle(FolderXmlElement parentFolder)
        {
            return CreateIncreaseTitleCore(parentFolder, DataType.Channel, TmpltType.None, PageType.None);
        }
        static public string CreateIncreaseFolderTitle(FolderXmlElement parentFolder)
        {
            return CreateIncreaseTitleCore(parentFolder, DataType.Folder, TmpltType.None, PageType.None);
        }
        static public string CreateIncreasePageTitle(FolderXmlElement parentFolder,PageType pageType)
        {
            return CreateIncreaseTitleCore(parentFolder, DataType.Page, TmpltType.None, pageType);
        }
        static public string CreateIncreaseTmpltTitle(FolderXmlElement parentFolder,TmpltType tmpltType)
        {
            return CreateIncreaseTitleCore(parentFolder, DataType.Tmplt, tmpltType, PageType.None);
        }

        static private string CreateIncreaseTitleCore(FolderXmlElement parentFolder, DataType dataType, TmpltType tmpltType, PageType pageType)
        {
            ///取默认Title
            string defaultTitle = "";
            switch (dataType)
            {
                case DataType.Channel:
                    defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.newChannel");
                    break;
                case DataType.Tmplt:
                    {
                        switch (tmpltType)
                        {
                            case TmpltType.General:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.newTmplt");
                                break;
                            case TmpltType.Product:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.productTmplt");
                                break;
                            case TmpltType.Project:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.projectTmplt");
                                break;
                            case TmpltType.InviteBidding:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.inviteBiddingTmplt");
                                break;
                            case TmpltType.Knowledge:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.knowledgeTmplt");
                                break;
                            case TmpltType.Hr:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.HRTmplt");
                                break;
                            case TmpltType.Home:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.homeTmplt");
                                break;
                            default:
                                Debug.Fail("未处理的TmpltType:" + tmpltType);
                                break;
                        }
                        break;
                    }
                case DataType.Page:
                    {
                        switch (pageType)
                        {
                            case PageType.None:
                                defaultTitle = "页面";
                                break;
                            case PageType.General:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.newPage");
                                break;
                            case PageType.Product:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.productPage");
                                break;
                            case PageType.Project:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.projectPage");
                                break;
                            case PageType.InviteBidding:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.inviteBiddingPage");
                                break;
                            case PageType.Knowledge:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.knowledgePage");
                                break;
                            case PageType.Hr:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.HRPage");
                                break;
                            case PageType.Home:
                                defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.homePage");
                                break;
                            default:
                                Debug.Fail("未处理的PageType:" + pageType);
                                break;
                        }
                        break;
                    }
                case DataType.Folder:
                    defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.addFolder");
                    break;
                default:
                    Debug.Fail("未知的DataType:" + dataType);
                    break;
            }

            ///遍历以生成一个不重名的Title
            for (int i = 1; i < int.MaxValue; i++)
            {
                string newTitle = defaultTitle + i;
                if (!ExistsSameTitle(parentFolder, dataType, newTitle))
                {
                    return newTitle;
                }
            }

            return defaultTitle + Utility.Guid.NewGuid();
        }

        /// <summary>
        /// 是否存在同名Title
        /// </summary>
        /// <param name="parentFolder"></param>
        /// <param name="dataType"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        static private bool ExistsSameTitle(FolderXmlElement parentFolder, DataType dataType,string title)
        {
            switch (dataType)
            {
                case DataType.Channel:
                case DataType.Folder:
                    foreach (XmlNode node in parentFolder.ChildNodes)
                    {
                        if (node is FolderXmlElement)
                        {
                            if (((SimpleExIndexXmlElement)node).Title == title)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case DataType.Tmplt:
                    foreach (XmlNode node in parentFolder.ChildNodes)
                    {
                        if (node is TmpltSimpleExXmlElement)
                        {
                            if (((SimpleExIndexXmlElement)node).Title == title)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case DataType.Page:
                    foreach (XmlNode node in parentFolder.ChildNodes)
                    {
                        if (node is PageSimpleExXmlElement)
                        {
                            if (((SimpleExIndexXmlElement)node).Title == title)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case DataType.File:
                    foreach (XmlNode node in parentFolder.ChildNodes)
                    {
                        if (node is FileSimpleExXmlElement)
                        {
                            if (((SimpleExIndexXmlElement)node).Title == title)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                default:
                    Debug.Fail("未知的DataType:" + dataType);
                    break;
            }
            return false;
        }

        #endregion
    }

    public enum ItemsDataMode
    {
        Unknown             = 0,
        /// <summary>
        /// 值存储在属性里，属性名必须是value
        /// </summary>
        ValueProperty       = 1,
        /// <summary>
        /// 值存储在节点的文本里
        /// </summary>
        Text                = 2,
        /// <summary>
        /// 值存储在节点的CData里
        /// </summary>
        CData               = 3,
    }
}
