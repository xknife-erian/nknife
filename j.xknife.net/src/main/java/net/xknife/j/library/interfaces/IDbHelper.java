package net.xknife.j.library.interfaces;

import java.sql.Connection;
import java.sql.ParameterMetaData;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Types;
import java.util.Properties;

import org.apache.commons.dbutils.QueryRunner;
import org.apache.commons.dbutils.ResultSetHandler;

import com.jolbox.bonecp.BoneCP;
import com.jolbox.bonecp.BoneCPConfig;

public interface IDbHelper
{
	public abstract Connection getConnection();

	/**
	 * 执行 Sql 语句,返回结果为整型主要用于执行非查询语句
	 * 
	 * @param cmdText
	 *            Sql 语句
	 * @return 非负数:正常执行; -1:执行错误;
	 */
	public abstract int update(String cmdText);

	/**
	 * 执行 Sql 语句,返回结果为整型 主要用于执行非查询语句
	 * 
	 * @param sql
	 * @param param
	 * @return
	 */
	public abstract int update(String sql, Object param);

	/**
	 * 执行 Sql 语句,返回结果为整型 主要用于执行非查询语句
	 * 
	 * @param cmdText
	 *            需要 ? 参数的 Sql 语句
	 * @param cmdParams
	 *            Sql 语句的参数表
	 * @return 非负数:正常执行; -1:执行错误;
	 */
	public abstract int update(String cmdText, Object... cmdParams);

	/**
	 * 批量执行 Sql 语句, 返回结果为整型. 主要用于执行无参数语句
	 * 
	 * @param cmdText
	 *            Sql 语句
	 * @return 非负数:正常执行; -1:执行错误;
	 */
	public abstract int[] batch(String... cmdTextList);

	/**
	 * Execute a batch of SQL INSERT, UPDATE, or DELETE queries. The <code>Connection</code> is retrieved from the <code>BoneCP</code> set in the constructor.
	 * This <code>Connection</code> must be in auto-commit mode or the update will not be saved.
	 * 
	 * @param sql
	 *            The SQL to execute.
	 * @param params
	 *            An array of query replacement parameters. Each row in this array is one set of batch replacement values.
	 * @return The number of rows updated per statement.
	 */
	public abstract int[] batch(String sql, Object[][] params);

	/**
	 * Executes the given SELECT SQL query and returns a result object. The <code>Connection</code> is retrieved from the <code>BoneCP</code> set in the
	 * constructor.
	 * 
	 * @param <T>
	 *            The type of object that the handler returns
	 * @param sql
	 *            The SQL statement to execute.
	 * @param rsh
	 *            The handler used to create the result object from the <code>ResultSet</code>.
	 * @param params
	 *            Initialize the PreparedStatement's IN parameters with this array.
	 * @return An object generated by the handler.
	 */
	public abstract <T> T query(String sql, ResultSetHandler<T> rsh, Object... params);

	/**
	 * Executes the given SELECT SQL without any replacement parameters. The <code>Connection</code> is retrieved from the <code>DataSource</code> set in the
	 * constructor.
	 * 
	 * @param <T>
	 *            The type of object that the handler returns
	 * @param sql
	 *            The SQL statement to execute.
	 * @param rsh
	 *            The handler used to create the result object from the <code>ResultSet</code>.
	 * 
	 * @return An object generated by the handler.
	 */
	public abstract <T> T query(String sql, ResultSetHandler<T> rsh);

	/**
	 * 构造一个Sql语句
	 * 
	 * @param sqlType
	 *            语句类型
	 * @param params
	 *            构造语句的参数集合
	 * @return Sql语句
	 */
	public abstract String buildSql(int sqlType, String... params);

	/**
	 * 使用JDBC访问数据库的通用助手类。(此处是指的一个数据库)<BR>
	 * 使用<code>BoneCP</code>进行数据库连接池管理。<BR>
	 * 使用<code>QueryRunner</code>进行查询过程的封装。
	 * 
	 * @author lukan@jeelu.com
	 */
	public abstract class DbHelper implements IDbHelper
	{
		static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(DbHelper.class);
		protected BoneCP _ConnPool = null;

