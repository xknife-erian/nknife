using System;
using System.Drawing;

namespace NKnife.Draws.Common
{
    public class RectangleSelectedEventArgs : EventArgs
    {
        public RectangleF Rectangle { get; set; }

        public RectangleSelectedEventArgs(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }
    }
}