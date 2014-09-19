using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewSingleUserForm : Form
    {
        DataRow _userRow = null;
        int _userId = -1;
        bool _isReserve=false;
        public ViewSingleUserForm(int userId, bool isReserve)
        {
            InitializeComponent();

            _userId = userId;
            DataTable userTable = OMWorkBench.DataAgent.GetSingleUser(userId,isReserve);
            _userRow = userTable.Rows[0];
            _isReserve = isReserve;

            InitLabValue();
        }

        private void InitLabValue()
        {
            try
            {
                IdLabel.Text = _userRow["username"].ToString();
                nameLabel.Text = _userRow["realname"].ToString();
                websiteLabel.Text = _userRow["web"].ToString();
                areaLabel.Text = OMWorkBench.BaseInfoDS.Tables["area"].Select("id=" + Convert.ToInt32(_userRow["city_id"]))[0]["name"].ToString();
                LinkManLabel.Text = _userRow["nickname"].ToString();
                TelLabel.Text = _userRow["phone"].ToString();
                parentLabel.Text = _userRow["orgName"].ToString();
                QQLabel.Text = _userRow["qq"].ToString();
                addressLab.Text = _userRow["detail_addr"].ToString();
                MSNLabel.Text = _userRow["msn"].ToString();
                emailLabel.Text = _userRow["email"].ToString();
                banlanceLabel.Text = Convert.ToDecimal(_userRow["balance"]).ToString("#.##");
                chargeNumLabel.Text = Convert.ToDecimal(_userRow["total_charged"]).ToString("#.##");
                if (!_isReserve)
                {
                    lastChargeTimeLabel.Text = _userRow["lastChargeTime"].ToString();
                    lastChargeAmountLabel.Text = Convert.ToDecimal(_userRow["lastChargeAmount"]).ToString("#.##");
                }
                else
                {
                    lastChargeTimeLabel.Text = "";
                    lastChargeAmountLabel.Text = "";
                }
            }
            catch { }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel thisLabel = sender as LinkLabel;
            switch (thisLabel.Name)
            {
                case "parentLabel":
                    {
                        break;
                    }
                case "chargeNumLabel":
                    {
                        DataTable chargeUserHistory = OMWorkBench.DataAgent.GetUserCharge(_userId);
                        ChargeUserHistForm chargeUser = new ChargeUserHistForm(chargeUserHistory);
                        chargeUser.ShowDialog(this);
                        break;
                    }
            }
        }

        private void addCommunicateBtn_Click(object sender, EventArgs e)
        {
            CommunicateForm comForm = new CommunicateForm();
            DataTable communicateLogTable = OMWorkBench.DataAgent.GetCommunicateLog(-1, -1, -1);
            
            if (comForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = communicateLogTable.NewRow();
                newRow["manager_id"] = OMWorkBench.MangerId;
                newRow["communicate_time"] = DateTime.Now;
                newRow["user_id"] = _userId;
                newRow["communicate_type"] = 2;
                newRow["description"] = comForm.Context;

                communicateLogTable.LoadDataRow(newRow.ItemArray, false);
                int i = OMWorkBench.DataAgent.UpdateCommunicateLog(communicateLogTable.GetChanges());
                communicateLogTable.AcceptChanges();
            }
        }
    }
}
