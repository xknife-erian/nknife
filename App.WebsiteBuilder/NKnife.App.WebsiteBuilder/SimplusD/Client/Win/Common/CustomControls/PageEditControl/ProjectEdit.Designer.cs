using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    partial class ProjectEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.conDoing = new System.Windows.Forms.CheckBox();
            this.conEndTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.conStartTime = new System.Windows.Forms.DateTimePicker();
            this.txtCost = new Jeelu.Win.ValidateTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目阶段:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(164, 21);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "预计完成时间:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "成本预算:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(52, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(159, 132);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // conDoing
            // 
            this.conDoing.AutoSize = true;
            this.conDoing.Location = new System.Drawing.Point(12, 12);
            this.conDoing.Name = "conDoing";
            this.conDoing.Size = new System.Drawing.Size(96, 16);
            this.conDoing.TabIndex = 0;
            this.conDoing.Text = "当前进行阶段";
            this.conDoing.UseVisualStyleBackColor = true;
            // 
            // conEndTime
            // 
            this.conEndTime.Location = new System.Drawing.Point(95, 78);
            this.conEndTime.Name = "conEndTime";
            this.conEndTime.Size = new System.Drawing.Size(164, 21);
            this.conEndTime.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "预计开始时间:";
            // 
            // conStartTime
            // 
            this.conStartTime.Location = new System.Drawing.Point(95, 55);
            this.conStartTime.Name = "conStartTime";
            this.conStartTime.Size = new System.Drawing.Size(164, 21);
            this.conStartTime.TabIndex = 2;
            // 
            // txtCost
            // 
            this.txtCost.Location = new System.Drawing.Point(95, 101);
            this.txtCost.Name = "txtCost";
            this.txtCost.RegexText = "^\\d*(\\.\\d{1,2})?$";
            this.txtCost.RegexTextRuntime = "^\\d*(\\.\\d{0,2})?$";
            this.txtCost.Size = new System.Drawing.Size(147, 21);
            this.txtCost.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "元";
            // 
            // ProjectEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 160);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.conStartTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCost);
            this.Controls.Add(this.conEndTime);
            this.Controls.Add(this.conDoing);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Name = "ProjectEdit";
            this.Text = "ProjectEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox conDoing;
        private System.Windows.Forms.DateTimePicker conEndTime;
        private ValidateTextBox txtCost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker conStartTime;
        private System.Windows.Forms.Label label5;
    }
}