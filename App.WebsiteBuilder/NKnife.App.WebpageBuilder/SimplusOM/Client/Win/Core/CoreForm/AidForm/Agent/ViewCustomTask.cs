using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class ViewCustomTask : OMBaseForm
    {
        DataSet customTaskDS = null;
        DataTable customTaskDT = null;
        public ViewCustomTask()
        {
            InitializeComponent();

            customTaskDGV.AutoGenerateColumns = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            mainToolStrip.Items["NewTSButton"].Visible = true;
            mainToolStrip.Items["EditTSButton"].Visible = true;
            mainToolStrip.Items["DeleteTSButton"].Visible = true;

            BindingReSource();
            id.DataPropertyName = "id";
            start_time.DataPropertyName = "start_time";
            end_time.DataPropertyName = "end_time";
            task_name.DataPropertyName = "task_name";
            description.DataPropertyName = "description";
            default_amount.DataPropertyName = "default_amount";
            rate_base.DataPropertyName = "rate_base";
            rate_inc.DataPropertyName = "rate_inc";
            is_effect.DataPropertyName = "is_effect";
        }


        void BindingReSource()
        {
            customTaskDS = OMWorkBench.DataAgent.GetCustomTask();
            customTaskDT = customTaskDS.Tables[0];
            customTaskDGV.DataSource = customTaskDT;
        }

        public override void NewCmd()
        {
            NewCustomTaskForm selfForm = new NewCustomTaskForm(customTaskDS);
            if (selfForm.ShowDialog() == DialogResult.OK)
            {
                BindingReSource();
            }
            base.NewCmd();
        }

        public override void DeleteCmd()
        {
            if (MessageBox.Show("您确定要删除吗？", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DataRow delRow =customTaskDS.Tables[0].Rows[customTaskDGV.CurrentRow.Index];
                delRow.Delete();
                int i = OMWorkBench.DataAgent.UpdateCustomTask(customTaskDS.GetChanges());
                customTaskDS.AcceptChanges();
                if (i > 0)
                    MessageBox.Show("删除成功！");
            }
        }
    }
}
