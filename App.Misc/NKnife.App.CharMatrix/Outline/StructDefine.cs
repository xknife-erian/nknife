using System.Runtime.InteropServices;

namespace NKnife.App.CharMatrix.Outline
{
    /// <summary>
    /// ������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FIXED
    {
        /// <summary>
        /// С������
        /// </summary>
        public ushort fract;
        /// <summary>
        /// ��������
        /// </summary>
        public short value;
    }

    /// <summary>
    /// ת������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MAT2
    {
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM11;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM12;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM21;
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED eM22;
    }

    /// <summary>
    /// ��ṹ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// Xֵ
        /// </summary>
        public int x;
        /// <summary>
        /// Yֵ
        /// </summary>
        public int y;
    }

    /// <summary>
    /// ͼ����Ϣ�ṹ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GLYPHMETRICS
    {
        public uint gmBlackBoxX;
        public uint gmBlackBoxY;
        [MarshalAs(UnmanagedType.Struct)]
        public POINT gmptGlyphOrigin;
        public short gmCellIncX;
        public short gmCellIncY;
    }

    /// <summary>
    /// ��չ��ṹ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTFX
    {
        /// <summary>
        /// Xֵ
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED x;
        /// <summary>
        /// Yֵ
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED y;
    }

    /// <summary>
    /// ��������ͷ�ṹ
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYGONHEADER
    {
        /// <summary>
        /// �����ֽ���
        /// </summary>
        public int cb;
        /// <summary>
        /// ��������
        /// </summary>
        public int dwType;
        /// <summary>
        /// ����ε���ʼ��
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public POINTFX pfxStart;
    }

    /// <summary>
    /// �߶�ͷ�ṹ��������������
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYCURVEHEAD
    {
        /// <summary>
        /// �����ͣ�����(TT_PRIM_LINE)=1;B��������(TT_PRIM_OSPLINE)=2
        /// </summary>
        public short wType;
        /// <summary>
        /// �������а�����ĸ���
        /// </summary>
        public short cpfx;
    }
}
