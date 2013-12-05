package net.xknife.data.mongo;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * 描述是一个面向Mongo的注解。
 * 
 * @author yangjuntao@jeelu.com 2013年11月24日
 */
@Retention(value = RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
public @interface Mongo
{
	public String value();
}
