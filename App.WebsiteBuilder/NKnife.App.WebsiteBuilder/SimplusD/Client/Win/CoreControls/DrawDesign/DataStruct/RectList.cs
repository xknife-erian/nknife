using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 页面矩形存储在list中
    /// </summary>
    public class RectList
    {
        #region 属性定义
        /// <summary>
        /// 无层次矩形数组
        /// </summary>
        public SDList<Rect> SnipRectList { get; set; }

        /// <summary>
        /// 获取或设置设计面板
        /// </summary>
        public DesignPanel TDPanel { get; private set; }

        #endregion

        #region 构造函数
        public RectList(DesignPanel tD)
        {
            SnipRectList = new SDList<Rect>();
            TDPanel = tD;
        }
        #endregion

        #region 公共函数成员接口


        /// <summary>
        /// 添加直线
        /// </summary>
        /// <param name="command"></param>
        public void AddLine(AddLineCommand command)
        {
            if (!command.IsRedo)///是第一次插入直线,而非重做撤销的插入直线命令
            {
                ///找到所有被分割之矩形  
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
            else///重做撤销的插入直线命令
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
        /// 撤销添加直线
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
        /// 删除直线
        /// </summary>
        /// <param name="line"></param>
        public void DeleteLine(DeleteLineCommand command)
        {
            if (!command.IsRedo)///是第一次插入直线,而非重做撤销的插入直线命令
            {
                command.IsRedo = true;
            }

            for (int i = 0; i < command.RemovedRects.Count; i++)
            {
                command.AddedRects[i].MergeTwoRect(command.RemovedRects[i]);
            }
        }

        /// <summary>
        /// 撤销删除直线
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
        /// 将矩形数组转为相邻的成对存储
        /// </summary>
        /// <param name="rectList"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public List<NeighbourRect> RectListToNeighbour(List<Rect> rectList, PartitionLine line)
        {
            ///先设置所有rect为未访问
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
        /// 返回被选择之矩形
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
        /// 返回没有被选择之矩形的最大边界中未被选择之矩形和选定却被锁定之矩形
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
        /// 是否可以合并被选择的矩形
        /// </summary>
        /// <returns></returns>
        public bool IsRectMergable()
        {
            List<Rect> selectedRects = GetSelectedRects();
            Rectangle rectangle = CommonFuns.FindRectsBorder(selectedRects);
            int rectSquare = 0;
            ///求面积之和
            foreach (Rect rect in selectedRects)
            {
                rectSquare += rect.Width * rect.Height;
            }

            return GetSelectedRects().Count >= 2 &&
                (GetDisMergableRects().Count < 1) &&
                rectSquare == rectangle.Width * rectangle.Height;
        }

        /// <summary>
        /// 是否是单个被选择且未被锁定:可以分割,粘贴等操作
        /// </summary>
        /// <returns></returns>
        public bool IsSingleUnLocked()
        {
            List<Rect> selectedRects = GetSelectedRects();
            if (selectedRects.Count!=1)///当被选择的矩形为1时才能被分割
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
        /// 是否只有一个页面片被选择
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
        /// 将一个矩形分割成多个矩形
        /// </summary>
        /// <param name="isRow">按横分割</param>
        /// <param name="partNum">分割矩形数</param>
        /// <returns></returns>
        internal List<Rect> PartRect(Rect rect, bool isRow, int partNum)
        {
            List<Rect> newRects = new List<Rect>();
            if (isRow)///按行分割
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
                ///最后一个特殊处理，以解决可能不能整除的问题
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
                ///最后一个特殊处理，以解决可能不能整除的问题
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
        /// 得到将可被锁定的矩形:选择且还未上锁
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
        /// 返回可被解锁之矩形:被选择且已被上锁
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
        /// 是否存在可被锁定之矩形
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
        /// 是否存在可被解锁之矩形
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
        /// 给定指定点和精度范围内得到可被选择的矩形,如果没有则返回空
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
