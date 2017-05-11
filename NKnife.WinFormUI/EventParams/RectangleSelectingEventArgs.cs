using System;
using System.Drawing;

namespace NKnife.Draws.EventParams
{
    public class RectangleSelectingEventArgs : EventArgs
    {
        public RectangleF Rectangle { get; set; }

        public RectangleSelectingEventArgs(RectangleF rectangle)
        {
            Rectangle = rectangle;
        }
    }
}