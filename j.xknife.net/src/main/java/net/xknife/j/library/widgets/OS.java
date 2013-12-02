package net.xknife.j.library.widgets;

import java.util.Properties;

import com.google.common.base.Strings;

/**
 * 对当前操作系统环境的一些参数的封装。
 * 
 * @author lukan@jeelu.com
 * 
 */
public class OS
{
	private static Properties systemProperties = System.getProperties(); // 获得系统属性集

	/**
	 * 当前操作系统是否是Windows操作系统，是返回True，否则反之。
	 * 
	 * @return
	 */
	public static boolean isWindows()
	{
		return os_name().contains("Window");
	}

	/** Java 运行时环境版本 */
	public static String java_version()
	{
		return systemProperties.getProperty("java.version");
	}

	/** Java 运行时环境供应商 */
	public static String java_vendor()
	{
		return systemProperties.getProperty("java.vendor");
	}

	/** Java 供应商的 URL */
	public static String java_vendor_url()
	{
		return systemProperties.getProperty("java.vendor.url");
	}

	/** Java 安装目录 */
	public static String java_home()
	{
		return systemProperties.getProperty("java.home");
	}

	/** Java 虚拟机规范版本 */
	public static String java_vm_specification_version()
	{
		return systemProperties.getProperty("java.vm.specification.version");
	}

	/** Java 虚拟机规范供应商 */
	public static String java_vm_specification_vendor()
	{
		return systemProperties.getProperty("java.vm.specification.vendor ");
	}

	/** Java 虚拟机规范名称 */
	public static String java_vm_specification_name()
	{
		return systemProperties.getProperty("java.vm.specification.name ");
	}

	/** Java 虚拟机实现版本 */
	public static String java_vm_version()
	{
		return systemProperties.getProperty("java.vm.version");
	}

	/** Java 虚拟机实现供应商 */
	public static String java_vm_vendor()
	{
		return systemProperties.getProperty("java.vm.vendor");
	}

	/** Java 虚拟机实现名称 */
	public static String java_vm_name()
	{
		return systemProperties.getProperty("java.vm.name");
	}

	/** Java 运行时环境规范版本 */
	public static String java_specification_version()
	{
		return systemProperties.getProperty("java.specification.version");
	}

	/** Java 运行时环境规范供应商 */
	public static String java_specification_vendor()
	{
		return systemProperties.getProperty("java.specification.vendor");
	}

	/** Java 运行时环境规范名称 */
	public static String java_specification_name()
	{
		return systemProperties.getProperty("java.specification.name");
	}

	/** Java 类格式版本号 */
	public static String java_class_version()
	{
		return systemProperties.getProperty("java.class.version");
	}

	/** Java 类路径 */
	public static String java_class_path()
	{
		return systemProperties.getProperty("java.class.path");
	}

	/** 加载库时搜索的路径列表 */
	public static String java_library_path()
	{
		return systemProperties.getProperty("java.library.path");
	}

	/** 默认的临时文件路径 */
	public static String java_io_tmpdir()
	{
		return systemProperties.getProperty("java.io.tmpdir");
	}

	/** 要使用的 JIT 编译器的名称 */
	public static String java_compiler()
	{
		return systemProperties.getProperty("java.compiler");
	}

	/** 一个或多个扩展目录的路径 */
	public static String java_ext_dirs()
	{
		return systemProperties.getProperty("java.ext.dirs");
	}

	/** 操作系统的名称 */
	public static String os_name()
	{
		return systemProperties.getProperty("os.name");
	}

	/** 操作系统的架构 */
	public static String os_arch()
	{
		return systemProperties.getProperty("os.arch");
	}

	/** 操作系统的版本 */
	public static String os_version()
	{
		return systemProperties.getProperty("os.version");
	}

	private static String s = null;

	/** 文件分隔符（在 UNIX 系统中是“/”） */
	public static String file_separator()
	{
		if (Strings.isNullOrEmpty(s))
		{
			s = systemProperties.getProperty("file.separator");
		}
		return s;
	}

	/** 路径分隔符（在 UNIX 系统中是“:”） */
	public static String path_separator()
	{
		return systemProperties.getProperty("path.separator");
	}

	/** 行分隔符（在 UNIX 系统中是“/n”） */
	public static String line_separator()
	{
		return systemProperties.getProperty("line.separator");
	}

	/** 用户的账户名称 */
	public static String user_name()
	{
		return systemProperties.getProperty("user.name");
	}

	/** 用户的主目录 */
	public static String user_home()
	{
		return systemProperties.getProperty("user.home");
	}

	/** 用户的当前工作目录 */
	public static String user_dir()
	{
		return systemProperties.getProperty("user.dir");
	}

