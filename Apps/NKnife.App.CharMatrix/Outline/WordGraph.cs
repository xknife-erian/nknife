using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using NKnife.App.CharMatrix.Outline.Data;

namespace NKnife.App.CharMatrix.Outline
{
    public static class WordGraph
    {
        private const int GGO_NATIVE = 2;

        /// <summary>
        /// 获取轮廓多边形列表
        /// </summary>
        /// <param name="hdc">场景</param>
        /// <param name="uChar">字符</param>
        /// <returns></returns>
        private static DOutline GetOutline(IntPtr hdc, uint uChar)
        {
            // 转置矩阵
            MAT2 mat2 = new MAT2();
            mat2.eM11.value = 1;
            mat2.eM22.value = 1;

            GLYPHMETRICS lpGlyph = new GLYPHMETRICS();

            //获取缓存区大小
            int bufferSize = GdiNativeMethods.GetGlyphOutline(hdc, uChar, GGO_NATIVE, out lpGlyph, 0, IntPtr.Zero, ref mat2);

            if (bufferSize > 0)
            {
                //获取成功后，分配托管内存
                IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
                try
                {
                    int ret = GdiNativeMethods.GetGlyphOutline(hdc, uChar, GGO_NATIVE, out lpGlyph, (uint)bufferSize, buffer, ref mat2);
                    if (ret > 0)
                    {
                        //构建轮廓
                        DOutline outline = new DOutline(lpGlyph.gmBlackBoxX, lpGlyph.gmBlackBoxY);
                        //从缓存区构造字型轮廓
                        FillPolygons(outline, buffer, bufferSize);

                        return outline;
                    }
                    else
                    {
                        throw new Exception("获取字型数据失败！");
                    }
                }
                finally
                {
                    //释放托管内存
                    Marshal.FreeHGlobal(buffer);
                }
            }
            else
            {
                throw new Exception("未能获取缓存区！");
            }
        }

        /// <summary>
        /// 从缓存区构造多边形填充到字型轮廓(一个字型轮廓包含多个多边形，每个多边形由若干个直线或曲线组成)
        /// </summary>
        /// <param name="outline">轮廓</param>
        /// <param name="buffer">缓存区指针</param>
        /// <param name="bufferSize">缓存区大小</param>
        /// <returns></returns>
        private static void FillPolygons(DOutline outline, IntPtr buffer, int bufferSize)
        {
            //多边形头大小
            int polygonHeaderSize = Marshal.SizeOf(typeof(TTPOLYGONHEADER));
            //线大小
            int lineSize = Marshal.SizeOf(typeof(TTPOLYCURVEHEAD));
            //点大小
            int pointFxSize = Marshal.SizeOf(typeof(POINTFX));

            //缓存区首地址值
            int ptr = buffer.ToInt32();
            //偏移量
            int offset = 0;

            //轮廓的多边形列表
            IList<DPolygon> polygons = outline.Polygons;
            
            while (offset < bufferSize)
            {
                //多边形头信息
                TTPOLYGONHEADER header = (TTPOLYGONHEADER)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(TTPOLYGONHEADER));
                //构建多边形
                DPolygon polygon = new DPolygon(header.dwType);
                //起始点
                polygon.Start = header.pfxStart;
                //获取尾索引
                int endCurvesIndex = offset + header.cb;
                //向后偏移一个项
                offset += polygonHeaderSize;

                while (offset < endCurvesIndex)
                {
                    //线段信息
                    TTPOLYCURVEHEAD lineHeader = (TTPOLYCURVEHEAD)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(TTPOLYCURVEHEAD));
                    //偏移到点序列首地址
                    offset += lineSize;

                    //构建线段
                    DLine line = new DLine(lineHeader.wType);

                    //读取点序列，加入线段中
                    for (int i = 0; i < lineHeader.cpfx; i++)
                    {
                        POINTFX point = (POINTFX)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(POINTFX));
                        //将点加入线段
                        line.Points.Add(point);

                        //偏移
                        offset += pointFxSize;
                    }
                    //将线加入多边形
                    polygon.Lines.Add(line);
                }
                //将多边形加入轮廓
                polygons.Add(polygon);
            }
        }

        /// <summary>
        /// 获取指定字符在指定字体下的轮廓
        /// </summary>
        /// <param name="uChar">字符</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        public static DOutline GetOutline(uint uChar, Font font)
        {
            //画板
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr hdc = g.GetHdc();

            //将字体选入场景
            IntPtr fontPtr = font.ToHfont();
            GdiNativeMethods.SelectObject(hdc, fontPtr);
            try
            {
                return GetOutline(hdc, uChar);
            }
            finally
            {
                //关闭字体句柄，释放场景
                GdiNativeMethods.CloseHandle(fontPtr);
                g.ReleaseHdc(hdc);
            }
        }
    }
}
