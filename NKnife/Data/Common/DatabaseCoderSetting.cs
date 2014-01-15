using System.Collections.Generic;
using System.Xml;
using NKnife.Configuring.CoderSetting;

namespace Gean.Data.Common
{
    public abstract class DatabaseCoderSetting : XmlCoderSetting
    {
        public Dictionary<string, DatabaseParms> ConnectionParmsMap { get; private set; }

        protected override void Load(XmlElement source)
        {
            ConnectionParmsMap = new Dictionary<string, DatabaseParms>();
            var nodelist = source.SelectNodes("Database");
            if (nodelist == null)
                return;
            //一个Database节点代表一个数据库的配置信息
            foreach (XmlNode xmlnode in nodelist)
            {
                if (xmlnode.NodeType != XmlNodeType.Element)
                    continue;
                var element = (XmlElement) xmlnode;
                if (!element.HasAttributes || !element.HasAttribute("Name"))
                    continue;
                var parms = new DatabaseParms();
                foreach (XmlNode itemNode in element.ChildNodes)
                {
                    if (itemNode.LocalName.Equals("Schema"))
                        // 解析数据库相关的一些原始SQL语句
                        SchemaParse(parms, (XmlElement)itemNode);
                    // 每一个节点代表一个数据库相关的参数，例：<pooling>true</pooling>
                    string key = itemNode.LocalName.Trim();
                    string value = itemNode.InnerText.Trim();
                    parms.Configs.Add(key, value);
                }
                ConnectionParmsMap.Add(element.GetAttribute("Name"), parms);
            }
        }

        /// <summary>解析数据库相关的一些原始SQL语句
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="itemEle"></param>
        private static void SchemaParse(DatabaseParms parms, XmlElement itemEle)
        {
            foreach (XmlNode item in itemEle.ChildNodes)
            {
                if (item.NodeType != XmlNodeType.Element)
                    continue;
                switch (item.LocalName.ToLower())
                {
                    #region case

                    case "table": //创建表
                        parms.Table = item.InnerText;
                        break;
                    case "index": //索引
                        parms.Index = item.InnerText;
                        break;
                    case "view": //视图
                        {
                            parms.View = new List<string>();
                            foreach (XmlNode view in item.ChildNodes)
                                parms.View.Add(view.InnerText.Trim());
                            break;
                        }
                    case "trigger": //触发器
                        {
                            parms.OnTable = new List<string>();
                            foreach (XmlNode view in item.ChildNodes)
                                parms.View.Add(view.InnerText.Trim());
                            break;
                        }
                    default:
                        break;

                    #endregion
                }
            }
            return;
        }
    }
}