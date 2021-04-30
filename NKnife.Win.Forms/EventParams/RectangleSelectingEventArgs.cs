using System;
using System.Drawing;

namespace NKnife.Win.Forms.EventParams
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