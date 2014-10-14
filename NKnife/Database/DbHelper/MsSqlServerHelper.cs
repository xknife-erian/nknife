using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace NKnife.Database.DbHelper
{
    public class MsSqlServerHelper
    {
        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <returns></returns>
        /// 
        public static bool TestConnection(string connStr)
        {
            try
            {
                using(var conn = new SqlConnection(connStr))
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
            using (var cn = new SqlConnection(connStr))
            {
                cn.Open();

                //����һ��SqlCommand���󣬲�������г�ʼ��
                var cmd = new SqlCommand();
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //����SqlDataAdapter�����Լ�DataSet
                var da = new SqlDataAdapter(cmd);
                var ds = new DataSet();
                da.Fill(ds);

                //����ds
                return ds;
            }
        }

        /// <summary>
        /// ����DataTable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText, string connStr)
        {
            using (var cn = new SqlConnection(connStr))
            {
                cn.Open();

                //����һ��SqlCommand���󣬲�������г�ʼ��
                var cmd = new SqlCommand();
                //cmd���Ը�ֵ
                cmd.Connection = cn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;

                //����SqlDataAdapter�����Լ�DataSet
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);

                //����ds
                return dt;
            }
        }

        //����DataReader
        public static IDataReader ExecuteDataReader(string cmdText, string connStr)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connStr);
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
        /// <param name="connStr"></param>
        /// <returns></returns>
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
        /// <param name="connStr"></param>
        /// <returns></returns>
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

        public static IEnumerable<string> GetCommands(string script)
        {
            Regex regex = new Regex(@"\r{0,1}\nGO\r{0,1}\n");
            string[] commands = regex.Split(script);
            return commands.Where(s => s.Trim().Length > 0);
        }
    }
}
