using System;
using System.Drawing;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Draw
        {
            /// <summary>
            /// 返回一个用两个点来确定的矩形
            /// </summary>
            /// <param name="point1"></param>
            /// <param name="point2"></param>
            /// <returns></returns>
            public static Rectangle PointToRectangle(Point point1, Point point2)
            {
                int x = point1.X, y = point1.Y;
                if (point1.X > point2.X)
                    x = point2.X;
                if (point1.Y > point2.Y)
                    y = point2.Y;

                return new Rectangle(x, y, Math.Abs(point2.X - point1.X), Math.Abs(point2.Y - point1.Y));
            }
        }
    }
}