package net.xknife.lang.interfaces;

import org.dom4j.Element;

/**
 * 一个描述一个类型可以通过XML进行翻译
 * 
 * @author lukan@jeelu.com
 * 
 */
public interface IXmlTranslate extends ITranslateable<Element>
{
	/**
	 * 将本类型的数据保存在一个Xml的Element节点中。
	 */
	@Override
	public Element translate();

	/**
	 * 从Element中解析得本类型，本类型的数据应由Element进行携带。
	 */
	@Override
	public void translate(Element element);
}
