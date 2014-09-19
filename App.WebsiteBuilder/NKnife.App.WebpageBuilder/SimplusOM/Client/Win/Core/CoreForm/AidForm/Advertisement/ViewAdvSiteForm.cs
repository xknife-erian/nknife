using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewAdvSiteForm : OMBaseForm
    {
        int _userId = -1;
        string _userName = "";
        DataTable _advWebSiteTable = null;
        BindingSource _advWebSiteSource = new BindingSource();

        public ViewAdvSiteForm(int userId,string userName)
        {
            InitializeComponent();
            _userId = userId;
            _userName = userName;
            advWebSiteDGV.AutoGenerateColumns = false;
            this.TabText = "广告网站__" + _userName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _advWebSiteTable = OMWorkBench.DataAgent.GetAdvWebSite(_userId);
            _advWebSiteSource.DataSource = _advWebSiteTable;

            advWebSiteDGV.DataSource = _advWebSiteSource;
            id.DataPropertyName = "websiteId";
            userName.DataPropertyName = "userName";
            siteName.DataPropertyName = "websiteName";
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell =advWebSiteDGV.CurrentRow.Cells[e.ColumnIndex] as DataGridViewCell;
            int siteId=Convert.ToInt32(advWebSiteDGV.CurrentRow.Cells["id"].Value);
            string sitename = advWebSiteDGV.CurrentRow.Cells["siteName"].Value.ToString();
            switch (cell.OwningColumn.Name)
            {
                case "siteName":
                    {
                        OMWorkBench.CreateForm(new ViewAdvForm(siteId,sitename));
                        break;
                    }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
          //  if (!string.IsNullOrEmpty(webSiteNameText.Text))
                _advWebSiteSource.Filter = "websiteName like '%" + webSiteNameText.Text + "%'";
        }
    }
}
