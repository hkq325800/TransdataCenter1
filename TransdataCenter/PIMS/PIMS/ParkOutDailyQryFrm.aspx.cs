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

public partial class ParkOutDailyQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
        PIMSQuery pimsqry = new PIMSQuery();

        xrf.mHeaderTableColumnCount = 3;
        xrf.mDetailTableColumnCount = 3;
        xrf.detailFields = new string[] { "BUSROUTENAME", "BUSSELFNO", "OUTINTIME" };

        xrf.ShowReportTitle("车辆出场日报表", "制表：", System.DateTime.Now.ToLongDateString());
        xrf.ReportHeader = new string[] { "线路", "车号", "出场时间" };
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
            strWhere.Append(" and (state=1 or state=2) ");

            DataSet ds = pimsqry.GetPIMSParkInOutDataByWhereSelStr("t1.BUSROUTENAME, t1.BUSSELFNO, t1.OUTINTIME", strWhere.ToString(), this);
            xrf.mDs = ds;
            xrf.ShowRepotDetail();

        

        this.rptViewDetail.Report = xrf;
    }
  
 

}