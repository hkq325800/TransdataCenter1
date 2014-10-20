using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

public class WidgetList
{
    OracleHelper oh = DBFactory.GetObject("OraConnString");
    #region 属性
    private int _modulecode;
    private string _modulename;
    private string _controlname;
    private int _moduletype;
    private int _moduleid;
    private string _moduledes;
    private int _optid;
    private DateTime _modifytime;
    private string _remark;
    /// <summary>
    /// 模块编号,最大值加一
    /// </summary>
    public int MODULECODE
    {
        set { _modulecode = value; }
        get { return _modulecode; }
    }
    /// <summary>
    /// 模块名称
    /// </summary>
    public string MODULENAME
    {
        set { _modulename = value; }
        get { return _modulename; }
    }
    /// <summary>
    /// 控件名称,用户自定义控件所在的路径及名称
    /// </summary>
    public string CONTROLNAME
    {
        set { _controlname = value; }
        get { return _controlname; }
    }
    /// <summary>
    /// 模块类型,1表示公用，个人不能维护，由系统开发是确定；2表示个人可设置。
    /// </summary>
    public int MODULETYPE
    {
        set { _moduletype = value; }
        get { return _moduletype; }
    }
    /// <summary>
    /// 对应功能,对应的功能ID，功能列表（功能ID），没有对应的使用缺省值。有对应功能编号的在个人设置时，如果对功能没有权限则不能设置。
    /// </summary>
    public int MODULEID
    {
        set { _moduleid = value; }
        get { return _moduleid; }
    }
    /// <summary>
    /// 模块说明
    /// </summary>
    public string MODULEDES
    {
        set { _moduledes = value; }
        get { return _moduledes; }
    }
    /// <summary>
    /// 操作员ID,最后操作该记录的操作人员，员工信息表 (员工ID)
    /// </summary>
    public int OPTID
    {
        set { _optid = value; }
        get { return _optid; }
    }
    /// <summary>
    /// 只读属性，操作时间,最后操作该记录的时间
    /// </summary>
    public DateTime MODIFYTIME
    {
        get { return _modifytime; }
    }
    /// <summary>
    /// 备注
    /// </summary>
    public string REMARK
    {
        set { _remark = value; }
        get { return _remark; }
    }
    #endregion

    #region 构造函数
    /// <summary>
    /// 构造一个空实体
    /// </summary>
    public WidgetList()
    {
        this._modulecode = -1;
    }
    /// <summary>
    /// 构造一个对象实体
    /// </summary>
    /// <param name="moduleCode">模块编号</param>
    public WidgetList(int moduleCode)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select MODULECODE,MODULENAME,CONTROLNAME,MODULETYPE,MODULEID,MODULEDES,OPTID,MODIFYTIME,REMARK ");
        strSql.Append(" FROM GP_WIDGETLIST ");
        strSql.Append(" where MODULECODE=:MODULECODE ");
        OracleParameter[] parameters = {
					new OracleParameter(":MODULECODE", OracleType.Number,4)};
        parameters[0].Value = moduleCode;
        try
        {
            DataSet ds = oh.GetData(CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MODULECODE"].ToString() != "")
                {
                    MODULECODE = int.Parse(ds.Tables[0].Rows[0]["MODULECODE"].ToString());
                }
                MODULENAME = ds.Tables[0].Rows[0]["MODULENAME"].ToString();
                CONTROLNAME = ds.Tables[0].Rows[0]["CONTROLNAME"].ToString();
                if (ds.Tables[0].Rows[0]["MODULETYPE"].ToString() != "")
                {
                    MODULETYPE = int.Parse(ds.Tables[0].Rows[0]["MODULETYPE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MODULEID"].ToString() != "")
                {
                    MODULEID = int.Parse(ds.Tables[0].Rows[0]["MODULEID"].ToString());
                }
                MODULEDES = ds.Tables[0].Rows[0]["MODULEDES"].ToString();
                if (ds.Tables[0].Rows[0]["OPTID"].ToString() != "")
                {
                    OPTID = int.Parse(ds.Tables[0].Rows[0]["OPTID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MODIFYTIME"].ToString() != "")
                {
                    this._modifytime = DateTime.Parse(ds.Tables[0].Rows[0]["MODIFYTIME"].ToString());
                }
                REMARK = ds.Tables[0].Rows[0]["REMARK"].ToString();
            }
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "构造出错：" + ex.Message);
            this._modulecode = -1;
        }
    }
    #endregion 构造函数

