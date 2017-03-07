using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.Kits.StarterKit.Forms
{
    public partial class UserToolStripForm : Form
    {
        public UserToolStripForm()
        {
            InitializeComponent();
            _Checkbox.CheckBox.CheckedChanged += (s, e) => checkBox1.Checked = _Checkbox.CheckBox.Checked;
            _DateTimePicker.DateTimePicker.ValueChanged += (s, e) => textBox1.Text = _DateTimePicker.DateTimePicker.Value.ToString();
        }
    }
}
