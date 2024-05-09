using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Outline.Data
{
    /// <summary>
    /// 轮廓数据
    /// </summary>
    public class DOutline
    {
        private uint width;
        private uint height;
        private IList<DPolygon> polygons;

        public DOutline(uint width, uint height)
        {
            this.width = width;
            this.height = height;
            this.polygons = new List<DPolygon>();
        }

        /// <summary>
        /// 获取宽度
        /// </summary>
        public uint Width
        {
            get { return width; }
        }
        /// <summary>
        /// 获取高度
        /// </summary>
        public uint Height
        {
            get { return height; }
        }

        /// <summary>
        /// 获取多边形列表
        /// </summary>
        public IList<DPolygon> Polygons
        {
            get { return polygons; }
        }
    }
}
