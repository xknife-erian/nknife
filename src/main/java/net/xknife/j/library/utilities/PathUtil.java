package net.xknife.j.library.utilities;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URL;
import java.net.URLDecoder;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.LinkedList;
import java.util.List;
import java.util.StringTokenizer;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

import net.xknife.j.library.exception.AppException;
import net.xknife.j.library.exception.VTechnicalException;
import net.xknife.j.library.widgets.Encoding;
import net.xknife.j.library.widgets.OS;
import net.xknife.j.library.widgets.Sber;

import com.google.common.base.Preconditions;
import com.google.common.base.Strings;

/**
 * 文件与文件系统(文件与目录)的一些常用操作的助手类
 * 
 * @author lukan@jeelu.com
 */
public class PathUtil
{
	/**
	 * 通过将给定路径名字符串转换为抽象路径名来创建一个新 File实例。如果给定字符串是空字符串，那么结果是空抽象路径名。
	 * 
	 * @param pathName
	 *            路径名字符串
	 * @return 新 File实例
	 */
	public static File newFile(final String pathName)
	{
		return new File(pathName);
	}

	/**
	 * 判断父类文件和子类文件间是否存在包含关系
	 * 
	 * @param parent
	 *            父类文件
	 * @param child
	 *            子类文件
	 * @return 若子类文件是父类文件目录下的文件，返回true;否则返回false
	 * @throws IOException
	 */
	public static boolean contains(final File parent, final File child) throws IOException
	{
		String parentName = conventPath(parent);
		String childName = conventPath(child);
		return childName.indexOf(parentName) == 0;
	}

	public static String relativePath(final File parent, final File child) throws IOException
	{
		Preconditions.checkArgument(contains(parent, child));

		String parentName = conventPath(parent);
		String childName = conventPath(child);

		String relative = childName.substring(parentName.length());

		if (relative.length() == 0)
		{
			return "/";
		}

		return "/" + relative.substring(0, relative.length() - 1);
	}

	/**
	 * 规一化文件格式，结尾均以路径分隔符标识
	 * 
	 * @param file
	 *            待规一化文件
	 * @return 规一化后的文件名
	 * @throws IOException
	 */
	static String conventPath(final File file) throws IOException
	{
		String path = file.getCanonicalPath();
		return path.endsWith(File.separator) ? path : path + File.separator;
	}

	/**
	 * 对两个URL路径进行拼装
	 * 
	 * @param parentUrl
	 *            父URL路径,可为空字符串
	 * @param childUrl
	 *            子URL路径,可为空字符串
	 * @return 拼装后URL路径
	 * @throws AppException
	 */
	public static String combine(final String parentUrl, final String childUrl) throws AppException
	{
		StringBuilder simplyParent = simplyStringBuilder(parentUrl);

		if ((simplyParent.length() == 0) || ('/' != simplyParent.charAt(simplyParent.length() - 1)))
		{
			simplyParent.append('/');
		}

		String simplyChild = simplyWithoutPrefix(childUrl);

		return simplyParent.append(simplyChild).toString();
	}

	/**
	 * 判断URL路径是否合法，判断逻辑参考{@link #simply(String)},并去掉以'/'开头的斜杠前缀
	 * 
	 * @param url
	 *            待判断URL路径
	 * @return 去掉斜杠前缀的符合规范的URL路径字符串
	 * @throws AppException
	 * 
	 * @see #simply
	 */
	public static String simplyWithoutPrefix(final String url) throws AppException
	{
		StringBuilder simply = simplyStringBuilder(url);

		if ((simply.length() > 0) && ('/' == simply.charAt(0)))
		{
			simply.deleteCharAt(0);
		}

		return simply.toString();
	}

	/**
	 * 判断URL路径是否合法，判断逻辑参考{@link #simply(String)},并去掉以'/'结尾的斜杠后缀
	 * 
	 * @param url
	 *            待判断URL路径
	 * @return 去掉斜杠后缀的符合规范的URL路径字符串
	 * @throws AppException
	 * 
	 * @see #simply
	 */
	public static String simplyWithoutSuffix(final String url) throws AppException
	{
		StringBuilder simply = simplyStringBuilder(url);
		if ((simply.length() > 1) && ('/' == simply.charAt(simply.length() - 1)))
		{
			simply.deleteCharAt(simply.length() - 1);
		}

		return simply.toString();
	}

	/**
	 * 判断URL路径是否合法，并省略掉URL特殊字符后面的内容，同时进行规一划处理（将正反斜杠统一为正斜杠表示）
	 * 
	 * @param url
	 *            待判断URL路径
	 * @return 符合规范的URL路径字符串
	 * @throws AppException
	 */
	public static String simply(final String url) throws AppException
	{
		StringBuilder sb = simplyStringBuilder(url);
		return sb.toString();
	}

