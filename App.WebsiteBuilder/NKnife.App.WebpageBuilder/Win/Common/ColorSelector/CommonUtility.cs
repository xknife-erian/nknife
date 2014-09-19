using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Jeelu.Win
{
    public static class CommonUtility
    {
        static public int AddDepth(int color, float depth,int bottomValue)
        {
            return (int)((color * depth) + (1 - depth) * bottomValue);
        }

        static public Color ApplyWhiteDepth(Color color, float depth)
        {
            return Color.FromArgb((int)(color.R + (255 - color.R) * (1-depth)),
                (int)(color.G + (255 - color.G) * (1 - depth)),
                (int)(color.B + (255 - color.B) * (1 - depth)));
        }

        static public Color ApplyBlackDepth(Color color, float depth)
        {
            return Color.FromArgb((int)(color.R * (1 - depth)),
                (int)(color.G * (1 - depth)),
                (int)(color.B * (1 - depth)));
        }

        /// <summary>
        /// 根据一个像素矩阵，将颜色数据从一个设备上下文按位块转换到另一个设备上下文。
        /// </summary>
        /// <param name="hdcDest">目标设备上下文的句柄</param>
        /// <param name="nXDest">目标左上角的x坐标</param>
        /// <param name="nYDest">目标左上角的y坐标</param>
        /// <param name="nWidth">目标矩形的宽度</param>
        /// <param name="nHeight">目标矩形的高度</param>
        /// <param name="hdcSrc">源设备上下文的句柄</param>
        /// <param name="nXSrc">源左上角的x坐标</param>
        /// <param name="nYSrc">源左上角的y坐标</param>
        /// <param name="dwRop">光栅操作代码</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        static public extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
    }
}
