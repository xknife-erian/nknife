using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class UserRegForm : MdiWebBrowserViewForm
    {
        private DataForScriptInUserOption _dataForScript;
        public DataForScriptInUserOption DataForScript
        {
            get { return _dataForScript; }
        }

        public UserRegForm()
            : base()
        {
            _dataForScript = new DataForScriptInUserOption(this);
            InitializeComponent();
            _webBrowser.ObjectForScripting = DataForScript;
        }

    }
}