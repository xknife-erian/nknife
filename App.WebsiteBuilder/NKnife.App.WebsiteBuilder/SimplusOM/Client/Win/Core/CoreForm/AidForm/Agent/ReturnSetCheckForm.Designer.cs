namespace Jeelu.SimplusOM.Client
{
    partial class ReturnSetCheckForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.monthReturnDGV = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.monthCheckBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.seasonCheckBtn = new System.Windows.Forms.Button();
            this.seasonReturnDGV = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.customReturnDGV = new System.Windows.Forms.DataGridView();
            this.monthId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthOrgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthYearMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthchecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.monthTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monthChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customRateBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customRateInc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customcheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.customChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonOrgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonYearMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonRateBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonRateInc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonTaskAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seasonCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.seasonChecker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customCheckBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monthReturnDGV)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seasonReturnDGV)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customReturnDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(768, 471);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.monthReturnDGV);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.monthCheckBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(760, 446);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "月返点设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // monthReturnDGV
            // 
            this.monthReturnDGV.AllowUserToAddRows = false;
            this.monthReturnDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.monthReturnDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.monthId,
            this.monthOrgName,
            this.monthYearMonth,
            this.rated,
            this.ratec,
            this.rateb,
            this.ratea,
            this.monthchecked,
            this.monthTask,
            this.monthChecker});
            this.monthReturnDGV.Location = new System.Drawing.Point(8, 64);
            this.monthReturnDGV.Name = "monthReturnDGV";
            this.monthReturnDGV.RowHeadersWidth = 25;
            this.monthReturnDGV.RowTemplate.Height = 23;
            this.monthReturnDGV.Size = new System.Drawing.Size(744, 374);
            this.monthReturnDGV.TabIndex = 1;
            this.monthReturnDGV.SelectionChanged += new System.EventHandler(this.monthReturnDGV_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(521, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "查看所有";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // monthCheckBtn
            // 
            this.monthCheckBtn.Location = new System.Drawing.Point(619, 21);
            this.monthCheckBtn.Name = "monthCheckBtn";
            this.monthCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.monthCheckBtn.TabIndex = 0;
            this.monthCheckBtn.Text = "提交";
            this.monthCheckBtn.UseVisualStyleBackColor = true;
            this.monthCheckBtn.Click += new System.EventHandler(this.monthCheckBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.seasonCheckBtn);
            this.tabPage2.Controls.Add(this.seasonReturnDGV);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(760, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "季度返点设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(515, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "查看所有";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // seasonCheckBtn
            // 
            this.seasonCheckBtn.Location = new System.Drawing.Point(613, 21);
            this.seasonCheckBtn.Name = "seasonCheckBtn";
            this.seasonCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.seasonCheckBtn.TabIndex = 1;
            this.seasonCheckBtn.Text = "提交";
            this.seasonCheckBtn.UseVisualStyleBackColor = true;
            this.seasonCheckBtn.Click += new System.EventHandler(this.seasonCheckBtn_Click);
            // 
            // seasonReturnDGV
            // 
            this.seasonReturnDGV.AllowUserToAddRows = false;
            this.seasonReturnDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.seasonReturnDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seasonId,
            this.seasonOrgName,
            this.seasonYearMonth,
            this.seasonRateBase,
            this.seasonRateInc,
            this.seasonTaskAmount,
            this.seasonCheck,
            this.seasonChecker});
            this.seasonReturnDGV.Location = new System.Drawing.Point(9, 50);
            this.seasonReturnDGV.Name = "seasonReturnDGV";
            this.seasonReturnDGV.RowHeadersWidth = 25;
            this.seasonReturnDGV.RowTemplate.Height = 23;
            this.seasonReturnDGV.Size = new System.Drawing.Size(720, 388);
            this.seasonReturnDGV.TabIndex = 0;
            this.seasonReturnDGV.SelectionChanged += new System.EventHandler(this.seasonReturnDGV_SelectionChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.customCheckBtn);
            this.tabPage3.Controls.Add(this.customReturnDGV);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(760, 446);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "自定义返点设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // customReturnDGV
            // 
            this.customReturnDGV.AllowUserToAddRows = false;
            this.customReturnDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customReturnDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customId,
            this.beginDate,
            this.endDate,
            this.customName,
            this.defAmount,
            this.customRateBase,
            this.customRateInc,
            this.customcheck,
            this.customChecker});
            this.customReturnDGV.Location = new System.Drawing.Point(8, 60);
            this.customReturnDGV.Name = "customReturnDGV";
            this.customReturnDGV.RowHeadersWidth = 25;
            this.customReturnDGV.RowTemplate.Height = 23;
            this.customReturnDGV.Size = new System.Drawing.Size(752, 378);
            this.customReturnDGV.TabIndex = 0;
            this.customReturnDGV.SelectionChanged += new System.EventHandler(this.customReturnDGV_SelectionChanged);
            // 
            // monthId
            // 
            this.monthId.HeaderText = "Column1";
            this.monthId.Name = "monthId";
            this.monthId.Visible = false;
            // 
            // monthOrgName
            // 
            this.monthOrgName.HeaderText = "代理商";
            this.monthOrgName.Name = "monthOrgName";
            // 
            // monthYearMonth
            // 
            dataGridViewCellStyle36.Format = "yyyy-MM";
            this.monthYearMonth.DefaultCellStyle = dataGridViewCellStyle36;
            this.monthYearMonth.HeaderText = "年月";
            this.monthYearMonth.Name = "monthYearMonth";
            this.monthYearMonth.Width = 60;
            // 
            // rated
            // 
            this.rated.HeaderText = "X>=100%";
            this.rated.Name = "rated";
            this.rated.Width = 70;
            // 
            // ratec
            // 
            this.ratec.HeaderText = "70%<=X<100%";
            this.ratec.Name = "ratec";
            this.ratec.Width = 80;
            // 
            // rateb
            // 
            this.rateb.HeaderText = "50%<=X<70%";
            this.rateb.Name = "rateb";
            this.rateb.Width = 80;
            // 
            // ratea
            // 
            this.ratea.HeaderText = "X<50%";
            this.ratea.Name = "ratea";
            this.ratea.Width = 60;
            // 
            // monthchecked
            // 
            this.monthchecked.HeaderText = "审核";
            this.monthchecked.Name = "monthchecked";
            this.monthchecked.Width = 60;
            // 
            // monthTask
            // 
            dataGridViewCellStyle37.Format = "#.##";
            this.monthTask.DefaultCellStyle = dataGridViewCellStyle37;
            this.monthTask.HeaderText = "月任务";
            this.monthTask.Name = "monthTask";
            // 
            // monthChecker
            // 
            this.monthChecker.HeaderText = "审核人";
            this.monthChecker.Name = "monthChecker";
            // 
            // customId
            // 
            this.customId.HeaderText = "Column1";
            this.customId.Name = "customId";
            this.customId.Visible = false;
            // 
            // beginDate
            // 
            this.beginDate.HeaderText = "起始时间";
            this.beginDate.Name = "beginDate";
            this.beginDate.Width = 80;
            // 
            // endDate
            // 
            this.endDate.HeaderText = "结束时间";
            this.endDate.Name = "endDate";
            this.endDate.Width = 80;
            // 
            // customName
            // 
            this.customName.HeaderText = "名称";
            this.customName.Name = "customName";
            // 
            // defAmount
            // 
            dataGridViewCellStyle38.Format = "#.##";
            this.defAmount.DefaultCellStyle = dataGridViewCellStyle38;
            this.defAmount.HeaderText = "默认任务量";
            this.defAmount.Name = "defAmount";
            // 
            // customRateBase
            // 
            this.customRateBase.HeaderText = "反点比率";
            this.customRateBase.Name = "customRateBase";
            this.customRateBase.Width = 80;
            // 
            // customRateInc
            // 
            this.customRateInc.HeaderText = "比率增量";
            this.customRateInc.Name = "customRateInc";
            this.customRateInc.Width = 80;
            // 
            // customcheck
            // 
            this.customcheck.HeaderText = "审核";
            this.customcheck.Name = "customcheck";
            this.customcheck.Width = 60;
            // 
            // customChecker
            // 
            this.customChecker.HeaderText = "审核人";
            this.customChecker.Name = "customChecker";
            this.customChecker.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.customChecker.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // seasonId
            // 
            this.seasonId.HeaderText = "Column1";
            this.seasonId.Name = "seasonId";
            this.seasonId.Visible = false;
            // 
            // seasonOrgName
            // 
            this.seasonOrgName.HeaderText = "代理商";
            this.seasonOrgName.Name = "seasonOrgName";
            // 
            // seasonYearMonth
            // 
            dataGridViewCellStyle39.Format = "yyyy-MM";
            this.seasonYearMonth.DefaultCellStyle = dataGridViewCellStyle39;
            this.seasonYearMonth.HeaderText = "年月";
            this.seasonYearMonth.Name = "seasonYearMonth";
            this.seasonYearMonth.Width = 60;
            // 
            // seasonRateBase
            // 
            this.seasonRateBase.HeaderText = "返点率";
            this.seasonRateBase.Name = "seasonRateBase";
            this.seasonRateBase.Width = 70;
            // 
            // seasonRateInc
            // 
            this.seasonRateInc.HeaderText = "比率增量";
            this.seasonRateInc.Name = "seasonRateInc";
            this.seasonRateInc.Width = 80;
            // 
            // seasonTaskAmount
            // 
            dataGridViewCellStyle40.Format = "#.##";
            this.seasonTaskAmount.DefaultCellStyle = dataGridViewCellStyle40;
            this.seasonTaskAmount.HeaderText = "季度总任务";
            this.seasonTaskAmount.Name = "seasonTaskAmount";
            // 
            // seasonCheck
            // 
            this.seasonCheck.HeaderText = "审核";
            this.seasonCheck.Name = "seasonCheck";
            this.seasonCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.seasonCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.seasonCheck.Width = 60;
            // 
            // seasonChecker
            // 
            this.seasonChecker.HeaderText = "审核人";
            this.seasonChecker.Name = "seasonChecker";
            // 
            // customCheckBtn
            // 
            this.customCheckBtn.Location = new System.Drawing.Point(635, 18);
            this.customCheckBtn.Name = "customCheckBtn";
            this.customCheckBtn.Size = new System.Drawing.Size(75, 23);
            this.customCheckBtn.TabIndex = 2;
            this.customCheckBtn.Text = "提交";
            this.customCheckBtn.UseVisualStyleBackColor = true;
            this.customCheckBtn.Click += new System.EventHandler(this.customCheckBtn_Click);
            // 
            // ReturnSetCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 471);
            this.Controls.Add(this.tabControl1);
            this.Name = "ReturnSetCheckForm";
            this.Text = "返点设置审核";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.monthReturnDGV)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seasonReturnDGV)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customReturnDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button monthCheckBtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView customReturnDGV;
        private System.Windows.Forms.DataGridView monthReturnDGV;
        private System.Windows.Forms.DataGridView seasonReturnDGV;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button seasonCheckBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthId;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthOrgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthYearMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn rated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratec;
        private System.Windows.Forms.DataGridViewTextBoxColumn rateb;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratea;
        private System.Windows.Forms.DataGridViewCheckBoxColumn monthchecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn monthChecker;
        private System.Windows.Forms.DataGridViewTextBoxColumn customId;
        private System.Windows.Forms.DataGridViewTextBoxColumn beginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn endDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn customName;
        private System.Windows.Forms.DataGridViewTextBoxColumn defAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn customRateBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn customRateInc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn customcheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn customChecker;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonId;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonOrgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonYearMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonRateBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonRateInc;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonTaskAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn seasonCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn seasonChecker;
        private System.Windows.Forms.Button customCheckBtn;
    }
}