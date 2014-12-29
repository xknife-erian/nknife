namespace NKnife.Kits.SerialKnife.Views
{
    partial class SettingForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnableMockDataConnectorRadioButton = new System.Windows.Forms.RadioButton();
            this.EnableRealDataConnectorRadioButton = new System.Windows.Forms.RadioButton();
            this.PortNumberComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveSettingButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.EnableRealDataConnectorRadioButton);
            this.groupBox1.Controls.Add(this.PortNumberComboBox);
            this.groupBox1.Controls.Add(this.EnableMockDataConnectorRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据连接器";
            // 
            // EnableMockDataConnectorRadioButton
            // 
            this.EnableMockDataConnectorRadioButton.AutoSize = true;
            this.EnableMockDataConnectorRadioButton.Checked = true;
            this.EnableMockDataConnectorRadioButton.Location = new System.Drawing.Point(34, 32);
            this.EnableMockDataConnectorRadioButton.Name = "EnableMockDataConnectorRadioButton";
            this.EnableMockDataConnectorRadioButton.Size = new System.Drawing.Size(109, 17);
            this.EnableMockDataConnectorRadioButton.TabIndex = 0;
            this.EnableMockDataConnectorRadioButton.TabStop = true;
            this.EnableMockDataConnectorRadioButton.Text = "仿真数据连接器";
            this.EnableMockDataConnectorRadioButton.UseVisualStyleBackColor = true;
            // 
            // EnableRealDataConnectorRadioButton
            // 
            this.EnableRealDataConnectorRadioButton.AutoSize = true;
            this.EnableRealDataConnectorRadioButton.Location = new System.Drawing.Point(186, 32);
            this.EnableRealDataConnectorRadioButton.Name = "EnableRealDataConnectorRadioButton";
            this.EnableRealDataConnectorRadioButton.Size = new System.Drawing.Size(133, 17);
            this.EnableRealDataConnectorRadioButton.TabIndex = 1;
            this.EnableRealDataConnectorRadioButton.Text = "真实串口数据连接器";
            this.EnableRealDataConnectorRadioButton.UseVisualStyleBackColor = true;
            // 
            // PortNumberComboBox
            // 
            this.PortNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortNumberComboBox.FormattingEnabled = true;
            this.PortNumberComboBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8"});
            this.PortNumberComboBox.Location = new System.Drawing.Point(94, 73);
            this.PortNumberComboBox.Name = "PortNumberComboBox";
            this.PortNumberComboBox.Size = new System.Drawing.Size(121, 21);
            this.PortNumberComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "端口号：";
            // 
            // SaveSettingButton
            // 
            this.SaveSettingButton.Location = new System.Drawing.Point(183, 158);
            this.SaveSettingButton.Name = "SaveSettingButton";
            this.SaveSettingButton.Size = new System.Drawing.Size(75, 36);
            this.SaveSettingButton.TabIndex = 1;
            this.SaveSettingButton.Text = "保存";
            this.SaveSettingButton.UseVisualStyleBackColor = true;
            this.SaveSettingButton.Click += new System.EventHandler(this.SaveSettingButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(289, 158);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 36);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "退出";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 210);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveSettingButton);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(120, 149);
            this.Name = "SettingForm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton EnableRealDataConnectorRadioButton;
        private System.Windows.Forms.RadioButton EnableMockDataConnectorRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PortNumberComboBox;
        private System.Windows.Forms.Button SaveSettingButton;
        private System.Windows.Forms.Button ExitButton;
    }
}