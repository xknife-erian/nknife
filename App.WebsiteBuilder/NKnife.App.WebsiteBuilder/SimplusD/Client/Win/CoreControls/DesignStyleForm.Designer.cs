namespace Jeelu.SimplusD.Client.Win
{
    partial class DesignStyleForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelDisplayStyle = new System.Windows.Forms.Panel();
            this.comboBoxSelectStyle = new System.Windows.Forms.ComboBox();
            this.labelSelectStyle = new System.Windows.Forms.Label();
            this.treeViewParts = new System.Windows.Forms.TreeView();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(426, 287);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(55, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "确 定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(487, 287);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(55, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取 消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // panelDisplayStyle
            // 
            this.panelDisplayStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDisplayStyle.BackColor = System.Drawing.Color.White;
            this.panelDisplayStyle.Location = new System.Drawing.Point(180, 33);
            this.panelDisplayStyle.Name = "panelDisplayStyle";
            this.panelDisplayStyle.Size = new System.Drawing.Size(362, 243);
            this.panelDisplayStyle.TabIndex = 3;
            // 
            // comboBoxSelectStyle
            // 
            this.comboBoxSelectStyle.FormattingEnabled = true;
            this.comboBoxSelectStyle.Location = new System.Drawing.Point(113, 6);
            this.comboBoxSelectStyle.Name = "comboBoxSelectStyle";
            this.comboBoxSelectStyle.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSelectStyle.TabIndex = 4;
            // 
            // labelSelectStyle
            // 
            this.labelSelectStyle.AutoSize = true;
            this.labelSelectStyle.Location = new System.Drawing.Point(12, 9);
            this.labelSelectStyle.Name = "labelSelectStyle";
            this.labelSelectStyle.Size = new System.Drawing.Size(95, 13);
            this.labelSelectStyle.TabIndex = 5;
            this.labelSelectStyle.Text = "选择/新建样式：";
            // 
            // treeViewParts
            // 
            this.treeViewParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewParts.Location = new System.Drawing.Point(12, 33);
            this.treeViewParts.Name = "treeViewParts";
            this.treeViewParts.ShowNodeToolTips = true;
            this.treeViewParts.Size = new System.Drawing.Size(162, 243);
            this.treeViewParts.TabIndex = 6;
            this.treeViewParts.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewParts_NodeMouseDoubleClick);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(240, 4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(55, 23);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "删 除";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // DesignStyleForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 322);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.treeViewParts);
            this.Controls.Add(this.labelSelectStyle);
            this.Controls.Add(this.comboBoxSelectStyle);
            this.Controls.Add(this.panelDisplayStyle);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "DesignStyleForm";
            this.Text = "编辑样式";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelDisplayStyle;
        private System.Windows.Forms.ComboBox comboBoxSelectStyle;
        private System.Windows.Forms.Label labelSelectStyle;
        private System.Windows.Forms.TreeView treeViewParts;
        private System.Windows.Forms.Button buttonDelete;
    }
}