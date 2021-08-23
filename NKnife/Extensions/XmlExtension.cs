using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using NKnife.Util;

// ReSharper disable once CheckNamespace
namespace System.Xml
{
    public static class XmlExtension
    {
        /// <summary>
        ///     快速创建XML的XmlDeclaration结点
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="version">The version.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="standalone">The standalone.</param>
        public static void SetXmlDeclaration(this XmlDocument document, string version = "1.0", string encoding = "UTF-8", string standalone = "")
        {
            XmlDeclaration xmldecl = document.CreateXmlDeclaration(version, encoding, standalone);
            XmlElement root = document.DocumentElement;
            document.InsertBefore(xmldecl, root);
        }

        /// <summary>
        ///     根据XmlElement的LocalName获取一组XmlElement中的第一个Element
        /// </summary>
        /// <param name="node">将要查找的父级XmlNode</param>
        /// <param name="name">要查找的Element的LcoalName</param>
        /// <param name="ifNotCreateNew">没有这个节点，是否新建。默认false，如果没有找到该节点，则返加null，不创建。</param>
        /// <returns></returns>
        public static XmlElement GetElementByName(this XmlNode node, string name, bool ifNotCreateNew = false)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            XmlNode singleNode = node.SelectSingleNode($@"(.//{name})[1]");
            if (singleNode == null && ifNotCreateNew)
            {
                if (node.OwnerDocument != null)
                {
                    XmlElement ele = node.OwnerDocument.CreateElement(name);
                    node.AppendChild(ele);
                    return ele;
                }
            }
            return singleNode as XmlElement;
        }

