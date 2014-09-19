
using System.Drawing;
namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ���ݸ��������ֱ�� 
    /// </summary>
    public class FindLineByPoint
    {
        #region ��������

        /// <summary>
        /// ��ȡ�����ø�����
        /// </summary>
        public Point Point { get; set; }

        #endregion

        #region ���캯��

        public FindLineByPoint(Point point)
        {
            Point = point;
        }

        #endregion

        #region ��������

        /// <summary>
        /// Ѱ��ʹ�ø���������start��end֮��֮�ָ���
        /// Ѱ���յ�λ�ڸ���ֱ��֮�ָ���
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
        /// ������������ֱ֮��
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
    /// ���ݸ���ֱ����Ѱ��ֱ��
    /// </summary>
    public class FindLineByLine
    {
        #region ��������

        /// <summary>
        /// �����������ָ���
        /// </summary>
        public PartitionLine Line { get; set; }

        #endregion

        #region ���캯��

        public FindLineByLine(PartitionLine line)
        {
            this.Line = line;
        }

        #endregion

        #region ��������

        /// <summary>
        /// �����������ֱ������֮�ָ���:lineToRect�ã�line��Line��"�ұ�"(�������г�ͷ�����ұ��������ͷ��)
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
        /// �����������ֱ������֮�ָ���:line��Line�ָ�
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
        /// �����ֱ�ߵ�position��ͬ,�����ཻ֮��,�򷵻���
        /// ��,�Ƿ����ص�
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
        /// �������ڻ����ڸ���ֱ��ֱ֮��
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
        /// ����Position���ڵ��ڸ���ֱ��ֱ֮��
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePosGt(PartitionLine line)
        {
            return line.IsRow == Line.IsRow &&
                line.Position > Line.Position;
        }

        /// <summary>
        /// ����Position С�ڵ��ڸ���ֱ��ֱ֮��
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicatePosLt(PartitionLine line)
        {
            return line.IsRow == Line.IsRow &&
                line.Position < Line.Position;
        }

        /// <summary>
        /// ���������յ������ֱ�����ֱ֮��
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
        /// �������ڸ���ֱ��ֱ֮��
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
        /// ������ʼ�ڸ���ֱ��ֱ֮��
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
        /// �����ཻ����β�ཻ����ֱ֮��
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
        /// �����ཻ����β�ཻ����ֱ֮��.
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
        /// ���Ұ�������ֱ��ֱ֮��
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
        /// ���ұ�����ֱ�߰���ֱ֮��
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
        /// Ѱ���յ�λ�ڸ���ֱ��֮�ָ���
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
        /// ���Һ���ֱ֪��ƽ������"�غ�"ֱ֮��
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
    /// ���ݸ���������Ѱ��ֱ��
    /// </summary>
    public class FindLineByRect
    {
        #region ��������

        /// <summary>
        /// ��ȡ�����ø�������
        /// </summary>
        public Rectangle Rect { get; set; }

        #endregion

        #region ���캯��

        public FindLineByRect(Rectangle rect)
        {
            this.Rect = rect;
        }

        #endregion

        #region ��������

        /// <summary>
        /// ������������ཻ�򱻰���֮�и���
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateCutedRect(PartitionLine line)
        {
            if (line.IsRow )//�Ǻ����и�
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
        /// �Ƿ���rect�ڲ�
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool PredicateInRect(PartitionLine line)
        {
            if (line.IsRow)//�Ǻ����и�
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
    /// Ѱ�����Ը�������(isRow)�и��������֮�ָ���
    /// </summary>
    public class FindLindByRectAndRow
    {
        #region ��������

        /// <summary>
        /// ��ȡ�����ø����ľ���
        /// </summary>
        public RectLayer Rect { get; set; }

        /// <summary>
        /// ��ȡ�����ø����ķ���
        /// </summary>
        public bool IsRow { get; set; }

        #endregion

        #region ���캯��

        public FindLindByRectAndRow(RectLayer rect, bool isRow)
        {
            this.Rect = rect;
            this.IsRow = isRow;
        }

        #endregion

        #region ��������

        //���Ұ���line���и���
        public bool Predicate(PartitionLine line)
        {
            if (IsRow == true)//�Ǻ����и�
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