using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Jeelu.Data;
using System.Threading;
using System.Net;
using System.IO.Compression;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jeelu.SimplusOM.Server
{
    public class ExeMySQLCommand
    {
        string dataBaseName = "jeelu_major";

        //byte[] SerializeDataSet(DataSet dataSet)
        //{
            
        //    BinaryFormatter ser = new BinaryFormatter();
        //    MemoryStream ms = new MemoryStream();
        //    ser.Serialize(ms, dataSet);
        //    byte[] buffer = ms.ToArray();
        //    return buffer;
        //}

        ///// <summary>
        ///// 压缩数据集
        ///// </summary>
        ///// <param name="ds"></param>
        ///// <returns></returns>
        //public static byte[] CompressDS(DataSet ds)
        //{
        //    GZipStream
        //    MemoryStream ms = new MemoryStream();
        //    ZipOutputStream zos = new ZipOutputStream(ms);
        //    zos.PutNextEntry(new ZipEntry(ds.DataSetName));
        //    BinaryFormatter bf = new BinaryFormatter();
        //    DataSetSurrogate dss = new DataSetSurrogate(ds);
        //    bf.Serialize(zos, dss);
        //    zos.CloseEntry();
        //    zos.Close();
        //    byte[] ret = ms.ToArray();
        //    ms.Close();
        //    return ret;
        //}

        byte[] CompressedDataSet(DataSet data)
        {
            try
            {
                BinaryFormatter ser = new BinaryFormatter();
                MemoryStream unCompressMS = new MemoryStream();
                data.RemotingFormat = SerializationFormat.Binary;
                ser.Serialize(unCompressMS, data);

                MemoryStream compressMs = new MemoryStream();
                GZipStream compressedStream = new GZipStream(compressMs, CompressionMode.Compress, true);
                compressedStream.Write(unCompressMS.ToArray(), 0, unCompressMS.ToArray().Length);
                compressedStream.Close();
                return compressMs.ToArray();
            }
            catch (ApplicationException ex)
            {
                return null;
            }
        }


        #region 登陆
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataSet Login(string agent, string name, string password)
        {
            string managerSql = "select org.*,manager.* from " + dataBaseName + ".manager manager ";
            managerSql += "left outer join " + dataBaseName + ".organization org on org.id=manager.organization_id ";
            managerSql += "where manager.code='" + name + "' and manager.password='" + password + "' and org.name='" + agent + "'";
            DataTable dt = DataAccess.SExecuteDataTable(managerSql);
            dt.TableName = "login";
            DataSet loginDS = new DataSet();
            loginDS.Tables.Add(dt);
            if (dt.Rows.Count == 0)
                return null;
            else
            {
                return loginDS;
            }
        }

        /// <summary>
        /// 取得基本信息与数据表架构信息
        /// </summary>
        /// <returns></returns>
        public byte[] GetBaseInfo()
        {
            DataSet baseInfoDS = null;
            string sql = "select * from " + dataBaseName + ".organization where 1=2;";
            sql += "select * from " + dataBaseName + ".manager where 1=2;";
            sql += "select * from " + dataBaseName + ".month_task where 1=2;";
            sql += "select * from " + dataBaseName + ".organize_cash_flow where 1=2;";
            sql += "select * from jeelu_community.area;";
            sql += "select *,industry.name iname from jeelu_community.industry;";
            sql += "select *  from " + dataBaseName + ".product_template;";
            sql += "select * from " + dataBaseName + ".user where 1=2;";
            sql += "select * from " + dataBaseName + ".web_union where 1=2;";
            sql += "select * from " + dataBaseName + ".web_union_domain where 1=2;";
            sql += "select * from " + dataBaseName + ".user_cash_flow where 1=2;";
            sql += "select * from " + dataBaseName + ".month_task where 1=2;";
            sql += "select * from " + dataBaseName + ".season_task where 1=2;";
            sql += "select * from " + dataBaseName + ".manager_area where 1=2;";
            sql += "select * from " + dataBaseName + ".manager_log where 1=2;";
            baseInfoDS = DataAccess.SExecuteDataSet(sql);

            baseInfoDS.Tables[0].TableName = "organization";
            baseInfoDS.Tables[1].TableName = "manager";
            baseInfoDS.Tables[2].TableName = "organize_return";
            baseInfoDS.Tables[3].TableName = "organize_cash_flow";
            baseInfoDS.Tables[4].TableName = "area";
            baseInfoDS.Tables[5].TableName = "industry";
            baseInfoDS.Tables[6].TableName = "product_template";
            baseInfoDS.Tables[7].TableName = "user";
            baseInfoDS.Tables[8].TableName = "web_union";
            baseInfoDS.Tables[9].TableName = "web_union_domain";
            baseInfoDS.Tables[10].TableName = "user_cash_flow";
            baseInfoDS.Tables[11].TableName = "month_task";
            baseInfoDS.Tables[12].TableName = "season_task";
            baseInfoDS.Tables[13].TableName = "manager_area";
            baseInfoDS.Tables[14].TableName = "manager_log";
            return CompressedDataSet(baseInfoDS);
        }

        #endregion
        public DataTable GetSelfTradeInfo(int currentAgentId, DateTime dateTime, int mode)
        {
            string sql = "select * from  " + dataBaseName + ".organize_return where receive_organize_id=" + currentAgentId;
            int year = dateTime.Year;
            int month = dateTime.Month;
            string beginTime = "";
            string endTime = "";
            if (mode == 0)
            {
                beginTime = year.ToString() + "-" + month.ToString() + "-" + "1";
                endTime = year.ToString() + "-" + (month + 1).ToString() + "-" + "1";
            }
            else
            {

                beginTime = year.ToString() + "-" + month.ToString() + "-" + "1";
                endTime = year.ToString() + "-" + (month + 3).ToString() + "-" + "1";
            }
            sql += " and time>='" + beginTime + "' and time<'" + endTime + "'";

            return DataAccess.SExecuteDataTable(sql);
        }

        #region 代理商
        /// <summary>
        /// 获取当前代理商下的代理商,用
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="rightGrade">0：查看全部，1：查看直属以及下级 2：查看直属</param>
        /// <returns></returns>
        public byte[] GetChildAgents(int parentId, int managerId, int rightGrade)
        {
            DataSet childDS = new DataSet();
            //直属
            string sql2 = "select  distinct org.*,ifnull((CASE when org.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql2 += "count(comlog.id) comCount,";
            sql2 += "area.code areaCode,area.name areaName,manager.name managerName from " + dataBaseName + ".organization org ";
            sql2 += "left outer join jeelu_community.area area on area.id=org.area_id ";
            sql2 += "left outer join " + dataBaseName + ".manager on manager.id=org.manager_id ";
            sql2 += "left outer join jeelu_major.communicate_log comlog on comlog.organization_id=org.id ";
            sql2 += "where org.parent_id=" + parentId + " and org.manager_id=" + managerId;
            sql2 += " and org.current_state<2  ";
            sql2 += " group by comlog.communicate_time desc,org.id ";


            //下级
            string sql1 = "select distinct org.*,ifnull((CASE when org.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql1 += "count(comlog.id) comCount,";
            sql1 += "area.code areaCode,area.name areaName,manager.name managerName from " + dataBaseName + ".organization org ";
            sql1 += "left outer join jeelu_community.area area on area.id=org.area_id ";
            sql1 += "left outer join " + dataBaseName + ".manager on manager.id=org.manager_id ";
            sql1 += "left outer join jeelu_major.communicate_log comlog on comlog.organization_id=org.id ";
            sql1 += "where org.parent_id in (select id from " + dataBaseName + ".organization where parent_id =" + parentId + " and manager_id=" + managerId + ")";
            sql1 += " and org.current_state<2  ";
            sql1 += " group by comlog.communicate_time desc,org.id ";

            //全部
            string sql0 = "select distinct org.*,ifnull((CASE when org.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql0 += "count(comlog.id) comCount,";
            sql0 += "area.code areaCode,area.name areaName,manager.name managerName from " + dataBaseName + ".organization org ";
            sql0 += "left outer join jeelu_community.area area on area.id=org.area_id ";
            sql0 += "left outer join " + dataBaseName + ".manager on manager.id=org.manager_id ";
            sql0 += "left outer join jeelu_major.communicate_log comlog on comlog.organization_id=org.id ";
            sql0 += "where org.parent_id =" + parentId + " or org.parent_id in (select id from " + dataBaseName + ".organization where parent_id =" + parentId + ")";
            sql0 += " and org.current_state<2  ";
            sql0 += " group by comlog.communicate_time desc,org.id ";
            DataTable dt0, dt1,dt2;
            switch (rightGrade)
            {
                case 0:
                    {//全部
                        dt0 = DataAccess.SExecuteDataTable(sql0);
                        childDS.Tables.Add(dt0);
                        break;
                    }
                case 1:
                    {//直属以及下级
                        dt1 = DataAccess.SExecuteDataTable(sql1);
                        dt2 = DataAccess.SExecuteDataTable(sql2);
                        dt1.Merge(dt2);
                        childDS.Tables.Add(dt1);
                        break;
                    }
                case 2:
                    {//直属
                        dt1 = DataAccess.SExecuteDataTable(sql1);
                        childDS.Tables.Add(dt1);
                        break;
                    }
            }

            childDS.Tables[0].TableName = "organization";
            return CompressedDataSet(childDS);
        }


        /// <summary>
        /// 获取登陆帐户基本信息,用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] GetSingleAgent(int id)
        {
            string sql = "select org.* ,porg.id pid,porg.name pname,area.name areaName,  ";
            sql += "count(distinct manager.id) managerNum ";//管理员个数
            sql += "from jeelu_major.organization org "; //本代理
            sql += "left outer join jeelu_community1.area area on area.id=org.area_id ";//地区
            sql += "left outer  join jeelu_major.manager on organization_id=org.id ";//管理员
            sql += "left outer join jeelu_major.organization porg on porg.id=org.parent_id ";//父代理商 
            sql += "where org.id=" + id + " group by org.id ;";

            sql += "select org.id,";
            sql += "ifnull(count(CASE when flow.finance_type=1  THEN  flow.amount END),0) chargeRecords,";//充值记录
            sql += "ifnull(count(CASE when flow.finance_type=2  THEN  flow.amount END),0) returnRecords, ";//返点记录
            sql += "ifnull(sum(CASE when flow.finance_type=1  THEN  flow.amount END),0) chargeSum,";//充值总金额
            sql += "ifnull(sum(CASE when flow.finance_type=2  THEN  flow.amount END),0) returnSum ";//返点总金额
            sql += "from jeelu_major.organization org ";
            sql += "left outer join jeelu_major.organize_cash_flow flow on org.id=flow.receive_organize_id ";
            sql += "where org.id=" + id + "  group by org.id;";

            sql += "select org.id,";
            sql += "ifnull(count(CASE when cflow.finance_type=1  THEN  cflow.amount END),0) chargeChildRecords,";//向下充值记录
            sql += "ifnull(count(CASE when cflow.finance_type=2  THEN  cflow.amount END),0) returnchildRecords, ";//向下返点记录
            sql += "ifnull(sum(CASE when cflow.finance_type=1  THEN  cflow.amount END),0) chargeChildSum,";//向下充值总金额
            sql += "ifnull(sum(CASE when cflow.finance_type=2  THEN  cflow.amount END),0) returnchildSum ";//向下返点总金额            
            sql += "from jeelu_major.organization org ";
            sql += "left outer join jeelu_major.organize_cash_flow cflow on org.id=cflow.send_organize_id ";//向下
            sql += "where org.id=" + id + "  group by org.id;";

            sql += "select org.id,flow.amount,flow.trade_time ";
            sql += "from jeelu_major.organization org ";
            sql += "left outer join jeelu_major.organize_cash_flow flow on flow.receive_organize_id=org.id  ";
            sql += "where send_organize_id=" + id + " and trade_time=(select max(trade_time)  from jeelu_major.organize_cash_flow cflow where  cflow.finance_type=1) ";
            sql += "group by org.id ;";



            sql += "select ifnull(count(CASE when is_reserve='y'  THEN  org.id END),0) lateAgentCount,";
            sql+="ifnull(count(CASE when is_reserve='n'  THEN  org.id END),0) standardAgentCount ";
            sql+=" from " + dataBaseName + ".organization org ";
            sql += "where org.parent_id=" + id + " or (org.parent_id in (select id from " + dataBaseName + ".organization where parent_id=" + id + "));";

            sql += "select ifnull(count(CASE when is_reserve='y'  THEN  user.id END),0) lateUserCount,";
            sql += "ifnull(count(CASE when is_reserve='n'  THEN  user.id END),0) standardUserCount ";
            sql += " from " + dataBaseName + ".user user ";
            sql += "where user.organization_id=" + id + " or (user.organization_id in (select id from " + dataBaseName + ".organization where parent_id=" + id + "));";

            sql += "select * from " + dataBaseName + ".manager where organization_id=" + id + ";";
            sql += "select * from " + dataBaseName + ".month_task where organization_id=" + id + ";";
            sql += "select * from " + dataBaseName + ".season_task where organization_id=" + id + ";";
            sql += "select * from " + dataBaseName + ".organize_cash_flow where receive_organize_id=" + id + ";";
            DataSet agentDs = DataAccess.SExecuteDataSet(sql);

            agentDs.Tables[0].TableName = "organization";
            agentDs.Tables[1].TableName = "flowRecords";
            agentDs.Tables[2].TableName = "downflowRecords";
            agentDs.Tables[3].TableName = "lastChargeRecord";
            agentDs.Tables[4].TableName = "org_stat";
            agentDs.Tables[5].TableName = "org_user_stat";
            agentDs.Tables[6].TableName = "manager";
            agentDs.Tables[7].TableName = "month_task";
            agentDs.Tables[8].TableName = "season_task";
            agentDs.Tables[9].TableName = "organize_cash_flow";

            return CompressedDataSet(agentDs);
        }

        /// <summary>
        /// 新增，修改代理商
        /// </summary>
        /// <param name="agentDS"></param>
        /// <returns></returns>
        public int NewAgent(DataSet agentDS)
        {
            DataTable orgTable = agentDS.Tables["organization"];
            DataTable managerTable = agentDS.Tables["manager"];
            DataTable monthTask = agentDS.Tables["month_task"];
            DataTable seasonTask = agentDS.Tables["season_task"];
            DataTable orgCashFlow = agentDS.Tables["organize_cash_flow"];
            bool is_reserve = false;//是否是潜在代理商,默认为不是，即正式代理
            if (orgTable.Rows[0]["is_reserve"].ToString() == "y")
            {
                is_reserve = true;
            }

            DataRow managerDR = agentDS.Tables["manager"].Rows[0];
            DataRow monthTaskDR = null;
            DataRow seasonTaskDR = null;
            DataRow orgCashFlowDR = null;
            if (!is_reserve)
            {
                monthTaskDR = agentDS.Tables["month_task"].Rows[0];
                seasonTaskDR = agentDS.Tables["season_task"].Rows[0];
                orgCashFlowDR = agentDS.Tables["organize_cash_flow"].Rows[0];
            }
            #region 新增五个表对应的记录：代理商信息表，管理者表，月任务表，季任务表，代理商现金流表
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".organization", orgTable);
            DataRow orgDR = agentDS.Tables["organization"].Rows[0];
            int agentId = Convert.ToInt32(orgDR["id"]);

            managerDR["organization_id"] = agentId;
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".manager", managerTable);
            int managerId = Convert.ToInt32(managerDR["id"]);

            if (!is_reserve)
            {
                monthTaskDR["organization_id"] = agentId;
                DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".month_task", monthTask);

                seasonTaskDR["organization_id"] = agentId;
                DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".season_task", seasonTask);

                orgCashFlowDR["receive_organize_id"] = agentId;
                DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".organize_cash_flow", orgCashFlow);

            #endregion
                //#region 完成一次现金流的操作:充值者减值-被充值者加值
                //string updateSendOrganizationSql = "update " + dataBaseName + ".organization set balance=balance-" + orgCashFlowDR["amount"] + " where id =" + orgDR["parent_id"];
                //DataAccess.SExecuteNonQuery(updateSendOrganizationSql);
                //string updateReceiveOrganizationSql = "update " + dataBaseName + ".organization set balance=balance+" + orgCashFlowDR["amount"] + " where id =" + agentId;
                //DataAccess.SExecuteNonQuery(updateReceiveOrganizationSql);
                //#endregion
            }
            return 1;

        }

        /// <summary>
        /// 更改代理商状态（冻结，解冻，删除）
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="toId"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int ChangeAgentSatate(int fromId, int toId, string reason, int state)
        {
            string updateOrgSql = "update " + dataBaseName + ".organization set current_state=" + state + " where id=" + toId;
            string insertOrgReasonSql = "insert into " + dataBaseName + ".change_reason(from_manager_id,to_organization_id,reason,change_type,change_time) ";
            insertOrgReasonSql += "values({0},{1},'{2}',{3},{4})";
            insertOrgReasonSql = string.Format(insertOrgReasonSql, fromId, toId, reason, state, "'" + DateTime.Now + "'");

            DataAccess.SExecuteNonQuery(updateOrgSql);
            return DataAccess.SExecuteNonQuery(insertOrgReasonSql);
        }

        /// <summary>
        /// 更改用户状态（冻结，解冻，删除）
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="toId"></param>
        /// <param name="reason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int ChangeUserSatate(int fromId, int toId, string reason, int state)
        {
            string updateOrgSql = "update " + dataBaseName + ".user set current_state=" + state + " where id=" + toId;
            string insertOrgReasonSql = "insert into " + dataBaseName + ".change_reason(from_manager_id,to_user_id,reason,change_type,change_time) ";
            insertOrgReasonSql += "values({0},{1},'{2}',{3},{4})";
            insertOrgReasonSql = string.Format(insertOrgReasonSql, fromId, toId, reason, state, "'" + DateTime.Now + "'");

            DataAccess.SExecuteNonQuery(updateOrgSql);
            return DataAccess.SExecuteNonQuery(insertOrgReasonSql);
        }

        /// <summary>
        /// 取得代理商充值，返点的汇总数据
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public DataSet GetAgentChargeReturn(int agentId)
        {
            DataSet chargeReturnDataSet = new DataSet();
            string chargeSql = "select max(trade_time) lastTime,count(*) chargeNum,sum(amount) sumAmount from " + dataBaseName + ".organize_cash_flow where  receive_organize_id=" + agentId;
            string returnSql = "select * from " + dataBaseName + ".organize_return where receive_organize_id=" + agentId;

            chargeReturnDataSet.Tables.Add(DataAccess.SExecuteDataTable(chargeSql));
            chargeReturnDataSet.Tables.Add(DataAccess.SExecuteDataTable(returnSql));
            return chargeReturnDataSet;
        }

        /// <summary>
        /// 取得代理商充值，返点的汇总数据
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public DataTable GetAgentChargeRecords(int agentId)
        {
            string chargeSql = "select flow.*,ifnull((CASE when flow.check_state=1  THEN  true END),false) checked,";
            chargeSql += "sendorg.name sendOrgName,receiveorg.name receiveOrgName, charger.name chargerName,checker.name checkerName ";
            chargeSql += "from " + dataBaseName + ".organize_cash_flow flow ";
            chargeSql += "left outer join " + dataBaseName + ".organization sendorg on sendorg.id=flow.send_organize_id  ";
            chargeSql += "left outer join " + dataBaseName + ".organization receiveorg on receiveorg.id=flow.receive_organize_id  ";
            chargeSql += "left outer join " + dataBaseName + ".manager charger on charger.id=flow.send_manager_id  ";
            chargeSql += "left outer join " + dataBaseName + ".manager checker on checker.id=flow.check_manager_id  ";
            chargeSql += "where  receive_organize_id=" + agentId;
            DataTable chargeTable = DataAccess.SExecuteDataTable(chargeSql);
            chargeTable.TableName = "charge";
            return chargeTable;
        }

        /// <summary>
        /// 取得对下充值记录
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetAgentChargeSubRecords(int parentId)
        {
            string chargeSql = "select flow.*,ifnull((CASE when flow.check_state=1  THEN  true END),false) checked,";
            chargeSql += "sendorg.name sendOrgName,receiveorg.name receiveOrgName, charger.name chargerName,checker.name checkerName ";
            chargeSql += "from " + dataBaseName + ".organize_cash_flow flow ";
            chargeSql += "left outer join " + dataBaseName + ".organization sendorg on sendorg.id=flow.send_organize_id  ";
            chargeSql += "left outer join " + dataBaseName + ".organization receiveorg on receiveorg.id=flow.receive_organize_id  ";
            chargeSql += "left outer join " + dataBaseName + ".manager charger on charger.id=flow.send_manager_id  ";
            chargeSql += "left outer join " + dataBaseName + ".manager checker on checker.id=flow.check_manager_id  ";
            chargeSql += "where  send_organize_id=" + parentId;
            DataTable chargeTable = DataAccess.SExecuteDataTable(chargeSql);
            chargeTable.TableName = "charge";
            return chargeTable;
        }

        /// <summary>
        /// 充值过程：资金流动表插入一条记录
        /// 返点（任务）表完成任务加额
        /// 充值方余额减少
        /// 被充值方余额增加
        /// </summary>
        /// <param name="dataSet"></param>
        public void ChargeAgent(DataTable chargeTable)
        {
            DataRow dr = chargeTable.Rows[0];
            string amount = dr["amount"].ToString();
            string finance_type = dr["finance_type"].ToString();
            string trade_time = dr["trade_time"].ToString();
            string send_organize_id = dr["send_organize_id"].ToString();
            string receive_organize_id = dr["receive_organize_id"].ToString();
            string manager_id = dr["send_manager_id"].ToString();
            string description = dr["description"].ToString();

            string insertFlowSql = "insert into " + dataBaseName + ".organize_cash_flow (description,amount,finance_type,trade_time,send_organize_id,receive_organize_id,send_manager_id) values ('{0}',{1},{2},'{3}',{4},{5},{6})";
            insertFlowSql = string.Format(insertFlowSql, description, amount, finance_type, trade_time, send_organize_id, receive_organize_id, manager_id);
            DataAccess.SExecuteNonQuery(insertFlowSql);
        }

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="dataSet"></param>
        public int ChargeUser(DataTable chargeTable)
        {
            DataRow dr = chargeTable.Rows[0];
            string amount = dr["amount"].ToString();
            string finance_type = dr["finance_type"].ToString();
            string trade_time = dr["trade_time"].ToString();
            string send_organize_id = dr["send_organize_id"].ToString();
            string receive_user_id = dr["receive_user_id"].ToString();
            string manager_id = dr["send_manager_id"].ToString();
            string description = dr["description"].ToString();

            string insertFlowSql = "insert into " + dataBaseName + ".organize_cash_flow (description,amount,finance_type,trade_time,send_organize_id,receive_user_id,send_manager_id) values ('{0}',{1},{2},'{3}',{4},{5},{6})";
            insertFlowSql = string.Format(insertFlowSql, description, amount, finance_type, trade_time, send_organize_id, receive_user_id, manager_id);
            DataAccess.SExecuteNonQuery(insertFlowSql);
            return 1;
        }

        public DataSet GetAgentReturn(int AgentId)
        {
            DataSet retDataSet = new DataSet();
            string sql = " select * from " + dataBaseName + ".organize_return where receive_organize_id=" + AgentId + ";";
            sql += "select * from " + dataBaseName + ".return_rate where organization_id=" + AgentId;
            retDataSet = DataAccess.SExecuteDataSet(sql);
            retDataSet.Tables[0].TableName = "organize_return";
            retDataSet.Tables[1].TableName = "return_rate";

            return retDataSet;
        }

        public int updateReturnRate(DataTable rateTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".return_rate", rateTable);
        }

        /// <summary>
        /// 修改代理商，用
        /// </summary>
        /// <param name="agentDS"></param>
        /// <returns></returns>
        public int UpdateAgent(DataSet agentDS)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".organization", agentDS.Tables["organization"]);
        }
        #endregion

        #region 用户
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="managerId"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public DataSet GetChildUsers(int agentId, int managerId, int rightGrade)
        {
            DataSet childDS = new DataSet();
            //员工直接管理下的客户
            string sql3 = "select user.*,0 manageState ,area.name areaName,org.name orgName,manager.name managerName, ";
            sql3 += "ifnull((CASE when user.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql3 += "count(comlog.id) comCount ";
            sql3 += "from " + dataBaseName + ".user user ";
            sql3 += "left outer join jeelu_community.area on area.id=user.city_id ";
            sql3 += "left outer join " + dataBaseName + ".organization org on org.id=organization_id ";
            sql3 += "left outer join " + dataBaseName + ".manager on manager.id=user.manager_id ";
            sql3 += "left outer join jeelu_major.communicate_log comlog on comlog.user_id=user.id ";
            sql3 += " where user.manager_id=" + managerId + " and user.current_state<>'2' ";
            sql3 += " group by comlog.communicate_time desc,user.id";

            //公司直接管理下的客户
            string sql2 = "select user.*,0 manageState ,area.name areaName ,org.name orgName,manager.name managerName, ";
            sql2 += "ifnull((CASE when user.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql2 += "count(comlog.id) comCount ";
            sql2 += "from " + dataBaseName + ".user user ";
            sql2 += "left outer join jeelu_community.area on area.id=user.city_id ";
            sql2 += "left outer join " + dataBaseName + ".organization org on org.id=organization_id ";
            sql2 += "left outer join " + dataBaseName + ".manager on manager.id=user.manager_id ";
            sql2 += "left outer join jeelu_major.communicate_log comlog on comlog.user_id=user.id ";
            sql2 += "where user.organization_id=" + agentId + " and user.current_state<>'2' ";
            sql2 += " group by comlog.communicate_time desc,user.id";

            //员工管辖的代理商下的客户
            string sql1 = "select user.*,1 manageState ,area.name areaName,org.name orgName,manager.name managerName, ";
            sql1 += "ifnull((CASE when user.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql1 += "count(comlog.id) comCount ";
            sql1 += "from " + dataBaseName + ".user user ";
            sql1 += "left outer join jeelu_community.area on area.id=user.city_id ";
            sql1 += "left outer join " + dataBaseName + ".organization org on org.id=organization_id ";
            sql1 += "left outer join " + dataBaseName + ".manager on manager.id=user.manager_id ";
            sql1 += "left outer join jeelu_major.communicate_log comlog on comlog.user_id=user.id ";
            sql1 += "where user.organization_id in (select id from " + dataBaseName + ".organization where parent_id =" + agentId + " and manager_id=" + managerId + ")";
            sql1 += " and user.current_state<>'2' ";
            sql1 += " group by comlog.communicate_time desc,user.id";
            
            //全部
            string sql0 = "select user.*,1 manageState ,area.name areaName ,org.name orgName,manager.name managerName, ";
            sql0 += "ifnull((CASE when user.is_reserve='y'  THEN  '潜在' END),'正式') isReserve,";
            sql0 += "count(comlog.id) comCount ";
            sql0 += "from " + dataBaseName + ".user user ";
            sql0 += "left outer join jeelu_community.area on area.id=user.city_id ";
            sql0 += "left outer join " + dataBaseName + ".organization org on org.id=organization_id ";
            sql0 += "left outer join " + dataBaseName + ".manager on manager.id=user.manager_id ";
            sql0 += "left outer join jeelu_major.communicate_log comlog on comlog.user_id=user.id ";
            sql0 += "where (user.organization_id in (select id from " + dataBaseName + ".organization where parent_id =" + agentId + ")";
            sql0 += " or user.organization_id =" + agentId+") and user.current_state<>'2' ";
            sql0 += " group by comlog.communicate_time desc,user.id ";

            DataTable dt0, dt1, dt2, dt3;
            switch (rightGrade)
            {
                case 0:
                    {
                        dt0 = DataAccess.SExecuteDataTable(sql0);
                        childDS.Tables.Add(dt0);
                        break;
                    }
                case 1:
                    {
                        dt2 = DataAccess.SExecuteDataTable(sql2);
                        childDS.Tables.Add(dt2);
                        break;
                    }
                case 2:
                    {
                        dt1 = DataAccess.SExecuteDataTable(sql1);
                        dt3 = DataAccess.SExecuteDataTable(sql3);
                        dt1.Merge(dt3);
                        childDS.Tables.Add(dt1);
                        break;
                    }
                case 3:
                    {
                        dt3 = DataAccess.SExecuteDataTable(sql3);
                        childDS.Tables.Add(dt3);
                        break;
                    }
            }


            string userCashFlowSql = "select * from " + dataBaseName + ".user_cash_flow where 1=2";
            string agentCashFlowSql = "select * from " + dataBaseName + ".organize_cash_flow where 1=2";

            DataTable dt4 = DataAccess.SExecuteDataTable(userCashFlowSql);
            DataTable dt5 = DataAccess.SExecuteDataTable(agentCashFlowSql);
            childDS.Tables.Add(dt4);
            childDS.Tables.Add(dt5);

            childDS.Tables[0].TableName = "user";
            childDS.Tables[1].TableName = "user_cash_flow";
            childDS.Tables[2].TableName = "organize_cash_flow";
            return childDS;
        }

        /// <summary>
        /// 得到单个用户的具体信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetSingleUser(int userId, bool isReserve)
        {
            string userSQL = "select user.*,";
            userSQL += "org.name orgName,";
            userSQL += "flow.trade_time lastChargeTime,flow.trade_amount lastChargeAmount";
            userSQL += " from " + dataBaseName + ".user ";
            userSQL += "left outer join " + dataBaseName + ".organization org on org.id=user.organization_id ";
            userSQL += "left outer join " + dataBaseName + ".user_cash_flow flow on user_id=user.id ";
            userSQL += "where user.id=" + userId;
            if (!isReserve)
                userSQL += " and flow.trade_time=(select  max(cflow.trade_time) from " + dataBaseName + ".user_cash_flow cflow where cflow.user_id=" + userId + ") ";
            userSQL += " group by user.id ";
            DataTable dt = DataAccess.SExecuteDataTable(userSQL);
            dt.TableName = "user";
            return dt;
        }
        /// <summary>
        /// 更新用户信息：用于新增用户
        /// </summary>
        /// <param name="userDS"></param>
        /// <returns></returns>
        public int UpdateUser(DataSet userDS)
        {
            string userSql = "select * from " + dataBaseName + ".user";
            string orgCashFlowSQL = "select * from " + dataBaseName + ".organize_cash_flow";

            DataTable userTable = userDS.Tables["user"];
            DataTable agentCashFlowTable = userDS.Tables["organize_cash_flow"];

            DataAccess.SExecuteUpdate(userSql, userTable);//新增一个用户
            bool isReserve = (userTable.Rows[0]["is_reserve"].ToString() == "n");
            int orgId = Convert.ToInt32(userTable.Rows[0]["organization_id"]);
            if (isReserve)
            {
                int userId = Convert.ToInt32(userTable.Rows[0]["id"]);

                agentCashFlowTable.Rows[0]["receive_user_id"] = userId;//新增一条代理商先现金流表
                agentCashFlowTable.Rows[0]["send_organize_id"] = orgId;
                DataAccess.SExecuteUpdate(orgCashFlowSQL, userDS.Tables["organize_cash_flow"]);
            }
            return 1;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            string dtpSql = "update " + dataBaseName + ".user set current_state=2 where id=" + id;
            return DataAccess.SExecuteNonQuery(dtpSql);
        }

        public DataTable GetExistUser(string code)
        {
            string sql = "select * from " + dataBaseName + ".user where username='" + code + "'";
            DataTable dt = DataAccess.SExecuteDataTable(sql);
            return dt;
        }

        public int AddExistUser(int orgId,int managerId, string userId)
        {
            string sql = "update " + dataBaseName + ".user set organization_id=" + orgId + ",manager_id="+managerId+" where username='" + userId + "'";
            DataTable dt = DataAccess.SExecuteDataTable(sql);
            return dt.Rows.Count;
        }

        public DataTable GetUserCharge(int currentUserId)
        {
            string sql = "select flow.*,org.name orgName ,manager.name manangerName";
            sql += " from jeelu_major.organize_cash_flow flow ";
            sql += " left outer join jeelu_major.user on user.id=flow.receive_user_id ";
            sql += " left outer join  jeelu_major.organization org on org.id=flow.send_organize_id ";
            sql += " left outer join  jeelu_major.manager on manager.id=flow.send_manager_id  ";
            sql += " where user.id=" + currentUserId;
            return DataAccess.SExecuteDataTable(sql);
        }



        #endregion

        public int NewManager(DataSet managerDS)
        {
            DataTable managerTable = managerDS.Tables["manager"];
            DataTable managerAreaTable = managerDS.Tables["manager_area"];
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".manager", managerTable);
            int managerId = Convert.ToInt32(managerTable.Rows[0]["id"]);
            foreach (DataRow dr in managerAreaTable.Rows)
            {
                dr["manager_id"] = managerId;
            }
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".manager_area", managerAreaTable);
            return 1;
        }

        /// <summary>
        /// 取得代理商或用户的状态变更列表
        /// </summary>
        /// <param name="currentId"></param>
        /// <param name="agentOrUser"></param>
        /// <returns></returns>
        public DataTable GetStateChangeReason(int currentId, int agentOrUser)
        {
            DataTable reasonTable = null;
            string reasonSql = "";
            if (agentOrUser == 0)
                reasonSql = "select * from " + dataBaseName + ".change_reason where to_organization_id=" + currentId;
            else
                reasonSql = "select * from " + dataBaseName + ".change_reason where to_user_id=" + currentId;
            reasonTable = DataAccess.SExecuteDataTable(reasonSql);
            return reasonTable;
        }


        #region webUnion
        public DataSet GetWebUnion()
        {
            string sql = "select webunion.*,count(webunion.id) webcount, ";
            sql += "ifnull(count(CASE when current_state=0  THEN  webunion.id END),0)   unchecked, ";
            sql += "ifnull(count(CASE when current_state=1  THEN  webunion.id END),0)   checked, ";
            sql += "ifnull(count(CASE when current_state=2  THEN  webunion.id END),0)  nopass, ";
            sql += "ifnull(count(CASE when current_state=3  THEN  webunion.id END),0)  edit_unchecked, ";
            sql += " ifnull(count(CASE when substring(industry_code,1,1)='1'  THEN  webunion.id END),0)  industryType, ";
            sql += "ifnull(count(CASE when substring(industry_code,1,1) in ('2','3')  THEN  webunion.id END),0)   blogType, ";
            sql += "ifnull(count(CASE when substring(industry_code,1,1)='4'  THEN  webunion.id END),0)  generalType, ";
            sql += "ifnull(count(CASE when substring(industry_code,1,1)='5'  THEN  webunion.id END),0)  doorType, ";
            sql += " '查看资料' viewInfo ";
            sql += "from " + dataBaseName + ".web_union webunion left outer join " + dataBaseName + ".web_union_domain site on webunion.id =site.web_union_id group by webunion.id";

            DataSet unionDS = new DataSet();
            DataTable unionDT = DataAccess.SExecuteDataTable(sql);
            unionDT.TableName = "webunion";
            unionDS.Tables.Add(unionDT);
            return unionDS;
        }

        public int UpdateWebUnion(DataTable webUnionTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".web_union", webUnionTable);
        }
        #endregion

        public DataSet GetWebSite(int webUnionId)
        {
            string sqlSite = "select website.*,ind.name indName,indback.name indBackName, ";
            sqlSite += "ifnull(CASE when current_state=1 THEN  '审核' ";
            sqlSite += " when current_state=0 THEN  '未审核' ";
            sqlSite += "when current_state=2 THEN  '未通过' END,'')  checkState ";
            sqlSite += "from " + dataBaseName + ".web_union_domain website  ";
            sqlSite += "left outer join jeelu_community.industry ind on ind.code=website.industry_code ";
            sqlSite += "left outer join jeelu_community.industry indback on indback.code=website.back_indu_code ";
            if (webUnionId > 0)
                sqlSite += " where web_union_id=" + webUnionId;

            string sqlsiteSta = "select count(website.id) siteCount,web_union.name ,";
            sqlsiteSta += "ifnull(count(CASE when current_state=0  THEN  website.id END),0)   unchecked, ";
            sqlsiteSta += "ifnull(count(CASE when current_state=1  THEN  website.id END),0)   checked, ";
            sqlsiteSta += "ifnull(count(CASE when current_state=2  THEN  website.id END),0)  nopass, ";
            sqlsiteSta += "ifnull(count(CASE when current_state=3  THEN  website.id END),0)  edit_unchecked ";
            sqlsiteSta += "from " + dataBaseName + ".web_union_domain website ";
            sqlsiteSta += "left outer join " + dataBaseName + ".web_union on web_union.id=web_union_id ";
            if (webUnionId > -1) sqlsiteSta += "where web_union_id= " + webUnionId;
            sqlsiteSta += " group by web_union_id ";
            DataSet websiteDS = new DataSet();
            DataTable websiteDT = DataAccess.SExecuteDataTable(sqlSite);
            websiteDT.TableName = "website";
            DataTable siteStaDT = DataAccess.SExecuteDataTable(sqlsiteSta);
            siteStaDT.TableName = "siteSta";
            websiteDS.Tables.Add(siteStaDT);
            websiteDS.Tables.Add(websiteDT);
            return websiteDS;
        }
        /// <summary>
        /// 更新网站数据
        /// </summary>
        /// <param name="siteTable"></param>
        /// <returns></returns>
        public int UpdateWebSite(DataTable siteTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".web_union_domain", siteTable);

        }




        /// <summary>
        /// 取得子级地区
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetSubArea(int parentId)
        {
            string sql = "select area_relation.*,area.name subAreaName  ";
            sql += " from jeelu_community.area_relation ";
            sql += " left outer join jeelu_community.area on area.id=sub_id ";
            sql += " where area_relation.parent_id=" + parentId + " and level_diff=1 ";
            DataTable areaTable = DataAccess.SExecuteDataTable(sql);
            areaTable.TableName = "area";
            return areaTable;
        }

        /// <summary>
        /// 取得子级行业
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public DataTable GetSubIndustry(int parentId)
        {
            string sql = "select industry.id,industry.code,industry.name  ";
            sql += " from jeelu_community.industry_relation ";
            sql += " left outer join jeelu_community.industry on industry.id=sub_id ";
            sql += " where industry_relation.parent_id=" + parentId + " and level_diff=1 ";
            DataTable industryTable = DataAccess.SExecuteDataTable(sql);
            industryTable.TableName = "industry";
            return industryTable;
        }

        /// <summary>
        /// 得到某地区某等级的代理商
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="areaId"></param>
        /// <param name="areaId2"></param>
        /// <returns></returns>
        public DataTable GetAgent(int grade, int areaId, int areaId2)
        {
            string sql = "select * from " + dataBaseName + ".organization where 1=1 ";
            if (grade < 0)
            {
                sql += " and organization.grade in (0,1) ";
                sql += " and organization.area_id in (" + areaId + "," + areaId2 + ")";
            }
            else
                sql += " and organization.grade=" + grade;
            if (grade > 0)
                sql += " and organization.area_id in (" + areaId + "," + areaId2 + ")";
            DataTable orgTable = DataAccess.SExecuteDataTable(sql);
            orgTable.TableName = "organiztion";
            return orgTable;
        }

        /// <summary>
        /// 取得某代理商下的全部管理员
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public DataTable GetManager(int agentId)
        {
            string sql = "select * ,'查看权限' viewRights,'操作日志' viewOpLog ,count(remind.id) remindCount ";
            sql += " from " + dataBaseName + ".manager ";
            sql += " left outer join " + dataBaseName + ".manager_remind remind on remind.manager_id=manager.id ";
            sql += " where organization_id =" + agentId;
            sql += " group by manager.id";
            return DataAccess.SExecuteDataTable(sql);
        }

        /// <summary>
        /// 取得某管理员与某代理商沟通记录
        /// </summary>
        /// <param name="_agentId"></param>
        /// <param name="_managerId"></param>
        /// <returns></returns>
        public DataTable GetCommunicateLog(int agentId, int userId, int managerId)
        {
            string sql = "select * from " + dataBaseName + ".communicate_log ";
            sql += "where manager_id=" + managerId;
            if (agentId > -1)
                sql += " and  organization_id =" + agentId;
            else if (userId > -1)
                sql += " and  user_id =" + userId;
            sql += " order by communicate_time desc ";
            return DataAccess.SExecuteDataTable(sql);
        }

        /// <summary>
        ///  更新代理商沟通记录
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public int UpdateCommunicateLog(DataTable dataTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".communicate_log", dataTable);
        }
        /// <summary>
        /// 取得某管理员提醒记录
        /// </summary>
        /// <param name="_managerId"></param>
        /// <returns></returns>
        public DataTable GetManagerRemind(int _managerId)
        {
            string sql = "select * from " + dataBaseName + ".manager_remind ";
            sql += "where manager_id=" + _managerId;
            return DataAccess.SExecuteDataTable(sql);
        }

        /// <summary>
        /// 更新某管理员提醒记录
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public int UpdateManagerRemind(DataTable dataTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".manager_remind", dataTable);
        }

        #region 新闻
        /// <summary>
        /// 取得全部的新闻记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetNews()
        {
            string sql = "select *,ifnull(CASE when column_id=1 THEN  '公司动态' ";
            sql += "when column_id=2 THEN  '行业动态' END,'') colName from " + dataBaseName + ".news";

            DataTable newsTable = DataAccess.SExecuteDataTable(sql);
            newsTable.TableName = "news";
            return newsTable;
        }

        public int UpdateNews(DataTable newsTable)
        {
            int effRows=DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".news", newsTable);
            if (effRows > 0)
                StartVisitThread();
            return effRows;
        }

        void StartVisitThread()
        {
            ThreadStart ths = new ThreadStart(VisitWebSite);
            Thread th = new Thread(ths);
            th.Start();
        }

        void VisitWebSite()
        {
            try
            {
                DataTable visitTable = DataAccess.SExecuteDataTable("select * from " + dataBaseName + ".news_visit_uri");
                foreach (DataRow dr in visitTable.Rows)
                {
                    WebClient client = new WebClient();
                    string visituri = dr["uri"].ToString();
                    System.IO.Stream s = client.OpenRead(visituri);
                    s.Close();
                    client.Dispose();
                }
            }
            catch
            { }
        }
        #endregion

        public DataSet GetReturnData(int agentId)
        {
            string sqlMonthReturn = "select * from " + dataBaseName + ".month_task ";
            sqlMonthReturn += " where organization_id=" + agentId;
            //sqlMonthReturn += " order by communicate_time desc ";
            string sqlSeasonReturn = "select * from " + dataBaseName + ".season_task ";
            sqlSeasonReturn += " where organization_id=" + agentId;
            //sqlSeasonReturn += " order by communicate_time desc ";

            DataTable monthReturn = DataAccess.SExecuteDataTable(sqlMonthReturn);
            DataTable seasonReturn = DataAccess.SExecuteDataTable(sqlSeasonReturn);
            monthReturn.TableName = "monthReturn";
            seasonReturn.TableName = "seasonReturn";

            DataSet returnDS = new DataSet();
            returnDS.Tables.Add(monthReturn);
            returnDS.Tables.Add(seasonReturn);
            return returnDS;
        }


        public int UpdateReturnData(DataSet returnDS)
        {
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".month_task", returnDS.Tables[0]);
            DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".season_task", returnDS.Tables[1]);

            return 1;
        }

        /// <summary>
        /// 新增，修改管理员
        /// </summary>
        /// <param name="managerTable"></param>
        /// <returns></returns>
        public int UpdateManager(DataTable managerTable)
        {
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".manager", managerTable);
        }

        /// <summary>
        /// 取得登陆后的管理员的基本信息和统计信息
        /// </summary>
        /// <param name="_managerId"></param>
        /// <returns></returns>
        public DataSet GetSingleManager(int _managerId)
        {
            DataSet managerDS = new DataSet();
            string sqlManager = "select *,org.name orgName from " + dataBaseName + ".manager ";
            sqlManager += " left outer join " + dataBaseName + ".organization org on manager.organization_id=org.id ";
            sqlManager += "where manager.id=" + _managerId;
            DataTable managerTable = DataAccess.SExecuteDataTable(sqlManager);
            managerTable.TableName = "manager";

            string sqlManagerSta = " select  (select count(org.id)  from "+dataBaseName+".manager ";
            sqlManagerSta += "left outer join " + dataBaseName + ".organization org on manager.id=org.manager_id  ";
            sqlManagerSta += "where manager.id=" + _managerId + ") orgCout, ";
            sqlManagerSta += "(select count(user.id)  from " + dataBaseName + ".manager  ";
            sqlManagerSta += "left outer join " + dataBaseName + ".user on manager.id=user.manager_id  ";
            sqlManagerSta += "where manager.id=" + _managerId + ") userCout, ";
            sqlManagerSta += "(select count(flow.id)  from "+dataBaseName+".organize_cash_flow flow ";
            sqlManagerSta += "where send_manager_id=" + _managerId + " and finance_type=1) chargeCount, ";
            sqlManagerSta += "(select  sum(flow.amount) from " + dataBaseName + ".organize_cash_flow flow ";
            sqlManagerSta += "where send_manager_id=" + _managerId + " and finance_type=1) chargeSum, ";
            sqlManagerSta += "(select  count(cashflow.amount) from jeelu_major.manager manager ";
sqlManagerSta += "right outer join  jeelu_major.organization org on org.manager_id=manager.id  ";
sqlManagerSta += "right outer join jeelu_major.organize_cash_flow cashflow on cashflow.send_organize_id=org.id ";
sqlManagerSta += "where manager.id=115) returnCount, ";
sqlManagerSta += "(select  sum(cashflow.amount) from jeelu_major.manager manager ";
sqlManagerSta += "right outer join  jeelu_major.organization org on org.manager_id=manager.id  ";
sqlManagerSta += "right outer join jeelu_major.organize_cash_flow cashflow on cashflow.send_organize_id=org.id ";
sqlManagerSta += "where manager.id=115) returnSum ";
            DataTable managerStaTable = DataAccess.SExecuteDataTable(sqlManagerSta);
            managerStaTable.TableName = "managerSta";

            managerDS.Tables.Add(managerTable);
            managerDS.Tables.Add(managerStaTable);
            return managerDS;
        }





        public int DeleteWebSite(int webSiteId)
        {
            string sqlDel = "delete  from " + dataBaseName + ".web_union_domain where id="+webSiteId;
            int data = DataAccess.SExecuteNonQuery(sqlDel);
            return data;
        }

        public DataTable GetWebUnionStat(int _webUnionId, int year, int month, int day)
        {
            string sql = "";
            if (year + month + day == 0)
            {
                sql = "select stat.*,ind.name indName from jeelu_warehouse.stat_union_indu stat";
                sql += " left outer join jeelu_community.industry  ind on ind.code=stat.industry_code ";
                sql += " where stat.webunion_id=" + _webUnionId;
            }
            if (year > 0 && month > 0 && day > 0)
            {
                sql = "select stat.*,ind.name indName from jeelu_warehouse.stat_union_indu_day stat";
                sql += " left outer join jeelu_community.industry  ind on ind.code=stat.industry_code ";
                sql += " where stat.webunion_id=" + _webUnionId;
                sql += " and year=" + year;
                sql += " and month=" + month;
                sql += " and day=" + day;
            }
            else if (year > 0 && month > 0)
            {
                sql = "select stat.*,ind.name indName from jeelu_warehouse.stat_union_indu_month stat";
                sql += " left outer join jeelu_community.industry  ind on ind.code=stat.industry_code ";
                sql += " where stat.webunion_id=" + _webUnionId;
                sql += " and year=" + year;
                sql += " and month=" + month;
            }
            else if (year>0)
            {
                sql = "select stat.*,ind.name indName from jeelu_warehouse.stat_union_indu_year stat";
                sql += " left outer join jeelu_community.industry  ind on ind.code=stat.industry_code ";
                sql += " where stat.webunion_id=" + _webUnionId;
                sql += " and year="+year;
            }

            DataTable dt = DataAccess.SExecuteDataTable(sql);
            return dt;
        }

        public DataSet GetCustomTask()
        {
            DataSet ds = new DataSet();
            string sqlcustom_task = "select * from " + dataBaseName + ".custom_task";
            DataTable customTaskDT = DataAccess.SExecuteDataTable(sqlcustom_task);
            customTaskDT.TableName = "customTask";

            string sqlorgan_custom_task = "select * from " + dataBaseName + ".organ_custom_task where 1=2 ";
            DataTable orgCustomTaskDT = DataAccess.SExecuteDataTable(sqlorgan_custom_task);
            orgCustomTaskDT.TableName = "orgCustomTask";

            ds.Tables.Add(customTaskDT);
            ds.Tables.Add(orgCustomTaskDT);
            return ds;
        }

        public int UpdateCustomTask(DataSet customTaskSet)
        {
            DataTable customTaskTable=customTaskSet.Tables[0];
            DataTable agentCustomTaskTable=customTaskSet.Tables[1];
            string sql = "select * from " + dataBaseName + ".custom_task";
            string sqlAgentCustomTask = "select * from " + dataBaseName + ".organ_custom_task";

            DataAccess.SExecuteUpdate(sql, customTaskTable);
            int customTaskId=Convert.ToInt32(customTaskTable.Rows[0]["id"]);
            foreach (DataRow agentCustomRow in agentCustomTaskTable.Rows)
            {
                agentCustomRow["custom_task_id"] = customTaskId;
            }
            int dt=DataAccess.SExecuteUpdate(sqlAgentCustomTask, agentCustomTaskTable);
            return dt;
        }

        public DataTable GetDomainStat(int webUnionID, string indCode, int year, int month, int day)
        {
            string sql = "";
            if (year > 0 && month > 0 && day > 0)
            {
                sql = "select statDomain.*,domain.domain_url siteurl,domain.name sitename ";
                sql += "FROM ";
                sql += "jeelu_warehouse.stat_domain_day statDomain ";
                sql += "Left Outer Join jeelu_major.web_union_domain  domain ON  domain.id= statDomain.domain_id ";
                sql += "left outer join jeelu_community.industry ind on ind.code= domain.industry_code ";
                sql += "where domain.industry_code='" + indCode + "' and web_union_id=" + webUnionID;
                sql += " and year=" + year;
                sql += " and month=" + month;
                sql += " and day=" + day;
            }
            else if (year > 0 && month > 0)
            {
                sql = "select statDomain.*,domain.domain_url siteurl,domain.name sitename ";
                sql += "FROM ";
                sql += "jeelu_warehouse.stat_domain_month statDomain ";
                sql += "Left Outer Join jeelu_major.web_union_domain  domain ON  domain.id= statDomain.domain_id ";
                sql += "left outer join jeelu_community.industry ind on ind.code= domain.industry_code ";
                sql += "where domain.industry_code='" + indCode + "' and web_union_id=" + webUnionID;
                sql += " and year=" + year;
                sql += " and month=" + month;
            }
            else if (year > 0)
            {
                sql = "select statDomain.*,domain.domain_url siteurl,domain.name sitename ";
                sql += "FROM ";
                sql += "jeelu_warehouse.stat_domain_year statDomain ";
                sql += "Left Outer Join jeelu_major.web_union_domain  domain ON  domain.id= statDomain.domain_id ";
                sql += "left outer join jeelu_community.industry ind on ind.code= domain.industry_code ";
                sql += "where domain.industry_code='" + indCode + "' and web_union_id=" + webUnionID;
                sql += " and year=" + year;
            }
            else
            {
                sql = "select statDomain.*,domain.domain_url siteurl,domain.name sitename ";
                sql += "FROM ";
                sql += "jeelu_warehouse.stat_domain_year statDomain ";
                sql += "Left Outer Join jeelu_major.web_union_domain  domain ON  domain.id= statDomain.domain_id ";
                sql += "left outer join jeelu_community.industry ind on ind.code= domain.industry_code ";
                sql += "where domain.industry_code='" + indCode + "' and web_union_id=" + webUnionID;
                sql += " and year=" + DateTime.Today.Year;
            }

            return DataAccess.SExecuteDataTable(sql);
        }

        public int TranAgent(int _agentId, int agentId, int managerId)
        {
            string sql="";
            if (managerId>-1)
            sql="update "+dataBaseName+".organization set parent_id="+agentId+",manager_id="+managerId+" where id ="+_agentId;
            else 
            sql="update "+dataBaseName+".organization set parent_id="+agentId+",manager_id="+0+" where id ="+_agentId;
            int d = DataAccess.SExecuteNonQuery(sql);

            return d;
        }

        public int TranUser(int _userId, int agentId, int managerId)
        {
            string sql = "";
            if (managerId > -1)
                sql = "update " + dataBaseName + ".user set organization_id=" + agentId + ",manager_id=" + managerId + " where id =" + _userId;
            else
                sql = "update " + dataBaseName + ".user set organization_id=" + agentId + ",manager_id=" + 0 + " where id =" + _userId;
            int d = DataAccess.SExecuteNonQuery(sql);

            return d;
        }

        /// <summary>
        /// 月度返点
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void CreateMonthRetrnData(int year, int month)
        {
            string monthTaskSql="select * from "+dataBaseName+".month_task ";
            monthTaskSql+="where year(start_date)="+year+ " and month(start_date)="+(month-1);
            DataTable orgMonthTaskTable=DataAccess.SExecuteDataTable(monthTaskSql);


            for (int i = 0; i < orgMonthTaskTable.Rows.Count; i++)
            {
                DataRow dr = orgMonthTaskTable.Rows[i];
                double finishedAount = Convert.ToDouble(dr["finished_amount"]);
                double taskAmount = Convert.ToDouble(dr["task_amount"]);
                double completeRate = finishedAount / taskAmount;
                double returnRate = 0;
                double returnAmount = 0;
                if (completeRate >= 1)
                {
                    returnRate = Convert.ToDouble(dr["rate_a"]);
                    returnAmount = finishedAount * Convert.ToDouble(dr["rate_a"]);
                }
                else if (completeRate < 1 && completeRate >= 0.7)
                {
                    returnRate = Convert.ToDouble(dr["rate_b"]);
                    returnAmount = finishedAount * Convert.ToDouble(dr["rate_b"]);
                }
                else if (completeRate < 0.7 && completeRate >= 0.5)
                {
                    returnRate = Convert.ToDouble(dr["rate_c"]);
                    returnAmount = finishedAount * Convert.ToDouble(dr["rate_c"]);
                }
                else
                {
                    returnRate = Convert.ToDouble(dr["rate_d"]);
                    returnAmount = finishedAount * Convert.ToDouble(dr["rate_d"]);
                }
                dr["return_rate"] = returnRate;
                dr["return_amount"] = returnAmount/100;
                dr["return_time"] = DateTime.Now;

               // CashFlow(orgMonthTaskTable.Rows[i], Convert.ToDecimal(returnAmount / 100));
            }
            int j = DataAccess.SExecuteUpdate(monthTaskSql, orgMonthTaskTable);
        }
        /// <summary>
        /// 季度返点
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void CreateSeasonRetrnData(int year, int month)
        {
            string seasonTaskSql = "select * from " + dataBaseName + ".season_task ";
            seasonTaskSql += "where year(start_date)=" + year;
            seasonTaskSql += " and month(start_date) in (" + (month - 1) + "," + (month - 2) + "," + (month - 3)+")";
            DataTable orgSeasonTaskTable = DataAccess.SExecuteDataTable(seasonTaskSql);


            for (int i = 0; i < orgSeasonTaskTable.Rows.Count; i++)
            {
                DataRow dr = orgSeasonTaskTable.Rows[i];
                double finishedAount = Convert.ToDouble(dr["finished_amount"]);
                double taskAmount = Convert.ToDouble(dr["task_amount"]);
                double returnBase = Convert.ToDouble(dr["rate_base"]);
                double returnInc = Convert.ToDouble(dr["rate_inc"]);
                double completeRate = finishedAount / taskAmount;
                double returnRate = returnBase;
                if ((completeRate - 100)>0)
                    returnRate+=Convert.ToInt32((completeRate - 100) / 50) * returnInc;
                double returnAmount = finishedAount * returnRate;

               
                dr["return_rate"] = returnRate;
                dr["return_amount"] = returnAmount / 100;
                dr["return_time"] = DateTime.Now;

              //  CashFlow(orgSeasonTaskTable.Rows[i], Convert.ToDecimal(returnAmount / 100));
            }
            int j = DataAccess.SExecuteUpdate(seasonTaskSql, orgSeasonTaskTable);
        }

        /// <summary>
        /// 自定义返点
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void CreateCustomRetrnData()
        {
            string customTaskSql = "select * from " + dataBaseName + ".organ_custom_task orgCustomTask";
            customTaskSql += " left outer join " + dataBaseName + ".custom_task customTask on customTask.id=orgCustomTask.custom_task_id ";
            customTaskSql += " where customTask.start_time<='" + DateTime.Now.ToShortDateString()+"'";
            customTaskSql += " and customTask.end_time>='" + DateTime.Now + "'"; ;
            DataTable orgSeasonTaskTable = DataAccess.SExecuteDataTable(customTaskSql);

            for (int i = 0; i < orgSeasonTaskTable.Rows.Count; i++)
            {
                DataRow dr = orgSeasonTaskTable.Rows[i];
                double finishedAount = 0;// Convert.ToDouble(dr["finished_amount"]);
                double taskAmount = Convert.ToDouble(dr["task_amount"]);
                double returnBase = Convert.ToDouble(dr["rate_base"]);
                double returnInc = Convert.ToDouble(dr["rate_inc"]);
                double completeRate = finishedAount / taskAmount;
                double returnRate = returnBase;
                if ((completeRate - 100) > 0)
                    returnRate += Convert.ToInt32((completeRate - 100) / 50) * returnInc;
                double returnAmount = finishedAount * returnRate;


                dr["return_rate"] = returnRate;
                dr["return_amount"] = returnAmount / 100;
                dr["return_time"] = DateTime.Now;

              //  CashFlow(orgSeasonTaskTable.Rows[i], Convert.ToDecimal(returnAmount / 100));
            }
            int j = DataAccess.SExecuteUpdate(customTaskSql, orgSeasonTaskTable);
        }

        void CashFlow(DataRow returnDR, decimal returnAmount)
        {
            //两次代理商余额修改
            int orgId = Convert.ToInt32(returnDR["organization_id"]);
            string strOrg = "select * from " + dataBaseName + ".organization where id=" + orgId;
            DataTable orgTable = DataAccess.SExecuteDataTable(strOrg);
            int parentId = Convert.ToInt32(orgTable.Rows[0]["parent_id"]);
            string strParentOrg = "select * from " + dataBaseName + ".organization where id=" + parentId;
            DataTable parentOrgTable = DataAccess.SExecuteDataTable(strParentOrg);
            orgTable.Rows[0]["balance"] = Convert.ToDecimal(orgTable.Rows[0]["balance"]) + returnAmount;
            parentOrgTable.Rows[0]["balance"] = Convert.ToDecimal(parentOrgTable.Rows[0]["balance"]) - returnAmount;
            DataAccess.SExecuteUpdate(strOrg, orgTable);
            DataAccess.SExecuteUpdate(strParentOrg, parentOrgTable);
            //一次现金流
            string cashSql = "select * from " + dataBaseName + ".organize_cash_flow where 1=2";
            DataTable orgCashFlowTable = DataAccess.SExecuteDataTable(cashSql);
            DataRow dr = orgCashFlowTable.NewRow();
            dr["amount"] = returnAmount;
            dr["finance_type"] = 2;
            dr["trade_time"] = DateTime.Now;
            dr["send_organize_id"] = parentId;
            dr["receive_organize_id"] = orgId;
            orgCashFlowTable.LoadDataRow(dr.ItemArray, false);
            DataAccess.SExecuteUpdate(cashSql, orgCashFlowTable);
        }

        public DataTable GetReturnRecords(int sendOrgId, int receiveOrgId)
        {
            string sql="select cashflow.*,sendorg.name sendname,receiveorg.name receivename  from "+dataBaseName+".organize_cash_flow cashflow ";
            sql += "left outer join  " + dataBaseName + ".organization sendorg on cashflow.send_organize_id=sendorg.id ";
            sql += "left outer join  " + dataBaseName + ".organization receiveorg on cashflow.receive_organize_id=receiveorg.id ";
            if (sendOrgId > 0)
            {
                sql += " where send_organize_id=" + sendOrgId;
            }
            else
            {
                sql += " where receive_organize_id=" + receiveOrgId;
            }

            sql += " and finance_type='2'";
            return DataAccess.SExecuteDataTable(sql);
        }

        public int CheckAgentCharge(int chargeId, int managerId)
        {
            string sql = "update " + dataBaseName + ".organize_cash_flow set check_state=1,check_manager_id=" + managerId + " where id=" + chargeId;

            DataAccess.SExecuteNonQuery(sql);
            string getChargeSql = "select * from " + dataBaseName + ".organize_cash_flow where id=" + chargeId;
            DataTable chargeTable = DataAccess.SExecuteDataTable(getChargeSql);
            string amount = chargeTable.Rows[0]["amount"].ToString();
            string send_organize_id = chargeTable.Rows[0]["send_organize_id"].ToString();
            string receive_organize_id = chargeTable.Rows[0]["receive_organize_id"].ToString();
            string updateMonthTaskSql = "update " + dataBaseName + ".month_task set finished_amount=finished_amount+" + decimal.Parse(amount) + " where organization_id =" + int.Parse(receive_organize_id);
            string updateSeasonTaskSql = "update " + dataBaseName + ".season_task set finished_amount=finished_amount+" + decimal.Parse(amount) + " where organization_id =" + int.Parse(receive_organize_id);
            string updateSendOrgSql = "update " + dataBaseName + ".organization set balance=balance-" + decimal.Parse(amount) + " where id =" + int.Parse(send_organize_id);
            string updateReceiveOrgSql = "update " + dataBaseName + ".organization set balance=balance+" + decimal.Parse(amount) + " where id =" + int.Parse(receive_organize_id);

            DataAccess.SExecuteNonQuery(updateMonthTaskSql);
            DataAccess.SExecuteNonQuery(updateSeasonTaskSql);
            DataAccess.SExecuteNonQuery(updateSendOrgSql);
            DataAccess.SExecuteNonQuery(updateReceiveOrgSql);
            return 1;
        }

        public DataTable GetMonthReturn(DateTime today, int _agentId)
        {
            int year=0;
            int month=0;
            string SQL  = " SELECT monthtask.*,monthcheck.*,org.name orgName,manager.name checkerName FROM " + dataBaseName + ".month_task monthtask ";
            SQL += "left outer join " + dataBaseName + ".month_task_check monthcheck on  monthtask.organization_id=monthcheck.organization_id ";
            SQL += "left outer join " + dataBaseName + ".organization org on  org.id=monthtask.organization_id ";
            SQL += "left outer join " + dataBaseName + ".manager manager on  manager.id=monthcheck.checker_id ";
            SQL += "where 1=1 ";
            if (today > DateTime.MinValue)
            {
                year = today.Year;
                month = today.Month;
                SQL += " and (year(monthtask.start_date)=" + year + " and month(monthtask.start_date)=" + month + ")";
            }
            if (_agentId > 0)
            {
                SQL += " and org.id=" + _agentId;
            }
           
            DataTable dt = DataAccess.SExecuteDataTable(SQL);
            return dt;
        }

        public DataTable GetSeasonReturn(DateTime today, int _agentId)
        {
            int year = 0;
            int month = 0;
            string SQL = " SELECT seasontask.*,seasoncheck.*,org.name orgName,manager.name checkerName FROM " + dataBaseName + ".season_task seasontask ";
            SQL += "left outer join " + dataBaseName + ".season_task_check seasoncheck on  seasontask.organization_id=seasoncheck.organization_id ";
            SQL += "left outer join " + dataBaseName + ".organization org on  org.id=seasontask.organization_id ";
            SQL += "left outer join " + dataBaseName + ".manager manager on  manager.id=seasoncheck.checker_id ";
            if (today > DateTime.MinValue)
            {
                year = today.Year;
                month = today.Month;
                SQL += " and (year(seasontask.start_date)=" + year + " and month(seasontask.start_date)=" + month + ")";
            }
            if (_agentId > 0)
            {
                SQL += " and org.id=" + _agentId;
            }
            DataTable dt = DataAccess.SExecuteDataTable(SQL);
            return dt;
        }

        public DataTable GetCustomReturn(DateTime today, int _agentId)
        {
            string SQL = " SELECT customtask.*,customcheck.*,manager.name checkerName FROM " + dataBaseName + ".custom_task customtask ";
            SQL += "left outer join " + dataBaseName + ".custom_task_check customcheck on  customtask.id=customcheck.custom_task_id ";
            SQL += "left outer join " + dataBaseName + ".manager manager on  manager.id=customcheck.checker_id ";
            if (today > DateTime.MinValue)
            {
                SQL += " and (customtask.start_time>'" + today + "' and customtask.end_time<='" + today + "')";
            }
            DataTable dt = DataAccess.SExecuteDataTable(SQL);
            return dt;
        }

        public int UpdateMonthReturnSetCheck(DataTable monthReturnTable)
        {
            
            DataTable dt = DataAccess.SExecuteDataTable("select * from " + dataBaseName + ".month_task_check where 1=2");
                    
            foreach (DataRow dr in monthReturnTable.Rows)
            {
                if (dr["check_state"]!=DBNull.Value && Convert.ToBoolean(dr["check_state"]))
                {
                   DataRow newCheckRow = dt.NewRow();
                    newCheckRow["organization_id"] =Convert.ToInt32(dr["organization_id"]);
                    newCheckRow["start_date"] = Convert.ToDateTime(dr["start_date"]);
                    newCheckRow["rate_d"] = Convert.ToInt32(dr["rate_d"]);
                    newCheckRow["rate_c"] = Convert.ToInt32(dr["rate_c"]);
                    newCheckRow["rate_b"] = Convert.ToInt32(dr["rate_b"]);
                    newCheckRow["rate_a"] = Convert.ToInt32(dr["rate_a"]);
                    newCheckRow["task_amount"] = Convert.ToDecimal(dr["task_amount"]);
                    newCheckRow["check_state"] = 1;
                    newCheckRow["editor_id"] =  Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["checker_id"] =  Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["editor_time"] =DateTime.Now;
                    newCheckRow["check_time"] = DateTime.Now;
                    dt.LoadDataRow(newCheckRow.ItemArray, false);
                }
            }
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".month_task_check where 1=2",dt);
        }

        public int UpdateSeasonReturnSetCheck(DataTable seasonReturnTable)
        {
            DataTable dt = DataAccess.SExecuteDataTable("select * from " + dataBaseName + ".season_task_check where 1=2");

            foreach (DataRow dr in seasonReturnTable.Rows)
            {
                if (dr["check_state"] != DBNull.Value && Convert.ToBoolean(dr["check_state"]))
                {
                    DataRow newCheckRow = dt.NewRow();
                    newCheckRow["organization_id"] = Convert.ToInt32(dr["organization_id"]);
                    newCheckRow["start_date"] = Convert.ToDateTime(dr["start_date"]);
                    newCheckRow["rate_base"] = Convert.ToInt32(dr["rate_base"]);
                    newCheckRow["rate_inc"] = Convert.ToInt32(dr["rate_inc"]);
                    newCheckRow["expected_amount"] = Convert.ToDecimal(dr["task_amount"]);
                    newCheckRow["check_state"] = 1;
                    newCheckRow["editor_id"] =  Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["checker_id"] =  Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["editor_time"] = DateTime.Now;
                    newCheckRow["check_time"] = DateTime.Now;
                    dt.LoadDataRow(newCheckRow.ItemArray, false);
                }
            }
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".season_task_check where 1=2", dt);
        
        }

        public int UpdateCustomReturnSetCheck(DataTable customReturnTable)
        {
            DataTable dt = DataAccess.SExecuteDataTable("select * from " + dataBaseName + ".custom_task_check where 1=2");

            foreach (DataRow dr in customReturnTable.Rows)
            {
                if (dr["check_state"] != DBNull.Value && Convert.ToBoolean(dr["check_state"]))
                {
                    DataRow newCheckRow = dt.NewRow();
                    newCheckRow["custom_task_id"] = Convert.ToInt32(dr["id"]);
                    newCheckRow["task_name"] = dr["task_name"].ToString();
                    newCheckRow["start_time"] = Convert.ToDateTime(dr["start_time"]);
                    newCheckRow["end_time"] = Convert.ToDateTime(dr["end_time"]);
                    newCheckRow["task_amount"] = Convert.ToInt32(dr["default_amount"]);
                    newCheckRow["task_rate"] = Convert.ToInt32(dr["rate_base"]);
                    newCheckRow["rate_inc"] = Convert.ToInt32(dr["rate_inc"]);
                    newCheckRow["check_state"] = 1;
                    newCheckRow["editor_id"] = Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["checker_id"] = Convert.ToInt32(dr["checker_id"]);
                    newCheckRow["editor_time"] = DateTime.Now;
                    newCheckRow["check_time"] = DateTime.Now;
                    dt.LoadDataRow(newCheckRow.ItemArray, false);
                }
            }
            return DataAccess.SExecuteUpdate("select * from " + dataBaseName + ".custom_task_check where 1=2", dt);
        
        }

        public int CheckWebSite(int webSiteId, int checkType)
        {
            string updateSql = "update " + dataBaseName + ".web_union_domain ";
            updateSql+=" set industry_code=" + checkType + ",current_state=1,regtime='"+DateTime.Today+"' where id=" + webSiteId;
            int d=DataAccess.SExecuteNonQuery(updateSql);
            return d;
        }

        public int CheckUserCharge(int chargeId, int managerId)
        {
            //审核代理商现金流表
            string sql = "update " + dataBaseName + ".organize_cash_flow set check_state=1,check_manager_id=" + managerId + " where id=" + chargeId;
            DataAccess.SExecuteNonQuery(sql);

            string agentCashFlowSQL = "select * from " + dataBaseName + ".organize_cash_flow where  id=" + chargeId;
            DataTable agentCashFlowTable = DataAccess.SExecuteDataTable(agentCashFlowSQL);
            decimal trade_amount = Convert.ToDecimal(agentCashFlowTable.Rows[0]["amount"]);
            
            DataTable productDt = DataAccess.SExecuteDataTable("select * from " + dataBaseName + ".product_template");
            decimal userAmount = Convert.ToDecimal(productDt.Select("organization_price=" + trade_amount)[0]["user_price"]);
            int userId = Convert.ToInt32(agentCashFlowTable.Rows[0]["receive_user_id"]);
            int agentId = Convert.ToInt32(agentCashFlowTable.Rows[0]["send_organize_id"]);

            //新增一条用户现金流表
            string userCashFlowSQL = "select * from " + dataBaseName + ".user_cash_flow where 1=2";
            DataTable userCashFlowTable = DataAccess.SExecuteDataTable(userCashFlowSQL);
            DataRow newUserCashFlow = userCashFlowTable.NewRow();
            newUserCashFlow["user_id"] = userId;
            newUserCashFlow["send_organization_id"] = agentId;
            newUserCashFlow["trade_amount"] = userAmount;
            newUserCashFlow["trade_time"] = DateTime.Today;
            newUserCashFlow["send_organization_id"] = agentId;
            userCashFlowTable.LoadDataRow(newUserCashFlow.ItemArray, false);
            DataAccess.SExecuteUpdate(userCashFlowSQL, userCashFlowTable);


            //更新代理商余额
            string updateAgentSql = "update " + dataBaseName + ".organization set balance=balance-" + trade_amount;
            updateAgentSql += " where id=" + Convert.ToInt32(agentCashFlowTable.Rows[0]["send_organize_id"]);
            DataAccess.SExecuteNonQuery(updateAgentSql);
            //更新广告主余额
            string updateUserSql = "update " + dataBaseName + ".user set balance=balance+" + userAmount;
            updateUserSql += ",total_charged=total_charged+1,total_charge_count=total_charge_count+ "+userAmount;
            updateAgentSql += " where id=" + Convert.ToInt32(agentCashFlowTable.Rows[0]["receive_user_id"]);
            DataAccess.SExecuteNonQuery(updateUserSql);

            return 1;
        }


        /// <summary>
        /// 取得某广告主下的广告网站
        /// </summary>
        /// <param name="_userId"></param>
        /// <returns></returns>
        public DataTable GetAdvWebSite(int _userId)
        {
            string sql = "select user.id userId,user.nickname userName,website.id websiteId,website.name websiteName ";
            sql += " from "+dataBaseName+".website ";
            sql += " left outer join "+dataBaseName+".user on user.id=website.user_id ";
            sql += "where user.id="+_userId;
            sql += " order by user.id desc";

            return DataAccess.SExecuteDataTable(sql);
        }

        /// <summary>
        /// 取得某一网站下的广告
        /// </summary>
        /// <param name="_siteId"></param>
        /// <returns></returns>
        public DataTable GetAdvertisement(int _siteId)
        {
            string sql = "select website.name websiteName,advtemp.name tempName,advertisement.id advId, advertisement.name advName ";
            sql += " from jeelu_advertisement.advertisement ";
            sql += " left outer join jeelu_major.website on website.id=advertisement.website_id ";
            sql += " left outer join jeelu_advertisement.advertise_temp advtemp on advtemp.id=advertisement.template_id ";
            sql += " where website_id="+_siteId;
            sql += " order by website.id ";

            return DataAccess.SExecuteDataTable(sql);
        }
    }
}
