using System;
using System.Data;
using System.Data.SqlClient;

namespace NKnife.TSQL
{
    public class DataExecute
    {
        /// <summary>
        /// �������ݿ�����
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

        //����DataSet
        public static DataSet ExecuteDataSet(string cmdText,string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //����һ��SqlCommand���󣬲�������г�ʼ��
                SqlCommand cmd = new SqlCommand();
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //����SqlDataAdapter�����Լ�DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                //����ds
                return ds;
            }
        }

        //����DataTable
        public static DataTable ExecuteDataTable(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //����һ��SqlCommand���󣬲�������г�ʼ��
                SqlCommand cmd = new SqlCommand();
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //����SqlDataAdapter�����Լ�DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                //����ds
                return dt;
            }
        }

        //����DataReader
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
        /// ִ��SQLָ��޷��ز�ѯ���
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        /// 
        public static int ExecuteNonQuery(string cmdText, string connStr)
        {
            using (SqlConnection cn = new SqlConnection(connStr))
            {
                cn.Open();

                //����һ��SqlCommand���󣬲�������г�ʼ��
                SqlCommand cmd = new SqlCommand();
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //ִ��
                int var = cmd.ExecuteNonQuery();

                //����var
                return var;
            }
        }


        /// <summary>
        /// ִ��SQLָ��ص�����ѯ���
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
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //ִ��
                object val = cmd.ExecuteScalar();

                //if (Convert.IsDBNull(val))
                //{
                //    return 0;
                //}
                //else
                //{
                    //����var
                    return val;
                //}
            }
        }
    }
}
