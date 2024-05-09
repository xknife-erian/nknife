using System;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Win.Forms.Kit.Forms
{
    public partial class ImagesPanelControlTestDockView : DockContent
    {
        public ImagesPanelControlTestDockView()
        {
            InitializeComponent();
            _PropertyGrid.SelectedObject = _ImagesPanel;
        }

        private void _FindButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var folder = dialog.SelectedPath;
                var files = Directory.GetFiles(folder, "*.jpg", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    _ImagesPanel.FillImages(file);
                }
            }
        }
    }
}
