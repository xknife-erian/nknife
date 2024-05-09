using System.Windows.Forms;

namespace NKnife.Win.Forms
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
