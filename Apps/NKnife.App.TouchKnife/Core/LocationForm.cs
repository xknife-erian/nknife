using System;
using System.Text;
using System.Windows.Forms;
using NKnife.Utility;

namespace NKnife.App.TouchInputKnife.Core
{
    public partial class LocationForm : Form
    {
        private readonly int _Mode;
        public LocationForm(int mode)
        {
            _Mode = mode;
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var w = Screen.PrimaryScreen.WorkingArea.Width;
            Left = w - Width-3;
            var h = Screen.PrimaryScreen.WorkingArea.Height;
            Top = h - Height - 3;
        }

        public string Command { get; set; }

        private void _OkButton_Click(object sender, EventArgs e)
        {
            var x = ((int)_XNumericUpDown.Value).ToString();
            var y = ((int)_YNumericUpDown.Value).ToString();
            var sb = new StringBuilder();
            for (int i = y.Length - 1; i >= 0; i--)
            {
                sb.Append(y[i]);
            }
            y = sb.ToString();

            sb.Clear();
            var random = new UtilityRandom();

            sb.Append(random.Next(1, 9));
            sb.Append(_Mode);
            sb.Append(random.Next(1, 9));
            sb.Append(x.Length);
            sb.Append(random.Next(1, 9));
            sb.Append(y.Length);
            sb.Append(random.Next(1, 9));
            sb.Append(x.PadRight(4, random.Next(1, 9).ToString()[0]));
            sb.Append(random.Next(1, 9));
            sb.Append(y.PadLeft(4, random.Next(1, 9).ToString()[0]));

            sb.Append('`');

            Command = sb.ToString();
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
