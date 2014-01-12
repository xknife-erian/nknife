using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

//代码

namespace NKnife.Graphics
{

    public class GraphUtil
    {

        #region 添加水印位置
        /// <summary>
        /// 添加水印位置
        /// </summary>
        public enum ImagePosition
        {
            /**/
            /// < summary> 
            /// 左上 
            /// < /summary> 
            LeftTop,
            /**/
            /// < summary> 
            /// 左下 
            /// < /summary> 
            LeftBottom,
            /**/
            /// < summary> 
            /// 右上 
            /// < /summary> 
            RightTop,
            /**/
            /// < summary> 
            /// 右下 
            /// < /summary> 
            RigthBottom,
            /**/
            /// < summary> 
            /// 顶部居中 
            /// < /summary> 
            TopMiddle,
            /**/
            /// < summary> 
            /// 底部居中 
            /// < /summary> 
            BottomMiddle,
            /**/
            /// < summary> 
            /// 中心 
            /// < /summary> 
            Center,
            LeftMiddle,
        }
        #endregion

        #region 在图片上添加透明水印文字
        /// < summary> 
        /// ASP.NET图片加水印：在图片上添加透明水印文字 
        /// < /summary> 
        /// < param name="sourcePicture">原来图片地址(路径+文件名)< /param> 
        /// < param name="waterWords">需要添加到图片上的文字< /param> 
        /// < param name="alpha">透明度(0.1~1.0之间)< /param> 
        /// < param name="position">文字显示的位置< /param> 
        /// < param name="fRewrite">是否覆盖原图片(如果不覆盖，那么将在同目录下生成一个文件名带0607的文件)< /param> 
        /// < returns>< /returns> 
        public static bool DrawWords(ref Image imgPhoto, string waterWords, float alpha,float angle, ImagePosition position)
        {
            System.Drawing.Graphics grPhoto = null;
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
                grPhoto = System.Drawing.Graphics.FromImage(imgPhoto);

                //设置图形的品质 
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                //将我们要添加水印的图片按照原始大小描绘（复制）到图形中 
                grPhoto.DrawImage(
                 imgPhoto,                                           //   要添加水印的图片 
                 new Rectangle(0, 0, phWidth, phHeight), // 根据要添加的水印图片的宽和高 
                 0,                                                     // X方向从0点开始描绘 
                 0,                                                     // Y方向   
                 phWidth,                                            // X方向描绘长度 
                 phHeight,                                           // Y方向描绘长度 
                 GraphicsUnit.Pixel);                              // 描绘的单位，这里用的是像素

                //根据图片的大小我们来确定添加上去的文字的大小 
                //在这里我们定义一个数组来确定 
                int[] sizes = new int[] { 48, 36, 28, 24, 16, 14, 12, 10 };

                //字体 
                Font crFont = null;
                //矩形的宽度和高度，SizeF有三个属性，分别为Height高，width宽，IsEmpty是否为空 
                SizeF crSize = new SizeF();

                //利用一个循环语句来选择我们要添加文字的型号
                //直到它的长度比图片的宽度小
                for (int i = 0; i < sizes.Length; i++)
                {
                    crFont = new Font("arial", sizes[i], FontStyle.Bold);

                    //测量用指定的 Font 对象绘制并用指定的 StringFormat 对象格式化的指定字符串。 
                    crSize = grPhoto.MeasureString(waterWords, crFont);

                    // ushort 关键字表示一种整数数据类型 
                    if ((ushort)crSize.Width < (ushort)phWidth)
                        break;
                }

                //截边5%的距离，定义文字显示(由于不同的图片显示的高和宽不同，所以按百分比截取) 
                int yPixlesFromBottom = (int)(phHeight * .05);

                //定义在图片上文字的位置 
                float wmHeight = crSize.Height;
                float wmWidth = crSize.Width;

                float xPosOfWm;
                float yPosOfWm;

                //设置水印的位置 
                switch (position)
                {
                    case ImagePosition.BottomMiddle:
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.Center:
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = phHeight / 2;
                        break;
                    case ImagePosition.LeftMiddle:
                        xPosOfWm = 20;
                        yPosOfWm = phHeight / 2;
                        break;
                    case ImagePosition.LeftBottom:
                        xPosOfWm = wmWidth / 2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.LeftTop:
                        xPosOfWm = wmWidth / 2;
                        yPosOfWm = wmHeight / 2;
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
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = wmWidth;
                        break;
                    default:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                }
                //封装文本布局信息（如对齐、文字方向和 Tab 停靠位），显示操作（如省略号插入和国家标准 (National) 数字替换）和 OpenType 功能。 
                StringFormat StrFormat = new StringFormat();

                //定义需要印的文字居中对齐 
                StrFormat.Alignment = StringAlignment.Center;

                //SolidBrush:定义单色画笔。画笔用于填充图形形状，如矩形、椭圆、扇形、多边形和封闭路径。 
                //这个画笔为描绘阴影的画笔，呈灰色 
                int m_alpha = Convert.ToInt32(256 * alpha);
                SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(m_alpha, 200, 200, 200));

