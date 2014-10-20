using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;
public partial class DriverRegister : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "EmpNo";
            ViewState["OrderDire"] = "ASC";
            bind();

        }

    }
    public void bind()
    {

        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(t1.ENTERTIME) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");
        strWhere.Append(" and t1.areaowner= 0");
        if (txtEmpNo.Text != string.Empty)
        {
            strWhere.Append(" and t2.empno='");
            strWhere.Append(txtEmpNo.Text.ToString().Trim());
            strWhere.Append("'");
        }

        if (txtEmpName.Text != string.Empty)
        {
            strWhere.Append(" and t2.EmpName='");
            strWhere.Append(txtEmpName.Text.ToString().Trim());
            strWhere.Append("'");
        }
      

        DataSet ds = oh.GetData(@"select t1.RECID,
       t2.EmpNo,
       t2.EmpName,
       t1.ENTERTIME,
       case when RESULT = 1 then '早到' when RESULT = 2 then '正常' when RESULT = 3 then '迟到' when RESULT = 4 then '未到' when RESULT = 5 then '未排班' end as RESULT,
       t1.AREAOWNER,
       t1.EQUIPID,
       case when STATFLAG = 0 then '统计' when STATFLAG = 1 then '未统计' end as STATFLAG,
       t1.OPTID,
       t1.MODIFYTIME,
       t1.REMARK
  from PI_REGISTER t1
       left join pi_EmployeeInfo t2 on t2.EmpID = t1.EMPID
 where 0=0 " + strWhere.ToString());

        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        GV.DataSource = dv;
        GV.DataBind();
        lblCount.Text = string.Format("共 {0} 条记录", ds.Tables[0].Rows.Count);
    }
    protected void GV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标经过时，行背景色变 
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
        }

        //给第一列设置自动增长的编号
        //if (e.Row.RowIndex != -1)
        //{
        //    int id = e.Row.RowIndex + 1;
        //    e.Row.Cells[0].Text = id.ToString();
        //}
    }
    /// <summary>
    /// 正反排序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GV_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }

        bind();

    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV.PageIndex = e.NewPageIndex;
        bind();//对GridView进行再次绑定

    }
    /// <summary>
    /// 分页跳转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Turn_Click(object sender, EventArgs e)
    {

        GV.PageIndex = int.Parse(((TextBox)GV.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
        bind();//对GridView进行再次绑定
    }

    protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    {
        bind();
    }
}
