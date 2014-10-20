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

public partial class BusParkDailyQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
      
        xrf.mHeaderTableColumnCount = 4;
        xrf.mDetailTableColumnCount = 4;
        xrf.detailFields = new string[] { "EMPNAME", "AREANO", "PARKNUMBER", "BUSSELFNO" };

        xrf.ShowReportTitle("车辆泊位日报表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "护场员姓名", "泊位区域", "停车数量", "车辆编号" };
        xrf.ColumnCount = new string[] { "1", "1", "1", "1" };
        xrf.ReportHeaderFunc();
        xrf.MergeCells = new string[] { "EMPNAME", "AREANO", "PARKNUMBER" };

      
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
            xrf.mDs = qryPIMS.GetPIMSParkDataByWhereStr(strWhere.ToString(), this);

            xrf.ShowRepotDetail();

        

        this.rptViewDetail.Report = xrf;
    }
  
 

}