	private static StringBuilder simplyStringBuilder(final String url) throws VTechnicalException
	{
		StringBuilder sb = new StringBuilder(url.length());
		StringBuilder path = new StringBuilder();
		boolean illegalPath = true;
		for (int index = 0; index < url.length(); index++)
		{
			char c = url.charAt(index);

			switch (c)
			{
				case '/':
				case '\\': // url 中不包含 "\"
					if ((path.length() == 0) && (index == 0))
					{
						sb.append(path).append('/');
						continue;
					}

					if (illegalPath)
					{
						throw new VTechnicalException(url);
					}

					sb.append(path).append('/'); // File.pathSeparatorChar;

					path = new StringBuilder();
					illegalPath = true;
					break;

				case '.':
					path.append('.');
					break;

				default:

					if (isBreakChar(c))
					{
						index = Integer.MAX_VALUE - 100;
						break;
					}

					if (!isLegalChar(c))
					{
						throw new VTechnicalException(url);
					}

					illegalPath = false;
					path.append(c);
			}
		}

		if (illegalPath & (path.length() > 0))
		{
			throw new VTechnicalException(url);
		}
		else
		{
			sb.append(path);
		}
		return sb;
	}

	final static char[] legalChars = ("abcdefghijklmnopqrstuvwxyz" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "0123456789" + "`.~!@$%^&()-_=+[{]};', ").toCharArray();

	/**
	 * 针对URL路径中的字符，判断是否合法字符
	 * 
	 * @param c
	 *            待判断字符
	 * @return 若是非ASCII字符，直接认为是合法字符；若是ASCII字符，则排除不允许为文件命名的字符(如\/:*?"<>|)之外的所有字符都认为是合法字符；其他情况都返回false
	 */
	private static boolean isLegalChar(final char c)
	{
		if (c >= 128)
		{
			return true;
		}

		for (char legal : legalChars)
		{
			if (c == legal)
			{
				return true;
			}
		}

		return false;
	}

	/**
	 * 判断是否是URL路径中的特殊字符，现在主要考虑'?'和'#'
	 * 
	 * @param c
	 *            待判断字符
	 * @return 当待判断字符是'?'或者'#'时，返回true，否则返回false
	 */
	// char[] breakChars = new char[] {'?', '#'};
	private static boolean isBreakChar(final char c)
	{
		return (('?' == c) || ('#' == c));
	}

	/**
	 * 将两个文件对象比较，得出相对路径
	 * 
	 * @param base
	 *            基础文件对象
	 * @param file
	 *            相对文件对象
	 * @return 相对于基础文件对象的相对路径
	 */
	public static String getRelativePath(final File base, final File file)
	{
		return getRelativePath(base.getAbsolutePath(), file.getAbsolutePath());
	}

	/**
	 * 将两个路径比较，得出相对路径
	 * 
	 * @param base
	 *            基础路径
	 * @param path
	 *            相对文件路径
	 * @return 相对于基础路径对象的相对路径
	 */
	public static String getRelativePath(final String base, final String path)
	{
		String[] bb = StringHelper.splitIgnoreBlank(getCanonicalPath(base), "[\\\\/]");
		String[] ff = StringHelper.splitIgnoreBlank(getCanonicalPath(path), "[\\\\/]");
		int len = Math.min(bb.length, ff.length);
		int pos = 0;
		for (; pos < len; pos++)
		{
			if (!bb[pos].equals(ff[pos]))
			{
				break;
			}
		}
		StringBuilder sb = new StringBuilder(StringHelper.dup("../", bb.length - 1 - pos));
		return sb.append(Sber.concat(pos, ff.length - pos, '/', ff)).toString();
	}

	/**
	 * 整理路径。 将会合并路径中的 ".."
	 * 
	 * @param path
	 *            路径
	 * @return 整理后的路径
	 */
	public static String getCanonicalPath(final String path)
	{
		String[] pa = StringHelper.splitIgnoreBlank(path, "[\\\\/]");
		LinkedList<String> paths = new LinkedList<String>();
		for (String s : pa)
		{
			if ("..".equals(s))
			{
				if (paths.size() > 0)
				{
					paths.removeLast();
				}
				continue;
			}
			else
			{
				paths.add(s);
			}
		}
		return Sber.concat("/", paths).toString();
	}

	/**
	 * @return 当前账户的主目录全路径
	 */
	public static String home()
	{
		return System.getProperty("user.home");
	}

	/**
	 * @param path
	 *            相对用户主目录的路径
	 * @return 相对用户主目录的全路径
	 */
	public static String home(final String path)
	{
		return home() + path;
	}

	/**
	 * 获取一个路径的绝对路径
	 * 
	 * @param path
	 *            路径
	 * @return 绝对路径
	 */
	public static String absolute(final String path)
	{
		return absolute(path, PathUtil.class.getClassLoader(), Encoding.defaultEncoding());
	}

	/**
	 * 获取一个路径的绝对路径
	 * 
	 * @param path
	 *            路径
	 * @param klassLoader
	 *            参考 ClassLoader
	 * @param enc
	 *            路径编码方式
	 * @return 绝对路径
	 */
	public static String absolute(String path, final ClassLoader klassLoader, final String enc)
	{
		path = normalize(path, enc);
		if (Strings.isNullOrEmpty(path))
		{
			return null;
		}

		File f = new File(path);
		if (!f.exists())
		{
			URL url = klassLoader.getResource(path);
			if (null == url)
			{
				url = Thread.currentThread().getContextClassLoader().getResource(path);
			}
			if (null == url)
			{
				url = ClassLoader.getSystemResource(path);
			}
			if (null != url)
			{
				return normalize(url.getPath(), Encoding.UTF8);// 通过URL获取String,一律使用UTF-8编码进行解码
			}
			return null;
		}
		return path;
	}

