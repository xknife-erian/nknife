using System;
using System.Windows.Forms;

namespace NKnife.Tools.Robot.CubeOctopus
{
    public partial class DistinguishCubeSurfaceForm : Form
    {
        //private Camera _Camera;

        public DistinguishCubeSurfaceForm()
        {
            InitializeComponent();
            _PlayButton.Click += PlayButtonClick;
            _StopButton.Click += StopButtonClick;
        }

        protected override void OnLoad(EventArgs e)
        {
            _PlayButton.Enabled = false;
            _StopButton.Enabled = true;
            //_Camera = new Camera(_VedioPanel.Handle, _VedioPanel.Width, _VedioPanel.Height);
            //_Camera.StartWebCam();
        }

        private void PlayButtonClick(object sender, System.EventArgs e)
        {
            _PlayButton.Enabled = false;
            _StopButton.Enabled = true;
            //_Camera = new Camera(_VedioPanel.Handle, _VedioPanel.Width, _VedioPanel.Height);
            //_Camera.StartWebCam();
        }

        private void StopButtonClick(object sender, System.EventArgs e)
        {
            _PlayButton.Enabled = true;
            _StopButton.Enabled = false;
            //_Camera.CloseWebcam();
        }
    }
}
