using System;
using System.Drawing;

namespace NKnife.Draws.EventParams
{
    public class BoardDesignDragParamsEventArgs : EventArgs
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public BoardDesignDragParamsEventArgs(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}