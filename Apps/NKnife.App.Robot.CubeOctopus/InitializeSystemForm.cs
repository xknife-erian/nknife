using System;
using System.Windows.Forms;
using NKnife.Tools.Robot.CubeOctopus.Base;

namespace NKnife.Tools.CubeOctopus
{
    public partial class InitializeSystemForm : Form
    {
        private RobotCommandant _Commandant;

        public InitializeSystemForm()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_Commandant != null)
                _Commandant.Stop();
        }

        private void OpenSerialPortButtonClick(object sender, EventArgs e)
        {
            _Commandant = new RobotCommandant();
            _Commandant.Initialize(3);

            InitCommandantToControls(this);
        }

        private void InitCommandantToControls(Control ctr)
        {
            var controlPanel = ctr as MechanicalArmsControlPanel;
            if (controlPanel != null)
            {
                MechanicalArmsControlPanel panel = controlPanel;
                panel.SetCommandant(_Commandant);
            }
            if (ctr.Controls.Count <= 0)
            {
                return;
            }
            foreach (Control subCtr in ctr.Controls)
            {
                InitCommandantToControls(subCtr);
            }
        }
    }
}