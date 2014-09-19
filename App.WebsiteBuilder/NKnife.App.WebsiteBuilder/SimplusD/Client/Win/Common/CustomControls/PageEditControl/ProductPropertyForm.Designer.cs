namespace Jeelu.SimplusD.Client.Win
{
    partial class ProductPropertyForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblAutoDefineProp = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.txtAutoDefineProp = new System.Windows.Forms.TextBox();
            this.txtPropGroupName = new System.Windows.Forms.TextBox();
            this.lblPropGroupName = new System.Windows.Forms.Label();
            this.conProductPropGroup = new Jeelu.Win.SelectGroup();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(373, 332);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblAutoDefineProp
            // 
            this.lblAutoDefineProp.AutoSize = true;
            this.lblAutoDefineProp.Location = new System.Drawing.Point(12, 36);
            this.lblAutoDefineProp.Name = "lblAutoDefineProp";
            this.lblAutoDefineProp.Size = new System.Drawing.Size(71, 12);
            this.lblAutoDefineProp.TabIndex = 11;
            this.lblAutoDefineProp.Text = "自定义属性:";
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(244, 32);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(38, 22);
            this.btnYes.TabIndex = 10;
            this.btnYes.Text = "添加";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtAutoDefineProp
            // 
            this.txtAutoDefineProp.Location = new System.Drawing.Point(83, 32);
            this.txtAutoDefineProp.Name = "txtAutoDefineProp";
            this.txtAutoDefineProp.Size = new System.Drawing.Size(158, 21);
            this.txtAutoDefineProp.TabIndex = 9;
            // 
            // txtPropGroupName
            // 
            this.txtPropGroupName.Location = new System.Drawing.Point(83, 9);
            this.txtPropGroupName.Name = "txtPropGroupName";
            this.txtPropGroupName.Size = new System.Drawing.Size(158, 21);
            this.txtPropGroupName.TabIndex = 8;
            // 
            // lblPropGroupName
            // 
            this.lblPropGroupName.AutoSize = true;
            this.lblPropGroupName.Location = new System.Drawing.Point(12, 13);
            this.lblPropGroupName.Name = "lblPropGroupName";
            this.lblPropGroupName.Size = new System.Drawing.Size(71, 12);
            this.lblPropGroupName.TabIndex = 7;
            this.lblPropGroupName.Text = "属性组名称:";
            // 
            // conProductPropGroup
            // 
            this.conProductPropGroup.DataSource = null;
            this.conProductPropGroup.DisplayMember = null;
            this.conProductPropGroup.HIndent = 8;
            this.conProductPropGroup.HorizontalCount = 6;
            this.conProductPropGroup.LineHeight = 20;
            this.conProductPropGroup.Location = new System.Drawing.Point(9, 79);
            this.conProductPropGroup.MultiSelect = true;
            this.conProductPropGroup.Name = "conProductPropGroup";
            this.conProductPropGroup.SelectedStringValues = new string[0];
            this.conProductPropGroup.SelectedValue = null;
            this.conProductPropGroup.SelectedValues = new object[0];
            this.conProductPropGroup.Size = new System.Drawing.Size(446, 250);
            this.conProductPropGroup.TabIndex = 14;
            this.conProductPropGroup.Text = null;
            this.conProductPropGroup.ValueMember = null;
            this.conProductPropGroup.VIndent = 5;
            // 
            // ProductPropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 367);
            this.Controls.Add(this.conProductPropGroup);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblAutoDefineProp);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.txtAutoDefineProp);
            this.Controls.Add(this.txtPropGroupName);
            this.Controls.Add(this.lblPropGroupName);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KeyPreview = true;
            this.Name = "ProductPropertyForm";
            this.Text = "ProductPropertyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblAutoDefineProp;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.TextBox txtAutoDefineProp;
        private System.Windows.Forms.TextBox txtPropGroupName;
        private System.Windows.Forms.Label lblPropGroupName;
        private Jeelu.Win.SelectGroup conProductPropGroup;


    }
}