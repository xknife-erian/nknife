using System.Collections.Generic;
using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ��
    /// </summary>
    public abstract class LineDrawing : IDrawing
    {
        private List<PointF> points;

        public LineDrawing()
        {
            points = new List<PointF>();
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public List<PointF> Points
        {
            get { return points; }
        }

        #region IDrawing ��Ա

        public abstract void Draw(IDrawingContext context);

        #endregion
    }
}
