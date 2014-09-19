using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class AddDelAgentControl : UserControl
    {
        DataTable addTable = null;
        Dictionary<int, string> areaPairs = new Dictionary<int, string>();
        public Dictionary<int, string> AreaPairs
        {
            get { return areaPairs; }
            set { areaPairs = value; }
        }

        DataTable areaTable = null;
        public DataTable AreaTable
        {
            get { return areaTable; }
            set
            {
                areaTable = value;
                BindingSource allSource = new BindingSource();
                allSource.DataSource = areaTable;

                AllAreaListBox.ValueMember = "id";
                AllAreaListBox.DisplayMember = "name";
                AllAreaListBox.DataSource = allSource;

                AddAreaListBox.ValueMember = "id";
                AddAreaListBox.DisplayMember = "name"; 
                addTable = areaTable.Clone();
                AddAreaListBox.DataSource = addTable;
            }
        }

        public AddDelAgentControl()
        {
            InitializeComponent();
        }

        private void AddAreaBtn_Click(object sender, EventArgs e)
        {

            int areaId = Convert.ToInt32(AllAreaListBox.SelectedValue);
            DataRow allRow = AreaTable.Select("id=" + areaId)[0];
            if (!areaPairs.ContainsKey(areaId))
            {
                addTable.LoadDataRow(allRow.ItemArray, false);
                areaPairs.Add(Convert.ToInt32(allRow["id"]), allRow["name"].ToString());
            }
            if (AllAreaListBox.SelectedIndex < AllAreaListBox.Items.Count - 1)
                AllAreaListBox.SelectedIndex += 1;
            else
                AllAreaListBox.SelectedIndex = 0;
        }

        private void DelAreaBtn_Click(object sender, EventArgs e)
        {
            if (AddAreaListBox.SelectedValue != null)
            {
                int areaId = Convert.ToInt32(AddAreaListBox.SelectedValue);
                areaPairs.Remove(areaId);
                DataRow addRows = addTable.Select("id=" + areaId)[0];
                addRows.Delete();
            }
        }
    }
}
