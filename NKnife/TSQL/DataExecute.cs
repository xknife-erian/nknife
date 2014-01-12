using System;
using System.Data;
using System.Data.SqlClient;

namespace NKnife.TSQL
{
    public class DataExecute
    {
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <returns></returns>
        /// 
        public static bool testConn(string connStr)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //返回DataSet
        public static DataSet ExecuteDataSet(string cmdText,string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //创建SqlDataAdapter对象以及DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //返回ds
                return ds;
            }
        }

        //返回DataTable
        public static DataTable ExecuteDataTable(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //创建SqlDataAdapter对象以及DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //返回ds
                return dt;
            }
        }

        //返回DataReader
        public static IDataReader ExecuteDataReader(string cmdText, string connStr)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception ex)
            {
                cmd.Dispose();
                conn.Close();
                return null;
            }
        }

        /// <summary>
        /// 执行SQL指令，无返回查询结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// 
        public static int ExecuteNonQuery(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //创建一个SqlCommand对象，并对其进行初始化
                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //执行
                int var = cmd.ExecuteNonQuery();

                //返回var
                return var;
            }
        }


        /// <summary>
        /// 执行SQL指令返回单个查询结果
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// 
        public static object ExecuteScalar(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                //cmd属性赋值
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //执行
                object val = cmd.ExecuteScalar();

                //if (Convert.IsDBNull(val))
                //{
                //    return 0;
                //}
                //else
                //{
                    //返回var
                    return val;
                //}
            }
        }
    }
}
