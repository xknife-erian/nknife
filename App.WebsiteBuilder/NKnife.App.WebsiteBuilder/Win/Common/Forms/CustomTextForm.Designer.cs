namespace Jeelu.Win
{
    partial class CustomTextForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxCustomText = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelImg = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(13, 209);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(47, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "资源名:";
            // 
            // textBoxCustomText
            // 
            this.textBoxCustomText.Location = new System.Drawing.Point(66, 206);
            this.textBoxCustomText.Name = "textBoxCustomText";
            this.textBoxCustomText.Size = new System.Drawing.Size(143, 21);
            this.textBoxCustomText.TabIndex = 1;
            this.textBoxCustomText.Leave += new System.EventHandler(this.textBoxCustomText_Leave);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(98, 233);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(52, 25);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "保 存";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(156, 233);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(52, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelImg
            // 
            this.panelImg.BackColor = System.Drawing.Color.White;
            this.panelImg.Location = new System.Drawing.Point(213, 10);
            this.panelImg.Name = "panelImg";
            this.panelImg.Size = new System.Drawing.Size(195, 248);
            this.panelImg.TabIndex = 4;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(14, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(195, 188);
            this.treeView1.TabIndex = 5;
            // 
            // CustomTextForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 270);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panelImg);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxCustomText);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomTextForm";
            this.Text = "CustomTextForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxCustomText;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelImg;
        private System.Windows.Forms.TreeView treeView1;
    }
}