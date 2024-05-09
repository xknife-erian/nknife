using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.Win.Forms.Frames.Base;

namespace NKnife.Win.Forms.EventParams
{
    public class RectangleClickEventArgs : EventArgs
    {
        public RectangleClickEventArgs(MouseEventArgs e, DrawingBoardDesignMode mode, RectangleF rect)
        {
            Mode = mode;
            Button = e.Button;
            Clicks = e.Clicks;
            Rectangle = rect;
        }
        public DrawingBoardDesignMode Mode { get; private set; }

        public RectangleF Rectangle { get; private set; }

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