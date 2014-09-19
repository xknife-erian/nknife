using System.Windows.Forms;
namespace Jeelu.SimplusD.Client.Win
{
    partial class frmInsertTableCode
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("无", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("左", 2);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("顶部", 1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("两者", 3);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertTableCode));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cellspacingTextBox = new Jeelu.Win.ValidateTextBox();
            this.widthTextBox = new Jeelu.Win.ValidateTextBox();
            this.cellpaddingTextBox = new Jeelu.Win.ValidateTextBox();
            this.borderWidthTextBox = new Jeelu.Win.ValidateTextBox();
            this.colTextBox = new Jeelu.Win.ValidateTextBox();
            this.rowTextBox = new Jeelu.Win.ValidateTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.widthUintComBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.headerListView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.alignComBox = new System.Windows.Forms.ComboBox();
            this.summaryTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(23, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 表格大小";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.cellspacingTextBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.widthTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cellpaddingTextBox, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.borderWidthTextBox, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.colTextBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.rowTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.Label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Label3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.Label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.widthUintComBox, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(15, 20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(310, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cellspacingTextBox
            // 
            this.cellspacingTextBox.Location = new System.Drawing.Point(96, 78);
            this.cellspacingTextBox.Name = "cellspacingTextBox";
            this.cellspacingTextBox.RegexText = "";
            this.cellspacingTextBox.RegexTextRuntime = "";
            this.cellspacingTextBox.Size = new System.Drawing.Size(56, 21);
            this.cellspacingTextBox.TabIndex = 5;
            this.cellspacingTextBox.Text = "1";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(96, 28);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.RegexText = "";
            this.widthTextBox.RegexTextRuntime = "";
            this.widthTextBox.Size = new System.Drawing.Size(56, 21);
            this.widthTextBox.TabIndex = 5;
            this.widthTextBox.Text = "300";
            // 
            // cellpaddingTextBox
            // 
            this.cellpaddingTextBox.Location = new System.Drawing.Point(235, 78);
            this.cellpaddingTextBox.Name = "cellpaddingTextBox";
            this.cellpaddingTextBox.RegexText = "";
            this.cellpaddingTextBox.RegexTextRuntime = "";
            this.cellpaddingTextBox.Size = new System.Drawing.Size(56, 21);
            this.cellpaddingTextBox.TabIndex = 5;
            this.cellpaddingTextBox.Text = "1";
            // 
            // borderWidthTextBox
            // 
            this.borderWidthTextBox.Location = new System.Drawing.Point(96, 53);
            this.borderWidthTextBox.Name = "borderWidthTextBox";
            this.borderWidthTextBox.RegexText = "";
            this.borderWidthTextBox.RegexTextRuntime = "";
            this.borderWidthTextBox.Size = new System.Drawing.Size(56, 21);
            this.borderWidthTextBox.TabIndex = 5;
            this.borderWidthTextBox.Text = "1";
            // 
            // colTextBox
            // 
            this.colTextBox.Location = new System.Drawing.Point(235, 3);
            this.colTextBox.Name = "colTextBox";
            this.colTextBox.RegexText = "";
            this.colTextBox.RegexTextRuntime = "";
            this.colTextBox.Size = new System.Drawing.Size(56, 21);
            this.colTextBox.TabIndex = 5;
            this.colTextBox.Text = "3";
            // 
            // rowTextBox
            // 
            this.rowTextBox.Location = new System.Drawing.Point(96, 3);
            this.rowTextBox.Name = "rowTextBox";
            this.rowTextBox.RegexText = "^[0-9]*$";
            this.rowTextBox.RegexTextRuntime = "^[0-9]*$";
            this.rowTextBox.Size = new System.Drawing.Size(56, 21);
            this.rowTextBox.TabIndex = 5;
            this.rowTextBox.Text = "3";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "单元格间距:";
            // 
            // Label1
            // 
            this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(55, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(35, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "行数:";
            // 
            // Label2
            // 
            this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(194, 6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(35, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "列数:";
            // 
            // Label3
            // 
            this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(31, 31);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(59, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "表格宽度:";
            // 
            // Label6
            // 
            this.Label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(19, 81);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(71, 13);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "单元格边距:";
            // 
            // Label4
            // 
            this.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(31, 56);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(59, 13);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "边框粗细:";
            // 
            // widthUintComBox
            // 
            this.widthUintComBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.widthUintComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.widthUintComBox.Location = new System.Drawing.Point(158, 28);
            this.widthUintComBox.Name = "widthUintComBox";
            this.widthUintComBox.Size = new System.Drawing.Size(59, 21);
            this.widthUintComBox.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(158, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "(像素)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.headerListView);
            this.groupBox2.Location = new System.Drawing.Point(23, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " 页眉";
            // 
            // headerListView
            // 
            this.headerListView.BackColor = System.Drawing.SystemColors.InactiveBorder;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.headerListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.headerListView.LargeImageList = this.imageList;
            this.headerListView.Location = new System.Drawing.Point(15, 20);
            this.headerListView.Name = "headerListView";
            this.headerListView.Size = new System.Drawing.Size(325, 64);
            this.headerListView.TabIndex = 0;
            this.headerListView.UseCompatibleStateImageBehavior = false;
            this.headerListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.headerListView_ItemSelectionChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "1.png");
            this.imageList.Images.SetKeyName(1, "2.png");
            this.imageList.Images.SetKeyName(2, "3.png");
            this.imageList.Images.SetKeyName(3, "4.png");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.alignComBox);
            this.groupBox3.Controls.Add(this.summaryTextBox);
            this.groupBox3.Controls.Add(this.titleTextBox);
            this.groupBox3.Controls.Add(this.Label7);
            this.groupBox3.Controls.Add(this.Label9);
            this.groupBox3.Controls.Add(this.Label8);
            this.groupBox3.Location = new System.Drawing.Point(23, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(356, 137);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " 铺助功能";
            // 
            // alignComBox
            // 
            this.alignComBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.alignComBox.Location = new System.Drawing.Point(70, 46);
            this.alignComBox.Name = "alignComBox";
            this.alignComBox.Size = new System.Drawing.Size(72, 21);
            this.alignComBox.TabIndex = 4;
            // 
            // summaryTextBox
            // 
            this.summaryTextBox.Location = new System.Drawing.Point(70, 72);
            this.summaryTextBox.Multiline = true;
            this.summaryTextBox.Name = "summaryTextBox";
            this.summaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.summaryTextBox.Size = new System.Drawing.Size(255, 49);
            this.summaryTextBox.TabIndex = 1;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(70, 20);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(255, 21);
            this.titleTextBox.TabIndex = 1;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(12, 23);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(35, 13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "标题:";
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(12, 72);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(35, 13);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "摘要:";
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(12, 49);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(59, 13);
            this.Label8.TabIndex = 2;
            this.Label8.Text = "对齐标题:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelBtn.Location = new System.Drawing.Point(299, 389);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(80, 25);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "取消";
            // 
            // okBtn
            // 
            this.okBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.okBtn.Location = new System.Drawing.Point(213, 389);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(80, 25);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "确定";
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // frmInsertTableCode
            // 
            this.AcceptButton = this.okBtn;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(402, 439);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInsertTableCode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表格";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmInsertTableCode_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInsertTableCode_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button cancelBtn;
        private Button okBtn;
        private Label Label2;
        private Label Label1;
        private Label Label6;
        private Label Label4;
        private Label Label3;
        private ListView headerListView;
        private ImageList imageList;
        private TextBox summaryTextBox;
        private TextBox titleTextBox;
        private Label Label7;
        private Label Label9;
        private Label Label8;
        private ComboBox widthUintComBox;
        private ComboBox alignComBox;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label5;
        private Label label10;
        private Jeelu.Win.ValidateTextBox rowTextBox;
        private Jeelu.Win.ValidateTextBox widthTextBox;
        private Jeelu.Win.ValidateTextBox borderWidthTextBox;
        private Jeelu.Win.ValidateTextBox colTextBox;
        private Jeelu.Win.ValidateTextBox cellspacingTextBox;
        private Jeelu.Win.ValidateTextBox cellpaddingTextBox;
    }
}