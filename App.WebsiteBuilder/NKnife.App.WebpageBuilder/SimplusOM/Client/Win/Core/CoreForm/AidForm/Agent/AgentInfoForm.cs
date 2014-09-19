using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusOM.Client.Win;

namespace Jeelu.SimplusOM.Client
{
    public partial class AgentInfoForm : OMBaseForm
    {
        DataSet _agentDS = null;
        int _agentId = -1;
        public AgentInfoForm(int agentId)
        {
            InitializeComponent();
            _agentId = agentId;
        }

        private void AgentInfoForm_Load(object sender, EventArgs e)
        {
            _agentDS =OMWorkBench.DeserializeDataSet(OMWorkBench.DataAgent.GetSingleAgent(_agentId));
            
            #region 
            DataTable areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            DataTable orgTable = _agentDS.Tables["organization"];//代理商基础信息表
            DataTable cashFlowRecordTable = _agentDS.Tables["flowRecords"];//现金流记录表
            DataTable downCashFlowRecordTable = _agentDS.Tables["downflowRecords"];//对下现金流量表
            DataTable lastChargeRecordTable = _agentDS.Tables["lastChargeRecord"];//最后充值记录
            
            DataTable cashFlowTable = _agentDS.Tables["organize_cash_flow"];//现金流表
            DataTable monthTaskTable = _agentDS.Tables["month_task"];//月任务表
            DataTable seasonTaskTable = _agentDS.Tables["season_task"];//季任务表

            DataRow orgRow=orgTable.Rows[0];

            IDLab.Text = orgRow["code"].ToString();
            NameLab.Text = orgRow["name"].ToString();
            SiteLab.Text = orgRow["website_url"].ToString();
            addressLab.Text = orgRow["address"].ToString();
            AreaLab.Text = areaTable.Select("id=" + Convert.ToInt32(orgRow["area_id"]))[0]["name"].ToString();//所在地理区域
            TelLab.Text = orgRow["phone"].ToString();//电话
            switch (orgRow["grade"].ToString())//代理等级
            {
                case "0": LevelLab.Text = "Jeelu"; break;
                case "1": LevelLab.Text = "总代"; break;
                case "2": LevelLab.Text = "二代"; break;
            }

            if (_agentDS.Tables["org_stat"].Rows.Count > 0)//下级代理商数量统计
            {
                SubStandardAgentNumLab.Text = _agentDS.Tables["org_stat"].Rows[0]["standardAgentCount"].ToString();//正式代理商
                SubLateAgentNumLab.Text = _agentDS.Tables["org_stat"].Rows[0]["lateAgentCount"].ToString();//潜在代理商
            }
            if (_agentDS.Tables["org_user_stat"].Rows.Count > 0)//下级广告主数量统计
            {
                SubStandardUserNumLab.Text = _agentDS.Tables["org_user_stat"].Rows[0]["standardUserCount"].ToString();//正式广告主
                SubLateUserNumLab.Text = _agentDS.Tables["org_user_stat"].Rows[0]["lateUserCount"].ToString();//潜在广告主
            }
            ManagerNumLab.Text = orgRow["managerNum"].ToString();//管理员数量
            ParentAgentLab.Text = orgRow["pname"].ToString();//所属代理商
            decimal balance = Convert.ToDecimal(orgRow["balance"]);//余额
            BalanceLab.Text = balance.ToString("#.##");

            OMWorkBench.Balance = balance;

            if (lastChargeRecordTable.Rows.Count > 0)//最后充值
            {
                LastChargeTimeLab.Text = lastChargeRecordTable.Rows[0]["trade_time"].ToString();//最后充值时间
                LastChargeNumLab.Text = Convert.ToDecimal(lastChargeRecordTable.Rows[0]["amount"]).ToString("#.##");//最后充值金额
            }
            else
            {
                LastChargeTimeLab.Text = "";
                LastChargeNumLab.Text = "";
            }

            ChargeSubLab.Text = downCashFlowRecordTable.Rows[0]["chargeChildRecords"].ToString();//对下充值记录
            ChargeLab.Text = cashFlowRecordTable.Rows[0]["chargeRecords"].ToString();//被充值记录

            ReturnSubLab.Text = downCashFlowRecordTable.Rows[0]["returnChildRecords"].ToString();//向下返点记录
            ReturnLab.Text = cashFlowRecordTable.Rows[0]["returnRecords"].ToString();//返点记录

            if (monthTaskTable.Rows.Count > 0)
            {
                decimal taskAmount = Convert.ToDecimal(monthTaskTable.Rows[0]["return_amount"] == DBNull.Value ? "0" : monthTaskTable.Rows[0]["task_amount"]);
                decimal monthRetRate = balance == 0 ? 0 : (taskAmount - balance) / balance;
                MonthTaskLab.Text = taskAmount.ToString("#.##");//月任务
                MonthReturnRateLab.Text =monthTaskTable.Rows[0]["return_rate"].ToString()+" %";//月返点比率
                monthReturnAmountLabel.Text =Convert.ToDecimal(monthTaskTable.Rows[0]["return_amount"]).ToString("#.##");//月返点金额
            }
            else
            {
                MonthTaskLab.Text = MonthReturnRateLab.Text =monthReturnAmountLabel.Text= "0";
            }
            if (seasonTaskTable.Rows.Count > 0)
            {
                QuarterNumLab.Text = seasonTaskTable.Rows[0]["return_amount"].ToString();//季度返点金额
                QuarterReturnRateLab.Text =(Convert.ToDecimal(seasonTaskTable.Rows[0]["return_rate"])).ToString("p");//季度返点比率
            }
            else
            {
                QuarterNumLab.Text = QuarterReturnRateLab.Text = "0";
            }

            decimal returnAmount = Convert.ToDecimal(cashFlowRecordTable.Rows[0]["returnSum"]);
            decimal returnSubAmount = Convert.ToDecimal(downCashFlowRecordTable.Rows[0]["returnchildSum"]);
            decimal incomeAmount = returnAmount - returnSubAmount;
            IncomeLab.Text = incomeAmount.ToString("#.##");//收益=被返点-向下返点
            #endregion
        }

