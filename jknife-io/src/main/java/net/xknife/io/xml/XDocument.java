package net.xknife.io.xml;

import com.google.common.base.Strings;
import net.xknife.lang.widgets.Encoding;
import org.dom4j.*;
import org.dom4j.io.OutputFormat;
import org.dom4j.io.SAXReader;
import org.dom4j.io.XMLWriter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.*;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;

/**
 * 一个基于Dom4j的XML的Document操作助手类型。
 */
public final class XDocument
{

    private static final Logger _logger = LoggerFactory.getLogger(XDocument.class);

	private XDocument()
	{
	}

	/**
	 * 表达最基础的XML文件中的可能的内容
	 */
	public static final String BASE_XML_CONTEXT = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Root />";

	/**
	 * @return 本类型包裹的真实的Document
	 */
	public Document my()
	{
		return _document;
	}

	private Document _document;

	public String asXML()
	{
		return _document.asXML();
	}

	/**
	 * 为当前的XML文件的写操作设置的锁对象
	 */
	private final Object _lock = new Object();

	/**
	 * @return 返回当前XML文档在磁盘中的绝对路径,全名
	 */
	public String getFilePath()
	{
		return _filePath;
	}

	/**
	 * 当前XML文档在磁盘中的绝对路径,全名
	 */
	private String _filePath;

	/**
	 * 载入XML文件的具体内容，如果该XML文件不存在，返回null。
	 * 
	 * @param filepath XML文件的全名，含绝对路径。
	 * @return
	 */
	public static XDocument loadXml(final String filepath)
	{
		File xml = new File(filepath);
		if (!xml.exists())
		{
			_logger.info("没有发现XML文件。文件全名：" + filepath);
            return null;
		}
		SAXReader reader = new SAXReader();
		reader.setEncoding(Encoding.UTF8);
		Document document = null;
		try
		{
			document = reader.read(new File(filepath));
		}
		catch (DocumentException e)
		{
			_logger.info("无法载入XML文件。文件全名：" + filepath + "。", e);
		}
		XDocument xDocument = new XDocument();
		xDocument._document = document;
		xDocument._filePath = filepath;
		return xDocument;
	}

	/**
	 * 载入XML文件的具体内容，通过InputStream的方式。
	 * 
	 * @param input
	 * @return
	 */
	public static XDocument loadXml(final InputStream input)
	{
		SAXReader reader = new SAXReader();
		reader.setEncoding(Encoding.UTF8);
		Document document = null;
		try
		{
			document = reader.read(input);
		}
		catch (DocumentException e)
		{
			_logger.info("无法从流中载入XML。", e);
		}
		XDocument xDocument = new XDocument();
		xDocument._document = document;
		return xDocument;
	}

	/**
	 * 载入XML文件的具体内容，通过XML文本的方式。
	 * 
	 * @return
	 */
	public static XDocument loadText(final String xmlText)
	{
		Document document = null;
		try
		{
			document = DocumentHelper.parseText(xmlText);
		}
		catch (DocumentException e)
		{
            _logger.info("无法将文本转换成XML格式。", e);
		}
		XDocument xDocument = new XDocument();
		xDocument._document = document;
		return xDocument;
	}

	/**
	 * 保存xml文件;
	 * 
	 * @param encode 储存的编码;
	 * @throws java.io.IOException
	 */
	public void save(final String encode)
	{
		if (Strings.isNullOrEmpty(_filePath))
			return;
		synchronized (_lock)
		{
			FileOutputStream stream = null;
			try
			{
				stream = new FileOutputStream(_filePath);
			}
			catch (FileNotFoundException e1)
			{
                _logger.warn("存储XML文件时，文件流无法创建。");
			}
			OutputFormat format = OutputFormat.createPrettyPrint();// 美化格式,易读。
			format.setEncoding(encode);
			XMLWriter writer = null;
			try
			{
				writer = new XMLWriter(stream, format);
			}
			catch (UnsupportedEncodingException e1)
			{
				_logger.warn("错误的文件文本格式。格式:" + encode);
			}
			try
			{
				writer.write(_document);
				writer.flush();
				writer.close();
			}
			catch (IOException e)
			{
				System.err.println(e.getMessage());
                _logger.warn("写入XML文件时出错。");
			}

		}
	}

