using System;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Win.Forms.EventParams
{
    public class RectangleSelectedEventArgs : EventArgs
    {
        public RectangleSelectedEventArgs(RectangleF rectangle, MouseEventArgs e)
        {
            Rectangle = rectangle;
            Button = e.Button;
            Clicks = e.Clicks;
            X = e.X;
            Y = e.Y;
            Delta = e.Delta;
        }

        public RectangleF Rectangle { get; set; }

        /// <summary>
        ///     Gets which mouse button was pressed.
        /// </summary>
        public MouseButtons Button { get; private set; }

        /// <summary>
        ///     Gets the number of times the mouse button was pressed and released.
        /// </summary>
        public int Clicks { get; private set; }

        /// <summary>
        ///     Gets the x-coordinate of a mouse click.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        ///     Gets the y-coordinate of a mouse click.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        ///     Gets a signed count of the number of detents the mouse wheel has rotated.
        /// </summary>
        public int Delta { get; private set; }

        /// <summary>
        ///     Gets the location of the mouse during MouseEvent.
        /// </summary>
        public Point Location
        {
            get { return new Point(X, Y); }
        }
    }
}