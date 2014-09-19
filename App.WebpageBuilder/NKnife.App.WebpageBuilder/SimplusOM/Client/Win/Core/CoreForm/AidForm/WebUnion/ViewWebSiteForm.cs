using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewWebSiteForm : OMBaseForm
    {
        BindingSource _websiteBDS = new BindingSource();
        DataSet _webSiteDS = null;
        DataSet _webUnionDS = null;
        string filterStr = "";
        int webUnionId = -1;

        public string FilterStr
        {
            get { return filterStr; }
            set { filterStr = value; }
        }

        public ViewWebSiteForm(DataSet webSiteDS,DataSet webUnionDS, string siteTypeCount)
        {
            InitializeComponent();

            webSiteDGV.AutoGenerateColumns = false;
            _webSiteDS = webSiteDS.Copy();
            _webUnionDS = webUnionDS.Copy();

            siteCountLabel.Text = siteTypeCount;

            if (webSiteDS.Tables["website"].Rows.Count > 0)
            {
                webUnionId = Convert.ToInt32(webSiteDS.Tables["website"].Rows[0]["web_union_id"]);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (OMWorkBench.ManagerWebUnion)
                {
                    mainToolStrip.Items["NewTSButton"].Visible = true;
                    mainToolStrip.Items["EditTSButton"].Visible = true;
                    mainToolStrip.Items["DeleteTSButton"].Visible = true;
                    mainToolStrip.Items["CheckTSButton"].Visible = true;
                    mainToolStrip.Items["CheckTSButton"].Text = "网站审核";
                }
                _websiteBDS.DataSource = _webSiteDS.Tables["website"];
                DataRow webSiteStaRow = _webSiteDS.Tables["siteSta"].Rows[0];

                _websiteBDS.Filter = filterStr;
                webSiteDGV.DataSource = _websiteBDS;
                siteCountLab.Text = _websiteBDS.Count.ToString();

                siteId.DataPropertyName = "id";
                sitename.DataPropertyName = "name";
                siteurl.DataPropertyName = "domain_url";
                sitetype1.DataPropertyName = "indBackName";
                sitetype2.DataPropertyName = "indName";
                siteaddtime.DataPropertyName = "regtime";
                sitestate.DataPropertyName = "checkState";

                if (_webSiteDS.Tables["siteSta"].Rows.Count > 0)
                {
                    DataRow dr = _webSiteDS.Tables["siteSta"].Rows[0];
                    CountLabel.Text = dr["siteCount"].ToString();
                    nocheckLabel.Text = dr["unchecked"].ToString();
                    checkLabel.Text = dr["checked"].ToString();
                    nopassLabel.Text = dr["nopass"].ToString();
                }
            }
            catch { }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_websiteBDS.Filter))
                _websiteBDS.Filter = " name like '%" + nameTextBox.Text + "%'";
            else
                _websiteBDS.Filter += " and name like '%" + nameTextBox.Text + "%'";
            _websiteBDS.ResetBindings(false);
        }


        public override void NewCmd()
        {
            DataRow websiteRow =_webSiteDS.Tables["website"].NewRow();
            DataTable unionTable = _webUnionDS.Tables["webunion"];
            ModifyWebSiteForm newWebUnionForm = new ModifyWebSiteForm(websiteRow,unionTable, FormUseMode.New);
            if (newWebUnionForm.ShowDialog() == DialogResult.OK)
            {
                _webSiteDS.Tables["website"].LoadDataRow(websiteRow.ItemArray, false);
                OMWorkBench.DataAgent.UpdateWebSite(_webSiteDS.Tables["website"]);
               // _webSiteDS.AcceptChanges();
                _webSiteDS = OMWorkBench.DataAgent.GetWebSite(webUnionId);
                OnLoad(null);
            }
        }

        public override void EditCmd()
        {
            DataRow websiteRow = _webSiteDS.Tables["website"].Select("id="+Convert.ToInt32(webSiteDGV.CurrentRow.Cells["siteId"].Value))[0];
            DataTable unionTable = _webUnionDS.Tables["webunion"];
            ModifyWebSiteForm editWebUnionForm = new ModifyWebSiteForm(websiteRow, unionTable,FormUseMode.Edit);
            if (editWebUnionForm.ShowDialog() == DialogResult.OK)
            {
                OMWorkBench.DataAgent.UpdateWebSite(_webSiteDS.Tables["website"]);
                _webSiteDS.AcceptChanges();
            }
        }

        public override void DeleteCmd()
        {
            if (MessageBox.Show("您确定要删除此项吗？", "删除确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int webSiteId = Convert.ToInt32(webSiteDGV.CurrentRow.Cells["siteId"].Value);
                int i = OMWorkBench.DataAgent.DeleteWebSite(webSiteId);
                if (i > 0)
                {
                    _webSiteDS.Tables["website"].Select("id=" + webSiteId)[0].Delete();
                    _webSiteDS.AcceptChanges();
                }
            }
        }

        public override void CheckCmd()
        {
            int webSiteId = Convert.ToInt32(webSiteDGV.CurrentRow.Cells["siteId"].Value);
            DataRow websiteRow = _webSiteDS.Tables["website"].Select("id=" + webSiteId)[0];
            CheckWebSiteForm checkWebSite = new CheckWebSiteForm(websiteRow);
            if (checkWebSite.ShowDialog() == DialogResult.OK)
            {
                int checkType = checkWebSite.CheckType;

                int effRows = OMWorkBench.DataAgent.CheckWebSite(webSiteId, checkType);
                if (effRows > 0)
                {
                    webSiteDGV.CurrentRow.Cells["sitetype2"].Value 
                        = OMWorkBench.BaseInfoDS.Tables["industry"].Select("code=" + checkType)[0]["name"].ToString();
                    webSiteDGV.CurrentRow.Cells["siteaddtime"].Value = DateTime.Today.ToShortDateString();
                    webSiteDGV.CurrentRow.Cells["sitestate"].Value = "审核";
                }
            }
        }

        private void webSiteDGV_SelectionChanged(object sender, EventArgs e)
        {
            int webSiteId = Convert.ToInt32(webSiteDGV.CurrentRow.Cells["siteId"].Value);
            if (webSiteId > 0)
            {
                DataRow websiteRow = _webSiteDS.Tables["website"].Select("id=" + webSiteId)[0];
                mainToolStrip.Items["CheckTSButton"].Enabled = (websiteRow["current_state"].ToString() != "1");
            }
        }

    }
}
