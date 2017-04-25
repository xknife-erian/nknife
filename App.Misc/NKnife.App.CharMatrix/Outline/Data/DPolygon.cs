using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Outline.Data
{
    /// <summary>
    /// 多边形数据
    /// </summary>
    public class DPolygon
    {
        private int type;
        private POINTFX start;
        private IList<DLine> lines;

        public DPolygon(int type)
        {
            this.type = type;
            this.lines = new List<DLine>();
        }
        /// <summary>
        /// 获取或设置
        /// </summary>
        public POINTFX Start
        {
            get { return start; }
            set { start = value; }
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        public int Type
        {
            get { return type; }
        }
        /// <summary>
        /// 获取线段列表
        /// </summary>
        public IList<DLine> Lines
        {
            get { return lines; }
        }
    }
}
