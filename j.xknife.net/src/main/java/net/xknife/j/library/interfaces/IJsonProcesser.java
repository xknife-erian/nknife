/**
 * 
 */
package net.xknife.j.library.interfaces;

/**
 * @author yangjuntao@jeelu.com 2013年10月30日
 */
public interface IJsonProcesser<T>
{
	public T json2Object(final String jsonString);

	public String object2Json(final T object);

}
