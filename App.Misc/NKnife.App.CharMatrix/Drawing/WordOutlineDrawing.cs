using System.Collections.Generic;
using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ��ͼ��
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
        /// ��ȡ������С
        /// </summary>
        public SizeF Size
        {
            get { return size; }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public IList<PolygonDrawing> Polygons
        {
            get { return polygons; }
        }

        #region IDrawing ��Ա

        public virtual void Draw(IDrawingContext context)
        {
            //�������
            foreach (PolygonDrawing polygon in polygons)
            {
                polygon.Draw(context);
            }
        }

        #endregion
    }
}
