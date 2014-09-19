using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ҳ����δ洢��list��
    /// </summary>
    public class RectList
    {
        #region ���Զ���
        /// <summary>
        /// �޲�ξ�������
        /// </summary>
        public SDList<Rect> SnipRectList { get; set; }

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        public DesignPanel TDPanel { get; private set; }

        #endregion

        #region ���캯��
        public RectList(DesignPanel tD)
        {
            SnipRectList = new SDList<Rect>();
            TDPanel = tD;
        }
        #endregion

        #region ����������Ա�ӿ�


        /// <summary>
        /// ���ֱ��
        /// </summary>
        /// <param name="command"></param>
        public void AddLine(AddLineCommand command)
        {
            if (!command.IsRedo)///�ǵ�һ�β���ֱ��,�������������Ĳ���ֱ������
            {
                ///�ҵ����б��ָ�֮����  
                PartitionLine line = new PartitionLine(command.Start, command.End, command.Position, command.IsRow);
                FindRectByLine findRect = new FindRectByLine(line);
                List<Rect> resultRects = SnipRectList.FindAll(findRect.PartitionLinePredicate);

                for (int i = 0; i < resultRects.Count; i++)
                {
                    command.RemovedRects.Add(resultRects[i]);
                    if (line.IsRow)
                    {

                        Rect firstRect = TDPanel.CreateRect(
                                                resultRects[i].X,
                                                resultRects[i].Y,
                                                resultRects[i].Width,
                                                line.Position - resultRects[i].Y
                                                );
                        Rect secondRect = TDPanel.CreateRect(
                                                resultRects[i].X,
                                                line.Position,
                                                resultRects[i].Width,
                                                resultRects[i].Y + resultRects[i].Height - line.Position
                                                );

                        // SnipRectList.Remove(resultRects[i]);
                        resultRects[i].IsDeleted = true;

                        SnipRectList.Add(firstRect);
                        SnipRectList.Add(secondRect);
                        command.AddedRects.Add(firstRect);
                        command.AddedRects.Add(secondRect);
                    }
                    else
                    {
                        Rect firstRect = TDPanel.CreateRect(
                                                resultRects[i].X,
                                                resultRects[i].Y,
                                                line.Position - resultRects[i].X,
                                                resultRects[i].Height
                                                );
                        Rect secondRect = TDPanel.CreateRect(
                                                line.Position,
                                                resultRects[i].Y,
                                                resultRects[i].Width + resultRects[i].X - line.Position,
                                                resultRects[i].Height
                                                );
                        //SnipRectList.Remove(resultRects[i]);
                        resultRects[i].IsDeleted = true;
                        SnipRectList.Add(firstRect);
                        SnipRectList.Add(secondRect);
                        command.AddedRects.Add(firstRect);
                        command.AddedRects.Add(secondRect);
                    }
                }
                command.IsRedo = true;
            }
            else///���������Ĳ���ֱ������
            {
                foreach (Rect rect in command.RemovedRects)
                {
                    rect.IsDeleted = true;
                }

                foreach (Rect rect in command.AddedRects)
                {
                    rect.IsDeleted = false;
                }
            }
        }


        /// <summary>
        /// �������ֱ��
        /// </summary>
        /// <param name="command"></param>
        public void UnAddLine(AddLineCommand command)
        {
            foreach (Rect rect in command.RemovedRects)
            {
                rect.IsDeleted = false;
            }

            foreach (Rect rect in command.AddedRects)
            {
                rect.IsDeleted = true;
            }
        }

        /// <summary>
        /// ɾ��ֱ��
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLine(DeleteLineCommand command)
        {
            if (!command.IsRedo)///�ǵ�һ�β���ֱ��,�������������Ĳ���ֱ������
            {
                command.IsRedo = true;
            }

            for (int i = 0; i < command.RemovedRects.Count; i++)
            {
                command.AddedRects[i].MergeTwoRect(command.RemovedRects[i]);
            }
        }

        /// <summary>
        /// ����ɾ��ֱ��
        /// </summary>
        /// <param name="line"></param>
        public void UnDeleteLine(DeleteLineCommand command)
        {
            for (int i = 0; i < command.RemovedRects.Count; i++)
            {
                command.AddedRects[i].PartTwoRect(command.RemovedRects[i]);
            }
        }

        /// <summary>
        /// ����������תΪ���ڵĳɶԴ洢
        /// </summary>
        /// <param name="rectList"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<NeighbourRect> RectListToNeighbour(List<Rect> rectList, PartitionLine line)
        {
            ///����������rectΪδ����
            List<NeighbourRect> neighbourRectList = new List<NeighbourRect>();
            if (line.IsRow)
            {
                rectList.Sort(new CompareRectX());
                for (int i = 0; i < rectList.Count; )
                {
                    neighbourRectList.Add(new NeighbourRect(rectList[i], rectList[i + 1]));
                    i += 2;
                }
            }
            else
            {
                rectList.Sort(new CompareRectY());
                for (int i = 0; i < rectList.Count; )
                {
                    neighbourRectList.Add(new NeighbourRect(rectList[i], rectList[i + 1]));
                    i += 2;
                }
            }
            return neighbourRectList;

        }

        /// <summary>
        /// ���ر�ѡ��֮����
        /// </summary>
        /// <returns></returns>
        public List<Rect> GetSelectedRects()
        {
            List<Rect> selectedRect = new List<Rect>();

            foreach (Rect rect in SnipRectList)
            {
                if (!rect.IsDeleted && rect.IsSelected)
                {
                    selectedRect.Add(rect);
                }
            }
            return selectedRect;
        }

        /// <summary>
        /// ����û�б�ѡ��֮���ε����߽���δ��ѡ��֮���κ�ѡ��ȴ������֮����
        /// </summary>
        /// <returns></returns>
        public List<Rect> GetDisMergableRects()
        {
            List<Rect> selectedRects = GetSelectedRects();
            Rectangle rectangle = CommonFuns.FindRectsBorder(selectedRects);
            FindRectByRect findRect = new FindRectByRect(rectangle);
            List<Rect> totalRects = this.SnipRectList.FindAll(findRect.FindInRectPredicate);

            List<Rect> dismergableRects = CommonFuns.RemainRectList(totalRects, selectedRects); ;
            foreach (Rect rect in selectedRects)
            {
                if (rect.IsLocked)
                {
                    dismergableRects.Add(rect);
                }
            }
            return dismergableRects;
        }
        
        /// <summary>
        /// �Ƿ���Ժϲ���ѡ��ľ���
        /// </summary>
        /// <returns></returns>
        public bool IsRectMergable()
        {
            List<Rect> selectedRects = GetSelectedRects();
            Rectangle rectangle = CommonFuns.FindRectsBorder(selectedRects);
            int rectSquare = 0;
            ///�����֮��
            foreach (Rect rect in selectedRects)
            {
                rectSquare += rect.Width * rect.Height;
            }

            return GetSelectedRects().Count >= 2 &&
                (GetDisMergableRects().Count < 1) &&
                rectSquare == rectangle.Width * rectangle.Height;
        }

        /// <summary>
        /// �Ƿ��ǵ�����ѡ����δ������:���Էָ�,ճ���Ȳ���
        /// </summary>
        /// <returns></returns>
        public bool IsSingleUnLocked()
        {
            List<Rect> selectedRects = GetSelectedRects();
            if (selectedRects.Count!=1)///����ѡ��ľ���Ϊ1ʱ���ܱ��ָ�
            {
                return false;
            }
            else if (selectedRects[0].IsLocked)
            {
                return false;
            } 
            return true;
        }

        /// <summary>
        /// �Ƿ�ֻ��һ��ҳ��Ƭ��ѡ��
        /// </summary>
        /// <returns></returns>
        public bool IsSingleSelected()
        {
            return GetSelectedRects().Count==1;
        }

        public bool IsHasSnipData()
        {
            List<Rect> selectedRects = GetSelectedRects();
            foreach (Rect rect in selectedRects)
            {
                if (rect.HasSnip)
                {
                    return true;
                }
            }
            return false;

        }
        #endregion

        /// <summary>
        /// ��һ�����ηָ�ɶ������
        /// </summary>
        /// <param name="isRow">����ָ�</param>
        /// <param name="partNum">�ָ������</param>
        /// <returns></returns>
        internal List<Rect> PartRect(Rect rect, bool isRow, int partNum)
        {
            List<Rect> newRects = new List<Rect>();
            if (isRow)///���зָ�
            {
                int step=rect.Height / partNum;
                int i = 0;
                for (; i < partNum-1; i++)
                {
                    newRects.Add(
                        TDPanel.CreateRect(
                            rect.X, 
                            rect.Y + i * step, 
                            rect.Width, 
                            step,
                            rect));
                }
                ///���һ�����⴦���Խ�����ܲ�������������
                newRects.Add(
                    TDPanel.CreateRect(
                        rect.X,
                        rect.Y + i * step,
                        rect.Width,
                        rect.Height - i * step,
                        rect));
            }
            else
            {
                int i = 0;
                int step=rect.Width/partNum;
                for (; i < partNum-1; i++)
                {
                    newRects.Add(
                        TDPanel.CreateRect(
                            rect.X+i*step,
                            rect.Y ,
                            step,
                            rect.Height,
                            rect));
                }
                ///���һ�����⴦���Խ�����ܲ�������������
                newRects.Add(
                        TDPanel.CreateRect(
                            rect.X + i * step,
                            rect.Y,
                            rect.Width-i*step,
                            rect.Height, 
                            rect));
            }
            SnipRectList.AddRange(newRects);
            return newRects;
        }

        /// <summary>
        /// �õ����ɱ������ľ���:ѡ���һ�δ����
        /// </summary>
        /// <returns></returns>
        internal List<Rect> GetToLockedRects()
        {
            List<Rect> toLockedRects = new List<Rect>();
            List<Rect> selectedRects = GetSelectedRects();
            foreach (Rect rect in selectedRects)
            {
                if (!rect.IsLocked)
                {
                    toLockedRects.Add(rect);
                }
            }
            return toLockedRects;
        }

        /// <summary>
        /// ���ؿɱ�����֮����:��ѡ�����ѱ�����
        /// </summary>
        /// <returns></returns>
        internal List<Rect> GetToUnLockedRects()
        {
            List<Rect> toUnLockedRects = new List<Rect>();
            List<Rect> selectedRects = GetSelectedRects();
            foreach (Rect rect in selectedRects)
            {
                if (rect.IsLocked)
                {
                    toUnLockedRects.Add(rect);
                }
            }
            return toUnLockedRects;        
        }

        /// <summary>
        /// �Ƿ���ڿɱ�����֮����
        /// </summary>
        /// <returns></returns>
        internal bool IsExistLockableRect()
        {
            List<Rect> selectedRects = GetSelectedRects();
            foreach (Rect rect in selectedRects)
            {
                if (!rect.IsLocked)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �Ƿ���ڿɱ�����֮����
        /// </summary>
        /// <returns></returns>
        internal bool IsExistUnLockableRect()
        {
            List<Rect> selectedRects = GetSelectedRects();
            foreach (Rect rect in selectedRects)
            {
                if (rect.IsLocked)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ����ָ����;��ȷ�Χ�ڵõ��ɱ�ѡ��ľ���,���û���򷵻ؿ�
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public Rect GetSelectedRect(Point pt, int precision)
        {
            foreach (Rect rect in SnipRectList)
            {
                if ((!rect.IsDeleted) && rect.IsSelectable(pt,precision))
                {
                    return rect;
                }
            }
            return null;
        }
    }
}
