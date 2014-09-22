namespace Jeelu.SimplusOM.Client
{
    partial class ViewCustomTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.customTaskDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.task_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.default_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_base = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rate_inc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_effect = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customTaskDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.tableLayoutPanel1);
            this.MainPanel.Size = new System.Drawing.Size(644, 387);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 602F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.customTaskDGV, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 387);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 74);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(516, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // customTaskDGV
            // 
            this.customTaskDGV.AllowUserToAddRows = false;
            this.customTaskDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customTaskDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.start_time,
            this.end_time,
            this.task_name,
            this.description,
            this.default_amount,
            this.rate_base,
            this.rate_inc,
            this.is_effect});
            this.customTaskDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTaskDGV.Location = new System.Drawing.Point(3, 83);
            this.customTaskDGV.Name = "customTaskDGV";
            this.customTaskDGV.RowHeadersWidth = 25;
            this.customTaskDGV.RowTemplate.Height = 23;
            this.customTaskDGV.Size = new System.Drawing.Size(638, 301);
            this.customTaskDGV.TabIndex = 1;
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // start_time
            // 
            this.start_time.HeaderText = "起始时间";
            this.start_time.Name = "start_time";
            // 
            // end_time
            // 
            this.end_time.HeaderText = "结束时间";
            this.end_time.Name = "end_time";
            // 
            // task_name
            // 
            this.task_name.HeaderText = "任务名称";
            this.task_name.Name = "task_name";
            // 
            // description
            // 
            this.description.HeaderText = "任务描述";
            this.description.Name = "description";
            // 
            // default_amount
            // 
            this.default_amount.HeaderText = "默认任务量";
            this.default_amount.Name = "default_amount";
            // 
            // rate_base
            // 
            this.rate_base.HeaderText = "反点比率(%)";
            this.rate_base.Name = "rate_base";
            // 
            // rate_inc
            // 
            this.rate_inc.HeaderText = "比率增量(%)";
            this.rate_inc.Name = "rate_inc";
            // 
            // is_effect
            // 
            this.is_effect.HeaderText = "是否生效";
            this.is_effect.Name = "is_effect";
            // 
            // ViewCustomTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 422);
            this.Name = "ViewCustomTask";
            this.TabText = "促销管理";
            this.Text = "促销管理";
            this.MainPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customTaskDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView customTaskDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn start_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn task_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn default_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_base;
        private System.Windows.Forms.DataGridViewTextBoxColumn rate_inc;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_effect;

    }
}