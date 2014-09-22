using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewAdvForm : OMBaseForm
    {
        int _siteId = -1;
        string _siteName = "";
        DataTable _advTable = null;
        BindingSource _advSource = new BindingSource();

        public ViewAdvForm(int siteId, string siteName)
        {
            InitializeComponent();
            _siteId = siteId;
            _siteName = siteName;
            advDGV.AutoGenerateColumns = false;
            this.TabText = "广告__" + _siteName;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _advTable = OMWorkBench.DataAgent.GetAdvertisement(_siteId);
            _advSource.DataSource = _advTable;

            advDGV.DataSource = _advSource;
            id.DataPropertyName = "advId";
            siteName.DataPropertyName = "websiteName";
            advName.DataPropertyName = "advName";
            tempName.DataPropertyName = "tempName";

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*DataGridViewCell cell = sender as DataGridViewCell;
            switch (cell.OwningColumn.Name)
            {
                case "siteName":
                    {
                        
                        break;
                    }
            }*/
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
                _advSource.Filter = "advName like '%" + advNameText.Text + "%'";
        }
    }
}
