using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace Jeelu.Data
{
    internal class OleDbDataAccess : DataAccess
    {
        private OleDbConnection _conn = new OleDbConnection();
        private OleDbCommand _cmd = new OleDbCommand();
        private OleDbDataAdapter _adp = new OleDbDataAdapter();
        private OleDbTransaction _tran;

        private int connCount;
        private int tranCount;

        public OleDbDataAccess(string connectString)
        {
            _conn.ConnectionString = ConnectionString;
            _cmd.Connection = _conn;
            _adp.SelectCommand = _cmd;
        }
        public override IDbTransaction Transaction
        {
            get
            {
                return _tran;
            }
        }

        override public void OpenConnect()
        {
            connCount++;
            if (connCount == 1)
            {
                _conn.Open();
            }
        }
        override public void CloseConnect()
        {
            connCount--;
            if (connCount <= 0)
            {
                connCount = 0;
                _conn.Close();
            }
        }

        override public void BeginTran()
        {
            tranCount++;
            if (tranCount == 1)
            {
                OpenConnect();
                _tran = _conn.BeginTransaction();
                _cmd.Transaction = _tran;
            }
        }
        override public void CommitTran()
        {
            tranCount--;
            if (tranCount <= 0)
            {
                tranCount = 0;
                _tran.Commit();
                CloseConnect();
            }
        }
        override public void RollbackTran()
        {
            tranCount--;
            if (tranCount <= 0)
            {
                tranCount = 0;
                _tran.Rollback();
                CloseConnect();
            }
        }

        override public int ExecuteNonQuery(string strCmd, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = strCmd;
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddRange(paramValues);
                _lastSQL = _cmd.CommandText;
                return _cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnect();
            }
        }
        override public object ExecuteScalar(string strCmd, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = strCmd;
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddRange(paramValues);
                _lastSQL = _cmd.CommandText;
                return _cmd.ExecuteScalar();
            }
            finally
            {
                CloseConnect();
            }
        }
        override public IDataReader ExecuteReader(string strCmd, params object[] paramValues)
        {
            _cmd.CommandText = strCmd;
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddRange(paramValues);
            _lastSQL = _cmd.CommandText;
            return _cmd.ExecuteReader();
        }
        override public DataSet ExecuteDataSet(string strCmd, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = strCmd;
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddRange(paramValues);
                _lastSQL = _cmd.CommandText;
                DataSet ds = new DataSet();
                _adp.Fill(ds);
                return ds;
            }
            finally
            {
                CloseConnect();
            }
        }
        override public DataTable ExecuteDataTable(string strCmd, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = strCmd;
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddRange(paramValues);
                _lastSQL = _cmd.CommandText;
                DataTable dt = new DataTable();
                _adp.Fill(dt);
                return dt;
            }
            finally
            {
                CloseConnect();
            }
        }
        public override int ExecuteMUpdate(string sql, DataSet dataSet, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                return _adp.Update(dataSet);
            }
            finally
            {
                CloseConnect();
            }
        }

        public override void ProcedureNonQuery(string procedureName, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(paramValues[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.ExecuteNonQuery();
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override void ProcedureNonQuery(string procedureName, string[] paramNames, object[] paramValues)
        {
            //检查参数有效性
            if (paramNames != null || paramValues != null)
            {
                if ((paramNames == null && paramValues != null) || (paramNames != null && paramValues == null))
                {
                    throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
                }
            }
            else if (paramNames.Length != paramValues.Length)
            {
                throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
            }

            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(new OleDbParameter(paramNames[i], paramValues[i]));
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.ExecuteNonQuery();
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override void ProcedureNonQuery(string procedureName, params IDbDataParameter[] dbParams)
        {
            if (dbParams.Length != 0 && !(dbParams[0] is OleDbParameter))
            {
                throw new ArgumentException("类型不匹配", "dbParams");
            }
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < dbParams.Length; i++)
                {
                    _cmd.Parameters.Add((OleDbParameter)dbParams[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.ExecuteNonQuery();
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override object ProcedureScalar(string procedureName, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                OleDbParameter returnParam = new OleDbParameter();
                returnParam.Direction = ParameterDirection.ReturnValue;
                _cmd.Parameters.Add(returnParam);
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(paramValues[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.ExecuteNonQuery();
                return returnParam.Value;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override object ProcedureScalar(string procedureName, string[] paramNames, object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                OleDbParameter returnParam = new OleDbParameter();
                returnParam.Direction = ParameterDirection.ReturnValue;
                _cmd.Parameters.Add(returnParam);
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(new OleDbParameter(paramNames[i], paramValues[i]));
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.ExecuteNonQuery();
                return returnParam.Value;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }

        public override DataSet ProcedureDataSet(string procedureName, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                OleDbParameter returnParam = new OleDbParameter();
                returnParam.Direction = ParameterDirection.ReturnValue;
                _cmd.Parameters.Add(returnParam);
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(paramValues[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                _adp.Fill(ds);
                return ds;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override DataSet ProcedureDataSet(string procedureName, string[] paramNames, object[] paramValues)
        {
            //检查参数有效性
            if (paramNames != null || paramValues != null)
            {
                if ((paramNames == null && paramValues != null) || (paramNames != null && paramValues == null))
                {
                    throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
                }
            }
            else if (paramNames.Length != paramValues.Length)
            {
                throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
            }

            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(new OleDbParameter(paramNames[i], paramValues[i]));
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                _adp.Fill(ds);
                return ds;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override DataSet ProcedureDataSet(string procedureName, params IDbDataParameter[] dbParams)
        {
            if (dbParams.Length != 0 && !(dbParams[0] is OleDbParameter))
            {
                throw new ArgumentException("类型不匹配", "dbParams");
            }
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < dbParams.Length; i++)
                {
                    _cmd.Parameters.Add(dbParams[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                _adp.Fill(ds);
                return ds;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override DataTable ProcedureDataTable(string procedureName, params object[] paramValues)
        {
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                OleDbParameter returnParam = new OleDbParameter();
                returnParam.Direction = ParameterDirection.ReturnValue;
                _cmd.Parameters.Add(returnParam);
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(paramValues[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                _adp.Fill(dt);
                return dt;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override DataTable ProcedureDataTable(string procedureName, string[] paramNames, object[] paramValues)
        {
            //检查参数有效性
            if (paramNames != null || paramValues != null)
            {
                if ((paramNames == null && paramValues != null) || (paramNames != null && paramValues == null))
                {
                    throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
                }
            }
            else if (paramNames.Length != paramValues.Length)
            {
                throw new ArgumentOutOfRangeException("paramNames", "paramsNames与paramsValues个数不一致");
            }

            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < paramValues.Length; i++)
                {
                    _cmd.Parameters.Add(new OleDbParameter(paramNames[i], paramValues[i]));
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                _adp.Fill(dt);
                return dt;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }
        public override DataTable ProcedureDataTable(string procedureName, params IDbDataParameter[] dbParams)
        {
            if (dbParams.Length != 0 && !(dbParams[0] is OleDbParameter))
            {
                throw new ArgumentException("类型不匹配", "dbParams");
            }
            try
            {
                OpenConnect();
                _cmd.CommandText = procedureName;
                for (int i = 0; i < dbParams.Length; i++)
                {
                    _cmd.Parameters.Add(dbParams[i]);
                }
                _cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                _adp.Fill(dt);
                return dt;
            }
            finally
            {
                _cmd.CommandType = CommandType.Text;
                _cmd.Parameters.Clear();
                CloseConnect();
            }
        }

        public override int ExecuteUpdate(string sql, DataTable table, params object[] paramValues)
        {
            throw new NotImplementedException();
        }
    }
}
