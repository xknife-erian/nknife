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
    public partial class ChildAgentInfoForm : OMBaseForm
    {
        DataSet _agentDS = null;
        int _agentId = -1;
        public ChildAgentInfoForm(int agentId)
        {
            InitializeComponent();
            _agentId = agentId;
        }

        private void AgentInfoForm_Load(object sender, EventArgs e)
        {
            _agentDS =OMWorkBench.DeserializeDataSet( OMWorkBench.DataAgent.GetSingleAgent(_agentId));
            #region
            DataTable areaTable = OMWorkBench.BaseInfoDS.Tables["area"];
            DataTable orgTable = _agentDS.Tables["organization"];//代理商基础信息表
            DataTable cashFlowRecordTable = _agentDS.Tables["flowRecords"];//现金流记录表
            DataTable downCashFlowRecordTable = _agentDS.Tables["downflowRecords"];
            DataTable lastChargeRecordTable = _agentDS.Tables["lastChargeRecord"];//最后充值记录

            DataTable cashFlowTable = _agentDS.Tables["organize_cash_flow"];//现金流表
            DataTable monthTaskTable = _agentDS.Tables["month_task"];//月任务表
            DataTable seasonTaskTable = _agentDS.Tables["season_task"];//季任务表

            DataRow orgRow = orgTable.Rows[0];
            this.TabText = "子代理商信息查看__" + orgRow["name"].ToString();

            IDLab.Text = orgRow["code"].ToString();
            NameLab.Text = orgRow["name"].ToString();
            SiteLab.Text = orgRow["website_url"].ToString();
            AreaLab.Text = areaTable.Select("id=" + Convert.ToInt32(orgRow["area_id"]))[0]["name"].ToString();
            TelLab.Text = orgRow["phone"].ToString();
            switch (orgRow["grade"].ToString())
            {
                case "0": LevelLab.Text = "Jeelu"; break;
                case "1": LevelLab.Text = "总代"; break;
                case "2": LevelLab.Text = "二代"; break;
            }


            if (_agentDS.Tables["org_stat"].Rows.Count > 0)//下级代理商数量统计
            {
                SubStandardAgentNumLab.Text = _agentDS.Tables["org_stat"].Rows[0]["standardAgentCount"].ToString();
                SubLateAgentNumLab.Text = _agentDS.Tables["org_stat"].Rows[0]["lateAgentCount"].ToString();
            }
            if (_agentDS.Tables["org_user_stat"].Rows.Count > 0)//下级广告主数量统计
            {
                SubStandardUserNumLab.Text = _agentDS.Tables["org_user_stat"].Rows[0]["standardUserCount"].ToString();
                SubLateUserNumLab.Text = _agentDS.Tables["org_user_stat"].Rows[0]["lateUserCount"].ToString();
            }
            ManagerNumLab.Text = orgRow["managerNum"].ToString();
            ParentAgentLab.Text = orgRow["pname"].ToString();
            decimal balance = Convert.ToDecimal(orgRow["balance"]);
            BalanceLab.Text = balance.ToString("#.##");

            if (lastChargeRecordTable.Rows.Count > 0)
            {
                LastChargeTimeLab.Text = lastChargeRecordTable.Rows[0]["trade_time"].ToString();
                LastChargeNumLab.Text = Convert.ToDecimal(lastChargeRecordTable.Rows[0]["amount"]).ToString("#.##");
            }
            else
            {
                LastChargeTimeLab.Text = LastChargeNumLab.Text = "0";
            }
            ChargeSubLab.Text = downCashFlowRecordTable.Rows[0]["chargeChildRecords"].ToString();
            ChargeLab.Text = cashFlowRecordTable.Rows[0]["chargeRecords"].ToString();

            ReturnSubLab.Text = downCashFlowRecordTable.Rows[0]["returnChildRecords"].ToString();
            ReturnLab.Text = cashFlowRecordTable.Rows[0]["returnRecords"].ToString();

            if (monthTaskTable.Rows.Count > 0)
            {
                decimal taskAmount = Convert.ToDecimal(monthTaskTable.Rows[0]["return_amount"] == DBNull.Value ? "0" : orgRow["taskAmount"]);
                decimal monthRetRate = balance == 0 ? 0 : (taskAmount - balance) / balance;
                MonthTaskLab.Text = taskAmount.ToString();
                MonthReturnRateLab.Text = monthTaskTable.Rows[0]["return_rate"].ToString()+" %";
                monthReturnAmountLabel.Text = monthTaskTable.Rows[0]["return_amount"].ToString();
            }
            else
            {
                MonthTaskLab.Text = MonthReturnRateLab.Text = monthReturnAmountLabel.Text = "0";
            }
            if (seasonTaskTable.Rows.Count > 0)
            {
                QuarterNumLab.Text = seasonTaskTable.Rows[0]["return_amount"].ToString();
                QuarterReturnRateLab.Text = seasonTaskTable.Rows[0]["return_rate"].ToString();
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
            OMWorkBench.CreateForm(new NewManagerForm(Convert.ToInt32(_agentDS.Tables["organization"].Rows[0]["id"])));
        }

        private void LinkLab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel clickLab = sender as LinkLabel;
            DataRow orgRow=_agentDS.Tables["organization"].Rows[0];
            switch (clickLab.Name)
            {
                case "SubStandardAgentNumLab":
                case "SubLateAgentNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewAgentForm(_agentId, -1, orgRow["name"].ToString(),false));
                        break;
                    }
                case "SubStandardUserNumLab":
                case "SubLateUserNumLab":
                    {
                        OMWorkBench.CreateForm(new ViewUserForm(false));
                        break;
                    }
                case "ManagerNumLab":
                    {  //应该不可以查看到子代理商的员工列表
                       // OMWorkBench.CreateForm(new ViewManagerForm(Convert.ToInt32(orgRow["id"]), orgRow["name"].ToString()));
                        break;
                    }
                case "ChargeLab":
                    {
                        DataTable chargeHistory = OMWorkBench.DataAgent.GetAgentChargeRecords(_agentId);
                        ChargeAgentHistForm chargeHistoryForm = new ChargeAgentHistForm(chargeHistory);
                        chargeHistoryForm.ShowDialog(this);
                        break;
                    }
                case "ChargeSubLab":
                    {
                        DataTable chargeHistory = OMWorkBench.DataAgent.GetAgentChargeSubRecords(_agentId);
                        ChargeAgentHistForm chargeHistoryForm = new ChargeAgentHistForm(chargeHistory);
                        chargeHistoryForm.ShowDialog(this);
                        break;
                    }
                case "ReturnLab":
                    {
                        int sendOrgId = -1 ;
                        int receiveOrgId =_agentId;
                        DataTable returnTable = OMWorkBench.DataAgent.GetReturnRecords(sendOrgId, receiveOrgId);
                        ReturnRecordsForm returnForm = new ReturnRecordsForm(returnTable);
                        returnForm.ShowDialog(this);
                        break;
                    }
                case "ReturnSubLab":
                    {
                        int sendOrgId =_agentId ;
                        int receiveOrgId = -1;
                        DataTable returnTable = OMWorkBench.DataAgent.GetReturnRecords(sendOrgId, receiveOrgId);
                        ReturnRecordsForm returnForm = new ReturnRecordsForm(returnTable);
                        returnForm.ShowDialog(this);
                        break;
                    }
                default:break;
            }
        }




    }
}
