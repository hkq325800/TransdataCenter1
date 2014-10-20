using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;
using System.Management;
using System.Web.UI.WebControls; 

    public class Utility
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        /// <summary>
        /// 使用正则表达式来判断给定的字符串是否含有常见的SQL注入攻击字符串
        /// </summary>
        /// <param name="strSQL">待验证的SQL字符串</param>
        /// <returns>ture表示验证通过，否则验证未通过</returns>
        public static bool IsValidatedSql(string strSQL)
        {

            bool reslut = true;
            //不能含有
            if (Regex.IsMatch(strSQL, "^drop:", RegexOptions.IgnoreCase))
                reslut = false;
            if (Regex.IsMatch(strSQL, "^0=0:", RegexOptions.IgnorePatternWhitespace))
                reslut = false;
            if (Regex.IsMatch(strSQL, "^create:", RegexOptions.IgnoreCase))
                reslut = false;
            if (Regex.IsMatch(strSQL, "^alert:", RegexOptions.IgnoreCase))
                reslut = false;
            return reslut;
        }

        /// <summary>
        /// 得到给定表给定字段的最大值，然后加一，也可以使用一个存储过程来代替
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">字段名</param>
        /// <returns>最大值</returns>
        public static int GetMaxValue(string sequenceName, string colName)
        {
            OracleHelper oh = DBFactory.GetObject("OraConnString");
            string strSQL = "select "+sequenceName+".nextval as "+colName+" from dual";

            return int.Parse(oh.ExecuteScalar(CommandType.Text, strSQL, null).ToString());
        }
        /// <summary>
        /// 得到程序集运行的计算机IP地址
        /// </summary>
        /// <remarks>如果想得到Web应用程序客户端的IP地址可以直接调用：Page.Request.UserHostAddress</remarks>
        /// <returns>服务端的IP地址</returns>
        public static string GetIPAddress()
        {
            string strIP = "";
            
            ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection MOC = MC.GetInstances();
            foreach (ManagementObject MO in MOC) 
            { 
                if ((bool)MO["IPEnabled"] == true) 
                { 
                    string[] IPAddresses = (string[])MO["IPAddress"]; 
                    if (IPAddresses.Length > 0)
                        strIP = IPAddresses[0]; 
                } 
            }
            return strIP;
        }
        /// <summary>
        /// 得到给定员工可以访问数据所属部门
        /// </summary>
        /// <param name="empID">员工工号</param>
        /// <returns></returns>
        public static DataTable GetDeptRightList(int empID)
        {
            return null;
        }
    }