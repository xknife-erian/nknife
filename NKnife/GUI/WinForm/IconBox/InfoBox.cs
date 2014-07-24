using System.Drawing;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class InfoBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Info; }
        }
    }
}
