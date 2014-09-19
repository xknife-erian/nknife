namespace Jeelu.SimplusOM.Client
{
    partial class LeftTreeView
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
            this.agentBtn = new System.Windows.Forms.Button();
            this.userBtn = new System.Windows.Forms.Button();
            this.jeeluBtn = new System.Windows.Forms.Button();
            this.webUnionBtn = new System.Windows.Forms.Button();
            this.msgBtn = new System.Windows.Forms.Button();
            this.newBtn = new System.Windows.Forms.Button();
            this.managerBtn = new System.Windows.Forms.Button();
            this.selfDefinedRetrunBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lastBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.priorBtn = new System.Windows.Forms.Button();
            this.firstBtn = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.MainPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Controls.Add(this.managerBtn);
            this.MainPanel.Controls.Add(this.webUnionBtn);
            this.MainPanel.Controls.Add(this.jeeluBtn);
            this.MainPanel.Controls.Add(this.newBtn);
            this.MainPanel.Controls.Add(this.selfDefinedRetrunBtn);
            this.MainPanel.Controls.Add(this.userBtn);
            this.MainPanel.Controls.Add(this.msgBtn);
            this.MainPanel.Controls.Add(this.agentBtn);
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Size = new System.Drawing.Size(128, 463);
            // 
            // agentBtn
            // 
            this.agentBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.agentBtn.Location = new System.Drawing.Point(27, 94);
            this.agentBtn.Name = "agentBtn";
            this.agentBtn.Size = new System.Drawing.Size(75, 23);
            this.agentBtn.TabIndex = 0;
            this.agentBtn.Text = "代理商管理";
            this.agentBtn.UseVisualStyleBackColor = true;
            this.agentBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // userBtn
            // 
            this.userBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.userBtn.Location = new System.Drawing.Point(27, 123);
            this.userBtn.Name = "userBtn";
            this.userBtn.Size = new System.Drawing.Size(75, 23);
            this.userBtn.TabIndex = 0;
            this.userBtn.Text = "广告主管理";
            this.userBtn.UseVisualStyleBackColor = true;
            this.userBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // jeeluBtn
            // 
            this.jeeluBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.jeeluBtn.Location = new System.Drawing.Point(27, 36);
            this.jeeluBtn.Name = "jeeluBtn";
            this.jeeluBtn.Size = new System.Drawing.Size(75, 23);
            this.jeeluBtn.TabIndex = 0;
            this.jeeluBtn.Text = "公司管理";
            this.jeeluBtn.UseVisualStyleBackColor = true;
            this.jeeluBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // webUnionBtn
            // 
            this.webUnionBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.webUnionBtn.Location = new System.Drawing.Point(27, 200);
            this.webUnionBtn.Name = "webUnionBtn";
            this.webUnionBtn.Size = new System.Drawing.Size(75, 23);
            this.webUnionBtn.TabIndex = 0;
            this.webUnionBtn.Text = "网盟管理";
            this.webUnionBtn.UseVisualStyleBackColor = true;
            this.webUnionBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // msgBtn
            // 
            this.msgBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.msgBtn.Location = new System.Drawing.Point(27, 258);
            this.msgBtn.Name = "msgBtn";
            this.msgBtn.Size = new System.Drawing.Size(75, 23);
            this.msgBtn.TabIndex = 0;
            this.msgBtn.Text = "消息管理";
            this.msgBtn.UseVisualStyleBackColor = true;
            this.msgBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // newBtn
            // 
            this.newBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.newBtn.Location = new System.Drawing.Point(27, 229);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(75, 23);
            this.newBtn.TabIndex = 0;
            this.newBtn.Text = "新闻管理";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // managerBtn
            // 
            this.managerBtn.Location = new System.Drawing.Point(27, 65);
            this.managerBtn.Name = "managerBtn";
            this.managerBtn.Size = new System.Drawing.Size(75, 23);
            this.managerBtn.TabIndex = 1;
            this.managerBtn.Text = "员工管理";
            this.managerBtn.UseVisualStyleBackColor = true;
            this.managerBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // selfDefinedRetrunBtn
            // 
            this.selfDefinedRetrunBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selfDefinedRetrunBtn.Location = new System.Drawing.Point(27, 152);
            this.selfDefinedRetrunBtn.Name = "selfDefinedRetrunBtn";
            this.selfDefinedRetrunBtn.Size = new System.Drawing.Size(75, 23);
            this.selfDefinedRetrunBtn.TabIndex = 0;
            this.selfDefinedRetrunBtn.Text = "促销管理";
            this.selfDefinedRetrunBtn.UseVisualStyleBackColor = true;
            this.selfDefinedRetrunBtn.Click += new System.EventHandler(this.Button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.lastBtn);
            this.panel1.Controls.Add(this.nextBtn);
            this.panel1.Controls.Add(this.priorBtn);
            this.panel1.Controls.Add(this.firstBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 176);
            this.panel1.TabIndex = 2;
            // 
            // lastBtn
            // 
            this.lastBtn.Location = new System.Drawing.Point(87, 150);
            this.lastBtn.Name = "lastBtn";
            this.lastBtn.Size = new System.Drawing.Size(26, 23);
            this.lastBtn.TabIndex = 0;
            this.lastBtn.Text = ">>";
            this.lastBtn.UseVisualStyleBackColor = true;
            this.lastBtn.Click += new System.EventHandler(this.lastBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(62, 150);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(26, 23);
            this.nextBtn.TabIndex = 0;
            this.nextBtn.Text = ">";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // priorBtn
            // 
            this.priorBtn.Location = new System.Drawing.Point(37, 150);
            this.priorBtn.Name = "priorBtn";
            this.priorBtn.Size = new System.Drawing.Size(26, 23);
            this.priorBtn.TabIndex = 0;
            this.priorBtn.Text = "<";
            this.priorBtn.UseVisualStyleBackColor = true;
            this.priorBtn.Click += new System.EventHandler(this.priorBtn_Click);
            // 
            // firstBtn
            // 
            this.firstBtn.Location = new System.Drawing.Point(12, 150);
            this.firstBtn.Name = "firstBtn";
            this.firstBtn.Size = new System.Drawing.Size(26, 23);
            this.firstBtn.TabIndex = 0;
            this.firstBtn.Text = "<<";
            this.firstBtn.UseVisualStyleBackColor = true;
            this.firstBtn.Click += new System.EventHandler(this.firstBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(122, 141);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // LeftTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(128, 488);
            this.Name = "LeftTreeView";
            this.TabText = "导航";
            this.Text = "导航";
            this.MainPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button agentBtn;
        private System.Windows.Forms.Button webUnionBtn;
        private System.Windows.Forms.Button jeeluBtn;
        private System.Windows.Forms.Button userBtn;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.Button msgBtn;
        private System.Windows.Forms.Button managerBtn;
        private System.Windows.Forms.Button selfDefinedRetrunBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button lastBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Button priorBtn;
        private System.Windows.Forms.Button firstBtn;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}