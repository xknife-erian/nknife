
using System.Drawing;
namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 根据给定点查找直线 
    /// </summary>
    public class FindLineByPoint
    {
        #region 公共属性

        /// <summary>
        /// 获取或设置给定点
        /// </summary>
        public Point Point { get; set; }

        #endregion

        #region 构造函数

        public FindLineByPoint(Point point)
        {
            Point = point;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 寻找使得给定点在其start和end之间之分割线
        /// 寻找终点位于给定直线之分割线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePointTo(PartitionLine line)
        {
            if (line.IsRow)
            {
                return line.Start <= Point.X &&
                    line.End >= Point.X;
            }
            else
            {
                return line.Start <= Point.Y &&
                    line.End >= Point.Y;  
            }
        }

        /// <summary>
        /// 给定点在其上之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePointIn(PartitionLine line)
        {
            if (line.IsRow)
            {
                return line.Position == Point.Y &&
                    line.Start < Point.X &&
                    line.End > Point.X;
            }
            else
            {
                return line.Position == Point.X && 
                    line.Start < Point.Y &&
                    line.End > Point.Y;  
            }
        }

        #endregion
    }

    /// <summary>
    /// 根据给定直线来寻找直线
    /// </summary>
    public class FindLineByLine
    {
        #region 公共属性

        /// <summary>
        /// 给定的条件分割线
        /// </summary>
        public PartitionLine Line { get; set; }

        #endregion

        #region 构造函数

        public FindLineByLine(PartitionLine line)
        {
            this.Line = line;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 查找能与给定直线正交之分割线:lineToRect用：line在Line的"右边"(允许左有出头，但右必须出“出头”)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePartedLineRight(PartitionLine line)
        {
            return line.Start <= Line.Position &&
                line.End > Line.Position &&
                line.Position > Line.Start &&
                line.Position < Line.End &&
                line.IsRow != Line.IsRow;
        }

        /// <summary>
        /// 查找能与给定直线正交之分割线:line被Line分割
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePartedLine(PartitionLine line)
        {
            return line.Start < Line.Position &&
                line.End > Line.Position &&
                line.Position >= Line.Start &&
                line.Position <= Line.End &&
                line.IsRow != Line.IsRow;
        }

        /// <summary>
        /// 如果两直线的position相同,且有相交之处,则返回真
        /// 即,是否有重叠
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateOverlap(PartitionLine line)
        {
            return line.Position == Line.Position &&
                line.IsRow == Line.IsRow &&
                ((line.Start <= Line.Start && line.End >= Line.Start) ||
                (line.Start > Line.Start && Line.End >= line.Start));
        }

        /// <summary>
        /// 查找起于或终于给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateStartEndTo(PartitionLine line)
        {
            return line.IsRow != Line.IsRow &&
                ((line.End ==Line.Position  || line.Start == Line.Position) &&
                line.Position >= Line.Start &&
                line.Position <= Line.End  );
        }

        /// <summary>
        /// 查找Position大于等于给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePosGt(PartitionLine line)
        {
            return line.IsRow == Line.IsRow &&
                line.Position > Line.Position;
        }

        /// <summary>
        /// 查找Position 小于等于给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePosLt(PartitionLine line)
        {
            return line.IsRow == Line.IsRow &&
                line.Position < Line.Position;
        }

        /// <summary>
        /// 查找起点或终点与给定直线相等之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateStartEndEqual(PartitionLine line)
        {
            return line.IsRow == Line.IsRow &&
                (line.Start >= Line.Start ||
                line.End <= Line.End);
        }

        /// <summary>
        /// 查找终于给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateEndTo(PartitionLine line)
        {
            return line.IsRow != Line.IsRow &&
                line.End == Line.Position  &&
                line.Position >= Line.Start &&
                line.Position <= Line.End;
        }

        /// <summary>
        /// 查找起始于给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateStartFrom(PartitionLine line)
        {
            return line.IsRow != Line.IsRow &&
                line.Start == Line.Position &&
                line.Position >= Line.Start &&
                line.Position <= Line.End;
        }

        /// <summary>
        /// 查找相交成首尾相交相连之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateJoined(PartitionLine line)
        {
            return line.IsRow != Line.IsRow &&
                ((line.End == Line.Position || line.Start == Line.Position) &&
                (line.Position == Line.Start || line.Position == Line.End));
                //((line.End == Line.Position && (line.Position == Line.Start || line.Position == Line.End)) ||
                //(line.Start == Line.Position && (line.Position == Line.Start || line.Position == Line.End)));
        }

        /// <summary>
        /// 查找相交成首尾相交相连之直线.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateCutedLine(PartitionLine line)
        {
                return 
                    line.Start < Line.Position &&
                line.End > Line.Position &&
                line.Position >= Line.Start &&
                line.Position <= Line.End &&
                line.IsRow != Line.IsRow;
        }

        /// <summary>
        /// 查找包含给定直线之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateIncludeLine(PartitionLine line)
        {
            return
                line.IsRow == Line.IsRow &&
                line.Position == Line.Position &&
                line.Start <= Line.Start &&
                line.End >= Line.End;
        }

        /// <summary>
        /// 查找被给定直线包含之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateIncludedLine(PartitionLine line)
        {
            return
                line.IsRow == Line.IsRow &&
                line.Position == Line.Position &&
                line.Start >= Line.Start &&
                line.End <= Line.End;
        }

        /// <summary>
        /// 寻找终点位于给定直线之分割线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateLineTo(PartitionLine line)
        {
            return line.Position == Line.End &&
                line.Start < Line.Position &&
                line.End > Line.Position &&
                line.IsRow != Line.IsRow;
        }

        /// <summary>
        /// 查找和已知直线平行且有"重合"之直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateParallelLine(PartitionLine line)
        {
            return  line.IsRow == Line.IsRow && 
                line.Position != Line.Position &&
                !(line.Start>=Line.End ||
                line.End <= Line.Start
                );

        }

        #endregion
    }

    /// <summary>
    /// 根据给定矩形来寻找直线
    /// </summary>
    public class FindLineByRect
    {
        #region 公共属性

        /// <summary>
        /// 获取或设置给定矩形
        /// </summary>
        public Rectangle Rect { get; set; }

        #endregion

        #region 构造函数

        public FindLineByRect(Rectangle rect)
        {
            this.Rect = rect;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 查找与矩形有相交或被包含之切割线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateCutedRect(PartitionLine line)
        {
            if (line.IsRow )//是横向切割
            {
                return line.Position > Rect.Y &&
                    line.Position < Rect.Y + Rect.Height &&
                    !(
                    line.End<=Rect.X ||
                    line.Start>=Rect.X+Rect.Width
                    );
            }
            else
            {
                return line.Position > Rect.X &&
                    line.Position < Rect.X + Rect.Width && 
                    !(
                    line.End <= Rect.Y ||
                    line.Start >= Rect.Y + Rect.Height
                    );
            }
        }

        /// <summary>
        /// 是否在rect内部
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateInRect(PartitionLine line)
        {
            if (line.IsRow)//是横向切割
            {
                return line.Start >= Rect.X &&
                    line.End <= Rect.X + Rect.Width &&
                    line.Position > Rect.Y &&
                    line.Position < Rect.Y + Rect.Height;
            }
            else
            {
                return line.Start >= Rect.Y &&
                    line.End <= Rect.Y + Rect.Height &&
                    line.Position > Rect.X &&
                    line.Position < Rect.X + Rect.Width;

            }
        }
        
        #endregion
    }

    /// <summary>
    /// 寻找能以给定方向(isRow)切割给定矩形之分割线
    /// </summary>
    public class FindLindByRectAndRow
    {
        #region 公共属性

        /// <summary>
        /// 获取或设置给定的矩形
        /// </summary>
        public RectLayer Rect { get; set; }

        /// <summary>
        /// 获取或设置给定的方向
        /// </summary>
        public bool IsRow { get; set; }

        #endregion

        #region 构造函数

        public FindLindByRectAndRow(RectLayer rect, bool isRow)
        {
            this.Rect = rect;
            this.IsRow = isRow;
        }

        #endregion

        #region 公共方法

        //查找包含line的切割线
        public bool Predicate(PartitionLine line)
        {
            if (IsRow == true)//是横向切割
            {
                return line.IsRow == IsRow &&
                    line.Start <= Rect.X &&
                    line.End >= Rect.X + Rect.Width &&
                    line.Position >= Rect.Y &&
                    line.Position <= Rect.Y + Rect.Height ;
            }
            else
            {
                return line.IsRow == IsRow &&
                    line.Start <= Rect.Y &&
                    line.End >= Rect.Y + Rect.Height &&
                    line.Position >= Rect.X &&
                    line.Position <= Rect.X + Rect.Width;
                    
            }
        }

        #endregion
    }
}