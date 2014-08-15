using System;
using System.Drawing;

namespace NKnife.Draws.Controls.Frames.Event
{
    public class BoardZoomEventArgs : EventArgs
    {
        /// <summary>
        /// 原大小
        /// </summary>
        public SizeF OldSize { get; private set; }

        /// <summary>
        /// 当放大的瞬间，鼠标点击的位置
        /// </summary>
        public PointF MouseClickedLocation { get; private set; }

        /// <summary>
        /// 原放大倍数
        /// </summary>
        public double OldMultiple { get; private set; }

        /// <summary>
        /// 当前放大倍数
        /// </summary>
        public double CurrentMultiple { get; private set; }

        public BoardZoomEventArgs(SizeF oldSize, PointF mouseClickedLocation, double srcMultiple, double currMultiple)
        {
            OldSize = oldSize;
            MouseClickedLocation = mouseClickedLocation;
            OldMultiple = srcMultiple;
            CurrentMultiple = currMultiple;
        }
    }
}
