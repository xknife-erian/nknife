using System;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Draws.Common.Event
{
    public class RectangleClickEventArgs : EventArgs
    {
        public RectangleClickEventArgs(MouseEventArgs e, ImagePanelDesignMode mode, bool inRect = false)
            : this(e, mode, inRect, RectangleF.Empty)
        {
        }

        public RectangleClickEventArgs(MouseEventArgs e, ImagePanelDesignMode mode, bool inRect, RectangleF rect)
        {
            Mode = mode;
            Button = e.Button;
            Clicks = e.Clicks;
            InRectange = inRect;
            Rectange = rect;
        }

        public bool InRectange { get; private set; }

        public RectangleF Rectange { get; private set; }

        public ImagePanelDesignMode Mode { get; private set; }

        /// <summary>
        ///     Gets which mouse button was pressed.
        /// </summary>
        public MouseButtons Button { get; private set; }

        /// <summary>
        ///     Gets the number of times the mouse button was pressed and released.
        /// </summary>
        public int Clicks { get; private set; }
    }
}