using System.Runtime.InteropServices;

namespace NKnife.App.CharMatrix.Outline
{
    /// <summary>
    /// 定点数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FIXED
    {
        /// <summary>
        /// 小数部分
        /// </summary>
        public ushort fract;
        /// <summary>
        /// 整数部分
        /// </summary>
        public short value;
    }

    /// <summary>
    /// 转换矩阵
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
    /// 点结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// X值
        /// </summary>
        public int x;
        /// <summary>
        /// Y值
        /// </summary>
        public int y;
    }

    /// <summary>
    /// 图形信息结构
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
    /// 扩展点结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINTFX
    {
        /// <summary>
        /// X值
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED x;
        /// <summary>
        /// Y值
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public FIXED y;
    }

    /// <summary>
    /// 字形轮廓头结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYGONHEADER
    {
        /// <summary>
        /// 轮廓字节数
        /// </summary>
        public int cb;
        /// <summary>
        /// 轮廓类型
        /// </summary>
        public int dwType;
        /// <summary>
        /// 多边形的起始点
        /// </summary>
        [MarshalAs(UnmanagedType.Struct)]
        public POINTFX pfxStart;
    }

    /// <summary>
    /// 线段头结构，不包含点序列
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TTPOLYCURVEHEAD
    {
        /// <summary>
        /// 线类型，折线(TT_PRIM_LINE)=1;B样条曲线(TT_PRIM_OSPLINE)=2
        /// </summary>
        public short wType;
        /// <summary>
        /// 点序列中包含点的个数
        /// </summary>
        public short cpfx;
    }
}
