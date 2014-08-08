using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NKnife.Draws.Designs.Event
{
    public class PanelZoomEventArgs : EventArgs
    {
        public SizeF SourceSize { get; private set; }
        public PointF MouseClickedLocation { get; private set; }
        public double SourceMultiple { get; private set; }
        public double CurrentMultiple { get; private set; }

        public PanelZoomEventArgs(SizeF sourceSize, PointF mouseClickedLocation, double srcMultiple, double currMultiple)
        {
            SourceSize = sourceSize;
            MouseClickedLocation = mouseClickedLocation;
            SourceMultiple = srcMultiple;
            CurrentMultiple = currMultiple;
        }
    }
}
