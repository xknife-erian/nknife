namespace Jeelu.SimplusOM.Client
{
    partial class OMBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OMBaseForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.NewTSButton = new System.Windows.Forms.ToolStripButton();
            this.EditTSButton = new System.Windows.Forms.ToolStripButton();
            this.FrozedTSButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteTSButton = new System.Windows.Forms.ToolStripButton();
            this.CancelTSButton = new System.Windows.Forms.ToolStripButton();
            this.SaveTSButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ChargeTSButton = new System.Windows.Forms.ToolStripButton();
            this.ReturnSetTSButton = new System.Windows.Forms.ToolStripButton();
            this.ReturnTSButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CheckTSButton = new System.Windows.Forms.ToolStripButton();
            this.PrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CloseTSButton = new System.Windows.Forms.ToolStripButton();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTSButton,
            this.EditTSButton,
            this.FrozedTSButton,
            this.DeleteTSButton,
            this.CancelTSButton,
            this.SaveTSButton,
            this.toolStripSeparator1,
            this.ChargeTSButton,
            this.ReturnSetTSButton,
            this.ReturnTSButton,
            this.toolStripSeparator2,
            this.CheckTSButton,
            this.PrintToolStripButton,
            this.toolStripSeparator3,
            this.CloseTSButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(602, 35);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "toolStrip1";
            this.mainToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainToolStrip_ItemClicked);
            // 
            // NewTSButton
            // 
            this.NewTSButton.Image = ((System.Drawing.Image)(resources.GetObject("NewTSButton.Image")));
            this.NewTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewTSButton.Name = "NewTSButton";
            this.NewTSButton.Size = new System.Drawing.Size(33, 32);
            this.NewTSButton.Text = "新增";
            this.NewTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // EditTSButton
            // 
            this.EditTSButton.Image = ((System.Drawing.Image)(resources.GetObject("EditTSButton.Image")));
            this.EditTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditTSButton.Name = "EditTSButton";
            this.EditTSButton.Size = new System.Drawing.Size(33, 32);
            this.EditTSButton.Text = "修改";
            this.EditTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // FrozedTSButton
            // 
            this.FrozedTSButton.Image = ((System.Drawing.Image)(resources.GetObject("FrozedTSButton.Image")));
            this.FrozedTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FrozedTSButton.Name = "FrozedTSButton";
            this.FrozedTSButton.Size = new System.Drawing.Size(33, 32);
            this.FrozedTSButton.Text = "冻结";
            this.FrozedTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // DeleteTSButton
            // 
            this.DeleteTSButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteTSButton.Image")));
            this.DeleteTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteTSButton.Name = "DeleteTSButton";
            this.DeleteTSButton.Size = new System.Drawing.Size(33, 32);
            this.DeleteTSButton.Text = "删除";
            this.DeleteTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // CancelTSButton
            // 
            this.CancelTSButton.Image = ((System.Drawing.Image)(resources.GetObject("CancelTSButton.Image")));
            this.CancelTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CancelTSButton.Name = "CancelTSButton";
            this.CancelTSButton.Size = new System.Drawing.Size(33, 32);
            this.CancelTSButton.Text = "取消";
            this.CancelTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CancelTSButton.ToolTipText = "取消";
            // 
            // SaveTSButton
            // 
            this.SaveTSButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveTSButton.Image")));
            this.SaveTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveTSButton.Name = "SaveTSButton";
            this.SaveTSButton.Size = new System.Drawing.Size(33, 32);
            this.SaveTSButton.Text = "保存";
            this.SaveTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SaveTSButton.ToolTipText = "保存";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // ChargeTSButton
            // 
            this.ChargeTSButton.Image = ((System.Drawing.Image)(resources.GetObject("ChargeTSButton.Image")));
            this.ChargeTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ChargeTSButton.Name = "ChargeTSButton";
            this.ChargeTSButton.Size = new System.Drawing.Size(33, 32);
            this.ChargeTSButton.Text = "充值";
            this.ChargeTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ReturnSetTSButton
            // 
            this.ReturnSetTSButton.Image = ((System.Drawing.Image)(resources.GetObject("ReturnSetTSButton.Image")));
            this.ReturnSetTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReturnSetTSButton.Name = "ReturnSetTSButton";
            this.ReturnSetTSButton.Size = new System.Drawing.Size(57, 32);
            this.ReturnSetTSButton.Text = "返点设置";
            this.ReturnSetTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ReturnTSButton
            // 
            this.ReturnTSButton.Image = ((System.Drawing.Image)(resources.GetObject("ReturnTSButton.Image")));
            this.ReturnTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReturnTSButton.Name = "ReturnTSButton";
            this.ReturnTSButton.Size = new System.Drawing.Size(33, 32);
            this.ReturnTSButton.Text = "返点";
            this.ReturnTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // CheckTSButton
            // 
            this.CheckTSButton.Image = ((System.Drawing.Image)(resources.GetObject("CheckTSButton.Image")));
            this.CheckTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CheckTSButton.Name = "CheckTSButton";
            this.CheckTSButton.Size = new System.Drawing.Size(33, 32);
            this.CheckTSButton.Text = "审核";
            this.CheckTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // PrintToolStripButton
            // 
            this.PrintToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PrintToolStripButton.Image")));
            this.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton.Name = "PrintToolStripButton";
            this.PrintToolStripButton.Size = new System.Drawing.Size(33, 32);
            this.PrintToolStripButton.Text = "打印";
            this.PrintToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 35);
            // 
            // CloseTSButton
            // 
            this.CloseTSButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseTSButton.Image")));
            this.CloseTSButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTSButton.Name = "CloseTSButton";
            this.CloseTSButton.Size = new System.Drawing.Size(33, 32);
            this.CloseTSButton.Text = "关闭";
            this.CloseTSButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 35);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(602, 260);
            this.MainPanel.TabIndex = 3;
            // 
            // OMBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 295);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.mainToolStrip);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "OMBaseForm";
            this.TabText = "OMBaseForm";
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton NewTSButton;
        private System.Windows.Forms.ToolStripButton EditTSButton;
        private System.Windows.Forms.ToolStripButton FrozedTSButton;
        private System.Windows.Forms.ToolStripButton DeleteTSButton;
        private System.Windows.Forms.ToolStripButton CancelTSButton;
        private System.Windows.Forms.ToolStripButton SaveTSButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ChargeTSButton;
        private System.Windows.Forms.ToolStripButton ReturnSetTSButton;
        private System.Windows.Forms.ToolStripButton ReturnTSButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton CheckTSButton;
        private System.Windows.Forms.ToolStripButton PrintToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton CloseTSButton;
        public System.Windows.Forms.Panel MainPanel;
        public System.Windows.Forms.ToolStrip mainToolStrip;

    }
}