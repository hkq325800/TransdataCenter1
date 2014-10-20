using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public abstract class SystemInfo
{
    public static bool CheckPurview(Page page, string moduleID, bool canTransfer)
    {
        ModuleInfo[] MODULES = Employee.GetSessionEmp(page).MODULES;
        foreach (ModuleInfo mi in MODULES)
            if (mi.moduleID == moduleID) return true;
        if (canTransfer)
            page.Server.Transfer("~/default.aspx");
        return false;
    }
}
/// <summary>
///SystemLogs 的摘要说明
/// </summary>
public abstract class SystemLogs
{
    public static int AddSystemLog(string staIP, string sysName, int logType, string optID, string optName, string content)
    {
        return DBFactory.GetObject("OraConnString").ExecuteNonQuery(@"
insert into G_LOG
(LOGID, IP, SYSNAME, LOGTYPE, LOGTIME, EMPID, EMPNAME, CONTENT)
values
(SEQ_G_LOG.nextval, :0, :1, :2, sysdate, :3, :4, :5)", staIP, sysName, logType, optID, optName, content);
    }

    public static int Add(Page page, string sysName, int logType, string content)
    {
        Employee emp = Employee.GetSessionEmp(page);
        if (emp != null)
        {
            return AddSystemLog(page.Request.UserHostAddress.ToString(), sysName, logType, emp.EMPID, emp.EMPNAME, content);
        }
        return 0;
    }
     
    public static int Add(string sysName, int logType, string optID, string optName, string content)
    {
        return AddSystemLog(Utility.GetIPAddress(), sysName, logType, optID, optName, content);
    }

    public static DataSet GetList(string BeginDate, string EndDate)
    {
        OracleHelper oh = DBFactory.GetObject("OraConnString");
        return oh.GetData(@"
select t1.IP as IP地址,
       t1.SYSNAME as 系统名称,
       t1.EMPID as 操作员编号,
       t1.EMPNAME as 操作员姓名,
       t1.CONTENT as 操作内容,
       t1.LOGTIME as 操作时间
  from G_LOG t1
 where trunc(t1.LOGTIME) between to_date(:0, 'yyyy-mm-dd') and
       to_date(:1, 'yyyy-mm-dd')
 order by t1.LOGTIME desc", BeginDate, EndDate);
    }
}
