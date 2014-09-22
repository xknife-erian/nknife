using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    public class UserGeneralForm : MdiWebBrowserViewForm
    {
        private DataForScriptInUserOption _dataForScript;
        public DataForScriptInUserOption DataForScript
        {
            get { return _dataForScript; }
        }

        public UserGeneralForm()
        {
            this.MinimumSize = new System.Drawing.Size(440,350);

            _dataForScript = new DataForScriptInUserOption(this);
            _webBrowser.ObjectForScripting = _dataForScript;
        }
    }
}
