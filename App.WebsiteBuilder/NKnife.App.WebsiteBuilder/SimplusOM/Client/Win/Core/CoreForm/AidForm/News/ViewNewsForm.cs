using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewNewsForm : OMBaseForm
    {

                BindingSource _newsBDS = new BindingSource();
        DataTable _newsTable = new DataTable();
        public ViewNewsForm()
        {
            InitializeComponent();

            newsDGV.AutoGenerateColumns = false;

            mainToolStrip.Items["NewTSButton"].Visible = true;
            mainToolStrip.Items["EditTSButton"].Visible = true;
            mainToolStrip.Items["DeleteTSButton"].Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            _newsTable = OMWorkBench.DataAgent.GetNews();
            _newsBDS.DataSource = _newsTable;

            newsDGV.DataSource = _newsBDS;

            newsId.DataPropertyName = "id";
            newsTitle.DataPropertyName = "title";
            newsCol.DataPropertyName = "colName";
            newsTime.DataPropertyName = "publish_time";
            newsContent.DataPropertyName = "content";
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string filterStr = "1=1 ";
            if (!string.IsNullOrEmpty(titleTextBox.Text))
                filterStr += " and title like '%" + titleTextBox.Text + "%'";
            if (beginDTP.Checked)
                filterStr += "and publish_time>='" + beginDTP.Value.ToShortDateString() + "'";
            if (endDTP.Checked)
                filterStr += "and publish_time<='" + endDTP.Value+ "'";

            _newsBDS.Filter = filterStr;
        }

        public override void NewCmd()
        {
            NewsForm newForm=new NewsForm();
            if (newForm.ShowDialog() == DialogResult.OK)
            {
                DataRow newRow = _newsTable.NewRow();
                newRow["manager_id"] = OMWorkBench.MangerId;
                newRow["title"] = newForm.NewsTitle;
                newRow["column_id"] = newForm.NewsCol;
                newRow["publish_time"] = newForm.NewsPublishTime;
                newRow["content"] = newForm.NewsContent;
                _newsTable.LoadDataRow(newRow.ItemArray, false);

                int rows = OMWorkBench.DataAgent.UpdateNews(_newsTable.GetChanges());
                _newsTable = OMWorkBench.DataAgent.GetNews();
                _newsBDS.DataSource = _newsTable;
            }
        }

        public override void EditCmd()
        {
            int newsId = Convert.ToInt32(newsDGV.CurrentRow.Cells["newsId"].Value);
            NewsForm newForm = new NewsForm();

            DataRow editRow = _newsTable.Select("id=" + newsId)[0];
            newForm.NewsTitle = editRow["title"].ToString();
            newForm.NewsCol =Convert.ToInt32(editRow["column_id"]);
            newForm.NewsPublishTime =Convert.ToDateTime(editRow["publish_time"]);
            newForm.NewsContent = editRow["content"].ToString();
            if (newForm.ShowDialog() == DialogResult.OK)
            {
                editRow["manager_id"] = OMWorkBench.MangerId;
                editRow["title"] = newForm.NewsTitle;
                editRow["column_id"] = newForm.NewsCol;
                editRow["publish_time"] = newForm.NewsPublishTime;
                editRow["content"] = newForm.NewsContent;

                int rows = OMWorkBench.DataAgent.UpdateNews(_newsTable.GetChanges());
                _newsTable.AcceptChanges();
            }
        }


        public override void DeleteCmd()
        {
            if (MessageBox.Show("您确定要删除吗？", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRow delRow = _newsTable.Rows[newsDGV.CurrentRow.Index];
                delRow.Delete();
                int i = OMWorkBench.DataAgent.UpdateNews(_newsTable.GetChanges());
                _newsTable.AcceptChanges();
                if (i > 0)
                    MessageBox.Show("删除成功！");
            }
        }
    }
}
