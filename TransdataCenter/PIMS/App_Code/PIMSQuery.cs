using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.UI;

public class PIMSQuery
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");

    /// <summary>
    /// 根据传入的查询条件，返回满足条件的车辆安检数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSCheckDataByWhereStr(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select t1.BUSUNITNAME,t1.BUSCHECKNUMBER,t2.BUSSELFNO,t2.CHECKRESULT");
        strSql.Append(" from GH_PI_BUSCHECK_DAILY t1 left join GH_PI_BUSCHECK_DAILY_DETAIL t2 ");
        strSql.Append("on t2.BUSCHECKDAILYID = t1.BUSCHECKDAILYID");
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append(" order by t1.busunit");
            try
            {

                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询系统安检数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的司乘人员进出数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSDriverInOutDataByWhereStr(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append(@"select t1.BUSCLASS,
       t1.BUSSELFNO,
       t1.EMPNAME,
       t1.OUTTIME,
       t1.REGISTERTIME
  from GH_PI_EMPREGISTER_DAILY t1");
        
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
       
            try
            {

                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询系统司乘人员进出数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的泊位数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSParkDataByWhereStr(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select t1.EMPNAME,t1.AREANO,t1.PARKNUMBER,t2.BUSSELFNO");
        strSql.Append(" from GH_PI_PARK_DAILY t1 left join GH_PI_PARK_DAILY_DETAIL t2 ");
        strSql.Append("on t2.PARKDAILYID = t1.PARKDAILYID");

        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append("  order by t1.EMPID,t1.AREANO");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询系统泊位数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的车辆进出场数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSParkInOutDataByWhereSelStr(string strSel,string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        strSql.Append(strSel);
        strSql.Append(" from GH_PI_PARKINOUTSTAT_DAILY t2 left join GH_PI_PARKINOUTDETAIL_DAILY t1 ");
        strSql.Append("on t2.INOUTSTATDAILYID = t1.INOUTSTATDAILYID");

        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append("  order by t2.DEPTID");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆进出场数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的车辆延误出场数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSParkOutDelaySelStr(string strSel, string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        strSql.Append(strSel);
        strSql.Append(" from  GH_PI_PARKINOUTDETAIL_DAILY t ");
       

        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            // strSql.Append("  order by t1.EMPID,t1.AREANO");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
 
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的车辆收银数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSCollectDataByWhereSelStr(string strSel, string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        strSql.Append(strSel);
       // strSql.Append(" from GH_PI_COLLECTSTAT_DAILY t1 left join GH_PI_COLLECTSTATDETAIL_DAILY t2 ");
        //strSql.Append("on t2.COLLECTSTATDAILYID = t1.COLLECTSTATDAILYID");

        strSql.Append(" from GH_PI_COLLECTSTAT_DAILY t1  ");
      

        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append("  order by t1.busunit");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    /// <summary>
    /// 根据传入的查询条件，返回满足条件的收银员收银数据
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的记录集</returns>
    public DataSet GetPIMSCollecterDataByWhereSelStr(string strSel, string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select ");
        strSql.Append(strSel);
        strSql.Append(" from GH_PI_COLLECTDETAIL_DAILY t1 ");
       

        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
          
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                //SystemLogs.AddSystemLog(Utility.GetIPAddress(), 0, 1, 0, "BicycleQuery类", "GetBicycleDetailByWhereStr方法出错：" + ex.Message);

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }

   /// <summary>
   /// 获取从本月第一天开始的累积收银数
   /// </summary>
   /// <returns></returns>
    public DataSet GetPIMSCollectSumNum(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select sum(t.bagnumber),t.empname from gh_pi_collectdetail_daily t ");
     


        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append(" group by t.empname");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
               
                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;

      //  return oh.GetData(@"select sum(t.bagnumber),t.empname from gh_pi_collectdetail_daily t where t.reportdate between trunc(add_months(last_day(sysdate), -1) + 1) and sysdate group by t.empname");
            
          
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataSet GetPIMSCollectMonthNum(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select t.reportdate,t.empname,sum(t.bagnumber) as sumbagnum from gh_pi_collectdetail_daily t ");



        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            strSql.Append(" group by t.reportdate,t.empname order by t.reportdate");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;

        //  return oh.GetData(@"select sum(t.bagnumber),t.empname from gh_pi_collectdetail_daily t where t.reportdate between trunc(add_months(last_day(sysdate), -1) + 1) and sysdate group by t.empname");


    }
    /// <summary>
    /// 获取当月收银人员。
    /// </summary>
    /// <returns></returns>
    public DataSet GetPIMSCollecter(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select distinct t.empname from gh_pi_collectdetail_daily t  ");



        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
           
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {

                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场查询车辆收银数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;

        //  return oh.GetData(@"select sum(t.bagnumber),t.empname from gh_pi_collectdetail_daily t where t.reportdate between trunc(add_months(last_day(sysdate), -1) + 1) and sysdate group by t.empname");


    }
    public DataTable GetGroup(string strWhere,Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select t.groupid,t.groupname from pi_group t");
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            try
            {

                return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
            }
            catch (Exception ex)
            {
                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场分组数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    public DataTable GetGroupMenber(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select t.groupid,t.empid,t1.empname from pi_usergroup t left join pi_employeeinfo t1 on t.empid=t1.empid");
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            try
            {

                return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
            }
            catch (Exception ex)
            {
                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场分组组内成员", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }
    public DataTable GetBusUnit(string strWhere, Page page)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select distinct deptid, deptname from  GH_PI_PARKINOUTSTAT_DAILY ");
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(" where 0=0 " + strWhere);
            try
            {

                return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
            }
            catch (Exception ex)
            {
                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "停车场分组数据", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GH_BICYCLE_DAILY方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }

}