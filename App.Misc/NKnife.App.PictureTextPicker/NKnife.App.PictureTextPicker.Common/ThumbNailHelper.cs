using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using NKnife.Adapters;
using NKnife.Interface;

namespace NKnife.App.PictureTextPicker.Common
{
    public class ThumbNailHelper
    {
        private static readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();

        /// <summary>
        /// 指定目录下的所有图片生成缩略图,缩略图在参数thumbNailDirectory文件夹中(生成后的尺寸以参数按比例缩小)
        /// </summary>
        /// <param name="pictureDirectory">图片所在目录</param>
        /// <param name="thumbNailDirectory">缩略图所在目录</param>
        /// <param name="thumbNailWidth">缩略图的长</param>
        /// <param name="thumbNailHeight">缩略图的高</param>
        /// <param name="pictureType">图片类别，如："*.jpg"、"*.gif"</param>
        /// <param name="fixSize">是否固定尺寸，true 固定；false 不固定</param>
        public static void GetSmallPicListMethod(string pictureDirectory,string thumbNailDirectory, int thumbNailWidth, int thumbNailHeight, string pictureType, bool fixSize)//
        {
            if (!Directory.Exists(pictureDirectory))
            {
                _Logger.Warn("指定图片目录不存在");
                return;
            }
            if (Directory.Exists(thumbNailDirectory))
            {
                try
                {
                    Directory.Delete(thumbNailDirectory, true);
                }
                catch (IOException)
                {
                    _Logger.Error("缩略图片目录正在使用中，无法重建");
                    return;
                }
            }
            Directory.CreateDirectory(thumbNailDirectory);

            string[] picPathList = Directory.GetFiles(pictureDirectory, pictureType);
            foreach (var tempPicPath in picPathList)
            {
                GetSmallPicMethod(tempPicPath, thumbNailDirectory, thumbNailWidth, thumbNailHeight, pictureType, fixSize);
            }
        }

        /// <summary>
        ///  把指定图片生成缩略图,缩略图在参数picFilePath目录下的temp文件夹中(生成后的尺寸以参数按比例缩小)
        ///  </summary>
        /// <param name="fullFilePath">图片完整路径（包含目录和文件名）</param>
        /// <param name="thumbNailDirectory">缩略图路径</param>
        /// <param name="width">缩略图的长</param>
        ///  <param name="height">缩略图的高</param>
        ///  <param name="picType">图片类别，如："*.jpg"、"*.gif"</param>

        public static void GetSmallPicMethod(string fullFilePath, string thumbNailDirectory, int width, int height, string picType, bool isFix)
        {
            if (!File.Exists(fullFilePath))
            {
                _Logger.Warn("指定图片不存在");
                return;
            }
            if (!Directory.Exists(thumbNailDirectory))
            {
                Directory.CreateDirectory(thumbNailDirectory);
            }


            var picName = Path.GetFileName(fullFilePath);
            if (picName == null)
                return;
            var originalFilename = Path.Combine(thumbNailDirectory,picName);
            //fp.FileBytes
            //缩小的倍数
            //从文件取得图片对象
            Image image = null;
            try
            {
                image = Image.FromFile(fullFilePath);
            }
            catch (FileNotFoundException)
            {
                _Logger.Error("图片不存在");
                if (image != null) image.Dispose();
                return;
            }
            catch (OutOfMemoryException)
            {
                _Logger.Error("图片太大，内存不足无法读取");
                if (image != null) image.Dispose();
                return;
            }
            catch (ArgumentException)
            {
                _Logger.Error("指定的图片路径错误");
                if (image != null) image.Dispose();
                return;
            }
            if (image.Width > width || image.Height > height)
            {
                int hi;
                int wi;
                if (!isFix)
                {
                    if (image.Width > image.Height)
                    {
                        wi = width;
                        hi = (int)Math.Floor(width / ((double)image.Width / image.Height));
                    }
                    else if (image.Width < image.Height)
                    {
                        hi = height;
                        wi = (int)Math.Floor(((double)image.Width / image.Height) * height);
                    }
                    else
                    {
                        if (width > height)
                        {
                            wi = width;
                            hi = width;
                        }
                        else
                        {
                            wi = height;
                            hi = height;
                        }
                    }
                }
                else
                {
                    wi = width;
                    hi = height;
                }
                var size = new Size(wi, hi);
                //新建一个bmp图片
                Image bitmap = new Bitmap(size.Width, size.Height);
                //新建一个画板
                var g = Graphics.FromImage(bitmap);
                //设置高质量插值法
                g.InterpolationMode = InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = SmoothingMode.HighQuality;
                //清空一下画布
                g.Clear(Color.Transparent);
                //在指定位置画图
                g.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                if (picName.ToLower().Contains(".jpg") || picName.Contains(".jpeg"))
                    bitmap.Save(originalFilename, ImageFormat.Jpeg);
                if (picName.ToLower().Contains(".gif"))
                    bitmap.Save(originalFilename, ImageFormat.Gif);
                if (picName.ToLower().Contains(".bmp"))
                    bitmap.Save(originalFilename, ImageFormat.Bmp);
                if (picName.ToLower().Contains(".tif"))
                    bitmap.Save(originalFilename, ImageFormat.Tiff);
                if (picName.ToLower().Contains(".png"))
                    bitmap.Save(originalFilename, ImageFormat.Png);
                g.Dispose();
                bitmap.Dispose();
                image.Dispose();
            }
            else
            {
                var picFile = new FileInfo(fullFilePath);
                picFile.CopyTo(originalFilename, true);
            }

        }
    }
}
