using System.Windows.Forms;
using System;
namespace PageCreator.Demo
{
    partial class Demo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.outXmlTextBox = new PageCreator.Demo.QuickTextBox();
            this.codeTextBox = new PageCreator.Demo.QuickTextBox();
            this.tree = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tmpltComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.pageComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.快速测试F5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sdsiteLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 49);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tree);
            this.splitContainer2.Size = new System.Drawing.Size(680, 382);
            this.splitContainer2.SplitterDistance = 427;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.outXmlTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.codeTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(427, 382);
            this.splitContainer1.SplitterDistance = 196;
            this.splitContainer1.TabIndex = 0;
            // 
            // outXmlTextBox
            // 
            this.outXmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outXmlTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.outXmlTextBox.Location = new System.Drawing.Point(0, 0);
            this.outXmlTextBox.MaxLength = 640000;
            this.outXmlTextBox.Multiline = true;
            this.outXmlTextBox.Name = "outXmlTextBox";
            this.outXmlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outXmlTextBox.Size = new System.Drawing.Size(196, 382);
            this.outXmlTextBox.TabIndex = 1;
            // 
            // codeTextBox
            // 
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.codeTextBox.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox.MaxLength = 640000;
            this.codeTextBox.Multiline = true;
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.codeTextBox.Size = new System.Drawing.Size(227, 382);
            this.codeTextBox.TabIndex = 0;
            // 
            // tree
            // 
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.Location = new System.Drawing.Point(0, 0);
            this.tree.Name = "tree";
            this.tree.ShowNodeToolTips = true;
            this.tree.Size = new System.Drawing.Size(249, 382);
            this.tree.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tmpltComboBox,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.pageComboBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "模板标题:";
            // 
            // tmpltComboBox
            // 
            this.tmpltComboBox.DropDownHeight = 300;
            this.tmpltComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tmpltComboBox.IntegralHeight = false;
            this.tmpltComboBox.Name = "tmpltComboBox";
            this.tmpltComboBox.Size = new System.Drawing.Size(240, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel2.Text = "页面标题:";
            // 
            // pageComboBox
            // 
            this.pageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pageComboBox.Name = "pageComboBox";
            this.pageComboBox.Size = new System.Drawing.Size(240, 25);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.快速测试F5ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(680, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.toolStripSeparator2,
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建NToolStripMenuItem
            // 
            this.新建NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripMenuItem.Image")));
            this.新建NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
            this.新建NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建NToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.新建NToolStripMenuItem.Text = "新建(&N)";
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
            this.打开OToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            this.退出XToolStripMenuItem.Click += new System.EventHandler(this.退出XToolStripMenuItem_Click);
            // 
            // 快速测试F5ToolStripMenuItem
            // 
            this.快速测试F5ToolStripMenuItem.Name = "快速测试F5ToolStripMenuItem";
            this.快速测试F5ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.快速测试F5ToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.快速测试F5ToolStripMenuItem.Text = "快速测试(F5)";
            this.快速测试F5ToolStripMenuItem.Click += new System.EventHandler(this.快速测试F5ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdsiteLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 431);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(680, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sdsiteLabel
            // 
            this.sdsiteLabel.Name = "sdsiteLabel";
            this.sdsiteLabel.Size = new System.Drawing.Size(109, 17);
            this.sdsiteLabel.Text = "toolStripStatusLabel1";
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 453);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Demo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DemoForm";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private QuickTextBox codeTextBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件FToolStripMenuItem;
        private ToolStripMenuItem 新建NToolStripMenuItem;
        private ToolStripMenuItem 打开OToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 退出XToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem 快速测试F5ToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripComboBox tmpltComboBox;
        private ToolStripStatusLabel sdsiteLabel;
        private SplitContainer splitContainer2;
        private QuickTextBox outXmlTextBox;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private SplitContainer splitContainer1;
        private TreeView tree;
        private ToolStripLabel toolStripLabel2;
        private ToolStripComboBox pageComboBox;
    }

    /// <summary>
    /// KvnluSoft：一个已设置为多行显示的TextBox，有简单的copy,paste等右键菜单
    /// </summary>
    public class QuickTextBox : TextBox
    {
        ContextMenuStrip _contextMenuStrip;
        ToolStripMenuItem _toolStripMenuItem;

        public QuickTextBox()
        {
            this.SuspendLayout();

            this.Multiline = true;
            this.MaxLength = 640000;
            this.ScrollBars = ScrollBars.Vertical;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            _contextMenuStrip = new ContextMenuStrip();
            _toolStripMenuItem = new ToolStripMenuItem("全选(&A)");
            _toolStripMenuItem.Click += new EventHandler(SelectAllEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("拷贝(&C)");
            _toolStripMenuItem.Click += new EventHandler(CopyEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("粘贴(&P)");
            _toolStripMenuItem.Click += new EventHandler(PasteEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            _toolStripMenuItem = new ToolStripMenuItem("剪切(&T)");
            _toolStripMenuItem.Click += new EventHandler(CutEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            ToolStripSeparator separator = new ToolStripSeparator();
            _contextMenuStrip.Items.Add(separator);
            _toolStripMenuItem = new ToolStripMenuItem("还原(&F)");
            _toolStripMenuItem.Click += new EventHandler(ClearEx);
            _contextMenuStrip.Items.Add(_toolStripMenuItem);
            this.ContextMenuStrip = _contextMenuStrip;
            this.ResumeLayout(false);

        }
        private void PasteEx(object sender, EventArgs e)
        {
            this.Paste();
        }
        private void CutEx(object sender, EventArgs e)
        {
            this.Cut();
        }
        private void ClearEx(object sender, EventArgs e)
        {
            this.Clear();
        }
        private void SelectAllEx(object sender, EventArgs e)
        {
            this.SelectAll();
        }
        private void CopyEx(object sender, EventArgs e)
        {
            this.Copy();
        }
    }
}

