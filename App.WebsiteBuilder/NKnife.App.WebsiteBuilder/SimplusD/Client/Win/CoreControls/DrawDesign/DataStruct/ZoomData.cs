using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 放大缩小数据类
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
        /// 得到最大的缩放值
        /// </summary>
        public static float MaxZoom
        {
            get { return 5F; }
        }

        /// <summary>
        /// 得到小的缩放值
        /// </summary>
        public static float MinZoom
        {
            get { return 0.5F; }
        }

        /// <summary>
        /// 放大当前尺寸
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
        /// 缩小当前尺寸
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
