using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ReturnSetCheckForm : Form
    {
        int _agentId = -1;
        DataTable monthReturnTable = null;
        DataTable seasonReturnTable = null;
        DataTable customReturnTable = null;

        public ReturnSetCheckForm(int agentId)
        {
            InitializeComponent();
            _agentId = agentId;

            monthReturnDGV.AutoGenerateColumns =
            seasonReturnDGV.AutoGenerateColumns =
            customReturnDGV.AutoGenerateColumns = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            DateTime today = DateTime.Today;
            monthReturnTable = OMWorkBench.DataAgent.GetMonthReturn(today, _agentId);
            seasonReturnTable = OMWorkBench.DataAgent.GetSeasonReturn(today, _agentId);
            customReturnTable = OMWorkBench.DataAgent.GetCustomReturn(today, _agentId);

            monthReturnDGV.DataSource = monthReturnTable;
            seasonReturnDGV.DataSource = seasonReturnTable;
            customReturnDGV.DataSource = customReturnTable;

            monthId.DataPropertyName = "id";
            monthOrgName.DataPropertyName = "orgName";
            monthchecked.DataPropertyName = "check_state";
            monthChecker.DataPropertyName = "checkerName";
            monthYearMonth.DataPropertyName = "start_date";
            monthTask.DataPropertyName = "task_amount";
            ratea.DataPropertyName = "rate_a";
            rateb.DataPropertyName = "rate_b";
            ratec.DataPropertyName = "rate_c";
            rated.DataPropertyName = "rate_d";

            seasonId.DataPropertyName = "id";
            seasonOrgName.DataPropertyName = "orgName";
            seasonYearMonth.DataPropertyName = "start_date";
            seasonTaskAmount.DataPropertyName = "task_amount";
            seasonRateBase.DataPropertyName = "rate_base";
            seasonRateInc.DataPropertyName = "rate_inc";
            seasonCheck.DataPropertyName = "check_state";
            seasonChecker.DataPropertyName = "checkerName";

            customId.DataPropertyName = "id";
            customName.DataPropertyName = "task_name";
            customRateBase.DataPropertyName = "rate_base";
            customRateInc.DataPropertyName = "rate_inc";
            customcheck.DataPropertyName = "check_state";
            customChecker.DataPropertyName = "checkerName";
            beginDate.DataPropertyName = "start_time";
            endDate.DataPropertyName = "end_time";
            defAmount.DataPropertyName = "default_amount";

            base.OnLoad(e);
        }

        private void monthCheckBtn_Click(object sender, EventArgs e)
        {
            SetForReturnSetCheck(monthReturnDGV, monthReturnTable, "monthChecker");
        }

        private void seasonCheckBtn_Click(object sender, EventArgs e)
        {
            SetForReturnSetCheck(seasonReturnDGV, seasonReturnTable, "seasonChecker");
        }

        private void customCheckBtn_Click(object sender, EventArgs e)
        {
            SetForReturnSetCheck(customReturnDGV, customReturnTable, "customChecker");
        }

        private void monthReturnDGV_SelectionChanged(object sender, EventArgs e)
        {
            SetDGVReadOnly(monthReturnDGV, monthReturnTable, "monthId");
        }

        private void seasonReturnDGV_SelectionChanged(object sender, EventArgs e)
        {
            SetDGVReadOnly(seasonReturnDGV, seasonReturnTable, "seasonId");
        }

        private void customReturnDGV_SelectionChanged(object sender, EventArgs e)
        {
            SetDGVReadOnly(customReturnDGV, customReturnTable, "customId");
        }

        void SetForReturnSetCheck(DataGridView dgv, DataTable table, string checker)
        {
            dgv.EndEdit();

            foreach (DataRow dr in table.Rows)
            {
                if (dr["check_state"] != DBNull.Value && Convert.ToBoolean(dr["check_state"]))
                {
                    dr["checker_id"] = OMWorkBench.MangerId;
                }
            }
            int s = 0;
            switch (checker)
            {
                case "monthChecker":
                    {
                        s = OMWorkBench.DataAgent.UpdateMonthReturnSetCheck(table.GetChanges());
                        break;
                    }
                case "seasonChecker":
                    {
                        s = OMWorkBench.DataAgent.UpdateSeasonReturnSetCheck(table.GetChanges());
                        break;
                    }
                case "customChecker":
                    {
                        s = OMWorkBench.DataAgent.UpdateCustomReturnSetCheck(table.GetChanges());
                        break;
                    }
            }

            if (s > 0)
            {
                dgv.CurrentRow.Cells[checker].Value = OMWorkBench.MangerName;
                table.AcceptChanges();
            }
        }

        void SetDGVReadOnly(DataGridView dgv, DataTable table, string idName)
        {
            int ins = Convert.ToInt32(dgv.CurrentRow.Cells[idName].Value);
            if (ins > 0)
            {
                DataRow dr = table.Select("id=" + ins)[0];
                if (dr["check_state", DataRowVersion.Original] != DBNull.Value && Convert.ToBoolean(dr["check_state", DataRowVersion.Original]))
                {
                    dgv.CurrentRow.ReadOnly = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            monthReturnTable = OMWorkBench.DataAgent.GetMonthReturn(DateTime.MinValue, -1);
            monthReturnDGV.DataSource = monthReturnTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seasonReturnTable = OMWorkBench.DataAgent.GetSeasonReturn(DateTime.MinValue, -1);
            seasonReturnDGV.DataSource = seasonReturnTable;
        }


    }
}
