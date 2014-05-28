using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Window.Common
{
    public class GridViewDockContent : DockContent
    {
        public GridViewDockContent()
        {
            InitializeComponent();
            ShowHint = DockState.Document;
        }

        private void InitializeComponent()
        {
            _Grid = new DataGridView();
            ((ISupportInitialize)(_Grid)).BeginInit();
            SuspendLayout();

            _Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _Grid.Dock = DockStyle.Fill;
            _Grid.Location = new Point(0, 0);
            _Grid.Name = "_Grid";
            _Grid.RowTemplate.Height = 23;
            _Grid.Size = new Size(637, 454);
            _Grid.TabIndex = 0;

            BackColor = SystemColors.Control;
            ClientSize = new Size(637, 454);
            Controls.Add(_Grid);
            Font = new Font("Tahoma", 8.25F);
            ((ISupportInitialize)(_Grid)).EndInit();
            ResumeLayout(false);
        }

        protected DataGridView _Grid;

    }
}
