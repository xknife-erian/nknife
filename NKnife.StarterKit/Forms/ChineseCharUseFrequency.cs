using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NKnife.Chinese;
using NKnife.Chinese.Common;

namespace NKnife.StarterKit.Forms
{
    public partial class ChineseCharUseFrequency : Form
    {
        public ChineseCharUseFrequency()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Fre1TextBox.Text = "";
            _TimeLabel.Text = "";
            if (!String.IsNullOrWhiteSpace(_CahrTextBox.Text))
            {
                var sw = new Stopwatch();
                sw.Start();
                int i = SimplifyChineseChar.IndexOf(_CahrTextBox.Text[0].ToString(CultureInfo.InvariantCulture));
                long time = sw.ElapsedMilliseconds;
                sw.Stop();
                _Fre1TextBox.Text = i.ToString(CultureInfo.InvariantCulture);
                _TimeLabel.Text = time.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _Fre2TextBox.Text = "";
            _TimeLabel.Text = "";
            if (!String.IsNullOrWhiteSpace(_StringTextbox.Text))
            {
                var sb = new StringBuilder();
                var ss = _StringTextbox.Text;
                var sw = new Stopwatch();
                sw.Start();
                foreach (var s in ss)
                {
                    int i = SimplifyChineseChar.IndexOf(s.ToString(CultureInfo.InvariantCulture));
                    sb.Append(s).Append(i).Append(';');
                }
                long time = sw.ElapsedMilliseconds;
                sw.Stop();
                _Fre2TextBox.Text = sb.ToString();
                _TimeLabel.Text = time.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
