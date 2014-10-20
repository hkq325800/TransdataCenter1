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

public partial class DriverInOutRegisDailyQryFrm : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    XtraReportFac xrf = new XtraReportFac();
    protected void Page_Load(object sender, EventArgs e)
    {
        
           

            xrf.mHeaderTableColumnCount = 5;
            xrf.mDetailTableColumnCount = 5;
            xrf.detailFields = new string[] { "BUSCLASS", "BUSSELFNO", "EMPNAME", "REGISTERTIME", "OUTTIME" };

            xrf.ShowReportTitle("停车场司、乘人员进出场登记表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

            xrf.ReportHeader = new string[] { "班别", "车号", "司、乘人员", "报到时间", "出场时间" };
            xrf.ColumnCount = new string[] { "1", "1", "1", "1", "1" };
            xrf.ReportHeaderFunc();

        
        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(reportdate) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");

        strWhere.Append(" and areaowner= ");
        strWhere.Append(" 0");

        PIMSQuery qryPIMS = new PIMSQuery();
        xrf.mDs = qryPIMS.GetPIMSDriverInOutDataByWhereStr(strWhere.ToString(), this);

        xrf.ShowRepotDetail();



        this.rptViewDetail.Report = xrf;
    }



}