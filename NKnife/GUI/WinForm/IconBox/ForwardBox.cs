using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class ForwardBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Properties.Resources.Forward; }
        }
    }
}
