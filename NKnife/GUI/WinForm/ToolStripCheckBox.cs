using System;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class ToolStripCheckBox : ToolStripControlHost
    {
        public ToolStripCheckBox()
            : base(new CheckBox())
        {
        }

        public CheckBox CheckBox => Control as CheckBox;

        public bool ToolStripCheckBoxEnabled
        {
            get { return CheckBox.Enabled; }
            set { CheckBox.Enabled = value; }
        }

        protected override void OnSubscribeControlEvents(Control c)
        {
            // Call the base method so the basic events are unsubscribed. 
            base.OnSubscribeControlEvents(c);
            var control = (CheckBox) c; // Remove the event. 
            control.CheckedChanged += CheckedChanged;
        }

        protected override void OnUnsubscribeControlEvents(Control c)
        {
            // Call the base method so the basic events are unsubscribed. 
            base.OnUnsubscribeControlEvents(c);
            var control = (CheckBox) c; // Remove the event. 
            control.CheckedChanged -= CheckedChanged;
        }

        public event EventHandler CheckedChanged;

        protected void OnCheckChanged(object sender, DateRangeEventArgs e)
        {
            CheckedChanged?.Invoke(this, e);
        }
    }
}