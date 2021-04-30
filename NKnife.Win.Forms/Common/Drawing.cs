using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace NKnife.Win.Forms.Common
{
    /// <summary>
    ///     一些关于Image操作的帮助方法
    /// </summary>
    public static class Drawing
    {
        /// <summary>
        ///     将指定的图片去除指定的透明色调，返回一个图片中的图形形状
        /// </summary>
        /// <param name="bitmap">指定的图片</param>
        /// <param name="transparencyColor">指定的透明色调</param>
        /// <returns></returns>
        public static Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap", "Bitmap cannot be null!");

            int height = bitmap.Height;
            int width = bitmap.Width;
            Region region = null;
            using (var path = new GraphicsPath())
            {
                for (int j = 0; j < height; j++)
                    for (int i = 0; i < width; i++)
                    {
                        if (bitmap.GetPixel(i, j) == transparencyColor)
                            continue;
                        int x0 = i;
                        while ((i < width) && (bitmap.GetPixel(i, j) != transparencyColor))
                            i++;
                        path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
                    }
                region = new Region(path);
            }
            return region;
        }

        /// <summary>
        ///     对指定的图片进行色彩反转
        /// </summary>
        /// <param name="bitmap">指定的图片</param>
        /// <returns></returns>
        public static bool Invert(Bitmap bitmap)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bmpData.Stride;
            IntPtr scan = bmpData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) scan;

                int nOffset = stride - bitmap.Width*3;
                int nWidth = bitmap.Width*3;

                for (int y = 0; y < bitmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte) (255 - p[0]);
                        ++p;
                    }
                    p += nOffset;
                }
            }
            bitmap.UnlockBits(bmpData);
            return true;
        }

        /// <summary>
        ///     变成黑白图
        /// </summary>
        /// <param name="pimage">原始图</param>
        /// <returns></returns>
        public static Bitmap ToGray(Bitmap pimage)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (pimage.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(pimage.Width, pimage.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(pimage.HorizontalResolution, pimage.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(pimage, 0, 0);
                }
            }
            else
            {
                source = pimage;
            }

            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int imageSize = sourceData.Stride*sourceData.Height;
            var sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            source.UnlockBits(sourceData);

            // Create destination bitmap
            var destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(
                new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly,
                PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride*destinationData.Height;
            var destinationBuffer = new byte[imageSize];

            int height = source.Height;
            int width = source.Width;
            const int threshold = 500;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                int sourceIndex = y*sourceData.Stride;
                int destinationIndex = y*destinationData.Stride;
                byte destinationValue = 0;
                int pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    int pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] +
                                     sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte) pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Dispose of source if not originally supplied bitmap
            if (source != pimage)
                source.Dispose();
            return destination;
        }

        /// <summary>
        ///     对指定的图片进行置为灰度模式
        /// </summary>
        /// <param name="bmp">指定的图片</param>
        public static bool GrayScale(Bitmap bmp)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr ptr = bmData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) ptr;
                int nOffset = stride - bmp.Width*3;
                for (int y = 0; y < bmp.Height; ++y)
                {
                    for (int x = 0; x < bmp.Width; ++x)
                    {
                        byte blue = p[0];
                        byte green = p[1];
                        byte red = p[2];

                        p[0] = p[1] = p[2] = (byte) (.299*red + .587*green + .114*blue);
                        p += 3;
                    }
                    p += nOffset;
                }
            }
            bmp.UnlockBits(bmData);
            return true;
        }

        /// <summary>
        ///     对指定的图片进行指定数量的亮度调节
        /// </summary>
        /// <param name="b">指定的图片</param>
        /// <param name="nBrightness">指定数量</param>
        /// <returns></returns>
        public static bool Brightness(Bitmap b, int nBrightness)
        {
            if (nBrightness < -255 || nBrightness > 255)
                return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr scan = bmData.Scan0;

            int nVal = 0;

            unsafe
            {
                var p = (byte*) (void*) scan;

                int nOffset = stride - b.Width*3;
                int nWidth = b.Width*3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = p[0] + nBrightness;

                        if (nVal < 0) nVal = 0;
                        if (nVal > 255) nVal = 255;

                        p[0] = (byte) nVal;

                        ++p;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        /// <summary>
        ///     对指定的图片进行指定数量的对比度调节
        /// </summary>
        /// <param name="b">指定的图片</param>
        /// <param name="nContrast">指定数量</param>
        /// <returns></returns>
        public static bool Contrast(Bitmap b, sbyte nContrast)
        {
            if (nContrast < -100) return false;
            if (nContrast > 100) return false;

            double pixel = 0, contrast = (100.0 + nContrast)/100.0;

            contrast *= contrast;

            int red, green, blue;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) Scan0;

                int nOffset = stride - b.Width*3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        blue = p[0];
                        green = p[1];
                        red = p[2];

                        pixel = red/255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[2] = (byte) pixel;

                        pixel = green/255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[1] = (byte) pixel;

                        pixel = blue/255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;
                        if (pixel < 0) pixel = 0;
                        if (pixel > 255) pixel = 255;
                        p[0] = (byte) pixel;

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Gamma(Bitmap b, double red, double green, double blue)
        {
            if (red < .2 || red > 5) return false;
            if (green < .2 || green > 5) return false;
            if (blue < .2 || blue > 5) return false;

            var redGamma = new byte[256];
            var greenGamma = new byte[256];
            var blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte) Math.Min(255, (int) ((255.0*Math.Pow(i/255.0, 1.0/red)) + 0.5));
                greenGamma[i] = (byte) Math.Min(255, (int) ((255.0*Math.Pow(i/255.0, 1.0/green)) + 0.5));
                blueGamma[i] = (byte) Math.Min(255, (int) ((255.0*Math.Pow(i/255.0, 1.0/blue)) + 0.5));
            }

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) Scan0;

                int nOffset = stride - b.Width*3;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        p[2] = redGamma[p[2]];
                        p[1] = greenGamma[p[1]];
                        p[0] = blueGamma[p[0]];

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        public static bool Color(Bitmap b, int red, int green, int blue)
        {
            if (red < -255 || red > 255) return false;
            if (green < -255 || green > 255) return false;
            if (blue < -255 || blue > 255) return false;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                var p = (byte*) (void*) Scan0;

                int nOffset = stride - b.Width*3;
                int nPixel;

                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        nPixel = p[2] + red;
                        nPixel = Math.Max(nPixel, 0);
                        p[2] = (byte) Math.Min(255, nPixel);

                        nPixel = p[1] + green;
                        nPixel = Math.Max(nPixel, 0);
                        p[1] = (byte) Math.Min(255, nPixel);

                        nPixel = p[0] + blue;
                        nPixel = Math.Max(nPixel, 0);
                        p[0] = (byte) Math.Min(255, nPixel);

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        /// <summary>
        ///     获取指定mimeType的ImageCodecInfo
        /// </summary>
        private static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            ImageCodecInfo[] codecInfo = ImageCodecInfo.GetImageEncoders();
            return codecInfo.FirstOrDefault(ici => ici.MimeType == mimeType);
        }

        /// <summary>
        ///     获取inputStream中的Bitmap对象
        /// </summary>
        public static Bitmap GetBitmapFromStream(Stream inputStream)
        {
            var bitmap = new Bitmap(inputStream);
            return bitmap;
        }

        /// <summary>
        ///     将Bitmap对象压缩为JPG图片类型
        /// </summary>
        /// <param name="bmp">源bitmap对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJpg(Bitmap bmp, string saveFilePath, int quality)
        {
            var p = new EncoderParameter(Encoder.Quality, quality);
            ;
            var ps = new EncoderParameters(1);
            ps.Param[0] = p;
            bmp.Save(saveFilePath, GetImageCodecInfo("image/jpeg"), ps);
            bmp.Dispose();
        }

        /// <summary>
        ///     将inputStream中的对象压缩为JPG图片类型
        /// </summary>
        /// <param name="inputStream">源Stream对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public static void CompressAsJpg(Stream inputStream, string saveFilePath, int quality)
        {
            Bitmap bmp = GetBitmapFromStream(inputStream);
            CompressAsJpg(bmp, saveFilePath, quality);
        }

        /// <summary>
        ///     将Bitmap对象裁剪为指定JPG文件
        /// </summary>
        /// <param name="bmp">源bmp对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="x">开始坐标x，单位：像素</param>
        /// <param name="y">开始坐标y，单位：像素</param>
        /// <param name="width">宽度：像素</param>
        /// <param name="height">高度：像素</param>
        public static void CutAsJpg(Bitmap bmp, string saveFilePath, int x, int y, int width, int height)
        {
            int bmpW = bmp.Width;
            int bmpH = bmp.Height;

            if (x >= bmpW || y >= bmpH)
            {
                CompressAsJpg(bmp, saveFilePath, 80);
                return;
            }

            if (x + width > bmpW)
            {
                width = bmpW - x;
            }

            if (y + height > bmpH)
            {
                height = bmpH - y;
            }

            var bmpOut = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpOut);
            g.DrawImage(bmp, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
            g.Dispose();
            bmp.Dispose();
            CompressAsJpg(bmpOut, saveFilePath, 80);
        }

        /// <summary>
        ///     将Stream中的对象裁剪为指定JPG文件
        /// </summary>
        /// <param name="inputStream">源bmp对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="x">开始坐标x，单位：像素</param>
        /// <param name="y">开始坐标y，单位：像素</param>
        /// <param name="width">宽度：像素</param>
        /// <param name="height">高度：像素</param>
        public static void CutAsJpg(Stream inputStream, string saveFilePath, int x, int y, int width, int height)
        {
            Bitmap bmp = GetBitmapFromStream(inputStream);
            CutAsJpg(bmp, saveFilePath, x, y, width, height);
        }

        /// <summary>
        ///     获取图片中的各帧
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavedPath">保存路径</param>
        public static void GetGifFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            var fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)  
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧  
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /// <summary>
        ///     获取图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavedPath"></param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        public static void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY,
            int pPartWidth, int pPartHeight,
            int pOrigStartPointX, int pOrigStartPointY)
        {
            Image originalImg = Image.FromFile(pPath);

            var partImg = new Bitmap(pPartWidth, pPartHeight);
            Graphics graphics = Graphics.FromImage(partImg);
            var destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY),
                new Size(pPartWidth, pPartHeight)); //目标位置  
            var origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY),
                new Size(pPartWidth, pPartHeight)); //原图位置（默认从原图中截取的图片大小等于目标图片的大小）  

            graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
            partImg.Save(pSavedPath + "\\part.jpg", ImageFormat.Jpeg);
        }

        /// <summary>
        ///     判断文件类型是否为WEB格式图片
        ///     (注：JPG,GIF,BMP,PNG)
        /// </summary>
        /// <param name="contentType">HttpPostedFile.ContentType</param>
        /// <returns></returns>
        public static bool IsWebImage(string contentType)
        {
            if (contentType == "image/pjpeg" || contentType == "image/jpeg" || contentType == "image/gif" ||
                contentType == "image/bmp" ||
                contentType == "image/png")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     图片描边
        /// </summary>
        public static void MiaoBian1(Image image)
        {
            var Bmp = new Bitmap(image);
            var GP = new GraphicsPath();
            Color C = System.Drawing.Color.FromArgb(0, 0, 0, 0);

            for (int i = 0; i < Bmp.Width; i++)
                for (int j = 0; j < Bmp.Height; j++)
                    // 这点不透明而且左右上下四点至少有一点是透明的，那这点就是边缘
                    if (Bmp.GetPixel(i, j) != C
                        && (i > 0 && Bmp.GetPixel(i - 1, j) == C
                            || i < Bmp.Width - 1 && Bmp.GetPixel(i + 1, j) == C
                            || j > 0 && Bmp.GetPixel(i, j - 1) == C
                            || j < Bmp.Height - 1 && Bmp.GetPixel(i, j + 1) == C))
                        GP.AddRectangle(new Rectangle(new Point(i, j), new Size(1, 1)));

            using (Graphics G = Graphics.FromImage(Bmp))
                G.DrawPath(Pens.Black, GP);

            image = Bmp;
        }

        /// <summary>
        ///     描边函数，效果自然
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <param name="font"></param>
        /// <param name="foreColor"></param>
        /// <param name="str"></param>
        public static void MiaoBian(Graphics g, Image image, Size size, Font font, Color foreColor, string str)
        {
            g.FillRectangle(Brushes.LimeGreen, 0, 0, image.Width, image.Height);
            try
            {
                int COUNT = 10; //每个字阴影绘制次数
                var path = new GraphicsPath();
                g.SmoothingMode = SmoothingMode.HighQuality; //高质量绘图
                // 各种参数
                var brush = new SolidBrush(foreColor);
                FontFamily family = font.FontFamily;
                var fontStyle = (int) font.Style;
                var emSize = (int) font.Size;
                var origin = new PointF(-emSize, 3); //(0, -emSize);//为了第一个字符在Y轴向上紧靠边上
                StringFormat format = StringFormat.GenericDefault; //默认字符串部分格式
                // 重点：初始化Pen组,描边渐变.
                var penArray = new Pen[COUNT];
                for (int i = 0; i < COUNT; i++)
                {
                    penArray[i] = new Pen(System.Drawing.Color.FromArgb(5*i, 5*i, 5*i), (float) (i*0.5));
                }
                // 分割字符串，每个绘制一次
                var singleText = new string[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    singleText[i] = str.Substring(i, 1);
                }

                // 绘制多个字符
                foreach (string single in singleText)
                {
                    //让origin在Y轴上每次偏移一个字体大小呵
                    origin.X += emSize;
                    //绘制多次边框
                    for (int i = 0; i < COUNT; i++)
                    {
                        path.AddString(single,
                            family,
                            fontStyle,
                            emSize,
                            origin,
                            format);
                        g.DrawPath(penArray[i], path);
                    }
                    //填充主色调,其在后是覆盖为了描边时描到的内侧区域
                    path.AddString(single,
                        family,
                        fontStyle,
                        emSize,
                        origin,
                        format
                        );
                    g.FillPath(brush, path);
                }
                //g.Save();
                g.DrawImage(image, new Point(0, 0));
                g.Flush();
                g.Dispose();
            }
            catch
            {
            }
        }

        #region 正方型裁剪并缩放

        /// <summary>
        ///     正方型裁剪
        ///     以图片中心为轴心，截取正方型，然后等比缩放
        ///     用于头像处理
        /// </summary>
        /// <remarks>吴剑 2010-11-23</remarks>
        /// <param name="postedFile">原图HttpPostedFile对象</param>
        /// <param name="fileSaveUrl">缩略图存放地址</param>
        /// <param name="side">指定的边长（正方型）</param>
        /// <param name="quality">质量（范围0-100）</param>
        public static void CutForSquare(HttpPostedFile postedFile, string fileSaveUrl, int side, int quality)
        {
            //创建目录
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initImage = Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //非正方型先裁剪为正方型
                if (initWidth != initHeight)
                {
                    // 截图对象
                    Image pickedImage = null;
                    Graphics pickedG = null;

                    // 宽大于高的横图
                    if (initWidth > initHeight)
                    {
                        //对象实例化
                        pickedImage = new Bitmap(initHeight, initHeight);
                        pickedG = Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = SmoothingMode.HighQuality;
                        //定位
                        var fromR = new Rectangle((initWidth - initHeight)/2, 0, initHeight, initHeight);
                        var toR = new Rectangle(0, 0, initHeight, initHeight);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                        //重置宽
                        initWidth = initHeight;
                    }
                        // 高大于宽的竖图
                    else
                    {
                        //对象实例化
                        pickedImage = new Bitmap(initWidth, initWidth);
                        pickedG = Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = SmoothingMode.HighQuality;
                        //定位
                        var fromR = new Rectangle(0, (initHeight - initWidth)/2, initWidth, initWidth);
                        var toR = new Rectangle(0, 0, initWidth, initWidth);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                        //重置高
                        initHeight = initWidth;
                    }

                    // 将截图对象赋给原图
                    initImage = (Image) pickedImage.Clone();
                    // 释放截图资源
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //缩略图对象
                Image resultImage = new Bitmap(side, side);
                Graphics resultG = Graphics.FromImage(resultImage);
                //设置质量
                resultG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = SmoothingMode.HighQuality;
                //用指定背景色清空画布
                resultG.Clear(System.Drawing.Color.White);
                //绘制缩略图
                resultG.DrawImage(initImage, new Rectangle(0, 0, side, side), new Rectangle(0, 0, initWidth, initHeight),
                    GraphicsUnit.Pixel);

                //关键质量控制
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" ||
                        i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                var ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                //保存缩略图
                resultImage.Save(fileSaveUrl, ici, ep);

                //释放关键质量控制所用资源
                ep.Dispose();

                //释放缩略图资源
                resultG.Dispose();
                resultImage.Dispose();

                //释放原始图片资源
                initImage.Dispose();
            }
        }

        /// <summary>
        ///     正方型裁剪
        ///     以图片中心为轴心，截取正方型，然后等比缩放
        ///     用于头像处理
        /// </summary>
        /// <param name="fromFile">原图HttpPostedFile对象</param>
        /// <param name="fileSaveUrl">缩略图存放地址</param>
        /// <param name="side">指定的边长（正方型）</param>
        /// <param name="quality">质量（范围0-100）</param>
        public static void CutForSquare(Stream fromFile, string fileSaveUrl, int side, int quality)
        {
            //创建目录
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initImage = Image.FromStream(fromFile, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //非正方型先裁剪为正方型
                if (initWidth != initHeight)
                {
                    // 截图对象
                    Image pickedImage = null;
                    Graphics pickedG = null;

                    // 宽大于高的横图
                    if (initWidth > initHeight)
                    {
                        //对象实例化
                        pickedImage = new Bitmap(initHeight, initHeight);
                        pickedG = Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = SmoothingMode.HighQuality;
                        //定位
                        var fromR = new Rectangle((initWidth - initHeight)/2, 0, initHeight, initHeight);
                        var toR = new Rectangle(0, 0, initHeight, initHeight);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                        //重置宽
                        initWidth = initHeight;
                    }
                        // 高大于宽的竖图
                    else
                    {
                        //对象实例化
                        pickedImage = new Bitmap(initWidth, initWidth);
                        pickedG = Graphics.FromImage(pickedImage);
                        //设置质量
                        pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = SmoothingMode.HighQuality;
                        //定位
                        var fromR = new Rectangle(0, (initHeight - initWidth)/2, initWidth, initWidth);
                        var toR = new Rectangle(0, 0, initWidth, initWidth);
                        //画图
                        pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);
                        //重置高
                        initHeight = initWidth;
                    }

                    // 将截图对象赋给原图
                    initImage = (Image) pickedImage.Clone();
                    // 释放截图资源
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //缩略图对象
                Image resultImage = new Bitmap(side, side);
                Graphics resultG = Graphics.FromImage(resultImage);
                //设置质量
                resultG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = SmoothingMode.HighQuality;
                //用指定背景色清空画布
                resultG.Clear(System.Drawing.Color.White);
                //绘制缩略图
                resultG.DrawImage(initImage, new Rectangle(0, 0, side, side), new Rectangle(0, 0, initWidth, initHeight),
                    GraphicsUnit.Pixel);

                //关键质量控制
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" ||
                        i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                var ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                //保存缩略图
                resultImage.Save(fileSaveUrl, ici, ep);

                //释放关键质量控制所用资源
                ep.Dispose();

                //释放缩略图资源
                resultG.Dispose();
                resultImage.Dispose();

                //释放原始图片资源
                initImage.Dispose();
            }
        }

        #endregion

        #region 固定模版裁剪并缩放

        /// <summary>
        ///     指定长宽裁剪
        ///     按模版比例最大范围的裁剪图片并缩放至模版尺寸
        /// </summary>
        /// <remarks>吴剑 2010-11-15</remarks>
        /// <param name="postedFile">原图HttpPostedFile对象</param>
        /// <param name="fileSaveUrl">保存路径</param>
        /// <param name="maxWidth">最大宽(单位:px)</param>
        /// <param name="maxHeight">最大高(单位:px)</param>
        /// <param name="quality">质量（范围0-100）</param>
        public static void CutForCustom(HttpPostedFile postedFile, string fileSaveUrl, int maxWidth, int maxHeight,
            int quality)
        {
            //从文件获取原始图片，并使用流中嵌入的颜色管理信息
            Image initImage = Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                initImage.Save(fileSaveUrl, ImageFormat.Jpeg);
            }
            else
            {
                //模版的宽高比例
                double templateRate = (double) maxWidth/maxHeight;
                //原图片的宽高比例
                double initRate = (double) initImage.Width/initImage.Height;

                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {
                    // 按模版大小生成最终图片
                    Image templateImage = new Bitmap(maxWidth, maxHeight);
                    Graphics templateG = Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = InterpolationMode.High;
                    templateG.SmoothingMode = SmoothingMode.HighQuality;
                    templateG.Clear(System.Drawing.Color.White);
                    templateG.DrawImage(initImage, new Rectangle(0, 0, maxWidth, maxHeight),
                        new Rectangle(0, 0, initImage.Width, initImage.Height),
                        GraphicsUnit.Pixel);
                    templateImage.Save(fileSaveUrl, ImageFormat.Jpeg);
                }
                    //原图与模版比例不等，裁剪后缩放
                else
                {
                    // 裁剪对象
                    Image pickedImage = null;
                    Graphics pickedG = null;

                    // 定位
                    var fromR = new Rectangle(0, 0, 0, 0); //原图裁剪定位
                    var toR = new Rectangle(0, 0, 0, 0); //目标定位

                    // 宽为标准进行裁剪
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化
                        pickedImage = new Bitmap(initImage.Width, (int) Math.Floor(initImage.Width/templateRate));
                        pickedG = Graphics.FromImage(pickedImage);

                        //裁剪源定位
                        fromR.X = 0;
                        fromR.Y = (int) Math.Floor((initImage.Height - initImage.Width/templateRate)/2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int) Math.Floor(initImage.Width/templateRate);

                        //裁剪目标定位
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int) Math.Floor(initImage.Width/templateRate);
                    }
                        // 高为标准进行裁剪
                    else
                    {
                        pickedImage = new Bitmap((int) Math.Floor(initImage.Height*templateRate), initImage.Height);
                        pickedG = Graphics.FromImage(pickedImage);

                        fromR.X = (int) Math.Floor((initImage.Width - initImage.Height*templateRate)/2);
                        fromR.Y = 0;
                        fromR.Width = (int) Math.Floor(initImage.Height*templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int) Math.Floor(initImage.Height*templateRate);
                        toR.Height = initImage.Height;
                    }

                    // 设置质量
                    pickedG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = SmoothingMode.HighQuality;

                    // 裁剪
                    pickedG.DrawImage(initImage, toR, fromR, GraphicsUnit.Pixel);

                    // 按模版大小生成最终图片
                    Image templateImage = new Bitmap(maxWidth, maxHeight);
                    Graphics templateG = Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = InterpolationMode.High;
                    templateG.SmoothingMode = SmoothingMode.HighQuality;
                    templateG.Clear(System.Drawing.Color.White);
                    templateG.DrawImage(pickedImage, new Rectangle(0, 0, maxWidth, maxHeight),
                        new Rectangle(0, 0, pickedImage.Width, pickedImage.Height),
                        GraphicsUnit.Pixel);

                    // 关键质量控制
                    //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                    ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo i in icis)
                    {
                        if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" ||
                            i.MimeType == "image/gif")
                        {
                            ici = i;
                        }
                    }
                    var ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(Encoder.Quality, quality);

                    // 保存缩略图
                    templateImage.Save(fileSaveUrl, ici, ep);
                    //templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // 释放资源
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }

            //释放资源
            initImage.Dispose();
        }

        #endregion

        #region 等比缩放

        /// <summary>
        ///     图片等比缩放
        /// </summary>
        /// <remarks>吴剑 2011-01-21</remarks>
        /// <param name="postedFile">原图HttpPostedFile对象</param>
        /// <param name="savePath">缩略图存放地址</param>
        /// <param name="targetWidth">指定的最大宽度</param>
        /// <param name="targetHeight">指定的最大高度</param>
        /// <param name="watermarkText">水印文字(为""表示不使用水印)</param>
        /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param>
        public static void ZoomAuto(HttpPostedFile postedFile, string savePath, Double targetWidth, Double targetHeight,
            string watermarkText,
            string watermarkImage)
        {
            //创建目录
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            Image initImage = Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //文字水印
                if (watermarkText != "")
                {
                    using (Graphics gWater = Graphics.FromImage(initImage))
                    {
                        var fontWater = new Font("黑体", 10);
                        Brush brushWater = new SolidBrush(System.Drawing.Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (Image wrImage = Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(initImage);

                                //透明属性
                                var imgAttributes = new ImageAttributes();
                                var colorMap = new ColorMap();
                                colorMap.OldColor = System.Drawing.Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = {colorMap};
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements =
                                {
                                    new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f}, //透明度:0.5
                                    new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                };

                                var wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                                    ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage,
                                    new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height,
                                        wrImage.Width, wrImage.Height), 0, 0,
                                    wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存
                initImage.Save(savePath, ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    // 如果宽大于模版
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放
                        newWidth = targetWidth;
                        newHeight = initImage.Height*(targetWidth/initImage.Width);
                    }
                }
                    //高大于宽（竖图）
                else
                {
                    // 如果高大于模版
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放
                        newHeight = targetHeight;
                        newWidth = initImage.Width*(targetHeight/initImage.Height);
                    }
                }

                //生成新图
                //新建一个bmp图片
                Image newImage = new Bitmap((int) newWidth, (int) newHeight);
                //新建一个画板
                Graphics newG = Graphics.FromImage(newImage);

                //设置质量
                newG.InterpolationMode = InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = SmoothingMode.HighQuality;

                //置背景色
                newG.Clear(System.Drawing.Color.White);
                //画图
                newG.DrawImage(initImage, new Rectangle(0, 0, newImage.Width, newImage.Height),
                    new Rectangle(0, 0, initImage.Width, initImage.Height),
                    GraphicsUnit.Pixel);

                //文字水印
                if (watermarkText != "")
                {
                    using (Graphics gWater = Graphics.FromImage(newImage))
                    {
                        var fontWater = new Font("宋体", 10);
                        Brush brushWater = new SolidBrush(System.Drawing.Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (Image wrImage = Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

                                //透明属性
                                var imgAttributes = new ImageAttributes();
                                var colorMap = new ColorMap();
                                colorMap.OldColor = System.Drawing.Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = {colorMap};
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements =
                                {
                                    new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                    new[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f}, //透明度:0.5
                                    new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                };

                                var wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                                    ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage,
                                    new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height,
                                        wrImage.Width, wrImage.Height), 0, 0,
                                    wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存缩略图
                newImage.Save(savePath, ImageFormat.Jpeg);

                //释放资源
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion

        #region 图片水印操作

        /// <summary>
        ///     水印的位置
        /// </summary>
        public enum MarkPosition
        {
            /// <summary>
            ///     左上角
            /// </summary>
            MP_Left_Top,

            /// <summary>
            ///     左下角
            /// </summary>
            MP_Left_Bottom,

            /// <summary>
            ///     右上角
            /// </summary>
            MP_Right_Top,

            /// <summary>
            ///     右下角
            /// </summary>
            MP_Right_Bottom
        }

        /// <summary>
        ///     给图片添加图片水印
        /// </summary>
        /// <param name="inputStream">包含要源图片的流</param>
        /// <param name="watermarkPath">水印图片的物理地址</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddPicWatermarkAsJPG(Stream inputStream, string watermarkPath, string saveFilePath,
            MarkPosition mp)
        {
            Image image = Image.FromStream(inputStream);
            var b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(System.Drawing.Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            AddWatermarkImage(g, watermarkPath, mp, image.Width, image.Height);

            try
            {
                CompressAsJpg(b, saveFilePath, 80);
            }
            catch
            {
                ;
            }
            finally
            {
                b.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        ///     给图片添加图片水印
        /// </summary>
        /// <param name="sourcePath">源图片的存储地址</param>
        /// <param name="watermarkPath">水印图片的物理地址</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddPicWatermarkAsJPG(string sourcePath, string watermarkPath, string saveFilePath,
            MarkPosition mp)
        {
            if (File.Exists(sourcePath))
            {
                using (var sr = new StreamReader(sourcePath))
                {
                    AddPicWatermarkAsJPG(sr.BaseStream, watermarkPath, saveFilePath, mp);
                }
            }
        }

        /// <summary>
        ///     给图片添加文字水印
        /// </summary>
        /// <param name="inputStream">包含要源图片的流</param>
        /// <param name="text">水印文字</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddTextWatermarkAsJpg(Stream inputStream, string text, string saveFilePath, MarkPosition mp)
        {
            Image image = Image.FromStream(inputStream);
            var b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(System.Drawing.Color.White);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, image.Width, image.Height);

            AddWatermarkText(g, text, mp, image.Width, image.Height);

            try
            {
                CompressAsJpg(b, saveFilePath, 80);
            }
            catch
            {
                ;
            }
            finally
            {
                b.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        ///     给图片添加文字水印
        /// </summary>
        /// <param name="sourcePath">源图片的存储地址</param>
        /// <param name="text">水印文字</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="mp">水印位置</param>
        public static void AddTextWatermarkAsJpg(string sourcePath, string text, string saveFilePath, MarkPosition mp)
        {
            if (File.Exists(sourcePath))
            {
                using (var sr = new StreamReader(sourcePath))
                {
                    AddTextWatermarkAsJpg(sr.BaseStream, text, saveFilePath, mp);
                }
            }
        }

        /// <summary>
        ///     添加文字水印
        /// </summary>
        /// <param name="picture">要加水印的原图像</param>
        /// <param name="text">水印文字</param>
        /// <param name="mp">添加的位置</param>
        /// <param name="width">原图像的宽度</param>
        /// <param name="height">原图像的高度</param>
        private static void AddWatermarkText(Graphics picture, string text, MarkPosition mp, int width, int height)
        {
            int[] sizes = {16, 14, 12, 10, 8, 6, 4};
            Font crFont = null;
            var crSize = new SizeF();
            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("Arial", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(text, crFont);

                if ((ushort) crSize.Width < (ushort) width)
                    break;
            }

            float xpos = 0;
            float ypos = 0;

            switch (mp)
            {
                case MarkPosition.MP_Left_Top:
                    xpos = (width*(float) .01) + (crSize.Width/2);
                    ypos = height*(float) .01;
                    break;
                case MarkPosition.MP_Right_Top:
                    xpos = (width*(float) .99) - (crSize.Width/2);
                    ypos = height*(float) .01;
                    break;
                case MarkPosition.MP_Right_Bottom:
                    xpos = (width*(float) .99) - (crSize.Width/2);
                    ypos = (height*(float) .99) - crSize.Height;
                    break;
                case MarkPosition.MP_Left_Bottom:
                    xpos = (width*(float) .01) + (crSize.Width/2);
                    ypos = (height*(float) .99) - crSize.Height;
                    break;
            }

            var StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            var semiTransBrush2 = new SolidBrush(System.Drawing.Color.FromArgb(153, 0, 0, 0));
            picture.DrawString(text, crFont, semiTransBrush2, xpos + 1, ypos + 1, StrFormat);

            var semiTransBrush = new SolidBrush(System.Drawing.Color.FromArgb(153, 255, 255, 255));
            picture.DrawString(text, crFont, semiTransBrush, xpos, ypos, StrFormat);

            semiTransBrush2.Dispose();
            semiTransBrush.Dispose();
        }

        /// <summary>
        ///     添加图片水印
        /// </summary>
        /// <param name="picture">要加水印的原图像</param>
        /// <param name="waterMarkPath">水印文件的物理地址</param>
        /// <param name="mp">添加的位置</param>
        /// <param name="width">原图像的宽度</param>
        /// <param name="height">原图像的高度</param>
        private static void AddWatermarkImage(Graphics picture, string waterMarkPath, MarkPosition mp, int width,
            int height)
        {
            Image watermark = new Bitmap(waterMarkPath);

            var imageAttributes = new ImageAttributes();
            var colorMap = new ColorMap();

            colorMap.OldColor = System.Drawing.Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = {colorMap};

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements =
            {
                new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                new[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
                new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            var colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;
            if ((width > watermark.Width*4) && (height > watermark.Height*4))
            {
                bl = 1;
            }
            else if ((width > watermark.Width*4) && (height < watermark.Height*4))
            {
                bl = Convert.ToDouble(height/4)/Convert.ToDouble(watermark.Height);
            }
            else if ((width < watermark.Width*4) && (height > watermark.Height*4))
            {
                bl = Convert.ToDouble(width/4)/Convert.ToDouble(watermark.Width);
            }
            else
            {
                if ((width*watermark.Height) > (height*watermark.Width))
                {
                    bl = Convert.ToDouble(height/4)/Convert.ToDouble(watermark.Height);
                }
                else
                {
                    bl = Convert.ToDouble(width/4)/Convert.ToDouble(watermark.Width);
                }
            }

            WatermarkWidth = Convert.ToInt32(watermark.Width*bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height*bl);


            switch (mp)
            {
                case MarkPosition.MP_Left_Top:
                    xpos = 10;
                    ypos = 10;
                    break;
                case MarkPosition.MP_Right_Top:
                    xpos = width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case MarkPosition.MP_Right_Bottom:
                    xpos = width - WatermarkWidth - 10;
                    ypos = height - WatermarkHeight - 10;
                    break;
                case MarkPosition.MP_Left_Bottom:
                    xpos = 10;
                    ypos = height - WatermarkHeight - 10;
                    break;
            }

            picture.DrawImage(watermark, new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight), 0, 0,
                watermark.Width, watermark.Height, GraphicsUnit.Pixel,
                imageAttributes);


            watermark.Dispose();
            imageAttributes.Dispose();
        }

        #endregion

        #region 在图片上添加透明水印文字

        /// <summary>
        ///     ASP.NET图片加水印：在图片上添加透明水印文字
        /// </summary>
        /// <param name="imgPhoto">原来图片地址(路径+文件名)</param>
        /// <param name="waterWords">需要添加到图片上的文字</param>
        /// <param name="alpha">透明度(0.1~1.0之间)</param>
        /// <param name="position">文字显示的位置</param>
        public static bool DrawWords(ref Image imgPhoto, string waterWords, float alpha, float angle,
            ImagePosition position)
        {
            Graphics grPhoto = null;
            try
            {
                //获取图片的宽和高 
                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                //***********************************************************************************************************
                //创建添加水印图片复本
                ////建立一个bitmap，和我们需要加水印的图片一样大小 
                //bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

                ////SetResolution：设置此 Bitmap 的分辨率 
                ////这里直接将我们需要添加水印的图片的分辨率赋给了bitmap 
                //bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                ////Graphics：封装一个 GDI+ 绘图图面。 
                //grPhoto = Graphics.FromImage(bmPhoto);
                //***********************************************************************************************************

                //Graphics：封装一个 GDI+ 绘图图面。 
                grPhoto = Graphics.FromImage(imgPhoto);

                //设置图形的品质 
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                //将我们要添加水印的图片按照原始大小描绘（复制）到图形中 
                grPhoto.DrawImage(
                    imgPhoto, //   要添加水印的图片 
                    new Rectangle(0, 0, phWidth, phHeight), // 根据要添加的水印图片的宽和高 
                    0, // X方向从0点开始描绘 
                    0, // Y方向   
                    phWidth, // X方向描绘长度 
                    phHeight, // Y方向描绘长度 
                    GraphicsUnit.Pixel); // 描绘的单位，这里用的是像素

                //根据图片的大小我们来确定添加上去的文字的大小 
                //在这里我们定义一个数组来确定 
                int[] sizes = {48, 36, 28, 24, 16, 14, 12, 10};

                //字体 
                Font crFont = null;
                //矩形的宽度和高度，SizeF有三个属性，分别为Height高，width宽，IsEmpty是否为空 
                var crSize = new SizeF();

                //利用一个循环语句来选择我们要添加文字的型号
                //直到它的长度比图片的宽度小
                for (int i = 0; i < sizes.Length; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);

                    //测量用指定的 Font 对象绘制并用指定的 StringFormat 对象格式化的指定字符串。 
                    crSize = grPhoto.MeasureString(waterWords, crFont);

                    // ushort 关键字表示一种整数数据类型 
                    if ((ushort) crSize.Width < (ushort) phWidth)
                        break;
                }

                //截边5%的距离，定义文字显示(由于不同的图片显示的高和宽不同，所以按百分比截取) 
                var yPixlesFromBottom = (int) (phHeight*.05);

                //定义在图片上文字的位置 
                float wmHeight = crSize.Height;
                float wmWidth = crSize.Width;

                float xPosOfWm;
                float yPosOfWm;

                //设置水印的位置 
                switch (position)
                {
                    case ImagePosition.BottomMiddle:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.Center:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = phHeight/2;
                        break;
                    case ImagePosition.LeftMiddle:
                        xPosOfWm = 20;
                        yPosOfWm = phHeight/2;
                        break;
                    case ImagePosition.LeftBottom:
                        xPosOfWm = wmWidth/2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.LeftTop:
                        xPosOfWm = wmWidth/2;
                        yPosOfWm = wmHeight/2;
                        break;
                    case ImagePosition.RightTop:
                        xPosOfWm = phWidth - wmWidth - 10;
                        yPosOfWm = wmHeight;
                        break;
                    case ImagePosition.RigthBottom:
                        xPosOfWm = phWidth - wmWidth - 10;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.TopMiddle:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = wmWidth;
                        break;
                    default:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                }
                //封装文本布局信息（如对齐、文字方向和 Tab 停靠位），显示操作（如省略号插入和国家标准 (National) 数字替换）和 OpenType 功能。 
                var StrFormat = new StringFormat();

                //定义需要印的文字居中对齐 
                StrFormat.Alignment = StringAlignment.Center;

                //SolidBrush:定义单色画笔。画笔用于填充图形形状，如矩形、椭圆、扇形、多边形和封闭路径。 
                //这个画笔为描绘阴影的画笔，呈灰色 
                int m_alpha = Convert.ToInt32(256*alpha);
                var semiTransBrush2 = new SolidBrush(System.Drawing.Color.FromArgb(m_alpha, 200, 200, 200));

                //描绘文字信息，这个图层向右和向下偏移一个像素，表示阴影效果 
                //DrawString 在指定矩形并且用指定的 Brush 和 Font 对象绘制指定的文本字符串。 
                grPhoto.RotateTransform(angle);
                grPhoto.DrawString(waterWords, //string of text 
                    crFont, //font 
                    semiTransBrush2, //Brush 
                    new PointF(xPosOfWm + 1, yPosOfWm + 1), //Position 
                    StrFormat);

                //从四个 ARGB 分量（alpha、红色、绿色和蓝色）值创建 Color 结构，这里设置透明度为153 
                //这个画笔为描绘正式文字的笔刷，呈白色 
                //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

                ////第二次绘制这个图形，建立在第一次描绘的基础上 
                //grPhoto.DrawString(waterWords,                 //string of text 
                //                           crFont,                                   //font 
                //                           semiTransBrush,                           //Brush 
                //                           new PointF(xPosOfWm, yPosOfWm), //Position 
                //                           StrFormat);
                //释放资源，将定义的Graphics实例grPhoto释放，grPhoto功德圆满 
                grPhoto.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (grPhoto != null)
                    grPhoto.Dispose();
            }
        }

        #endregion

        #region 在图片上添加透明水印图片

        /// <summary>
        ///     图片加水印：在图片上添加透明水印文字
        /// </summary>
        /// <param name="srcImg">
        ///     原来图片地址(路径+文件名)
        /// </param>
        /// <param name="subImg">
        ///     需要添加到图片上的图片
        /// </param>
        /// <param name="alpha">
        ///     透明度(0.1~1.0之间)
        /// </param>
        /// <param name="imgPosition"></param>
        /// <param name="rewrite">
        ///     是否覆盖原图片(如果不覆盖，那么将在同目录下生成一个文件名带0607的文件)
        /// </param>
        /// <param name="errMsg"></param>
        public static bool DrawImages(string srcImg, string subImg, float alpha, ImagePosition imgPosition,
            bool rewrite, out string errMsg)
        {
            errMsg = string.Empty;
            if (!File.Exists(srcImg))
            {
                errMsg = "文件不存在！";
                return false;
            }
            string fileExtension = Path.GetExtension(srcImg).ToLower();
            if (fileExtension != ".gif" && fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".bmp")
            {
                errMsg = "不是图片文件！";
                return false;
            }

            Image imgPhoto = null;
            Image copyImage = null;
            Graphics grPhoto = null;
            MemoryStream ms_s = null;
            MemoryStream ms_i = null;
            try
            {
                //创建一个图片对象用来装载要被添加水印的图片 
                //将图片读成文件流 
                var fs_s = new FileStream(srcImg, FileMode.Open);
                var bt_s = new byte[int.Parse(fs_s.Length.ToString())];
                //将文件流字节码放进数组 
                fs_s.Read(bt_s, 0, int.Parse(fs_s.Length.ToString()));
                fs_s.Close();
                fs_s.Dispose();
                ms_s = new MemoryStream(bt_s);
                imgPhoto = Image.FromStream(ms_s);

                //获取图片的宽和高 
                int phWidth = imgPhoto.Width;
                int phHeight = imgPhoto.Height;

                //Graphics：封装一个 GDI+ 绘图图面。 
                grPhoto = Graphics.FromImage(imgPhoto);

                //设置图形的品质 
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                //将我们要添加水印的图片按照原始大小描绘（复制）到图形中 
                grPhoto.DrawImage(
                    imgPhoto, //   要添加水印的图片 
                    new Rectangle(0, 0, phWidth, phHeight), // 根据要添加的水印图片的宽和高 
                    0, // X方向从0点开始描绘 
                    0, // Y方向   
                    phWidth, // X方向描绘长度 
                    phHeight, // Y方向描绘长度 
                    GraphicsUnit.Pixel); // 描绘的单位，这里用的是像素

                //限制水印图片的大小为要添加水印的图片的十分之一
                //将图片读成文件流 
                var fs_i = new FileStream(subImg, FileMode.Open);
                var bt_i = new byte[int.Parse(fs_i.Length.ToString())];
                //将文件流字节码放进数组 
                fs_i.Read(bt_i, 0, int.Parse(fs_i.Length.ToString()));
                fs_i.Close();
                fs_i.Dispose();
                ms_i = new MemoryStream(bt_i);
                copyImage = Image.FromStream(ms_i);

                int wmHeight = copyImage.Height;
                int wmWidth = copyImage.Width;
                int rate = copyImage.Width/(phWidth/10);
                if (rate == 0 || rate == 1)
                {
                    wmHeight = copyImage.Height;
                    wmWidth = copyImage.Width;
                }
                else
                {
                    wmWidth = phWidth/10;
                    wmHeight = phHeight/(rate + 2);
                }
                //控制水印图片的位置
                int xPosOfWm;
                int yPosOfWm;
                switch (imgPosition)
                {
                    case ImagePosition.BottomMiddle:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.Center:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = phHeight/2;
                        break;
                    case ImagePosition.LeftBottom:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.LeftTop:
                        xPosOfWm = wmWidth/2;
                        yPosOfWm = wmHeight/2;
                        break;
                    case ImagePosition.RightTop:
                        xPosOfWm = phWidth - wmWidth - 10;
                        yPosOfWm = wmHeight;
                        break;
                    case ImagePosition.RigthBottom:
                        xPosOfWm = phWidth - wmWidth - 10;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.TopMiddle:
                        xPosOfWm = phWidth/2;
                        yPosOfWm = wmWidth;
                        break;
                    default:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                }

                //控制水印图片的透明度
                var imageAttributes = new ImageAttributes();
                var colorMap = new ColorMap();

                colorMap.OldColor = System.Drawing.Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = System.Drawing.Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = {colorMap};

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements =
                {
                    new[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                    new[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                    new[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                    new[] {0.0f, 0.0f, 0.0f, alpha, 0.0f},
                    new[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                };

                var wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //复制水印图片到原图片上

                grPhoto.DrawImage(copyImage, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, copyImage.Width,
                    copyImage.Height, GraphicsUnit.Pixel, imageAttributes);

                //释放资源，将定义的Graphics实例grPhoto释放，grPhoto功德圆满 
                grPhoto.Dispose();

                //将grPhoto保存 
                if (rewrite)
                {
                    //if (File.Exists(sourcePicture))
                    //{
                    //    File.Delete(sourcePicture);
                    //}
                    imgPhoto.Save(srcImg.Replace(Path.GetExtension(srcImg), "") + fileExtension);
                }
                else
                {
                    // 目标图片名称及全路径 
                    string targetImage = srcImg.Replace(Path.GetExtension(srcImg), "") + "_new" +
                                         fileExtension;
                    imgPhoto.Save(targetImage);
                }
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            finally
            {
                if (ms_s != null)
                {
                    ms_s.Close();
                    ms_s.Dispose();
                }
                if (ms_i != null)
                {
                    ms_i.Close();
                    ms_i.Dispose();
                }
                if (imgPhoto != null)
                    imgPhoto.Dispose();
                if (grPhoto != null)
                    grPhoto.Dispose();
            }
        }

        #endregion

        #region 添加水印位置

        /// <summary>
        ///     添加水印位置
        /// </summary>
        public enum ImagePosition
        {
            /// <summary>
            ///     左上
            /// </summary>
            LeftTop,

            /// <summary>
            ///     左下
            /// </summary>
            LeftBottom,

            /// <summary>
            ///     右上
            /// </summary>
            RightTop,

            /// <summary>
            ///     右下
            /// </summary>
            RigthBottom,

            /// <summary>
            ///     顶部居中
            /// </summary>
            TopMiddle,

            /// <summary>
            ///     底部居中
            /// </summary>
            BottomMiddle,

            /// <summary>
            ///     中心
            /// </summary>
            Center,
            LeftMiddle,
        }

        #endregion

        #region 获取缩略图

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
            g.Clear(System.Drawing.Color.WhiteSmoke);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //可以在这里设置填充背景颜色
            //g.Clear(System.Drawing.Color.White);

            g.DrawImage(imgSource, new Rectangle((destWidth - sW)/2, (destHeight - sH)/2, sW, sH), 0, 0, imgSource.Width,
                imgSource.Height,
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
                ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegIci = arrayIci.FirstOrDefault(t => t.FormatDescription.Equals("JPEG"));

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

        #endregion
    }
}

/* 图像的Gamma变换:一种提高图像亮度的方法，但是是非直线的变换，更加适合人眼的观察方式。
  
 * 我们在处理RGB 的图像时经常遭遇到一个非常令人讨厌的问题，那就是色彩的准确度问题。
 * RGB 的图像往往会因为搭配的硬件有所不同而出现不一致的结果。所以经常出现的问题就
 * 是：在某一操作平台所制作的图像到了另外一台机器上看就不是那么回事了。例如，一张
 * 在 PC 上制作出的杰作移到了MAC上浏览就变得灰灰白白的甚至有点褪色的样子。
 * 这个问题是因为并非所有的显示器都是一个样的，常常会因为显示器摆放位置周围的以及
 * 亮度的调整值不同而无法一致。但是RGB 各数值与实际屏幕屏幕上所显示的色彩几乎是一
 * 模一样的。例如当我们将红色频设置为 200 时，理论上应该就会比红色频设置为 100 时
 * 看来明亮 2 倍，但实际上并非如此。而实际影响这种结果的因素，我们称他为gamma。
 
  (gamma为参数，r，g,   b为原图某象素，r',g',b'为目的像素)  
      r'=   max(0,min(255,(r/255)^gamma   *   255))  
      g'=   max(0,min(255,(g/255)^gamma   *   255))  
      b'=   max(0,min(255,(b/255)^gamma   *   255))   
  
  利用以上公式可用来矫正显示器亮度的非线性.定性关系可由下面的推导得出:  
   
  看看gamma值的变化对函数关系(r,g,b)->(r',g',b')值的变化的影响:  
   
  当gamma=1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      =   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'=   g,   b'=   b,也即r,g,b成份都不变,因而亮度也不变  
   
  当gamma>1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      >   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'>   g,   b'>   b,也即r,g,b成份都变大,因而亮度也变大  
   
  当gamma<1时,  
  r'=   max(0,min(255,(r/255)^gamma   *   255))  
      <   max(0,min(255,(r/255)   *   255))  
      =   max(0,min(255,r))  
      =   max(0,r)  
      =   r  
  同样可知   g'<   g,   b'<   b,也即r,g,b成份都变小,因而亮度也变小  
   
  可以根据gamma值的变化来定量地画出(r,b,b)->(r',g',b')亮度变化的曲线,详见  
   
  http://www.zzwu.net/free/zzwu/gamma.htm   
 */

/* Demo
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CSharpFilters
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Drawing.Bitmap m_Bitmap;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem FileLoad;
        private System.Windows.Forms.MenuItem FileSave;
        private System.Windows.Forms.MenuItem FileExit;
        private System.Windows.Forms.MenuItem FilterInvert;
        private System.Windows.Forms.MenuItem FilterGrayScale;
        private System.Windows.Forms.MenuItem FilterBrightness;
        private System.Windows.Forms.MenuItem FilterContrast;
        private System.Windows.Forms.MenuItem FilterGamma;
        private System.Windows.Forms.MenuItem FilterColor;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem Zoom25;
        private double Zoom = 1.0;
        private System.Windows.Forms.MenuItem Zoom50;
        private System.Windows.Forms.MenuItem Zoom100;
        private System.Windows.Forms.MenuItem Zoom150;
        private System.Windows.Forms.MenuItem Zoom200;
        private System.Windows.Forms.MenuItem Zoom300;
        private System.Windows.Forms.MenuItem Zoom500;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            InitializeComponent();

            m_Bitmap= new Bitmap(2, 2);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.FileLoad = new System.Windows.Forms.MenuItem();
            this.FileSave = new System.Windows.Forms.MenuItem();
            this.FileExit = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.FilterInvert = new System.Windows.Forms.MenuItem();
            this.FilterGrayScale = new System.Windows.Forms.MenuItem();
            this.FilterBrightness = new System.Windows.Forms.MenuItem();
            this.FilterContrast = new System.Windows.Forms.MenuItem();
            this.FilterGamma = new System.Windows.Forms.MenuItem();
            this.FilterColor = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.Zoom25 = new System.Windows.Forms.MenuItem();
            this.Zoom50 = new System.Windows.Forms.MenuItem();
            this.Zoom100 = new System.Windows.Forms.MenuItem();
            this.Zoom150 = new System.Windows.Forms.MenuItem();
            this.Zoom200 = new System.Windows.Forms.MenuItem();
            this.Zoom300 = new System.Windows.Forms.MenuItem();
            this.Zoom500 = new System.Windows.Forms.MenuItem();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.menuItem1,
                                                                                      this.menuItem4,
                                                                                      this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.FileLoad,
                                                                                      this.FileSave,
                                                                                      this.FileExit});
            this.menuItem1.Text = "File";
            // 
            // FileLoad
            // 
            this.FileLoad.Index = 0;
            this.FileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.FileLoad.Text = "Load";
            this.FileLoad.Click += new System.EventHandler(this.File_Load);
            // 
            // FileSave
            // 
            this.FileSave.Index = 1;
            this.FileSave.Text = "Save";
            this.FileSave.Click += new System.EventHandler(this.File_Save);
            // 
            // FileExit
            // 
            this.FileExit.Index = 2;
            this.FileExit.Text = "Exit";
            this.FileExit.Click += new System.EventHandler(this.File_Exit);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.FilterInvert,
                                                                                      this.FilterGrayScale,
                                                                                      this.FilterBrightness,
                                                                                      this.FilterContrast,
                                                                                      this.FilterGamma,
                                                                                      this.FilterColor});
            this.menuItem4.Text = "Filter";
            // 
            // FilterInvert
            // 
            this.FilterInvert.Index = 0;
            this.FilterInvert.Text = "Invert";
            this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
            // 
            // FilterGrayScale
            // 
            this.FilterGrayScale.Index = 1;
            this.FilterGrayScale.Text = "GrayScale";
            this.FilterGrayScale.Click += new System.EventHandler(this.Filter_GrayScale);
            // 
            // FilterBrightness
            // 
            this.FilterBrightness.Index = 2;
            this.FilterBrightness.Text = "Brightness";
            this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
            // 
            // FilterContrast
            // 
            this.FilterContrast.Index = 3;
            this.FilterContrast.Text = "Contrast";
            this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
            // 
            // FilterGamma
            // 
            this.FilterGamma.Index = 4;
            this.FilterGamma.Text = "Gamma";
            this.FilterGamma.Click += new System.EventHandler(this.Filter_Gamma);
            // 
            // FilterColor
            // 
            this.FilterSystem.Drawing.Color.Index = 5;
            this.FilterSystem.Drawing.Color.Text = "Color";
            this.FilterSystem.Drawing.Color.Click += new System.EventHandler(this.Filter_Color);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.Zoom25,
                                                                                      this.Zoom50,
                                                                                      this.Zoom100,
                                                                                      this.Zoom150,
                                                                                      this.Zoom200,
                                                                                      this.Zoom300,
                                                                                      this.Zoom500});
            this.menuItem2.Text = "Zoom";
            // 
            // Zoom25
            // 
            this.Zoom25.Index = 0;
            this.Zoom25.Text = "25%";
            this.Zoom25.Click += new System.EventHandler(this.OnZoom25);
            // 
            // Zoom50
            // 
            this.Zoom50.Index = 1;
            this.Zoom50.Text = "50%";
            this.Zoom50.Click += new System.EventHandler(this.OnZoom50);
            // 
            // Zoom100
            // 
            this.Zoom100.Index = 2;
            this.Zoom100.Text = "100%";
            this.Zoom100.Click += new System.EventHandler(this.OnZoom100);
            // 
            // Zoom150
            // 
            this.Zoom150.Index = 3;
            this.Zoom150.Text = "150%";
            this.Zoom150.Click += new System.EventHandler(this.OnZoom150);
            // 
            // Zoom200
            // 
            this.Zoom200.Index = 4;
            this.Zoom200.Text = "200%";
            this.Zoom200.Click += new System.EventHandler(this.OnZoom200);
            // 
            // Zoom300
            // 
            this.Zoom300.Index = 5;
            this.Zoom300.Text = "300%";
            this.Zoom300.Click += new System.EventHandler(this.OnZoom300);
            // 
            // Zoom500
            // 
            this.Zoom500.Index = 6;
            this.Zoom500.Text = "500%";
            this.Zoom500.Click += new System.EventHandler(this.OnZoom500);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(488, 421);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Graphic Filters For Dummies";
            this.Load += new System.EventHandler(this.Form1_Load);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }

        protected override void OnPaint (PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width*Zoom), (int)(m_Bitmap.Height * Zoom)));
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
        }

        private void File_Load(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\" ;
            openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg";
            openFileDialog.FilterIndex = 2 ;
            openFileDialog.RestoreDirectory = true ;

            if(DialogResult.OK == openFileDialog.ShowDialog())
            {
                m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
                this.AutoScroll = true;
                this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
                this.Invalidate();
            }
        }

        private void File_Save(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = "c:\\" ;
            saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg" ;
            saveFileDialog.FilterIndex = 1 ;
            saveFileDialog.RestoreDirectory = true ;

            if(DialogResult.OK == saveFileDialog.ShowDialog())
            {
                m_Bitmap.Save(saveFileDialog.FileName);
            }
        }

        private void File_Exit(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Filter_Invert(object sender, System.EventArgs e)
        {
            if(BitmapFilter.Invert(m_Bitmap))
                this.Invalidate();
        }

        private void Filter_GrayScale(object sender, System.EventArgs e)
        {
            if(BitmapFilter.GrayScale(m_Bitmap))
                this.Invalidate();
        }

        private void Filter_Brightness(object sender, System.EventArgs e)
        {
            Parameter dlg = new Parameter();
            dlg.nValue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if(BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
                    this.Invalidate();
            }
        }

        private void Filter_Contrast(object sender, System.EventArgs e)
        {
            Parameter dlg = new Parameter();
            dlg.nValue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if(BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
                    this.Invalidate();
            }
        
        }

        private void Filter_Gamma(object sender, System.EventArgs e)
        {
            GammaInput dlg = new GammaInput();
            dlg.red = dlg.green = dlg.blue = 1;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if(BitmapFilter.Gamma(m_Bitmap, dlg.red, dlg.green, dlg.blue))
                    this.Invalidate();
            }
        }

        private void Filter_Color(object sender, System.EventArgs e)
        {
            ColorInput dlg = new ColorInput();
            dlg.red = dlg.green = dlg.blue = 0;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                if(BitmapFilter.Color(m_Bitmap, dlg.red, dlg.green, dlg.blue))
                    this.Invalidate();
            }
        
        }

        private void OnZoom25(object sender, System.EventArgs e)
        {
            Zoom = .25;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom50(object sender, System.EventArgs e)
        {
            Zoom = .5;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom100(object sender, System.EventArgs e)
        {
            Zoom = 1.0;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom150(object sender, System.EventArgs e)
        {
            Zoom = 1.5;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom200(object sender, System.EventArgs e)
        {
            Zoom = 2.0;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom300(object sender, System.EventArgs e)
        {
            Zoom = 3.0;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }

        private void OnZoom500(object sender, System.EventArgs e)
        {
            Zoom = 5;
            this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
            this.Invalidate();
        }
    }
}
*/