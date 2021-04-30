using System.Collections.Generic;

namespace NKnife.App.CharMatrix.Outline.Data
{
    /// <summary>
    /// 线数据
    /// </summary>
    public class DLine
    {
        private int type;
        private IList<POINTFX> points;

        public DLine(int type)
        {
            this.type = type;
            this.points = new List<POINTFX>();
        }
        /// <summary>
        /// 获取类型
        /// </summary>
        public int Type
        {
            get { return type; }
        }
        /// <summary>
        /// 获取点序列
        /// </summary>
        public IList<POINTFX> Points
        {
            get { return points; }
        }
    }
}
