using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 绘图上下文接口
    /// </summary>
    public interface IDrawingContext
    {
        /// <summary>
        /// 获取画板
        /// </summary>
        Graphics Graphics { get;}
        /// <summary>
        /// 获取画笔
        /// </summary>
        Pen Pen { get;}
        /// <summary>
        /// 获取画刷
        /// </summary>
        Brush Brush { get;}
    }
}
