using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Web.UI;

public class CommBLLUtil
{
    OracleHelper oh = DBFactory.GetObject("OraConnString");
    /// <summary>
    /// 得到部门下运营线路的数据
    /// </summary>
    /// <param name="depID">部门ID</param>
    /// <returns>运行线路数据表</returns>
    public static DataTable GetDepRoute(string depID,Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from gp_routenum");
        strSql.Append(" where DepID=:DepID");
        OracleParameter[] parameters = {
					new OracleParameter(":DepID", OracleType.VarChar,2)
                                           };
        parameters[0].Value = depID;

        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), parameters).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetDepRout方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 得到单位下运营线路的数据
    /// </summary>
    /// <param name="unitNUM">单位编号</param>
    /// <returns>运行线路数据表</returns>
    public static DataTable GetUnitRoute(Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from g_routenum");
        // strSql.Append(" where RouteNum like ");
        //strSql.Append("concat(:UnitNum,'%')");
        //strSql.Append("+'%'");
        // OracleParameter[] parameters = {
        //		new OracleParameter(":UnitNum", OracleType.VarChar,2)
        //                   };
        //   parameters[0].Value = unitNUM;

        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetUnitRoute方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 得到分公司下面所有车队
    /// </summary>
    /// <param name="unitNUM">分公司编号在加油日报表中的编号</param>
    /// <returns>车队数据表</returns>
    public static DataTable GetTeamRoute(string unitNUM, Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from g_depinfo");
        strSql.Append(" where parentid=:UnitNum");
        //strSql.Append("and SymBol='4'");
        OracleParameter[] parameters = {
					new OracleParameter(":UnitNum", OracleType.VarChar,4)
                                           };
        parameters[0].Value = unitNUM;
        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), parameters).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetTeamRoute方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 取得所有有车的分公司
    /// </summary>
    /// <returns></returns>
    public static DataTable GetBusCompany(Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
       // strSql.Append("select count(*), t2.depname, t2.depid from g_businfo t1, G_DEPINFO t2 where t1.ownerdep = t2.depid and t2.deptype=1 group by t2.depname, t2.depid ");
        strSql.Append("select * from G_DEPINFO t where t.DEPTYPE=1");
        //strSql.Append(" where ParentNum=:UnitNum");
        //strSql.Append("and SymBol='4'");
        //  OracleParameter[] parameters = {
        //	new OracleParameter(":UnitNum", OracleType.VarChar,4)
        //  };
        //  parameters[0].Value = unitNUM;
        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetTeamRoute方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 得到集团公司下面所有分公司
    /// </summary>
    /// <param name="unitNUM">分公司编号在加油日报表中的编号</param>
    /// <returns>分给你公司数据表</returns>
    public static DataTable GetGroupUnitRoute(string unitNUM, Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from gp_unitnum");
        strSql.Append(" where ParentNum=:UnitNum");
        strSql.Append("and SymBol='1'");
        OracleParameter[] parameters = {
					new OracleParameter(":UnitNum", OracleType.VarChar,4)
                                           };
        parameters[0].Value = unitNUM;
        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), parameters).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1,Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetGroupUnitRoute方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 得到自行车所属区域信息
    /// </summary>
    /// <returns>区域信息表</returns>
    public static DataTable GetAreaInfo(Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from CB_AREAINFO");
        try
        {
            return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetAreaInfo方法出错：" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// 得到AreaId下的所有网点信息
    /// </summary>
    /// <param name="AreaId"></param>
    /// <returns></returns>
    public static DataTable GetStationInfo(string AreaId, Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from CB_STATIONINFO");
        // strSql.Append("select * from v_gh_bicycle_daily ");
        //strSql.Append("(select beloareanum, t1.NETNAME, t1.NETNUM from GH_BICYCLE_DAILY t1 group by t1.NETNAME, t1.NETNUM ,beloareanum)");
        strSql.Append(" where areaid=:AreaId");
        OracleParameter[] parameters = {
					new OracleParameter(":AreaId", OracleType.VarChar,16)
                                           };
        parameters[0].Value = AreaId;
        try
        {

            return oh.GetData(CommandType.Text, strSql.ToString(), parameters).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetDepRout方法出错：" + ex.Message);
            return null;
        }
    }

    public static DataTable GetAnjianInfo(Page page)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from PI_GROUP t where t.TYPEID=0");
        try
        {

            return oh.GetData(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "通用业务处理", 1, Employee.GetSessionEmp(page).EMPID, Employee.GetSessionEmp(page).EMPNAME, "GetAnjianInfo方法出错：" + ex.Message);
            return null;
        }
    }

}

