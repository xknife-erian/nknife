package net.xknife.io.properties;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Properties;

/**
 * Properties文件的助手类
 *
 * 【继续维护中......】
 *
 * Created by yangjuntao@xknife.net on 2014-07-24.
 */
public class XProperties
{
    private static final Logger _Logger = LoggerFactory.getLogger(XProperties.class);

    /**
     * 读取*.properties文件
     *
     * @param propertiesPath
     * @return
     */
    public static Properties load(String propertiesPath)
    {
        Properties properties = new Properties();
        try
        {
            properties.load(new FileInputStream(new File(propertiesPath)));
        } catch (IOException e)
        {
            _Logger.error("读取文件错误，请检查！" + propertiesPath, e);
            return null;
        }
        Enumeration keys = properties.propertyNames();
        while (keys.hasMoreElements())
        {
            String key = keys.nextElement().toString();
            properties.setProperty(key, properties.getProperty(key));
        }
        return properties;
    }
}
