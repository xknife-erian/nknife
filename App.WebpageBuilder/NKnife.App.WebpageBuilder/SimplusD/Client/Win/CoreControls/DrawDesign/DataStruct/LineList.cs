using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 保存所有分割线与线段
    /// </summary>
    public class LineList
    {
        #region 属性定义
        /// <summary>
        /// 横向直线
        /// </summary>
        public SDList<PartitionLine> HPartionLines { get; set; }

        /// <summary>
        /// 纵向直线
        /// </summary>
        public SDList<PartitionLine> VPartionLines { get; set; }

        /// <summary>
        /// 四条边界直线
        /// </summary>
        public SDList<PartitionLine> BorderLines { get; set; }

        #endregion

        #region 构造函数

        public LineList()
        {
            HPartionLines = new SDList<PartitionLine>();
            VPartionLines = new SDList<PartitionLine>();
            BorderLines = new SDList<PartitionLine>();
        }

        #endregion

        #region 公共成员接口

        public void AddLine(int start, int end, int position, bool isRow)
        {
            PartitionLine line = new PartitionLine(start, end, position, isRow);
            List<PartitionLine> resultLines;  ///找到与给定直线正交之分割线的结果集
            List<PartitionLine> overLapLine; ///被重叠的直线
            ///找到所有被line切割的分割线
            FindLineByLine findLinePartion = new FindLineByLine(line);
            if (line.IsRow)
            {
                resultLines = VPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    ///处理被其切割了其他的直线,同时处理自己也被切割了的情形
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        resultLines[i].PartitionByLine(line);
                        line.PartitionByLine(resultLines[i]);
                    }
                }
                ///添加新绘之直线,先判断是否有重叠直线
                FindLineByLine findByLine = new FindLineByLine(line);
                overLapLine = HPartionLines.FindAll(findByLine.PredicateOverlap);
                if (overLapLine.Count == 0)
                {
                    HPartionLines.Add(line);
                }
                else if (overLapLine.Count == 1)
                {
                    overLapLine[0].MergeOverlapLine(line);
                }
                else
                {
                    PartitionLine removedLine = line.MergeOverlapLine(overLapLine[0], overLapLine[1]);
                    HPartionLines.Remove(removedLine);
                }
            }
            else
            {
                resultLines = HPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    ///处理被其切割了其他的直线,同时处理自己也被切割了的情形
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        resultLines[i].PartitionByLine(line);
                        line.PartitionByLine(resultLines[i]);
                    }
                }
                ///添加新绘之直线,先判断是否有重叠直线
                FindLineByLine findByLine = new FindLineByLine(line);
                overLapLine = VPartionLines.FindAll(findByLine.PredicateOverlap);
                if (overLapLine.Count == 0)
                {
                    VPartionLines.Add(line);
                }
                else if (overLapLine.Count == 1)
                {
                    overLapLine[0].MergeOverlapLine(line);
                }
                else
                {
                    PartitionLine removedLine = line.MergeOverlapLine(overLapLine[0], overLapLine[1]);
                    VPartionLines.Remove(removedLine);
                }

            }
        }

        /// <summary>
        /// 撤销添加分割线
        /// </summary>
        /// <param name="line"></param>
        public void UnAddLine(int start, int end, int position, bool isRow)
        {
            DeleteLine(start, end, position, isRow);
        }

        /// <summary>
        /// 删除直线
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLine(int start, int end, int position, bool isRow)
        {
            List<PartitionLine> lines = GetLine(new PartitionLine(start, end, position, isRow));
            foreach (PartitionLine line in lines)
            {
                DeleteLine(line);
            }
        }

        /// <summary>
        /// 删除直线
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLine(PartitionLine line)
        {
            List<PartitionLine> resultLines;  ///找到与给定直线正交之分割线的结果集
            ///找到所有被line切割的分割线
            FindLineByLine findLinePartion = new FindLineByLine(line);
            if (line.IsRow)///横向直线
            {
                resultLines = VPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        ///合并孩子线段
                        if (line.FatherLine == null
                            || (line.FatherLine != null &&
                            (line.FatherLine.Start == resultLines[i].Position || line.FatherLine.End == resultLines[i].Position)
                            ))
                        {
                            resultLines[i].MergeByPos(line.Position);
                        }

                    }
                }
                ///不能简单的HPartionLines.Remove(line),此处只用的为line中的数据,而非引用.因为有可能
                ///HPartionLines.Remove(line);

                FindLineByLine findByLine = new FindLineByLine(line);
                PartitionLine overLapLine = HPartionLines.Find(findByLine.PredicateIncludeLine);
                if (overLapLine.Start == line.Start && overLapLine.End == line.End)///如果是同一条线
                {
                    HPartionLines.Remove(overLapLine);
                }
                else
                {
                    PartitionLine newLine = overLapLine.RemoveChildLine(line);
                    if (newLine != null)///如果产生了新的线段
                    {
                        this.HPartionLines.Add(newLine);
                    }
                    if (overLapLine.ChildLines != null && overLapLine.ChildLines.Count == 0)
                    {
                        overLapLine.ChildLines.AddFirst(
                            new LinkedListNode<PartitionLine>(
                            new PartitionLine(
                            overLapLine.Start, 
                            overLapLine.End, 
                            overLapLine.Position, 
                            overLapLine.IsRow)));
                        //line.ChildLines = null;
                    }
                    else if (overLapLine.ChildLines == null)
                    {
                        overLapLine.ChildLines = new SDLinkedList<PartitionLine>();
                        overLapLine.ChildLines.AddFirst(
                            new LinkedListNode<PartitionLine>(
                            new PartitionLine(
                            overLapLine.Start,
                            overLapLine.End,
                            overLapLine.Position,
                            overLapLine.IsRow)));
                    }
                }
            }
            else///删除纵向切割线
            {
                resultLines = HPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        if (line.FatherLine == null
                            || (line.FatherLine != null &&
                            (line.FatherLine.Start == resultLines[i].Position || line.FatherLine.End == resultLines[i].Position)
                            ))
                        {
                            ///合并孩子线段
                            resultLines[i].MergeByPos(line.Position);
                        }
                    }
                }
                FindLineByLine findByLine = new FindLineByLine(line);
                PartitionLine overLapLine = VPartionLines.Find(findByLine.PredicateIncludeLine);
                if (overLapLine !=null )
                {

                
                if ( overLapLine.Start == line.Start && overLapLine.End == line.End)///如果是同一条线
                {
                    VPartionLines.Remove(overLapLine);
                }
                else
                {
                    PartitionLine newLine = overLapLine.RemoveChildLine(line);
                    if (newLine != null)///如果产生了新的线段
                    {
                        this.VPartionLines.Add(newLine);
                    }
                    if (overLapLine.ChildLines != null && overLapLine.ChildLines.Count == 0)
                    {
                        overLapLine.ChildLines.AddFirst(
                            new LinkedListNode<PartitionLine>(
                            new PartitionLine(
                            overLapLine.Start,
                            overLapLine.End,
                            overLapLine.Position,
                            overLapLine.IsRow)));
                        //line.ChildLines = null;
                    }
                    else if (overLapLine.ChildLines == null)
                    {
                        overLapLine.ChildLines = new SDLinkedList<PartitionLine>();
                        overLapLine.ChildLines.AddFirst(
                            new LinkedListNode<PartitionLine>(
                            new PartitionLine(
                            overLapLine.Start,
                            overLapLine.End,
                            overLapLine.Position,
                            overLapLine.IsRow)));
                    }
                }
                }
            }
        }

        /// <summary>
        /// 撤销删除直线
        /// </summary>
        /// <param name="line"></param>
        public void UnDeleteLine(int start, int end, int position, bool isRow)
        {
            AddLine(start, end, position, isRow);
        }

        /// <summary>
        /// 得到被选择之直线
        /// </summary>
        /// <returns></returns>
        public List<PartitionLine> GetSelectedLines()
        {
            List<PartitionLine> selectedLines = new List<PartitionLine>();

            List<PartitionLine> allLines = new List<PartitionLine>();
            allLines.AddRange(HPartionLines);
            allLines.AddRange(VPartionLines);

            foreach (PartitionLine line in allLines)
            {
                if (line.IsSelected)
                {
                    selectedLines.Add(line);
                }
                else
                {
                    if (line.ChildLines != null)
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            if (childLine.IsSelected)
                            {
                                selectedLines.Add(childLine);
                            }
                        }
                    }
                }
            }
            return selectedLines;
        }
        #endregion

        /// <summary>
        /// 是否存在被选择的直线
        /// </summary>
        public bool IsExistSelectedLine()
        {
            return GetSelectedLines().Count >= 1;
        }

        /// <summary>
        /// 返回不能被删除之直线
        /// </summary>
        /// <returns></returns>
        public List<PartitionLine> GetDisDeletableLine()
        {
            List<PartitionLine> resultList = new List<PartitionLine>();
            List<PartitionLine> selectedLines = GetSelectedLines();
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            FindLineByLine findByLine;
            foreach (PartitionLine line in selectedLines)
            {
                findByLine = new FindLineByLine(line);
                if (line.IsLocked || allLines.Find(findByLine.PredicateStartEndTo) != null)
                {
                    resultList.Add(line);
                }
                else if (line.ChildLines != null)
                {
                    foreach (PartitionLine childLine in line.ChildLines)
                    {
                        if (childLine.IsLocked)
                        {
                            resultList.Add(childLine);
                        }
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// 删除矩形内直线
        /// </summary>
        /// <param name="rectangle"></param>
        public void DeleteRectLine(MergeRectCommand command)
        {
            command.InRectLines = FindInRectLine(command.BoundaryRect);

            foreach (PartitionLine line in command.InRectLines)
            {
                DeleteLine(line);
            }

        }

        /// <summary>
        /// 撤销删除矩形内直线
        /// </summary>
        /// <param name="rectangle"></param>
        public void UnDeleteRectLine(MergeRectCommand command)
        {
            foreach (PartitionLine line in command.InRectLines)
            {
                AddLine(line.Start, line.End, line.Position, line.IsRow);
            }
        }

        /// <summary>
        /// 查找位于rect内部之线段,需进入直线的子线段查找
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public List<PartitionLine> FindInRectLine(Rectangle rect)
        {
            List<PartitionLine> inRectLines = new List<PartitionLine>();
            List<PartitionLine> CutedLines;
            List<PartitionLine> CutedChildLines;

            FindLineByRect findByRect = new FindLineByRect(rect);
            CutedLines = HPartionLines.FindAll(findByRect.PredicateCutedRect);
            CutedLines.AddRange(VPartionLines.FindAll(findByRect.PredicateCutedRect));

            foreach (PartitionLine line in CutedLines)
            {
                if (line.IsInRect(rect))///line全部包含于rect内部
                {
                    inRectLines.Add(line);
                }
                else
                {
                    if (line.ChildLines != null)
                    {
                        CutedChildLines = line.ChildLines.FindAll(findByRect.PredicateInRect);
                        inRectLines.AddRange(CutedChildLines);
                    }
                }
            }
            return inRectLines;
        }

        /// <summary>
        /// 选择直线
        /// </summary>
        /// <param name="line"></param>
        internal void SelectLine(PartitionLine line)
        {
            List<PartitionLine> selectedLines = this.GetLine(line);
            foreach (PartitionLine selectedLine in selectedLines)
            {
                selectedLine.SelectLine();
            }
        }

        /// <summary>
        /// 取消选择直线
        /// </summary>
        /// <param name="line"></param>
        internal void UnSelectLine(PartitionLine line)
        {
            List<PartitionLine> selectedLines = this.GetLine(line);
            foreach (PartitionLine selectedLine in selectedLines)
            {
                selectedLine.UnSelectLine();
            }
        }

        /// <summary>
        /// 返回和给定line相等的line的引用
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<PartitionLine> GetLine(PartitionLine line)
        {
            List<PartitionLine> resultLines = new List<PartitionLine>();

            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            FindLineByLine findByLine = new FindLineByLine(line);
            PartitionLine includeLine = allLines.Find(findByLine.PredicateIncludeLine);

            if (includeLine == null)
            {
                return resultLines;
            }
            if (line.Start == includeLine.Start && line.End == includeLine.End)///line即为父直线
            {
                resultLines.Add(includeLine);
                return resultLines;
            }
            if (includeLine.ChildLines != null)
            {
                List<PartitionLine> childLines =
                    includeLine.ChildLines.FindAll(findByLine.PredicateIncludedLine);
                resultLines.AddRange(childLines);
            }
            return resultLines;
        }

        internal List<PartitionLine> PartRect(Rect rect, bool isRow, int partNum)
        {
            List<PartitionLine> newLines = new List<PartitionLine>();
            if (isRow)
            {
                int step = rect.Height / partNum;
                for (int i = 1; i < partNum; i++)///生成partNum－1条直线
                {
                    newLines.Add(
                        new PartitionLine(rect.X, rect.X + rect.Width, rect.Y + i * step, true));
                }
            }
            else
            {
                int step = rect.Width / partNum;
                for (int i = 1; i < partNum; i++)///生成partNum－1条直线
                {
                    newLines.Add(
                        new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X + i * step, false));
                }
            }
            return newLines;
        }

        /// <summary>
        /// 得到将可被锁定的直线:选择且还未上锁
        /// </summary>
        /// <returns></returns>
        internal List<PartitionLine> GetToLockedLines()
        {
            List<PartitionLine> toLockedLines = new List<PartitionLine>();
            List<PartitionLine> selectedLines = GetSelectedLines();
            foreach (PartitionLine line in selectedLines)
            {
                if (!line.IsLocked)
                {
                    toLockedLines.Add(line);
                }
            }
            return toLockedLines;
        }

        /// <summary>
        /// 得到可被解锁的直线(锁定且已被选择)
        /// </summary>
        /// <returns></returns>
        internal List<PartitionLine> GetToUnLockedLines()
        {
            List<PartitionLine> toUnLockedLines = new List<PartitionLine>();
            List<PartitionLine> selectedLines = GetSelectedLines();
            foreach (PartitionLine line in selectedLines)
            {
                if (line.IsLocked)
                {
                    toUnLockedLines.Add(line);
                }
            }
            return toUnLockedLines;
        }

        /// <summary>
        /// 是否存在可被锁定之直线
        /// </summary>
        /// <returns></returns>
        internal bool IsExistLockableLine()
        {
            List<PartitionLine> selectedLines = GetSelectedLines();
            foreach (PartitionLine line in selectedLines)
            {
                if (!line.IsLocked)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否存在可被解锁之直线
        /// </summary>
        /// <returns></returns>
        internal bool IsExistUnLockableLine()
        {
            List<PartitionLine> selectedLines = GetSelectedLines();
            foreach (PartitionLine line in selectedLines)
            {
                if (line.IsLocked)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 添加一个结点到直线中,可能会导致直线分割
        /// </summary>
        /// <param name="point"></param>
        /// <param name="isRow">当isRow为true时,此点需要落在横向线上</param>
        internal void AddPoint(Point point, bool isRow)
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            FindLineByPoint findByPoint = new FindLineByPoint(point);
            PartitionLine resultLine = allLines.Find(findByPoint.PredicatePointIn);

            if (resultLine != null)
            {
                resultLine.PartitionByPoint(point);
            }
        }

        /// <summary>
        /// 删除直线中的一个结点,可能会导致直线合并
        /// </summary>
        /// <param name="point"></param>
        /// <param name="p"></param>
        internal void DeletePoint(Point point, bool p)
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            FindLineByPoint findByPoint = new FindLineByPoint(point);
            PartitionLine resultLine = allLines.Find(findByPoint.PredicatePointIn);

            if (resultLine!=null)
            {
                resultLine.MergeByPoint(point);
            }           
        }

        /// <summary>
        /// 得到被选择之直线,并将其拼接成(如果能)一条直线返回,否则返回空
        /// </summary>
        /// <returns>如果能拼接成(如果能)一条直线返回,否则返回空</returns>
        internal PartitionLine GetSelectedLine()
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            List<PartitionLine> selectedLines = GetSelectedLines();
            if (selectedLines.Count<1)
            {
                return null;
            }
            selectedLines.Sort(new CompareLineStart());
            PartitionLine selectedLine = new PartitionLine(
                selectedLines[0].Start,
                selectedLines[0].End,
                selectedLines[0].Position,
                selectedLines[0].IsRow);
            selectedLine.IsLocked = selectedLines[0].IsLocked;
            for (int i = 1; i < selectedLines.Count; i++)
            {
                if (selectedLines[i].IsRow!=selectedLine.IsRow || selectedLines[i].Position!=selectedLine.Position || selectedLine.End!=selectedLines[i].Start)
                {
                    return null;
                }
                if(!selectedLines[i].CanMove)
                    selectedLine.IsLocked = selectedLines[i].IsLocked;
                selectedLine.End = selectedLines[i].End;
            }
            FindLineByLine findByLine = new FindLineByLine(selectedLine);
            PartitionLine  joinedLine= allLines.Find(findByLine.PredicateJoined) ;
            if (joinedLine != null)
	        {
                return null;
	        }  
            
            return selectedLine;
        }

        /// <summary>
        /// 添加直线,不做其他动作,只是添加数据到数组
        /// </summary>
        /// <param name="line"></param>
        public void AddLine(PartitionLine line)
        {
            if (line.IsRow)
            {
                HPartionLines.Add(line);
            }
            else
            {
                VPartionLines.Add(line);
            }
        }

        /// <summary>
        /// 删除直线,不做其他动作,只是在数组中删除数据
        /// </summary>
        /// <param name="line"></param>
        internal void RemoveLine(PartitionLine line)
        {
            if (line.IsRow)
            {
                HPartionLines.Remove(line);
            }
            else
            {
                VPartionLines.Remove(line);
            }
        }

        /// <summary>
        /// 得到已知直线能移动的边界直线
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        internal PartitionLine[] GetLineBorderLine(PartitionLine line)
        {
            PartitionLine[] borderLines = new PartitionLine[2];
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);

            FindLineByLine findByLine = new FindLineByLine(line);
            List<PartitionLine> resultLines = allLines.FindAll(findByLine.PredicateParallelLine);
            resultLines.Sort(new CompareLLDistance(line));
            int i = 1;
            for (; i < resultLines.Count; i++)
            {
                if ((resultLines[0].Position < line.Position && resultLines[i].Position > line.Position)
                    ||(resultLines[0].Position > line.Position && resultLines[i].Position < line.Position))
                {
                    break;
                }
            }
            ///左或上的直线放在前面返回
            if (resultLines[0].Position<resultLines[i].Position)
            {
                borderLines[0] = resultLines[0];
                borderLines[1] = resultLines[i];
            }
            else
            {
                borderLines[0] = resultLines[i];
                borderLines[1] = resultLines[0];
            }
            return borderLines;
        }

        /// <summary>
        /// 获取给定点可以选择的直线
        /// </summary>
        /// <param name="pt">给定点</param>
        /// <param name="precision">选择精度</param>
        /// <param name="isSelectedChild">是否是选择孩子直线</param>
        /// <returns>如果不存在,则返回空,否则返回被选择之直线</returns>
        internal PartitionLine GetSelectedLine(Point pt, int precision, bool isSelectedChild)
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);
            //foreach (PartitionLine borderLine in BorderLines)//删除边界直线
            //{
            //    allLines.Remove(borderLine);
            //}

            foreach (PartitionLine line in allLines)
            {
                if (line.IsSelectable(pt, precision))
                {
                    if (!isSelectedChild || line.ChildLines == null || line.ChildLines.Count == 0)
                        //经测试,C#中的或运算是从左到右,故这样写不会引起null异常
                    {
                        return line;
                    }
                    else//有孩子线段
                    {
                        foreach (PartitionLine childLine in line.ChildLines)
                        {
                            if (childLine.IsSelectable(pt,precision))
                            {
                                return childLine;
                            }       
                        }
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
