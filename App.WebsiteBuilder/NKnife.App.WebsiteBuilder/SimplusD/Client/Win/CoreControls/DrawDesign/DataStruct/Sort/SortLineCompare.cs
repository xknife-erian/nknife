using System.Collections.Generic;
using System;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 根据分割线之position值来排序
    /// </summary>
    public class CompareLinePosition : IComparer<PartitionLine>
    {
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.Position - y.Position;
        }
    }

    /// <summary>
    /// 根据分割线之position值来排序
    /// </summary>
    public class CompareLineLen : IComparer<PartitionLine>
    { 
        /// <summary>
        /// 从startLine的otherPos开始计算长度
        /// </summary>  
        public PartitionLine StartLine { get; set; }

        #region 构造函数

        public CompareLineLen(PartitionLine line)
        {
            StartLine = line;
        }

        #endregion

        //逆向排序,即长的在前
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return (y.End - StartLine.Position) - (x.End - StartLine.Position);
        }
    }

    /// <summary>
    /// 根据两条直线之start来排序
    /// </summary>
    public class CompareLineStart : IComparer<PartitionLine>
    {
        //逆向排序,即长的在前
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.Start - y.Start;
        }
    }

    /// <summary>
    /// 根据两条直线之end来排序
    /// </summary>
    public class CompareLineEnd : IComparer<PartitionLine>
    {
        //逆向排序,即长的在前
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.End - y.End;
        }
    }

    /// <summary>
    /// 根据与给定直线的距离来排序
    /// </summary>
    public class CompareLLDistance : IComparer<PartitionLine>
    {
        PartitionLine _startLine; /// 从startLine的otherPos开始计算长度
        /// 
        /// <summary>
        /// 从startLine的otherPos开始计算长度
        /// </summary>  
        public PartitionLine StartLine
        {
            get { return _startLine; }
            set { _startLine = value; }
        }

        public CompareLLDistance(PartitionLine line)
        {
            StartLine = line;
        }
        /// <summary>
        /// 比较与给定直线的距离,用于排序,越与给定直线近的越在前
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return Math.Abs(x.Position - StartLine.Position) - Math.Abs(y.Position - StartLine.Position);
        }
    }

    /// <summary>
    /// 根据与给定点的距离来排序,直线与点的距离
    /// </summary>
    public class CompareLPDistance : IComparer<PartitionLine>
    {
        Point startPoint; /// 从startPoint到otherPos开始计算距离

        /// <summary>
        /// 从startPoint的otherPos开始计算距离
        /// </summary>  
        public Point StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public CompareLPDistance(Point point)
        {
            StartPoint = point;
        }
        /// <summary>
        /// 比较与给定直线的距离,用于排序,越与给定直线近的越在前
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(PartitionLine x, PartitionLine y)
        {
            if (x.IsRow && y.IsRow)
                return Math.Abs(x.Position - StartPoint.Y)
                    - Math.Abs(y.Position - StartPoint.Y);
            else if ((!x.IsRow) && (!y.IsRow))
            {
                return Math.Abs(x.Position - StartPoint.X)
                    - Math.Abs(y.Position - StartPoint.X);
            }
            else if (x.IsRow && (!y.IsRow))
            {
                return Math.Abs(x.Position - StartPoint.Y)
                     - Math.Abs(y.Position - StartPoint.X);
            }
            else
	        {
                return Math.Abs(x.Position - StartPoint.X)
                    - Math.Abs(y.Position - StartPoint.X);
	        }                        
        }
    }
}