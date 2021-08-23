using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NKnife.Win.Forms.Common
{
    /// <summary>
    ///     在 .NET Framework 中 Graphics.DrawString 方法提供了基本的文本绘制功能。
    ///     然而，这个方法本身缺乏对字符格式的控制能力，例如不支持多数文本处理器支持
    ///     的字符间距（大概微软认为不会有人编写基于 .NET 的文本处理器）。这个问题最
    ///     简单的解决方法是将整个字符串“化整为零”，一个字符一个字符的按照指定间距画
    ///     出来。然而这样做会产生大量的临时字符串，而且有巨大的 PInvoke 代价。那有
    ///     没有其他的方法呢？答案是肯定的——GDI+ 底层提供了GdipDrawDriverString方法，
    ///     允许我们对单个字符的输出位置进行控制。遗憾的是也许因为这个方法太底层了，
    ///     所以在 .NET Framework 中并没有针对它的封装。
    ///     （顺便说一下，Office 从 Office XP 开始就使用 GDI+ 作为绘图引擎，
    ///     象 Visio 中的文本绘制就使用了 GdipDrawDriverString）
    ///     下面是对 GdipDrawDriverString 的简单封装。
    /// </summary>
    public static class GdiPlus
    {
        public static void DrawDriverString(Graphics graphics, string text, Font font, Brush brush, PointF[] positions)
        {
            DrawDriverString(graphics, text, font, brush, positions, null);
        }

        public static void DrawDriverString(Graphics graphics, string text, Font font, Brush brush, PointF[] positions,
            Matrix matrix)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (text == null)
                throw new ArgumentNullException("text");
            if (font == null)
                throw new ArgumentNullException("font");
            if (brush == null)
                throw new ArgumentNullException("brush");
            if (positions == null)
                throw new ArgumentNullException("positions");

            // Get hGraphics
            FieldInfo field = typeof (Graphics).GetField("nativeGraphics",
                BindingFlags.Instance | BindingFlags.NonPublic);
            var hGraphics = (IntPtr) field.GetValue(graphics);

            // Get hFont
            field = typeof (Font).GetField("nativeFont",
                BindingFlags.Instance | BindingFlags.NonPublic);
            var hFont = (IntPtr) field.GetValue(font);

            // Get hBrush
            field = typeof (Brush).GetField("nativeBrush",
                BindingFlags.Instance | BindingFlags.NonPublic);
            var hBrush = (IntPtr) field.GetValue(brush);

            // Get hMatrix
            IntPtr hMatrix = IntPtr.Zero;
            if (matrix != null)
            {
                field = typeof (Matrix).GetField("nativeMatrix",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                hMatrix = (IntPtr) field.GetValue(matrix);
            }

            int result = GdipDrawDriverString(hGraphics, text, text.Length,
                hFont, hBrush, positions, (int) DriverStringOptions.CmapLookup, hMatrix);
        }

        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal static extern int GdipMeasureDriverString(IntPtr graphics,
            string text, int length, IntPtr font, PointF[] positions,
            int flags, IntPtr matrix, ref RectangleF bounds);

        [DllImport("Gdiplus.dll", CharSet = CharSet.Unicode)]
        internal static extern int GdipDrawDriverString(IntPtr graphics,
            string text, int length, IntPtr font, IntPtr brush,
            PointF[] positions, int flags, IntPtr matrix);

        private enum DriverStringOptions
        {
            CmapLookup = 1,
            Vertical = 2,
            Advance = 4,
            LimitSubpixel = 8,
        }
    }
}
