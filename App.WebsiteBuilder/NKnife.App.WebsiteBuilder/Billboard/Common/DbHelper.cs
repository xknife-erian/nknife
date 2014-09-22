using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Data;
using System.Data;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    public class DbHelper
    {
        protected DbHelper() { }

        private static SqlXml _InnerSqlXml { get; set; }

        /// <summary>
        /// 初始化数据库链接
        /// </summary>
        public static DbHelper Creator(string sqlxmlfile)
        {
            DbHelper dbhelper = new DbHelper();

            _InnerSqlXml = new SqlXml(sqlxmlfile);
            DataAccess.Initialize(DbProvider.MySql,
                _InnerSqlXml.Host, _InnerSqlXml.Dbname, _InnerSqlXml.UserName, _InnerSqlXml.PassWord);

            ///从数据库中取所有的未抓取过的url
            dbhelper.GetCrawlUrls();
            ///从数据库中取所有的网站的Url解析规则及正文提取规则。
            dbhelper.GetWebRuleCollection();

            return dbhelper;
        }

        public WebPage[] CrawlUrls
        {
            get { return this._CrawlUrls; }
        }
        private WebPage[] _CrawlUrls;
        private void GetCrawlUrls()
        {
            //TODO: 目前fetch表暂还在jeelu_advertisement表，不日内将移植到jeelu_url表
            DataTable dt = DataAccess.SExecuteDataTable(
                @"select id,url,etag,content_length,last_modified,encoding 
                        from 
                            jeelu_advertisement.fetchurl f 
                        where f.is_new <> 'n' limit 20");
            List<WebPage> t = new List<WebPage>();
            foreach (DataRow row in dt.Rows)
            {
                WebPage wp = new WebPage();
                Debug.Assert(!(row["url"] is DBNull), "");
                wp.Url = (string)row["url"];
                if (!(row["etag"] is DBNull))
                {
                    wp.HeaderEtag = (string)row["etag"];
                }
                if (!(row["content_length"] is DBNull))
                {
                    wp.HeaderContentLength = (string)row["content_length"];
                }
                if (!(row["last_modified"] is DBNull))
                {
                    wp.HeaderLastModified = (string)row["last_modified"];
                }
                if (!(row["encoding"] is DBNull))
                {
                    wp.Encoding = Encoding.GetEncoding((string)row["encoding"]);
                }
                t.Add(wp);
            }

            this._CrawlUrls = t.ToArray();
        }

        /// <summary>
        /// Key是域名，value是该域名下的各种Url规则集合
        /// </summary>
        public Dictionary<string, WebRuleCollection> WebRuleCollections
        {
            get { return this._WebRuleCollections; }
        }
        private Dictionary<string, WebRuleCollection> _WebRuleCollections = new Dictionary<string, WebRuleCollection>();
        private void GetWebRuleCollection()
        {
            Dictionary<long, string> hostDic = new Dictionary<long, string>();
            /// 规则的参数信息集合
            Dictionary<long, List<ParamState>> paramStateDic = GetParamStateDic();
            /// 规则的正文抽取规则集合
            Dictionary<long, List<ExtractRule>> ruleExtractRuleDic = GetRuleExtractRuleDic();

            /// 1. 创建Dictionary[long, WebRuleCollection]，Key是域名ID，value是该域名下的各种Url规则集合
            Dictionary<long, WebRuleCollection> longruleDic = new Dictionary<long, WebRuleCollection>();
            foreach (DataRow row in GetAllHosts().Rows)
            {
                WebRuleCollection wrc = new WebRuleCollection(row[1].ToString());
                wrc.ID = (long)row[0];
                wrc.SiteName = row[1].ToString();
                longruleDic.Add(wrc.ID, wrc);
                hostDic.Add(wrc.ID, wrc.SiteName);
            }

            ///2. 添加每个域名具体的所有URL规则
            foreach (DataRow row in GetAllRules().Rows)
            {
                long hostid = (long)row[1];

                WebRule wr = new WebRule();
                wr.ID = (long)row[0];
                wr.RuleName = row[3].ToString();
                wr.RuleState = WebRuleState.None;

                /// 3.添加URL下的参数集合
                List<ParamState> paramStateList;
                if (paramStateDic.TryGetValue(wr.ID, out paramStateList))
                {
                    foreach (ParamState item in paramStateList)
                    {
                        wr.AddParam(item);
                    }
                }

                /// 4.添加URL下的正文抽取规则集合
                List<ExtractRule> extractRuleList;
                if (ruleExtractRuleDic.TryGetValue(wr.ID, out extractRuleList))
                {
                    foreach (ExtractRule item in extractRuleList)
                    {
                        wr.ExtractRuleCollection.Add(item);
                    }
                }
                longruleDic[hostid].Add(wr);
            }

            foreach (var item in hostDic)
            {
                this._WebRuleCollections.Add(item.Value, longruleDic[item.Key]);
            }
        }

        /// <summary>
        /// 更新网站的Url解析规则及正文提取规则
        /// </summary>
        /// <param name="ListRuleColl"></param>
        public static void UpdateRuleCollection(List<WebRuleCollection> ListRuleColl)
        {
            DataTable AllRules = DataAccess.SExecuteDataTable(
                @"SELECT id,domain_id,static_reg,ordered_param,last_manual_time,is_ignore,description,is_dynamic 
                        FROM 
                            jeeboard_url.fetchurl_rule");
            DataTable AllParams = DataAccess.SExecuteDataTable(
                @"SELECT id,rule_id,code,is_effect,description 
                        FROM 
                            jeeboard_url.fetchurl_param");
            DataTable AllExtractRule = DataAccess.SExecuteDataTable(
                @"SELECT id,rule_id,name,start,end,description 
                        FROM 
                            jeeboard_url.fetchurl_extract_rule");

            List<WebRule> listDelRule = new List<WebRule>();
            foreach (WebRuleCollection wrc in ListRuleColl)
            {
                listDelRule.Clear();
                foreach (WebRule wr in wrc)
                {
                    if (wr.RuleState != WebRuleState.None)
                    {
                        DataTable dt = null;
                        switch (wr.RuleState)
                        {
                            case WebRuleState.New:
                                DataRow row = AllRules.NewRow();
                                row["domain_id"] = wrc.ID;
                                row["ordered_param"] = wr.RuleName;
                                row["last_manual_time"] = new DateTime();
                                AllRules.Rows.Add(row);
                                dt = AllRules.GetChanges();
                                DataAccess.SExecuteUpdate("SELECT * FROM jeeboard_url.fetchurl_rule", dt);
                                AllRules.AcceptChanges();
                                int iruleID = Convert.ToInt32(dt.Rows[0]["id"]);

                                foreach (ParamState param in wr.ParamStates.Values)
                                {
                                    string sql = "insert into jeeboard_url.fetchurl_param(reg_id,code,is_effect,description) values(" + iruleID + ",'" + param.ParamKey + "','" + param.KeyState.ToString() + "','')";
                                    DataAccess.SExecuteNonQuery(sql);
                                    param.IsStateChange = false;
                                }
                                foreach (ExtractRule er in wr.ExtractRuleCollection)
                                {
                                    string sql = "insert into jeeboard_url.fetchurl_extract_rule(rule_id,name,start,end,description) values(" + iruleID + ",'" + er.Name + "','" + er.Start + ",'" + er.End + ",'" + "','')";
                                    DataAccess.SExecuteNonQuery(sql);
                                    er.IsStateChange = false;
                                }
                                wr.RuleState = WebRuleState.None;
                                break;
                            case WebRuleState.Modify:

                                foreach (ParamState param in wr.ParamStates.Values)
                                {
                                    if (param.IsStateChange)
                                    {
                                        DataAccess.SExecuteNonQuery("update jeeboard_url.fetchurl_param set is_effect='" + param.KeyState.ToString() + "' where id=" + param.ID);
                                        AllParams.AcceptChanges();
                                        param.IsStateChange = false;
                                    }
                                }
                                foreach (ExtractRule er in wr.ExtractRuleCollection)
                                {
                                    if (er.IsStateChange)
                                    {
                                        string sql = string.Empty;
                                        switch (er.ExtractRuleState)
                                        {
                                            case WebRuleState.New:
                                                sql = "insert into jeeboard_url.fetchurl_extract_rule(rule_id,name,start,end,description) values(" + wr.ID + ",'" + er.Name + "','" + er.Start + "','" + er.End + "','')";
                                                break;
                                            case WebRuleState.Modify:
                                                sql = @"update jeeboard_url.fetchurl_extract_rule set start='" + er.Start + "',end='" + er.End + "' where id=" + er.ID;
                                                break;
                                            case WebRuleState.Delete:
                                                sql = "delete from jeeboard_url.fetchurl_extract_rule where id= " + er.ID;
                                                break;
                                            default:
                                                break;
                                        }
                                        DataAccess.SExecuteNonQuery(sql);
                                        AllExtractRule.AcceptChanges();
                                        er.IsStateChange = false;
                                    }
                                }
                                wr.RuleState = WebRuleState.None;

                                break;
                            case WebRuleState.Delete:
                                DataAccess.SExecuteNonQuery("delete from jeeboard_url.fetchurl_param where reg_id= " + wr.ID);
                                AllParams.AcceptChanges();

                                DataAccess.SExecuteNonQuery("delete from jeeboard_url.fetchurl_extract_rule where rule_id= " + wr.ID);
                                AllExtractRule.AcceptChanges();

                                DataAccess.SExecuteNonQuery("delete from jeeboard_url.fetchurl_rule where id= " + wr.ID);
                                AllRules.AcceptChanges();

                                listDelRule.Add(wr);
                                break;
                            default:
                                break;
                        }
                    }
                }
                foreach (WebRule wr in listDelRule)
                {
                    wrc.Remove(wr);
                }
            }
        }

        /// <summary>
        /// 将列表中的广告的状态修改为已索引，并保存到数据库
        /// </summary>
        public static void UpdateAdState(List<long> ids)
        {
            if (ids != null && ids.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ids)
                {
                    sb.Append(item).Append(",");
                }
                string str = sb.ToString(0, sb.Length - 1);
                DataAccess.SExecuteNonQuery("update jeelu_advertisement.advertisement ad set current_state = 7 where id in ( ?str )", str);
            }
        }


        /******************************* 一些数据库操作的静态方法集 ************************************/

        /// <summary>
        /// 获取在所有数据库中已制定规则的域名
        /// </summary>
        private static DataTable GetAllHosts()
        {
            return DataAccess.SExecuteDataTable("SELECT id,host FROM jeeboard_url.fetchurl_host");
        }

        /// <summary>
        /// 获取在所有数据库中域名的规则集
        /// </summary>
        private static DataTable GetAllRules()
        {
            ///根据：域名对象ID/规则名称，查找她的所对应的规则。
            ///Sql Select的参数：
            ///     主键|域名id|静态正则(先忽略)|动态url的排序后参数|最后人工修改时间|人工处理时是否忽略|描述|是否动态url
            DataTable AllRules = DataAccess.SExecuteDataTable(
                @"SELECT id,domain_id,static_reg,ordered_param,last_manual_time,is_ignore,description,is_dynamic 
                        FROM 
                            jeeboard_url.fetchurl_rule");
            return AllRules;
        }

        /// <summary>
        /// 获取在所有数据库中域名的URL参数集
        /// </summary>
        /// <returns></returns>
        private static DataTable GetRuleParams()
        {
            //所有的规则的参数
            //主键  规则id  编码(例如id、type)    是否有效  描述    
            DataTable AllParams = DataAccess.SExecuteDataTable(
                @"SELECT id,rule_id,code,is_effect,description FROM jeeboard_url.fetchurl_param");
            return AllParams;
        }
        private static Dictionary<long, List<ParamState>> GetParamStateDic()
        {
            Dictionary<long, List<ParamState>> paramStateDic = new Dictionary<long, List<ParamState>>();
            foreach (DataRow Parma in GetRuleParams().Rows)
            {
                WebKeyState wks = WebKeyState.Availability;
                switch (Parma[3].ToString())
                {
                    case "Availability":
                        wks = WebKeyState.Availability;
                        break;
                    case "Invalidation":
                        wks = WebKeyState.Invalidation;
                        break;
                    default:
                        break;
                }
                ParamState ps = new ParamState(Parma[2].ToString(), wks);
                ps.ID = (long)Parma[0];
                ps.IsStateChange = false;

                List<ParamState> psList;
                if (!paramStateDic.TryGetValue((long)Parma[1], out psList))
                {
                    psList = new List<ParamState>();
                    paramStateDic.Add((long)Parma[1], psList);
                }
                psList.Add(ps);
            }
            return paramStateDic;
        }

        /// <summary>
        /// 获取在所有数据库中域名的正文抽取规则集
        /// </summary>
        private static DataTable GetRuleExtractRule()
        {
            //给WebRule 增加正文抽取规则
            DataTable AllExtractRule = DataAccess.SExecuteDataTable(
                @"SELECT id,rule_id,name,start,end,description FROM jeeboard_url.fetchurl_extract_rule");
            return AllExtractRule;
        }
        private static Dictionary<long, List<ExtractRule>> GetRuleExtractRuleDic()
        {
            Dictionary<long, List<ExtractRule>> ruleExtractRuleDic = new Dictionary<long, List<ExtractRule>>();
            //给规则增加正文抽取规则
            foreach (DataRow ExtRule in GetRuleExtractRule().Rows)
            {
                ExtractRule er = new ExtractRule(ExtRule[2].ToString(), ExtRule[3].ToString(), ExtRule[4].ToString(), "");
                er.ID = (long)ExtRule[0];
                er.ExtractRuleState = WebRuleState.None;
                er.IsStateChange = false;

                List<ExtractRule> erList;
                if (!ruleExtractRuleDic.TryGetValue((long)ExtRule[1], out erList))
                {
                    erList = new List<ExtractRule>();
                    ruleExtractRuleDic.Add((long)ExtRule[1], erList);
                }
                erList.Add(er);
            }
            return ruleExtractRuleDic;
        }

        /// <summary>
        /// 从数据库中取所有需要匹配广告的广告信息
        /// </summary>
        internal static DataTable GetAdvertisements()
        {
            //TODO: 将魔法数字5，98放入配置文件
            return DataAccess.SExecuteDataTable(
                @"select id,keyword,current_state 
                        from 
                            jeelu_advertisement.advertisement ad 
                        where 
                            ad.keyword is not null and ad.current_state 
                        between 5 and 98");
        }

        /// <summary>
        /// 从数据库中取所有刚发布，尚未开始匹配的广告信息, 即所谓需要增量处理的广告
        /// </summary>
        /// <returns>数据结果</returns>
        internal static DataTable GetIncreaseAdvertisements()
        {
            //TODO: 将魔法数字6放入配置文件
            return DataAccess.SExecuteDataTable(
                @"select id,keyword,current_state 
                        from 
                            jeelu_advertisement.advertisement ad 
                        where 
                            ad.keyword is not null and ad.current_state = 6");
        }

    }

    class SqlXml
    {
        private XmlDocument _InnerDocument = new XmlDocument();
        public SqlXml(string file)
        {
            this._InnerDocument.Load(file);
            this.SqlDictionary = new Dictionary<string, string>();
            foreach (XmlNode node in this._InnerDocument.DocumentElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element && node.LocalName != "sql")
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                foreach (XmlNode subnode in ele.ChildNodes)
                {
                    if (subnode.NodeType != XmlNodeType.CDATA)
                    {
                        continue;
                    }
                    XmlCDataSection cdata = (XmlCDataSection)subnode;
                    this.SqlDictionary.Add(ele.GetAttribute("name"), cdata.InnerText);
                }
            }
            this.Host = this._InnerDocument.DocumentElement.GetAttribute("host");
            this.Dbname = this._InnerDocument.DocumentElement.GetAttribute("dbname");
            this.UserName = this._InnerDocument.DocumentElement.GetAttribute("username");
            this.PassWord = this._InnerDocument.DocumentElement.GetAttribute("password");
        }
        private Dictionary<string, string> SqlDictionary { get; set; }

        public string this[string key]
        {
            get { return this.SqlDictionary[key]; }
        }

        public string Host { get; private set; }
        public string Dbname { get; private set; }
        public string UserName { get; private set; }
        public string PassWord { get; private set; }
    }
}



/*
/// <summary>
/// 从正则式库中取符合条件的正则式
/// </summary>
/// <param name="Host">域名</param>
/// <param name="OrderedParam">参数</param>
/// <returns></returns>
public static DataTable GetFetchurlReg(string Host, string OrderedParam)
{
    return DataAccess.SExecuteDataTable("SELECT id FROM jeelu_url.fetchurl_reg where host = ?host and ordered_param = ?orderedParam", Host, OrderedParam);
}
/// <summary>
/// 从url参数库中取某正则式的所有参数
/// </summary>
/// <param name="RegId">正则式id</param>
/// <returns></returns>
public static DataTable GetFetchurlParam(long RegId)
{
    return DataAccess.SExecuteDataTable("SELECT code FROM jeelu_url.fetchurl_param where reg_id = ?regId and is_effect = 'y' order by code asc", RegId);
}

*/