	/**
	 * 让路径变成正常路径，将 ~ 替换成用户主目录
	 * 
	 * @param path
	 *            路径
	 * @return 正常化后的路径
	 */
	public static String normalize(final String path)
	{
		return normalize(path, Encoding.defaultEncoding());
	}

	/**
	 * 让路径变成正常路径，将 ~ 替换成用户主目录
	 * 
	 * @param path
	 *            路径
	 * @param enc
	 *            路径编码方式
	 * @return 正常化后的路径
	 */
	public static String normalize(String path, final String enc)
	{
		if (Strings.isNullOrEmpty(path))
		{
			return null;
		}
		if (path.charAt(0) == '~')
		{
			path = home() + path.substring(1);
		}
		try
		{
			return URLDecoder.decode(path, enc);
		}
		catch (UnsupportedEncodingException e)
		{
			return null;
		}

	}

	/**
	 * 读取文本文件内容
	 * 
	 * @param filePathAndName
	 *            带有完整绝对路径的文件名
	 * @param encoding
	 *            文本文件打开的编码方式
	 * @return 返回文本文件的内容
	 */
	public static String readText(final String filePathAndName, String encoding) throws IOException
	{
		encoding = encoding.trim();
		StringBuffer str = new StringBuffer("");
		String st = "";
		try
		{
			FileInputStream fs = new FileInputStream(filePathAndName);
			InputStreamReader isr;
			if (encoding.equals(""))
			{
				isr = new InputStreamReader(fs);
			}
			else
			{
				isr = new InputStreamReader(fs, encoding);
			}
			@SuppressWarnings("resource")
			BufferedReader br = new BufferedReader(isr);
			try
			{
				String data = "";
				while ((data = br.readLine()) != null)
				{
					str.append(data + " ");
				}
			}
			catch (Exception e)
			{
				str.append(e.toString());
			}
			st = str.toString();
		}
		catch (IOException es)
		{
			st = "";
		}
		return st;
	}

	/**
	 * 新建目录，当该目录中的最子级的父级目录不存在时建议使用createFolders更安全一些。
	 * 
	 * @param folderPath
	 *            目录
	 * @return 返回目录创建后的路径
	 */
	public static String createFolder(final String folderPath)
	{
		String path = folderPath;

		java.io.File myFilePath = new java.io.File(path);
		path = folderPath;
		if (!myFilePath.exists())
		{
			myFilePath.mkdir();
		}

		return path;
	}

	/**
	 * 多级目录创建
	 * 
	 * @param rootPath
	 *            准备要在本级目录下创建新目录的目录路径 例如 c:myf
	 * @param paths
	 *            无限级目录参数，各级目录以单竖线区分 例如 a|b|c
	 * @return 返回创建文件后的路径
	 */
	public static String createFolders(final String rootPath, final String paths)
	{
		String root = rootPath;

		String subPath;
		root = rootPath;
		StringTokenizer st = new StringTokenizer(paths, "|");
		for (; st.hasMoreTokens();)
		{
			subPath = st.nextToken().trim();
			if (root.lastIndexOf(OS.file_separator()) != -1)
			{
				root = createFolder(root + OS.file_separator() + subPath);
			}
			else
			{
				root = createFolder(root + OS.file_separator() + subPath + OS.file_separator());
			}
		}
		return root;
	}

	/**
	 * 多级目录创建
	 * 
	 * @param rootPath
	 *            准备要在本级目录下创建新目录的目录路径 例如 c:myf
	 * @param paths
	 *            无限级目录的子级目录字符串的数组
	 * @return 返回创建文件后的路径
	 */
	public static String createFolders(final String rootPath, final String[] paths)
	{
		String root = rootPath;

		String subPath;
		root = rootPath;
		for (String element : paths)
		{
			subPath = element.trim();
			if (root.lastIndexOf(OS.file_separator()) != -1)
			{
				root = createFolder(root + OS.file_separator() + subPath);
			}
			else
			{
				root = createFolder(root + OS.file_separator() + subPath + OS.file_separator());
			}
		}
		return root;
	}

	/**
	 * 新建文本文件
	 * 
	 * @param filePathAndName
	 *            文本文件完整绝对路径及文件名
	 * @param fileContent
	 *            文本文件内容
	 * @return
	 * @throws IOException
	 */
	public static void createFile(final String filePathAndName, final String fileContent) throws IOException
	{
		String filePath = filePathAndName;
		filePath = filePath.toString();
		File myFilePath = new File(filePath);
		if (!myFilePath.exists())
		{
			myFilePath.createNewFile();
		}
		FileWriter resultFile = new FileWriter(myFilePath);
		PrintWriter myFile = new PrintWriter(resultFile);
		String strContent = fileContent;
		myFile.println(strContent);
		myFile.close();
		resultFile.close();
	}