		/**
		 * 在子类中描述连接池的配置
		 */
		protected abstract Properties getPoolConfigProperties();

		/**
		 * 构造函数，在构造函数中初始化连接池与DbUtil
		 */
		public DbHelper()
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

		/**
		 * Executes the given INSERT, UPDATE, or DELETE SQL statement without any replacement parameters. The <code>Connection</code> is retrieved from the
		 * <code>BoneCP</code> set in the constructor. This <code>Connection</code> must be in auto-commit mode or the update will not be saved.
		 * 
		 * @param sql
		 *            The SQL statement to execute.
		 * @return The number of rows updated.
		 */
		@Override
		public int update(final String sql)
		{
			int replay = 0;
			Connection connection = getConnection();
			try
			{
				replay = new QueryRunner().update(connection, sql);
			}
			catch (SQLException e)
			{
				_Logger.warn(String.format("执行语句错误，%s", sql), e);
			}
			finally
			{
				close(connection);
			}
			return replay;
		}

		/**
		 * Executes the given INSERT, UPDATE, or DELETE SQL statement with a single replacement parameter. The <code>Connection</code> is retrieved from the
		 * <code>BoneCP</code> set in the constructor. This <code>Connection</code> must be in auto-commit mode or the update will not be saved.
		 * 
		 * @param sql
		 *            The SQL statement to execute.
		 * @param param
		 *            The replacement parameter.
		 * @return The number of rows updated.
		 */
		@Override
		public int update(final String sql, final Object param)
		{
			int replay = 0;
			Connection connection = getConnection();
			try
			{
				connection.setAutoCommit(false);
				replay = new QueryRunner().update(connection, sql, new Object[] { param });
			}
			catch (SQLException e)
			{
				_Logger.warn(String.format("执行语句错误，%s", sql), e);
			}
			finally
			{
				close(connection);
			}
			return replay;
		}

		/**
		 * Executes the given INSERT, UPDATE, or DELETE SQL statement. The <code>Connection</code> is retrieved from the <code>BoneCP</code> set in the
		 * constructor. This <code>Connection</code> must be in auto-commit mode or the update will not be saved.
		 * 
		 * @param sql
		 *            The SQL statement to execute.
		 * @param params
		 *            Initializes the PreparedStatement's IN (i.e. '?') parameters.
		 * @return The number of rows updated.
		 */
		@Override
		public int update(final String sql, final Object... params)
		{
			int replay = 0;
			Connection connection = getConnection();
			try
			{
				connection.setAutoCommit(false);
				replay = new QueryRunner().update(connection, sql, params);
			}
			catch (SQLException e)
			{
				_Logger.warn(String.format("执行语句错误，%s", sql), e);
			}
			finally
			{
				close(connection);
			}
			return replay;
		}

		/**
		 * 批量执行 Sql 含参数单格式语句。=> SQL INSERT, UPDATE, or DELETE queries.
		 * 
		 * @param conn
		 *            The Connection to use to run the query. The caller is responsible for closing this Connection.
		 * @param sql
		 *            The SQL to execute.
		 * @param params
		 *            An array of query replacement parameters. Each row in this array is one set of batch replacement values.
		 * @return The number of rows updated per statement.
		 */

		@Override
		public int[] batch(final String sql, final Object[][] params)
		{
			int[] replay = null;
			Connection connection = getConnection();
			try
			{
				connection.setAutoCommit(true);
				replay = new QueryRunner().batch(connection, sql, params);
			}
			catch (SQLException e)
			{
				_Logger.warn(String.format("批量执行语句错误，%s", sql), e);
				replay = new int[params.length];
				for (int i : replay)
				{
					replay[i] = -1;
				}
			}
			finally
			{
				close(connection);
			}
			return replay;
		}

