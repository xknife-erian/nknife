using System.Windows.Forms;
namespace Jeelu.Win
{
    partial class InsertPicCodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertPicCodeForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picAlignComboBox = new System.Windows.Forms.ComboBox();
            this.picBorderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.picHspaceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.picVspaceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.heightUnitComboBox = new System.Windows.Forms.ComboBox();
            this.widthUnitComBox = new System.Windows.Forms.ComboBox();
            this.PicHeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PicWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.resizeButton = new System.Windows.Forms.Button();
            this.limitCheckBox = new System.Windows.Forms.CheckBox();
            this.heightCheckBox = new System.Windows.Forms.CheckBox();
            this.widthCheckBox = new System.Windows.Forms.CheckBox();
            this.picPathBtn = new System.Windows.Forms.Button();
            this.replaceWordTextBox = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.picPathTextBox = new System.Windows.Forms.TextBox();
            this.picIDTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pic2PathBtn = new System.Windows.Forms.Button();
            this.picNameTextBox = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.pic2PathTextBox = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBorderNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHspaceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVspaceNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicHeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicWidthNumericUpDown)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(503, 357);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.picPathBtn);
            this.tabPage1.Controls.Add(this.replaceWordTextBox);
            this.tabPage1.Controls.Add(this.Label2);
            this.tabPage1.Controls.Add(this.picPathTextBox);
            this.tabPage1.Controls.Add(this.picIDTextBox);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.Label11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(495, 331);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "通常";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picAlignComboBox);
            this.groupBox2.Controls.Add(this.picBorderNumericUpDown);
            this.groupBox2.Controls.Add(this.picHspaceNumericUpDown);
            this.groupBox2.Controls.Add(this.picVspaceNumericUpDown);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.Label6);
            this.groupBox2.Controls.Add(this.Label5);
            this.groupBox2.Controls.Add(this.Label4);
            this.groupBox2.Controls.Add(this.Label3);
            this.groupBox2.Location = new System.Drawing.Point(50, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 129);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " 位置信息";
            // 
            // picAlignComboBox
            // 
            this.picAlignComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.picAlignComboBox.Items.AddRange(new object[] {
            "left",
            "right",
            "middle"});
            this.picAlignComboBox.Location = new System.Drawing.Point(105, 20);
            this.picAlignComboBox.Name = "picAlignComboBox";
            this.picAlignComboBox.Size = new System.Drawing.Size(113, 21);
            this.picAlignComboBox.TabIndex = 13;
            // 
            // picBorderNumericUpDown
            // 
            this.picBorderNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBorderNumericUpDown.Location = new System.Drawing.Point(106, 93);
            this.picBorderNumericUpDown.Name = "picBorderNumericUpDown";
            this.picBorderNumericUpDown.Size = new System.Drawing.Size(81, 21);
            this.picBorderNumericUpDown.TabIndex = 12;
            // 
            // picHspaceNumericUpDown
            // 
            this.picHspaceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHspaceNumericUpDown.Location = new System.Drawing.Point(105, 44);
            this.picHspaceNumericUpDown.Name = "picHspaceNumericUpDown";
            this.picHspaceNumericUpDown.Size = new System.Drawing.Size(82, 21);
            this.picHspaceNumericUpDown.TabIndex = 11;
            // 
            // picVspaceNumericUpDown
            // 
            this.picVspaceNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVspaceNumericUpDown.Location = new System.Drawing.Point(105, 68);
            this.picVspaceNumericUpDown.Name = "picVspaceNumericUpDown";
            this.picVspaceNumericUpDown.Size = new System.Drawing.Size(84, 21);
            this.picVspaceNumericUpDown.TabIndex = 10;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(195, 99);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 13);
            this.label31.TabIndex = 9;
            this.label31.Text = "像素";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(195, 74);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "像素";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "像素";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(255, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(65, 65);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(40, 96);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(59, 13);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "框线粗细:";
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(40, 72);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 13);
            this.Label5.TabIndex = 1;
            this.Label5.Text = "垂直间距:";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(40, 48);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(59, 13);
            this.Label4.TabIndex = 1;
            this.Label4.Text = "水平间距:";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(64, 25);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(35, 13);
            this.Label3.TabIndex = 1;
            this.Label3.Text = "对齐:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.heightUnitComboBox);
            this.groupBox1.Controls.Add(this.widthUnitComBox);
            this.groupBox1.Controls.Add(this.PicHeightNumericUpDown);
            this.groupBox1.Controls.Add(this.PicWidthNumericUpDown);
            this.groupBox1.Controls.Add(this.resizeButton);
            this.groupBox1.Controls.Add(this.limitCheckBox);
            this.groupBox1.Controls.Add(this.heightCheckBox);
            this.groupBox1.Controls.Add(this.widthCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(47, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 84);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 图像大小";
            // 
            // heightUnitComboBox
            // 
            this.heightUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.heightUnitComboBox.Items.AddRange(new object[] {
            "px",
            "%"});
            this.heightUnitComboBox.Location = new System.Drawing.Point(166, 43);
            this.heightUnitComboBox.Name = "heightUnitComboBox";
            this.heightUnitComboBox.Size = new System.Drawing.Size(52, 21);
            this.heightUnitComboBox.TabIndex = 9;
            // 
            // widthUnitComBox
            // 
            this.widthUnitComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthUnitComBox.Items.AddRange(new object[] {
            "px",
            "%"});
            this.widthUnitComBox.Location = new System.Drawing.Point(166, 18);
            this.widthUnitComBox.Name = "widthUnitComBox";
            this.widthUnitComBox.Size = new System.Drawing.Size(52, 21);
            this.widthUnitComBox.TabIndex = 8;
            // 
            // PicHeightNumericUpDown
            // 
            this.PicHeightNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicHeightNumericUpDown.Location = new System.Drawing.Point(105, 44);
            this.PicHeightNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.PicHeightNumericUpDown.Name = "PicHeightNumericUpDown";
            this.PicHeightNumericUpDown.Size = new System.Drawing.Size(55, 21);
            this.PicHeightNumericUpDown.TabIndex = 3;
            this.PicHeightNumericUpDown.Validated += new System.EventHandler(this.PicHeightNumericUpDown_Validated);
            // 
            // PicWidthNumericUpDown
            // 
            this.PicWidthNumericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicWidthNumericUpDown.Location = new System.Drawing.Point(105, 19);
            this.PicWidthNumericUpDown.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.PicWidthNumericUpDown.Name = "PicWidthNumericUpDown";
            this.PicWidthNumericUpDown.Size = new System.Drawing.Size(55, 21);
            this.PicWidthNumericUpDown.TabIndex = 7;
            this.PicWidthNumericUpDown.Validated += new System.EventHandler(this.PicWidthNumericUpDown_Validated);
            // 
            // resizeButton
            // 
            this.resizeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.resizeButton.Location = new System.Drawing.Point(240, 20);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(107, 21);
            this.resizeButton.TabIndex = 6;
            this.resizeButton.Text = "恢复原始大小";
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            // 
            // limitCheckBox
            // 
            this.limitCheckBox.Enabled = false;
            this.limitCheckBox.Location = new System.Drawing.Point(240, 47);
            this.limitCheckBox.Name = "limitCheckBox";
            this.limitCheckBox.Size = new System.Drawing.Size(77, 18);
            this.limitCheckBox.TabIndex = 5;
            this.limitCheckBox.Text = "限制比例";
            this.limitCheckBox.CheckedChanged += new System.EventHandler(this.scaleCheckBox_CheckedChanged);
            // 
            // heightCheckBox
            // 
            this.heightCheckBox.Location = new System.Drawing.Point(22, 47);
            this.heightCheckBox.Name = "heightCheckBox";
            this.heightCheckBox.Size = new System.Drawing.Size(77, 18);
            this.heightCheckBox.TabIndex = 0;
            this.heightCheckBox.Text = "高度(&H):";
            this.heightCheckBox.CheckedChanged += new System.EventHandler(this.heightCheckBox_CheckedChanged);
            // 
            // widthCheckBox
            // 
            this.widthCheckBox.Location = new System.Drawing.Point(22, 20);
            this.widthCheckBox.Name = "widthCheckBox";
            this.widthCheckBox.Size = new System.Drawing.Size(77, 18);
            this.widthCheckBox.TabIndex = 0;
            this.widthCheckBox.Text = "宽度(&W):";
            this.widthCheckBox.CheckedChanged += new System.EventHandler(this.widthCheckBox_CheckedChanged);
            // 
            // picPathBtn
            // 
            this.picPathBtn.Image = ((System.Drawing.Image)(resources.GetObject("picPathBtn.Image")));
            this.picPathBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.picPathBtn.Location = new System.Drawing.Point(398, 16);
            this.picPathBtn.Name = "picPathBtn";
            this.picPathBtn.Size = new System.Drawing.Size(24, 23);
            this.picPathBtn.TabIndex = 2;
            this.picPathBtn.Click += new System.EventHandler(this.picPathBtn_Click);
            // 
            // replaceWordTextBox
            // 
            this.replaceWordTextBox.Location = new System.Drawing.Point(122, 68);
            this.replaceWordTextBox.Name = "replaceWordTextBox";
            this.replaceWordTextBox.Size = new System.Drawing.Size(300, 21);
            this.replaceWordTextBox.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(47, 72);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(59, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "替代文字:";
            // 
            // picPathTextBox
            // 
            this.picPathTextBox.Location = new System.Drawing.Point(122, 43);
            this.picPathTextBox.Name = "picPathTextBox";
            this.picPathTextBox.Size = new System.Drawing.Size(300, 21);
            this.picPathTextBox.TabIndex = 1;
            // 
            // picIDTextBox
            // 
            this.picIDTextBox.Location = new System.Drawing.Point(122, 17);
            this.picIDTextBox.Name = "picIDTextBox";
            this.picIDTextBox.Size = new System.Drawing.Size(270, 21);
            this.picIDTextBox.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(47, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "图像路径:";
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(47, 20);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(59, 13);
            this.Label11.TabIndex = 0;
            this.Label11.Text = "图像ID:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Label9);
            this.tabPage2.Controls.Add(this.pictureBox2);
            this.tabPage2.Controls.Add(this.pic2PathBtn);
            this.tabPage2.Controls.Add(this.picNameTextBox);
            this.tabPage2.Controls.Add(this.Label7);
            this.tabPage2.Controls.Add(this.pic2PathTextBox);
            this.tabPage2.Controls.Add(this.Label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(495, 367);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "替换";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(148, 69);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(25, 13);
            this.Label9.TabIndex = 9;
            this.Label9.Text = "info";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(122, 69);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pic2PathBtn
            // 
            this.pic2PathBtn.Image = ((System.Drawing.Image)(resources.GetObject("pic2PathBtn.Image")));
            this.pic2PathBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pic2PathBtn.Location = new System.Drawing.Point(393, 16);
            this.pic2PathBtn.Name = "pic2PathBtn";
            this.pic2PathBtn.Size = new System.Drawing.Size(28, 23);
            this.pic2PathBtn.TabIndex = 7;
            this.pic2PathBtn.Click += new System.EventHandler(this.pic2PathBtn_Click);
            // 
            // picNameTextBox
            // 
            this.picNameTextBox.Location = new System.Drawing.Point(121, 42);
            this.picNameTextBox.Name = "picNameTextBox";
            this.picNameTextBox.Size = new System.Drawing.Size(300, 21);
            this.picNameTextBox.TabIndex = 6;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(47, 46);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(59, 13);
            this.Label7.TabIndex = 3;
            this.Label7.Text = "名称属性:";
            // 
            // pic2PathTextBox
            // 
            this.pic2PathTextBox.Location = new System.Drawing.Point(121, 17);
            this.pic2PathTextBox.Name = "pic2PathTextBox";
            this.pic2PathTextBox.Size = new System.Drawing.Size(261, 21);
            this.pic2PathTextBox.TabIndex = 5;
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(47, 20);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(59, 13);
            this.Label8.TabIndex = 4;
            this.Label8.Text = "图像路径:";
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okBtn.Location = new System.Drawing.Point(322, 387);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(82, 24);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "确定";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelBtn.Location = new System.Drawing.Point(413, 387);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(82, 24);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "取消";
            // 
            // InsertPicCodeForm
            // 
            this.AcceptButton = this.okBtn;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(528, 425);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertPicCodeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "图像";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBorderNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHspaceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVspaceNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicHeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicWidthNumericUpDown)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button picPathBtn;
        private TextBox picIDTextBox;
        private Label Label11;
        private TabPage tabPage2;
        private GroupBox groupBox1;
        private CheckBox heightCheckBox;
        private CheckBox widthCheckBox;
        private TextBox replaceWordTextBox;
        private Label Label2;
        private Button resizeButton;
        private CheckBox limitCheckBox;
        private GroupBox groupBox2;
        private PictureBox pictureBox1;
        private Label Label6;
        private Label Label5;
        private Label Label4;
        private Label Label3;
        private Button pic2PathBtn;
        private TextBox picNameTextBox;
        private Label Label7;
        private TextBox pic2PathTextBox;
        private Label Label8;
        private Button okBtn;
        private Button cancelBtn;
        private Label Label9;
        private PictureBox pictureBox2;
        private Label label31;
        private Label label21;
        private Label label1;
        private NumericUpDown PicWidthNumericUpDown;
        private NumericUpDown picHspaceNumericUpDown;
        private NumericUpDown picVspaceNumericUpDown;
        private NumericUpDown PicHeightNumericUpDown;
        private NumericUpDown picBorderNumericUpDown;
        private ComboBox heightUnitComboBox;
        private ComboBox widthUnitComBox;
        private ComboBox picAlignComboBox;
        private TextBox picPathTextBox;
        private Label label10;
    }
}