	/**
	 * 有编码方式的文本文件创建
	 * 
	 * @param filePathAndName
	 *            文本文件完整绝对路径及文件名
	 * @param fileContent
	 *            文本文件内容
	 * @param encoding
	 *            编码方式 例如 GBK 或者 UTF-8
	 * @return
	 * @throws IOException
	 */
	public static void createFile(final String filePathAndName, final String fileContent, final String encoding) throws IOException
	{
		String filePath = filePathAndName;
		filePath = filePath.toString();
		File myFilePath = new File(filePath);
		if (!myFilePath.exists())
		{
			myFilePath.createNewFile();
		}
		PrintWriter myFile = new PrintWriter(myFilePath, encoding);
		String strContent = fileContent;
		myFile.println(strContent);
		myFile.close();
	}

	/**
	 * 删除文件
	 * 
	 * @param filePathAndName
	 *            文本文件完整绝对路径及文件名
	 * @return Boolean 成功删除返回true遭遇异常返回false
	 */
	public static boolean delFile(final String filePathAndName)
	{
		boolean bea = false;

		String filePath = filePathAndName;
		File myDelFile = new File(filePath);
		if (myDelFile.exists())
		{
			myDelFile.delete();
			bea = true;
		}
		else
		{
			bea = false;
		}
		return bea;
	}

	/**
	 * 删除文件夹
	 * 
	 * @param folderPath
	 *            文件夹完整绝对路径
	 * @return
	 */
	public static void delFolder(final String folderPath)
	{
		delAllFile(folderPath); // 删除完里面所有内容
		String filePath = folderPath;
		filePath = filePath.toString();
		java.io.File myFilePath = new java.io.File(filePath);
		myFilePath.delete(); // 删除空文件夹
	}

	/**
	 * 删除指定文件夹下所有文件
	 * 
	 * @param path
	 *            文件夹完整绝对路径
	 * @return
	 * @return
	 */
	public static boolean delAllFile(final String path)
	{
		boolean bea = false;
		File file = new File(path);
		if (!file.exists())
		{
			return bea;
		}
		if (!file.isDirectory())
		{
			return bea;
		}
		String[] tempList = file.list();
		File temp = null;
		for (String element : tempList)
		{
			if (path.endsWith(File.separator))
			{
				temp = new File(path + element);
			}
			else
			{
				temp = new File(path + File.separator + element);
			}
			if (temp.isFile())
			{
				temp.delete();
			}
			if (temp.isDirectory())
			{
				delAllFile(path + "/" + element);// 先删除文件夹里面的文件
				delFolder(path + "/" + element);// 再删除空文件夹
				bea = true;
			}
		}
		return bea;
	}

	/**
	 * 复制单个文件
	 * 
	 * @param oldPathFile
	 *            准备复制的文件源
	 * @param newPathFile
	 *            拷贝到新绝对路径带文件名
	 * @return
	 * @throws IOException
	 */
	public static void copyFile(final String oldPathFile, final String newPathFile) throws IOException
	{
		int byteread = 0;
		File oldfile = new File(oldPathFile);
		if (oldfile.exists())
		{ // 文件存在时
			InputStream inStream = new FileInputStream(oldPathFile); // 读入原文件
			@SuppressWarnings("resource")
			FileOutputStream fs = new FileOutputStream(newPathFile);
			byte[] buffer = new byte[1444];
			while ((byteread = inStream.read(buffer)) != -1)
			{
				fs.write(buffer, 0, byteread);
			}
			inStream.close();
		}
	}

	/**
	 * 复制整个文件夹的内容
	 * 
	 * @param oldPath
	 *            准备拷贝的目录
	 * @param newPath
	 *            指定绝对路径的新目录
	 * @return
	 * @throws IOException
	 */
	public static void copyFolder(final String oldPath, final String newPath) throws IOException
	{
		new File(newPath).mkdirs(); // 如果文件夹不存在 则建立新文件夹
		File a = new File(oldPath);
		String[] file = a.list();
		File temp = null;
		for (String element : file)
		{
			if (oldPath.endsWith(File.separator))
			{
				temp = new File(oldPath + element);
			}
			else
			{
				temp = new File(oldPath + File.separator + element);
			}
			if (temp.isFile())
			{
				FileInputStream input = new FileInputStream(temp);
				FileOutputStream output = new FileOutputStream(newPath + "/" + (temp.getName()).toString());
				byte[] b = new byte[1024 * 5];
				int len;
				while ((len = input.read(b)) != -1)
				{
					output.write(b, 0, len);
				}
				output.flush();
				output.close();
				input.close();
			}
			if (temp.isDirectory())
			{
				copyFolder(oldPath + "/" + element, newPath + "/" + element);
			}
		}
	}

	/**
	 * 移动文件
	 * 
	 * @param oldPath
	 * @param newPath
	 * @return
	 * @throws IOException
	 */
	public static void moveFile(final String oldPath, final String newPath) throws IOException
	{
		copyFile(oldPath, newPath);
		delFile(oldPath);
	}

	/**
	 * 移动目录
	 * 
	 * @param oldPath
	 * @param newPath
	 * @return
	 * @throws IOException
	 */
	public static void moveFolder(final String oldPath, final String newPath) throws IOException
	{
		copyFolder(oldPath, newPath);
		delFolder(oldPath);
	}

	// ================================

