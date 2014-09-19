using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// �ָ���,�����������ϵ��߶�
    /// </summary>
    public class PartitionLine
    {
        #region ���Զ���

        /// <summary>
        /// ��ȡ�������Ƿ���ƶ�
        /// </summary>
        public bool CanMove { get { return !IsLocked; } }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsRow { get; set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// ��ʼλ��
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// �յ�λ��
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// λ�����꣬���Ǻ����Ǵ�����ΪY���꣬����ΪX����
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// ���߶�
        /// </summary>
        public SDLinkedList<PartitionLine> ChildLines { get; set; }

        /// <summary>
        /// ��ֱ��
        /// </summary>
        public PartitionLine FatherLine { get; set; }

        /// <summary>
        /// �Ƿ���ѡ��״̬
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���츸��ֱ��
        /// </summary>
        public PartitionLine(int start, int end, int position, bool isRow)
        {
            Start = start;
            End = end;
            Position = position;
            IsRow = isRow;
        }

        /// <summary>
        /// ���ݸ�ֱ�����������߶�
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
        /// �������캯��
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
            if (line.ChildLines != null)///��������
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

        #region ���������ӿ�

        /// <summary>
        /// ��ֱ�߱�����ֱ���и�
        /// </summary>
        /// <param name="line">����ֱ��</param>
        public void PartitionByLine(PartitionLine line)
        {
            if (this.IsRow == line.IsRow || this.Start >= line.Position || this.End <= line.Position) return;///�����߲����δ�ཻ

            PartitionByPos(line.Position);
        }

        /// <summary>
        /// ����λ�����ָ��߶�
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
            ///���û�к����߶�
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
                ///�ҵ����и���߶�
                FindLineByPoint findByPoint = new FindLineByPoint(point);
                LinkedListNode<PartitionLine> resultNode = ChildLines.Find(findByPoint.PredicatePointIn);

                ///�ڱ��и���߶�ǰ�����һ�߶�,ͬʱɾ��ԭ�߶�
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
        /// ���ݵ�ָ��߶�
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
        /// �Ƿ�ɱ�ѡ��
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
        /// �Ƿ�ɱ���������ѡ��
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
        /// �õ��ɱ�ѡ������߶�
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">ѡ�񾫶�</param>
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
        /// �õ���ѡ��֮���߶�����
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">ѡ�񾫶�</param>
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
        /// ���ز��ص�����ֱ��
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<PartitionLine> GetNotOverlapLine(List<PartitionLine> lines)
        {
            List<PartitionLine> resultLines = new List<PartitionLine>();
            ///�Ƚ�lines��start����
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
        /// ��line1,this��line2�ϲ�
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        internal PartitionLine MergeOverlapLine(PartitionLine line1, PartitionLine line2)
        {
            ///�ҵ�start��С��ֱ��
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
        /// �ϲ������ص���ֱ��
        /// </summary>
        /// <param name="line"></param>
        public void MergeOverlapLine(PartitionLine line)
        {
            ///this��ǰ
            if (line.Start == this.End)
            {
                if (this.ChildLines != null)///����к���,��Ѻ��Ӻϲ�
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
                else///�������Ҳû�к���,���ȴ�������,�ٺϲ�
                {
                    PartitionLine selfChildLine = new PartitionLine(this);
                    this.End = line.End;
                    this.ChildLines = new SDLinkedList<PartitionLine>();
                    ChildLines.AddFirst(selfChildLine);///���Լ�����һ�ݲ�ʹ���Ϊ�Լ��ĺ���
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
                line.ChildLines = null;///�Ͽ��Ӻ�������
            }
            else
            {
                if (this.ChildLines != null)///����к���,��Ѻ��Ӻϲ�
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
                    ChildLines.AddFirst(selfChildLine);///���Լ�����һ�ݲ�ʹ���Ϊ�Լ��ĺ���
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
        /// �����м�����ϲ����������߶�
        /// </summary>
        /// <param name="position">���ϲ����������߶�֮�ָ��</param>
        public void MergeByPos(int partitionPos)
        {
            ///������Ϊ�ջ�ָ��λ���߶�����ʱ,ֱ�ӷ���
            if (ChildLines == null || partitionPos <= Start || partitionPos >= End)
            {
                return;
            }
            LinkedListNode<PartitionLine> lineNode = ChildLines.First;
            for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)///��whileȡ�������һЩ
            {
                if (lineNode.Value.End != partitionPos)
                {
                    continue;
                }
                lineNode.Value.End = lineNode.Next.Value.End;///�ϲ���ɾ��
                ChildLines.Remove(lineNode.Next);
                break;
            }
        }

        /// <summary>
        /// �����м�����ϲ����������߶�
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
        /// ɾ�������߶�
        /// </summary>
        /// <param name="line"></param>
        /// <returns>����line�����,���Ϊnull,����lineΪthis�����һ���߶λ����߶�</returns>
        public PartitionLine RemoveChildLine(PartitionLine line)
        {
            ///������Ϊ�ջ�ָ��λ���߶�����ʱ,ֱ�ӷ���
            if (ChildLines == null || this.IsRow != line.IsRow || this.Position != line.Position)
            {
                return null;
            }
            if (line.Start == this.Start)///��ɾ���߶��Ǳ���֮��ʼ�߶�
            {
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                LinkedListNode<PartitionLine> nextNode;
                do
                {
                    nextNode = lineNode.Next;
                    this.ChildLines.Remove(lineNode);
                    lineNode = nextNode;
                } while (lineNode != null && lineNode.Value.Start < line.End);///ɾ���߶Σ�ֱ����ɾ��ֱ֮�ߵ���ʼλ�õ���line.end
                this.Start = line.End;
                return null;
            }
            else if (line.End == this.End)///line��Ϊthis�����һ����һϵ���߶�
            {
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)
                {
                    if (lineNode.Value.Start == line.Start)
                    {
                        LinkedListNode<PartitionLine> nextNode;
                        ///ɾ��������line�غϵ����߶�
                        while (lineNode != null)
                        {
                            nextNode = lineNode.Next;///��������һ����� 
                            this.ChildLines.Remove(lineNode);
                            lineNode = nextNode;
                        }
                        break;
                    }
                }
                this.End = line.Start;
                return null;
            }
            else/// (line.End < this.End)///ɾ����ֳ������߶�
            {
                PartitionLine newLine = new PartitionLine(line.End, this.End, this.Position, this.IsRow);
                newLine.ChildLines = new SDLinkedList<PartitionLine>();
                LinkedListNode<PartitionLine> lineNode = ChildLines.First;
                for (int i = 0; i < ChildLines.Count; i++, lineNode = lineNode.Next)
                {
                    if (lineNode.Value.Start == line.Start)
                    {
                        LinkedListNode<PartitionLine> nextNode;
                        ///ɾ��������line�غϵ����߶�
                        while (lineNode != null)
                        {
                            nextNode = lineNode.Next;///��������һ����� 
                            if (lineNode.Value.Start >= line.End)///�Ӵ˴���ʼ�����µ�����
                            {
                                lineNode.Value.FatherLine = lineNode.Value;
                                newLine.ChildLines.AddLast(lineNode.Value);///���ӵ��µ�����
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
        /// ����ֱ��,ͬʱ��ס�����߶�
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
        /// �⿪������ֱ��
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
        /// ѡ��ֱ��,�������ǰ���жϿɱ�ѡ��
        /// </summary>
        public void SelectLine()
        {
            this.IsSelected = true;
            /////���������߶β���ѡ��
            //if (ChildLines!=null)
            //{
            //    foreach (PartitionLine childLine in ChildLines)
            //    {
            //        childLine.IsSelected = false;
            //    }
            //}
            //else///��������߶�,�������丸�ײ���ѡ��
            //{
            //    FatherLine.IsSelected=false;
            //}            
        }

        /// <summary>
        /// ����ѡ��ֱ��
        /// </summary>
        public void UnSelectLine()
        {
            this.IsSelected = false;
        }

        /// <summary>
        /// �Ƿ���rect�ڲ�
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool IsInRect(Rectangle rect)
        {
            if (this.IsRow)//�Ǻ����и�
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
        /// �õ���ѡ��֮���߶�����
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPrecision">ѡ�񾫶�</param>
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
        /// �õ����б�ѡ���ֱ�ߵ����߶μ�
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
