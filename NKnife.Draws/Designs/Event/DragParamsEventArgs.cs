using System;
using System.Drawing;

namespace NKnife.Draws.Designs.Event
{
    public class DragParamsEventArgs : EventArgs
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public DragParamsEventArgs(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}