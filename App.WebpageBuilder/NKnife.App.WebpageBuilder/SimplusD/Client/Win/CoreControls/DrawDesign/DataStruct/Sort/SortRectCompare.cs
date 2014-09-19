using System.Collections.Generic;
using System;
using System.Drawing;
namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 根据rect.x来排序
    /// </summary>
    public class CompareRectX : IComparer<Rect>
    {
        /// <summary>
        /// 根据rect.x来排序
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
    /// 根据rect.y来排序
    /// </summary>
    public class CompareRectY : IComparer<Rect>
    {
        /// <summary>
        /// 根据rect.y来排序
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
    /// 根据Right
    /// </summary>
    public class CompareRectRight : IComparer<Rect>
    {
        /// <summary>
        /// 根据Right来排序
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
    /// 根据bottom
    /// </summary>
    public class CompareRectBottom : IComparer<Rect>
    {
        /// <summary>
        /// 根据Right来排序
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