﻿using System.Collections.Generic;
using System.Drawing;

namespace NKnife.Draws.Designs.Base
{
    /// <summary>
    /// 图片设计图板,当设计时,绘制的矩形的集合
    /// </summary>
    public class RectangleList : List<RectangleF>
    {
        /// <summary>
        /// 选中的矩形
        /// </summary>
        public List<RectangleF> Selected { get; set; }

        /// <summary>
        /// 鼠标正悬停在这个矩形的上方
        /// </summary>
        public RectangleF Hover { get; set; }
    }
}
