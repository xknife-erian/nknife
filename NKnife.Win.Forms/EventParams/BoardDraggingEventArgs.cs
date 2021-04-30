using System;
using System.Drawing;

namespace NKnife.Win.Forms.EventParams
{
    public class BoardDraggingEventArgs : EventArgs
    {
        public BoardDraggingEventArgs(Point start, Point current)
        {
            StartPoint = start;
            CurrentPoint = current;
        }

        /// <summary>
        ///     拖动时鼠标按下时的点，该点理解为起点
        /// </summary>
        public Point StartPoint { get; private set; }

        /// <summary>
        ///     鼠标一直在被拖动，该点是当前的点
        /// </summary>
        public Point CurrentPoint { get; private set; }
    }
}