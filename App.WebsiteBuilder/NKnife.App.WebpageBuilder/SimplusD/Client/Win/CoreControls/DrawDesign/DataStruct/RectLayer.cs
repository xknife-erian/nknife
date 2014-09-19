using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// ���㼶�洢����
    /// </summary>
    public class RectLayer 
    {
        #region ��������

        /// <summary>
        /// edit by zhenghao at 2008-06-24 10:00
        /// ��ȡ������Css
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        /// ��ȡ���������ϽǺ�����
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// ��ȡ���������Ͻ�������
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// ��ȡ�����ÿ��
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// ��ȡ�����ø߶�
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// ��ȡ�������Ƿ��ǰ����и�
        /// </summary>
        public bool IsRow { get; set; }

        /// <summary>
        /// ��ȡ�������Ӿ���ҳ��Ƭ
        /// </summary>
        public List<RectLayer> ChildRects { get; set; }

        #endregion

        #region ���캯��

        public RectLayer(int x, int y, int width, int height,string css)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Css = css;
        }

        #endregion
    }
}
