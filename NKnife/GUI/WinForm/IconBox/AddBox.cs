using System.Drawing;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class AddBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Add; }
        }
    }
}