        public override void NewCmd()
        {
            OMWorkBench.CreateForm(new NewManagerForm(OMWorkBench.AgentId));
        }

        private void LinkLab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel clickLab = sender as LinkLabel;
            switch (clickLab.Name)
            {
                case "SubStandardAgentNumLab":
                case "SubLateAgentNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewAgentForm(OMWorkBench.AgentId, OMWorkBench.MangerId, _agentDS.Tables["organization"].Rows[0]["name"].ToString(), false));
                        break;
                    }
                case "SubStandardUserNumLab":
                case "SubLateUserNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewUserForm(false));
                        break;
                    }
                case "ManagerNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewManagerForm(OMWorkBench.AgentId, OMWorkBench.AgentName));
                        break;
                    }
                case "ChargeLab":
                    {
                        DataTable chargeHistory = OMWorkBench.DataAgent.GetAgentChargeRecords(OMWorkBench.AgentId);
                        ChargeAgentHistForm chargeHistoryForm = new ChargeAgentHistForm(chargeHistory);
                        chargeHistoryForm.ShowDialog(this);
                        break;
                    }
                case "ChargeSubLab":
                    {
                        DataTable chargeHistory = OMWorkBench.DataAgent.GetAgentChargeSubRecords(OMWorkBench.AgentId);
                        ChargeAgentHistForm chargeHistoryForm = new ChargeAgentHistForm(chargeHistory);
                        chargeHistoryForm.ShowDialog(this);
                        break;
                    }
                case "ReturnLab":
                    {
                        int sendOrgId = -1;
                        int receiveOrgId =OMWorkBench.AgentId ;
                        DataTable returnTable = OMWorkBench.DataAgent.GetReturnRecords(sendOrgId,receiveOrgId);
                        ReturnRecordsForm returnForm=new ReturnRecordsForm(returnTable);
                        returnForm.ShowDialog(this);
                        break;
                    }
                case "ReturnSubLab":
                    {
                        int sendOrgId = -1;
                        int receiveOrgId =OMWorkBench.AgentId ;
                        DataTable returnTable = OMWorkBench.DataAgent.GetReturnRecords(sendOrgId, receiveOrgId);
                        ReturnRecordsForm returnForm = new ReturnRecordsForm(returnTable);
                        returnForm.ShowDialog(this);
                        break;
                    }
                default: break;
            }
        }




    }
}
