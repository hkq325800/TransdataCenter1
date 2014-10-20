using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;
public partial class LabelQuery : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "RFIDNO";
            ViewState["OrderDire"] = "ASC";
            bind();

        }

    }
    public void bind()
    {
        StringBuilder strWhere = new StringBuilder();
      
        strWhere.Append(" and t1.areaowner= 0");
        strWhere.Append(" and trunc(t1.MODIFYTIME) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");
        if (txtRfidNO.Text != string.Empty)
        {
            strWhere.Append(" and RFIDNO='");
            strWhere.Append(txtRfidNO.Text.ToString().Trim());
            strWhere.Append("'");
        }
       

        DataSet ds = oh.GetData(@"select t1.AREAOWNER,
       t1.MODIFYTIME,
       t1.OPTID,
       t1.REMARK,
       t1.RFIDID,
       t1.RFIDINFO,
       t1.RFIDNO,
       t1.RFIDSTATE,
       t1.RFIDTYPE
  from PI_RFIDINFO t1
 where 0 = 0"+strWhere.ToString() );

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