	/**
	 * 将数据写入文件,成功就返回true,失败就返回false
	 * 
	 * @param file
	 *            需要写入的文件
	 * @param data
	 *            需要写入的数据
	 * @return true 如果写入成功
	 */
	public static boolean write(final File file, final byte data[])
	{
		try
		{
			FileOutputStream fos = new FileOutputStream(file);
			fos.write(data);
			fos.flush();
			fos.close();
		}
		catch (Throwable e)
		{
			return false;
		}
		return true;
	}

	/**
	 * 将文件后缀改名，从而生成一个新的文件对象。但是并不在磁盘上创建它
	 * 
	 * @param f
	 *            文件
	 * @param suffix
	 *            新后缀
	 * @return 新文件对象
	 */
	public static File renameSuffix(final File f, final String suffix)
	{
		if ((null == f) || (null == suffix) || (suffix.length() == 0))
		{
			return f;
		}
		return new File(renameSuffix(f.getAbsolutePath(), suffix));
	}

	/**
	 * 将文件路径后缀改名，从而生成一个新的文件路径。
	 * 
	 * @param path
	 *            文件路径
	 * @param suffix
	 *            新后缀
	 * @return 新文件后缀
	 */
	public static String renameSuffix(final String path, final String suffix)
	{
		int pos = path.length();
		for (--pos; pos > 0; pos--)
		{
			if (path.charAt(pos) == '.')
			{
				break;
			}
			if ((path.charAt(pos) == '/') || (path.charAt(pos) == '\\'))
			{
				pos = -1;
				break;
			}
		}
		if (0 >= pos)
		{
			return path + suffix;
		}
		return path.substring(0, pos) + suffix;
	}

	/**
	 * 获取文件主名。 即去掉后缀的名称
	 * 
	 * @param path
	 *            文件路径
	 * @return 文件主名
	 */
	public static String getMajorName(final String path)
	{
		int len = path.length();
		int l = 0;
		int r = len;
		for (int i = r - 1; i > 0; i--)
		{
			if (r == len)
			{
				if (path.charAt(i) == '.')
				{
					r = i;
				}
			}
			if ((path.charAt(i) == '/') || (path.charAt(i) == '\\'))
			{
				l = i + 1;
				break;
			}
		}
		return path.substring(l, r);
	}

	/**
	 * 获取文件主名。 即去掉后缀的名称
	 * 
	 * @param f
	 *            文件
	 * @return 文件主名
	 */
	public static String getMajorName(final File f)
	{
		return getMajorName(f.getAbsolutePath());
	}

	/**
	 * 获取文件后缀名，不包括 '.'，如 'abc.gif','，则返回 'gif'
	 * 
	 * @param f
	 *            文件
	 * @return 文件后缀名
	 */
	public static String getSuffixName(final File f)
	{
		if (null == f)
		{
			return null;
		}
		return getSuffixName(f.getAbsolutePath());
	}

	/**
	 * 获取文件后缀名，不包括 '.'，如 'abc.gif','，则返回 'gif'
	 * 
	 * @param path
	 *            文件路径
	 * @return 文件后缀名
	 */
	public static String getSuffixName(final String path)
	{
		if (null == path)
		{
			return null;
		}
		int pos = path.lastIndexOf('.');
		if (-1 == pos)
		{
			return "";
		}
		return path.substring(pos + 1);
	}

	/**
	 * 根据正则式，从压缩文件中获取文件
	 * 
	 * @param zip
	 *            压缩文件
	 * @param regex
	 *            正则式，用来匹配文件名
	 * @return 数组
	 */
	public static ZipEntry[] findEntryInZip(final ZipFile zip, final String regex)
	{
		List<ZipEntry> list = new LinkedList<ZipEntry>();
		Enumeration<? extends ZipEntry> en = zip.entries();
		while (en.hasMoreElements())
		{
			ZipEntry ze = en.nextElement();
			if ((null == regex) || ze.getName().matches(regex))
			{
				list.add(ze);
			}
		}
		return list.toArray(new ZipEntry[list.size()]);
	}

	/**
	 * 试图生成一个文件对象，如果文件不存在则创建它。 如果给出的 PATH 是相对路径 则会在 CLASSPATH 中寻找，如果未找到，则会在用户主目录中创建这个文件
	 * 
	 * @param path
	 *            文件路径，可以以 ~ 开头，也可以是 CLASSPATH 下面的路径
	 * @return 文件对象
	 * @throws IOException
	 *             创建失败
	 */
	public static File createFileIfNoExists(final String path) throws IOException, VTechnicalException
	{
		String thePath = absolute(path);
		if (null == thePath)
		{
			thePath = normalize(path);
		}
		File f = new File(thePath);
		if (!f.exists())
		{
			createNewFile(f);
		}
		if (!f.isFile())
		{
			throw new IOException(String.format("'%s' should be a file!", path));
		}
		return f;
	}

