using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// 绘图上下文
    /// </summary>
    public class DrawingContext : IDrawingContext
    {
        private Graphics graphics;
        private Pen pen;
        private Brush brush;
        
        public DrawingContext()
        {
        }

        public DrawingContext(Graphics graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// 获取或设置画刷
        /// </summary>
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        /// <summary>
        /// 获取或设置画笔
        /// </summary>
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        /// <summary>
        /// 获取或设置画板
        /// </summary>
        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
    }
}
