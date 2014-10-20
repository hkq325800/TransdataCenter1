using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.Sql;
using System.Data.SqlClient;
//todo 日志记录
public class PurviewQuery
{
    OracleHelper dbAccess = DBFactory.GetObject();

    public DataSet GetAppList()
    {
        return dbAccess.GetData("select t.SYSNAME, t.SYSID, t.SYSADDRESS from GP_APPLIST t order by t.SYSID");
    }

    public DataSet GetRoleList(string sysCode)
    {
        return dbAccess.GetData(CommandType.Text, @"
select t.ROLENAME, t.ROLEID, t.B_STATUS
  from GP_ROLE t
 where t.RoleID <> 0 and t.SYSID = :SysCode", new OracleParameter("SysCode", sysCode));
    }

    public DataSet GetModuleList(string sysCode)
    {
        return dbAccess.GetData(CommandType.Text, @"
select t.MODULENAME, t.MODULEID, t.FATHERID , t.REMARK
  from GP_MODULELIST t
 where t.SYSID = :SYSCODE", new OracleParameter("SYSCODE", sysCode));
    }

    public DataSet GetRoleModuleList(string roleID)
    {
        return dbAccess.GetData(CommandType.Text, @"
select t.MODULEID
  from GP_ROLEMODULE t
 where t.ROLEID = :ROLEID", new OracleParameter("ROLEID", roleID));
    }

    public int UpdateRoleModuleList(string roleID, Employee OptInfo, List<string> moduleIDList)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, "更新角色 " + roleID + " 的权限");
        try
        {
            dbAccess.ExecuteNonQuery(CommandType.Text, "delete from GP_ROLEMODULE where RoleID =:RoleID", new OracleParameter("RoleID", roleID));
            foreach (string moduleID in moduleIDList)
            {
                dbAccess.ExecuteNonQuery(CommandType.Text, @"
insert into GP_ROLEMODULE
       (RoleID, MODULEID, OPTNAME, MODIFYTIME)
values
       (:RoleID, :MODULEID, :OPTNAME, sysdate)",
                new OracleParameter("RoleID", roleID),
                new OracleParameter("MODULEID", moduleID),
                new OracleParameter("OPTNAME", OptInfo.EMPNAME));
            }
            return 1;
        }
        catch
        {
            SystemLogs.Add("权限管理", 0, OptInfo.EMPID, OptInfo.EMPNAME, "更新角色 " + roleID + " 的权限失败");
            return 0;
        }
    }

    public DataSet GetRoleMemberList(string roleID)
    {
        return dbAccess.GetData(@"
select Concat(Concat(e.EMPID, ' '), e.EMPNAME), t.EMPID
  from GP_ROLEUSER t
       left join G_EMPINFO e on t.EMPID = e.EMPID
 where t.ROLEID = :0
 order by e.EMPID", roleID);
    }

    public bool ExistedMemberInRole(string EMPID, string roleID)
    {
        return dbAccess.GetData(CommandType.Text, "select 1 from GP_ROLEUSER t where t.ROLEID = :ROLEID and t.EMPID = :EMPID",
             new OracleParameter("ROLEID", roleID), new OracleParameter("EMPID", EMPID.Trim())).Tables[0].Rows.Count > 0;
    }

    public DataSet GetEmpInfo(string EMPID)
    {
        return dbAccess.GetData(@"
select t1.EMPID,
       t1.EMPNAME,
       t1.EMPSEX,
       t1.DEPID,
       t2.DEPNAME,
       t1.POSTID,
       t3.ITEMVALUE,
       t1.VALID,
       t1.PASSWORD
  from G_EMPINFO t1
       left join G_DEPINFO t2 on t1.DEPID = t2.DEPID
       left join G_DIC t3 on t3.DICID = 'POSTID' and t1.POSTID = t3.ITEMID
 where t1.EMPID = :0", EMPID);
    }

    public bool AddMemberToRole(string EMPID, string roleID, Employee OptInfo)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("增加角色成员 {0}", EMPID));
        try
        {
            return dbAccess.ExecuteNonQuery(CommandType.Text, @"
insert into GP_ROLEUSER
(ROLEID, EMPID, OPTNAME, MODIFYTIME)
values
(:ROLEID, :EMPID, :OPTNAME, sysdate)",
                new OracleParameter("ROLEID", roleID.Trim()),
                new OracleParameter("EMPID", EMPID.Trim()),
                new OracleParameter("OPTNAME", OptInfo.EMPNAME)) == 1;
        }
        catch
        {
            SystemLogs.Add("权限管理", 0, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("增加角色成员 {0} 失败", EMPID));
            return false;
        }
    }

    public bool DeleteMemberFromRole(string EMPID, string roleID, Employee OptInfo)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("删除角色成员 {0}", EMPID));
        try
        {
            return dbAccess.ExecuteNonQuery(CommandType.Text, "delete from GP_ROLEUSER where ROLEID=:ROLEID and EMPID=:EMPID",
                new OracleParameter("ROLEID", roleID.Trim()),
                new OracleParameter("EMPID", EMPID.Trim())) == 1;
        }
        catch
        {
            SystemLogs.Add("权限管理", 0, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("删除角色成员 {0} 失败", EMPID));
            return false;
        }

    }

    public DataSet GetRoleDeptList(string roleID)
    {
        return dbAccess.GetData(CommandType.Text, "select t.DEPID from GP_ROLEDEP t where t.ROLEID = :ROLEID", new OracleParameter("ROLEID", roleID));
    }

    public DataSet GetDeptList()
    {
        return dbAccess.GetData(CommandType.Text, "select t.DEPNAME, t.DEPID, t.PARENTID, t.REMARK from G_DEPINFO t order by t.DEPID", null);
    }

    public int UpdateRoleDeptList(string roleID, Employee OptInfo, List<string> deptIDList)
    {
        SystemLogs.Add("权限管理", 3, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("更新 {0} 角色可管理部门信息 {0}", roleID));
        try
        {
            dbAccess.ExecuteNonQuery(CommandType.Text, "delete from GP_ROLEDEP where RoleID =:RoleID", new OracleParameter("RoleID", roleID));
            foreach (string deptID in deptIDList)
            {
                dbAccess.ExecuteNonQuery(CommandType.Text, @"
insert into GP_ROLEDEP
       (RoleID, DEPID, OPTNAME, MODIFYTIME)
values
       (:RoleID, :DEPID, :OPTNAME, sysdate)",
                new OracleParameter("RoleID", roleID),
                new OracleParameter("DEPID", deptID),
                new OracleParameter("OPTNAME", OptInfo.EMPNAME));
            }
            return 1;
        }
        catch
        {
            SystemLogs.Add("权限管理", 0, OptInfo.EMPID, OptInfo.EMPNAME, string.Format("更新 {0} 角色可管理部门信息 {0} 失败", roleID));
            return 0;
        }
    }
}