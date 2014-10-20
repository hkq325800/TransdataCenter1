using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
namespace hdcweb.soc.DBAccess
{
    public class OracleDBAccess
    {
        private static OracleConnection conn;
        private static OracleConnection getConn()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString2"].ToString();
            if (conn == null)
                conn = new OracleConnection(connStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public static int ExecuteNonQuery(string cmdText)
        {

            OracleCommand cmd = getConn().CreateCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandText = cmdText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd.ExecuteNonQuery();
        }

        public static object ExecuteScalar(string cmdText)
        {
            OracleCommand cmd = getConn().CreateCommand();
            try
            {
                
                cmd.CommandText = cmdText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return cmd.ExecuteScalar();/////////
        }

        public static OracleDataReader ExecuteReader(string cmdText)
        {
            OracleCommand cmd = getConn().CreateCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandText = cmdText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd.ExecuteReader();
        }

        public static DataTable Select(string cmdText)
        {
            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}
            OracleDataAdapter da = new OracleDataAdapter(cmdText, getConn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
