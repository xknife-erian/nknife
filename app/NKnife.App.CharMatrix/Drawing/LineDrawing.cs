using System.Collections.Generic;
using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 线
    /// </summary>
    public abstract class LineDrawing : IDrawing
    {
        private List<PointF> points;

        public LineDrawing()
        {
            points = new List<PointF>();
        }

        /// <summary>
        /// 获取点序列
        /// </summary>
        public List<PointF> Points
        {
            get { return points; }
        }

        #region IDrawing 成员

        public abstract void Draw(IDrawingContext context);

        #endregion
    }
}
