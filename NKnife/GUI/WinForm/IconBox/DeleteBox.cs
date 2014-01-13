using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class DeleteBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Properties.Resources.Delete; }
        }
    }
}

