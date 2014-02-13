using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class LogOffBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return NKnife.Resources.IconBoxResource.LogOff; }
        }
    }
}

