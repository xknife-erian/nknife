using System.Collections.Generic;
using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 字图形
    /// </summary>
    public class WordOutlineDrawing : IDrawing
    {
        private SizeF size;
        private IList<PolygonDrawing> polygons;

        public WordOutlineDrawing(SizeF size)
        {
            this.size = size;
            this.polygons = new List<PolygonDrawing>();
        }

        /// <summary>
        /// 获取轮廓大小
        /// </summary>
        public SizeF Size
        {
            get { return size; }
        }

        /// <summary>
        /// 获取字体轮廓
        /// </summary>
        public IList<PolygonDrawing> Polygons
        {
            get { return polygons; }
        }

        #region IDrawing 成员

        public virtual void Draw(IDrawingContext context)
        {
            //画多边形
            foreach (PolygonDrawing polygon in polygons)
            {
                polygon.Draw(context);
            }
        }

        #endregion
    }
}
