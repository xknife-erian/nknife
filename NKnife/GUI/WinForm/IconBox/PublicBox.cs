using System.Drawing;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class PublicBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Public; }
        }
    }
}
