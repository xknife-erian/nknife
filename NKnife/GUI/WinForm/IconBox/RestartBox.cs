using System.Drawing;

namespace NKnife.GUI.WinForm.IconBox
{
    public class RestartBox : Gean.Gui.WinForm.IconBox.IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Properties.Resources.Restart; }
        }
    }
}
