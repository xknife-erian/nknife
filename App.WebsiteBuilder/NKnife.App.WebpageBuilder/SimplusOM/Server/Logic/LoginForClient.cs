using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Jeelu.SimplusOM.Server
{
    public class LoginForClient
    {
        ExeMySQLCommand exeCmd = new ExeMySQLCommand();

        public DataSet Login(string agent,string name, string password)
        {
            return exeCmd.Login(agent,name, password);
        }

        public DataTable GetSelfTradeInfo(int currentAgentId, DateTime dateTime, int mode)
        {
            return exeCmd.GetSelfTradeInfo(currentAgentId, dateTime, mode);
        }

        #region 代理商
        public byte[] GetChildAgents(int parentId, int managerId, int rightGrade)
        {
            return exeCmd.GetChildAgents(parentId, managerId, rightGrade);
        }

        public int NewAgent(DataSet agentDS)
        {
            return exeCmd.NewAgent(agentDS);
        }

        public DataSet GetAgentChargeReturn(int agentId)
        {
            return exeCmd.GetAgentChargeReturn(agentId);
        }

        public DataTable GetAgentChargeRecords(int currentAgentId)
        {
            return exeCmd.GetAgentChargeRecords(currentAgentId);
        }


        public byte[] GetSingleAgent(int currentChildAgentId)
        {
            return exeCmd.GetSingleAgent(currentChildAgentId);
        }


        public DataSet GetAgentReturn(int AgentId)
        {
            return exeCmd.GetAgentReturn(AgentId);
        }

        public int updateReturnRate(DataTable rateTable)
        {
            return exeCmd.updateReturnRate(rateTable);
        }

        public int UpdateAgent(DataSet agentDS)
        {
            return exeCmd.UpdateAgent(agentDS);
        }

        public void ChargeAgent(DataTable chargeTable)
        {
            exeCmd.ChargeAgent(chargeTable);
        }

        #endregion

        #region 用户
        public int UpdateUser(DataSet userDS)
        {
            return exeCmd.UpdateUser(userDS);
        }

        public int DeleteUser(int id)
        {
            return exeCmd.DeleteUser(id);
        }

        public DataTable GetExistUser(string userId)
        {
            return exeCmd.GetExistUser(userId);
        }


        public int AddExistUser(int orgId, int managerId, string userId)
        {
            return exeCmd.AddExistUser(orgId, managerId,userId);
        }

        public DataTable GetUserCharge(int currentUserId)
        {
            return exeCmd.GetUserCharge(currentUserId);
        }

        public DataSet GetChildUsers(int agentId, int managerId, int rightGrade)
        {
            return exeCmd.GetChildUsers(agentId,managerId,rightGrade);
        }

        #endregion









        public int NewManager(DataSet managerDS)
        {
            return exeCmd.NewManager(managerDS);
        }

        public DataTable GetStateChangeReason(int currentId, int agentOrUser)
        {
            return exeCmd.GetStateChangeReason(currentId, agentOrUser);
        }

        public byte[] GetBaseInfo()
        {
            return exeCmd.GetBaseInfo();
        }

        public DataSet GetWebUnion()
        {
            return exeCmd.GetWebUnion();
        }

        public int UpdateWebUnion(DataTable webUnionTable)
        {
            return exeCmd.UpdateWebUnion(webUnionTable);
        }

        public DataSet GetWebSite(int webUnionId)
        {
            return exeCmd.GetWebSite(webUnionId);
        }

        public int UpdateWebSite(DataTable siteTable)
        {
            return exeCmd.UpdateWebSite(siteTable);
        }

        public DataTable GetAgentChargeSubRecords(int parentId)
        {
            return exeCmd.GetAgentChargeSubRecords(parentId);
        }

        public DataTable GetSingleUser(int userId, bool isReserve)
        {
            return exeCmd.GetSingleUser(userId, isReserve);
        }

        public DataTable GetSubArea(int parentId)
        {
            return exeCmd.GetSubArea(parentId);
        }

        public DataTable GetAgent(int grade, int areaId, int areaId2)
        {
            return exeCmd.GetAgent(grade, areaId, areaId2);
        }

        public DataTable GetManager(int agentId)
        {
            return exeCmd.GetManager(agentId);
        }

        public DataTable GetCommunicateLog(int agentId,int userId, int managerId)
        {
            return exeCmd.GetCommunicateLog(agentId,userId,managerId);
        }

        public int UpdateCommunicateLog(DataTable dataTable)
        {
            return exeCmd.UpdateCommunicateLog(dataTable);
        }

        public DataTable GetManagerRemind(int _managerId)
        {
            return exeCmd.GetManagerRemind(_managerId);
        }

        public int UpdateManagerRemind(DataTable dataTable)
        {
            return exeCmd.UpdateManagerRemind(dataTable);
        }

        public DataTable GetNews()
        {
            return exeCmd.GetNews();
        }

        public int UpdateNews(DataTable newsTable)
        {
            return exeCmd.UpdateNews(newsTable);
        }

        public DataSet GetReturnData(int agentId)
        {
            return exeCmd.GetReturnData(agentId);
        }

        public int UpdateReturnData(DataSet returnDS)
        {
            return exeCmd.UpdateReturnData(returnDS);
        }

        public int ChangeUserSatate(int formId, int toId, string reason, int state)
        {
            return exeCmd.ChangeUserSatate(formId, toId, reason, state);        
        }

        public int ChangeAgentSatate(int formId, int toId, string reason, int state)
        {
            return exeCmd.ChangeAgentSatate(formId, toId, reason, state); 
        }

        public int UpdateManager(DataTable managerTable)
        {
            return exeCmd.UpdateManager(managerTable);
        }

        public DataSet GetSingleManager(int _managerId)
        {
            return exeCmd.GetSingleManager(_managerId);
        }

        public int DeleteWebSite(int webSiteId)
        {
            return exeCmd.DeleteWebSite(webSiteId);
        }

        public DataTable GetWebUnionStat(int _webUnionId, int year, int month, int day)
        {
            return exeCmd.GetWebUnionStat(_webUnionId,year,month,day);
        }

        public DataSet GetCustomTask()
        {
            return exeCmd.GetCustomTask();
        }

        public int UpdateCustomTask(DataSet customTaskSet)
        {
            return exeCmd.UpdateCustomTask(customTaskSet);
        }

        public DataTable GetDomainStat(int webUnionID, string indCode, int year, int month, int day)
        {
            return exeCmd. GetDomainStat(webUnionID, indCode,year, month, day);
        }

        public int TranAgent(int _agentId, int agentId, int managerId)
        {
            return exeCmd.TranAgent(_agentId, agentId, managerId);
        }

        public int TranUser(int _userId, int agentId, int managerId)
        {
            return exeCmd.TranUser(_userId, agentId, managerId);
        }

        public DataTable GetReturnRecords(int sendOrgId, int receiveOrgId)
        {
            return exeCmd.GetReturnRecords(sendOrgId, receiveOrgId);
        }

        public int CheckAgentCharge(int chargeId, int managerId)
        {
            return exeCmd.CheckAgentCharge(chargeId, managerId);
        }



        public DataTable GetMonthReturn(DateTime today, int _agentId)
        {
            return exeCmd.GetMonthReturn(today, _agentId);
        }

        public DataTable GetSeasonReturn(DateTime today, int _agentId)
        {
            return exeCmd.GetSeasonReturn(today, _agentId);
        }

        public DataTable GetCustomReturn(DateTime today, int _agentId)
        {
            return exeCmd.GetCustomReturn(today, _agentId);
        }

        public int UpdateMonthReturnSetCheck(DataTable monthReturnTable)
        {
            return exeCmd.UpdateMonthReturnSetCheck(monthReturnTable);
        }

        public int UpdateSeasonReturnSetCheck(DataTable seasonReturnTable)
        {
            return exeCmd.UpdateSeasonReturnSetCheck(seasonReturnTable);
        }

        public int UpdateCustomReturnSetCheck(DataTable customReturnTable)
        {
            return exeCmd.UpdateCustomReturnSetCheck(customReturnTable);
        }

        public int CheckWebSite(int webSiteId, int checkType)
        {
            return exeCmd.CheckWebSite(webSiteId, checkType);
        }

        public int CheckUserCharge(int chargeId, int managerId)
        {
            return exeCmd.CheckUserCharge(chargeId, managerId);
        }

        public int ChargeUser(DataTable orgCashFlow)
        {
            return exeCmd.ChargeUser(orgCashFlow);
        }

        public DataTable GetSubIndustry(int parentId)
        {
            return exeCmd.GetSubIndustry(parentId);
        }

        public DataTable GetAdvWebSite(int _userId)
        {
            return exeCmd.GetAdvWebSite(_userId);
        }

        public DataTable GetAdvertisement(int _siteId)
        {
            return exeCmd.GetAdvertisement(_siteId);
        }
    }
}
