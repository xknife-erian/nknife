using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.KeywordResonator.Client
{
    internal partial class RichProgressForm : BaseForm
    {
        internal RichProgressForm()
        {
            InitializeComponent();
        }

        internal string Description { get; set; }
        internal string MasterLabel { get; set; }
        internal string FollowLabel { get; set; }

    }
}
