using System.Drawing;
using System.Drawing.Drawing2D;

namespace NKnife.App.Sudoku.Controls
{
    internal static class GraphicsHelper
    {
        /// <summary>
        /// 数独界面中的一个单元格中的文字的绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="str"></param>
        /// <param name="rect"></param>
        public static void CellTextPaint(Graphics g, string str, Brush brush, Rectangle rect)
        {
            if (string.IsNullOrEmpty(str))
            {
                return;
            }
            //计算字符绘制的位置
            float fontoffset = 14;
            PointF point = new PointF(fontoffset / 2 + fontoffset / 4, fontoffset / 2 - fontoffset / 5);

            Font font = new Font("Times New Roman", rect.Width - fontoffset, FontStyle.Bold, GraphicsUnit.Pixel);
            GraphicsHelper.CellTextPaint(g, str, rect, brush, font, point);
        }
        public static void CellTextPaint(Graphics g, string str, Rectangle rect, Brush textBrush, Font font, PointF point)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawString(str, font, textBrush, point);
        }

        /// <summary>
        /// 为数独界面的一个单元格进行完整的绘制
        /// </summary>
        /// <param name="g">绘制的Graphics</param>
        /// <param name="currRect">要绘制的矩形</param>
        public static void CellPaint(Graphics g, Rectangle currRect)
        {
            GraphicsHelper.CellPaint(g, currRect,
                Color.Gray,
                SystemColors.Control,
                Brushes.BlanchedAlmond);
        }
        /// <summary>
        /// 为数独界面的一个单元格进行完整的绘制
        /// </summary>
        /// <param name="g">绘制的Graphics</param>
        /// <param name="currRect">要绘制的矩形</param>
        /// <param name="bottomLeverCenterColor">绘制时最底层中心点的颜色(底层绘制时是一个向四周辐射渐变的色块)</param>
        /// <param name="parentBackColor">最底层绘制时，中心向外侧辐射时最外周的颜色</param>
        /// <param name="mainLevelColor">最上层包含数字的矩形的颜色</param>
        public static void CellPaint(Graphics g, Rectangle currRect, Color bottomLeverCenterColor, Color parentBackColor, Brush mainLevelColor)
        {
            GraphicsHelper.RadiusDiagonal(g, currRect, bottomLeverCenterColor, parentBackColor);
            int top = currRect.Top + 6;
            int bottom = currRect.Bottom - 6;
            int left = currRect.Left + 6;
            int right = currRect.Right - 6;
            g.FillRectangle(mainLevelColor, new Rectangle(top, left, right - left, bottom - top));
        }

        /// <summary>
        /// 对一个矩形的从中心向四周的辐射渐变
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="centerColor">中心点的颜色</param>
        /// <param name="borderColor">矩形最外侧的颜色</param>
        public static void RadiusDiagonal(Graphics g, Rectangle rect, Color centerColor, Color borderColor)
        {
            Rectangle innerRect = new Rectangle(new Point(3, 3), new Size(rect.Width - 7, rect.Height - 7));
            LinearGradientBrush lBrush;

            //中心点坐标
            Point centerPoint = new Point(rect.Width / 2, rect.Height / 2);

            //中心点至上边
            lBrush = new LinearGradientBrush(rect, borderColor, centerColor,
                LinearGradientMode.Vertical);
            g.FillPolygon(lBrush,
                new Point[]{centerPoint,
                    new Point(rect.Left,rect.Top),
                    new Point(rect.Right,rect.Top)});
            //中心点至下边
            lBrush = new LinearGradientBrush(rect, centerColor, borderColor,
                LinearGradientMode.Vertical);
            g.FillPolygon(lBrush,
                new Point[]{centerPoint,
                    new Point(rect.Right,rect.Bottom),
                    new Point(rect.Left,rect.Bottom)});
            //中心点至左边
            lBrush = new LinearGradientBrush(rect,  borderColor,centerColor,
                LinearGradientMode.Horizontal);
            g.FillPolygon(lBrush,
                new Point[]{centerPoint,
                    new Point(rect.Left,rect.Top),
                    new Point(rect.Left,rect.Bottom)});
            //中心点至右边
            lBrush = new LinearGradientBrush(rect, centerColor, borderColor,
                LinearGradientMode.Horizontal);
            g.FillPolygon(lBrush,
                new Point[]{centerPoint,
                    new Point(rect.Right,rect.Top),
                    new Point(rect.Right,rect.Bottom)});
        }
    }
}
