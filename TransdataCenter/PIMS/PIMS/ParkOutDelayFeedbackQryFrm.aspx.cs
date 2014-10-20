using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

public partial class ParkOutDelayFeedbackQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();

        PIMSQuery pimsqry = new PIMSQuery();
        xrf.mHeaderTableColumnCount = 7;
        xrf.mDetailTableColumnCount = 7;



        if (!IsPostBack)
        {
            ddlBusUnit.DataSource = pimsqry.GetBusUnit("", this);  //取得营运公司;
            ddlBusUnit.DataValueField = "deptid";
            ddlBusUnit.DataTextField = "deptname";
            ddlBusUnit.DataBind();
            ddlBusUnit.Items.Insert(0, "请选择单位");




        }
        xrf.detailFields = new string[] { "BUSSELFNO",  "FIRSTTIME", "OUTINTIME", "LATETIME", "MODIFYRESULT", "BLAMEANALYSIS", "Remark" };

        xrf.ShowReportTitle("早出场延误情况反馈表", "公司：" + (ddlBusUnit.SelectedItem.Text == null ? string.Empty : ddlBusUnit.SelectedItem.Text), System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "车号",  "计划出场时间", "实际出场时间", "延误时间", "延误原因", "责任分析", "备注" };
       
        xrf.ReportHeaderFunc();

       
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" and trunc(reportdate) ");
            strWhere.Append("between to_date('");
            strWhere.Append(DateConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd') AND to_date('");
            strWhere.Append(DateConvert1.EndDate);
            strWhere.Append("','yyyy-mm-dd')");
            if (ddlBusUnit.SelectedIndex != 0)
            {
                strWhere.Append(" and t2.deptid= ");
                strWhere.Append(ddlBusUnit.SelectedItem.Value);
            }
            strWhere.Append(" and t2.areaowner= ");
            strWhere.Append(" 0");
            strWhere.Append(" and t1.STATE=2 ");

            DataSet ds = pimsqry.GetPIMSParkInOutDataByWhereSelStr("t2.AREAOWNERNAME,t1.BLAMEANALYSIS,t1.BUSSELFNO,t2.deptid,t2.deptname,t1.FIRSTTIME,t1.LATETIME,t1.MODIFYRESULT,t1.OUTINTIME,'' as Remark",
                strWhere.ToString(), this);


            xrf.mDs = ds;
            xrf.ShowRepotDetail();
        

        this.rptViewDetail.Report = xrf;
    }



    protected void ddlBusUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}