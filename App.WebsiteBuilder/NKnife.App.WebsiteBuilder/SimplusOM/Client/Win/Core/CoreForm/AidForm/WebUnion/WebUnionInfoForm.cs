using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class WebUnionInfoForm : Form
    {
        int _webUnionId = -1;
        DataSet _webSiteDS = null;
        DataTable webUnionStatDT = null;
        BindingSource unionSource = new BindingSource();
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        int day = DateTime.Today.Day;

        public WebUnionInfoForm(int webUnionId, DataSet webSiteDS)
        {
            InitializeComponent();

            statDGV.AutoGenerateColumns = false;  
            _webUnionId = webUnionId;
            _webSiteDS = webSiteDS;

            initComBoBox();
        }
        void initComBoBox()
        {
            for (int i =DateTime.Today.Year ; i>1990; i--)
            {
                yearComboBox.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                monthComboBox.Items.Add(i);
            }
            for (int i = 1; i <= 31; i++)
            {
                dayComboBox.Items.Add(i);
            }
            viewTypeComboBox.SelectedIndex = 0;
            yearComboBox.Text = year.ToString();
            monthComboBox.Text = month.ToString();
            dayComboBox.Text = day.ToString();
        }
        protected override void OnLoad(EventArgs e)
        {
            SearchResult(_webUnionId, year, month, day);
            id.DataPropertyName = "id";
            siteType.DataPropertyName = "indName";
            pageView.DataPropertyName = "pageviews";
            ipClick.DataPropertyName = "ipclicks";

            if (_webSiteDS.Tables["siteSta"].Rows.Count > 0)
            {
                DataRow dr = _webSiteDS.Tables["siteSta"].Rows[0];
                CountLabel.Text = dr["siteCount"].ToString();
                nocheckLabel.Text = dr["unchecked"].ToString();
                checkLabel.Text = dr["checked"].ToString();
                nopassLabel.Text = dr["nopass"].ToString();
            }
            base.OnLoad(e);
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                year = int.Parse(string.IsNullOrEmpty(yearComboBox.Text) ? "0": yearComboBox.Text);
                month = int.Parse(string.IsNullOrEmpty(monthComboBox.Text) ? "0" : monthComboBox.Text);
                day = int.Parse(string.IsNullOrEmpty(dayComboBox.Text) ? "0" : dayComboBox.Text);

                SearchResult(_webUnionId, year, month, day);
            }
            catch { }
        }

        void SearchResult(int _webUnionId, int year, int month, int day)
        {
            webUnionStatDT = OMWorkBench.DataAgent.GetWebUnionStat(_webUnionId, year, month, day);
            unionSource.DataSource = webUnionStatDT;
            statDGV.DataSource = unionSource;

            int pvCount = 0;
            int ipclickCount = 0;
            foreach (DataRow dr in webUnionStatDT.Rows)
            {
                pvCount += Convert.ToInt32(dr["pageviews"]);
                ipclickCount += Convert.ToInt32(dr["ipclicks"]);
            }

            pvCountLabel.Text = pvCount.ToString();
            ipclickCountLabel.Text = ipclickCount.ToString();
        }

        private void statDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow dr = webUnionStatDT.Rows[0];
            string industry_code =dr["industry_code"].ToString();
            WebSiteInfoForm siteInfo = new WebSiteInfoForm(_webUnionId,industry_code, year, month, day);
            siteInfo.ShowDialog(this);
        }
    }
}
