package net.xknife.lang.utilities;

import java.security.SecureRandom;
import java.util.UUID;

/**
 * 封装各种生成唯一性ID算法的工具类.
 * 
 */
public class Identities
{
	private static SecureRandom random = new SecureRandom();

	/**
	 * 封装JDK自带的UUID, 通过Random数字生成, 中间有-分割.
	 */
	public static String uuid()
	{
		return UUID.randomUUID().toString();
	}

	/**
	 * 封装JDK自带的UUID, 通过Random数字生成, 中间无-分割.
	 */
	public static String uuid2()
	{
		return UUID.randomUUID().toString().replaceAll("-", "");
	}

	/**
	 * 使用SecureRandom随机生成Long.
	 */
	public static long randomLong()
	{
		return Math.abs(random.nextLong());
	}
<<<<<<< HEAD
	//
	// /**
	// * 基于Base62编码的SecureRandom随机生成bytes.
	// */
=======

	/**
	 * 基于Base62编码的SecureRandom随机生成bytes.
	 */
>>>>>>> 51e27eee47256de0ec3adc8187bc6339e87d5728
	// public static String randomBase62(final int length) {
	// byte[] randomBytes = new byte[length];
	// random.nextBytes(randomBytes);
	// return Encodes.encodeBase62(randomBytes);
	// }
}