		/**
		 * 批量执行 Sql 无参数多格式语句, 返回结果为整型. 主要用于执行无参数语句
		 * 
		 * @param cmdText
		 *            Sql 语句
		 * @return 非负数:正常执行; -1:执行错误;
		 */

		@Override
		public int[] batch(final String... sqlList)
		{
			Statement statement = getStatement();
			for (String sql : sqlList)
			{
				try
				{
					statement.addBatch(sql);
				}
				catch (SQLException e)
				{
					_Logger.warn(String.format("添加批处理语句时异常，异常语句：%s", sql), e);
				}
			}
			int[] reply = new int[] { -1 };
			try
			{
				reply = statement.executeBatch();
			}
			catch (SQLException e)
			{
				StringBuilder sb = new StringBuilder();
				for (String sql : sqlList)
				{
					sb.append(sql);
				}
				_Logger.warn("执行批处理语句时出错." + sb.toString(), e);
			}
			return reply;
		}

		/**
		 * Executes the given SELECT SQL query and returns a result object. The <code>Connection</code> is retrieved from the <code>BoneCP</code> set in the
		 * constructor.
		 * 
		 * @param <T>
		 *            The type of object that the handler returns
		 * @param sql
		 *            The SQL statement to execute.
		 * @param rsh
		 *            The handler used to create the result object from the <code>ResultSet</code>.
		 * @param params
		 *            Initialize the PreparedStatement's IN parameters with this array.
		 * @return An object generated by the handler.
		 */

		@Override
		public <T> T query(final String sql, final ResultSetHandler<T> rsh, final Object... params)
		{
			Connection connection = getConnection();
			try
			{
				T t = new QueryRunner().query(connection, sql, rsh, params);
				return t;
			}
			catch (SQLException e)
			{
				_Logger.warn(String.format("执行查询异常，异常语句：%s", sql), e);
			}
			finally
			{
				close(connection);
			}
			return null;
		}

		/**
		 * Executes the given SELECT SQL without any replacement parameters. The <code>Connection</code> is retrieved from the <code>BoneCP</code> set in the
		 * constructor.
		 * 
		 * @param <T>
		 *            The type of object that the handler returns
		 * @param sql
		 *            The SQL statement to execute.
		 * @param rsh
		 *            The handler used to create the result object from the <code>ResultSet</code>.
		 * @return An object generated by the handler.
		 */
		@Override
		public <T> T query(final String sql, final ResultSetHandler<T> rsh)
		{
			Connection connection = getConnection();
			try
			{
				T t = new QueryRunner().query(connection, sql, rsh);
				return t;
			}
			catch (SQLException e)
			{
				_Logger.error("执行查询异常。" + sql, e);
			}
			finally
			{
				close(connection);
			}
			return null;
		}

		protected Statement getStatement()
		{
			if (_ConnPool == null)
			{
				return null;
			}
			try
			{
				return getConnection().createStatement();
			}
			catch (SQLException ex)
			{
				_Logger.error("获取 Statement失败。", ex);
				return null;
			}
		}

		protected PreparedStatement getPreparedStatement(final String cmdText, final Object... cmdParams)
		{
			if (_ConnPool == null)
			{
				return null;
			}
			try
			{
				Connection connection = getConnection();
				PreparedStatement statement = connection.prepareStatement(cmdText);
				fillStatement(statement, cmdParams);
				return statement;
			}
			catch (SQLException ex)
			{
				_Logger.error(String.format("获取 Statement失败。%s", cmdText), ex);
				return null;
			}
		}

