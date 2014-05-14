package net.xknife.data;

import com.jolbox.bonecp.BoneCP;
import com.jolbox.bonecp.BoneCPConfig;
import net.xknife.data.api.IDbHelper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.Properties;

/**
 * 使用JDBC访问数据库的通用助手类。(此处是指的一个数据库)<BR>
 * 使用<code>BoneCP</code>进行数据库连接池管理。<BR>
 * 使用<code>QueryRunner</code>进行查询过程的封装。
 *
 * Created by yangjuntao@xknife.net on 2014/5/13 0013.
 */
public abstract class BoneCPDbHelper extends IDbHelper.AbstractBaseDbHelper
{
    private static final Logger _Logger = LoggerFactory.getLogger(BoneCPDbHelper.class);

    protected BoneCP _ConnPool = null;

    /**
     * 在子类中描述连接池的配置
     */
    protected abstract Properties getPoolConfigProperties();

    /**
     * 构造函数，在构造函数中初始化连接池与DbUtil
     */
    public BoneCPDbHelper()
    {
        setBoneCP();
    }

    /**
     * 设置数据库连接池
     */
    protected void setBoneCP()
    {
        BoneCPConfig config = null;
        try
        {
            config = new BoneCPConfig(getPoolConfigProperties());
        }
        catch (Exception e)
        {
            _Logger.error("无法载入连接池配置信息。", e);
            return;
        }
        try
        {
            _ConnPool = new BoneCP(config);
        }
        catch (SQLException e)
        {
            _Logger.error("无法创建数据库连接池。", e);
        }
    }

    @Override
    public Connection getConnection()
    {
        try
        {
            return _ConnPool.getConnection();
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
        return _ConnPool != null;
    }
}
