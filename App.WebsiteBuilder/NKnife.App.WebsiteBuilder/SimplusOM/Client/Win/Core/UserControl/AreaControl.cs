using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class AreaControl : UserControl
    {
        public AreaControl()
        {
            InitializeComponent();
        }

        public ComboBox Area1ComboBox
        {
            get
            {
                return area1ComboBox;
            }
        }
        public ComboBox Area2ComboBox
        {
            get
            {
                return area2ComboBox;
            }
        }
        public ComboBox Area3ComboBox
        {
            get
            {
                return area3ComboBox;
            }
        }

        public DataTable areaSource
        {
            set
            {
                DataTable _areaTable = value;
                BindingSource areaSource = new BindingSource();
                areaSource.DataSource = _areaTable;
                areaSource.Filter = "level=1";

                area1ComboBox.ValueMember = "id";
                area1ComboBox.DisplayMember = "name";
                area1ComboBox.DataSource = areaSource;
            }
        }

        private void AreaComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox currentComboBox = sender as ComboBox;

            switch (currentComboBox.Name)
            {
                case "area1ComboBox":
                    {
                        int parentId = Convert.ToInt32(area1ComboBox.SelectedValue);
                        DataTable subAreaTable = OMWorkBench.DataAgent.GetSubArea(parentId);
                        area2ComboBox.ValueMember = "sub_id";
                        area2ComboBox.DisplayMember = "subAreaName";
                        area2ComboBox.DataSource = subAreaTable;

                        break;
                    }
                case "area2ComboBox":
                    {
                        int parentId = Convert.ToInt32(area2ComboBox.SelectedValue);
                        DataTable subAreaTable = OMWorkBench.DataAgent.GetSubArea(parentId);
                        area3ComboBox.DataSource = subAreaTable;
                        area3ComboBox.ValueMember = "sub_id";
                        area3ComboBox.DisplayMember = "subAreaName"; break;
                    }
                case "area3ComboBox":
                    {
                        break;
                    }
            }
        }
    }
}
