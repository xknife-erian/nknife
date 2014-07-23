using System.Drawing;
using System.Windows.Forms.PropertyGridInternal;
using NKnife.ShareResources;

namespace NKnife.GUI.WinForm.IconBox
{
    public class ShutdownBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return IconBoxResource.Shutdown; }
        }
    }
}
