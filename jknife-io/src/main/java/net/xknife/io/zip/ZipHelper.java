package net.xknife.io.zip;

import com.google.common.collect.Lists;
import net.xknife.lang.utilities.PathUtil;
import net.xknife.lang.utilities.StringHelper;
import net.xknife.lang.widgets.OS;

import java.io.*;
import java.util.List;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

public class ZipHelper
{
	static final int BUFFER = 1024;

	/**
	 * 解压一个指定的Zip压缩包
	 * 
	 * @param unzipFile 等解压文件
	 * @param resultRootPath 解压后的所有文件存储的根路径
	 * @return
	 * @throws java.io.IOException
	 */
	public static File[] unZip(File unzipFile, String resultRootPath) throws IOException
	{
		if (!unzipFile.exists())
			throw new FileNotFoundException(unzipFile.getPath());
		if (OS.isWindows() && (resultRootPath.charAt(0) == '/'))
			resultRootPath = resultRootPath.substring(1);
		PathUtil.createDirIfNoExists(resultRootPath);

		List<File> files = Lists.newArrayList();
		FileInputStream fileInputStream = new FileInputStream(unzipFile);
		ZipInputStream zipStream = new ZipInputStream(new BufferedInputStream(fileInputStream));
		ZipEntry entry;
		while ((entry = zipStream.getNextEntry()) != null)
		{
			int count;
			byte data[] = new byte[BUFFER];

			if (entry.isDirectory())
				continue;

			String abs = entry.getName();
			File unDirPath = getRealFileName(resultRootPath, abs);
            PathUtil.createDirIfNoExists(unDirPath.getParent());

			BufferedOutputStream dest = new BufferedOutputStream(new FileOutputStream(unDirPath));

			while ((count = zipStream.read(data, 0, BUFFER)) != -1)
				dest.write(data, 0, count);

			dest.flush();
			dest.close();
			files.add(unDirPath);
		}
		zipStream.close();
		return files.toArray(new File[files.size()]);
	}

	/**
	 * 给定根目录，返回一个相对路径所对应的实际文件名.
	 * 
	 * @param resultRootPath 指定根目录
	 * @param absFileName 相对路径名，来自于ZipEntry中的name
	 * @return java.io.File 实际的文件
	 */
	private static File getRealFileName(String resultRootPath, String absFileName)
	{
		int n = absFileName.indexOf('/');
		String c = "\\";
		if (!(n < 0))
			c = "/";

		String[] dirs = StringHelper.splitIgnoreBlank(absFileName, c);

		File ret = new File(resultRootPath);// 创建文件对象

		if (dirs.length > 1)
			for (int i = 0; i < dirs.length - 1; i++)
				ret = new File(ret, dirs[i]);

		if (!ret.exists())
			ret.mkdirs();// 创建此抽象路径名指定的目录
		// 根据 ret 抽象路径名和 child 路径名字符串创建一个新 File 实例
		ret = new File(ret, dirs[dirs.length - 1]);

		return ret;
	}

    public static void main(String[] args)
    {
        try
        {
            String resultPath = "C:\\data";// /解压到的目标文件路径
            String zipFile = "C:\\data\\vip\\config.zip";// 要解压的压缩文件的存放路径

            File file = new File(zipFile);

            String realname = file.getName();
            System.out.println(realname);
            System.out.println("要解压缩的文件名...\t" + zipFile);
            System.out.println("解压到的目录...\t\t" + resultPath);

            File[] files = unZip(file, resultPath);
            for (File file2 : files)
                System.out.println(file2);
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

}
