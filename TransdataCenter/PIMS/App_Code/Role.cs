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

/// <summary>
///Role 的摘要说明
/// </summary>
public class Role
{
    public string ROLENAME, OPTNAME, REMARK;
    public int ROLEID, SYSID, B_STATUS, ROLETYPE;
    public Role(int ROLEID)
    {
        DataTable dt = DBFactory.GetObject("OraConnString").GetData(@"
select * from GP_ROLE t where t.ROLEID = :0", ROLEID).Tables[0];
        if (dt.Rows.Count == 0)
        {
            this.ROLEID = -1;
        }
        else
        {
            this.ROLEID = ROLEID;
            ROLENAME = dt.Rows[0]["ROLENAME"].ToString();
            OPTNAME = dt.Rows[0]["OPTNAME"].ToString();
            REMARK = dt.Rows[0]["REMARK"].ToString();
            SYSID = Convert.ToInt32(dt.Rows[0]["SYSID"]);
            B_STATUS = Convert.ToInt32(dt.Rows[0]["B_STATUS"]);
            ROLETYPE = Convert.ToInt32(dt.Rows[0]["ROLETYPE"]);
        }
    }

    public static int AddRole(string ROLENAME, int SYSID, int B_STATUS, int ROLETYPE, string REMARK, Employee OptInfo)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("添加角色信息 {0}", ROLENAME));
        try
        {
            return DBFactory.GetObject("OraConnString").ExecuteNonQuery(@"
insert into GP_ROLE
       (ROLEID,
       ROLENAME,
       SYSID,
       OPTNAME,
       MODIFYTIME,
       B_STATUS,
       ROLETYPE,
       REMARK)
values
       (SEQ_GP_Role.Nextval, :0, :1, :2, SysDate, :3, :4, :5)",
           ROLENAME, SYSID, OptInfo.EMPNAME, B_STATUS, ROLETYPE, REMARK);
        }
        catch
        {
            SystemLogs.Add("权限管理", 0,  OptInfo.EMPID, OptInfo.EMPNAME, string.Format("添加角色信息 {0} 失败", ROLENAME));
            return 0;
        }
    }

    public static int UpdateRole(int ROLEID, string ROLENAME, int B_STATUS, int ROLETYPE, string REMARK, Employee OptInfo)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("更新角色信息 {0}", ROLENAME));
        try
        {
            return DBFactory.GetObject("OraConnString").ExecuteNonQuery(@"
update GP_ROLE set 
       ROLENAME=:1,
       OPTNAME=:2,
       MODIFYTIME=Sysdate,
       B_STATUS=:3,
       ROLETYPE=:4,
       REMARK=:5
       where ROLEID=:0",
           ROLEID, ROLENAME, OptInfo.EMPNAME, B_STATUS, ROLETYPE, REMARK);
        }
        catch
        {
            SystemLogs.Add("权限管理", 0, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("添加角色信息 {0} 失败", ROLENAME));
            return 0;
        }
    }
}
