using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using NKnife.Draws.Controls.Frames.Base;

namespace NKnife.App.PictureTextPicker.Common.Entities
{
    /// <summary>
    /// 与一个PictureFrame对应的文档对象，可用于显示属性grid
    /// </summary>
    public class PictureFrameDocument
    {
        public PictureFrameDocument()
        {
            RectangleList = new RectangleList();
        }

        [Category("基本")]
        [Description("单据宽度。实际宽度，单位mm。请用尺子精确测量。")]
        public int ImageWidth { get; set; }

        [Category("基本")]
        [Description("单据高度。实际高度，单位mm。请用尺子精确测量。")]
        public int ImageHeight { get; set; }

        [Category("基本")]
        [Description("单据扫描图")]
        public string ImageFileName { get; set; }

        [Browsable(false)]
        public RectangleList RectangleList { get; private set; }
    }
}
