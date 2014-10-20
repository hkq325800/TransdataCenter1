using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;
public partial class CheckResult : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "BusSelfNo";
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
        if (txtDriverId.Text != string.Empty)
        {
            strWhere.Append(" and t3.empno='");
            strWhere.Append(txtDriverId.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtBusSelfNo.Text != string.Empty)
        {
            strWhere.Append(" and t2.BusSelfNo='");
            strWhere.Append(txtBusSelfNo.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtBusNo.Text != string.Empty)
        {
            strWhere.Append(" and t2.BusNo='");
            strWhere.Append(txtBusNo.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtDriverName.Text != string.Empty)
        {
            strWhere.Append(" and t3.EmpName='");
            strWhere.Append(txtDriverName.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtChecker.Text != string.Empty)
        {
            strWhere.Append(" and t6.EmpName='");
            strWhere.Append(txtChecker.Text.ToString().Trim());
            strWhere.Append("'");
        }

        DataSet ds = oh.GetData(@"select t1.RECID,
       t2.BusSelfNo,
       t2.BUSNO ,
       t3.EmpNo as DRIVERID,
       t3.EmpName as DRIVERName,
       t1.CHECKRESULT,
       t6.EMPNAME,
       t1.ENTERTIME,
       case when t1.FITFLAG = 1 then ' 一致 ' else ' 不一致 ' end as FITFLAG,
       t5.EQUIPNO,
       t1.REMARK,
       t1.AREAOWNER,
       t1.OPTID,
         t1.MODIFYTIME
  from PI_CHECK t1
       left join PI_BUSINFO t2 on t1.BUSID = t2.BUSID 
       left join PI_EMPLOYEEINFO t6 on t6.EMPID = t1.EMPID 
       left join PI_EMPLOYEEINFO t3 on t3.EMPID = t1.DRIVERID 
       left join pi_Equipment t5 on t5.EQUIPID = t1.EQUIPID
where 0 = 0" + strWhere.ToString());

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
