namespace Jeelu.SimplusOM.Client
{
    partial class ViewUserForm
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
            this.userDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userId = new System.Windows.Forms.DataGridViewLinkColumn();
            this.userName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ownerOrg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerManager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.communicate = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.typeCombobox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.managerTextBox = new System.Windows.Forms.TextBox();
            this.userCount3LinkLabel = new System.Windows.Forms.LinkLabel();
            this.userCountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.userCount2LinkLabel = new System.Windows.Forms.LinkLabel();
            this.OKBtn = new System.Windows.Forms.Button();
            this.areaComboBox = new System.Windows.Forms.ComboBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.userDGV);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Size = new System.Drawing.Size(697, 335);
            // 
            // userDGV
            // 
            this.userDGV.AllowUserToAddRows = false;
            this.userDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.userArea,
            this.userId,
            this.userName,
            this.ownerOrg,
            this.ownerManager,
            this.isReserve,
            this.communicate});
            this.userDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userDGV.Location = new System.Drawing.Point(0, 77);
            this.userDGV.Name = "userDGV";
            this.userDGV.ReadOnly = true;
            this.userDGV.RowTemplate.Height = 23;
            this.userDGV.Size = new System.Drawing.Size(697, 258);
            this.userDGV.TabIndex = 2;
            this.userDGV.SelectionChanged += new System.EventHandler(this.userDGV_SelectionChanged);
            this.userDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userDGV_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // userArea
            // 
            this.userArea.HeaderText = "地区";
            this.userArea.Name = "userArea";
            this.userArea.ReadOnly = true;
            // 
            // userId
            // 
            this.userId.HeaderText = "广告主ID";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            this.userId.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.userId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // userName
            // 
            this.userName.HeaderText = "广告主名称";
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            this.userName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.userName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ownerOrg
            // 
            this.ownerOrg.HeaderText = "归属代理商";
            this.ownerOrg.Name = "ownerOrg";
            this.ownerOrg.ReadOnly = true;
            this.ownerOrg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ownerOrg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ownerManager
            // 
            this.ownerManager.HeaderText = "所属管理员";
            this.ownerManager.Name = "ownerManager";
            this.ownerManager.ReadOnly = true;
            // 
            // isReserve
            // 
            this.isReserve.HeaderText = "状态";
            this.isReserve.Name = "isReserve";
            this.isReserve.ReadOnly = true;
            this.isReserve.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isReserve.Width = 60;
            // 
            // communicate
            // 
            this.communicate.HeaderText = "沟通记录";
            this.communicate.Name = "communicate";
            this.communicate.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.typeCombobox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.managerTextBox);
            this.panel1.Controls.Add(this.userCount3LinkLabel);
            this.panel1.Controls.Add(this.userCountLinkLabel);
            this.panel1.Controls.Add(this.userCount2LinkLabel);
            this.panel1.Controls.Add(this.OKBtn);
            this.panel1.Controls.Add(this.areaComboBox);
            this.panel1.Controls.Add(this.IDTextBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 77);
            this.panel1.TabIndex = 1;
            // 
            // typeCombobox
            // 
            this.typeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombobox.FormattingEnabled = true;
            this.typeCombobox.Items.AddRange(new object[] {
            "潜在",
            "正式",
            "全部"});
            this.typeCombobox.Location = new System.Drawing.Point(477, 22);
            this.typeCombobox.Name = "typeCombobox";
            this.typeCombobox.Size = new System.Drawing.Size(96, 20);
            this.typeCombobox.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "类型：";
            // 
            // managerTextBox
            // 
            this.managerTextBox.Location = new System.Drawing.Point(362, 20);
            this.managerTextBox.Name = "managerTextBox";
            this.managerTextBox.Size = new System.Drawing.Size(62, 21);
            this.managerTextBox.TabIndex = 5;
            // 
            // userCount3LinkLabel
            // 
            this.userCount3LinkLabel.AutoSize = true;
            this.userCount3LinkLabel.Location = new System.Drawing.Point(427, 54);
            this.userCount3LinkLabel.Name = "userCount3LinkLabel";
            this.userCount3LinkLabel.Size = new System.Drawing.Size(11, 12);
            this.userCount3LinkLabel.TabIndex = 4;
            this.userCount3LinkLabel.TabStop = true;
            this.userCount3LinkLabel.Text = "6";
            this.userCount3LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // userCountLinkLabel
            // 
            this.userCountLinkLabel.AutoSize = true;
            this.userCountLinkLabel.Location = new System.Drawing.Point(103, 54);
            this.userCountLinkLabel.Name = "userCountLinkLabel";
            this.userCountLinkLabel.Size = new System.Drawing.Size(11, 12);
            this.userCountLinkLabel.TabIndex = 4;
            this.userCountLinkLabel.TabStop = true;
            this.userCountLinkLabel.Text = "9";
            this.userCountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // userCount2LinkLabel
            // 
            this.userCount2LinkLabel.AutoSize = true;
            this.userCount2LinkLabel.Location = new System.Drawing.Point(266, 54);
            this.userCount2LinkLabel.Name = "userCount2LinkLabel";
            this.userCount2LinkLabel.Size = new System.Drawing.Size(11, 12);
            this.userCount2LinkLabel.TabIndex = 4;
            this.userCount2LinkLabel.TabStop = true;
            this.userCount2LinkLabel.Text = "3";
            this.userCount2LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(601, 16);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 3;
            this.OKBtn.Text = "查询";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // areaComboBox
            // 
            this.areaComboBox.FormattingEnabled = true;
            this.areaComboBox.Location = new System.Drawing.Point(212, 20);
            this.areaComboBox.Name = "areaComboBox";
            this.areaComboBox.Size = new System.Drawing.Size(72, 20);
            this.areaComboBox.TabIndex = 2;
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(90, 19);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(57, 21);
            this.IDTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "间接数量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "直接归属：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "广告主总数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "负责人：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "所属地区：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "广告主ID：";
            // 
            // ViewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(697, 370);
            this.Name = "ViewUserForm";
            this.TabText = "广告主管理";
            this.Text = "广告主管理";
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView userDGV;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.ComboBox areaComboBox;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel userCount3LinkLabel;
        private System.Windows.Forms.LinkLabel userCountLinkLabel;
        private System.Windows.Forms.LinkLabel userCount2LinkLabel;
        private System.Windows.Forms.TextBox managerTextBox;
        private System.Windows.Forms.ComboBox typeCombobox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn userArea;
        private System.Windows.Forms.DataGridViewLinkColumn userId;
        private System.Windows.Forms.DataGridViewLinkColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerOrg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn isReserve;
        private System.Windows.Forms.DataGridViewLinkColumn communicate;

    }
}