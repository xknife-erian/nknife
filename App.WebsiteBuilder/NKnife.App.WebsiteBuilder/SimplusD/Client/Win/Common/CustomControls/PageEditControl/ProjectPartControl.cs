using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProjectPartControl : UserControl
    {
        /// <summary>
        /// 内部使用的一个变量，存储现有的数据
        /// </summary>
        List<ProjectPart> projData = new List<ProjectPart>();
        /// <summary>
        /// 提供给外界使用的
        /// </summary>
        public ProjectPart[] value
        {
            get
            {
                return projData.ToArray();
            }
            set
            {
                projData.Clear();
                foreach (ProjectPart part in value)
                {
                    projData.Add(part);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            MadeRow();
        }
        private string _pageProjectId;
        public string PageProjectId
        {
            get { return _pageProjectId; }
            set { _pageProjectId = value; }
        }
        public ProjectPartControl()
        {
            InitializeComponent();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void MadeRow()
        {
            if (this.projData != null)
            {
                foreach (ProjectPart part in projData)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridView1, part.PartName,part.PartStartTime, part.PartEndTime, part.partCost);
                    row.Tag = part;
                    this.dataGridView1.Rows.Add(row);
                    if (part.IsDoing)
                    {
                        row.DefaultCellStyle.BackColor = Color.Azure;
                    }
                }
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProjectEdit form = new ProjectEdit();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                if (form.projectPartList != null)
                {
                    ///
                    projData.Add(form.projectPartList);
                    ///
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridView1, form.projectPartList.PartName,form.projectPartList.PartStartTime, form.projectPartList.PartEndTime, form.projectPartList.partCost);
                    row.Tag = form.projectPartList;
                    this.dataGridView1.Rows.Add(row);
                    if (form.projectPartList.IsDoing)
                    {
                        row.DefaultCellStyle.BackColor = Color.Azure;
                    }
                }

            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rowCollection = this.dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in rowCollection)
            {
                ProjectPart part = (ProjectPart)row.Tag;
                int i=row.Index;
                ProjectEdit form = new ProjectEdit(part);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    part = form.projectPartList;
                    projData[i] = part;
                    row.SetValues(part.PartName,part.PartStartTime,part.PartEndTime, part.partCost);
                    if (part.IsDoing)
                    {
                        row.DefaultCellStyle.BackColor = Color.Azure;
                    }
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
             DataGridViewSelectedRowCollection rowCollection = this.dataGridView1.SelectedRows;
             foreach (DataGridViewRow row in rowCollection)
             {
                 ProjectPart part = (ProjectPart)row.Tag;
                 projData.Remove(part);
                 this.dataGridView1.Rows.Remove(row);
             }
             OnCheckChanged(EventArgs.Empty);
        }
        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }

    }
}
