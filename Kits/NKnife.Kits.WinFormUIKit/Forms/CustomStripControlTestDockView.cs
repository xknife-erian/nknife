using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.WinFormUIKit.Forms
{
    public partial class CustomStripControlTestDockView : DockContent
    {
        public CustomStripControlTestDockView()
        {
            InitializeComponent();
            _Checkbox.CheckBox.CheckedChanged += (s, e) => checkBox1.Checked = _Checkbox.CheckBox.Checked;
            _DateTimePicker.DateTimePicker.ValueChanged += (s, e) => textBox1.Text = _DateTimePicker.DateTimePicker.Value.ToString();
        }
    }
}
