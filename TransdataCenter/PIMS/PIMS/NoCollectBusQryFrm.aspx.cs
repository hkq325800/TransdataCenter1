using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;

public partial class PIMS_NoCollectBusQryFrm : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bind();

        }
    }

    public void bind()
    {

       
        string busunit = Request["busunit"];
        var begindate = Session["BeginDate"];
        var enddate = Session["begindate"];

        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(reportdate) ");
        strWhere.Append("between to_date('");
        strWhere.Append(begindate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(begindate);
        strWhere.Append("','yyyy-mm-dd')");


        if (Session["BeginDate"] != null  && busunit != null)
        {
            DataSet ds = oh.GetData(@"select t2.busroutename,t2.busselfno  from GH_PI_COLLECTSTAT_DAILY t1 left join GH_PI_COLLECTSTATDETAIL_DAILY t2
on t2.COLLECTSTATDAILYID = t1.COLLECTSTATDAILYID where  t1.busunit=:0 " + strWhere.ToString() + " order by t2.busroutename", busunit);
            DataTable dt = new DataTable();
            int i;
            for (i = 0; i < 6; i++)
                dt.Columns.Add();
            DataRow row = null;
            string routeName = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (routeName != dr[0].ToString())
                {
                    i = 0;
                    routeName = dr[0].ToString();
                    row = dt.Rows.Add("||" + routeName);
                    row = dt.Rows.Add();
                }
                if (i > 5)
                {
                    i = 0;
                    row = dt.Rows.Add();
                }
                row[i] = dr[1];
                i++;
            }
            GV.DataSource = dt;
            GV.DataBind();
            foreach (GridViewRow gvrow in GV.Rows)
            {
                if (gvrow.Cells[0].Text.StartsWith("||"))
                {
                    for (i = 5; i > 0; i--)
                        gvrow.Cells.RemoveAt(i);
                    gvrow.Cells[0].ColumnSpan = 6;
                    gvrow.Cells[0].Text = gvrow.Cells[0].Text.Remove(0, 2);
                    gvrow.BackColor = System.Drawing.Color.DarkGray;
                }
            }
            DataSet ds1 = oh.GetData(@"select busunitname  from GH_PI_COLLECTSTAT_DAILY t1 where t1.busunit=:0 ", busunit);
            GV.HeaderRow.Cells.Clear();
            TableCell cell = new TableCell();
            cell.Text = string.Format("{0} 至 {1} 内 {2} 未收银车辆明细表", begindate, enddate, ds1.Tables[0].Rows[0][0].ToString());
            cell.ColumnSpan = 6;
            GV.HeaderRow.Cells.Add(cell);

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    {
        bind();
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "utf-8";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode("未收银明细.xls"));
        Response.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文 
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        System.Globalization.CultureInfo myCItrad =
        new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter =
        new System.Web.UI.HtmlTextWriter(oStringWriter);
        GV.AllowPaging = false;
        bind();
        GV.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.Flush();
        Response.End();
        GV.AllowPaging = true;
    }
    protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV.PageIndex = e.NewPageIndex;
        bind();//对GridView进行再次绑定
    }
    protected void Turn_Click(object sender, EventArgs e)
    {
        GV.PageIndex = int.Parse(((TextBox)GV.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
        bind();//对GridView进行再次绑定
    }
}
