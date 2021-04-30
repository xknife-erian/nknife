using System;
using System.Drawing;

namespace NKnife.Win.Forms.EventParams
{
    public class BoardZoomEventArgs : EventArgs
    {
        public BoardZoomEventArgs(PointF mouseClickedLocation, double oldZoom)
        {
            MouseClickedLocation = mouseClickedLocation;
            OldZoom = oldZoom;
        }

        /// <summary>
        ///     未缩放时鼠标点击的位置
        /// </summary>
        public PointF MouseClickedLocation { get; private set; }

        /// <summary>
        ///     原缩放率
        /// </summary>
        public double OldZoom { get; private set; }
    }
}