    /// <summary>
    /// 把数据表转化为对象列表
    /// </summary>
    /// <param name="dt">待转换的数据表</param>
    /// <returns>对象列表</returns>
    private List<WidgetList> GetListFromDataTable(DataTable dt)
    {
        List<WidgetList> tempList = new List<WidgetList>();

        foreach (DataRow dr in dt.Rows)
        {
            WidgetList temp = new WidgetList();
            temp.MODULECODE = -1; ;
            if (dr["MODULECODE"].ToString() != "")
            {
                temp.MODULECODE = int.Parse(dr["MODULECODE"].ToString());
            }
            temp.MODULENAME = dr["MODULENAME"].ToString();
            temp.CONTROLNAME = dr["CONTROLNAME"].ToString();
            if (dr["MODULETYPE"].ToString() != "")
            {
                temp.MODULETYPE = int.Parse(dr["MODULETYPE"].ToString());
            }
            if (dr["MODULEID"].ToString() != "")
            {
                temp.MODULEID = int.Parse(dr["MODULEID"].ToString());
            }
            temp.MODULEDES = dr["MODULEDES"].ToString();
            if (dr["OPTID"].ToString() != "")
            {
                temp.OPTID = int.Parse(dr["OPTID"].ToString());
            }
            if (dr["MODIFYTIME"].ToString() != "")
            {
                temp._modifytime = DateTime.Parse(dr["MODIFYTIME"].ToString());
            }
            temp.REMARK = dr["REMARK"].ToString();
            tempList.Add(temp);
        }
        if (tempList.Count == 0)
        {
            WidgetList temp = new WidgetList();
            temp._modulecode = -1; ;
            tempList.Add(temp);
        }
        return tempList;
    }

