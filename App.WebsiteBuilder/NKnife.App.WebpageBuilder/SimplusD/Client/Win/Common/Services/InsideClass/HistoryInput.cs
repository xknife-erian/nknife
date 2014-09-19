using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class HistoryInput
        {
            static XmlDocument _histotyInputRecord;
            static string _historyInputFile;

            /// <summary>
            /// 构造函数:加载文件
            /// </summary>
            static HistoryInput()
            {
                _historyInputFile = PathService.Config_HistoryInputRecord;
                _histotyInputRecord = new XmlDocument();
                if (File.Exists(_historyInputFile))
                {
                    try
                    {
                        _histotyInputRecord.Load(_historyInputFile);
                    }
                    catch
                    {
                        File.Delete(_historyInputFile);
                        throw;
                    }
                }
                else
                {
                    /////如果加载此xml文件出了异常，则删除此文件
                    //Service.Exception.ShowException(ex); 
                    _histotyInputRecord.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
                        <recoders>
                            <alias>
                            </alias>
                            <contentSource>
                            </contentSource>
                            <modifyBy>
                            </modifyBy>
                        </recoders>");
                }
            }
            /// <summary>
            /// 取出当前用户之前输入的相应文本
            /// </summary>
            /// <returns></returns>
            static public List<string> GetValues(HistoryInputRecordType rt)
            {
                XmlNodeList xnl = null;
                List<string> list = new List<string>();

                switch (rt)
                {
                    case HistoryInputRecordType.CreaterBy:
                        xnl = _histotyInputRecord.SelectSingleNode("recoders/alias").ChildNodes;
                        break;
                    case HistoryInputRecordType.ContentSource:
                        xnl = _histotyInputRecord.SelectSingleNode("recoders/contentSource").ChildNodes;
                        break;
                    case HistoryInputRecordType.ModifyBy:
                        xnl = _histotyInputRecord.SelectSingleNode("recoders/modifyBy").ChildNodes;
                        break;
                    default:
                        Debug.Fail("");
                        break;
                }

                if (xnl != null)
                {
                    foreach (XmlNode node in xnl)
                    {
                        list.Add(node.InnerText);
                    }
                }

                return list;
            }
            /// <summary>
            /// 将用户输入的值放入文件中
            /// </summary>
            /// <param name="text">传入的值</param>
            /// <param name="rt">当前操作的哪种类型</param>
            static public void AddValue(string text, HistoryInputRecordType rt)
            {
                XmlNode node = null;
                string nodeName = string.Empty;
                string xPath = string.Empty;
                switch (rt)
                {
                    case HistoryInputRecordType.CreaterBy:
                        node = _histotyInputRecord.SelectSingleNode("recoders/alias");
                        nodeName = "name";
                        xPath = string.Format("/recoders/alias/name/text()[string() = '{0}']/parent::node()", text);
                        break;
                    case HistoryInputRecordType.ContentSource:
                        node = _histotyInputRecord.SelectSingleNode("recoders/contentSource");
                        nodeName = "source";
                        xPath = string.Format("/recoders/contentSource/source/text()[string() = '{0}']/parent::node()", text);
                        break;
                    case HistoryInputRecordType.ModifyBy:
                        node = _histotyInputRecord.SelectSingleNode("recoders/modifyBy");
                        nodeName = "name";
                        xPath = string.Format("/recoders/modifyBy/name/text()[string() = '{0}']/parent::node()", text);
                        break;
                    default:
                        Debug.Fail("");
                        break;
                }

                if (!node.HasChildNodes)
                {
                    XmlElement ele = _histotyInputRecord.CreateElement(nodeName);
                    ele.InnerText = text;
                    node.AppendChild(ele);
                }
                else
                {
                    ///通过text查找相应的节点是否存在,存在则删除，后在创建，不存在则创建
                    XmlNode textNode = node.SelectSingleNode(xPath);
                    if (textNode != null)
                    {
                        node.RemoveChild(textNode);
                    }
                    XmlElement element = _histotyInputRecord.CreateElement(nodeName);
                    element.InnerText = text;

                    XmlNode FirChild = node.FirstChild;
                    node.InsertBefore(element, FirChild);
                }

                _histotyInputRecord.Save(_historyInputFile);
            }

        }

    }
    public enum HistoryInputRecordType
    {
        /// <summary>
        /// 作者（創建者）别名
        /// </summary>
        CreaterBy,
        /// <summary>
        /// 修改者別名
        /// </summary>
        ModifyBy,
        /// <summary>
        /// 文章來源
        /// </summary>
        ContentSource,
    }
}