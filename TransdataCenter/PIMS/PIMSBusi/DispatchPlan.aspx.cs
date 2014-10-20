using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;
public partial class DispatchPlan : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "BUSCLASS";
            ViewState["OrderDire"] = "ASC";
            bind();

        }

    }
    public void bind()
    {
        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(t1.ADJUSTDATE) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");
        strWhere.Append(" and t1.area_owner= 0");
       
        DataSet ds = oh.GetData(@"select t1.ORDERID,
       t1.ADJUSTDATE,
       t1.LINENAME,
       t1.BUSSELFNO,
       t1.BUSCLASS,
       t1.DRIVERACODE,
       t1.DRIVERANAME,
       t1.STEWARDACODE,
       t1.STEWARDANAME,
       t1.STEWARDBCODE,
       t1.STEWARDBNAME,
       t1.DRIVERBCODE,
       t1.DRIVERBNAME,
       t1.FIRSTTIME,
       t1.CHANGETIME,
       t1.ABACKTIME,
       t1.BBACKTIME,
       t1.AREA_OWNER,
       t1.OPTID,
       t1.MODIFYTIME,
       case when SW_STATUS = 1 then '原始' when SW_STATUS = 2 then '已修改' when SW_STATUS = -1 then '删除' end as SW_STATUS,
       t1.REMARK
  from PI_DISPATCHPLAN_OLD t1
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
