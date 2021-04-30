namespace Gean.Module.Chess.Demo
{
    partial class MyDemoDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("RootNode");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyDemoDialog));
            this._ClearButton = new System.Windows.Forms.Button();
            this._OKButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._RecordTree = new System.Windows.Forms.TreeView();
            this._StepTree = new System.Windows.Forms.TreeView();
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._textBox2 = new System.Windows.Forms.TextBox();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._textBox3 = new System.Windows.Forms.TextBox();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ClearButton
            // 
            this._ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearButton.Location = new System.Drawing.Point(346, 366);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(87, 29);
            this._ClearButton.TabIndex = 5;
            this._ClearButton.Text = "Clear";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this.Clear);
            // 
            // _OKButton
            // 
            this._OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._OKButton.Location = new System.Drawing.Point(520, 366);
            this._OKButton.Name = "_OKButton";
            this._OKButton.Size = new System.Drawing.Size(87, 29);
            this._OKButton.TabIndex = 0;
            this._OKButton.Text = "OK";
            this._OKButton.UseVisualStyleBackColor = true;
            this._OKButton.Click += new System.EventHandler(this._OKButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(613, 366);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 29);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _RecordTree
            // 
            this._RecordTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._RecordTree.Location = new System.Drawing.Point(12, 64);
            this._RecordTree.Name = "_RecordTree";
            this._RecordTree.Size = new System.Drawing.Size(328, 173);
            this._RecordTree.TabIndex = 6;
            // 
            // _StepTree
            // 
            this._StepTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._StepTree.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._StepTree.Location = new System.Drawing.Point(346, 64);
            this._StepTree.Name = "_TreeView";
            treeNode1.Name = "_RootNode";
            treeNode1.Text = "RootNode";
            this._StepTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this._StepTree.Size = new System.Drawing.Size(354, 240);
            this._StepTree.TabIndex = 7;
            this._StepTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._TreeView_NodeMouseClick);
            // 
            // _textBox1
            // 
            this._textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox1.Location = new System.Drawing.Point(12, 12);
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.ReadOnly = true;
            this._textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox1.Size = new System.Drawing.Size(688, 46);
            this._textBox1.TabIndex = 8;
            this._textBox1.Text = "_textBox1 [ReadOnly]";
            // 
            // _textBox2
            // 
            this._textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBox2.Location = new System.Drawing.Point(12, 243);
            this._textBox2.Multiline = true;
            this._textBox2.Name = "_textBox2";
            this._textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox2.Size = new System.Drawing.Size(328, 125);
            this._textBox2.TabIndex = 9;
            this._textBox2.Text = "_textBox2";
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel1,
            this._statusLabel2});
            this._statusStrip.Location = new System.Drawing.Point(0, 406);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(712, 22);
            this._statusStrip.TabIndex = 10;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _statusLabel1
            // 
            this._statusLabel1.Name = "_statusLabel1";
            this._statusLabel1.Size = new System.Drawing.Size(21, 17);
            this._statusLabel1.Text = "OK";
            // 
            // _statusLabel2
            // 
            this._statusLabel2.Name = "_statusLabel2";
            this._statusLabel2.Size = new System.Drawing.Size(19, 17);
            this._statusLabel2.Text = "...";
            // 
            // _textBox3
            // 
            this._textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox3.Location = new System.Drawing.Point(346, 310);
            this._textBox3.Multiline = true;
            this._textBox3.Name = "_textBox3";
            this._textBox3.ReadOnly = true;
            this._textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox3.Size = new System.Drawing.Size(354, 45);
            this._textBox3.TabIndex = 11;
            this._textBox3.Text = "_textBox3 [ReadOnly]";
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(12, 372);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(328, 21);
            this._progressBar.Step = 1;
            this._progressBar.TabIndex = 12;
            // 
            // MyDemoDialog
            // 
            this.AcceptButton = this._OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(712, 428);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._textBox3);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._textBox2);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._StepTree);
            this.Controls.Add(this._RecordTree);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._OKButton);
            this.Controls.Add(this._ClearButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyDemoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DemoApplicationForm";
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ClearButton;
        private System.Windows.Forms.Button _OKButton;
        private System.Windows.Forms.Button _CancelButton;
        internal System.Windows.Forms.StatusStrip _statusStrip;
        internal System.Windows.Forms.TreeNode _TreeNode;
        internal System.Windows.Forms.TextBox _textBox3;
        private System.Windows.Forms.TreeView _RecordTree;
        private System.Windows.Forms.TreeView _StepTree;
        private System.Windows.Forms.TextBox _textBox1;
        private System.Windows.Forms.TextBox _textBox2;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel2;
        private System.Windows.Forms.ProgressBar _progressBar;
    }
}

