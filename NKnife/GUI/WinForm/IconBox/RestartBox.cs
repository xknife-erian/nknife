using System.Drawing;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class RestartBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Restart; }
        }
    }
}