        /// <summary>
        ///     如果Element有一个属性的名为“id”，根据XmlElement的LocalName和id的值获取一组XmlElement中的第一个Element
        /// </summary>
        /// <param name="node">将要查找的父级XmlNode</param>
        /// <param name="name">要查找的Element的LcoalName</param>
        /// <param name="id">id的值</param>
        /// <returns></returns>
        public static XmlElement GetElementById(this XmlNode node, string id, string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(id));
            Debug.Assert(!string.IsNullOrEmpty(name));
            //XPath如：(.//channel[@id='1234'])[1]
            return (XmlElement)node.SelectSingleNode(string.Format(@"(.//{1}[@id='{0}'])[1]", id, name));
        }

        /// <summary>
        ///     从XmlElement里查代一组值是否存在，返回不存在的值，已存在的值的不返回。
        ///     如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将搜索的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodeType">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="valueList">值的数组</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>返回一组不包含在父级Element中的值的数组</returns>
        public static string[] ContainsValuesByGroupItems(this XmlElement groupEle, string itemNodeName, XmlNodeType nodeType, string attributeName, params string[] valueList)
        {
            var t = new List<string>();
            t.AddRange(valueList);
            return ContainsValuesByGroupItems(groupEle, itemNodeName, nodeType, attributeName, t);
        }

        /// <summary>
        ///     从XmlElement里查代一组值是否存在，返回不存在的值，已存在的值的不返回。
        ///     如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将搜索的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodeType">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="valueList">值的集合</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>返回一组不包含在父级Element中的值的数组</returns>
        public static string[] ContainsValuesByGroupItems(this XmlElement groupEle, string itemNodeName, XmlNodeType nodeType, string attributeName, List<string> valueList)
        {
            var returnValues = new List<string>(valueList.Count);
            XmlNodeList nodes = groupEle.SelectNodes(itemNodeName);
            switch (nodeType)
            {
                #region case

                case XmlNodeType.Attribute:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(attributeName));
                        foreach (XmlNode node in nodes)
                        {
                            var ele = node as XmlElement;
                            string value = ele.GetAttribute(attributeName);
                            if (!valueList.Contains(value))
                            {
                                returnValues.Add(value);
                            }
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                case XmlNodeType.Text:
                    {
                        foreach (XmlNode node in nodes)
                        {
                            var ele = node as XmlElement;
                            string value = ele.InnerText;
                            if (!valueList.Contains(value))
                            {
                                returnValues.Add(value);
                            }
                        }
                        break;
                    }
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.EndElement:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.None:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;

                    #endregion
            }
            return returnValues.ToArray();
        }

        /// <summary>
        ///     从XmlElement里获取一组值。
        ///     如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">从此XmlElement里获取值</param>
        /// <param name="itemNodeName">子节点的LocalName</param>
        /// <param name="nodeType">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>一组值的字符串</returns>
        public static string[] GetGroupItemsValue(this XmlElement groupEle, string itemNodeName, XmlNodeType nodeType, string attributeName)
        {
            XmlNodeList nodes = groupEle.SelectNodes(itemNodeName);
            var returnItems = new string[nodes.Count];

            switch (nodeType)
            {
                #region case

                case XmlNodeType.Attribute:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(attributeName));
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.Attributes[attributeName].Value;
                            i++;
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                case XmlNodeType.Text:
                    {
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.InnerText;
                            i++;
                        }
                        break;
                    }
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.EndElement:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.None:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;

                    #endregion
            }

            return returnItems;
        }

        /// <summary>
        ///     向XmlElement里追加一组节点，并设置这组节点的值。
        ///     如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将设置的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodeType">数据存储的节点类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <param name="isRepeat">是否允许有重复的值,true允许,false不允许(如不允许将增加大量的运算时间)</param>
        /// <param name="valueList">值的数组</param>
        public static void AppendGroupItemsValue(this XmlElement groupEle, string itemNodeName, XmlNodeType nodeType, string attributeName, bool isRepeat, params string[] valueList)
        {
            var t = new List<string>();
            t.AddRange(valueList);
            AppendGroupItemsValue(groupEle, itemNodeName, nodeType, attributeName, isRepeat, t);
        }

        /// <summary>
        ///     向XmlElement里追加一组节点，并设置这组节点的值。
        ///     如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将设置的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodeType">数据存储的节点类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <param name="isRepeat">是否允许有重复的值,true允许,false不允许(如不允许将增加大量的运算时间)</param>
        /// <param name="valueList">值的集合</param>
        public static void AppendGroupItemsValue(this XmlElement groupEle, string itemNodeName, XmlNodeType nodeType, string attributeName, bool isRepeat, List<string> valueList)
        {
            switch (nodeType)
            {
                #region case

                case XmlNodeType.Attribute:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument?.CreateElement(itemNodeName);
                                item?.SetAttribute(attributeName, value);
                                if (item != null)
                                    groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            var vList = new List<string>(GetGroupItemsValue(groupEle, itemNodeName, nodeType, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    item.SetAttribute(attributeName, value);
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                XmlCDataSection cdata = groupEle.OwnerDocument.CreateCDataSection(value);
                                item.AppendChild(cdata);
                                groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            var vList = new List<string>(GetGroupItemsValue(groupEle, itemNodeName, nodeType, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    XmlCDataSection cdata = groupEle.OwnerDocument.CreateCDataSection(value);
                                    item.AppendChild(cdata);
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                case XmlNodeType.Text:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                item.InnerText = value;
                                groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            var vList = new List<string>(GetGroupItemsValue(groupEle, itemNodeName, nodeType, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    item.InnerText = value;
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;

                    #endregion
            }
        }

        /// <summary>
        ///     清除所有NodeType为Element的子节点
        /// </summary>
        /// <param name="node">所有子节点的父节点</param>
        public static void RemoveAllElements(this XmlNode node)
        {
            if (!(node is XmlElement) || node.ChildNodes.Count <= 0)
                return;
            var i = 0;
            while (node.ChildNodes.Count > i)
            {
                if (node.ChildNodes[i] is XmlElement)
                    node.RemoveChild(node.ChildNodes[i]);
                else
                    i++;
            }
        }

        /// <summary>
        ///     获得一个XmlNode的CData节点(当他有时，如无，将返回Null)
        /// </summary>
        /// <param name="childNode">The child node.</param>
        /// <returns></returns>
        public static XmlCDataSection GetCDataElement(this XmlNode childNode)
        {
            return childNode.ChildNodes
                .Cast<XmlNode>()
                .Where(xmlNode => xmlNode.NodeType == XmlNodeType.CDATA)
                .Cast<XmlCDataSection>()
                .FirstOrDefault();
        }

        /// <summary>
        ///     创建并追加XmlNode的CData节点
        /// </summary>
        /// <param name="childNode">The child node.</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetCDataElement(this XmlNode childNode, string value)
        {
            if (childNode.OwnerDocument != null)
            {
                XmlCDataSection cd = childNode.OwnerDocument.CreateCDataSection(value);
                childNode.AppendChild(cd);
            }
            else
            {
                Debug.Fail("childNode.OwnerDocument != null");
            }
        }

        /// <summary>
        ///     快速创建并追加一个普通的数据节点
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="localName">The localName.</param>
        /// <param name="value">The value.</param>
        /// <param name="attributes">属性集合</param>
        public static XmlElement SetChildElement(this XmlElement element, string localName, object value, params Tuple<string, string>[] attributes)
        {
            XmlElement ele = null;
            if (element != null && element.OwnerDocument != null)
            {
                ele = element.OwnerDocument.CreateElement(localName);
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                    ele.InnerText = value.ToString();
                if (!UtilCollection.IsNullOrEmpty(attributes))
                {
                    foreach (var attribute in attributes)
                    {
                        if (!ele.HasAttribute(attribute.Item1))
                            ele.SetAttribute(attribute.Item1, attribute.Item2);
                    }
                }
                element.AppendChild(ele);
            }
            return ele;
        }
    }
}