		/**
		 * 填充PreparedStatement参数。从QueryRunner抄出来的方法。
		 * 
		 * @param stmt
		 * @param params
		 * @throws SQLException
		 */
		protected static void fillStatement(final PreparedStatement stmt, final Object... params) throws SQLException
		{
			if (params == null)
			{
				return;
			}
			ParameterMetaData pmd = stmt.getParameterMetaData();
			if (pmd.getParameterCount() < params.length)
			{
				throw new SQLException("Too many parameters: expected " + pmd.getParameterCount() + ", was given " + params.length);
			}
			for (int i = 0; i < params.length; i++)
			{
				if (params[i] != null)
				{
					stmt.setObject(i + 1, params[i]);
				}
				else
				{
					// VARCHAR works with many drivers regardless
					// of the actual column type. Oddly, NULL and
					// OTHER don't work with Oracle's drivers.
					int sqlType = Types.VARCHAR;
					try
					{
						sqlType = pmd.getParameterType(i + 1);
					}
					catch (SQLException e)
					{
						_Logger.warn(String.format("获取ParameterType失败。"), e);
					}
					stmt.setNull(i + 1, sqlType);
				}
			}
		}

		/**
		 * 关闭ResultSet，其中会反向按顺序执行三项重要的关闭。<BR>
		 * rs.close();<BR>
		 * rs.getStatement().close();<BR>
		 * rs.getStatement().getConnection().close();
		 */
		public static void close(final ResultSet resultSet)
		{
			try
			{
				if (resultSet != null)
				{
					resultSet.close();
				}
			}
			catch (Exception e)
			{
				_Logger.error("ResultSet关闭方法：关闭ResultSet发生异常。" + e.getMessage(), e);
			}
			finally
			{
				try
				{
					if (resultSet != null)
					{
						Statement statement = resultSet.getStatement();
						if (statement != null)
						{
							statement.close();
						}
					}
				}
				catch (Exception e)
				{
					_Logger.error("ResultSet关闭方法：关闭Statement发生异常。" + e.getMessage(), e);
				}
				finally
				{
					try
					{
						if ((resultSet != null) && (resultSet.getStatement() != null))
						{
							Connection connection = resultSet.getStatement().getConnection();
							if (connection != null)
							{
								connection.close();
							}
						}
					}
					catch (Exception e)
					{
						_Logger.error("ResultSet关闭方法：关闭Connection发生异常。" + e.getMessage(), e);
					}
				}
			}
		}

		/**
		 * 关闭Statement，其中会反向按顺序执行两项重要的关闭。<BR>
		 * statement.close();<BR>
		 * statement.getConnection().close();
		 */
		public static void close(final Statement statement)
		{
			try
			{
				if (statement != null)
				{
					statement.close();
				}
			}
			catch (Exception e)
			{
				_Logger.error("Statement关闭方法：关闭Statement发生异常。" + e.getMessage(), e);
			}
			finally
			{
				try
				{
					if (statement != null)
					{
						Connection connection = statement.getConnection();
						if (connection != null)
						{
							connection.close();
						}
					}
				}
				catch (Exception e)
				{
					_Logger.error("Statement关闭方法：关闭Connection发生异常。" + e.getMessage(), e);
				}
			}
		}

		/**
		 * 关闭数据库Connection
		 * 
		 * @param conn
		 */
		protected static void close(final Connection connection)
		{
			try
			{
				if (connection != null)
				{
					// 这个连接的关闭实际并未真正关闭，仅仅是将连接进行释放进池中。
					// 因为这个连接是从池中获取，而这个连接类是BoneCP重写过的
					connection.close();
				}
			}
			catch (Exception e)
			{
				_Logger.error("Connection关闭方法：异常。" + e.getMessage(), e);
			}
		}

		/**
		 * 将 Sql 的字段值进行转意，可以用来防止 Sql 注入攻击
		 * 
		 * @param s
		 *            字段值
		 * @return 格式化后的 Sql 字段值，可以直接拼装在 Sql 里面
		 */
		protected static CharSequence escapeFieldValue(final CharSequence s)
		{
			if (null == s)
			{
				return null;
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < s.length(); i++)
			{
				char c = s.charAt(i);
				if (c == '\'')
				{
					sb.append('\'').append('\'');
				}
				else if (c == '\\')
				{
					sb.append('\\').append('\\');
				}
				else
				{
					sb.append(c);
				}
			}
			return sb;
		}
	}

}
