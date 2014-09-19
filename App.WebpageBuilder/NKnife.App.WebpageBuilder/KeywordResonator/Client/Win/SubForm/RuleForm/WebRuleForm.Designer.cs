namespace Jeelu.KeywordResonator.Client
{
    partial class WebRuleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebRuleForm));
            this.OkBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.DomainTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RuleComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ModifyTextRuleBtn = new System.Windows.Forms.Button();
            this.NewTextRuleBtn = new System.Windows.Forms.Button();
            this.DelTextRuleBtn = new System.Windows.Forms.Button();
            this.TextRuleNameComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._ParamPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BuildRuleBtn = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ruleNameTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.DeleteRuleBtn = new System.Windows.Forms.ToolStripButton();
            this.ModifiedRuleBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveRuleBtn = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(562, 516);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(102, 25);
            this.OkBtn.TabIndex = 1;
            this.OkBtn.Text = "确定(&O)";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(670, 516);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(102, 25);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "取消(&C)";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(20, 19);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DomainTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(752, 490);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 3;
            // 
            // DomainTreeView
            // 
            this.DomainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DomainTreeView.HideSelection = false;
            this.DomainTreeView.Location = new System.Drawing.Point(0, 25);
            this.DomainTreeView.Name = "DomainTreeView";
            this.DomainTreeView.Size = new System.Drawing.Size(250, 465);
            this.DomainTreeView.TabIndex = 1;
            this.DomainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DomainTreeView_AfterSelect);
            this.DomainTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.DomainTreeView_BeforeSelect);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(250, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "0-9"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(75, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(28, 22);
            this.toolStripButton1.Text = "ALL";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "增";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "改";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "删";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer2.Size = new System.Drawing.Size(498, 490);
            this.splitContainer2.SplitterDistance = 41;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RuleComboBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已定义规则列表";
            // 
            // RuleComboBox
            // 
            this.RuleComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RuleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RuleComboBox.FormattingEnabled = true;
            this.RuleComboBox.Location = new System.Drawing.Point(3, 17);
            this.RuleComboBox.Name = "RuleComboBox";
            this.RuleComboBox.Size = new System.Drawing.Size(492, 21);
            this.RuleComboBox.TabIndex = 0;
            this.RuleComboBox.SelectedIndexChanged += new System.EventHandler(this.RuleComboBox_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 345);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(498, 105);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "规则说明 (设计者备忘)";
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(3, 17);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(492, 85);
            this.textBox3.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ModifyTextRuleBtn);
            this.groupBox6.Controls.Add(this.NewTextRuleBtn);
            this.groupBox6.Controls.Add(this.DelTextRuleBtn);
            this.groupBox6.Controls.Add(this.TextRuleNameComboBox);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(0, 287);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(498, 58);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "正文抽取规则";
            // 
            // ModifyTextRuleBtn
            // 
            this.ModifyTextRuleBtn.Location = new System.Drawing.Point(311, 19);
            this.ModifyTextRuleBtn.Name = "ModifyTextRuleBtn";
            this.ModifyTextRuleBtn.Size = new System.Drawing.Size(60, 22);
            this.ModifyTextRuleBtn.TabIndex = 4;
            this.ModifyTextRuleBtn.Text = "修改";
            this.ModifyTextRuleBtn.UseVisualStyleBackColor = true;
            this.ModifyTextRuleBtn.Click += new System.EventHandler(this.ModifyTextRuleBtn_Click);
            // 
            // NewTextRuleBtn
            // 
            this.NewTextRuleBtn.Location = new System.Drawing.Point(245, 19);
            this.NewTextRuleBtn.Name = "NewTextRuleBtn";
            this.NewTextRuleBtn.Size = new System.Drawing.Size(60, 22);
            this.NewTextRuleBtn.TabIndex = 3;
            this.NewTextRuleBtn.Text = "新建";
            this.NewTextRuleBtn.UseVisualStyleBackColor = true;
            this.NewTextRuleBtn.Click += new System.EventHandler(this.NewTextRuleBtn_Click);
            // 
            // DelTextRuleBtn
            // 
            this.DelTextRuleBtn.Location = new System.Drawing.Point(377, 19);
            this.DelTextRuleBtn.Name = "DelTextRuleBtn";
            this.DelTextRuleBtn.Size = new System.Drawing.Size(60, 22);
            this.DelTextRuleBtn.TabIndex = 2;
            this.DelTextRuleBtn.Text = "删除";
            this.DelTextRuleBtn.UseVisualStyleBackColor = true;
            this.DelTextRuleBtn.Click += new System.EventHandler(this.DelTextRuleBtn_Click);
            // 
            // TextRuleNameComboBox
            // 
            this.TextRuleNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TextRuleNameComboBox.FormattingEnabled = true;
            this.TextRuleNameComboBox.Location = new System.Drawing.Point(3, 19);
            this.TextRuleNameComboBox.Name = "TextRuleNameComboBox";
            this.TextRuleNameComboBox.Size = new System.Drawing.Size(228, 21);
            this.TextRuleNameComboBox.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this._ParamPanel);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 151);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(498, 136);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数列表(动态生成)";
            // 
            // _ParamPanel
            // 
            this._ParamPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParamPanel.Location = new System.Drawing.Point(3, 17);
            this._ParamPanel.Name = "_ParamPanel";
            this._ParamPanel.Size = new System.Drawing.Size(492, 116);
            this._ParamPanel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BuildRuleBtn);
            this.groupBox3.Controls.Add(this.urlTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(498, 84);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "规则名称示例URL";
            // 
            // BuildRuleBtn
            // 
            this.BuildRuleBtn.Location = new System.Drawing.Point(400, 58);
            this.BuildRuleBtn.Name = "BuildRuleBtn";
            this.BuildRuleBtn.Size = new System.Drawing.Size(94, 22);
            this.BuildRuleBtn.TabIndex = 1;
            this.BuildRuleBtn.Text = "生成规则";
            this.BuildRuleBtn.UseVisualStyleBackColor = true;
            this.BuildRuleBtn.Click += new System.EventHandler(this.BuildRuleBtn_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.urlTextBox.Location = new System.Drawing.Point(3, 17);
            this.urlTextBox.Multiline = true;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.urlTextBox.Size = new System.Drawing.Size(492, 37);
            this.urlTextBox.TabIndex = 0;
            this.urlTextBox.TextChanged += new System.EventHandler(this.urlTextBox_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ruleNameTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 42);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "规则名称";
            // 
            // ruleNameTextBox
            // 
            this.ruleNameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ruleNameTextBox.Location = new System.Drawing.Point(3, 17);
            this.ruleNameTextBox.Name = "ruleNameTextBox";
            this.ruleNameTextBox.ReadOnly = true;
            this.ruleNameTextBox.Size = new System.Drawing.Size(492, 21);
            this.ruleNameTextBox.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteRuleBtn,
            this.ModifiedRuleBtn,
            this.toolStripSeparator1,
            this.SaveRuleBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(498, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // DeleteRuleBtn
            // 
            this.DeleteRuleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteRuleBtn.Image = ((System.Drawing.Image)(resources.GetObject("DeleteRuleBtn.Image")));
            this.DeleteRuleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteRuleBtn.Name = "DeleteRuleBtn";
            this.DeleteRuleBtn.Size = new System.Drawing.Size(59, 22);
            this.DeleteRuleBtn.Text = "删除规则";
            this.DeleteRuleBtn.Click += new System.EventHandler(this.DeleteRuleBtn_Click);
            // 
            // ModifiedRuleBtn
            // 
            this.ModifiedRuleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ModifiedRuleBtn.Image = ((System.Drawing.Image)(resources.GetObject("ModifiedRuleBtn.Image")));
            this.ModifiedRuleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ModifiedRuleBtn.Name = "ModifiedRuleBtn";
            this.ModifiedRuleBtn.Size = new System.Drawing.Size(59, 22);
            this.ModifiedRuleBtn.Text = "修改规则";
            this.ModifiedRuleBtn.Click += new System.EventHandler(this.ModifiedRuleBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveRuleBtn
            // 
            this.SaveRuleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveRuleBtn.Name = "SaveRuleBtn";
            this.SaveRuleBtn.Size = new System.Drawing.Size(59, 22);
            this.SaveRuleBtn.Text = "保存规则";
            this.SaveRuleBtn.Click += new System.EventHandler(this.SaveRuleBtn_Click);
            // 
            // WebRuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 565);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OkBtn);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "WebRuleForm";
            this.Padding = new System.Windows.Forms.Padding(20, 19, 20, 19);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网站规则管理器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView DomainTreeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox RuleComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ruleNameTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton DeleteRuleBtn;
        private System.Windows.Forms.ToolStripButton ModifiedRuleBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BuildRuleBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton SaveRuleBtn;
        private System.Windows.Forms.FlowLayoutPanel _ParamPanel;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button ModifyTextRuleBtn;
        private System.Windows.Forms.Button NewTextRuleBtn;
        private System.Windows.Forms.Button DelTextRuleBtn;
        private System.Windows.Forms.ComboBox TextRuleNameComboBox;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;


    }
}