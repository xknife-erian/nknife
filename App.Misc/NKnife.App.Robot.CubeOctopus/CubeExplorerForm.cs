using System;
using System.Windows.Forms;
using NKnife.Utility;

namespace NKnife.Tools.Robot.CubeOctopus
{
    public partial class CubeExplorerForm : Form
    {
        public CubeExplorerForm()
        {
            InitializeComponent();
        }

        private void LeftCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.LeftCW90();
        }

        private void LeftCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.LeftCCW90();
        }

        private void RightCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.RightCCW90();
        }

        private void RightCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.RightCW90();
        }

        private void UpCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.UpCW90();
        }

        private void UpCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.UpCCW90();
        }

        private void DownCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.DownCW90();
        }

        private void DwonCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.DownCCW90();
        }

        private void FrontCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.FrontCW90();
        }

        private void FrontCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.FrontCCW90();
        }

        private void BackCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.BackCW90();
        }

        private void BackCCW90ButtonClick(object sender, EventArgs e)
        {
            _CubePaper.BackCCW90();
        }

        private readonly UtilityRandom _Random = new UtilityRandom();
        private void RandomButtonClick(object sender, EventArgs e)
        {
            _RandomStepTextbox.Text = string.Empty;
            _CubePaper.Initialize();

            for (int i = 0; i < _RandomStepNumber.Value; i++)
            {
                var m = _Random.Next(1, 13);
                switch (m)
                {
                    case 1:
                        _CubePaper.BackCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "B'");
                        break;
                    case 2:
                        _CubePaper.BackCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "B");
                        break;
                    case 3:
                        _CubePaper.DownCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "D'");
                        break;
                    case 4:
                        _CubePaper.DownCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "D");
                        break;
                    case 5:
                        _CubePaper.FrontCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "F'");
                        break;
                    case 6:
                        _CubePaper.FrontCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "F'");
                        break;
                    case 7:
                        _CubePaper.UpCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "U'");
                        break;
                    case 8:
                        _CubePaper.UpCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "U");
                        break;
                    case 9:
                        _CubePaper.LeftCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "L'");
                        break;
                    case 10:
                        _CubePaper.LeftCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "L");
                        break;
                    case 11:
                        _CubePaper.RightCCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "R'");
                        break;
                    case 12:
                        _CubePaper.RightCW90();
                        _RandomStepTextbox.Text = string.Format("{0}{1}", _RandomStepTextbox.Text, "R");
                        break;
                    default:
                        break;
                }
                _RandomStepTextbox.Refresh();
            }
        }

        private void InitButtonClick(object sender, EventArgs e)
        {
            _CubePaper.Initialize();
        }
    }
}
