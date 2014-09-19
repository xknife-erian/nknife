using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 按层级存储矩形
    /// </summary>
    public class RectLayer 
    {
        #region 公共属性

        /// <summary>
        /// edit by zhenghao at 2008-06-24 10:00
        /// 获取或设置Css
        /// </summary>
        public string Css { get; set; }

        /// <summary>
        /// 获取或设置左上角横坐标
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// 获取或设置左上角纵坐标
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 获取或设置高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 获取或设置是否是按行切割
        /// </summary>
        public bool IsRow { get; set; }

        /// <summary>
        /// 获取或设置子矩形页面片
        /// </summary>
        public List<RectLayer> ChildRects { get; set; }

        #endregion

        #region 构造函数

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
