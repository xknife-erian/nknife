package net.xknife.library.interfaces;

/**
 * 描述一个类型可以向指定的类型翻译，且可以从指定的类型翻译出来。
 * 
 * @author lukan@p-an.com 2013年10月19日
 * @param <T>
 *            指定的类型
 */
public interface ITranslateable<T>
{
	public T translate();

	public void translate(T entity);
}
