using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ManagerMindForm : OMBaseForm
    {
        int _managerId = -1;
        BindingSource _remindBDS = new BindingSource();
        DataTable _remindTable = null;
        public ManagerMindForm(int managerId)
        {
            InitializeComponent();

            _managerId = managerId;
            remindDGV.AutoGenerateColumns = false;
            remindDGV.AllowUserToAddRows = false;

            this.TabText += "--";// +_parentAgentName;
            mainToolStrip.Items["NewTSButton"].Visible = true;
            mainToolStrip.Items["EditTSButton"].Visible = true;
            // mainToolStrip.Items["FrozedTSButton"].Visible = true;
            mainToolStrip.Items["DeleteTSButton"].Visible = true;
            // mainToolStrip.Items["ChargeTSButton"].Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            _remindTable = OMWorkBench.DataAgent.GetManagerRemind(_managerId);
            _remindBDS = new BindingSource();
            _remindBDS.DataSource = _remindTable;
            remindDGV.DataSource = _remindBDS;
            comTime.DataPropertyName = "remind_time";
            comContent.DataPropertyName = "description";

            base.OnLoad(e);
        }



        public override void NewCmd()
        {
            RemindForm remindForm = new RemindForm();
            if (remindForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = _remindTable.NewRow();
                newRow["manager_id"] = _managerId;
                newRow["remind_time"] = DateTime.Now;
                newRow["description"] = remindForm.Context;
                _remindTable.LoadDataRow(newRow.ItemArray, false);
                int i = OMWorkBench.DataAgent.UpdateManagerRemind(_remindTable.GetChanges());
                _remindTable.AcceptChanges();
                _remindTable = OMWorkBench.DataAgent.GetManagerRemind(_managerId);
            }
        }

        public override void EditCmd()
        {
            RemindForm remindForm = new RemindForm();
            remindForm.Context = remindDGV.CurrentRow.Cells["comContent"].Value.ToString();
            if (remindForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = _remindTable.Rows[remindDGV .CurrentRow.Index];
                newRow["remind_time"] = DateTime.Now;
                newRow["description"] = remindForm.Context;
                int i = OMWorkBench.DataAgent.UpdateManagerRemind(_remindTable.GetChanges());
                _remindTable.AcceptChanges();
                _remindBDS.DataSource = _remindTable;
            }
        }

        
        public override void DeleteCmd()
        {
            if (MessageBox.Show("您确定要删除吗？", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRow newRow = _remindTable.Rows[remindDGV.CurrentRow.Index];
                newRow.Delete();
                int i = OMWorkBench.DataAgent.UpdateManagerRemind(_remindTable.GetChanges());
                _remindTable.AcceptChanges();
                if (i > 0)
                    MessageBox.Show("删除成功！");
            }
        }

        /// <summary>
        /// 筛选查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBtn_Click(object sender, EventArgs e)
        {
            string filterStr = "1=1";
            if (beginDTP.Checked)
                filterStr += "and remind_time >= '" + beginDTP.Value.ToShortDateString() + "'";
            if (endDTP.Checked)
                filterStr += "' and remind_time<'" + endDTP.Value + "'";
            _remindBDS.Filter = filterStr;


           
        }



    }
}