	public static void display()
	{
		System.out.println("当前操作系统: " + (isWindows() ? "Windows." : "No Windows."));
		System.out.println("运行时环境版本: " + systemProperties.getProperty("java.version")); // Java 运行时环境版本
		System.out.println("运行时环境供应商: " + systemProperties.getProperty("java.vendor")); // Java 运行时环境供应商
		System.out.println("供应商的 URL: " + systemProperties.getProperty("java.vendor.url")); // Java 供应商的 URL
		System.out.println("安装目录: " + systemProperties.getProperty("java.home")); // Java 安装目录
		System.out.println("虚拟机规范版本: " + systemProperties.getProperty("java.vm.specification.version")); // Java 虚拟机规范版本
		System.out.println("虚拟机规范供应商: " + systemProperties.getProperty("java.vm.specification.vendor ")); // Java 虚拟机规范供应商
		System.out.println("虚拟机规范名称: " + systemProperties.getProperty("java.vm.specification.name ")); // Java 虚拟机规范名称
		System.out.println("虚拟机实现版本: " + systemProperties.getProperty("java.vm.version")); // Java 虚拟机实现版本
		System.out.println("虚拟机实现供应商: " + systemProperties.getProperty("java.vm.vendor")); // Java 虚拟机实现供应商
		System.out.println("虚拟机实现名称: " + systemProperties.getProperty("java.vm.name")); // Java 虚拟机实现名称
		System.out.println("运行时环境规范版本: " + systemProperties.getProperty("java.specification.version")); // Java 运行时环境规范版本
		System.out.println("运行时环境规范供应商: " + systemProperties.getProperty("java.specification.vendor")); // Java 运行时环境规范供应商
		System.out.println("运行时环境规范供应商: " + systemProperties.getProperty("java.specification.name")); // Java 运行时环境规范供应商
		System.out.println("类格式版本号: " + systemProperties.getProperty("java.class.version")); // Java 类格式版本号
		System.out.println("类路径: " + systemProperties.getProperty("java.class.path")); // Java 类路径
		System.out.println("加载库时搜索的路径列表: " + systemProperties.getProperty("java.library.path")); // 加载库时搜索的路径列表
		System.out.println("默认的临时文件路径: " + systemProperties.getProperty("java.io.tmpdir")); // 默认的临时文件路径
		System.out.println("要使用的 JIT 编译器的名称: " + systemProperties.getProperty("java.compiler")); // 要使用的 JIT 编译器的名称
		System.out.println("一个或多个扩展目录的路径: " + systemProperties.getProperty("java.ext.dirs")); // 一个或多个扩展目录的路径
		System.out.println("操作系统的名称: " + systemProperties.getProperty("os.name")); // 操作系统的名称
		System.out.println("操作系统的架构: " + systemProperties.getProperty("os.arch")); // 操作系统的架构
		System.out.println("操作系统的版本: " + systemProperties.getProperty("os.version")); // 操作系统的版本
		System.out.println("文件分隔符（在 UNIX 系统中是“/”）: " + systemProperties.getProperty("file.separator")); // 文件分隔符（在 UNIX 系统中是“/”）
		System.out.println("路径分隔符（在 UNIX 系统中是“:”）: " + systemProperties.getProperty("path.separator")); // 路径分隔符（在 UNIX 系统中是“:”）
		System.out.println("行分隔符（在 UNIX 系统中是“/n”）: " + systemProperties.getProperty("line.separator")); // 行分隔符（在 UNIX 系统中是“/n”）
		System.out.println("用户的账户名称: " + systemProperties.getProperty("user.name")); // 用户的账户名称
		System.out.println("用户的主目录: " + systemProperties.getProperty("user.home")); // 用户的主目录
		System.out.println("用户的当前工作目录: " + systemProperties.getProperty("user.dir")); // 用户的当前工作目录
		System.out.println("===========");
		OS os = new OS();
		try
		{
			System.out.println("Class.getResource(\"/\").getFile(): " + os.getClass().getResource("/").getFile());
		}
		catch (Exception e)
		{
		}
		try
		{
			System.out.println("Class.getResource(\"\").getFile(): " + os.getClass().getResource("").getFile());
		}
		catch (Exception e)
		{
		}
		System.out.println("===========");
		System.out.println();
	}

	/**
	 * 类型的主方法。一般用来做测试使用。
	 * 
	 * @param args
	 *            方法的传入参数集合
	 */
	public static void main(final String[] args)
	{
		System.out.println(" // == 开始测试 ===============");
		display();
		System.out.println(" // == 测试完成 ===============");
	}
}
