namespace Jeelu.SimplusD.Client.Win
{
    partial class SelectChannelShowForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TreePanel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.conShowChildNode = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.conProject = new System.Windows.Forms.CheckBox();
            this.conHr = new System.Windows.Forms.CheckBox();
            this.conInviteBidding = new System.Windows.Forms.CheckBox();
            this.conKnowledge = new System.Windows.Forms.CheckBox();
            this.conProduct = new System.Windows.Forms.CheckBox();
            this.conGeneral = new System.Windows.Forms.CheckBox();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CannelBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TreePanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(456, 419);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 0;
            // 
            // TreePanel
            // 
            this.TreePanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TreePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreePanel.Location = new System.Drawing.Point(0, 0);
            this.TreePanel.Name = "TreePanel";
            this.TreePanel.Size = new System.Drawing.Size(202, 419);
            this.TreePanel.TabIndex = 1;
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
            this.splitContainer2.Panel1.Controls.Add(this.conShowChildNode);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(250, 419);
            this.splitContainer2.SplitterDistance = 173;
            this.splitContainer2.TabIndex = 0;
            // 
            // conShowChildNode
            // 
            this.conShowChildNode.AutoSize = true;
            this.conShowChildNode.Location = new System.Drawing.Point(7, 17);
            this.conShowChildNode.Name = "conShowChildNode";
            this.conShowChildNode.Size = new System.Drawing.Size(78, 16);
            this.conShowChildNode.TabIndex = 1;
            this.conShowChildNode.Text = "checkBox1";
            this.conShowChildNode.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.conProject);
            this.groupBox1.Controls.Add(this.conHr);
            this.groupBox1.Controls.Add(this.conInviteBidding);
            this.groupBox1.Controls.Add(this.conKnowledge);
            this.groupBox1.Controls.Add(this.conProduct);
            this.groupBox1.Controls.Add(this.conGeneral);
            this.groupBox1.Location = new System.Drawing.Point(4, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择显示页面";
            // 
            // conProject
            // 
            this.conProject.AutoSize = true;
            this.conProject.Location = new System.Drawing.Point(15, 64);
            this.conProject.Name = "conProject";
            this.conProject.Size = new System.Drawing.Size(72, 16);
            this.conProject.TabIndex = 5;
            this.conProject.Text = "项目页面";
            this.conProject.UseVisualStyleBackColor = true;
            // 
            // conHr
            // 
            this.conHr.AutoSize = true;
            this.conHr.Location = new System.Drawing.Point(132, 64);
            this.conHr.Name = "conHr";
            this.conHr.Size = new System.Drawing.Size(72, 16);
            this.conHr.TabIndex = 4;
            this.conHr.Text = "招聘页面";
            this.conHr.UseVisualStyleBackColor = true;
            // 
            // conInviteBidding
            // 
            this.conInviteBidding.AutoSize = true;
            this.conInviteBidding.Location = new System.Drawing.Point(15, 42);
            this.conInviteBidding.Name = "conInviteBidding";
            this.conInviteBidding.Size = new System.Drawing.Size(72, 16);
            this.conInviteBidding.TabIndex = 3;
            this.conInviteBidding.Text = "招标页面";
            this.conInviteBidding.UseVisualStyleBackColor = true;
            // 
            // conKnowledge
            // 
            this.conKnowledge.AutoSize = true;
            this.conKnowledge.Location = new System.Drawing.Point(132, 20);
            this.conKnowledge.Name = "conKnowledge";
            this.conKnowledge.Size = new System.Drawing.Size(72, 16);
            this.conKnowledge.TabIndex = 2;
            this.conKnowledge.Text = "知识页面";
            this.conKnowledge.UseVisualStyleBackColor = true;
            // 
            // conProduct
            // 
            this.conProduct.AutoSize = true;
            this.conProduct.Location = new System.Drawing.Point(132, 42);
            this.conProduct.Name = "conProduct";
            this.conProduct.Size = new System.Drawing.Size(72, 16);
            this.conProduct.TabIndex = 1;
            this.conProduct.Text = "商品页面";
            this.conProduct.UseVisualStyleBackColor = true;
            // 
            // conGeneral
            // 
            this.conGeneral.AutoSize = true;
            this.conGeneral.Location = new System.Drawing.Point(15, 20);
            this.conGeneral.Name = "conGeneral";
            this.conGeneral.Size = new System.Drawing.Size(72, 16);
            this.conGeneral.TabIndex = 0;
            this.conGeneral.Text = "普通页面";
            this.conGeneral.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.CannelBtn);
            this.splitContainer3.Panel2.Controls.Add(this.OKBtn);
            this.splitContainer3.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel2_Paint);
            this.splitContainer3.Size = new System.Drawing.Size(250, 242);
            this.splitContainer3.SplitterDistance = 196;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 182);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "注释";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(23, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "的结果为显示所有选中页面";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(25, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "为不显示子级的页面";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(23, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "包括所有子频道的页面信息”筛选结果";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(9, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "3.上面对应的两种方情况在不选择“显示";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(23, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "子频道的页面信息\"的同时选中页面筛选";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(9, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "2.在选中网站频道并选中\"显示包括所有";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(23, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "显示选中频道下的所有选中页面";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "1.在选择频道的同时选中页面筛选结果为";
            // 
            // CannelBtn
            // 
            this.CannelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CannelBtn.Location = new System.Drawing.Point(132, 7);
            this.CannelBtn.Name = "CannelBtn";
            this.CannelBtn.Size = new System.Drawing.Size(57, 22);
            this.CannelBtn.TabIndex = 3;
            this.CannelBtn.Text = "取消";
            this.CannelBtn.UseVisualStyleBackColor = true;
            this.CannelBtn.Click += new System.EventHandler(this.CannelBtn_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(65, 7);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(61, 22);
            this.OKBtn.TabIndex = 2;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // SelectChannelShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 419);
            this.Controls.Add(this.splitContainer1);
            this.Name = "SelectChannelShowForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "选择模板";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel TreePanel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox conProject;
        private System.Windows.Forms.CheckBox conHr;
        private System.Windows.Forms.CheckBox conInviteBidding;
        private System.Windows.Forms.CheckBox conKnowledge;
        private System.Windows.Forms.CheckBox conProduct;
        private System.Windows.Forms.CheckBox conGeneral;
        private System.Windows.Forms.CheckBox conShowChildNode;
        private System.Windows.Forms.Button CannelBtn;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;

    }
}