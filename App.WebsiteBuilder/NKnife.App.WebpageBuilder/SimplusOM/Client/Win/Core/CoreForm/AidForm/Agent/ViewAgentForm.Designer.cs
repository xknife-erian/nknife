namespace Jeelu.SimplusOM.Client
{
    partial class ViewAgentForm
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
            this.agentCount3LinkLabel = new System.Windows.Forms.LinkLabel();
            this.agentCount2LinkLabel = new System.Windows.Forms.LinkLabel();
            this.agentCountLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.typeCombobox = new System.Windows.Forms.ComboBox();
            this.areaComboBox = new System.Windows.Forms.ComboBox();
            this.managerTextBox = new System.Windows.Forms.TextBox();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AgentDGV = new System.Windows.Forms.DataGridView();
            this.agentBDS = new System.Windows.Forms.BindingSource(this.components);
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.area = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code = new System.Windows.Forms.DataGridViewLinkColumn();
            this.agentName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.subNum = new System.Windows.Forms.DataGridViewLinkColumn();
            this.manager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isReserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.communicate = new System.Windows.Forms.DataGridViewLinkColumn();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AgentDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.agentBDS)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Size = new System.Drawing.Size(720, 270);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.agentCount3LinkLabel);
            this.panel1.Controls.Add(this.agentCount2LinkLabel);
            this.panel1.Controls.Add(this.agentCountLinkLabel);
            this.panel1.Controls.Add(this.SearchBtn);
            this.panel1.Controls.Add(this.typeCombobox);
            this.panel1.Controls.Add(this.areaComboBox);
            this.panel1.Controls.Add(this.managerTextBox);
            this.panel1.Controls.Add(this.CodeTextBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(720, 77);
            this.panel1.TabIndex = 0;
            // 
            // agentCount3LinkLabel
            // 
            this.agentCount3LinkLabel.AutoSize = true;
            this.agentCount3LinkLabel.Location = new System.Drawing.Point(418, 54);
            this.agentCount3LinkLabel.Name = "agentCount3LinkLabel";
            this.agentCount3LinkLabel.Size = new System.Drawing.Size(23, 12);
            this.agentCount3LinkLabel.TabIndex = 4;
            this.agentCount3LinkLabel.TabStop = true;
            this.agentCount3LinkLabel.Text = "150";
            this.agentCount3LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // agentCount2LinkLabel
            // 
            this.agentCount2LinkLabel.AutoSize = true;
            this.agentCount2LinkLabel.Location = new System.Drawing.Point(264, 54);
            this.agentCount2LinkLabel.Name = "agentCount2LinkLabel";
            this.agentCount2LinkLabel.Size = new System.Drawing.Size(17, 12);
            this.agentCount2LinkLabel.TabIndex = 4;
            this.agentCount2LinkLabel.TabStop = true;
            this.agentCount2LinkLabel.Text = "50";
            this.agentCount2LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // agentCountLinkLabel
            // 
            this.agentCountLinkLabel.AutoSize = true;
            this.agentCountLinkLabel.Location = new System.Drawing.Point(104, 54);
            this.agentCountLinkLabel.Name = "agentCountLinkLabel";
            this.agentCountLinkLabel.Size = new System.Drawing.Size(23, 12);
            this.agentCountLinkLabel.TabIndex = 4;
            this.agentCountLinkLabel.TabStop = true;
            this.agentCountLinkLabel.Text = "200";
            this.agentCountLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(637, 17);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 23);
            this.SearchBtn.TabIndex = 3;
            this.SearchBtn.Text = "查询";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // typeCombobox
            // 
            this.typeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombobox.FormattingEnabled = true;
            this.typeCombobox.Items.AddRange(new object[] {
            "潜在",
            "正式",
            "全部"});
            this.typeCombobox.Location = new System.Drawing.Point(494, 19);
            this.typeCombobox.Name = "typeCombobox";
            this.typeCombobox.Size = new System.Drawing.Size(96, 20);
            this.typeCombobox.TabIndex = 2;
            // 
            // areaComboBox
            // 
            this.areaComboBox.FormattingEnabled = true;
            this.areaComboBox.Location = new System.Drawing.Point(220, 19);
            this.areaComboBox.Name = "areaComboBox";
            this.areaComboBox.Size = new System.Drawing.Size(82, 20);
            this.areaComboBox.TabIndex = 2;
            // 
            // managerTextBox
            // 
            this.managerTextBox.Location = new System.Drawing.Point(367, 18);
            this.managerTextBox.Name = "managerTextBox";
            this.managerTextBox.Size = new System.Drawing.Size(74, 21);
            this.managerTextBox.TabIndex = 1;
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Location = new System.Drawing.Point(90, 19);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(77, 21);
            this.CodeTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "二代数量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "总代数量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "代理商总数：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "负责人：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "地区：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代理商ID：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.AgentDGV);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 77);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 193);
            this.panel2.TabIndex = 1;
            // 
            // AgentDGV
            // 
            this.AgentDGV.AllowUserToAddRows = false;
            this.AgentDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AgentDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.area,
            this.code,
            this.agentName,
            this.subNum,
            this.manager,
            this.isReserve,
            this.communicate});
            this.AgentDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AgentDGV.Location = new System.Drawing.Point(0, 0);
            this.AgentDGV.Name = "AgentDGV";
            this.AgentDGV.ReadOnly = true;
            this.AgentDGV.RowTemplate.Height = 23;
            this.AgentDGV.Size = new System.Drawing.Size(720, 193);
            this.AgentDGV.TabIndex = 0;
            this.AgentDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AgentDGV_CellClick);
            this.AgentDGV.SelectionChanged += new System.EventHandler(this.AgentDGV_SelectionChanged);
            this.AgentDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AgentDGV_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "Column1";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // area
            // 
            this.area.HeaderText = "地区";
            this.area.Name = "area";
            this.area.ReadOnly = true;
            // 
            // code
            // 
            this.code.HeaderText = "代理商ID";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            this.code.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // agentName
            // 
            this.agentName.HeaderText = "代理商名称";
            this.agentName.Name = "agentName";
            this.agentName.ReadOnly = true;
            this.agentName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.agentName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.agentName.Width = 200;
            // 
            // subNum
            // 
            this.subNum.HeaderText = "下级数目";
            this.subNum.Name = "subNum";
            this.subNum.ReadOnly = true;
            this.subNum.Visible = false;
            // 
            // manager
            // 
            this.manager.HeaderText = "负责人";
            this.manager.Name = "manager";
            this.manager.ReadOnly = true;
            this.manager.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.manager.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // isReserve
            // 
            this.isReserve.HeaderText = "状态";
            this.isReserve.Name = "isReserve";
            this.isReserve.ReadOnly = true;
            this.isReserve.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isReserve.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.isReserve.Width = 60;
            // 
            // communicate
            // 
            this.communicate.HeaderText = "沟通记录";
            this.communicate.Name = "communicate";
            this.communicate.ReadOnly = true;
            this.communicate.Text = "沟通记录";
            this.communicate.Width = 80;
            // 
            // ViewAgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(720, 295);
            this.Name = "ViewAgentForm";
            this.TabText = "代理商管理";
            this.Text = "代理商管理";
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AgentDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.agentBDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView AgentDGV;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.ComboBox areaComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource agentBDS;
        private System.Windows.Forms.LinkLabel agentCount3LinkLabel;
        private System.Windows.Forms.LinkLabel agentCount2LinkLabel;
        private System.Windows.Forms.LinkLabel agentCountLinkLabel;
        private System.Windows.Forms.TextBox managerTextBox;
        private System.Windows.Forms.ComboBox typeCombobox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn area;
        private System.Windows.Forms.DataGridViewLinkColumn code;
        private System.Windows.Forms.DataGridViewLinkColumn agentName;
        private System.Windows.Forms.DataGridViewLinkColumn subNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn manager;
        private System.Windows.Forms.DataGridViewTextBoxColumn isReserve;
        private System.Windows.Forms.DataGridViewLinkColumn communicate;
    }
}