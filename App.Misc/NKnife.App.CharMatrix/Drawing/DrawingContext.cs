using System.Drawing;

namespace NKnife.App.CharMatrix.Drawing
{
    /// <summary>
    /// ��ͼ������
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
        /// ��ȡ�����û�ˢ
        /// </summary>
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        /// <summary>
        /// ��ȡ�����û���
        /// </summary>
        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        /// <summary>
        /// ��ȡ�����û���
        /// </summary>
        public Graphics Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }
    }
}
