using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
public partial class ParkinfoQuery : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlParkArea.DataSource = oh.GetData("select t.areaid,t.areades from PI_PARKAREA t where t.areaowner=0");
            ddlParkArea.DataValueField = "areaid";
            ddlParkArea.DataTextField = "areades";
            ddlParkArea.DataBind();

            ViewState["SortOrder"] = "PARKNO";
            ViewState["OrderDire"] = "ASC";
            bind();

        }

    }
    public void bind()
    {
        DataSet ds = oh.GetData(@"select t1.PARKID,
       t1.PARKNO,
       t1.AREAOWNER,
       t3.AREANO as parkarea,
       t2.GarageNO as GARAGEOWNER,
       case when PARKBUSTYPE = 0 then '大' when PARKBUSTYPE = 1 then '中' when PARKBUSTYPE = 2 then '小' end as PARKBUSTYPE,
       case when PARKTYPE = 0 then '常备泊位' when PARKTYPE = 1 then '后备泊位' when PARKTYPE = 2 then '临时泊位' end as PARKTYPE,
       case when PARKSTATE = 0 then '空' when PARKSTATE = 1 then '占用'else '未启用' end as PARKSTATE,
       t1.REMARK
  from PI_PARKINFO t1
       left join pi_GarageInfo t2 on t2.GarageID = t1.GARAGEOWNER 
       left join PI_PARKAREA t3 on t3.AREAID = t1.parkarea
 where t1.AREAOWNER = 0 and t1.parkarea = :0
 order by t1.PARKID",ddlParkArea.SelectedItem.Value);

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
