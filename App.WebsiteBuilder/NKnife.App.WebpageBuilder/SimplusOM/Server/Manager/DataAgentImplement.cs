using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Server
{
    [Serializable]
    public class DataAgentImplement : MarshalByRefObject, DataAgent
    {
        LoginForClient dataLogic = new LoginForClient();
        #region 代理商
        public byte[] GetChildAgents(int parentId, int managerId, int rightGrade)
        {
            return dataLogic.GetChildAgents(parentId,  managerId,rightGrade);
        }
        public int NewAgent(DataSet ds)
        {
            return dataLogic.NewAgent(ds);
        }
        public void ChargeAgent(DataTable chargeTable)
        {
            dataLogic.ChargeAgent(chargeTable);
        }
        #endregion
        #region 用户成员
        public int UpdateUser(DataSet userDS)
        {
            return dataLogic.UpdateUser(userDS);
        }
        public int DeleteUser(int id)
        {
            return dataLogic.DeleteUser(id);
        }
        #endregion

        public DataSet Login(string agent, string name, string password)
        {
            return dataLogic.Login(agent,name, password);
        }

        public DataSet GetAgentChargeReturn(int agentId)
        {
            return dataLogic.GetAgentChargeReturn(agentId);
        }

        public DataTable GetAgentChargeRecords(int currentAgentId)
        {
            return dataLogic.GetAgentChargeRecords(currentAgentId);
        }

        public byte[] GetSingleAgent(int currentChildAgentId)
        {
            return dataLogic.GetSingleAgent(currentChildAgentId);
        }

        public DataSet GetAgentReturn(int AgentId)
        {
            return dataLogic.GetAgentReturn(AgentId);
        }

        public int updateReturnRate(DataTable rateTable)
        {
            return dataLogic.updateReturnRate(rateTable);
        }

        public int UpdateAgent(DataSet agentDS)
        {
            return dataLogic.UpdateAgent(agentDS);
        }


        public DataTable GetSelfTradeInfo(int currentAgentId, DateTime dateTime, int mode)
        {
            return dataLogic.GetSelfTradeInfo(currentAgentId, dateTime, mode);
        }


        public DataTable GetExistUser(string userId)
        {
            return dataLogic.GetExistUser(userId);
        }



        public int AddExistUser(int orgId, int managerId, string userId)
        {
            return dataLogic.AddExistUser(orgId, managerId,userId);
        }



        public DataTable GetUserCharge(int currentUserId)
        {
            return dataLogic.GetUserCharge(currentUserId);
        }



        public DataSet GetChildUsers(int agentId, int managerId, int rightGrade)
        {
            return dataLogic.GetChildUsers(agentId, managerId, rightGrade);
        }

        public int NewManager(DataSet managerDS)
        {
            return dataLogic.NewManager(managerDS);
        }

        public DataTable GetStateChangeReason(int currentId, int agentOrUser)
        {
            return dataLogic.GetStateChangeReason(currentId, agentOrUser);
        }

        public byte[] GetBaseInfo()
        {
            return dataLogic.GetBaseInfo();
        }

        public DataSet GetWebUnion()
        {
            return dataLogic.GetWebUnion();
        }

        public int UpdateWebUnion(DataTable webUnionTable)
        {
            return dataLogic.UpdateWebUnion(webUnionTable);
        }

        public DataSet GetWebSite(int webUnionId)
        {
            return dataLogic.GetWebSite(webUnionId);
        }

        public int UpdateWebSite(DataTable siteTable)
        {
            return dataLogic.UpdateWebSite(siteTable);
        }

        public DataTable GetAgentChargeSubRecords(int parentId)
        {
            return dataLogic.GetAgentChargeSubRecords(parentId);
        }

        public DataTable GetSingleUser(int userId, bool isReserve)
        {
            return dataLogic.GetSingleUser(userId, isReserve);
        }


        public DataTable GetSubArea(int parentId)
        {
            return dataLogic.GetSubArea(parentId);
        }

 
        public DataTable GetAgent(int grade, int areaId, int areaId2)
        {
            return dataLogic.GetAgent(grade, areaId,areaId2);
        }



        public DataTable GetManager(int agentId)
        {
            return dataLogic.GetManager(agentId);
        }


        public DataTable GetCommunicateLog(int agentId,int userId, int managerId)
        {
            return dataLogic.GetCommunicateLog(agentId, userId,managerId);
        }

        public int UpdateCommunicateLog(DataTable dataTable)
        {
            return dataLogic.UpdateCommunicateLog(dataTable);
        }


        public DataTable GetManagerRemind(int _managerId)
        {
            return dataLogic.GetManagerRemind(_managerId);
        }


        public int UpdateManagerRemind(DataTable dataTable)
        {
            return dataLogic. UpdateManagerRemind(dataTable);
        }



        public DataTable GetNews()
        {
            return dataLogic.GetNews();
        }



        public int UpdateNews(DataTable newsTable)
        {
            return dataLogic.UpdateNews(newsTable);
        }


        public DataSet GetReturnData(int agentId)
        {
            return dataLogic.GetReturnData(agentId);
        }



        public int UpdateReturnData(DataSet returnDS)
        {
            return dataLogic.UpdateReturnData(returnDS);
        }



        public int ChangeAgentSatate(int formId, int toId, string reason, int state)
        {
            return dataLogic.ChangeAgentSatate(formId, toId, reason, state);
        }

        public int ChangeUserSatate(int formId, int toId, string reason, int state)
        {
           return dataLogic.ChangeUserSatate(formId, toId, reason, state);
        }


        public int UpdateManager(DataTable managerTable)
        {
            return dataLogic.UpdateManager(managerTable);
        }


        public DataSet GetSingleManager(int _managerId)
        {
            return dataLogic.GetSingleManager(_managerId);
        }


        public int DeleteWebSite(int webSiteId)
        {
            return dataLogic.DeleteWebSite(webSiteId);
        }




        public DataTable GetWebUnionStat(int _webUnionId,int year,int month,int day)
        {
            return dataLogic.GetWebUnionStat(_webUnionId,year,month,day);
        }



        public DataSet GetCustomTask()
        {
            return dataLogic.GetCustomTask();
        }



        public int UpdateCustomTask(DataSet customTaskSet)
        {
            return dataLogic.UpdateCustomTask(customTaskSet);
        }



        public DataTable GetDomainStat(int webUnionID, string indCode, int year, int month, int day)
        {
            return dataLogic.GetDomainStat(webUnionID, indCode,year, month, day);
        }



        public int TranAgent(int _agentId, int agentId, int managerId)
        {
            return dataLogic.TranAgent(_agentId, agentId, managerId);
        }



        public int TranUser(int _userId, int agentId, int managerId)
        {
            return dataLogic.TranUser(_userId, agentId, managerId);
        }



        public DataTable GetReturnRecords(int sendOrgId, int receiveOrgId)
        {
            return dataLogic.GetReturnRecords(sendOrgId, receiveOrgId);
        }


        public int CheckAgentCharge(int chargeId, int managerId)
        {
            return dataLogic.CheckAgentCharge(chargeId, managerId);
        }


        #region DataAgent 成员


        public DataTable GetMonthReturn(DateTime today, int _agentId)
        {
            return dataLogic.GetMonthReturn(today, _agentId);
        }

        public DataTable GetSeasonReturn(DateTime today, int _agentId)
        {
            return dataLogic.GetSeasonReturn(today, _agentId);
        }

        public DataTable GetCustomReturn(DateTime today, int _agentId)
        {
            return dataLogic.GetCustomReturn(today, _agentId);
        }

        #endregion

        #region DataAgent 成员


        public int UpdateMonthReturnSetCheck(DataTable monthReturnTable)
        {
            return dataLogic.UpdateMonthReturnSetCheck(monthReturnTable);
        }

        public int UpdateSeasonReturnSetCheck(DataTable seasonReturnTable)
        {
            return dataLogic.UpdateSeasonReturnSetCheck(seasonReturnTable);
        }

        public int UpdateCustomReturnSetCheck(DataTable customReturnTable)
        {
            return dataLogic.UpdateCustomReturnSetCheck(customReturnTable);
        }

        #endregion

        #region DataAgent 成员


        public int CheckWebSite(int webSiteId, int checkType)
        {
            return dataLogic.CheckWebSite(webSiteId, checkType);
        }

        #endregion

        #region DataAgent 成员


        public int CheckUserCharge(int chargeId, int managerId)
        {
            return dataLogic.CheckUserCharge(chargeId, managerId);
        }



        public int ChargeUser(DataTable orgCashFlow)
        {
            return dataLogic.ChargeUser(orgCashFlow);
        }



        public DataTable GetSubIndustry(int parentId)
        {
            return dataLogic.GetSubIndustry(parentId);
        }


        public DataTable GetAdvWebSite(int _userId)
        {
            return dataLogic.GetAdvWebSite(_userId);
        }

        #endregion

        #region DataAgent 成员


        public DataTable GetAdvertisement(int _siteId)
        {
            return dataLogic.GetAdvertisement(_siteId);
        }

        #endregion
    }
}
