using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class PrivateBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return NKnife.Resources.IconBoxResource.Private; }
        }
    }
}
