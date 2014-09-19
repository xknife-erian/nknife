using System.Collections.Generic;
using System;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ���ݷָ���֮positionֵ������
    /// </summary>
    public class CompareLinePosition : IComparer<PartitionLine>
    {
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.Position - y.Position;
        }
    }

    /// <summary>
    /// ���ݷָ���֮positionֵ������
    /// </summary>
    public class CompareLineLen : IComparer<PartitionLine>
    { 
        /// <summary>
        /// ��startLine��otherPos��ʼ���㳤��
        /// </summary>  
        public PartitionLine StartLine { get; set; }

        #region ���캯��

        public CompareLineLen(PartitionLine line)
        {
            StartLine = line;
        }

        #endregion

        //��������,��������ǰ
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return (y.End - StartLine.Position) - (x.End - StartLine.Position);
        }
    }

    /// <summary>
    /// ��������ֱ��֮start������
    /// </summary>
    public class CompareLineStart : IComparer<PartitionLine>
    {
        //��������,��������ǰ
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.Start - y.Start;
        }
    }

    /// <summary>
    /// ��������ֱ��֮end������
    /// </summary>
    public class CompareLineEnd : IComparer<PartitionLine>
    {
        //��������,��������ǰ
        public int Compare(PartitionLine x, PartitionLine y)
        {
            return x.End - y.End;
        }
    }

    /// <summary>
    /// ���������ֱ�ߵľ���������
    /// </summary>
    public class CompareLLDistance : IComparer<PartitionLine>
    {
        PartitionLine _startLine; /// ��startLine��otherPos��ʼ���㳤��
        /// 
        /// <summary>
        /// ��startLine��otherPos��ʼ���㳤��
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
        /// �Ƚ������ֱ�ߵľ���,��������,Խ�����ֱ�߽���Խ��ǰ
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
    /// �����������ľ���������,ֱ�����ľ���
    /// </summary>
    public class CompareLPDistance : IComparer<PartitionLine>
    {
        Point startPoint; /// ��startPoint��otherPos��ʼ�������

        /// <summary>
        /// ��startPoint��otherPos��ʼ�������
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
        /// �Ƚ������ֱ�ߵľ���,��������,Խ�����ֱ�߽���Խ��ǰ
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