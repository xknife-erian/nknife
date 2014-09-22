namespace Jeelu.SimplusOM.Client
{
    partial class NewsForm
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
            this.pulishTimeDTP = new System.Windows.Forms.DateTimePicker();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.newColComboBox = new System.Windows.Forms.ComboBox();
            this.contentWebBrowser = new System.Windows.Forms.WebBrowser();
            this.addImgBtn = new System.Windows.Forms.Button();
            this.AddPBtn = new System.Windows.Forms.Button();
            this.AddBBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pulishTimeDTP
            // 
            this.pulishTimeDTP.Location = new System.Drawing.Point(111, 69);
            this.pulishTimeDTP.Name = "pulishTimeDTP";
            this.pulishTimeDTP.Size = new System.Drawing.Size(280, 21);
            this.pulishTimeDTP.TabIndex = 10;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(111, 15);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(280, 21);
            this.titleTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "内容：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "发布时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "栏目：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "标题：";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(456, 525);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 12;
            this.OKBtn.Text = "确定";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(537, 525);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 12;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // newColComboBox
            // 
            this.newColComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newColComboBox.FormattingEnabled = true;
            this.newColComboBox.Items.AddRange(new object[] {
            "公司动态",
            "行业动态"});
            this.newColComboBox.Location = new System.Drawing.Point(111, 43);
            this.newColComboBox.Name = "newColComboBox";
            this.newColComboBox.Size = new System.Drawing.Size(280, 20);
            this.newColComboBox.TabIndex = 13;
            // 
            // contentWebBrowser
            // 
            this.contentWebBrowser.Location = new System.Drawing.Point(30, 130);
            this.contentWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.contentWebBrowser.Name = "contentWebBrowser";
            this.contentWebBrowser.Size = new System.Drawing.Size(568, 389);
            this.contentWebBrowser.TabIndex = 14;
            // 
            // addImgBtn
            // 
            this.addImgBtn.Location = new System.Drawing.Point(84, 101);
            this.addImgBtn.Name = "addImgBtn";
            this.addImgBtn.Size = new System.Drawing.Size(38, 23);
            this.addImgBtn.TabIndex = 15;
            this.addImgBtn.Text = "图片";
            this.addImgBtn.UseVisualStyleBackColor = true;
            this.addImgBtn.Click += new System.EventHandler(this.AddImgBtn_Click);
            // 
            // AddPBtn
            // 
            this.AddPBtn.Location = new System.Drawing.Point(128, 101);
            this.AddPBtn.Name = "AddPBtn";
            this.AddPBtn.Size = new System.Drawing.Size(38, 23);
            this.AddPBtn.TabIndex = 15;
            this.AddPBtn.Text = "<P>";
            this.AddPBtn.UseVisualStyleBackColor = true;
            this.AddPBtn.Click += new System.EventHandler(this.AddPBtn_Click);
            // 
            // AddBBtn
            // 
            this.AddBBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddBBtn.Location = new System.Drawing.Point(172, 101);
            this.AddBBtn.Name = "AddBBtn";
            this.AddBBtn.Size = new System.Drawing.Size(38, 23);
            this.AddBBtn.TabIndex = 15;
            this.AddBBtn.Text = "<B>";
            this.AddBBtn.UseVisualStyleBackColor = true;
            this.AddBBtn.Click += new System.EventHandler(this.AddBBtn_Click);
            // 
            // NewsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 560);
            this.Controls.Add(this.AddBBtn);
            this.Controls.Add(this.AddPBtn);
            this.Controls.Add(this.addImgBtn);
            this.Controls.Add(this.contentWebBrowser);
            this.Controls.Add(this.newColComboBox);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.pulishTimeDTP);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewsForm";
            this.Text = "新闻";
            this.Load += new System.EventHandler(this.NewsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker pulishTimeDTP;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.ComboBox newColComboBox;
        private System.Windows.Forms.WebBrowser contentWebBrowser;
        private System.Windows.Forms.Button addImgBtn;
        private System.Windows.Forms.Button AddPBtn;
        private System.Windows.Forms.Button AddBBtn;

    }
}