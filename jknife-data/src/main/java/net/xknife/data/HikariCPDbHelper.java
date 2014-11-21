package net.xknife.data;

import com.zaxxer.hikari.HikariConfig;
import com.zaxxer.hikari.HikariDataSource;
import net.xknife.data.api.IDbHelper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.Properties;

/**
 * 使用HikariCP实现数据库连接池。
 * HikariCP uses milliseconds for all time values.
 *
 * Created by yangjuntao@xknife.net on 2014/5/14 0014.
 */
public abstract class HikariCPDbHelper extends IDbHelper.AbstractBaseDbHelper
{
    private static final Logger _Logger = LoggerFactory.getLogger(HikariCPDbHelper.class);

    protected HikariDataSource _DataSource = null;
    /**
     * 在子类中描述连接池的配置文件路径
     */
    protected abstract Properties getPoolConfigProperties();

    protected HikariCPDbHelper()
    {
        setHikari();
    }

    private void setHikari()
    {
        HikariConfig config = null;
        try
        {
            config = new HikariConfig(getPoolConfigProperties());
        }
        catch (Exception e)
        {
            _Logger.error("无法载入连接池配置信息。", e);
            return;
        }
        _DataSource = new HikariDataSource(config);
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

    @Override
    public boolean hasConnectionProvider()
    {
        return _DataSource != null;
    }
}
