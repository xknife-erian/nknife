using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;
using System.Threading;

namespace Jeelu.Data
{
	/// <summary>
	/// 数据访问类
	/// </summary>
	abstract public class DataAccess
	{
		static private string _connectionString;
		static private DbProvider _provider;
		/// <summary>
		/// 获取连接字符串
		/// </summary>
		static public string ConnectionString
		{
			get
			{
				return _connectionString;
			}
		}
        static protected string _lastSQL;
        static public string LastSQL
        {
            get
            {
                return _lastSQL;
            }
        }

		/// <summary>
		/// 获取数据访问提供者(OleDb or Sql)
		/// </summary>
		static public DbProvider Provider
		{
			get
			{
				return _provider;
			}
		}

		#region 异步执行sql
		static private Queue _queue;
		static private object SyncQueue = new object();
		/// <summary>
		/// 将一个sql语句添加到命令任务队列中
		/// </summary>
		/// <param name="strCmd"></param>
		static public void QueueCommandWork(string strCmd)
		{
			if (_queue == null)
			{
				lock (SyncQueue)
				{
					if (_queue == null)
					{
						InitCommandWork();
					}
				}
			}

			_queue.Enqueue(strCmd);
		}
		static private void InitCommandWork()
		{
			_queue = Queue.Synchronized(new Queue());
			ThreadPool.QueueUserWorkItem(new WaitCallback(DoWhile));
		}
		static private void DoWhile(object state)
		{
			DataAccess da = DataAccess.Create();
			try
			{
				da.OpenConnect();
				while (true)
				{
					while (_queue.Count != 0)
					{
						object obj = _queue.Dequeue();
						try
						{
							da.ExecuteNonQuery(obj.ToString());
						}
						catch(Exception)
						{
						}
					}

					Thread.Sleep(1);
				}
			}
			finally
			{
				da.CloseConnect();
			}
		}
		#endregion

		/// <summary>
		/// 对外隐藏其构造方法
		/// </summary>
		protected DataAccess()
		{
		}

		/// <summary>
		/// 创建DataAccess对象
		/// </summary>
		/// <returns></returns>
		static public DataAccess Create()
		{
			switch (Provider)
			{
				case DbProvider.OleDb:
					return new OleDbDataAccess(ConnectionString);
					
				case DbProvider.SqlClient:
					return new SqlDataAccess(ConnectionString);

                case DbProvider.MySql:
                    return new MySqlDataAccess(ConnectionString);

				default:
					throw new Exception("未初始化或初始化失败。");
			}
		}

		/// <summary>
        /// 初始化配置
		/// </summary>
		/// <param name="provider">驱动</param>
		/// <param name="dataSource">服务器(ip)</param>
		/// <param name="initialCatalog">数据库名</param>
		/// <param name="user"></param>
		/// <param name="password"></param>
		static public void Initialize(DbProvider provider,string dataSource,string initialCatalog,string user,string password)
		{
			_provider = provider;
			switch (provider)
			{
				case DbProvider.OleDb:
					_connectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;User ID={0};Password={1};Initial Catalog={2};Data Source={3}";
					break;

				case DbProvider.SqlClient:
					_connectionString = "User id={0};Password={1};integrated security=SSPI;Initial Catalog={2};Data Source={3}";
					break;

                case DbProvider.MySql:
                    _connectionString = "server='{3}';user id='{0}';Password='{1}';persist security info=True;database='{2}'";
                    break;

				default:
					throw new Exception("未预料的DbProvider参数值");
			}
			_connectionString = string.Format(_connectionString,user,password,initialCatalog,dataSource);
		}
		/// <summary>
		/// 初始化配置[不推荐使用]
		/// </summary>
		/// <param name="connectionString"></param>
		static public void Initialize(DbProvider provider,string connectionString)
		{
			_provider = provider;
			
			_connectionString = connectionString;
		}

		abstract public IDbTransaction Transaction{get;}
		/// <summary>
		/// 打开数据库连接，如果数据库已经打开，则将连接计数加1
		/// </summary>
		abstract public void OpenConnect();
		/// <summary>
		/// 将连接计数减1，如果连接计数为0则关闭连接
		/// </summary>
		abstract public void CloseConnect();

		/// <summary>
		/// 开启事务(内部会调用OpenConnect)
		/// </summary>
		abstract public void BeginTran();
		/// <summary>
		/// 提交事务(内部会调用CloseConnect)
		/// </summary>
		abstract public void CommitTran();
		/// <summary>
		/// 回滚事务(内部会调用CloseConnect)
		/// </summary>
		abstract public void RollbackTran();

