using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 分割线,包括其在其上的线段
    /// </summary>
    public class PartitionLine
    {
        #region 属性定义

        /// <summary>
        /// 获取或设置是否可移动
        /// </summary>
        public bool CanMove { get { return !IsLocked; } }

        /// <summary>
        /// 是否是行
        /// </summary>
        public bool IsRow { get; set; }

        /// <summary>
        /// 是否已锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 终点位置
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// 位置坐标，当是横线是此坐标为Y坐标，否则为X座标
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 子线段
        /// </summary>
        public SDLinkedList<PartitionLine> ChildLines { get; set; }

        /// <summary>
        /// 父直线
        /// </summary>
        public PartitionLine FatherLine { get; set; }

        /// <summary>
        /// 是否处于选择状态
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造父亲直线
        /// </summary>
        public PartitionLine(int start, int end, int position, bool isRow)
        {
            Start = start;
            End = end;
            Position = position;
            IsRow = isRow;
        }

        /// <summary>
        /// 根据父直线来构造子线段
        /// </summary>
        public PartitionLine(int start, int end, PartitionLine fatherLine)
        {
            Start = start;
            End = end;
            Position = fatherLine.Position;
            IsRow = fatherLine.IsRow;

            FatherLine = fatherLine;
            ChildLines = null;
        }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        public PartitionLine(PartitionLine line)
        {
            if (line == null)
            {
                return;
            }

            IsSelected = line.IsSelected;
            IsLocked = line.IsLocked;

            Start = line.Start;
            End = line.End;
            Position = line.Position;
            IsRow = line.IsRow;
            if (line.ChildLines != null)///拷贝孩子
            {
                ChildLines = new SDLinkedList<PartitionLine>();
                foreach (PartitionLine childLine in line.ChildLines)
                {
                    ChildLines.AddLast(new PartitionLine(childLine));
                }
            }
            if (line.FatherLine != null)
            {
                FatherLine = line.FatherLine;
            }
        }
        #endregion

        #region 公共函数接口

        /// <summary>
        /// 父直线被给定直线切割
        /// </summary>
        /// <param name="line">给定直线</param>
        public void PartitionByLine(PartitionLine line)
        {
            if (this.IsRow == line.IsRow || this.Start >= line.Position || this.End <= line.Position) return;///两条线并向或并未相交

            PartitionByPos(line.Position);
        }

        /// <summary>
        /// 根据位置来分割线段
        /// </summary>
        /// <param name="position"></param>
        public void PartitionByPos(int position)
        {
            Point point;
            if (IsRow)
            {
                point = new Point(position, Position);
            }
            else
            {
                point = new Point(Position, position);
            }
            ///如果没有孩子线段
            if (this.ChildLines == null)
            {
                ChildLines = new SDLinkedList<PartitionLine>();
                PartitionLine preLine = new PartitionLine(this.Start, position, this.Position, this.IsRow);
                PartitionLine aftLine = new PartitionLine(position, this.End, this.Position, this.IsRow);
                preLine.FatherLine = this;
                aftLine.FatherLine = this;
                ChildLines.AddFirst(preLine);
                ChildLines.AddLast(aftLine);
            }
            else
            {
                ///找到被切割的线段
                FindLineByPoint findByPoint = new FindLineByPoint(point);
                LinkedListNode<PartitionLine> resultNode = ChildLines.Find(findByPoint.PredicatePointIn);

                ///在被切割的线段前后添加一线段,同时删除原线段
                if (resultNode != null)
                {
                    PartitionLine preLine = new PartitionLine(resultNode.Value.Start, position, resultNode.Value.Position, this.IsRow);
                    PartitionLine aftLine = new PartitionLine(position, resultNode.Value.End, resultNode.Value.Position, this.IsRow);
                    preLine.FatherLine = this;
                    aftLine.FatherLine = this;
                    ChildLines.AddBefore(resultNode, preLine);
                    ChildLines.AddAfter(resultNode, aftLine);
                    ChildLines.Remove(resultNode);
                }

            }
        }

        /// <summary>
        /// 根据点分割线段
        /// </summary>
        /// <param name="point"></param>
        public void PartitionByPoint(Point point)
        {
            if (IsRow)
            {
                PartitionByPos(point.X);
            }
            else
            {
                PartitionByPos(point.Y);
            }
        }

        /// <summary>
        /// 是否可被选择
        /// </summary>
        public bool IsSelectable(Point point, int precision)
        {
            if (IsRow)
            {
                return Math.Abs(point.Y - Position) <= precision &&
                    point.X > Start &&
                    point.X < End;
            }
            else
            {
                return Math.Abs(point.X - Position) <= precision &&
                    point.Y > Start &&
                    point.Y < End;
            }
        }

        /// <summary>
        /// 是否可被给定矩形选择
        /// </summary>
        public bool IsSelectable(Rectangle rect)
        {
            if (IsRow)
            {
                return this.Start < rect.X &&
                    this.End > rect.X + rect.Width &&
                    this.Position > rect.Y &&
                    this.Position < rect.Y + rect.Height;
            }
            else
            {
                return this.Start < rect.Y &&
                    this.End > rect.Y + rect.Height &&
                    this.Position > rect.X &&
                    this.Position < rect.X + rect.Width;
            }
        }

        /// <summary>
        /// 得到可被选择的子线段
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">选择精度</param>
        /// <returns></returns>
        public PartitionLine SelectableChildLine(Point point, int selectPrecision)
        {
            if (ChildLines == null)
            {
                if (IsSelectable(point, selectPrecision))
                {
                    return this;
                }
            }
            else
            {
                foreach (PartitionLine childLine in ChildLines)
                {
                    if (childLine.IsSelectable(point, selectPrecision))
                    {
                        return childLine;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 得到被选择之子线段数组
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">选择精度</param>
        /// <returns></returns>
        public List<PartitionLine> GetSelectedChildLines()
        {
            List<PartitionLine> selectedChildLines = new List<PartitionLine>();
            if (ChildLines != null)
            {
                foreach (PartitionLine childLine in ChildLines)
                {
                    if (childLine.IsSelected)
                    {
                        selectedChildLines.Add(childLine);
                    }
                }
            }
            return selectedChildLines;
        }

        /// <summary>
        /// 返回不重叠部分直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<PartitionLine> GetNotOverlapLine(List<PartitionLine> lines)
        {
            List<PartitionLine> resultLines = new List<PartitionLine>();
            ///先将lines按start排序
            int start = Start;
            lines.Sort(new CompareLineStart());
            foreach (PartitionLine line in lines)
            {
                if (start < line.Start)
                {
                    resultLines.Add(new PartitionLine(start, line.Start, line.Position, line.IsRow));
                    start = line.End;
                }
                else
                {
                    start = line.End;
                }
            }
            if (lines[lines.Count - 1].End < this.End)
            {
                resultLines.Add(new PartitionLine(lines[lines.Count - 1].End, this.End, this.Position, this.IsRow));
            }
            return resultLines;
        }

        /// <summary>
        /// 将line1,this，line2合并
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        internal PartitionLine MergeOverlapLine(PartitionLine line1, PartitionLine line2)
        {
            ///找到start较小的直线
            PartitionLine firstLine, secondLine;

            if (line1.Start < line2.Start)
            {
                firstLine = line1;
                secondLine = line2;
            }
            else
            {
                firstLine = line2;
                secondLine = line1;
            }
            firstLine.MergeOverlapLine(this);
            firstLine.MergeOverlapLine(secondLine);

            return secondLine;
        }

        /// <summary>
        /// 合并两根重叠的直线
        /// </summary>
        /// <param name="line"></param>
        public void MergeOverlapLine(PartitionLine line)
        {
            ///this在前
            if (line.Start == this.End)
            {
                if (this.ChildLines != null)///如果有孩子,则把孩子合并
                {
                    this.End = line.End;
                    if (line.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            childLine.FatherLine = this;
                            this.ChildLines.AddLast(childLine);
                        }
                    }
                    else
                    {
                        line.FatherLine = this;
                        this.ChildLines.AddLast(line);
                    }
                }
                else///如果本身也没有孩子,则先创建孩子,再合并
                {
                    PartitionLine selfChildLine = new PartitionLine(this);
                    this.End = line.End;
                    this.ChildLines = new SDLinkedList<PartitionLine>();
                    ChildLines.AddFirst(selfChildLine);///将自己拷贝一份并使其成为自己的孩子
                    if (line.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            childLine.FatherLine = this;
                            this.ChildLines.AddLast(childLine);
                        }
                    }
                    else
                    {
                        line.FatherLine = this;
                        this.ChildLines.AddLast(line);
                    }
                }
                line.ChildLines = null;///断开子孩子链接
            }
            else
            {
                if (this.ChildLines != null)///如果有孩子,则把孩子合并
                {
                    this.Start = line.Start;
                    if (line.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            childLine.FatherLine = this;
                            this.ChildLines.AddFirst(childLine);
                        }
                    }
                    else
                    {
                        line.FatherLine = this;
                        this.ChildLines.AddFirst(line);
                    }
                }
                else
                {
                    PartitionLine selfChildLine = new PartitionLine(this);
                    this.Start = line.Start;
                    this.ChildLines = new SDLinkedList<PartitionLine>();
                    ChildLines.AddFirst(selfChildLine);///将自己拷贝一份并使其成为自己的孩子
                    if (line.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            childLine.FatherLine = this;
                            this.ChildLines.AddFirst(childLine);
                        }
                    }
                    else
                    {
                        line.FatherLine = this;
                        this.ChildLines.AddLast(line);
                    }

                }
                line.ChildLines = null;
            }
        }

        /// <summary>
        /// 根据中间坐标合并两个孩子线段
        /// </summary>
        /// <param name="position">将合并的两孩子线段之分割点</param>
        public void MergeByPos(int partitionPos)
        {
            ///当孩子为空或分割点位于线段两端时,直接返回
            if (ChildLines == null || partitionPos <= Start || partitionPos >= End)
            {
                return;
            }
            LinkedListNode<PartitionLine> lineNode = ChildLines.First;
            for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)///作while取代会更好一些
            {
                if (lineNode.Value.End != partitionPos)
                {
                    continue;
                }
                lineNode.Value.End = lineNode.Next.Value.End;///合并后删除
                ChildLines.Remove(lineNode.Next);
                break;
            }
        }

        /// <summary>
        /// 根据中间坐标合并两个孩子线段
        /// </summary>
        /// <param name="point"></param>
        public void MergeByPoint(Point point)
        {
            if (IsRow)
            {
                MergeByPos(point.X);
            }
            else
            {
                MergeByPos(point.Y);
            }
        }

        /// <summary>
        /// 删除孩子线段
        /// </summary>
        /// <param name="line"></param>
        /// <returns>返回line后的线,如果为null,表明line为this的最后一节线段或首线段</returns>
        public PartitionLine RemoveChildLine(PartitionLine line)
        {
            ///当孩子为空或分割点位于线段两端时,直接返回
            if (ChildLines == null || this.IsRow != line.IsRow || this.Position != line.Position)
            {
                return null;
            }
            if (line.Start == this.Start)///待删除线段是本线之起始线段
            {
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                LinkedListNode<PartitionLine> nextNode;
                do
                {
                    nextNode = lineNode.Next;
                    this.ChildLines.Remove(lineNode);
                    lineNode = nextNode;
                } while (lineNode != null && lineNode.Value.Start < line.End);///删除线段，直到待删除之直线的起始位置等于line.end
                this.Start = line.End;
                return null;
            }
            else if (line.End == this.End)///line即为this的最后一个或一系列线段
            {
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)
                {
                    if (lineNode.Value.Start == line.Start)
                    {
                        LinkedListNode<PartitionLine> nextNode;
                        ///删除所有与line重合的子线段
                        while (lineNode != null)
                        {
                            nextNode = lineNode.Next;///保存其下一个结点 
                            this.ChildLines.Remove(lineNode);
                            lineNode = nextNode;
                        }
                        break;
                    }
                }
                this.End = line.Start;
                return null;
            }
            else/// (line.End < this.End)///删除后分成两个线段
            {
                PartitionLine newLine = new PartitionLine(line.End, this.End, this.Position, this.IsRow);
                newLine.ChildLines = new SDLinkedList<PartitionLine>();
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)
                {
                    if (lineNode.Value.Start == line.Start)
                    {
                        LinkedListNode<PartitionLine> nextNode;
                        ///删除所有与line重合的子线段
                        while (lineNode != null)
                        {
                            nextNode = lineNode.Next;///保存其下一个结点 
                            if (lineNode.Value.Start >= line.End)///从此处开始加入新的链表
                            {
                                lineNode.Value.FatherLine = lineNode.Value;
                                newLine.ChildLines.AddLast(lineNode.Value);///链接到新的链表
                            }
                            this.ChildLines.Remove(lineNode);
                            lineNode = nextNode;
                        }
                        break;
                    }
                }
                this.End = line.Start;
                return newLine;
            }
        }

        /// <summary>
        /// 锁定直线,同时锁住其子线段
        /// </summary>
        public void LockLine()
        {
            this.IsLocked = true;
            if (ChildLines != null)
            {
                foreach (PartitionLine line in ChildLines)
                {
                    line.IsLocked = true;
                }
            }
        }

        /// <summary>
        /// 解开锁定的直线
        /// </summary>
        public void UnLockLine()
        {
            this.IsLocked = false;
            if (ChildLines != null)
            {
                foreach (PartitionLine line in ChildLines)
                {
                    line.IsLocked = false;
                }
            }
        }

        /// <summary>
        /// 选择直线,假设调用前已判断可被选择
        /// </summary>
        public void SelectLine()
        {
            this.IsSelected = true;
            /////设置其子线段不被选择
            //if (ChildLines!=null)
            //{
            //    foreach (PartitionLine childLine in ChildLines)
            //    {
            //        childLine.IsSelected = false;
            //    }
            //}
            //else///如果是子线段,则设置其父亲不被选择
            //{
            //    FatherLine.IsSelected=false;
            //}            
        }

        /// <summary>
        /// 撤销选择直线
        /// </summary>
        public void UnSelectLine()
        {
            this.IsSelected = false;
        }

        /// <summary>
        /// 是否在rect内部
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IsInRect(Rectangle rect)
        {
            if (this.IsRow)//是横向切割
            {
                return this.Start >= rect.X &&
                    this.End <= rect.X + rect.Width &&
                    this.Position > rect.Y &&
                    this.Position < rect.Y + rect.Height;
            }
            else
            {
                return this.Start >= rect.Y &&
                    this.End <= rect.Y + rect.Height &&
                    this.Position > rect.X &&
                    this.Position < rect.X + rect.Width;
            }
        }

        /// <summary>
        /// 得到被选择之子线段数组
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">选择精度</param>
        /// <returns></returns>
        public PartitionLine GetSelectedChildLine(Point point, int selectPrecision)
        {
            if (ChildLines == null)
            {
                if (IsSelectable(point, selectPrecision))
                {
                    return this;
                }
            }
            else
            {
                foreach (PartitionLine childLine in ChildLines)
                {
                    if (childLine.IsSelectable(point, selectPrecision))
                    {
                        return childLine;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 得到所有被选择的直线的子线段集
        /// </summary>
        /// <returns></returns>
        internal List<PartitionLine> GetSelectedChildLine()
        {
            List<PartitionLine> selectedChildLines = new List<PartitionLine>();
            if (ChildLines != null)
            {
                foreach (PartitionLine childLine in ChildLines)
                {
                    if (childLine.IsSelected)
                    {
                        selectedChildLines.Add(childLine);
                    }
                }
            }
            return selectedChildLines;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PartitionLine line2 = (PartitionLine)obj;

            if (this.IsRow != line2.IsRow)
            {
                return false;
            }

            if (this.Start != line2.Start)
            {
                return false;
            }

            if (this.End != line2.End)
            {
                return false;
            }

            if (this.Position != line2.Position)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Position;
        }
    }
}
