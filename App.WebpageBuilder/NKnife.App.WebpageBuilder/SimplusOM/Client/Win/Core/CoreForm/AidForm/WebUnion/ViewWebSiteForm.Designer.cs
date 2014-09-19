namespace Jeelu.SimplusOM.Client
{
    partial class ViewWebSiteForm
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.searchBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nopassLabel = new System.Windows.Forms.Label();
            this.checkLabel = new System.Windows.Forms.Label();
            this.nocheckLabel = new System.Windows.Forms.Label();
            this.siteCountLab = new System.Windows.Forms.Label();
            this.CountLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nopassLab = new System.Windows.Forms.Label();
            this.NoCheckLab = new System.Windows.Forms.Label();
            this.checkLab = new System.Windows.Forms.Label();
            this.siteCountLabel = new System.Windows.Forms.Label();
            this.webSiteDGV = new System.Windows.Forms.DataGridView();
            this.siteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sitename = new System.Windows.Forms.DataGridViewLinkColumn();
            this.siteurl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sitetype1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sitetype2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.siteaddtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sitestate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webSiteDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.tableLayoutPanel1);
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Size = new System.Drawing.Size(687, 391);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(467, 32);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 21);
            this.nameTextBox.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(396, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "网站名称：";
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(586, 32);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 4;
            this.searchBtn.Text = "查询";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.webSiteDGV, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(687, 391);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.nopassLabel);
            this.panel2.Controls.Add(this.checkLabel);
            this.panel2.Controls.Add(this.nocheckLabel);
            this.panel2.Controls.Add(this.siteCountLab);
            this.panel2.Controls.Add(this.CountLabel);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.nopassLab);
            this.panel2.Controls.Add(this.NoCheckLab);
            this.panel2.Controls.Add(this.checkLab);
            this.panel2.Controls.Add(this.siteCountLabel);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.nameTextBox);
            this.panel2.Controls.Add(this.searchBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(681, 94);
            this.panel2.TabIndex = 7;
            // 
            // nopassLabel
            // 
            this.nopassLabel.AutoSize = true;
            this.nopassLabel.Location = new System.Drawing.Point(354, 56);
            this.nopassLabel.Name = "nopassLabel";
            this.nopassLabel.Size = new System.Drawing.Size(11, 12);
            this.nopassLabel.TabIndex = 5;
            this.nopassLabel.Text = "0";
            // 
            // checkLabel
            // 
            this.checkLabel.AutoSize = true;
            this.checkLabel.Location = new System.Drawing.Point(170, 56);
            this.checkLabel.Name = "checkLabel";
            this.checkLabel.Size = new System.Drawing.Size(11, 12);
            this.checkLabel.TabIndex = 5;
            this.checkLabel.Text = "0";
            // 
            // nocheckLabel
            // 
            this.nocheckLabel.AutoSize = true;
            this.nocheckLabel.Location = new System.Drawing.Point(354, 37);
            this.nocheckLabel.Name = "nocheckLabel";
            this.nocheckLabel.Size = new System.Drawing.Size(11, 12);
            this.nocheckLabel.TabIndex = 5;
            this.nocheckLabel.Text = "0";
            // 
            // siteCountLab
            // 
            this.siteCountLab.AutoSize = true;
            this.siteCountLab.Location = new System.Drawing.Point(170, 15);
            this.siteCountLab.Name = "siteCountLab";
            this.siteCountLab.Size = new System.Drawing.Size(11, 12);
            this.siteCountLab.TabIndex = 5;
            this.siteCountLab.Text = "0";
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Location = new System.Drawing.Point(170, 37);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(11, 12);
            this.CountLabel.TabIndex = 5;
            this.CountLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "提交网站数量：";
            // 
            // nopassLab
            // 
            this.nopassLab.AutoSize = true;
            this.nopassLab.Location = new System.Drawing.Point(208, 56);
            this.nopassLab.Name = "nopassLab";
            this.nopassLab.Size = new System.Drawing.Size(101, 12);
            this.nopassLab.TabIndex = 5;
            this.nopassLab.Text = "不合格网站数量：";
            // 
            // NoCheckLab
            // 
            this.NoCheckLab.AutoSize = true;
            this.NoCheckLab.Location = new System.Drawing.Point(208, 37);
            this.NoCheckLab.Name = "NoCheckLab";
            this.NoCheckLab.Size = new System.Drawing.Size(101, 12);
            this.NoCheckLab.TabIndex = 5;
            this.NoCheckLab.Text = "未审核网站数量：";
            // 
            // checkLab
            // 
            this.checkLab.AutoSize = true;
            this.checkLab.Location = new System.Drawing.Point(24, 56);
            this.checkLab.Name = "checkLab";
            this.checkLab.Size = new System.Drawing.Size(101, 12);
            this.checkLab.TabIndex = 5;
            this.checkLab.Text = "已审核网站数量：";
            // 
            // siteCountLabel
            // 
            this.siteCountLabel.AutoSize = true;
            this.siteCountLabel.Location = new System.Drawing.Point(24, 15);
            this.siteCountLabel.Name = "siteCountLabel";
            this.siteCountLabel.Size = new System.Drawing.Size(65, 12);
            this.siteCountLabel.TabIndex = 5;
            this.siteCountLabel.Text = "网站数量：";
            // 
            // webSiteDGV
            // 
            this.webSiteDGV.AllowUserToAddRows = false;
            this.webSiteDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.webSiteDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.siteId,
            this.sitename,
            this.siteurl,
            this.sitetype1,
            this.sitetype2,
            this.siteaddtime,
            this.sitestate});
            this.webSiteDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webSiteDGV.Location = new System.Drawing.Point(3, 103);
            this.webSiteDGV.Name = "webSiteDGV";
            this.webSiteDGV.ReadOnly = true;
            this.webSiteDGV.RowTemplate.Height = 23;
            this.webSiteDGV.Size = new System.Drawing.Size(681, 325);
            this.webSiteDGV.TabIndex = 8;
            this.webSiteDGV.SelectionChanged += new System.EventHandler(this.webSiteDGV_SelectionChanged);
            // 
            // siteId
            // 
            this.siteId.HeaderText = "Column1";
            this.siteId.Name = "siteId";
            this.siteId.ReadOnly = true;
            this.siteId.Visible = false;
            // 
            // sitename
            // 
            this.sitename.HeaderText = "网站名称";
            this.sitename.Name = "sitename";
            this.sitename.ReadOnly = true;
            this.sitename.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sitename.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // siteurl
            // 
            this.siteurl.HeaderText = "网址";
            this.siteurl.Name = "siteurl";
            this.siteurl.ReadOnly = true;
            // 
            // sitetype1
            // 
            this.sitetype1.HeaderText = "自填类型";
            this.sitetype1.Name = "sitetype1";
            this.sitetype1.ReadOnly = true;
            // 
            // sitetype2
            // 
            this.sitetype2.HeaderText = "审核类型";
            this.sitetype2.Name = "sitetype2";
            this.sitetype2.ReadOnly = true;
            // 
            // siteaddtime
            // 
            this.siteaddtime.HeaderText = "加入时间";
            this.siteaddtime.Name = "siteaddtime";
            this.siteaddtime.ReadOnly = true;
            // 
            // sitestate
            // 
            this.sitestate.HeaderText = "状态";
            this.sitestate.Name = "sitestate";
            this.sitestate.ReadOnly = true;
            // 
            // ViewWebSiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 416);
            this.Name = "ViewWebSiteForm";
            this.TabText = "网站管理";
            this.Text = "网站管理";
            this.MainPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webSiteDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView webSiteDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteId;
        private System.Windows.Forms.DataGridViewLinkColumn sitename;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteurl;
        private System.Windows.Forms.DataGridViewTextBoxColumn sitetype1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sitetype2;
        private System.Windows.Forms.DataGridViewTextBoxColumn siteaddtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn sitestate;
        private System.Windows.Forms.Label siteCountLabel;
        private System.Windows.Forms.Label nopassLabel;
        private System.Windows.Forms.Label checkLabel;
        private System.Windows.Forms.Label nocheckLabel;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label nopassLab;
        private System.Windows.Forms.Label NoCheckLab;
        private System.Windows.Forms.Label checkLab;
        private System.Windows.Forms.Label siteCountLab;
    }
}