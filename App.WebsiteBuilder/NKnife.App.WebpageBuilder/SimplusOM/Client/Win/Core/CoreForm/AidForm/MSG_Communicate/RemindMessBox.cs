using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class RemindMessBox : Form
    {
        public RemindMessBox(string msgText)
        {
            InitializeComponent();

            textBox1.Text = msgText;
        }
    }
}
