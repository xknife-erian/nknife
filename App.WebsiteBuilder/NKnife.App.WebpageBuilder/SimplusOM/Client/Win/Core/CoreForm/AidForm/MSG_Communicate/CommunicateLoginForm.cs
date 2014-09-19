using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class CommunicateLoginForm : OMBaseForm
    {
        int _agentId=-1;
        int _userId = -1;
        int _managerId=-1;
        DataTable communicateLogTable = null;
        BindingSource _logBDS = null;

        public CommunicateLoginForm(int agentId,int userId,int managerId)
        {
            InitializeComponent();

            _agentId = agentId;
            _userId = userId;
            _managerId = managerId;

            communicateLogDGV.AutoGenerateColumns = false;

            mainToolStrip.Items["NewTSButton"].Visible = true;
            mainToolStrip.Items["EditTSButton"].Visible = true;
            // mainToolStrip.Items["FrozedTSButton"].Visible = true;
            mainToolStrip.Items["DeleteTSButton"].Visible = true;
            // mainToolStrip.Items["ChargeTSButton"].Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.TabText += "--" + _managerId.ToString();

            communicateLogTable = OMWorkBench.DataAgent.GetCommunicateLog(_agentId,_userId, _managerId);
            _logBDS = new BindingSource();
            _logBDS.DataSource = communicateLogTable;
            communicateLogDGV.DataSource = _logBDS;
            comTime.DataPropertyName = "communicate_time";
            comContent.DataPropertyName = "description";

            base.OnLoad(e);
        }

        public override void NewCmd()
        {
            CommunicateForm comForm = new CommunicateForm();
            if (comForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = communicateLogTable.NewRow();
                newRow["manager_id"] = _managerId;
                newRow["communicate_time"] = DateTime.Now;
                if (_agentId > -1)
                {
                    newRow["organization_id"] = _agentId;
                    newRow["communicate_type"] = 1;
                }
                else if (_userId>-1)
                {
                    newRow["user_id"] = _userId;
                    newRow["communicate_type"] = 2;
                }
                
                newRow["description"] = comForm.Context;
                communicateLogTable.LoadDataRow(newRow.ItemArray, false);
               int i= OMWorkBench.DataAgent.UpdateCommunicateLog(communicateLogTable.GetChanges());
               communicateLogTable.AcceptChanges();
               if (_agentId>-1)
               communicateLogTable = OMWorkBench.DataAgent.GetCommunicateLog(_agentId,-1, _managerId);
                else 
               communicateLogTable = OMWorkBench.DataAgent.GetCommunicateLog(-1, _userId, _managerId);
            }
        }

        public override void EditCmd()
        {
            CommunicateForm comForm = new CommunicateForm();
            comForm.Context = communicateLogDGV.CurrentRow.Cells["comContent"].Value.ToString();
            if (comForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = communicateLogTable.Rows[communicateLogDGV.CurrentRow.Index];
                newRow["communicate_time"] = DateTime.Now;
                newRow["description"] = comForm.Context;
                int i = OMWorkBench.DataAgent.UpdateCommunicateLog(communicateLogTable.GetChanges());
                communicateLogTable.AcceptChanges();
                _logBDS.DataSource = communicateLogTable;
            }
        }

        public override void DeleteCmd()
        {

            if (MessageBox.Show("您确定要删除吗？", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRow newRow = communicateLogTable.Rows[communicateLogDGV.CurrentRow.Index];
                newRow.Delete();
                int i = OMWorkBench.DataAgent.UpdateCommunicateLog(communicateLogTable);//.GetChanges());
                communicateLogTable.AcceptChanges();
                if (i > 0)
                    MessageBox.Show("删除成功！");

            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string filterStr = "1=1";
            if (beginDTP.Checked)
                filterStr += " and communicate_time >= '" + beginDTP.Value.ToShortDateString() + "'";
            if (endDTP.Checked)
                filterStr += " and communicate_time<'" + endDTP.Value + "'";
            _logBDS.Filter = filterStr;

        }

        /// <summary>
        ///具体显示沟通记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void communicateLogDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string comText = communicateLogDGV.CurrentRow.Cells["comContent"].Value.ToString();
            CommunicateContentForm comForm = new CommunicateContentForm(comText);
            comForm.ShowDialog(this);
        }
    }
}
