using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class OptionData
    {
        /// <summary>
        /// 存储选项窗体所需的语言文字
        /// </summary>
        static internal Dictionary<string, string> TextDic;

        private XmlDocument _OptionXmlDocument = null;

        internal void Load()
         {
             _OptionXmlDocument = SoftwareOption.SoftOptionXmlDocument;
            if (TextDic == null)
            {
                TextDic = this.FillTextDic();
            }
        }

        /// <summary>
        /// 从选项文件中将所有的语言文本填充入Dictionary
        /// </summary>
        private Dictionary<string, string> FillTextDic()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            XmlElement textEle = (XmlElement)_OptionXmlDocument.DocumentElement.SelectSingleNode("//texts");

            foreach (XmlNode node in textEle.ChildNodes)
            {
                XmlElement ele = (XmlElement)node;
                string key = ele.GetAttribute("name");
                string textValue = ele.InnerText;
                dic.Add(key, textValue);
            }
            return dic;
        }

        public void Save()
        {
            SoftwareOption.Save();
            //_OptionXmlDocument.Save(PathService.Config_GlobalSetting);
        }
        /// <summary>
        /// 设置选项保存时间
        /// </summary>
        public DateTime SaveTime
        {
            get { return Utility.Convert.StringToDateTime(_OptionXmlDocument.DocumentElement.GetAttribute("saveTime")); }
            set { _OptionXmlDocument.DocumentElement.SetAttribute("saveTime", value.ToString()); }
        }

        /// <summary>
        /// 获取用于显示树视图的集合
        /// </summary>
        /// <returns></returns>
        internal TreeNode[] ToTreeNode()
         {
            TreeNode treeNode = new TreeNode();
            XmlElement optionEle = _OptionXmlDocument.DocumentElement;
            ReadOptionNode(optionEle, treeNode);

            TreeNode[] treeNodeColl = new TreeNode[treeNode.Nodes.Count];
            treeNode.Nodes.CopyTo(treeNodeColl, 0);

            return treeNodeColl;
        }

        public Dictionary<string, AutoLayoutPanel> OptionPanelDic = new Dictionary<string, AutoLayoutPanel>();
        Type[] types = typeof(SoftwareOption).Assembly.GetTypes();

        /// <summary>
        /// 读取文件中的选项内容,主用于显示树视图
        /// </summary>
        private void ReadOptionNode(XmlElement element, TreeNode parentTreeNode)
        {
            if (element.HasChildNodes)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node.Name == "option" || node.Name == "items")
                    {
                        string panelName = node.Attributes["name"].Value;
                        string nodeName = node.Attributes["text"].Value;
                        
                        TreeNode treeNode = new TreeNode(nodeName);
                        treeNode.Name = panelName;

                        AutoLayoutPanel autoPanel = null;
                        if (!OptionPanelDic.TryGetValue(panelName, out autoPanel))
                        {
                            foreach (Type type in types)
                            {
                                if (type.IsClass)
                                {
                                    object[] atts = type.GetCustomAttributes(typeof(SoftOptionClassAttribute), false);
                                    if (atts.Length > 0 && ((SoftOptionClassAttribute)atts[0]).PanelName == panelName)
                                    {
                                        // autoPanel = AutoLayoutPanel.CreatePanel(typeof(SoftOptionClassAttribute), PathService.CL_DataSources_Folder, type);
                                        autoPanel = AutoLayoutPanelEx.CreatePanel(typeof(PropertyPadAttribute), type,false,true);
                                        OptionPanelDic.Add(panelName, autoPanel);
                                    }
                                }
                            }
                            
                        }
                        treeNode.Tag = autoPanel;

                        ///添加节点
                        parentTreeNode.Nodes.Add(treeNode);
                        if (node.HasChildNodes)
                        {
                            ReadOptionNode((XmlElement)node, treeNode);
                        }
                    }
                }
            }

        }//ReadOptionNode

    }
}
