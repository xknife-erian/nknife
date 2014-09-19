using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// �������зָ������߶�
    /// </summary>
    public class LineList
    {
        #region ���Զ���
        /// <summary>
        /// ����ֱ��
        /// </summary>
        public SDList<PartitionLine> HPartionLines { get; set; }

        /// <summary>
        /// ����ֱ��
        /// </summary>
        public SDList<PartitionLine> VPartionLines { get; set; }

        /// <summary>
        /// �����߽�ֱ��
        /// </summary>
        public SDList<PartitionLine> BorderLines { get; set; }

        #endregion

        #region ���캯��

        public LineList()
        {
            HPartionLines = new SDList<PartitionLine>();
            VPartionLines = new SDList<PartitionLine>();
            BorderLines = new SDList<PartitionLine>();
        }

        #endregion

        #region ������Ա�ӿ�

        public void AddLine(int start, int end, int position, bool isRow)
        {
            PartitionLine line = new PartitionLine(start, end, position, isRow);
            List<PartitionLine> resultLines;  ///�ҵ������ֱ������֮�ָ��ߵĽ����
            List<PartitionLine> overLapLine; ///���ص���ֱ��
            ///�ҵ����б�line�и�ķָ���
            FindLineByLine findLinePartion = new FindLineByLine(line);
            if (line.IsRow)
            {
                resultLines = VPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    ///�������и���������ֱ��,ͬʱ�����Լ�Ҳ���и��˵�����
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        resultLines[i].PartitionByLine(line);
                        line.PartitionByLine(resultLines[i]);
                    }
                }
                ///����»�ֱ֮��,���ж��Ƿ����ص�ֱ��
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
                    ///�������и���������ֱ��,ͬʱ�����Լ�Ҳ���и��˵�����
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        resultLines[i].PartitionByLine(line);
                        line.PartitionByLine(resultLines[i]);
                    }
                }
                ///����»�ֱ֮��,���ж��Ƿ����ص�ֱ��
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
        /// ������ӷָ���
        /// </summary>
        /// <param name="line"></param>
        public void UnAddLine(int start, int end, int position, bool isRow)
        {
            DeleteLine(start, end, position, isRow);
        }

        /// <summary>
        /// ɾ��ֱ��
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
        /// ɾ��ֱ��
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLine(PartitionLine line)
        {
            List<PartitionLine> resultLines;  ///�ҵ������ֱ������֮�ָ��ߵĽ����
            ///�ҵ����б�line�и�ķָ���
            FindLineByLine findLinePartion = new FindLineByLine(line);
            if (line.IsRow)///����ֱ��
            {
                resultLines = VPartionLines.FindAll(findLinePartion.PredicatePartedLine);
                if (resultLines.Count >= 1)
                {
                    for (int i = 0; i < resultLines.Count; i++)
                    {
                        ///�ϲ������߶�
                        if (line.FatherLine == null
                            || (line.FatherLine != null &&
                            (line.FatherLine.Start == resultLines[i].Position || line.FatherLine.End == resultLines[i].Position)
                            ))
                        {
                            resultLines[i].MergeByPos(line.Position);
                        }

                    }
                }
                ///���ܼ򵥵�HPartionLines.Remove(line),�˴�ֻ�õ�Ϊline�е�����,��������.��Ϊ�п���
                ///HPartionLines.Remove(line);

                FindLineByLine findByLine = new FindLineByLine(line);
                PartitionLine overLapLine = HPartionLines.Find(findByLine.PredicateIncludeLine);
                if (overLapLine.Start == line.Start && overLapLine.End == line.End)///�����ͬһ����
                {
                    HPartionLines.Remove(overLapLine);
                }
                else
                {
                    PartitionLine newLine = overLapLine.RemoveChildLine(line);
                    if (newLine != null)///����������µ��߶�
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
            else///ɾ�������и���
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
                            ///�ϲ������߶�
                            resultLines[i].MergeByPos(line.Position);
                        }
                    }
                }
                FindLineByLine findByLine = new FindLineByLine(line);
                PartitionLine overLapLine = VPartionLines.Find(findByLine.PredicateIncludeLine);
                if (overLapLine !=null )
                {

                
                if ( overLapLine.Start == line.Start && overLapLine.End == line.End)///�����ͬһ����
                {
                    VPartionLines.Remove(overLapLine);
                }
                else
                {
                    PartitionLine newLine = overLapLine.RemoveChildLine(line);
                    if (newLine != null)///����������µ��߶�
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
        /// ����ɾ��ֱ��
        /// </summary>
        /// <param name="line"></param>
        public void UnDeleteLine(int start, int end, int position, bool isRow)
        {
            AddLine(start, end, position, isRow);
        }

        /// <summary>
        /// �õ���ѡ��ֱ֮��
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
        /// �Ƿ���ڱ�ѡ���ֱ��
        /// </summary>
        public bool IsExistSelectedLine()
        {
            return GetSelectedLines().Count >= 1;
        }

        /// <summary>
        /// ���ز��ܱ�ɾ��ֱ֮��
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
        /// ɾ��������ֱ��
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
        /// ����ɾ��������ֱ��
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
        /// ����λ��rect�ڲ�֮�߶�,�����ֱ�ߵ����߶β���
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
                if (line.IsInRect(rect))///lineȫ��������rect�ڲ�
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
        /// ѡ��ֱ��
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
        /// ȡ��ѡ��ֱ��
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
        /// ���غ͸���line��ȵ�line������
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
            if (line.Start == includeLine.Start && line.End == includeLine.End)///line��Ϊ��ֱ��
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
                for (int i = 1; i < partNum; i++)///����partNum��1��ֱ��
                {
                    newLines.Add(
                        new PartitionLine(rect.X, rect.X + rect.Width, rect.Y + i * step, true));
                }
            }
            else
            {
                int step = rect.Width / partNum;
                for (int i = 1; i < partNum; i++)///����partNum��1��ֱ��
                {
                    newLines.Add(
                        new PartitionLine(rect.Y, rect.Y + rect.Height, rect.X + i * step, false));
                }
            }
            return newLines;
        }

        /// <summary>
        /// �õ����ɱ�������ֱ��:ѡ���һ�δ����
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
        /// �õ��ɱ�������ֱ��(�������ѱ�ѡ��)
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
        /// �Ƿ���ڿɱ�����ֱ֮��
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
        /// �Ƿ���ڿɱ�����ֱ֮��
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
        /// ���һ����㵽ֱ����,���ܻᵼ��ֱ�߷ָ�
        /// </summary>
        /// <param name="point"></param>
        /// <param name="isRow">��isRowΪtrueʱ,�˵���Ҫ���ں�������</param>
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
        /// ɾ��ֱ���е�һ�����,���ܻᵼ��ֱ�ߺϲ�
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
        /// �õ���ѡ��ֱ֮��,������ƴ�ӳ�(�����)һ��ֱ�߷���,���򷵻ؿ�
        /// </summary>
        /// <returns>�����ƴ�ӳ�(�����)һ��ֱ�߷���,���򷵻ؿ�</returns>
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
        /// ���ֱ��,������������,ֻ��������ݵ�����
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
        /// ɾ��ֱ��,������������,ֻ����������ɾ������
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
        /// �õ���ֱ֪�����ƶ��ı߽�ֱ��
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
            ///����ϵ�ֱ�߷���ǰ�淵��
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
        /// ��ȡ���������ѡ���ֱ��
        /// </summary>
        /// <param name="pt">������</param>
        /// <param name="precision">ѡ�񾫶�</param>
        /// <param name="isSelectedChild">�Ƿ���ѡ����ֱ��</param>
        /// <returns>���������,�򷵻ؿ�,���򷵻ر�ѡ��ֱ֮��</returns>
        internal PartitionLine GetSelectedLine(Point pt, int precision, bool isSelectedChild)
        {
            List<PartitionLine> allLines = new List<PartitionLine>(HPartionLines);
            allLines.AddRange(VPartionLines);
            //foreach (PartitionLine borderLine in BorderLines)//ɾ���߽�ֱ��
            //{
            //    allLines.Remove(borderLine);
            //}

            foreach (PartitionLine line in allLines)
            {
                if (line.IsSelectable(pt, precision))
                {
                    if (!isSelectedChild || line.ChildLines == null || line.ChildLines.Count == 0)
                        //������,C#�еĻ������Ǵ�����,������д��������null�쳣
                    {
                        return line;
                    }
                    else//�к����߶�
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
