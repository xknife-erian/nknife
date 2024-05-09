using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.NLog.WinForm.Example.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.NLog.WinForm.Example
{
    public partial class LogPanelTestView : DockContent
    {
        public LogPanelTestView()
        {
            InitializeComponent();
            ShowIcon = true;
            Icon = Resources.sub_form;
        }
    }
}
