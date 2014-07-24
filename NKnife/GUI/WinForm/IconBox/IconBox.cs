using System.Drawing;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.IconBox
{
    public abstract class IconBox : Control
    {
        protected bool _IsFixSize = true;
        protected abstract Icon CoreIcon { get; }

        public bool IsFixSize
        {
            get { return _IsFixSize; }
            set { _IsFixSize = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //画Icon
            Size size = CoreIcon.Size;
            if (!_IsFixSize)
            {
                size = new Size(Width - 1, Height - 1);
            }
            var rect = new Rectangle(new Point(0, 0), size);
            e.Graphics.DrawIcon(CoreIcon, rect);
        }
    }
}