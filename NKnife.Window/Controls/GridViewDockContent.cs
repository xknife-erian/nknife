using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Window.Controls
{
    public class GridViewDockContent : DockContent
    {
        protected ToolStripButton _AddBtn;
        protected ToolStripButton _DeleteBtn;

        protected DataGridView _Grid;
        protected ToolStripButton _ModifyBtn;
        protected ToolStripButton _SelectBtn;
        protected ToolStrip _ToolStrip;
        private ToolStripSeparator _ToolStripSeparator;

        public GridViewDockContent()
        {
            InitializeComponent();
            ShowHint = DockState.Document;
        }

        private void InitializeComponent()
        {
            var resources = new ComponentResourceManager(typeof (GridViewDockContent));
            _Grid = new DataGridView();
            _ToolStrip = new ToolStrip();
            _AddBtn = new ToolStripButton();
            _ModifyBtn = new ToolStripButton();
            _DeleteBtn = new ToolStripButton();
            _ToolStripSeparator = new ToolStripSeparator();
            _SelectBtn = new ToolStripButton();
            ((ISupportInitialize) (_Grid)).BeginInit();
            _ToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _Grid
            // 
            _Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _Grid.Dock = DockStyle.Fill;
            _Grid.Location = new Point(0, 25);
            _Grid.Name = "_Grid";
            _Grid.RowTemplate.Height = 23;
            _Grid.Size = new Size(637, 429);
            _Grid.TabIndex = 0;
            // 
            // _ToolStrip
            // 
            _ToolStrip.Items.AddRange(new ToolStripItem[]
                                          {
                                              _AddBtn,
                                              _ModifyBtn,
                                              _DeleteBtn,
                                              _ToolStripSeparator,
                                              _SelectBtn
                                          });
            _ToolStrip.Location = new Point(0, 0);
            _ToolStrip.Name = "_ToolStrip";
            _ToolStrip.Size = new Size(637, 25);
            _ToolStrip.TabIndex = 1;
            _ToolStrip.Text = "toolStrip1";
            // 
            // _AddBtn
            // 
            _AddBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _AddBtn.Image = ((Image) (resources.GetObject("_AddBtn.Image")));
            _AddBtn.ImageTransparentColor = Color.Magenta;
            _AddBtn.Size = new Size(59, 22);
            _AddBtn.Text = "新增记录";
            // 
            // _ModifyBtn
            // 
            _ModifyBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _ModifyBtn.Image = ((Image) (resources.GetObject("_ModifyBtn.Image")));
            _ModifyBtn.ImageTransparentColor = Color.Magenta;
            _ModifyBtn.Size = new Size(59, 22);
            _ModifyBtn.Text = "修改记录";
            // 
            // _DeleteBtn
            // 
            _DeleteBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _DeleteBtn.Image = ((Image) (resources.GetObject("_DeleteBtn.Image")));
            _DeleteBtn.ImageTransparentColor = Color.Magenta;
            _DeleteBtn.Size = new Size(59, 22);
            _DeleteBtn.Text = "删除记录";
            // 
            // toolStripSeparator1
            // 
            _ToolStripSeparator.Size = new Size(6, 25);
            // 
            // _SelectBtn
            // 
            _SelectBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _SelectBtn.Image = ((Image) (resources.GetObject("_SelectBtn.Image")));
            _SelectBtn.ImageTransparentColor = Color.Magenta;
            _SelectBtn.Size = new Size(59, 22);
            _SelectBtn.Text = "高级查询";
            // 
            // GridViewDockContent
            // 
            BackColor = SystemColors.Control;
            ClientSize = new Size(637, 454);
            Controls.Add(_Grid);
            Controls.Add(_ToolStrip);
            Font = new Font("Tahoma", 8.25F);
            Name = "GridViewDockContent";
            ((ISupportInitialize) (_Grid)).EndInit();
            _ToolStrip.ResumeLayout(false);
            _ToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}