
namespace NKnife.Win.Forms.Kit.Forms
{
    partial class CustomStripControlTestDockView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomStripControlTestDockView));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "Window",
            "View Sub Item",
            "宝塔山是革命圣地延安的重要标志和象征"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "Framework ",
            "For a full description",
            "Unit of Work是用来解决领域模型存储和变更工作"}, -1);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._Checkbox = new NKnife.Win.Forms.ToolStripCheckBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._DateTimePicker = new NKnife.Win.Forms.ToolStripDateTimePicker();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.multipleImageToolStrip1 = new NKnife.Win.Forms.MultipleImageToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._FontListView = new System.Windows.Forms.ListView();
            this.h1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.h2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.h3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.h4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.multipleImageToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._Checkbox,
            this.toolStripSeparator1,
            this._DateTimePicker,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(10, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(606, 26);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _Checkbox
            // 
            this._Checkbox.Name = "_Checkbox";
            this._Checkbox.Size = new System.Drawing.Size(111, 23);
            this._Checkbox.Text = "工具栏上的选择";
            this._Checkbox.ToolStripCheckBoxEnabled = true;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // _DateTimePicker
            // 
            this._DateTimePicker.Name = "_DateTimePicker";
            this._DateTimePicker.Size = new System.Drawing.Size(130, 23);
            this._DateTimePicker.Text = "2017年3月7日";
            this._DateTimePicker.ToolStripDateTimePickerEnabled = true;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 63);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(135, 21);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "与工具栏的选择互动";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(142, 90);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 23);
            this.textBox1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(32, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(626, 136);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工具条自定义控件测试";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "与工具栏的日期互动:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.multipleImageToolStrip1);
            this.groupBox2.Location = new System.Drawing.Point(32, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 85);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MultipleImageToolStrip";
            // 
            // multipleImageToolStrip1
            // 
            this.multipleImageToolStrip1.DefaultImageProvider = this.multipleImageToolStrip1;
            this.multipleImageToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.multipleImageToolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.multipleImageToolStrip1.ImageSize = NKnife.Win.Forms.ImageSize.ExtraLarge;
            this.multipleImageToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.multipleImageToolStrip1.Location = new System.Drawing.Point(3, 19);
            this.multipleImageToolStrip1.Name = "multipleImageToolStrip1";
            this.multipleImageToolStrip1.Size = new System.Drawing.Size(620, 55);
            this.multipleImageToolStrip1.TabIndex = 0;
            this.multipleImageToolStrip1.Text = "multipleImageToolStrip1";
            this.multipleImageToolStrip1.UseUnknownImageSizeIcon = true;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // _FontListView
            // 
            this._FontListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.h1,
            this.h2,
            this.h3,
            this.h4});
            this._FontListView.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._FontListView.FullRowSelect = true;
            this._FontListView.GridLines = true;
            this._FontListView.HideSelection = false;
            this._FontListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this._FontListView.Location = new System.Drawing.Point(32, 268);
            this._FontListView.Name = "_FontListView";
            this._FontListView.Size = new System.Drawing.Size(626, 98);
            this._FontListView.TabIndex = 5;
            this._FontListView.UseCompatibleStateImageBehavior = false;
            this._FontListView.View = System.Windows.Forms.View.Details;
            // 
            // h1
            // 
            this.h1.Text = "编号";
            // 
            // h2
            // 
            this.h2.Width = 100;
            // 
            // h3
            // 
            this.h3.Width = 120;
            // 
            // h4
            // 
            this.h4.Text = "汉字";
            this.h4.Width = 300;
            // 
            // CustomStripControlTestDockView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(692, 456);
            this.Controls.Add(this._FontListView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CustomStripControlTestDockView";
            this.Text = "CustomStripControlTestDockView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.multipleImageToolStrip1.ResumeLayout(false);
            this.multipleImageToolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private ToolStripCheckBox _Checkbox;
        private ToolStripDateTimePicker _DateTimePicker;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox groupBox2;
        private MultipleImageToolStrip multipleImageToolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ListView _FontListView;
        private System.Windows.Forms.ColumnHeader h1;
        private System.Windows.Forms.ColumnHeader h2;
        private System.Windows.Forms.ColumnHeader h3;
        private System.Windows.Forms.ColumnHeader h4;
    }
}