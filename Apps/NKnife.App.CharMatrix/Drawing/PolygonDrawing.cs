using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 多边形
    /// </summary>
    public class PolygonDrawing : IDrawing
    {
        private IList<LineDrawing> lines;

        public PolygonDrawing()
        {
            lines = new List<LineDrawing>();
        }

        /// <summary>
        /// 获取线列表
        /// </summary>
        public IList<LineDrawing> Lines
        {
            get { return lines; }
        }

        #region IDrawing 成员

        public virtual void Draw(IDrawingContext context)
        {
            //画线段
            foreach (IDrawing line in lines)
            {
                line.Draw(context);
            }
        }

        #endregion
    }
}
