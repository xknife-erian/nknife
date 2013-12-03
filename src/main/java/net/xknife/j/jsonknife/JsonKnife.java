package net.xknife.j.jsonknife;

import java.io.IOException;
import java.io.StringWriter;

import com.fasterxml.jackson.core.JsonFactory;
import com.fasterxml.jackson.core.JsonGenerator;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.guava.GuavaModule;
import com.fasterxml.jackson.datatype.joda.JodaModule;

/**
 * 基于jackson的第三方json处理库定义的一些帮助方法封装。<br/>
 * 本类型全局采用一个ObjectMapper。<br/>
 * 一般情况请采用JsonUtil.getFactory()来获取JsonFactory，并从该Factory中获取对应流的JsonGenerator并进行操作。
 * 
 * @author lukan@p-an.com 2013年11月8日
 */
public class JsonKnife
{
	protected static ObjectMapper _Mapper;
	static
	{
		_Mapper = new ObjectMapper();
		_Mapper.registerModule(new JodaModule());
		_Mapper.registerModule(new GuavaModule());
	}

	/**
	 * 获取一个Json解码器和编码器的工厂。通过该工厂可以快速的create指定的Generator和Parser。
	 * 
	 * @return
	 */
	public static JsonFactory getFactory()
	{
		return _Mapper.getFactory();
	}

	public static ObjectMapper getMapper()
	{
		return _Mapper;
	}

	/**
	 * 将实体转换成Json字符串并返回。
	 * 
	 * @param bean
	 *            实体Bean
	 * @return
	 * @throws IOException
	 */
	public static <T> String toJson(final T bean) throws IOException
	{
		String result = "{}";
		StringWriter writer = new StringWriter();
		JsonGenerator generator = getFactory().createGenerator(writer);
		generator.writeObject(bean);
		generator.flush();
		result = writer.toString();
		writer.close();
		generator.close();
		return result;
	}
}
