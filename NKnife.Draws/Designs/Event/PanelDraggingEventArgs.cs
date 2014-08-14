using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NKnife.Draws.Designs.Event
{
    public class PanelDraggingEventArgs :EventArgs
    {
        public Point MousePoint { get; private set; }

        public PanelDraggingEventArgs(Point locaiton)
        {
            MousePoint = locaiton;
        }
    }
}
