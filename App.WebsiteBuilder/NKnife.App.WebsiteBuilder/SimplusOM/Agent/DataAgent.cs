using System;
using System.Data;

namespace Jeelu.SimplusOM
{
    public interface DataAgent
    {
        DataSet Login(string agent,string name, string password);
        byte[] GetBaseInfo();
        DataTable GetSelfTradeInfo(int currentAgentId, DateTime dateTime, int mode);
        #region 代理商
        byte[] GetChildAgents(int parntId, int managerId, int rightGrade);
        int NewAgent(DataSet agentDS);
        int UpdateAgent(DataSet agentDS);
        int ChangeAgentSatate(int formId, int toId, string reason, int state);
        int ChangeUserSatate(int formId, int toId, string reason, int state);
        void ChargeAgent(DataTable chargeTable);
        DataSet GetAgentChargeReturn(int agentId);
        DataTable GetAgentChargeRecords(int currentAgentId);
        byte[] GetSingleAgent(int currentChildAgentId);
        DataSet GetAgentReturn(int AgentId);
        int updateReturnRate(DataTable rateTable);
        #endregion
        #region 用户
        DataSet GetChildUsers(int agentId, int managerId, int right);
        int UpdateUser(DataSet userDS);
        int DeleteUser(int id);
        DataTable GetExistUser(string userId);
        int AddExistUser(int orgId,int managerId, string userId);
        DataTable GetUserCharge(int currentUserId);
        #endregion
        #region 员工管理
        int NewManager(DataSet managerDS);
        #endregion
        #region 网盟
        DataSet GetWebUnion();
        int UpdateWebUnion(DataTable webUnionTable);
        DataSet GetWebSite(int webUnionId);
        int UpdateWebSite(DataTable siteTable);
        #endregion



        DataTable GetStateChangeReason(int currentId, int agentOrUser);

        DataTable GetAgentChargeSubRecords(int p);

        DataTable GetSingleUser(int userId,bool isReserve);

        DataTable GetSubArea(int parentId);

        DataTable GetAgent(int grade, int areaId, int areaId2);

        DataTable GetManager(int agentId);

        DataTable GetCommunicateLog(int agentId,int userId, int managerId);

        int UpdateCommunicateLog(DataTable dataTable);

        DataTable GetManagerRemind(int _managerId);

        int UpdateManagerRemind(DataTable dataTable);

        DataTable GetNews();

        int UpdateNews(DataTable dataTable);

        DataSet GetReturnData(int agentId);

        int UpdateReturnData(DataSet returnDS);

        int UpdateManager(DataTable managerTable);

        DataSet GetSingleManager(int _managerId);

        int DeleteWebSite(int webSiteId);

        DataTable GetWebUnionStat(int _webUnionId,int year,int month,int day);

        DataSet GetCustomTask();

        int UpdateCustomTask(DataSet customTaskSet);

        DataTable GetDomainStat(int webUnionID, string indCode,int year, int month, int day);

        int TranAgent(int _agentId, int agentId, int managerId);

        int TranUser(int _userId, int agentId, int managerId);

        DataTable GetReturnRecords(int sendOrgId, int receiveOrgId);

        int CheckAgentCharge(int chargeId, int managerId);

        DataTable GetMonthReturn(DateTime today, int _agentId);

        DataTable GetSeasonReturn(DateTime today, int _agentId);

        DataTable GetCustomReturn(DateTime today, int _agentId);

        int UpdateMonthReturnSetCheck(DataTable monthReturnTable);

        int UpdateSeasonReturnSetCheck(DataTable seasonReturnTable);

        int UpdateCustomReturnSetCheck(DataTable customReturnTable);

        int CheckWebSite(int webSiteId, int checkType);

        int CheckUserCharge(int chargeId, int managerId);

        int ChargeUser(DataTable orgCashFlow);

        DataTable GetSubIndustry(int parentId);

        DataTable GetAdvWebSite(int _userId);

        DataTable GetAdvertisement(int _siteId);
    }
}
