package net.xknife.io.image;

import com.sun.image.codec.jpeg.JPEGCodec;
import com.sun.image.codec.jpeg.JPEGEncodeParam;
import com.sun.image.codec.jpeg.JPEGImageEncoder;
import net.xknife.io.image.domain.PrintDocumentBean;
import net.xknife.io.image.domain.PrintDocumentBeanList;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.geom.AffineTransform;
import java.awt.image.*;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.URL;

/**
 * 图片帮助类：添加文字到图片制定位置，图片合并
 * <p/>
 * Created by yangjuntao@xknife.net on 2014/8/16 0016.
 */
public class ImageHelper
{
    private static final Logger _Logger = LoggerFactory.getLogger(ImageHelper.class);

    private Font font = new Font("微软雅黑", Font.PLAIN, 40);
    private int fontSize = 0;

    public void setFont(String fontStyle, int fontSize)
    {
        this.fontSize = fontSize;
        this.font = new Font(fontStyle, Font.PLAIN, fontSize);
    }

    public Graphics2D createGraphics2D(BufferedImage img)
    {
        Graphics2D g = img.createGraphics();
        g.setBackground(Color.WHITE);
        g.setColor(Color.BLACK);//设置字体颜色
        if (this.font != null)
        {
            g.setFont(this.font);
        }

        return g;
    }

    /**
     * 导入本地图片到缓冲区
     */
    public BufferedImage loadImageLocal(String imgName)
    {
        try
        {
            File file = new File(imgName);
            return ImageIO.read(file);
        } catch (IOException e)
        {
            _Logger.warn("警告（图片不存在），路径 :: " + imgName);
        }
        return null;
    }

    /**
     * 导入网络图片到缓冲区
     */
    public BufferedImage loadImageURL(String imgName)
    {
        try
        {
            URL url = new URL(imgName);
            return ImageIO.read(url);
        } catch (IOException e)
        {
            _Logger.warn("loadImageURL", e);
        }
        return null;
    }

    /**
     * 生成新图片到本地
     */
    public void writeImageLocal(String newImage, BufferedImage img)
    {
        if (newImage != null && img != null)
        {
            try
            {
                File newFile = new File(newImage);
                ImageIO.write(img, "jpg", newFile);
            } catch (IOException e)
            {
                _Logger.warn("writeImageLocal", e);
            }
        }
    }

    /**
     * 修改图片,返回修改后的图片缓冲区（只输出一行文本）
     */
    public BufferedImage modifyImage(BufferedImage img, Object content, int x, int y)
    {
        Graphics2D g = createGraphics2D(img);

        try
        {
            int w = img.getWidth();
            int h = img.getHeight();
            // 验证输出位置的纵坐标和横坐标
            if (x >= h || y >= w)
            {
                x = h - this.fontSize + 2;
                y = w;
            }
            if (content != null)
            {
                g.drawString(content.toString(), x, y);
            }
            g.dispose();
        } catch (Exception e)
        {
            _Logger.warn("", e);
        }

        return img;
    }

    /**
     * 修改图片, 在图片指定位置填写内容
     */
    public BufferedImage modifyImage(BufferedImage img, PrintDocumentBeanList beans)
    {
        Graphics2D g = createGraphics2D(img);
        int w = img.getWidth();
        int h = img.getHeight();

        try
        {
            for (PrintDocumentBean bean : beans)
            {
                double top = Double.valueOf(bean.getTop());
                double left = Double.valueOf(bean.getLeft());
                if (top >= h || left >= w)
                {
                    _Logger.warn(String.format("打印内容%s(top:%d,left:%d)超出图片范围", top, left));
                    continue;
                }
                String content = bean.getContent();
                if (content != null)
                {
                    g.drawString(content, (int) left, (int) top);
                }
            }

            g.dispose();
        } catch (Exception e)
        {
            _Logger.warn("", e);
        }

        return img;
    }

    /**
     * 合并图片
     *
     * @param top
     * @param bottom
     * @return
     */
    public BufferedImage modifyImage(BufferedImage top, BufferedImage bottom, int x, int y)
    {
        Graphics2D g;
        try
        {
            int w = top.getWidth();
            int h = top.getHeight();

            g = bottom.createGraphics();
            g.drawImage(top, x, y, w, h, null);
            g.dispose();
        } catch (Exception e)
        {
            _Logger.warn("", e);
        }

        return bottom;
    }

    /**
     * 修改图片,返回修改后的图片缓冲区（输出多个文本段）
     *
     * @param img
     * @param contentArr
     * @param x
     * @param y
     * @param isOneLine  true表示将内容在一行中输出；false表示将内容多行输出
     * @return
     */
    public BufferedImage modifyImage(BufferedImage img, Object[] contentArr, int x, int y, boolean isOneLine)
    {
        Graphics2D g = createGraphics2D(img);
        try
        {
            int w = img.getWidth();
            int h = img.getHeight();

            // 验证输出位置的纵坐标和横坐标
            if (x >= h || y >= w)
            {
                x = h - this.fontSize + 2;
                y = w;
            }
            if (contentArr != null)
            {
                int ContentLen = contentArr.length;
                if (isOneLine)
                {
                    for (int i = 0; i < ContentLen; i++)
                    {
                        g.drawString(contentArr[i].toString(), x, y);
                        x += contentArr[i].toString().length() * this.fontSize / 2 + 5;// 重新计算文本输出位置
                    }
                } else
                {
                    for (int i = 0; i < ContentLen; i++)
                    {
                        g.drawString(contentArr[i].toString(), x, y);
                        y += this.fontSize + 2;// 重新计算文本输出位置
                    }
                }
            }
            g.dispose();
        } catch (Exception e)
        {
            _Logger.warn("", e);
        }

        return img;
    }

