using System;
using System.Data.OleDb;
using System.Text;

namespace NKnife.Net.EMTASS
{
    /// <summary>
    /// 测试用Access数据库类
    /// </summary>
    public class DbEvent : TOleDatabaseBase
    {
        private OleDbCommand m_command;  // 自定义的字段

        /// <summary>
        /// 重写 Open 方法
        /// </summary>
        public override void Open()
        {
            base.Open();  // 打开数据库

            m_command = new OleDbCommand();
            m_command.Connection = (OleDbConnection)this.DbConnection;

            // OleDbCommand 不能像 SqlCommand 在 CommandText 使用参数名称
            m_command.CommandText = "insert into DatagramTextTable(SessionIP, SessionName, DatagramSize) values (?, ?, ?)";

            m_command.Parameters.Add(new OleDbParameter("SessionIP", OleDbType.VarChar));
            m_command.Parameters.Add(new OleDbParameter("SessionName", OleDbType.VarChar));
            m_command.Parameters.Add(new OleDbParameter("DatagramSize", OleDbType.Integer));
        }

        /// <summary>
        /// 自定义数据存储方法
        /// </summary>
        public override void Store(byte[] datagramBytes, TSessionBase session)
        {
            string datagramText = Encoding.Default.GetString(datagramBytes);
            try
            {
                m_command.Parameters["SessionIP"].Value = session.IP;
                m_command.Parameters["SessionName"].Value = session.Name;
                m_command.Parameters["DatagramSize"].Value = datagramBytes.Length;

                m_command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                this.OnDatabaseException(err);
            }
        }
    }
}
