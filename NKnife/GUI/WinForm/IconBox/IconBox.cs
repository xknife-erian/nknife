using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gean.Gui.WinForm.IconBox
{
    public abstract class IconBox : Control
    {
        protected abstract Icon CoreIcon { get; }
        public bool IsFixSize { get { return _IsFixSize; } set { _IsFixSize = value; } }
        protected bool _IsFixSize = true;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //画Icon
            Size size = this.CoreIcon.Size;
            if (!_IsFixSize)
            {
                size = new Size(this.Width - 1, this.Height - 1);
            }
            Rectangle rect = new Rectangle(new Point(0, 0), size);
            e.Graphics.DrawIcon(this.CoreIcon, rect);
        }
    }
}
