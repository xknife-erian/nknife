using System;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Draws
{
    public class G
    {
        /// <summary>
        ///     ����ָ���������㣬ȷ����Ҫ�ػ������
        /// </summary>
        /// <param name="start">�ϴλ���ʱ�����</param>
        /// <param name="end">�ϴλ���ʱ���յ�</param>
        /// <param name="current">��ǰ�ĵ�</param>
        /// <returns></returns>
        public static Rectangle[] GetRectanglesForInvalidate(Point start, Point end, Point current)
        {
            Rectangle r0 = GetRectandle(start, end);
            if ((current.X >= end.X && current.Y >= end.Y) || (current.X <= end.X && current.Y <= end.Y))
            {
                r0 = new Rectangle(r0.X + 1, r0.Y + 1, r0.Width-1, r0.Height-1);
//                Console.WriteLine(start);
//                Console.WriteLine(end);
//                Console.WriteLine(current);
//                Console.WriteLine(r0);
//                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^");
                return new[] {r0};
            }
            var r1 = new Rectangle(current.X, start.Y, Math.Abs(end.X - current.X), Math.Abs(current.Y - start.Y));
            var r2 = new Rectangle(start.X, current.Y, Math.Abs(end.X - start.X), Math.Abs(end.Y - current.Y));
//            Console.WriteLine(start);
//            Console.WriteLine(end);
//            Console.WriteLine(current);
//            Console.WriteLine(r1);
//            Console.WriteLine(r2);
//            Console.WriteLine("________________________");
            return new[] { r1, r2 };
        }

        public static Rectangle GetRectandle(Point start, Point end)
        {
            int m = start.X < end.X ? start.X : end.X;
            int n = start.Y < end.Y ? start.Y : end.Y;
            int w = Math.Abs(end.X - start.X);
            int y = Math.Abs(end.Y - start.Y);
            return new Rectangle(m, n, w, y);
        }

        /// <summary>
        ///     ����һ������������ȷ���ľ���
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

        /// <summary>
        ///     ����һ������������ȷ���ľ���
        /// </summary>
        /// <param name="aaaa"></param>
        /// <param name="bbbb"></param>
        /// <returns></returns>
        public static Rectangle PointToRectangleOfRemove(Point aaaa, Point bbbb)
        {
            int x = aaaa.X, y = aaaa.Y;

            if (aaaa.X > bbbb.X)
                x = bbbb.X;
            if (aaaa.Y > bbbb.Y)
                y = bbbb.Y;

            return new Rectangle(x + 1, y + 1, Math.Abs(bbbb.X - aaaa.X) - 1, Math.Abs(bbbb.Y - aaaa.Y) - 1);
        }

        /// <summary>
        ///     �ж��ַ����Ƿ���һ���Ϸ�������
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidNum(string s)
        {
            if (s.Length < 1)
            {
                return false;
            }
            if (s[0] == 0)
            {
                return false;
            }
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     ʹ��������ľֲ������ػ�
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        public static void Invalidate(Control ctl, int pixel, Point pt1, Point pt2)
        {
            Rectangle rect = PointToRectangle(pt1, pt2);
            if (rect.Width == 0)
            {
                rect.Width = 1;
            }
            if (rect.Height == 0)
            {
                rect.Height = 1;
            }
            ctl.Invalidate(new Rectangle(rect.X - pixel, rect.Y - pixel, rect.Width + 4*pixel, rect.Height + 4*pixel));
        }

        public static void Invalidate(Control ctl, int pixel, Point pt1, Point pt2, float zoom)
        {
            Rectangle rect = PointToRectangle(pt1, pt2);
            if (rect.Width == 0)
            {
                rect.Width = 1;
            }
            if (rect.Height == 0)
            {
                rect.Height = 1;
            }
            Invalidate(ctl, pixel, rect, zoom);
        }

        /// <summary>
        ///     ˢ������
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        public static void Invalidate(Control ctl, int pixel, int x, int y, int width, int heigth)
        {
            ctl.Invalidate(new Rectangle(x - pixel, y - pixel, width + 4*pixel, heigth + 4*pixel));
        }

        public static void Invalidate(Control ctl, int pixel, int x, int y, int width, int heigth, float zoom)
        {
            ctl.Invalidate(new Rectangle(
                (int) (x*zoom - pixel),
                (int) (y*zoom - pixel),
                (int) (width*zoom + 4*pixel),
                (int) (heigth*zoom + 4*pixel)));
        }


        /// <summary>
        ///     ʹ����Rectangle�ľֲ������ػ�
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pixel"></param>
        public static void Invalidate(Control ctl, int pixel, Rectangle rect)
        {
            ctl.Invalidate(
                new Rectangle(rect.X - pixel, rect.Y - pixel, rect.Width + 4*pixel, rect.Height + 4*pixel));
        }

        public static void Invalidate(Control ctl, int pixel, Rectangle rect, float zoom)
        {
            ctl.Invalidate(
                new Rectangle(
                    (int) (rect.X*zoom - pixel),
                    (int) (rect.Y*zoom - pixel),
                    (int) (rect.Width*zoom + 4*pixel),
                    (int) (rect.Height*zoom + 4*pixel)));
        }

        public static void Invalidate(Control ctl, Rectangle rect, float zoom)
        {
            ctl.Invalidate(
                new Rectangle(
                    (int) (rect.X*zoom) - 1,
                    (int) (rect.Y*zoom) - 1,
                    (int) (rect.Width*zoom) + 2,
                    (int) (rect.Height*zoom) + 2));
        }
    }
}