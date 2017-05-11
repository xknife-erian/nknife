using System.Windows.Forms;

namespace NKnife.Draws.WinForm
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
    }
}