                //描绘文字信息，这个图层向右和向下偏移一个像素，表示阴影效果 
                //DrawString 在指定矩形并且用指定的 Brush 和 Font 对象绘制指定的文本字符串。 
                grPhoto.RotateTransform(angle);  
                grPhoto.DrawString(waterWords,                                    //string of text 
                                           crFont,                                         //font 
                                           semiTransBrush2,                            //Brush 
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
        /// < summary> 
        /// ASP.NET图片加水印：在图片上添加透明水印文字 
        /// < /summary> 
        /// < param name="sourcePicture">原来图片地址(路径+文件名)< /param> 
        /// < param name="waterPicture">需要添加到图片上的图片< /param> 
        /// < param name="alpha">透明度(0.1~1.0之间)< /param>   
        /// < param name="fRewrite">是否覆盖原图片(如果不覆盖，那么将在同目录下生成一个文件名带0607的文件)< /param> 
        /// < returns>< /returns> 
        public static bool DrawImages(string sourcePicture, string InsPicture, float alpha, ImagePosition position, bool fRewrite, out string ErrMsg)
        {
            ErrMsg = string.Empty;
            if (!File.Exists(sourcePicture))
            {
                ErrMsg = "文件不存在！";
                return false;
            }
            string fileExtension = Path.GetExtension(sourcePicture).ToLower();
            if (fileExtension != ".gif" && fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".bmp")
            {
                ErrMsg = "不是图片文件！";
                return false;
            }

            Image imgPhoto = null;
            Image copyImage = null;
            System.Drawing.Graphics grPhoto = null;
            MemoryStream ms_s = null;
            MemoryStream ms_i = null;
            try
            {
                //创建一个图片对象用来装载要被添加水印的图片 
                //将图片读成文件流 
                FileStream fs_s = new FileStream(sourcePicture, FileMode.Open);
                byte[] bt_s = new byte[int.Parse(fs_s.Length.ToString())];
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
                grPhoto = System.Drawing.Graphics.FromImage(imgPhoto);

                //设置图形的品质 
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

                //将我们要添加水印的图片按照原始大小描绘（复制）到图形中 
                grPhoto.DrawImage(
                 imgPhoto,                                           //   要添加水印的图片 
                 new Rectangle(0, 0, phWidth, phHeight), // 根据要添加的水印图片的宽和高 
                 0,                                                     // X方向从0点开始描绘 
                 0,                                                     // Y方向   
                 phWidth,                                            // X方向描绘长度 
                 phHeight,                                           // Y方向描绘长度 
                 GraphicsUnit.Pixel);                               // 描绘的单位，这里用的是像素

                //限制水印图片的大小为要添加水印的图片的十分之一
                //将图片读成文件流 
                FileStream fs_i = new FileStream(InsPicture, FileMode.Open);
                byte[] bt_i = new byte[int.Parse(fs_i.Length.ToString())];
                //将文件流字节码放进数组 
                fs_i.Read(bt_i, 0, int.Parse(fs_i.Length.ToString()));
                fs_i.Close();
                fs_i.Dispose();
                ms_i = new MemoryStream(bt_i);
                copyImage = Image.FromStream(ms_i);

                int wmHeight = copyImage.Height;
                int wmWidth = copyImage.Width;
                int rate = copyImage.Width / (phWidth / 10);
                if (rate == 0 || rate == 1)
                {
                    wmHeight = copyImage.Height;
                    wmWidth = copyImage.Width;
                }
                else
                {
                    wmWidth = phWidth / 10;
                    wmHeight = phHeight / (rate + 2);
                }
                //控制水印图片的位置
                int xPosOfWm;
                int yPosOfWm;
                switch (position)
                {
                    case ImagePosition.BottomMiddle:
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.Center:
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = phHeight / 2;
                        break;
                    case ImagePosition.LeftBottom:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                    case ImagePosition.LeftTop:
                        xPosOfWm = wmWidth / 2;
                        yPosOfWm = wmHeight / 2;
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
                        xPosOfWm = phWidth / 2;
                        yPosOfWm = wmWidth;
                        break;
                    default:
                        xPosOfWm = wmWidth;
                        yPosOfWm = phHeight - wmHeight - 10;
                        break;
                }

                //控制水印图片的透明度
                ImageAttributes imageAttributes = new ImageAttributes();
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = {
                                                new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                                new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                                new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                                new float[] {0.0f, 0.0f, 0.0f, alpha, 0.0f},
                                                new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                                };

                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //复制水印图片到原图片上

                grPhoto.DrawImage(copyImage, new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel, imageAttributes);

                //释放资源，将定义的Graphics实例grPhoto释放，grPhoto功德圆满 
                grPhoto.Dispose();

                //将grPhoto保存 
                if (fRewrite)
                {
                    //if (File.Exists(sourcePicture))
                    //{
                    //    File.Delete(sourcePicture);
                    //}
                    imgPhoto.Save(sourcePicture.Replace(Path.GetExtension(sourcePicture), "") + fileExtension);
                }
                else
                {
                    // 目标图片名称及全路径 
                    string targetImage = sourcePicture.Replace(Path.GetExtension(sourcePicture), "") + "_new" + fileExtension;
                    imgPhoto.Save(targetImage);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrMsg = ex.Message;
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
    }
}