using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 根据给定矩形寻找页面矩形SnipRect(SRect)
    /// </summary>
    public class FindRectByRect
    {
        #region 公共属性

        ///
        ///获取或设置给定的条件矩形
        ///
        public Rectangle Rect { get; set; }

        #endregion

        #region 构造函数

        public FindRectByRect(Rectangle rect)
        {
            Rect = rect;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 寻找位于Rectangle rect内部之snipRect
        /// </summary>
        public bool FindInRectPredicate(Rect rect)
        {
            return  !rect.IsDeleted &&
                rect.X >= Rect.X &&
                rect.Y >= Rect.Y &&
                rect.X + rect.Width <= Rect.Right &&
                rect.Y + rect.Height <= Rect.Bottom;
        }

        /// <summary>
        /// 寻找可被矩形框选择的矩形：在矩形框内部或与矩形框相交。
        /// </summary>
        public bool FindSelectableRectPredicate(Rect rect)
        {
            return !(rect.X > Rect.X+Rect.Width ||
                rect.X +rect.Width < Rect.X ||
                rect.Y > Rect.Y+Rect.Height ||
                rect.Y + rect.Height < Rect.Y);
        }

        /// <summary>
        /// 寻找位于Rectangle rect内部之snipRect
        /// </summary>
        public bool FindEqualRect(Rect sRect)
        {
            return !sRect.IsDeleted &&
                sRect.X == Rect.X &&
                sRect.Y == Rect.Y &&
                sRect.Width == Rect.Width &&
                sRect.Height == Rect.Height;
        }

        #endregion
    }

    /// <summary>
    /// 根据给定矩形寻找页面矩形SnipRect(SRect)
    /// </summary>
    public class FindRectByLayerRect
    {
        #region 公共属性

        ///
        ///获取或设置给定的条件矩形
        ///
        public RectLayer Rect { get; set; }

        #endregion

        #region 构造函数

        public FindRectByLayerRect(RectLayer rect)
        {
            Rect = rect;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 寻找位于Rectangle rect内部之snipRect
        /// </summary>
        /// <param name="sRect"></param>
        /// <returns></returns>
        public bool PredicateEqualRect(Rect snipRect)
        {
            return !snipRect.IsDeleted &&
                snipRect.X == Rect.X &&
                snipRect.Y == Rect.Y &&
                snipRect.Width == Rect.Width &&
                snipRect.Height == Rect.Height;
        }

        #endregion
    }

    /// <summary>
    /// 根据给定分割线寻找页面矩形SnipRect(SRect)
    /// </summary>
    public class FindRectByLine
    {
        #region 公共属性

        ///
        ///给定的条件分割线
        ///
        public PartitionLine Line { get; set; }

        #endregion

        #region 构造函数

        public FindRectByLine(PartitionLine line)
        {
            Line = line;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 寻找可被line分割的矩形
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PartitionLinePredicate(Rect sRect)
        {
            if (Line.IsRow)
            {
                return (!sRect.IsDeleted) &&
                    sRect.Y < Line.Position &&
                    sRect.Y + sRect.Height > Line.Position &&
                    sRect.X >= Line.Start &&
                    sRect.X + sRect.Width <= Line.End;
            }
            else
            {
                return (!sRect.IsDeleted) &&
                    sRect.X < Line.Position &&
                    sRect.X + sRect.Width > Line.Position &&
                    sRect.Y >= Line.Start &&
                    sRect.Y + sRect.Height <= Line.End;
            }
        }

        /// <summary>
        /// 找到所有以line做边界线的矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool BorderLinePredicate(Rect rect)
        {
            if (Line.IsRow)
            {
                return ((!rect.IsDeleted) &&
                    Line.Position == rect.Y &&
                    Line.Start <= rect.X &&
                    Line.End >= rect.X + rect.Width) ||
                    ((!rect.IsDeleted) && 
                    Line.Position == (rect.Y + rect.Height) &&
                    Line.Start <= rect.X &&
                    Line.End >= (rect.X + rect.Width)
                    );
            }
            else
            {
                return (
                    (!rect.IsDeleted )&&
                    Line.Position == rect.X&&
                    Line.Start <= rect.Y &&
                    Line.End >= rect.Y + rect.Height) ||
                    ((!rect.IsDeleted) && 
                    Line.Position == (rect.X + rect.Width) &&
                    Line.Start <= rect.Y&&
                    Line.End >= (rect.Y + rect.Height)
                    );
            }
        }

        #endregion
    }
}