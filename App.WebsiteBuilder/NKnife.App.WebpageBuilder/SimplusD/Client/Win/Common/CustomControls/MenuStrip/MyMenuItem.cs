using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class MyMenuItem : ToolStripMenuItem
    {
        string _keyId;

        public string KeyId
        {
            get { return _keyId; }
        }

        ToolStripItem _toolStripItem;
        public ToolStripItem ToolStripItem
        {
            get { return _toolStripItem; }
            set { _toolStripItem = value; }
        }

        private bool _isMustOpenSite;
        public bool IsMustOpenSite
        {
            get { return _isMustOpenSite; }
        }

        public MyMenuItem(string text, string keyId,bool isMustOpenSite)
            : base(text)
        {
            this._isMustOpenSite = isMustOpenSite;
            this._keyId = keyId;
        }
        public MyMenuItem(string text, string keyId)
            : this(text, keyId, false)
        {
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            //�˵���Enabled���Ըı�ʱ������й�����ToolStripItem����ı���Enalbed����
            if (_toolStripItem != null)
            {
                _toolStripItem.Enabled = base.Enabled;
            }

            base.OnEnabledChanged(e);
        }
    }
}
