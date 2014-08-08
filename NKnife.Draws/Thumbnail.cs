using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace NKnife.Draws
{
    /// <summary>
    ///     缩略图相关算法
    /// </summary>
    public class Thumbnail
    {
        /// <SUMMARY>
        ///     图片缩放
        /// </SUMMARY>
        /// <PARAM name="sourceFile">图片源路径</PARAM>
        /// <PARAM name="destFile">缩放后图片输出路径</PARAM>
        /// <PARAM name="destHeight">缩放后图片高度</PARAM>
        /// <PARAM name="destWidth">缩放后图片宽度</PARAM>
        /// <RETURNS></RETURNS>
        public static bool GetThumbnail(string sourceFile, string destFile, int destHeight, int destWidth)
        {
            Image imgSource = Image.FromFile(sourceFile);
            ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            // 按比例缩放
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;

            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth*destHeight) > (sHeight*destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth*sHeight)/sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth*destHeight)/sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            var outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.WhiteSmoke);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //可以在这里设置填充背景颜色
            //g.Clear(System.Drawing.Color.White);

            g.DrawImage(imgSource, new Rectangle((destWidth - sW)/2, (destHeight - sH)/2, sW, sH), 0, 0, imgSource.Width, imgSource.Height,
                GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            try
            {
                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                var arrayIci = ImageCodecInfo.GetImageEncoders();
                var jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));

                if (jpegIci != null)
                {
                    outBmp.Save(destFile, jpegIci, encoderParams);
                }
                else
                {
                    outBmp.Save(destFile, thisFormat);
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                imgSource.Dispose();
                outBmp.Dispose();
            }
        }
    }
}