using System;
using System.Drawing;

namespace NKnife.Draws.Controls.Frames.Event
{
    public class BoardZoomEventArgs : EventArgs
    {
        public SizeF SourceSize { get; private set; }
        public PointF MouseClickedLocation { get; private set; }
        public double SourceMultiple { get; private set; }
        public double CurrentMultiple { get; private set; }

        public BoardZoomEventArgs(SizeF sourceSize, PointF mouseClickedLocation, double srcMultiple, double currMultiple)
        {
            SourceSize = sourceSize;
            MouseClickedLocation = mouseClickedLocation;
            SourceMultiple = srcMultiple;
            CurrentMultiple = currMultiple;
        }
    }
}
