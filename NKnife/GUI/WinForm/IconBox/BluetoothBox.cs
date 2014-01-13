using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class BluetoothBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Properties.Resources.Bluetooth; }
        }
    }
}