	/**
	 * 试图生成一个目录对象，如果文件不存在则创建它。 如果给出的 PATH 是相对路径 则会在 CLASSPATH 中寻找，如果未找到，则会在用户主目录中创建这个目录
	 * 
	 * @param path
	 *            文件路径，可以以 ~ 开头，也可以是 CLASSPATH 下面的路径
	 * @return 文件对象
	 * @throws IOException
	 *             创建失败
	 */
	public static File createDirIfNoExists(final String path) throws IOException
	{
		String thePath = absolute(path);
		if (null == thePath)
		{
			thePath = normalize(path);
		}
		File f = new File(thePath);
		if (!f.exists())
		{
			makeDir(f);
		}
		if (!f.isDirectory())
		{
			throw new IOException(String.format("'%s' should be a file!", path));
		}
		return f;
	}

	/**
	 * 从 CLASSPATH 下寻找一个文件
	 * 
	 * @param path
	 *            文件路径
	 * @param klassLoader
	 *            参考 ClassLoader
	 * @param enc
	 *            文件路径编码
	 * @return 文件对象，如果不存在，则为 null
	 */
	public static File findFile(String path, final ClassLoader klassLoader, final String enc)
	{
		path = absolute(path, klassLoader, enc);
		if (null == path)
		{
			return null;
		}
		return new File(path);
	}

	/**
	 * 从 CLASSPATH 下寻找一个文件
	 * 
	 * @param path
	 *            文件路径
	 * @param enc
	 *            文件路径编码
	 * @return 文件对象，如果不存在，则为 null
	 */
	public static File findFile(final String path, final String enc)
	{
		return findFile(path, PathUtil.class.getClassLoader(), enc);
	}

	/**
	 * 从 CLASSPATH 下寻找一个文件
	 * 
	 * @param path
	 *            文件路径
	 * @param klassLoader
	 *            使用该 ClassLoader进行查找
	 * @return 文件对象，如果不存在，则为 null
	 */
	public static File findFile(final String path, final ClassLoader klassLoader)
	{
		return findFile(path, klassLoader, Encoding.defaultEncoding());
	}

	/**
	 * 从 CLASSPATH 下寻找一个文件
	 * 
	 * @param path
	 *            文件路径
	 * @return 文件对象，如果不存在，则为 null
	 */
	public static File findFile(final String path)
	{
		return findFile(path, PathUtil.class.getClassLoader(), Encoding.defaultEncoding());
	}

	/**
	 * 获取输出流
	 * 
	 * @param path
	 *            文件路径
	 * @param klass
	 *            参考的类， -- 会用这个类的 ClassLoader
	 * @param enc
	 *            文件路径编码
	 * @return 输出流
	 */
	public static InputStream findFileAsStream(final String path, final Class<?> klass, final String enc)
	{
		File f = new File(path);
		if (f.exists())
		{
			try
			{
				return new FileInputStream(f);
			}
			catch (FileNotFoundException e1)
			{
				return null;
			}
		}
		if (null != klass)
		{
			InputStream ins = klass.getClassLoader().getResourceAsStream(path);
			if (null == ins)
			{
				ins = Thread.currentThread().getContextClassLoader().getResourceAsStream(path);
			}
			if (null != ins)
			{
				return ins;
			}
		}
		return ClassLoader.getSystemResourceAsStream(path);
	}

	/**
	 * 获取输出流
	 * 
	 * @param path
	 *            文件路径
	 * @param enc
	 *            文件路径编码
	 * @return 输出流
	 */
	public static InputStream findFileAsStream(final String path, final String enc)
	{
		return findFileAsStream(path, PathUtil.class, enc);
	}

	/**
	 * 获取输出流
	 * 
	 * @param path
	 *            文件路径
	 * @param klass
	 *            参考的类， -- 会用这个类的 ClassLoader
	 * @return 输出流
	 */
	public static InputStream findFileAsStream(final String path, final Class<?> klass)
	{
		return findFileAsStream(path, klass, Encoding.defaultEncoding());
	}

	/**
	 * 获取输出流
	 * 
	 * @param path
	 *            文件路径
	 * @return 输出流
	 */
	public static InputStream findFileAsStream(final String path)
	{
		return findFileAsStream(path, PathUtil.class, Encoding.defaultEncoding());
	}

	/**
	 * 文件对象是否是目录，可接受 null
	 */
	public static boolean isDirectory(final File f)
	{
		if (null == f)
		{
			return false;
		}
		if (!f.exists())
		{
			return false;
		}
		if (!f.isDirectory())
		{
			return false;
		}
		return true;
	}

	/**
	 * 文件对象是否是文件(包括文件是否存在的判断)，可接受 null
	 */
	public static boolean isFile(final File f)
	{
		return (null != f) && f.exists() && f.isFile();
	}

	/**
	 * 创建新文件，如果父目录不存在，也一并创建。可接受 null 参数
	 * 
	 * @param f
	 *            文件对象
	 * @return false:当文件已存在; true:创建成功
	 * @throws IOException
	 */
	public static boolean createNewFile(final File f) throws IOException
	{
		if ((null == f) || f.exists())
		{
			return false;
		}
		makeDir(f.getParentFile());
		return f.createNewFile();
	}

	/**
	 * 创建新目录，如果父目录不存在，也一并创建。可接受 null 参数
	 * 
	 * @param dir
	 *            目录对象
	 * @return false，如果目录已存在。 true 创建成功
	 * @throws IOException
	 */
	public static boolean makeDir(final File dir)
	{
		if ((null == dir) || dir.exists())
		{
			return false;
		}
		return dir.mkdirs();
	}

