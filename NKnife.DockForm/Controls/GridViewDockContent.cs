using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.DockForm.Controls
{
    public class GridViewDockContent : DockContent
    {
        protected ToolStripButton _AddButton;
        protected ToolStripButton _DeleteButton;

        protected DataGridView _Grid;
        protected ToolStripButton _ModifyButton;
        protected ToolStripButton _SelectButton;
        protected ToolStrip _ToolStrip;
        private ToolStripSeparator _ToolStripSeparator;

        public GridViewDockContent()
        {
            InitializeComponent();
            ShowHint = DockState.Document;
        }

        private void InitializeComponent()
        {
            this._Grid = new System.Windows.Forms.DataGridView();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._AddButton = new System.Windows.Forms.ToolStripButton();
            this._ModifyButton = new System.Windows.Forms.ToolStripButton();
            this._DeleteButton = new System.Windows.Forms.ToolStripButton();
            this._ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this._SelectButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this._Grid)).BeginInit();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Grid
            // 
            this._Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Grid.Location = new System.Drawing.Point(0, 25);
            this._Grid.Name = "_Grid";
            this._Grid.RowTemplate.Height = 23;
            this._Grid.Size = new System.Drawing.Size(637, 429);
            this._Grid.TabIndex = 0;
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._AddButton,
            this._ModifyButton,
            this._DeleteButton,
            this._ToolStripSeparator,
            this._SelectButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(637, 25);
            this._ToolStrip.TabIndex = 1;
            // 
            // _AddButton
            // 
            this._AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._AddButton.Name = "_AddButton";
            this._AddButton.Size = new System.Drawing.Size(60, 22);
            this._AddButton.Text = "新增记录";
            // 
            // _ModifyButton
            // 
            this._ModifyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ModifyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ModifyButton.Name = "_ModifyButton";
            this._ModifyButton.Size = new System.Drawing.Size(60, 22);
            this._ModifyButton.Text = "修改记录";
            // 
            // _DeleteButton
            // 
            this._DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._DeleteButton.Name = "_DeleteButton";
            this._DeleteButton.Size = new System.Drawing.Size(60, 22);
            this._DeleteButton.Text = "删除记录";
            // 
            // _ToolStripSeparator
            // 
            this._ToolStripSeparator.Name = "_ToolStripSeparator";
            this._ToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // _SelectButton
            // 
            this._SelectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._SelectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SelectButton.Name = "_SelectButton";
            this._SelectButton.Size = new System.Drawing.Size(60, 22);
            this._SelectButton.Text = "高级查询";
            // 
            // GridViewDockContent
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(637, 454);
            this.Controls.Add(this._Grid);
            this.Controls.Add(this._ToolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "GridViewDockContent";
            ((System.ComponentModel.ISupportInitialize)(this._Grid)).EndInit();
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}