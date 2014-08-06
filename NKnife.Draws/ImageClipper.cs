using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NKnife.Draws
{
    public class ImageClipper
    {
        private readonly int _X;
        private readonly int _Y;
        private int _Height;
        private int _Width;

        /// <summary>
        ///     ImageClipper类的构造函数
        /// </summary>
        /// <param name="x">表示打算修剪图片的横坐标</param>
        /// <param name="y">表示打算修剪图片的纵坐标</param>
        /// <param name="width">表示打算修剪图片的宽度</param>
        /// <param name="heigth">表示打算修剪图片的高度</param>
        public ImageClipper(int x, int y, int width, int heigth)
        {
            _X = x;
            _Y = y;
            _Width = width;
            _Height = heigth;
        }

        /// <summary>
        ///     剪裁,GDI+
        /// </summary>
        /// <param name="srcBitmap">原始Bitmap,即需要裁剪的图片</param>
        /// <returns>剪裁后的Bitmap</returns>
        public Bitmap Clip(Bitmap srcBitmap)
        {
            if (srcBitmap == null)
            {
                return null;
            }

            int w = srcBitmap.Width;
            int h = srcBitmap.Height;

            if (_X >= w || _Y >= h)
            {
                return null;
            }

            if (_X + _Width > w)
            {
                _Width = w - _X;
            }

            if (_Y + _Height > h)
            {
                _Height = h - _Y;
            }

            try
            {
                var bmpOut = new Bitmap(_Width, _Height, PixelFormat.Max);
                using (Graphics g = Graphics.FromImage(bmpOut))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    //指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
                    var destRect = new Rectangle(0, 0, _Width, _Height);

                    //指定要绘制的 image 对象的矩形部分。将此部分进行缩放以适合 destRect 参数所指定的矩形。
                    var srcRect = new Rectangle(_X, _Y, _Width, _Height);
                    g.DrawImage(srcBitmap, destRect, srcRect, GraphicsUnit.Pixel);
                    g.Dispose();
                }
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }
    }
}