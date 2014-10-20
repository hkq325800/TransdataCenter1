using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using hdcweb.soc.DBAccess;

namespace hdcweb.soc.DAL
{
    public class webDAL
    {
        public static string selectFirstData(string sql)
        {
            try
            {
                return OracleDBAccess.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable selectDataTable(string sql)
        {
            try
            {
                return OracleDBAccess.Select(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
