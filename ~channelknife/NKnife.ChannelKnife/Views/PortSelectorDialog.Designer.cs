
using System.Windows.Forms;

namespace NKnife.ChannelKnife.Views
{
    partial class PortSelectorDialog
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
            this._AcceptButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._ListView = new BrightIdeasSoftware.DataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this._TabControl = new System.Windows.Forms.TabControl();
            this._SerialPortTabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this._ListView)).BeginInit();
            this._TabControl.SuspendLayout();
            this._SerialPortTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Enabled = false;
            this._AcceptButton.Location = new System.Drawing.Point(272, 220);
            this._AcceptButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(71, 31);
            this._AcceptButton.TabIndex = 0;
            this._AcceptButton.Text = "确定(&A)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(349, 220);
            this._CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(71, 31);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _ListView
            // 
            this._ListView.AllColumns.Add(this.olvColumn1);
            this._ListView.AllColumns.Add(this.olvColumn2);
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this._ListView.DataSource = null;
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.Location = new System.Drawing.Point(3, 3);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.ShowGroups = false;
            this._ListView.Size = new System.Drawing.Size(396, 165);
            this._ListView.TabIndex = 3;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "SerialPort";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Text = "端口";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Description";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Text = "描述";
            this.olvColumn2.Width = 301;
            // 
            // _TabControl
            // 
            this._TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._TabControl.Controls.Add(this._SerialPortTabPage);
            this._TabControl.Location = new System.Drawing.Point(12, 12);
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(410, 201);
            this._TabControl.TabIndex = 4;
            // 
            // _SerialPortTabPage
            // 
            this._SerialPortTabPage.Controls.Add(this._ListView);
            this._SerialPortTabPage.Location = new System.Drawing.Point(4, 26);
            this._SerialPortTabPage.Name = "_SerialPortTabPage";
            this._SerialPortTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._SerialPortTabPage.Size = new System.Drawing.Size(402, 171);
            this._SerialPortTabPage.TabIndex = 0;
            this._SerialPortTabPage.Text = "串口";
            this._SerialPortTabPage.UseVisualStyleBackColor = true;
            // 
            // PortSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(434, 264);
            this.Controls.Add(this._TabControl);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PortSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "端口选择";
            ((System.ComponentModel.ISupportInitialize)(this._ListView)).EndInit();
            this._TabControl.ResumeLayout(false);
            this._SerialPortTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private BrightIdeasSoftware.DataListView _ListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private TabControl _TabControl;
        private TabPage _SerialPortTabPage;
    }
}