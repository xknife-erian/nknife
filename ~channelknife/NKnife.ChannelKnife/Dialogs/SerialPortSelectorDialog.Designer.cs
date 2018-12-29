
using System.Windows.Forms;

namespace NKnife.ChannelKnife.Dialogs
{
    partial class SerialPortSelectorDialog
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
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this._ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // _AcceptButton
            // 
            this._AcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._AcceptButton.Location = new System.Drawing.Point(328, 174);
            this._AcceptButton.Name = "_AcceptButton";
            this._AcceptButton.Size = new System.Drawing.Size(75, 27);
            this._AcceptButton.TabIndex = 0;
            this._AcceptButton.Text = "确定(&A)";
            this._AcceptButton.UseVisualStyleBackColor = true;
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(409, 174);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(75, 27);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "取消(&C)";
            this._CancelButton.UseVisualStyleBackColor = true;
            // 
            // _ListView
            // 
            this._ListView.AllColumns.Add(this.olvColumn1);
            this._ListView.AllColumns.Add(this.olvColumn2);
            this._ListView.AllColumns.Add(this.olvColumn3);
            this._ListView.AllColumns.Add(this.olvColumn4);
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this._ListView.DataSource = null;
            this._ListView.Location = new System.Drawing.Point(13, 13);
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(471, 155);
            this._ListView.TabIndex = 2;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.CellPadding = null;
            // 
            // olvColumn2
            // 
            this.olvColumn2.CellPadding = null;
            // 
            // olvColumn3
            // 
            this.olvColumn3.CellPadding = null;
            // 
            // olvColumn4
            // 
            this.olvColumn4.CellPadding = null;
            // 
            // SerialPortSelectorDialog
            // 
            this.AcceptButton = this._AcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(496, 212);
            this.Controls.Add(this._ListView);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._AcceptButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SerialPortSelectorDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择串口";
            ((System.ComponentModel.ISupportInitialize)(this._ListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _AcceptButton;
        private System.Windows.Forms.Button _CancelButton;
        private BrightIdeasSoftware.DataListView _ListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}