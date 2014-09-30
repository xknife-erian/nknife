using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Utility.File;

namespace NKnife.StarterKit.Forms
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