    public void resize(File originalFile, File resizedFile, int newWidth, float quality) throws IOException
    {

        if (quality > 1)
        {
            throw new IllegalArgumentException("Quality has to be between 0 and 1");
        }

        ImageIcon ii = new ImageIcon(originalFile.getCanonicalPath());
        Image i = ii.getImage();

        int iWidth = i.getWidth(null);
        int iHeight = i.getHeight(null);

        Image resizedImage = null;
        if (iWidth > iHeight)
        {
            resizedImage = i.getScaledInstance(newWidth, (newWidth * iHeight) / iWidth, Image.SCALE_SMOOTH);
        } else
        {
            resizedImage = i.getScaledInstance((newWidth * iWidth) / iHeight, newWidth, Image.SCALE_SMOOTH);
        }

        // This code ensures that all the pixels in the image are loaded.
        Image temp = new ImageIcon(resizedImage).getImage();

        // Create the buffered image.
        BufferedImage bufferedImage = new BufferedImage(temp.getWidth(null), temp.getHeight(null), BufferedImage.TYPE_INT_RGB);

        // Copy image to buffered image.
        Graphics g = bufferedImage.createGraphics();

        // Clear background and paint the image.
        g.setColor(Color.white);
        g.fillRect(0, 0, temp.getWidth(null), temp.getHeight(null));
        g.drawImage(temp, 0, 0, null);
        g.dispose();

        // Soften.
        float softenFactor = 0.05f;
        float[] softenArray = {0, softenFactor, 0, softenFactor, 1 - (softenFactor * 4), softenFactor, 0, softenFactor, 0};
        Kernel kernel = new Kernel(3, 3, softenArray);
        ConvolveOp cOp = new ConvolveOp(kernel, ConvolveOp.EDGE_NO_OP, null);
        bufferedImage = cOp.filter(bufferedImage, null);

        // Write the jpeg to a file.
        FileOutputStream out = new FileOutputStream(resizedFile);

        // Encodes image as a JPEG data stream
        JPEGImageEncoder encoder = JPEGCodec.createJPEGEncoder(out);

        JPEGEncodeParam param = encoder.getDefaultJPEGEncodeParam(bufferedImage);

        param.setQuality(quality, true);

        encoder.setJPEGEncodeParam(param);
        encoder.encode(bufferedImage);

        out.flush();
        out.close();
    }

    /**
     * 执行转换.压缩
     */
    public static boolean executeConvert(File source, File dest, int width, int height)
    {
        boolean result = false;

        // 源图必须存在
        if (null == source || false == source.exists())
        {
            return result;
        }
        // 目标图片不能存在
        if (null == dest || true == dest.exists())
        {
            return result;
        }

        BufferedImage srcImage = null;
        try
        {
            srcImage = ImageIO.read(source);
        } catch (IOException e)
        {
            e.printStackTrace();
        }
        String imgType = "JPEG";

        if (null != srcImage)
        {
            BufferedImage dstImage = resize(srcImage, width, height);

            if (null != dstImage)
            {
                try
                {
                    ImageIO.write(dstImage, imgType, dest);
                    result = true;
                } catch (IOException e)
                {
                    e.printStackTrace();
                }
            }
        }

        return result;
    }

    public static BufferedImage resize(BufferedImage source, int targetW, int targetH)
    {
        // targetW，targetH分别表示目标长和宽
        int type = source.getType();

        double sx = (double) targetW / source.getWidth();
        double sy = (double) targetH / source.getHeight();
        // 这里想实现在targetW，targetH范围内实现等比缩放。如果不需要等比缩放
        // 则将下面的if else语句注释即可
        if (sx > sy)
        {
            sx = sy;
            targetW = (int) (sx * source.getWidth());
        } else
        {
            sy = sx;
            targetH = (int) (sy * source.getHeight());
        }

        BufferedImage target;
        if (type == BufferedImage.TYPE_CUSTOM)
        {
            // handmade
            ColorModel cm = source.getColorModel();
            WritableRaster raster = cm.createCompatibleWritableRaster(targetW, targetH);
            boolean alphaPreMultiplied = cm.isAlphaPremultiplied();
            target = new BufferedImage(cm, raster, alphaPreMultiplied, null);
        } else
        {
            target = new BufferedImage(targetW, targetH, type);
        }
        Graphics2D g = target.createGraphics();
        // smoother than exLax:
        g.setRenderingHint(RenderingHints.KEY_RENDERING, RenderingHints.VALUE_RENDER_QUALITY);
        g.drawRenderedImage(source, AffineTransform.getScaleInstance(sx, sy));
        g.dispose();

        return target;
    }

}
