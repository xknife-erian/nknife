package net.xknife.data;

import com.mchange.v2.c3p0.ComboPooledDataSource;
import net.xknife.data.api.IDbHelper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.SQLException;

/**
 * 使用JDBC访问数据库的通用助手类。(此处是指的一个数据库)<BR>
 * 使用<code>C3P0</code>进行数据库连接池管理。<BR>
 * 使用<code>QueryRunner</code>进行查询过程的封装。
 *
 * Created by yangjuntao@xknife.net on 2014/5/14 0014.
 */
public abstract class C3P0DbHelper extends IDbHelper.AbstractBaseDbHelper
{
    private static final Logger _Logger = LoggerFactory.getLogger(C3P0DbHelper.class);

    protected DataSource _DataSource = null;
    /**
     * 在子类中描述数据源名称，请参考c3p0-config.xml配置
     */
    protected abstract String getDataSourceName();

    /**
     * 构造函数，在构造函数中初始化连接池与DbUtil
     */
    public C3P0DbHelper()
    {
        setC3P0();
    }

    /**
     * 设置数据库连接池
     */
    protected void setC3P0()
    {
        try
        {
            _DataSource = new ComboPooledDataSource(getDataSourceName());
        }
        catch (Exception e)
        {
            _Logger.error("无法载入数据源信息。", e);
            return;
        }
    }

    @Override
    public Connection getConnection()
    {
        try
        {
            return _DataSource.getConnection();
        }
        catch (SQLException e)
        {
            _Logger.warn("无法从连接池中获得数据库连接", e);
            return null;
        }
    }

    public DataSource getDataSource()
    {
        return _DataSource;
    }

    @Override
    public boolean hasConnectionProvider()
    {
        return _DataSource != null;
    }
}
