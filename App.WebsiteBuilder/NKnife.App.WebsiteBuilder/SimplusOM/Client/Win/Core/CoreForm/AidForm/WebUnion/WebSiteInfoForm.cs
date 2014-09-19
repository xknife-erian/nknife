using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class WebSiteInfoForm : Form
    {
        int _webUnionId=0;
        string _indCode="";
        int _year=0;
        int _month=0;
        int _day=0;

        public WebSiteInfoForm(int webUnionId,string indCode,int year, int month, int day)
        {
            InitializeComponent();

            statDGV.AutoGenerateColumns = false;  

            _webUnionId = webUnionId;
            _indCode = indCode;
            _year = year;
            _month = month;
            _day = day;
        }

        protected override void OnLoad(EventArgs e)
        {
            id.DataPropertyName = "id";
            siteURL.DataPropertyName = "siteurl";
            siteName.DataPropertyName = "sitename";
            ipClick.DataPropertyName = "ipclicks";
            income.DataPropertyName = "total_price";

            DataTable webDomainStatDT = OMWorkBench.DataAgent.GetDomainStat(_webUnionId, _indCode, _year, _month, _day);
            BindingSource domainStatSource = new BindingSource();
            domainStatSource.DataSource = webDomainStatDT;
            statDGV.DataSource = domainStatSource;

            int ipclickCount = 0;
            decimal incomeCount = 0;
            foreach (DataRow dr in webDomainStatDT.Rows)
            {
                ipclickCount += Convert.ToInt32(dr["ipclicks"]);
                incomeCount += Convert.ToInt32(dr["total_price"]);
            }

            sumIncomeLabel.Text = incomeCount.ToString();
            ipclickCountLabel.Text = ipclickCount.ToString();
            base.OnLoad(e);
        }

    }
}
