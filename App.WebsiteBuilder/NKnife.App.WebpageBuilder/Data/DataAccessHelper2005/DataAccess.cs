using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;
using System.Threading;

namespace Jeelu.Data
{
	/// <summary>
	/// ���ݷ�����
	/// </summary>
	abstract public class DataAccess
	{
		static private string _connectionString;
		static private DbProvider _provider;
		/// <summary>
		/// ��ȡ�����ַ���
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
		/// ��ȡ���ݷ����ṩ��(OleDb or Sql)
		/// </summary>
		static public DbProvider Provider
		{
			get
			{
				return _provider;
			}
		}

		#region �첽ִ��sql
		static private Queue _queue;
		static private object SyncQueue = new object();
		/// <summary>
		/// ��һ��sql�����ӵ��������������
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
		/// ���������乹�췽��
		/// </summary>
		protected DataAccess()
		{
		}

		/// <summary>
		/// ����DataAccess����
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
					throw new Exception("δ��ʼ�����ʼ��ʧ�ܡ�");
			}
		}

		/// <summary>
        /// ��ʼ������
		/// </summary>
		/// <param name="provider">����</param>
		/// <param name="dataSource">������(ip)</param>
		/// <param name="initialCatalog">���ݿ���</param>
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
					throw new Exception("δԤ�ϵ�DbProvider����ֵ");
			}
			_connectionString = string.Format(_connectionString,user,password,initialCatalog,dataSource);
		}
		/// <summary>
		/// ��ʼ������[���Ƽ�ʹ��]
		/// </summary>
		/// <param name="connectionString"></param>
		static public void Initialize(DbProvider provider,string connectionString)
		{
			_provider = provider;
			
			_connectionString = connectionString;
		}

		abstract public IDbTransaction Transaction{get;}
		/// <summary>
		/// �����ݿ����ӣ�������ݿ��Ѿ��򿪣������Ӽ�����1
		/// </summary>
		abstract public void OpenConnect();
		/// <summary>
		/// �����Ӽ�����1��������Ӽ���Ϊ0��ر�����
		/// </summary>
		abstract public void CloseConnect();

		/// <summary>
		/// ��������(�ڲ������OpenConnect)
		/// </summary>
		abstract public void BeginTran();
		/// <summary>
		/// �ύ����(�ڲ������CloseConnect)
		/// </summary>
		abstract public void CommitTran();
		/// <summary>
		/// �ع�����(�ڲ������CloseConnect)
		/// </summary>
		abstract public void RollbackTran();

		/// <summary>
		/// ִ��ָ���� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public int ExecuteNonQuery(string strCmd, params object[] paramValues);
		/// <summary>
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л��С�
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public object ExecuteScalar(string strCmd, params object[] paramValues);
		/// <summary>
		/// ִ�в�ѯ������һ��DataReader��������(�˺����ڲ��������OpenConnect��CloseConnect)
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public IDataReader ExecuteReader(string strCmd, params object[] paramValues);
		/// <summary>
		/// ִ�в�ѯ��������һ��DataSet����
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        abstract public DataSet ExecuteDataSet(string strCmd, params object[] paramValues);
		/// <summary>
		/// ִ�в�ѯ��������һ��DataTable����
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
		/// ִ��ָ���� Transact-SQL ��䲢������Ӱ���������
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public int SExecuteNonQuery(string strCmd, params object[] paramValues)
		{
            return Create().ExecuteNonQuery(strCmd, paramValues);
		}
		/// <summary>
		/// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л��С�
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public object SExecuteScalar(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteScalar(strCmd, paramValues);
		}
		/// <summary>
		/// ִ�в�ѯ��������һ��DataSet����
		/// </summary>
		/// <param name="strCmd"></param>
		/// <returns></returns>
        static public DataSet SExecuteDataSet(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteDataSet(strCmd, paramValues);
		}
		/// <summary>
		/// ִ�в�ѯ��������һ��DataTable����
		/// </summary>
        static public DataTable SExecuteDataTable(string strCmd, params object[] paramValues)
        {
            return Create().ExecuteDataTable(strCmd, paramValues);
		}

        /// <summary>
        /// ִ�и��£������ظ��µ�����
        /// </summary>
        static public int SExecuteMUpdate(string sql, DataSet dataSet, params object[] paramValues)
        {
            return Create().ExecuteMUpdate(sql, dataSet, paramValues);
        }

        /// <summary>
        /// ִ�и��£������ظ��µ�ID
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
