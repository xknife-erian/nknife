using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Jeelu.SimplusD.Client.Win
{
    public class LinkMenuItem : MyMenuItem
    {
        string _linkAddress;
        public string LinkAddress
        {
            get { return _linkAddress; }
        }
        public LinkMenuItem(string text, string keyId, string linkAddress):base(text, keyId)
        {
            this._linkAddress = linkAddress;
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            Service.Workbench.NavigationUrl(this._linkAddress);
        }
    }

}
