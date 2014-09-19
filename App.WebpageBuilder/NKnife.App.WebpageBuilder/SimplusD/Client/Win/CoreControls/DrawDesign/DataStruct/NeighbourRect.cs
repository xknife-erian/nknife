using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ���ڵľ��ζ�
    /// </summary>
    public class NeighbourRect
    {
        #region ���Զ���

        /// <summary>
        /// ��ȡ�����õ�һ������
        /// </summary>
        public Rect FirstRect { get; set; }

        /// <summary>
        /// ��ȡ�����õڶ�������
        /// </summary>
        public Rect SecondRect { get; set; }

        #endregion

        #region ���캯��

        public NeighbourRect(Rect first, Rect second)
        {
            FirstRect = first;
            SecondRect = second;
        }

        #endregion
    }
}
