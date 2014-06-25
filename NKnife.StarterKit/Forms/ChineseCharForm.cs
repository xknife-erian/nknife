using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.International.Converters.PinYinConverter;

namespace NKnife.StarterKit.Forms
{
    public partial class ChineseCharForm : Form
    {
        public ChineseCharForm()
        {
            InitializeComponent();
        }

        private void PinyinInput_TextChanged(object sender, EventArgs e)
        {
            var chars = ChineseChar.GetChars(_PinyinTextbox.Text);
            if (chars == null)
            {
                return;
            }
            var sb = new StringBuilder();
            foreach (var c in chars)
            {
                sb.Append(c).Append(',');
            }
            _ChineseCharTextbox.Text = sb.ToString();

        }

        private void _ClearButton_Click(object sender, EventArgs e)
        {
            _ChineseCharTextbox.Clear();
            _PinyinTextbox.Clear();
        }
    }
}
