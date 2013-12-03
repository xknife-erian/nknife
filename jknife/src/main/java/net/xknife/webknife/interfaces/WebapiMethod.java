package net.xknife.webknife.interfaces;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * 描述在IController接口的实现类中，作为Servlet方法的注解。
 * 
 * @author lukan@p-an.com 2013年11月13日
 */
@Retention(value = RetentionPolicy.RUNTIME)
@Target(ElementType.METHOD)
public @interface WebapiMethod
{
	/**
	 * 对应在Servlet名的第三部份的名称。如“login”，一般情况下应与方法名相同
	 * 
	 * @return 对应在Servlet名的第三部份的名称。
	 */
	String value();
}
