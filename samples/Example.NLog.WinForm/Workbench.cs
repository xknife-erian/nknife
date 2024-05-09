using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.NLog.WinForm.Example.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.NLog.WinForm.Example
{
    public partial class Workbench : Form
    {
        private DockPanel _dockPanel;

        public Workbench()
        {
            InitializeComponent();
            ShowIcon = true;
            Icon = Resources.form;

            InitializeDockPanel();
        }

        private void InitializeDockPanel()
        {
            _dockPanel = new DockPanel {Theme = new VS2015BlueTheme()};
            Controls.Add(_dockPanel);
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.BringToFront();
            _dockPanel.DockLeftPortion = 280;
            _dockPanel.DockBottomPortion = 230;

            BuildPanels(2, DockState.DockBottom);
            BuildPanels(1, DockState.DockRight);
            BuildPanels(1, DockState.Document);

            var toolboxView = new LeftToolboxView();
            toolboxView.Show(_dockPanel, DockState.DockLeft);
        }

        private void BuildPanels(int count, DockState state)
        {
            for (int i = 0; i < count; i++)
            {
                var view = new LogPanelTestView();
                view.Text = $"View{i + 1}";
                view.Show(_dockPanel, state);
            }
        }
    }
}
