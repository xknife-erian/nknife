using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class CommonFuns
    {
        /// <summary>
        /// ����һ������������ȷ���ľ���
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
        /// ����һ����������ȷ������
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static PartitionLine PointToLine(Point point1, Point point2)
        {
            int start, end;
            if (point1.X == point2.X)
            {
                start = point1.Y <= point2.Y ? point1.Y : point2.Y;
                end = point1.Y <= point2.Y ? point2.Y : point1.Y;

                return new PartitionLine(start, end, point1.X, false);
            }
            else if (point1.Y == point2.Y)
            {
                start = point1.X <= point2.X ? point1.X : point2.X;
                end = point1.X <= point2.X ? point2.X : point1.X;

                return new PartitionLine(start, end, point1.Y, true);
            }
            else
                return null;
        }

        /// <summary>
        /// �õ����εı߽���
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static PartitionLine[] GetRectBorderLines(Point pt1, Point pt2)
        {

            PartitionLine[] borderLines = new PartitionLine[4];
            Rectangle rect = CommonFuns.PointToRectangle(pt1, pt2);
            borderLines[0] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y, true);
            borderLines[1] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y + rect.Height, true);
            borderLines[2] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X, false);
            borderLines[3] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X + rect.Width, false);
            return borderLines;
        }

        /// <summary>
        /// �õ����εı߽���
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PartitionLine[] GetRectBorderLines(Rectangle rect)
        {
            PartitionLine[] borderLines = new PartitionLine[4];
            borderLines[0] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y, true);
            borderLines[1] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y + rect.Height, true);
            borderLines[2] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X, false);
            borderLines[3] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X + rect.Width, false);
            return borderLines;
        }

        /// <summary>
        /// �õ����εı߽���
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PartitionLine[] GetRectBorderLines(Rect rect)
        {
            PartitionLine[] borderLines = new PartitionLine[4];
            borderLines[0] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y, true);
            borderLines[1] = new PartitionLine(rect.X, rect.X + rect.Width, rect.Y + rect.Height, true);
            borderLines[2] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X, false);
            borderLines[3] = new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X + rect.Width, false);
            return borderLines;
        }

        /// <summary>
        /// ���Ҳ�����ҳ��Ƭ��������֮���߽�
        /// </summary>
        /// <param name="rects"></param>
        /// <returns></returns>
        public static Rectangle FindRectsBorder(List<Rect> rects)
        {
            if (rects == null || rects.Count < 1)
            {
                return default(Rectangle);
            }
            Rectangle rect = new Rectangle();

            ///��x����
            rects.Sort(new CompareRectX());
            rect.X = rects[0].X;

            ///��Y����
            rects.Sort(new CompareRectY());
            rect.Y = rects[0].Y;

            ///Right
            rects.Sort(new CompareRectRight());
            rect.Width = rects[rects.Count - 1].X + rects[rects.Count - 1].Width - rect.X;

            ///bottom
            rects.Sort(new CompareRectBottom());
            rect.Height = rects[rects.Count - 1].Y + rects[rects.Count - 1].Height - rect.Y;


            return rect;
        }

        /// <summary>
        /// ����δʣ���(δ��ѡ���)��������
        /// </summary>
        /// <param name="totalRectList"></param>
        /// <param name="selectedRectList"></param>
        /// <returns></returns>
        public static List<Rect> RemainRectList(List<Rect> totalRectList, List<Rect> selectedRectList)
        {
            List<Rect> remainRects = new List<Rect>();
            bool isSelected;

            foreach (Rect tRect in totalRectList)
            {
                isSelected = false;
                foreach (Rect sRect in selectedRectList)
                {
                    if (tRect == sRect)
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (!isSelected)
                {
                    remainRects.Add(tRect);
                }
            }
            return remainRects;
        }

        /// <summary>
        /// ���ر�ѡ�еĵ�ѡ������
        /// </summary>
        /// <param name="groupBox"></param>
        /// <returns></returns>
        public static int GetChedIndex(GroupBox groupBox)
        {
            int i = 0;
            foreach (Control ctrl in groupBox.Controls)
            {
                if (((RadioButton)ctrl).Checked)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// �ж��ַ����Ƿ���һ���Ϸ�������
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
        /// ʹ��������ľֲ������ػ�
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        public static void Invalidate(Control ctl, int pixel, Point pt1, Point pt2)
        {
            Rectangle rect = CommonFuns.PointToRectangle(pt1, pt2);
            if (rect.Width == 0)
            {
                rect.Width = 1;
            }
            if (rect.Height == 0)
            {
                rect.Height = 1;
            }
            ctl.Invalidate(new Rectangle(rect.X - pixel, rect.Y - pixel, rect.Width + 4 * pixel, rect.Height + 4 * pixel));
        }

        public static void Invalidate(Control ctl, int pixel, Point pt1, Point pt2, float zoom)
        {
            Rectangle rect = CommonFuns.PointToRectangle(pt1, pt2);
            if (rect.Width == 0)
            {
                rect.Width = 1;
            }
            if (rect.Height == 0)
            {
                rect.Height = 1;
            }
            CommonFuns.Invalidate(ctl, pixel, rect, zoom);
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        public static void Invalidate(Control ctl, int pixel, int x, int y, int width, int heigth)
        {
            ctl.Invalidate(new Rectangle(x - pixel, y - pixel, width + 4 * pixel, heigth + 4 * pixel));
        }

        public static void Invalidate(Control ctl, int pixel, int x, int y, int width, int heigth, float zoom)
        {
            ctl.Invalidate(new Rectangle(
                (int)(x * zoom - pixel),
                (int)(y * zoom - pixel),
                (int)(width * zoom + 4 * pixel),
                (int)(heigth * zoom + 4 * pixel)));
        }

        ///// <summary>
        ///// ˢ������
        ///// </summary>
        ///// <param name="ctl"></param>
        ///// <param name="pixel"></param>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="width"></param>
        ///// <param name="heigth"></param>
        //public static void Invalidate(Control ctl, int pixel, float x, float y, float width, float heigth)
        //{
        //    ctl.Invalidate(new Rectangle(x - pixel, y - pixel, width + 4 * pixel, heigth + 4 * pixel));
        //}

        /// <summary>
        /// ʹ����rect�ľֲ������ػ�
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="rect"></param>
        public static void Invalidate(Control ctl, int pixel, Rect rect)
        {
            ctl.Invalidate(
                new Rectangle(rect.X - pixel, rect.Y - pixel, rect.Width + 4 * pixel, rect.Height + 4 * pixel));
        }
        public static void Invalidate(Control ctl, int pixel, Rect rect, float zoom)
        {
            ctl.Invalidate(
                new Rectangle(
                (int)((rect.X - pixel) * zoom),
                (int)((rect.Y - pixel) * zoom),
                (int)(rect.Width * zoom + 4 * pixel),
                (int)(rect.Height * zoom + 4 * pixel)));
        }

        /// <summary>
        /// ʹ����Rectangle�ľֲ������ػ�
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="pixel"></param>
        public static void Invalidate(Control ctl, int pixel, Rectangle rect)
        {
            ctl.Invalidate(
                new Rectangle(rect.X - pixel, rect.Y - pixel, rect.Width + 4 * pixel, rect.Height + 4 * pixel));
        }
        public static void Invalidate(Control ctl, int pixel, Rectangle rect, float zoom)
        {
            ctl.Invalidate(
                new Rectangle(
                (int)(rect.X * zoom - pixel),
                (int)(rect.Y * zoom - pixel),
                (int)(rect.Width * zoom + 4 * pixel),
                (int)(rect.Height * zoom + 4 * pixel)));
        }

        public static void Invalidate(Control ctl,  Rectangle rect, float zoom)
        {
            ctl.Invalidate(
                new Rectangle(
                (int)(rect.X * zoom)-1,
                (int)(rect.Y * zoom )-1,
                (int)(rect.Width * zoom )+2,
                (int)(rect.Height * zoom )+2));
        }

        /// <summary>
        /// ʹ����line�ľֲ������ػ�
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="pixel"></param>
        /// <param name="line"></param>
        /// <param name="zoom"></param>
        public static void Invalidate(Control ctl, int pixel, PartitionLine line, float zoom)
        {
            if (line.IsRow)
            {
                ctl.Invalidate(
                    new Rectangle(
                    (int)(line.Start * zoom - pixel),
                    (int)(line.Position * zoom - pixel),
                    (int)((line.End - line.Start) * zoom + 4 * pixel),
                    (int)(4 * pixel)));
            }
            else
            {
                ctl.Invalidate(
                    new Rectangle(
                    (int)(line.Position * zoom - pixel),
                    (int)(line.Start * zoom - pixel),
                    (int)(4 * pixel),
                    (int)((line.End - line.Start) * zoom) + 4 * pixel));
            }
        }

        public static void Invalidate(Control ctl, int pixel, PartitionLine line)
        {
            if (line.IsRow)
            {
                ctl.Invalidate(
                    new Rectangle(
                    line.Start - pixel,
                    line.Position - pixel,
                    line.End - line.Start + 4 * pixel,
                    4 * pixel));
            }
            else
            {
                ctl.Invalidate(
                    new Rectangle(
                    line.Position - pixel,
                    line.Start - pixel,
                    4 * pixel,
                    line.End - line.Start + 4 * pixel
                    ));
            }
        }
    }
}
