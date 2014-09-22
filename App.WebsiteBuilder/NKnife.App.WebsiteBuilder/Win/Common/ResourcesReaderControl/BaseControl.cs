using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class BaseControl : Control
    {
        public BaseControl()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (!this.DesignMode)
            {
                ResourcesReader.SetControlPropertyHelper(this);
            }
        }

        #region ResourcesReader
        public string GetText(string key)
        {
            return ResourcesReader.GetText(key, this);
        }

        public Icon GetIcon(string key)
        {
            return ResourcesReader.GetIcon(key, this);
        }

        public Image GetImage(string key)
        {
            return ResourcesReader.GetImage(key, this);
        }

        public Cursor GetCursor(string key)
        {
            return ResourcesReader.GetCursor(key, this);
        }

        public byte[] GetBytes(string key)
        {
            return ResourcesReader.GetBytes(key, this);
        }
        #endregion
    }
}
