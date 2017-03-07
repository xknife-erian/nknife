using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm
{
    public class ToolStripDateTimePicker : ToolStripControlHost
    {
        public ToolStripDateTimePicker()
            : base(new DateTimePicker())
        {
        }

        public DateTimePicker DateTimePicker => Control as DateTimePicker;

        public bool ToolStripDateTimePickerEnabled
        {
            get { return DateTimePicker.Enabled; }
            set { DateTimePicker.Enabled = value; }
        }
    }
}
