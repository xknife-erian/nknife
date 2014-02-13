using System.Drawing;
using System.Windows.Forms.PropertyGridInternal;

namespace NKnife.GUI.WinForm.IconBox
{
    public class ShutdownBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return NKnife.Resources.IconBoxResource.Shutdown; }
        }
    }
}
