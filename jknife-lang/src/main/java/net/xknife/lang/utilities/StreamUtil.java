package net.xknife.lang.utilities;

import java.io.*;

import net.xknife.lang.widgets.Encoding;

/**
 * 提供了一组创建 Reader/Writer/InputStream/OutputStream 的便利函数
 */
public class StreamUtil
{
    /**
     * 将指定的输入流进行复制，得到一个新的输入流
     * @param source
     * @return
     * @throws IOException
     */
    public static InputStream copy(InputStream source) throws IOException
    {
        //读取源流成为字节数组
        int count = source.available();
        byte[] b = new byte[count];
        source.read(b);
        source.close();
        //通过字节数组流复制出新的输入流
        InputStream in = new ByteArrayInputStream(b);
        return in;
    }

    /**
     * 判断两个输入流是否严格相等
     */
    public static boolean equals(final InputStream sA, final InputStream sB) throws IOException
    {
        int dA;
        while ((dA = sA.read()) != -1)
        {
            if (dA != sB.read())
            {
                return false;
            }
        }
        return sB.read() == -1;
    }

    /**
     * 将一段文本全部写入一个输出流
     *
     * @param ops 输出流
     * @param cs  文本
     * @throws IOException
     */
    public static void write(final OutputStream ops, final CharSequence cs) throws IOException
    {
        if ((null != cs) && (null != ops))
        {
            ops.write(cs.toString().getBytes());
        }
    }

    /**
     * 关闭一个可关闭对象，可以接受 null。如果成功关闭，返回 true，发生异常 返回 false
     *
     * @param cb 可关闭对象
     * @return 是否成功关闭
     */
    public static boolean safeClose(final Closeable cb)
    {
        if (null != cb)
        {
            try
            {
                cb.close();
            } catch (IOException e)
            {
                return false;
            }
        }
        return true;
    }

    public static void safeFlush(final Flushable fa)
    {
        if (null != fa)
        {
            try
            {
                fa.flush();
            } catch (IOException e)
            {
            }
        }
    }

    /**
     * 为一个输入流包裹一个缓冲流。如果这个输入流本身就是缓冲流，则直接返回
     *
     * @param ins 输入流。
     * @return 缓冲流
     */
    public static BufferedInputStream buff(final InputStream ins)
    {
        if (ins instanceof BufferedInputStream)
        {
            return (BufferedInputStream) ins;
        }
        return new BufferedInputStream(ins);
    }

    /**
     * 为一个输出流包裹一个缓冲流。如果这个输出流本身就是缓冲流，则直接返回
     *
     * @param ops 输出流。
     * @return 缓冲流
     */
    public static BufferedOutputStream buff(final OutputStream ops)
    {
        if (ops instanceof BufferedOutputStream)
        {
            return (BufferedOutputStream) ops;
        }
        return new BufferedOutputStream(ops);
    }

    /**
     * 根据一个文件路径建立一个输入流
     *
     * @param path 文件路径
     * @return 输入流
     */
    public static InputStream fileIn(final String path)
    {
        InputStream ins = PathUtil.findFileAsStream(path);
        if (null == ins)
        {
            File f = PathUtil.findFile(path);
            if (null != f)
            {
                try
                {
                    ins = new FileInputStream(f);
                } catch (FileNotFoundException e)
                {
                }
            }
        }
        return buff(ins);
    }

    /**
     * 根据一个文件路径建立一个输入流
     *
     * @param file 文件
     * @return 输入流
     */
    public static InputStream fileIn(final File file)
    {
        try
        {
            return buff(new FileInputStream(file));
        } catch (FileNotFoundException e)
        {
            throw MiscUtil.wrapThrow(e);
        }
    }

    /**
     * 根据一个文件路径建立一个 UTF-8文本输入流
     *
     * @param path 文件路径
     * @return 文本输入流
     */
    public static Reader fileInr(final String path)
    {
        return new InputStreamReader(fileIn(path), Encoding.CHARSET_UTF8);
    }

    /**
     * 根据一个文件路径建立一个 UTF-8 文本输入流
     *
     * @param file 文件
     * @return 文本输入流
     */
    public static Reader fileInr(final File file)
    {
        return new InputStreamReader(fileIn(file), Encoding.CHARSET_UTF8);
    }

    /**
     * 根据一个文件路径建立一个输出流
     *
     * @param path 文件路径
     * @return 输出流
     */
    public static OutputStream fileOut(final String path)
    {
        return fileOut(PathUtil.findFile(path));
    }

    /**
     * 根据一个文件建立一个输出流
     *
     * @param file 文件
     * @return 输出流
     */
    public static OutputStream fileOut(final File file)
    {
        try
        {
            return buff(new FileOutputStream(file));
        } catch (FileNotFoundException e)
        {
            throw MiscUtil.wrapThrow(e);
        }
    }

    /**
     * 根据一个文件路径建立一个 UTF-8 文本输出流
     *
     * @param path 文件路径
     * @return 文本输出流
     */
    public static Writer fileOutw(final String path)
    {
        return fileOutw(PathUtil.findFile(path));
    }

    /**
     * 根据一个文件建立一个 UTF-8 文本输出流
     *
     * @param file 文件
     * @return 输出流
     */
    public static Writer fileOutw(final File file)
    {
        return new OutputStreamWriter(fileOut(file), Encoding.CHARSET_UTF8);
    }
}
