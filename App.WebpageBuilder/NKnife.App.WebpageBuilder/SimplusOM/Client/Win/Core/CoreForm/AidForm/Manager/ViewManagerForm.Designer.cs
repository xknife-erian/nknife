namespace Jeelu.SimplusOM.Client
{
    partial class ViewManagerForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.managerDGV = new System.Windows.Forms.DataGridView();
            this.managerBDS = new System.Windows.Forms.BindingSource(this.components);
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewLinkColumn();
            this.area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rights = new System.Windows.Forms.DataGridViewLinkColumn();
            this.log = new System.Windows.Forms.DataGridViewLinkColumn();
            this.remind = new System.Windows.Forms.DataGridViewLinkColumn();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.managerDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBDS)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Size = new System.Drawing.Size(651, 283);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SearchBtn);
            this.panel1.Controls.Add(this.NameTextBox);
            this.panel1.Controls.Add(this.IDTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 72);
            this.panel1.TabIndex = 0;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(487, 23);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 2;
            this.SearchBtn.Text = "查询";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(224, 26);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 21);
            this.NameTextBox.TabIndex = 1;
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(60, 26);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(100, 21);
            this.IDTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.managerDGV);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(651, 211);
            this.panel2.TabIndex = 1;
            // 
            // managerDGV
            // 
            this.managerDGV.AllowUserToAddRows = false;
            this.managerDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.managerDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.code,
            this.name,
            this.area,
            this.rights,
            this.log,
            this.remind});
            this.managerDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managerDGV.Location = new System.Drawing.Point(0, 0);
            this.managerDGV.Name = "managerDGV";
            this.managerDGV.RowTemplate.Height = 23;
            this.managerDGV.Size = new System.Drawing.Size(651, 211);
            this.managerDGV.TabIndex = 0;
            this.managerDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.managerDGV_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // code
            // 
            this.code.HeaderText = "ID";
            this.code.Name = "code";
            // 
            // name
            // 
            this.name.HeaderText = "姓名";
            this.name.Name = "name";
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // area
            // 
            this.area.HeaderText = "管理地区";
            this.area.Name = "area";
            this.area.Visible = false;
            // 
            // rights
            // 
            this.rights.HeaderText = "查看权限";
            this.rights.Name = "rights";
            // 
            // log
            // 
            this.log.HeaderText = "操作日志";
            this.log.Name = "log";
            this.log.Visible = false;
            // 
            // remind
            // 
            this.remind.HeaderText = "提醒记录";
            this.remind.Name = "remind";
            // 
            // ViewManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(651, 318);
            this.Name = "ViewManagerForm";
            this.TabText = "员工管理";
            this.Text = "员工管理";
            this.Load += new System.EventHandler(this.ViewManagerForm_Load);
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.managerDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managerBDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.DataGridView managerDGV;
        private System.Windows.Forms.BindingSource managerBDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewLinkColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn area;
        private System.Windows.Forms.DataGridViewLinkColumn rights;
        private System.Windows.Forms.DataGridViewLinkColumn log;
        private System.Windows.Forms.DataGridViewLinkColumn remind;
    }
}