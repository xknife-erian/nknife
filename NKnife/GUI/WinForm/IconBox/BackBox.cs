using System.Drawing;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class BackBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Back; }
        }
    }
}