    #region 增、删、改
    /// <summary>		
    ///增加门户模块信息
    /// </summary>
    /// <returns>1，表示增加成功；否则表示增加失败，返回出错信息</returns>
    public string Add()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into GP_WIDGETLIST(");
        strSql.Append("MODULECODE,MODULENAME,CONTROLNAME,MODULETYPE,MODULEID,MODULEDES,OPTID,MODIFYTIME,REMARK)");
        strSql.Append(" values (");
        strSql.Append(":MODULECODE,:MODULENAME,:CONTROLNAME,:MODULETYPE,:MODULEID,:MODULEDES,:OPTID,SysDate,:REMARK)");
        OracleParameter[] parameters = {
					new OracleParameter(":MODULECODE", OracleType.Number,4),
					new OracleParameter(":MODULENAME", OracleType.VarChar,64),
					new OracleParameter(":CONTROLNAME", OracleType.VarChar,128),
					new OracleParameter(":MODULETYPE", OracleType.Number,4),
					new OracleParameter(":MODULEID", OracleType.Number,4),
					new OracleParameter(":MODULEDES", OracleType.VarChar,256),
					new OracleParameter(":OPTID", OracleType.Number,4),
					new OracleParameter(":REMARK", OracleType.VarChar,100)};
        parameters[0].Value = Utility.GetMaxValue("gp_WidgetList", "ModuleCode"); ;
        parameters[1].Value = MODULENAME;
        parameters[2].Value = CONTROLNAME;
        parameters[3].Value = MODULETYPE;
        parameters[4].Value = MODULEID;
        parameters[5].Value = MODULEDES;
        parameters[6].Value = OPTID;
        parameters[7].Value = REMARK;
        try
        {
            oh.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return "1";
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "Add方法出错：" + ex.Message);
            return ex.Message;
        }
    }

    /// <summary>		
    ///更新门户模块信息
    /// </summary>
    /// <returns>1，表示更新成功；否则表示更新失败，返回出错信息 </returns>
    public string Update()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update GP_WIDGETLIST set ");
        strSql.Append("MODULENAME=:MODULENAME,");
        strSql.Append("CONTROLNAME=:CONTROLNAME,");
        strSql.Append("MODULETYPE=:MODULETYPE,");
        strSql.Append("MODULEID=:MODULEID,");
        strSql.Append("MODULEDES=:MODULEDES,");
        strSql.Append("OPTID=:OPTID,");
        strSql.Append("MODIFYTIME=SysDate,");
        strSql.Append("REMARK=:REMARK");
        strSql.Append(" where MODULECODE=:MODULECODE ");
        OracleParameter[] parameters = {
					new OracleParameter(":MODULECODE", OracleType.Number,4),
					new OracleParameter(":MODULENAME", OracleType.VarChar,64),
					new OracleParameter(":CONTROLNAME", OracleType.VarChar,128),
					new OracleParameter(":MODULETYPE", OracleType.Number,4),
					new OracleParameter(":MODULEID", OracleType.Number,4),
					new OracleParameter(":MODULEDES", OracleType.VarChar,256),
					new OracleParameter(":OPTID", OracleType.Number,4),
					new OracleParameter(":REMARK", OracleType.VarChar,100)};
        parameters[0].Value = MODULECODE;
        parameters[1].Value = MODULENAME;
        parameters[2].Value = CONTROLNAME;
        parameters[3].Value = MODULETYPE;
        parameters[4].Value = MODULEID;
        parameters[5].Value = MODULEDES;
        parameters[6].Value = OPTID;
        parameters[7].Value = REMARK;

        try
        {
            oh.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return "1";
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "Update方法出错：" + ex.Message);
            return ex.Message;
        }
    }

    /// <summary>		
    ///删除一个门户模块
    /// </summary>
    /// <returns>1，表示删除成功；否则表示删除失败，返回出错信息 </returns>
    public string Delete()
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from GP_WIDGETLIST ");
        strSql.Append(" where MODULECODE=:MODULECODE ");
        OracleParameter[] parameters = {
					new OracleParameter(":MODULECODE", OracleType.Number,4)};
        parameters[0].Value = MODULECODE;
        try
        {
            oh.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return "1";
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "Delete方法出错：" + ex.Message);
            return ex.Message;
        }
    }
    #endregion

    #region 业务处理
    /// <summary>
    /// 把一个实体对象添加到数据库
    /// </summary>
    /// <param name="appList">实体对象</param>
    /// <returns>1，表示增加成功；否则表示增加失败，返回出错信息</returns>
    public string Add(WidgetList appList)
    {
        return appList.Add();
    }
    /// <summary>
    /// 把一个实体对象更新到数据库
    /// </summary>
    /// <param name="appList">实体对象</param>
    /// <returns>1，表示更新成功；否则表示更新失败，返回出错信息 </returns>
    public string Update(WidgetList appList)
    {
        return appList.Update();
    }
    /// <summary>
    /// 把一个实体对象从数据库中删除
    /// </summary>
    /// <param name="appList">实体对象</param>
    /// <returns>1，表示删除成功；否则表示删除失败，返回出错信息 </returns>
    public string Delete(WidgetList appList)
    {
        return appList.Delete();
    }
    /// <summary>
    /// 根据系统编号删除一个门户模块
    /// </summary>
    /// <param name="moduleCode">门户模块编号</param>
    /// <returns>1，表示删除成功；否则表示删除失败，返回出错信息 </returns>
    public string DeleteByCode(int moduleCode)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from GP_WIDGETLIST ");
        strSql.Append(" where MODULECODE=:MODULECODE ");
        OracleParameter[] parameters = {
					new OracleParameter(":MODULECODE", OracleType.Number,4)};
        parameters[0].Value = moduleCode;
        try
        {
            oh.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return "1";
        }
        catch (Exception ex)
        {
            SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "DeleteByCode方法出错：" + ex.Message);
            return ex.Message;
        }
    }

    /// <summary>
    /// 根据传入的查询字符串得到满足条件的门户模块数据集
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的门户模块数据集</returns>
    public DataSet GetDataByStr(string strWhere)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from gp_widgetlist t1,gp_widgetvisible t2 where t1.modulecode(+)=t2.modulecode ");
        if (Utility.IsValidatedSql(strWhere.Trim()))
        {
            strSql.Append(strWhere);
            strSql.Append(" order by ShowOrder desc");
            try
            {
                return oh.GetData(CommandType.Text, strSql.ToString(), null);
            }
            catch (Exception ex)
            {
                SystemLogs.AddSystemLog(Utility.GetIPAddress(), "数据中心门户", 1, "0", "WidgetList类", "GetAppDataByStr方法出错：" + ex.Message);
                return null;
            }
        }
        else
            return null;
    }

    /// <summary>
    /// 得到所有门户模块信息
    /// </summary>
    /// <returns>全部门户模块列表</returns>
    public DataSet GetAllData()
    {
        return GetDataByStr("");
    }

    /// <summary>
    /// 根据门户模块编号得到门户模块对象
    /// </summary>
    /// <param name="moduleCode">门户模块编号</param>
    /// <returns>门户模块对象</returns>
    public WidgetList GetObjectByID(int moduleCode)
    {
        return new WidgetList(moduleCode);
    }
    /// <summary>
    /// 根据传入的查询字符串得到满足条件的门户模块列表
    /// </summary>
    /// <param name="strWhere">查询条件</param>
    /// <returns>满足条件的门户模块列表</returns>
    public List<WidgetList> GetObjectListByStr(string strWhere)
    {
        DataSet temp = GetDataByStr(strWhere);
        if (temp != null)
        {
            return GetListFromDataTable(temp.Tables[0]);
        }
        else
            return null;
    }
    #endregion

}