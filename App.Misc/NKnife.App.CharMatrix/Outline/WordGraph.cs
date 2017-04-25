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
        /// ��ȡ����������б�
        /// </summary>
        /// <param name="hdc">����</param>
        /// <param name="uChar">�ַ�</param>
        /// <returns></returns>
        private static DOutline GetOutline(IntPtr hdc, uint uChar)
        {
            // ת�þ���
            MAT2 mat2 = new MAT2();
            mat2.eM11.value = 1;
            mat2.eM22.value = 1;

            GLYPHMETRICS lpGlyph = new GLYPHMETRICS();

            //��ȡ��������С
            int bufferSize = GdiNativeMethods.GetGlyphOutline(hdc, uChar, GGO_NATIVE, out lpGlyph, 0, IntPtr.Zero, ref mat2);

            if (bufferSize > 0)
            {
                //��ȡ�ɹ��󣬷����й��ڴ�
                IntPtr buffer = Marshal.AllocHGlobal(bufferSize);
                try
                {
                    int ret = GdiNativeMethods.GetGlyphOutline(hdc, uChar, GGO_NATIVE, out lpGlyph, (uint)bufferSize, buffer, ref mat2);
                    if (ret > 0)
                    {
                        //��������
                        DOutline outline = new DOutline(lpGlyph.gmBlackBoxX, lpGlyph.gmBlackBoxY);
                        //�ӻ�����������������
                        FillPolygons(outline, buffer, bufferSize);

                        return outline;
                    }
                    else
                    {
                        throw new Exception("��ȡ��������ʧ�ܣ�");
                    }
                }
                finally
                {
                    //�ͷ��й��ڴ�
                    Marshal.FreeHGlobal(buffer);
                }
            }
            else
            {
                throw new Exception("δ�ܻ�ȡ��������");
            }
        }

        /// <summary>
        /// �ӻ���������������䵽��������(һ���������������������Σ�ÿ������������ɸ�ֱ�߻��������)
        /// </summary>
        /// <param name="outline">����</param>
        /// <param name="buffer">������ָ��</param>
        /// <param name="bufferSize">��������С</param>
        /// <returns></returns>
        private static void FillPolygons(DOutline outline, IntPtr buffer, int bufferSize)
        {
            //�����ͷ��С
            int polygonHeaderSize = Marshal.SizeOf(typeof(TTPOLYGONHEADER));
            //�ߴ�С
            int lineSize = Marshal.SizeOf(typeof(TTPOLYCURVEHEAD));
            //���С
            int pointFxSize = Marshal.SizeOf(typeof(POINTFX));

            //�������׵�ֵַ
            int ptr = buffer.ToInt32();
            //ƫ����
            int offset = 0;

            //�����Ķ�����б�
            IList<DPolygon> polygons = outline.Polygons;
            
            while (offset < bufferSize)
            {
                //�����ͷ��Ϣ
                TTPOLYGONHEADER header = (TTPOLYGONHEADER)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(TTPOLYGONHEADER));
                //���������
                DPolygon polygon = new DPolygon(header.dwType);
                //��ʼ��
                polygon.Start = header.pfxStart;
                //��ȡβ����
                int endCurvesIndex = offset + header.cb;
                //���ƫ��һ����
                offset += polygonHeaderSize;

                while (offset < endCurvesIndex)
                {
                    //�߶���Ϣ
                    TTPOLYCURVEHEAD lineHeader = (TTPOLYCURVEHEAD)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(TTPOLYCURVEHEAD));
                    //ƫ�Ƶ��������׵�ַ
                    offset += lineSize;

                    //�����߶�
                    DLine line = new DLine(lineHeader.wType);

                    //��ȡ�����У������߶���
                    for (int i = 0; i < lineHeader.cpfx; i++)
                    {
                        POINTFX point = (POINTFX)Marshal.PtrToStructure(new IntPtr(ptr + offset), typeof(POINTFX));
                        //��������߶�
                        line.Points.Add(point);

                        //ƫ��
                        offset += pointFxSize;
                    }
                    //���߼�������
                    polygon.Lines.Add(line);
                }
                //������μ�������
                polygons.Add(polygon);
            }
        }

        /// <summary>
        /// ��ȡָ���ַ���ָ�������µ�����
        /// </summary>
        /// <param name="uChar">�ַ�</param>
        /// <param name="font">����</param>
        /// <returns></returns>
        public static DOutline GetOutline(uint uChar, Font font)
        {
            //����
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr hdc = g.GetHdc();

            //������ѡ�볡��
            IntPtr fontPtr = font.ToHfont();
            GdiNativeMethods.SelectObject(hdc, fontPtr);
            try
            {
                return GetOutline(hdc, uChar);
            }
            finally
            {
                //�ر����������ͷų���
                GdiNativeMethods.CloseHandle(fontPtr);
                g.ReleaseHdc(hdc);
            }
        }
    }
}
