using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class BackBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return NKnife.Resources.IconBoxResource.Back; }
        }
    }
}