	/**
	 * 强行删除一个目录，包括这个目录下所有的子目录和文件
	 * 
	 * @param dir
	 *            目录
	 * @return 是否删除成功
	 * @throws IOException
	 */
	public static boolean deleteDir(final File dir) throws IOException
	{
		if ((null == dir) || !dir.exists())
		{
			return false;
		}
		File[] files = dir.listFiles();
		boolean re = false;
		if (null != files)
		{
			if (files.length == 0)
			{
				return dir.delete();
			}
			for (File f : files)
			{
				if (f.isDirectory())
				{
					re |= deleteDir(f);
				}
				else
				{
					re |= deleteFile(f);
				}
			}
			re |= dir.delete();
		}
		return re;
	}

	/**
	 * 删除一个文件
	 * 
	 * @param f
	 *            文件
	 * @return 是否删除成功
	 * @throws IOException
	 */
	public static boolean deleteFile(final File f)
	{
		if (null == f)
		{
			return false;
		}
		return f.delete();
	}

	/**
	 * 清除一个目录里所有的内容
	 * 
	 * @param dir
	 *            目录
	 * @return 是否清除成功
	 * @throws IOException
	 */
	public static boolean clearDir(final File dir) throws IOException
	{
		if (null == dir)
		{
			return false;
		}
		if (!dir.exists())
		{
			return false;
		}
		File[] fs = dir.listFiles();
		for (File f : fs)
		{
			if (f.isFile())
			{
				deleteFile(f);
			}
			else if (f.isDirectory())
			{
				deleteDir(f);
			}
		}
		return false;
	}

	/**
	 * 拷贝一个文件
	 * 
	 * @param src
	 *            原始文件
	 * @param target
	 *            新文件
	 * @return 是否拷贝成功
	 * @throws IOException
	 */
	public static boolean copyFile(final File src, final File target) throws IOException
	{
		if ((src == null) || (target == null) || !src.exists())
		{
			return false;
		}
		if (!target.exists())
		{
			if (!createNewFile(target))
			{
				return false;
			}
		}
		InputStream ins = new BufferedInputStream(new FileInputStream(src));
		OutputStream ops = new BufferedOutputStream(new FileOutputStream(target));
		int b;
		while (-1 != (b = ins.read()))
		{
			ops.write(b);
		}

		StreamUtil.safeClose(ins);
		StreamUtil.safeClose(ops);
		return target.setLastModified(src.lastModified());
	}

	/**
	 * 拷贝一个目录
	 * 
	 * @param src
	 *            原始目录
	 * @param target
	 *            新目录
	 * @return 是否拷贝成功
	 * @throws IOException
	 */
	public static boolean copyDir(final File src, final File target) throws IOException
	{
		if ((src == null) || (target == null) || !src.exists())
		{
			return false;
		}
		if (!src.isDirectory())
		{
			throw new IOException(src.getAbsolutePath() + " should be a directory!");
		}
		if (!target.exists())
		{
			if (!makeDir(target))
			{
				return false;
			}
		}
		boolean re = true;
		File[] files = src.listFiles();
		if (null != files)
		{
			for (File f : files)
			{
				if (f.isFile())
				{
					re &= copyFile(f, new File(target.getAbsolutePath() + "/" + f.getName()));
				}
				else
				{
					re &= copyDir(f, new File(target.getAbsolutePath() + "/" + f.getName()));
				}
			}
		}
		return re;
	}

	/**
	 * 将文件移动到新的位置
	 * 
	 * @param src
	 *            原始文件
	 * @param target
	 *            新文件
	 * @return 移动是否成功
	 * @throws IOException
	 */
	public static boolean move(final File src, final File target) throws IOException
	{
		if ((src == null) || (target == null))
		{
			return false;
		}
		makeDir(target.getParentFile());
		return src.renameTo(target);
	}

	/**
	 * 将文件改名
	 * 
	 * @param src
	 *            文件
	 * @param newName
	 *            新名称
	 * @return 改名是否成功
	 */
	public static boolean rename(final File src, final String newName)
	{
		if ((src == null) || (newName == null))
		{
			return false;
		}
		if (src.exists())
		{
			File newFile = new File(src.getParent() + "/" + newName);
			if (newFile.exists())
			{
				return false;
			}
			makeDir(newFile.getParentFile());
			return src.renameTo(newFile);
		}
		return false;
	}

	/**
	 * 修改路径
	 * 
	 * @param path
	 *            路径
	 * @param newName
	 *            新名称
	 * @return 新路径
	 */
	public static String renamePath(final String path, final String newName)
	{
		if (!Strings.isNullOrEmpty(path))
		{
			int pos = path.replace('\\', '/').lastIndexOf('/');
			if (pos > 0)
			{
				return path.substring(0, pos) + "/" + newName;
			}
		}
		return newName;
	}

	/**
	 * @param path
	 *            路径
	 * @return 父路径
	 */
	public static String getParent(final String path)
	{
		if (Strings.isNullOrEmpty(path))
		{
			return path;
		}
		int pos = path.replace('\\', '/').lastIndexOf('/');
		if (pos > 0)
		{
			return path.substring(0, pos);
		}
		return "/";
	}

