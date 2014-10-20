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

public partial class BusCollectDailyQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
        PIMSQuery pimsqry = new PIMSQuery();
        xrf.mHeaderTableColumnCount = 6;
        xrf.mDetailTableColumnCount = 6;

        xrf.linkUrl = "./NoCollectBusQryFrm.aspx?BUSUNIT=";
        xrf.linkText = "BUSUNIT";
        xrf.linkNewWindow = true;
        xrf.detailFields = new string[] { "BUSUNITNAME", "PLANBUSNUMBER", "ACTUALCOLLECTNUMBER", "NOCOLLECTNUMBER", "ACTUALBAGNUMBER","BUSUNIT" };

        xrf.ShowReportTitle("停车场收银日报表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "单位", "计划营运车辆数", "实际收银车辆数", "未收银车辆数", "实际钱袋数","单位编号" };
      
        xrf.ReportHeaderFunc();

      //  xrf.MergeCells = new string[] { "BUSUNITNAME", "PLANBUSNUMBER", "ACTUALCOLLECTNUMBER", "NOCOLLECTNUMBER", "ACTUALBAGNUMBER" }; //设置需要行单元格合并的字段
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" and trunc(reportdate) ");
            strWhere.Append("between to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd') AND to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd')");
            strWhere.Append(" and areaowner= ");
            strWhere.Append(" 0");
            DataSet ds = pimsqry.GetPIMSCollectDataByWhereSelStr(" t1.ACTUALBAGNUMBER,t1.ACTUALCOLLECTNUMBER ,t1.NOCOLLECTNUMBER,t1.PLANBUSNUMBER,t1.BUSUNITNAME, BUSUNIT",
               strWhere.ToString(), this);

            xrf.mDs = ds;
            xrf.ShowRepotDetail();

        

        this.rptViewDetail.Report = xrf;
    }
  
 

}