	/**
	 * 以UTF-8格式保存xml文件;
	 */
	public void save()
	{
		this.save(Encoding.UTF8);
	}

	/**
	 * @return 获得文档的根节点
	 */
	public Element getRootElement()
	{
		return _document.getRootElement();
	}

	/**
	 * 一个常用的快速从根节点下获取一个节点的方法。<br>
	 * 一般当根节点下的各个节点一般只有一个的情况下使用该方法可以快速的通过指定的限定名获得该Element。<br>
	 * 如果节点不存在时，直接创建该节点。(未持久化保存)
	 * 
	 * @param localName
	 * @return
	 */
	public Element single(final String localName)
	{
		String xpath = String.format("%s", localName);
		Node myNode = _document.getRootElement().selectSingleNode(xpath);
		if (null == myNode)
		{
			myNode = DocumentHelper.createElement(localName);
			_document.getRootElement().add(myNode);
			_logger.info(String.format("在XML文件的根节点下没有指定的节点。指定的节点的限定名:%s", localName));
		}
		return (Element) myNode;
	}

	/**
	 * 根据指定的ID和节点的限定名获得指定的Element
	 * 
	 * @param localName 节点的限定名
	 * @param id id值
	 * @return
	 */
	public Element selectById(final String localName, final String id)
	{
		String xpath = String.format("//%s[@%s='%s']", localName, "id", id);
		return (Element) _document.getRootElement().selectSingleNode(xpath);
	}

	// =================================================

	@Override
	public String toString()
	{
		return _document.asXML();
	}

	/**
	 * 设置节点的属性,当该属性不存在时,主动创建该属性
	 * 
	 * @param element
	 * @param name
	 * @param value
	 */
	public static void setAttribute(final Element element, final String name, final String value)
	{
		Attribute attribute = element.attribute(name);
		if (null == attribute)
		{
			attribute = DocumentHelper.createAttribute(element, name, value);
			element.add(attribute);
		}
		else
			attribute.setValue(value);
	}

	/**
	 * 从Element的属性集合生成HashMap
	 * 
	 * @param node
	 * @return
	 */
	public static HashMap<String, String> toMapByAttributes(final Node node)
	{
		if (null == node)
			return new HashMap<String, String>();
		if (node instanceof Element)
			return toMapByAttributes((Element) node);
		return new HashMap<String, String>();
	}

    /**
     * 从Element的子节点集合生成URL GET请求需要的参数字符串，格式param1=v1&param2=v2;
     *
     * @param parent
     * @return
     */
    public static String toUrlGetParam(final Element parent) throws UnsupportedEncodingException
    {
        String result = "";
        if (null == parent)
            return result;

        Iterator<Node> eles = parent.nodeIterator();
        Node node;
        Element ele;
        while (eles.hasNext())
        {
            node = eles.next();
            if(!(node instanceof Element))
            {
                continue;
            }
            ele = (Element)node;
            result += String.format("%s=%s&", ele.getName(), URLEncoder.encode(ele.getTextTrim(), "UTF-8"));
        }

        return result.substring(0, result.length()-1);
    }

	/**
	 * 检查Element是否存在子结点
	 * 
	 * @param element
	 * @return
	 */
	public static boolean existChild(final Element element)
	{
		List<?> childs = element.elements();
		if (childs.size() > 0)
			return true;
		return false;
	}

	/**
	 * 检查当前Element中是否存在某一属性名称
	 * 
	 * @param element
	 * @param attr_name
	 * @return
	 */
	public static boolean existAttribute(final Element element, final String attr_name)
	{
		Attribute attribute = element.attribute(attr_name);
		if (attribute != null)
			return true;
		return false;
	}

}
