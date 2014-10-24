using System;
using System.IO;
using System.Windows.Forms;

namespace NKnife.Kits.StarterKit.Forms
{
    public partial class ImagesPanelDemo : Form
    {
        public ImagesPanelDemo()
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