		/// <summary>
		/// 执行指定的 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public int ExecuteNonQuery(string strCmd, params object[] paramValues);
		/// <summary>
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public object ExecuteScalar(string strCmd, params object[] paramValues);
		/// <summary>
		/// 执行查询，生成一个DataReader返回数据(此函数内部不会调用OpenConnect和CloseConnect)
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public IDataReader ExecuteReader(string strCmd, params object[] paramValues);
		/// <summary>
		/// 执行查询，并返回一个DataSet对象
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public DataSet ExecuteDataSet(string strCmd, params object[] paramValues);
		/// <summary>
		/// 执行查询，并返回一个DataTable对象
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public DataTable ExecuteDataTable(string strCmd, params object[] paramValues);
        abstract public int ExecuteMUpdate(string sql, DataSet dataSet, params object[] paramValues);
        abstract public int ExecuteUpdate(string sql, DataTable table, params object[] paramValues);

        abstract public void ProcedureNonQuery(string procedureName, params object[] paramValues);
        abstract public void ProcedureNonQuery(string procedureName, string[] paramNames,object[] paramValues);
        abstract public void ProcedureNonQuery(string procedureName, params IDbDataParameter[] dbParams);
        abstract public object ProcedureScalar(string procedureName, params object[] paramValues);
        abstract public object ProcedureScalar(string procedureName, string[] paramNames, object[] paramValues);
        abstract public DataSet ProcedureDataSet(string procedureName, params object[] paramValues);
        abstract public DataSet ProcedureDataSet(string procedureName, string[] paramNames, object[] paramValues);
        abstract public DataSet ProcedureDataSet(string procedureName, params IDbDataParameter[] dbParams);
        abstract public DataTable ProcedureDataTable(string procedureName, params object[] paramValues);
        abstract public DataTable ProcedureDataTable(string procedureName, string[] paramNames, object[] paramValues);
        abstract public DataTable ProcedureDataTable(string procedureName, params IDbDataParameter[] dbParams);

		/// <summary>
		/// 执行指定的 Transact-SQL 语句并返回受影响的行数。
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public int SExecuteNonQuery(string strCmd, params object[] paramValues)
		{
            return Create().ExecuteNonQuery(strCmd, paramValues);
		}
		/// <summary>
		/// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public object SExecuteScalar(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteScalar(strCmd, paramValues);
		}
		/// <summary>
		/// 执行查询，并返回一个DataSet对象
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public DataSet SExecuteDataSet(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteDataSet(strCmd, paramValues);
		}
		/// <summary>
		/// 执行查询，并返回一个DataTable对象
		/// </summary>
        static public DataTable SExecuteDataTable(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteDataTable(strCmd, paramValues);
		}

        /// <summary>
        /// 执行更新，并返回更新的行数
        /// </summary>
        static public int SExecuteMUpdate(string sql, DataSet dataSet, params object[] paramValues)
        {
            return Create().ExecuteMUpdate(sql, dataSet, paramValues);
        }

        /// <summary>
        /// 执行更新，并返回更新的ID
        /// </summary>
        static public int SExecuteUpdate(string sql, DataTable table, params object[] paramValues)
        {
            return Create().ExecuteUpdate(sql, table, paramValues);
        }

        static public void SProcedureNonQuery(string procedureName, params object[] paramValues)
        {
            Create().ProcedureNonQuery(procedureName, paramValues);
        }
        static public void SProcedureNonQuery(string procedureName, string[] paramNames, object[] paramValues)
        {
            Create().ProcedureNonQuery(procedureName, paramNames, paramValues);
        }
        static public void SProcedureNonQuery(string procedureName, params IDbDataParameter[] dbParams)
        {
            Create().ProcedureNonQuery(procedureName, dbParams);
        }
        static public object SProcedureScalar(string procedureName, params object[] paramValues)
        {
            return Create().ProcedureScalar(procedureName, paramValues);
        }
        static public object SProcedureScalar(string procedureName, string[] paramNames, object[] paramValues)
        {
            return Create().ProcedureScalar(procedureName, paramNames ,paramValues);
        }
        static public DataSet SProcedureDataSet(string procedureName, params object[] paramValues)
        {
            return Create().ProcedureDataSet(procedureName, paramValues);
        }
        static public DataSet SProcedureDataSet(string procedureName, string[] paramNames, object[] paramValues)
        {
            return Create().ProcedureDataSet(procedureName, paramNames, paramValues);
        }
        static public DataSet SProcedureDataSet(string procedureName, params IDbDataParameter[] dbParams)
        {
            return Create().ProcedureDataSet(procedureName, dbParams);
        }
        static public DataTable SProcedureDataTable(string procedureName, params object[] paramValues)
        {
            return Create().ProcedureDataTable(procedureName, paramValues);
        }
        static public DataTable SProcedureDataTable(string procedureName, string[] paramNames, object[] paramValues)
        {
            return Create().ProcedureDataTable(procedureName, paramNames, paramValues);
        }
        static public DataTable SProcedureDataTable(string procedureName, params IDbDataParameter[] dbParams)
        {
            return Create().ProcedureDataTable(procedureName, dbParams);
        }

	}
	public enum DbProvider
	{
		OleDb,
		SqlClient,
        MySql,
	}
}
