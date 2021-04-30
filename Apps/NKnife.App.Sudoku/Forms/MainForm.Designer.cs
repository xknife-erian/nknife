using NKnife.App.Sudoku.Controls;
using NKnife.GUI.WinForm;

namespace NKnife.App.Sudoku.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._OpenDoXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._NewExerciseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._MainToolStrip = new System.Windows.Forms.ToolStrip();
            this._OpenDoXmlToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this._NewExerciseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.打印PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.帮助LToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._MainTabControl = new System.Windows.Forms.TabControl();
            this._SudokuManageTabPage = new System.Windows.Forms.TabPage();
            this._ExerciseTree = new ExerciseTree();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this._RuntimeTabPage = new System.Windows.Forms.TabPage();
            this.numericLedBox1 = new NumericLedBox();
            this.menuStrip1.SuspendLayout();
            this._MainToolStrip.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this._MainTabControl.SuspendLayout();
            this._SudokuManageTabPage.SuspendLayout();
            this._RuntimeTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLedBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.关于AToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this._OpenDoXmlToolStripMenuItem,
            this._Open,
            this.toolStripMenuItem5,
            this.toolStripSeparator2,
            this._NewExerciseToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem1.Text = "新建题库(&N)";
            // 
            // _OpenDoXmlToolStripMenuItem
            // 
            this._OpenDoXmlToolStripMenuItem.Name = "_OpenDoXmlToolStripMenuItem";
            this._OpenDoXmlToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this._OpenDoXmlToolStripMenuItem.Text = "打开题库(&O)";
            this._OpenDoXmlToolStripMenuItem.Click += new System.EventHandler(this._OpenDoXmlToolStripMenuItem_Click);
            // 
            // _Open
            // 
            this._Open.Name = "_Open";
            this._Open.Size = new System.Drawing.Size(158, 22);
            this._Open.Text = "打开题库(&O)";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem5.Text = "保存题库(&S)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // _NewExerciseToolStripMenuItem
            // 
            this._NewExerciseToolStripMenuItem.Name = "_NewExerciseToolStripMenuItem";
            this._NewExerciseToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this._NewExerciseToolStripMenuItem.Text = "新建题目(&E)";
            this._NewExerciseToolStripMenuItem.Click += new System.EventHandler(this._NewExerciseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem3.Text = "批量导入题目(&I)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 22);
            this.toolStripSeparator4.Text = "打印(&P)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(155, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AToolStripMenuItem1});
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.关于AToolStripMenuItem.Text = "关于(&A)";
            // 
            // 关于AToolStripMenuItem1
            // 
            this.关于AToolStripMenuItem1.Name = "关于AToolStripMenuItem1";
            this.关于AToolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.关于AToolStripMenuItem1.Text = "关于(&A)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _MainToolStrip
            // 
            this._MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OpenDoXmlToolStripButton,
            this.保存SToolStripButton,
            this.toolStripSeparator6,
            this._NewExerciseToolStripButton,
            this.toolStripSeparator7,
            this.打印PToolStripButton,
            this.toolStripSeparator,
            this.帮助LToolStripButton});
            this._MainToolStrip.Location = new System.Drawing.Point(0, 24);
            this._MainToolStrip.Name = "_MainToolStrip";
            this._MainToolStrip.Size = new System.Drawing.Size(792, 25);
            this._MainToolStrip.TabIndex = 3;
            this._MainToolStrip.Text = "MainToolStrip";
            // 
            // _OpenDoXmlToolStripButton
            // 
            this._OpenDoXmlToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._OpenDoXmlToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_OpenDoXmlToolStripButton.Image")));
            this._OpenDoXmlToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OpenDoXmlToolStripButton.Name = "_OpenDoXmlToolStripButton";
            this._OpenDoXmlToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._OpenDoXmlToolStripButton.Text = "打开(&O)";
            this._OpenDoXmlToolStripButton.Click += new System.EventHandler(this._OpenDoXmlToolStripButton_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // _NewExerciseToolStripButton
            // 
            this._NewExerciseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._NewExerciseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_NewExerciseToolStripButton.Image")));
            this._NewExerciseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._NewExerciseToolStripButton.Name = "_NewExerciseToolStripButton";
            this._NewExerciseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._NewExerciseToolStripButton.Text = "新建(&N)";
            this._NewExerciseToolStripButton.Click += new System.EventHandler(this._NewExerciseToolStripButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // 打印PToolStripButton
            // 
            this.打印PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打印PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打印PToolStripButton.Image")));
            this.打印PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打印PToolStripButton.Name = "打印PToolStripButton";
            this.打印PToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打印PToolStripButton.Text = "打印(&P)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // 帮助LToolStripButton
            // 
            this.帮助LToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.帮助LToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("帮助LToolStripButton.Image")));
            this.帮助LToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.帮助LToolStripButton.Name = "帮助LToolStripButton";
            this.帮助LToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.帮助LToolStripButton.Text = "帮助(&L)";
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(0, 49);
            this._SplitContainer.Name = "_SplitContainer";
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this._MainTabControl);
            this._SplitContainer.Size = new System.Drawing.Size(792, 502);
            this._SplitContainer.SplitterDistance = 596;
            this._SplitContainer.TabIndex = 4;
            // 
            // _MainTabControl
            // 
            this._MainTabControl.Controls.Add(this._SudokuManageTabPage);
            this._MainTabControl.Controls.Add(this._RuntimeTabPage);
            this._MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainTabControl.Location = new System.Drawing.Point(0, 0);
            this._MainTabControl.Name = "_MainTabControl";
            this._MainTabControl.SelectedIndex = 0;
            this._MainTabControl.Size = new System.Drawing.Size(192, 502);
            this._MainTabControl.TabIndex = 0;
            // 
            // _SudokuManageTabPage
            // 
            this._SudokuManageTabPage.Controls.Add(this._ExerciseTree);
            this._SudokuManageTabPage.Controls.Add(this.statusStrip2);
            this._SudokuManageTabPage.Controls.Add(this.toolStrip2);
            this._SudokuManageTabPage.Location = new System.Drawing.Point(4, 22);
            this._SudokuManageTabPage.Name = "_SudokuManageTabPage";
            this._SudokuManageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._SudokuManageTabPage.Size = new System.Drawing.Size(184, 476);
            this._SudokuManageTabPage.TabIndex = 0;
            this._SudokuManageTabPage.Text = "Manage";
            this._SudokuManageTabPage.UseVisualStyleBackColor = true;
            // 
            // _ExerciseTree
            // 
            this._ExerciseTree.AllowDrop = true;
            this._ExerciseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ExerciseTree.HideSelection = false;
            this._ExerciseTree.Location = new System.Drawing.Point(3, 28);
            this._ExerciseTree.Name = "_ExerciseTree";
            this._ExerciseTree.Size = new System.Drawing.Size(178, 423);
            this._ExerciseTree.TabIndex = 3;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Location = new System.Drawing.Point(3, 451);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(178, 22);
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(178, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // _RuntimeTabPage
            // 
            this._RuntimeTabPage.Controls.Add(this.numericLedBox1);
            this._RuntimeTabPage.Location = new System.Drawing.Point(4, 21);
            this._RuntimeTabPage.Name = "_RuntimeTabPage";
            this._RuntimeTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._RuntimeTabPage.Size = new System.Drawing.Size(184, 477);
            this._RuntimeTabPage.TabIndex = 1;
            this._RuntimeTabPage.Text = "Runtime";
            this._RuntimeTabPage.UseVisualStyleBackColor = true;
            // 
            // numericLedBox1
            // 
            this.numericLedBox1.BackColor = System.Drawing.Color.Transparent;
            this.numericLedBox1.BackColor_1 = System.Drawing.Color.Black;
            this.numericLedBox1.BackColor_2 = System.Drawing.Color.DimGray;
            this.numericLedBox1.BevelRate = 0.5F;
            this.numericLedBox1.FadedColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.numericLedBox1.ForeColor = System.Drawing.Color.LightGreen;
            this.numericLedBox1.HighlightOpaque = ((byte)(50));
            this.numericLedBox1.Location = new System.Drawing.Point(6, 35);
            this.numericLedBox1.Name = "numericLedBox1";
            this.numericLedBox1.Size = new System.Drawing.Size(172, 34);
            this.numericLedBox1.TabIndex = 0;
            this.numericLedBox1.Text = "35.34.0000";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this._SplitContainer);
            this.Controls.Add(this._MainToolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aarhus.Front.Sudoku";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._MainToolStrip.ResumeLayout(false);
            this._MainToolStrip.PerformLayout();
            this._SplitContainer.Panel2.ResumeLayout(false);
            this._SplitContainer.ResumeLayout(false);
            this._MainTabControl.ResumeLayout(false);
            this._SudokuManageTabPage.ResumeLayout(false);
            this._SudokuManageTabPage.PerformLayout();
            this._RuntimeTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericLedBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip _MainToolStrip;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.TabControl _MainTabControl;
        private System.Windows.Forms.TabPage _SudokuManageTabPage;
        private System.Windows.Forms.TabPage _RuntimeTabPage;
        private System.Windows.Forms.ToolStripButton _NewExerciseToolStripButton;
        private System.Windows.Forms.ToolStripButton _OpenDoXmlToolStripButton;
        private System.Windows.Forms.ToolStripButton 保存SToolStripButton;
        private System.Windows.Forms.ToolStripButton 打印PToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton 帮助LToolStripButton;
        private NumericLedBox numericLedBox1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem _NewExerciseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem _OpenDoXmlToolStripMenuItem;
        private ExerciseTree _ExerciseTree;
	}
}
