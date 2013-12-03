package net.xknife.library.utilities;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

/**
 * Message Digest Algorithm MD5（中文全名为消息摘要算法第五版）为计算机安全领域广泛使用的一种散列函数，用以提供消息的完整性保护。<br/>
 * 该算法的文件号为RFC 1321（R.Rivest,MIT Laboratory for Computer Science and RSA Data Security Inc. April 1992）<br/>
 * MD5的作用是让大容量信息在用数字签名软件签署私人密钥前被"压缩"成一种保密的格式（就是把一个任意长度的字节串变换成一定长的十六进制数字串）<br/>
 * 
 * @author lukan@p-an.com 2013年11月5日
 */
public class MD5
{
	static char _HexDigits[] = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

	/**
	 * 得到一个源字符串的加密后MD5值
	 * 
	 * @param source
	 *            源字符串
	 * @return
	 * @throws NoSuchAlgorithmException
	 */
	public static String encode(final String source) throws NoSuchAlgorithmException
	{
		byte[] bytes = source.getBytes();
		// 使用MD5创建MessageDigest对象
		MessageDigest md5 = MessageDigest.getInstance("MD5");
		md5.update(bytes);
		byte[] md5bytes = md5.digest();
		int md5bytesLength = md5bytes.length;
		char chars[] = new char[md5bytesLength * 2];
		int k = 0;
		for (int i = 0; i < md5bytesLength; i++)
		{
			byte b = md5bytes[i];
			// 将每个数(int)b进行双字节加密
			chars[k++] = _HexDigits[(b >> 4) & 0xf];
			chars[k++] = _HexDigits[b & 0xf];
		}
		return new String(chars);
	}
}
