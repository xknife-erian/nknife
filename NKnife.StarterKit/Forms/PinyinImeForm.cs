using System;
using System.Text;
using System.Windows.Forms;

namespace NKnife.StarterKit.Forms
{
    public partial class PinyinImeForm : Form
    {
        public PinyinImeForm()
        {
            InitializeComponent();
        }

        private void PinyinInput_TextChanged(object sender, EventArgs e)
        {
//            char[] cs = Pinyin.GetCharArrayOfPinyin(((Control) sender).Text);
//            var sb = new StringBuilder();
//            foreach (char c in cs)
//            {
//                sb.Append(c).Append('.');
//            }
//            _ChineseCharTextbox.Text = sb.ToString();
        }

        private void _ClearButton_Click(object sender, EventArgs e)
        {
            _ChineseCharTextbox.Clear();
            _PinyinTextbox.Clear();
        }
    }
}