using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class IndustryControl : UserControl
    {
        public IndustryControl()
        {
            InitializeComponent();
        }

        public ComboBox Industry1ComboBox
        {
            get
            {
                return industry1ComboBox;
            }
        }
        public ComboBox Industry2ComboBox
        {
            get
            {
                return industry2ComboBox;
            }
        }

        DataTable _industryTable = null;
        public DataTable IndustrySource
        {
            set
            {
                _industryTable = value;
                BindingSource industrySource = new BindingSource();
                industrySource.DataSource = _industryTable;
                industrySource.Filter = "level=1";

                industry1ComboBox.ValueMember = "id";
                industry1ComboBox.DisplayMember = "name";
                industry1ComboBox.DataSource = industrySource;
            }
            get
            {
                return _industryTable;
            }
        }

        private void IndustryComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox currentComboBox = sender as ComboBox;

            switch (currentComboBox.Name)
            {
                case "industry1ComboBox":
                    {
                        int parentId = Convert.ToInt32(industry1ComboBox.SelectedValue);
                        DataTable subAreaTable = OMWorkBench.DataAgent.GetSubIndustry(parentId);
                        industry2ComboBox.ValueMember = "id";
                        industry2ComboBox.DisplayMember = "name";
                        industry2ComboBox.DataSource = subAreaTable;

                        break;
                    }
            }
        }
    }
}
