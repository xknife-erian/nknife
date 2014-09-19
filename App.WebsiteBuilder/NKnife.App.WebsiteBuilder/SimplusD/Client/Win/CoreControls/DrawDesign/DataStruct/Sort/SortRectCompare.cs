using System.Collections.Generic;
using System;
using System.Drawing;
namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ����rect.x������
    /// </summary>
    public class CompareRectX : IComparer<Rect>
    {
        /// <summary>
        /// ����rect.x������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Rect x, Rect y)
        {
            return x.X - y.X;
        }
    }

    /// <summary>
    /// ����rect.y������
    /// </summary>
    public class CompareRectY : IComparer<Rect>
    {
        /// <summary>
        /// ����rect.y������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Rect x, Rect y)
        {
            return x.Y - y.Y;
        }
    }

    /// <summary>
    /// ����Right
    /// </summary>
    public class CompareRectRight : IComparer<Rect>
    {
        /// <summary>
        /// ����Right������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Rect x, Rect y)
        {
            return (x.X+x.Width)-(y.X+y.Width);
        }
    }

    /// <summary>
    /// ����bottom
    /// </summary>
    public class CompareRectBottom : IComparer<Rect>
    {
        /// <summary>
        /// ����Right������
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Rect x, Rect y)
        {
            return (x.Y  + x.Height) - (y.Y + y.Height);
        }
    }

}