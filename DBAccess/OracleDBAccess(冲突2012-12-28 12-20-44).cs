using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb; 

namespace hdcweb.soc.DBAccess
{
    public class OracleDBAccess
    {
        private static OleDbConnection conn;
        private static OleDbConnection getConn()
        {
            string connStr = "Provider=MSDAORA.1;User ID=hdcweb;Password=hdcweb;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.254.24)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = gjjt5)))";
            if (conn == null)
                conn = new OleDbConnection(connStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public static int ExecuteNonQuery(string cmdText)
        {

           OleDbCommand cmd = getConn().CreateCommand();
            try
            {
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
            OleDbCommand cmd = getConn().CreateCommand();
            try
            {
                cmd.CommandText = cmdText;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd.ExecuteScalar();
    }

        public static OleDbDataReader ExecuteReader(string cmdText)
        {
            OleDbCommand cmd = getConn().CreateCommand();
            try
            {
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
            OleDbDataAdapter da = new OleDbDataAdapter(cmdText, getConn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
