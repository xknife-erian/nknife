package net.xknife.web.interfaces;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * 描述是一个面向WebAPI的Controller接口。
 * 
 * @author lukan@p-an.com 2013年11月13日 23:37:11
 */
@Retention(value = RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface WebapiController
{
	/**
	 * 对应在Servlet名的第一和第二部份的名称。如“user/administrator”。其中第一部份表达为项目模块的名称，第二部份是Controller的名称。
	 * 
	 * @return
	 */
	public String value();
}
