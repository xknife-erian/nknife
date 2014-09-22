using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// �Ŵ���С������
    /// </summary>
    public class ZoomData
    {
        private static float[] _zoomDatas = new float[] { 0.5F, 0.6667F, 1F, 2F, 3F, 4F, 5F};//, 6F, 7F, 8F, 12F, 16F };

        protected static float[] ZoomDatas
        {
            get { return _zoomDatas; }
            set { _zoomDatas = value; }
        }

        /// <summary>
        /// �õ���������ֵ
        /// </summary>
        public static float MaxZoom
        {
            get { return 5F; }
        }

        /// <summary>
        /// �õ�С������ֵ
        /// </summary>
        public static float MinZoom
        {
            get { return 0.5F; }
        }

        /// <summary>
        /// �Ŵ�ǰ�ߴ�
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static float ZoomIn(float cur)
        {
            int i = 0;
            for (; i < ZoomDatas.Length - 1; i++)
            {
                if (cur >= ZoomDatas[i] && cur < ZoomDatas[i + 1])
                {
                    return ZoomDatas[i + 1];
                }
            }
            return ZoomDatas[i];
        }

        /// <summary>
        /// ��С��ǰ�ߴ�
        /// </summary>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static float ZoomOut(float cur)
        {
            for (int i = ZoomDatas.Length - 1; i >= 1; i--)
            {
                if (cur <= ZoomDatas[i] && cur > ZoomDatas[i - 1])
                {
                    return ZoomDatas[i - 1];
                }
            }
            return ZoomDatas[0];
        }
    }
}
