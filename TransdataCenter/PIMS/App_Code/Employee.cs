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
using System.Collections.Generic;

/// <summary>
///Employee 的摘要说明
/// </summary>
public class Employee : ClientEmployee
{
    public Employee(ClientEmployee EMP)
    {
        EMPID = EMP.EMPID;
        EMPNAME = EMP.EMPNAME;
        EMPSEX = EMP.EMPSEX;
        DEPID = EMP.DEPID;
        DEPNAME = EMP.DEPNAME;
        POSTID = EMP.POSTID;
        POSTNAME = EMP.POSTNAME;
        ROLEIDS = EMP.ROLEIDS;
        MODULES = EMP.MODULES;
        DEPTS = EMP.DEPTS;
    }
    public static Employee GetSessionEmp(Page page)
    {
        return (Employee)page.Session["Employee"];
    }
    public static void SetSessionEmp(Page page, Employee emp)
    {
        page.Session.Clear();
        page.Session["Employee"] = emp;
    }
    public static Employee GetSessionEmp(UserControl control)
    {
        return (Employee)control.Session["Employee"];
    }
    public static void SetSessionEmp(UserControl control, Employee emp)
    {
        control.Session.Clear();
        control.Session["Employee"] = emp;
    }

    /// <summary>
    /// 得到当前员工要在中间显示的模块列表
    /// </summary>
    /// <returns>门户模块列表</returns>
    public List<WidgetList> GetShowWidgetList()
    {
        List<WidgetList> result = new List<WidgetList>();
        DataTable dt = DBFactory.GetObject().GetData(@"
select t1.MODULECODE,
       t1.MODULENAME,
       t1.CONTROLNAME,
       t1.MODULETYPE,
       t1.MODULEID,
       t1.MODULEDES,
       t2.VISIBLE
  from GP_WIDGETLIST t1 
  left join GP_WIDGETVISIBLE t2 on t1.MODULECODE = t2.MODULECODE and t2.EMPID = :0
 order by t1.MODULETYPE asc, t2.SHOWORDER desc", EMPID).Tables[0];
        foreach (DataRow dr in dt.Rows)
            if (dr[6] == DBNull.Value || dr[6].ToString() == "1")
            {
                WidgetList t = new WidgetList();
                t.MODULECODE = Convert.ToInt32(dr[0]);
                t.MODULENAME = dr[1].ToString();
                t.CONTROLNAME = dr[2].ToString();
                t.MODULETYPE = Convert.ToInt32(dr[3]);
                t.MODULEID = Convert.ToInt32(dr[4]);
                t.MODULEDES = dr[5].ToString();
                result.Add(t);
            }
        return result;
    }
}
