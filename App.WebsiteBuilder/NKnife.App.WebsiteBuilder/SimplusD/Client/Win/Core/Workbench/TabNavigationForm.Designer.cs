namespace Jeelu.SimplusD.Client.Win
{
    partial class TabNavigationForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewPad = new System.Windows.Forms.ListView();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "活动工具窗口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(201, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "活动文件";
            // 
            // listViewPad
            // 
            this.listViewPad.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewPad.BackColor = System.Drawing.SystemColors.Control;
            this.listViewPad.Location = new System.Drawing.Point(11, 26);
            this.listViewPad.MultiSelect = false;
            this.listViewPad.Name = "listViewPad";
            this.listViewPad.Size = new System.Drawing.Size(181, 325);
            this.listViewPad.TabIndex = 1;
            this.listViewPad.UseCompatibleStateImageBehavior = false;
            this.listViewPad.View = System.Windows.Forms.View.List;
            this.listViewPad.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewPad_MouseClick);
            // 
            // listViewFile
            // 
            this.listViewFile.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewFile.BackColor = System.Drawing.SystemColors.Control;
            this.listViewFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewFile.Location = new System.Drawing.Point(202, 26);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(361, 325);
            this.listViewFile.TabIndex = 0;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.List;
            this.listViewFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewFile_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.listViewPad);
            this.panel1.Controls.Add(this.listViewFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 364);
            this.panel1.TabIndex = 2;
            // 
            // TabNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 364);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TabNavigationForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TabNavigationForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewPad;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.Panel panel1;
    }
}