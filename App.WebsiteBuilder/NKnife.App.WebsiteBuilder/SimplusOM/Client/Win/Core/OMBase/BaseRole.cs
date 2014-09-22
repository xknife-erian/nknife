using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusOM.Client
{
    public class Agent
    {
        public string AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentArea { get; set; }
        public string AgentMonthTask { get; set; }
        public string AgentFirstCharge { get; set; }
        public string AgentLinkMan { get; set; }
        public string AgentLinkTel { get; set; }
        public string AgentPostCode { get; set; }
        public string AgentAdress { get; set; }
        public string AgentEmail { get; set; }
        public string AgentURL { get; set; }

        public string AgentManagerId { get; set; }
        public string AgentManagerPwd { get; set; }
    }

    public class Manager
    {
        public string ManagerID { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPwd { get; set; }
        public string ManagerQQ { get; set; }
        public string ManagerMSN { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerAreas { get; set; }
    }

    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassWord { get; set; }

        public string UserLinkMan { get; set; }
        public string UserLinkTel { get; set; }
        public string UserFax { get; set; }
        public string UserEmail { get; set; }

        public string UserPostCode { get; set; }
        public string UserAdress { get; set; }

        public string WebURL { get; set; }
        public string WebSite { get; set; }
        public string WebIndustry { get; set; }
    }
}
