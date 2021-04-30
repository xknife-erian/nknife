using System.Drawing;

namespace NKnife.Win.Forms.Common
{
    class Redraw
    {
        public Rectangle[] Get(Point start, Point end, Point current)
        {
            return null;
        }

        public Quadrant In(Point o, Point t)
        {
            //在数学上：
            //x>0,y>0时在第一象限
            //x<0,y>0时在第二象限
            //x<0,y<0时在第三象限
            //x>0,y<0时在第四象限
            if (t.X > o.X && t.Y > o.Y)
                return Quadrant.Q4;
            if (t.X < o.X && t.Y > o.Y)
                return Quadrant.Q3;
            if (t.X < o.X && t.Y < o.Y)
                return Quadrant.Q2;
            if (t.X > o.X && t.Y < o.Y)
                return Quadrant.Q1;

            if (t.X > o.X && t.Y == o.Y)
                return Quadrant.Xz;
            if (t.X < o.X && t.Y == o.Y)
                return Quadrant.Xf;
            if (t.X == o.X && t.Y > o.Y)
                return Quadrant.Yf;
            if (t.X == o.X && t.Y < o.Y)
                return Quadrant.Yz;

            return Quadrant.O;
        }

        /// <summary>
        /// 象限中的位置
        /// </summary>
        public enum Quadrant
        {
            /// <summary>
            /// 第一象限
            /// </summary>
            Q1,
            /// <summary>
            /// 第二象限
            /// </summary>
            Q2,
            /// <summary>
            /// 第三象限
            /// </summary>
            Q3,
            /// <summary>
            /// 第四象限
            /// </summary>
            Q4,
            /// <summary>
            /// x轴正方向
            /// </summary>
            Xz,
            /// <summary>
            /// x轴负方向
            /// </summary>
            Xf,
            /// <summary>
            /// y轴正方向
            /// </summary>
            Yz,
            /// <summary>
            /// y轴负方向
            /// </summary>
            Yf,
            /// <summary>
            /// 原点
            /// </summary>
            O
        }
    }
}
