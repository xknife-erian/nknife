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
        /// �Ƴ�ָ��Ԫ�ص������ӽڵ�
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
        /// ��XmlElement���ȡһ��ֵ���磺��groups����item value="a"����/item����item value="B"����/item����/groups��
        /// </summary>
        /// <param name="groupEle">�Ӵ�XmlElement���ȡֵ���������groupsԪ��</param>
        /// <param name="itemNodeName">item�����֣��������"item"</param>
        /// <param name="dataMode">���ݴ洢�����͡�������ItemsDataMode.ValueProperty</param>
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
                    throw new Exception("�����ڴ���δ֪��ItemsDataMode:" + dataMode);
            }

            return returnItems;
        }

        static public void SetGroupItems(XmlElement groupEle, string itemNodeName, ItemsDataMode dataMode,IEnumerable<string> values)
        {
            ///�����
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
                    throw new Exception("�����ڴ���δ֪��ItemsDataMode:" + dataMode);
            }
        }

        /// <summary>
        /// ͨ�����ڵ��ȡ�ӽڵ��ֵ���磺��option����partIdSeed��1��/partIdSeed����/option��
        /// </summary>
        /// <param name="parentEle">���ڵ㡣��������ģ�option�ڵ�</param>
        /// <param name="itemNodeName">�ӽڵ��������������"partIdSeed"</param>
        /// <param name="dataMode">���ݴ洢ģʽ����������ItemsDataMode.Text</param>
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
                    throw new Exception("�����ڴ���δ֪��ItemsDataMode:" + dataMode);
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
                    throw new Exception("�����ڴ���δ֪��ItemsDataMode:" + dataMode);
            }
        }

        /// <summary>
        /// �ڵ����ݵ����
        /// </summary>
        /// <param name="srcEle">Դ�ڵ�</param>
        /// <param name="targetEle">Ŀ��ڵ�</param>
        static public void CopyXmlElement(XmlElement srcEle, XmlElement targetEle)
        {
            Debug.Assert(srcEle != null);
            Debug.Assert(targetEle != null);

            ///���Ŀ��ڵ������
            targetEle.RemoveAll();

            ///��������(XmlAttribute)
            foreach (XmlAttribute srcAtt in srcEle.Attributes)
            {
                XmlAttribute targetAtt = targetEle.OwnerDocument.CreateAttribute(srcAtt.Name);
                targetAtt.Value = srcAtt.Value;

                targetEle.Attributes.Append(targetAtt);
            }

            ///��������
            targetEle.InnerXml = srcEle.InnerXml;
        }

        static public void CopyXmlDocument(XmlDocument srcDoc, XmlDocument targetDoc)
        {
            Debug.Assert(srcDoc != null);
            Debug.Assert(targetDoc != null);

            targetDoc.LoadXml(srcDoc.OuterXml);
        }

        /// <summary>
        /// ����ID
        /// </summary>
        /// <returns></returns>
        static public string CreateIncreaseId()
        {
            //todo:
            //lock (typeof(XmlUtilService))
            //{
            //    string strSeed = GetValue(Service.Sdsite.CurrentDocument.HelperData, "partIdSeed", ItemsDataMode.Text);
            //    strSeed = string.IsNullOrEmpty(strSeed) ? "0" : strSeed;
            //    ///�ڽڵ��л�ȡ����ֵ
            //    long seed = long.Parse(strSeed);

            //    ///�������ұ��浽�ļ���
            //    seed++;
            //    SetValue(Service.Sdsite.CurrentDocument.HelperData, "partIdSeed", ItemsDataMode.Text, seed.ToString());

            //    Service.Sdsite.CurrentDocument.Save();

            //    ///����
            //    return "ee_" + seed;
            //}
            return null;
        }

        /// <summary>
        /// �ж��Ƿ����ͬ��
        /// </summary>
        /// <param name="parentFolder">��Element</param>
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

        #region �����Զ�������Title

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
            ///ȡĬ��Title
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
                                Debug.Fail("δ�����TmpltType:" + tmpltType);
                                break;
                        }
                        break;
                    }
                case DataType.Page:
                    {
                        switch (pageType)
                        {
                            case PageType.None:
                                defaultTitle = "ҳ��";
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
                                Debug.Fail("δ�����PageType:" + pageType);
                                break;
                        }
                        break;
                    }
                case DataType.Folder:
                    defaultTitle = ResourceService.GetResourceText("Tree.MyTreeMenu.addFolder");
                    break;
                default:
                    Debug.Fail("δ֪��DataType:" + dataType);
                    break;
            }

            ///����������һ����������Title
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
        /// �Ƿ����ͬ��Title
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
                    Debug.Fail("δ֪��DataType:" + dataType);
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
        /// ֵ�洢�������������������value
        /// </summary>
        ValueProperty       = 1,
        /// <summary>
        /// ֵ�洢�ڽڵ���ı���
        /// </summary>
        Text                = 2,
        /// <summary>
        /// ֵ�洢�ڽڵ��CData��
        /// </summary>
        CData               = 3,
    }
}
