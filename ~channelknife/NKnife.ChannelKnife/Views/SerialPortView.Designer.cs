using System.Windows.Forms;
using NKnife.ChannelKnife.Views.Controls;

namespace NKnife.ChannelKnife.Views
{
    partial class SerialPortView
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
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._PauseButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._SwitchSyncSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
            this._SyncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AsyncMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._IsDisplayQuestionCheckbox = new CheckBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._DataClearButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._TxCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this._RxCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this._BaudRateLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._RightPanel = new System.Windows.Forms.Panel();
            this._ListView = new ChannelDataListView();
            this._QuestionsEditorPanel = new QuestionsEditorPanel();
            this._SerialConfigPanel = new ConfigPanel();
            this._ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this._RightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartButton,
            this._PauseButton,
            this._StopButton,
            this.toolStripSeparator1,
            this._SwitchSyncSplitButton,
            this.toolStripSeparator2,
            //this._IsDisplayQuestionCheckbox,
            this.toolStripSeparator3,
            this._DataClearButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._ToolStrip.Size = new System.Drawing.Size(517, 25);
            this._ToolStrip.TabIndex = 2;
            // 
            // _StartButton
            // 
            this._StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartButton.Margin = new System.Windows.Forms.Padding(2);
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(23, 21);
            this._StartButton.Text = "打开端口监听";
            // 
            // _PauseButton
            // 
            this._PauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PauseButton.Margin = new System.Windows.Forms.Padding(2);
            this._PauseButton.Name = "_PauseButton";
            this._PauseButton.Size = new System.Drawing.Size(23, 21);
            this._PauseButton.Text = "暂停端口监听";
            // 
            // _StopButton
            // 
            this._StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopButton.Margin = new System.Windows.Forms.Padding(2);
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(23, 21);
            this._StopButton.Text = "关闭端口监听";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _SwitchSyncSplitButton
            // 
            this._SwitchSyncSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SyncMenuItem,
            this._AsyncMenuItem});
            this._SwitchSyncSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SwitchSyncSplitButton.Name = "_SwitchSyncSplitButton";
            this._SwitchSyncSplitButton.Size = new System.Drawing.Size(61, 22);
            this._SwitchSyncSplitButton.Text = "异步";
            // 
            // _SyncMenuItem
            // 
            this._SyncMenuItem.Name = "_SyncMenuItem";
            this._SyncMenuItem.Size = new System.Drawing.Size(100, 22);
            this._SyncMenuItem.Text = "同步";
            // 
            // _AsyncMenuItem
            // 
            this._AsyncMenuItem.Name = "_AsyncMenuItem";
            this._AsyncMenuItem.Size = new System.Drawing.Size(100, 22);
            this._AsyncMenuItem.Text = "异步";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _IsDisplayQuestionCheckbox
            // 
            this._IsDisplayQuestionCheckbox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this._IsDisplayQuestionCheckbox.Name = "_IsDisplayQuestionCheckbox";
            this._IsDisplayQuestionCheckbox.Size = new System.Drawing.Size(72, 21);
            this._IsDisplayQuestionCheckbox.Text = "显示发送";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _DataClearButton
            // 
            this._DataClearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._DataClearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DataClearButton.Name = "_DataClearButton";
            this._DataClearButton.Size = new System.Drawing.Size(23, 22);
            this._DataClearButton.Text = "清空数据";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._ListView);
            this.splitContainer1.Panel1.Controls.Add(this._StatusStrip);
            this.splitContainer1.Panel1.Controls.Add(this._ToolStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this._RightPanel);
            this.splitContainer1.Panel2MinSize = 220;
            this.splitContainer1.Size = new System.Drawing.Size(821, 595);
            this.splitContainer1.SplitterDistance = 517;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._TxCountLabel,
            this.toolStripStatusLabel3,
            this._RxCountLabel,
            this.toolStripStatusLabel5,
            this._BaudRateLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 573);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(517, 22);
            this._StatusStrip.TabIndex = 4;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel1.Text = "TX:";
            // 
            // _TxCountLabel
            // 
            this._TxCountLabel.Name = "_TxCountLabel";
            this._TxCountLabel.Size = new System.Drawing.Size(15, 17);
            this._TxCountLabel.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabel3.Text = "RX:";
            // 
            // _RxCountLabel
            // 
            this._RxCountLabel.Name = "_RxCountLabel";
            this._RxCountLabel.Size = new System.Drawing.Size(15, 17);
            this._RxCountLabel.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(419, 17);
            this.toolStripStatusLabel5.Spring = true;
            // 
            // _BaudRateLabel
            // 
            this._BaudRateLabel.Name = "_BaudRateLabel";
            this._BaudRateLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _RightPanel
            // 
            this._RightPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._RightPanel.Controls.Add(this._QuestionsEditorPanel);
            this._RightPanel.Controls.Add(this._SerialConfigPanel);
            this._RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._RightPanel.Location = new System.Drawing.Point(0, 0);
            this._RightPanel.Name = "_RightPanel";
            this._RightPanel.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this._RightPanel.Size = new System.Drawing.Size(299, 595);
            this._RightPanel.TabIndex = 0;
            // 
            // _ListView
            // 
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._ListView.Location = new System.Drawing.Point(0, 25);
            this._ListView.Name = "_ListView";
            this._ListView.SerialDataListViewViewData = null;
            this._ListView.Size = new System.Drawing.Size(517, 548);
            this._ListView.TabIndex = 3;
            // 
            // _QuestionsEditorPanel
            // 
            this._QuestionsEditorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._QuestionsEditorPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._QuestionsEditorPanel.Location = new System.Drawing.Point(0, 293);
            this._QuestionsEditorPanel.Name = "_QuestionsEditorPanel";
            this._QuestionsEditorPanel.Size = new System.Drawing.Size(295, 204);
            this._QuestionsEditorPanel.TabIndex = 2;
            this._QuestionsEditorPanel.ViewData = null;
            // 
            // _SerialConfigPanel
            // 
            this._SerialConfigPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._SerialConfigPanel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this._SerialConfigPanel.Location = new System.Drawing.Point(0, 0);
            this._SerialConfigPanel.Margin = new System.Windows.Forms.Padding(0);
            this._SerialConfigPanel.Name = "_SerialConfigPanel";
            this._SerialConfigPanel.Size = new System.Drawing.Size(295, 293);
            this._SerialConfigPanel.TabIndex = 3;
            this._SerialConfigPanel.ViewData = null;
            // 
            // SerialPortView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 595);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SerialPortView";
            this.Text = "SerialPortView";
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this._RightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _StartButton;
        private System.Windows.Forms.ToolStripButton _PauseButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ChannelDataListView _ListView;
        private System.Windows.Forms.ToolStripButton _DataClearButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton _SwitchSyncSplitButton;
        private System.Windows.Forms.ToolStripMenuItem _SyncMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AsyncMenuItem;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _TxCountLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel _RxCountLabel;
        private System.Windows.Forms.Panel _RightPanel;
        private QuestionsEditorPanel _QuestionsEditorPanel;
        private ConfigPanel _SerialConfigPanel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel _BaudRateLabel;
        private CheckBox _IsDisplayQuestionCheckbox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}