	/**
	 * @param path
	 *            全路径
	 * @return 文件或者目录名
	 */
	public static String getName(final String path)
	{
		if (!Strings.isNullOrEmpty(path))
		{
			int pos = path.replace('\\', '/').lastIndexOf('/');
			if (pos > 0)
			{
				return path.substring(pos);
			}
		}
		return path;
	}

	/**
	 * 将一个目录下的特殊名称的目录彻底删除，比如 '.svn' 或者 '.cvs'
	 * 
	 * @param dir
	 *            目录
	 * @param name
	 *            要清除的目录名
	 * @throws IOException
	 */
	public static void cleanAllFolderInSubFolderes(final File dir, final String name) throws IOException
	{
		File[] files = dir.listFiles();
		for (File d : files)
		{
			if (d.isDirectory())
			{
				if (d.getName().equalsIgnoreCase(name))
				{
					deleteDir(d);
				}
				else
				{
					cleanAllFolderInSubFolderes(d, name);
				}
			}
		}
	}

	/**
	 * 精确比较两个文件是否相等
	 * 
	 * @param f1
	 *            文件1
	 * @param f2
	 *            文件2
	 * @return 是否相等
	 */
	public static boolean isEquals(final File f1, final File f2)
	{
		if (!f1.isFile() || !f2.isFile())
		{
			return false;
		}
		InputStream ins1 = null;
		InputStream ins2 = null;
		try
		{
			ins1 = new BufferedInputStream(new FileInputStream(f1));
			ins2 = new BufferedInputStream(new FileInputStream(f2));
			return StreamUtil.equals(ins1, ins2);
		}
		catch (Exception e)
		{
			return false;
		}
		finally
		{
			StreamUtil.safeClose(ins1);
			StreamUtil.safeClose(ins2);
		}
	}

	/**
	 * 在一个目录下，获取一个文件对象
	 * 
	 * @param dir
	 *            目录
	 * @param path
	 *            文件相对路径
	 * @return 文件
	 */
	public static File getFile(final File dir, final String path)
	{
		if (dir.exists())
		{
			if (dir.isDirectory())
			{
				return new File(dir.getAbsolutePath() + "/" + path);
			}
			return new File(dir.getParent() + "/" + path);
		}
		return new File(path);
	}

	/**
	 * 获取一个目录下所有子目录。子目录如果以 '.' 开头，将被忽略
	 * 
	 * @param dir
	 *            目录
	 * @return 子目录数组
	 */
	public static File[] dirs(final File dir)
	{
		return dir.listFiles(new FileFilter()
		{

			public boolean accept(final File f)
			{
				return !f.isHidden() && f.isDirectory() && !f.getName().startsWith(".");
			}
		});
	}

	/**
	 * 递归查找获取一个目录下所有子目录(及子目录的子目录)。子目录如果以 '.' 开头，将被忽略
	 * <p/>
	 * <b>包含传入的目录</b>
	 * 
	 * @param dir
	 *            目录
	 * @return 子目录数组
	 */
	public static File[] scanDirs(final File dir)
	{
		ArrayList<File> list = new ArrayList<File>();
		list.add(dir);
		scanDirs(dir, list);
		return list.toArray(new File[list.size()]);
	}

	private static void scanDirs(final File rootDir, final List<File> list)
	{
		File[] dirs = rootDir.listFiles(new FileFilter()
		{

			public boolean accept(final File f)
			{
				return !f.isHidden() && f.isDirectory() && !f.getName().startsWith(".");
			}
		});
		if (dirs != null)
		{
			for (File dir : dirs)
			{
				scanDirs(dir, list);
				list.add(dir);
			}
		}
	}

	/**
	 * 获取一个目录下所有的文件。隐藏文件会被忽略。
	 * 
	 * @param dir
	 *            目录
	 * @param suffix
	 *            文件后缀名。如果为 null，则获取全部文件
	 * @return 文件数组
	 */
	public static File[] files(final File dir, final String suffix)
	{
		return dir.listFiles(new FileFilter()
		{
			public boolean accept(final File f)
			{
				return !f.isHidden() && f.isFile() && ((null == suffix) || f.getName().endsWith(suffix));
			}
		});
	}

	/**
	 * 判断两个文件内容是否相等
	 * 
	 * @param f1
	 *            文件对象
	 * @param f2
	 *            文件对象
	 * @return <ul>
	 *         <li>true: 两个文件内容完全相等
	 *         <li>false: 任何一个文件对象为 null，不存在 或内容不相等
	 *         </ul>
	 */
	public static boolean equals(final File f1, final File f2)
	{
		if ((null == f1) || (null == f2))
		{
			return false;
		}
		InputStream ins1, ins2;
		ins1 = StreamUtil.fileIn(f1);
		ins2 = StreamUtil.fileIn(f2);
		if ((null == ins1) || (null == ins2))
		{
			return false;
		}

		try
		{
			return StreamUtil.equals(ins1, ins2);
		}
		catch (IOException e)
		{
			throw MiscUtil.wrapThrow(e);
		}
		finally
		{
			StreamUtil.safeClose(ins1);
			StreamUtil.safeClose(ins2);
		}

	}

}
