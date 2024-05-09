using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Win.Forms
{
    /// <summary>һ�����Կ���ͨ��Graphics����ָ��������Button�ϵĿؼ�
    /// </summary>
    public sealed class ImageButton : Button
    {
        #region ImageButtonStyle enum

        public enum ImageButtonStyle
        {
            /// <summary>
            /// �Ӻ�
            /// </summary>
            Add,
            /// <summary>
            /// ����
            /// </summary>
            Subtract,
            /// <summary>
            /// ���ϼ�ͷ
            /// </summary>
            ArrowUp,
            /// <summary>
            /// ���¼�ͷ
            /// </summary>
            ArrowDown,
            /// <summary>
            /// �����ͷ
            /// </summary>
            ArrowLeft,
            /// <summary>
            /// ���Ҽ�ͷ
            /// </summary>
            ArrowRight,
        }

        #endregion

        private Color _ButtonColor = Color.FromArgb(0, 120, 0);

        public ImageButton()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }

        /// <summary>���ŵ���ʽ
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        [Category("ImageButton")]
        public ImageButtonStyle Style { get; set; }

        /// <summary>���ŵ���ɫ
        /// </summary>
        /// <value>
        /// The color of the button.
        /// </value>
        [Category("ImageButton")]
        public Color ButtonColor
        {
            get { return _ButtonColor; }
            set { _ButtonColor = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            float offsetX = (float) Width/10 + (float) Width/10;
            float offsetY = (float) Height/10 + (float) Height/10;
            Graphics g = pevent.Graphics;
            switch (Style)
            {
                case ImageButtonStyle.Add:
                    {
                        // ���Ӻŵĺ���
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_ButtonColor);
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        // ���Ӻŵ�����
                        var locationV = new PointF((float) Width/2 - offsetX/2, offsetY);
                        float widthV = offsetX;
                        float heightV = Height - offsetY - offsetY;
                        var rV = new RectangleF(locationV, new SizeF(widthV, heightV));
                        g.FillRectangle(brush, rV);
                        break;
                    }
                case ImageButtonStyle.Subtract:
                    {
                        // �����ŵĺ���
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = Width - offsetX - offsetX;
                        float heightH = offsetY;
                        Brush brush = new SolidBrush(_ButtonColor);
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowUp:
                    {
                        // ����ͷ
                        var pointF1 = new PointF((float) Width/2, offsetY);
                        var pointF2 = new PointF(offsetX, (float) Height/2);
                        var pointF3 = new PointF(Width - offsetX, (float) Height/2);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // ����ͷ������
                        var locationH = new PointF((float) Width/2 - offsetX/2, (float) Height/2);
                        float widthH = offsetX;
                        float heightH = (float) Height/2 - offsetY;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowDown:
                    {
                        // ����ͷ
                        var pointF1 = new PointF((float) Width/2, Height - offsetY);
                        var pointF2 = new PointF(offsetX, (float) Height/2);
                        var pointF3 = new PointF(Width - offsetX, (float) Height/2);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // ����ͷ������
                        var locationH = new PointF((float) Width/2 - offsetX/2, offsetY);
                        float widthH = offsetX;
                        float heightH = (float) Height/2 - offsetY;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
                case ImageButtonStyle.ArrowLeft:
                    {
                        // ����ͷ
                        var pointF1 = new PointF(offsetX, (float) Height/2);
                        var pointF2 = new PointF((float) Width/2, offsetY);
                        var pointF3 = new PointF((float) Width/2, Height - offsetY);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // ����ͷ�ĺ���
                        var locationH = new PointF((float)Width / 2, (float)Height / 2 - offsetY / 2);
                        float widthH = (float)Width / 2 - offsetX;
                        float heightH = offsetY + 1;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }

                case ImageButtonStyle.ArrowRight:
                    {
                        // ����ͷ
                        var pointF1 = new PointF(Width - offsetX, (float) Height/2);
                        var pointF2 = new PointF((float) Width/2, offsetY);
                        var pointF3 = new PointF((float) Width/2, Height - offsetY);
                        var pointFArr = new[] {pointF1, pointF2, pointF3};
                        Brush brush = new SolidBrush(_ButtonColor);
                        g.FillPolygon(brush, pointFArr);

                        // ����ͷ�ĺ���
                        var locationH = new PointF(offsetX, (float)Height / 2 - offsetY / 2);
                        float widthH = (float)Width / 2 - offsetX + 2;
                        float heightH = offsetY + 1;
                        var rH = new RectangleF(locationH, new SizeF(widthH, heightH));
                        g.FillRectangle(brush, rH);
                        break;
                    }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Text = string.Empty;
        }
    }
}