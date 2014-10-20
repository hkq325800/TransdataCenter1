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

public partial class ParkOutStatQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
        OracleHelper oh = DBFactory.GetObject("PIMSConnString");
        xrf.mHeaderTableColumnCount = 5;
        xrf.mDetailTableColumnCount = 5;
        xrf.detailFields = new string[] { "DEPTNAME", "SHOULDAPPEARANCENUMBER", "NORMALAPPEARANCENUMBER", "LATEAPPEARANCENUMBER", "NOAPPEARANCENUMBER" };

        xrf.ShowReportTitle("车辆出场汇总表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());
        xrf.ReportHeader = new string[] { "单位", "应出场车辆数", "正常出场车辆数", "延误出场车辆数", "未出场车辆数" };
        xrf.ReportHeaderFunc();

        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(reportdate) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");
        strWhere.Append(" and t2.areaowner= ");
        strWhere.Append(" 0");

        StringBuilder sql = new StringBuilder();
        sql.Append("select t2.DEPTNAME, t2.SHOULDAPPEARANCENUMBER,   t2.NORMALAPPEARANCENUMBER, t2.LATEAPPEARANCENUMBER,t2.NOAPPEARANCENUMBER");
        sql.Append(" from  GH_PI_PARKINOUTSTAT_DAILY t2 where 0=0");
        DataSet ds = oh.GetData(sql.ToString()+ strWhere.ToString());
        xrf.mDs = ds;
        xrf.ShowRepotDetail();



        this.rptViewDetail.Report = xrf;
    }



}