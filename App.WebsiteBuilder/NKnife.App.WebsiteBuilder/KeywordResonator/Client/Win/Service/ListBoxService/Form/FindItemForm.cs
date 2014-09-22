using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    public partial class FindItemForm : Form
    {
        private KeywordListView _checkListBox;
        public FindItemForm(KeywordListView checkListBox)
        {
            InitializeComponent();
            _checkListBox = checkListBox;
        }

        private void btn_FindNext_Click(object sender, EventArgs e)
        {
            _checkListBox.FindCheckBoxItem(txt_Source.Text.Trim(), chk_MatchWord.Checked, chk_CaseSensitive.Checked, true);
        }

        private void btn_FindPrev_Click(object sender, EventArgs e)
        {
            _checkListBox.FindCheckBoxItem(txt_Source.Text.Trim(), chk_MatchWord.Checked, chk_CaseSensitive.Checked, false);
        }

        private void FindItemForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
