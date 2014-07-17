﻿using System.Windows.Forms;
using NKnife.DockForm.Views.MdiForms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.DockForm.Views
{
    public sealed partial class Workbench : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        public Workbench()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Cursor = Cursors.WaitCursor;
            InitializeDockPanel();
            Cursor = Cursors.Default;
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();
        }

        public void AddMenuItem(params ToolStripItem[] items)
        {
            _MenuStrip.Items.AddRange(items);
        }
    }
}