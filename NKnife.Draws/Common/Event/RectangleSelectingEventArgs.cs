using System;
using System.Drawing;

namespace NKnife.Draws.Common.Event
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