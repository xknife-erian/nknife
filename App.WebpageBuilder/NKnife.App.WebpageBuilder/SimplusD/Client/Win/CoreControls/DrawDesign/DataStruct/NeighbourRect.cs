using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 相邻的矩形对
    /// </summary>
    public class NeighbourRect
    {
        #region 属性定义

        /// <summary>
        /// 获取或设置第一个矩形
        /// </summary>
        public Rect FirstRect { get; set; }

        /// <summary>
        /// 获取或设置第二个矩形
        /// </summary>
        public Rect SecondRect { get; set; }

        #endregion

        #region 构造函数

        public NeighbourRect(Rect first, Rect second)
        {
            FirstRect = first;
            SecondRect = second;
        }

        #endregion
    }
}
