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

public partial class ParkOutDelayDetailQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
      //  OracleHelper oh = DBFactory.GetObject("PIMSConnString");
        PIMSQuery pimsqry = new PIMSQuery();

        xrf.mHeaderTableColumnCount = 8;
        xrf.mDetailTableColumnCount = 8;
        xrf.detailFields = new string[] { "deptname", "BUSROUTENAME", "BUSSELFNO", "EMPNAME", "FIRSTTIME", "OUTINTIME", "LATETIME", "MODIFYRESULT" };

        xrf.ShowReportTitle("延误出场明细表", "制表：", System.DateTime.Now.ToLongDateString());
      

        xrf.ReportHeader = new string[] {"公司","路别","车号","姓名","计划出场时间","实际出场时间","延误时间（分）","延误原因"};

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
            strWhere.Append(" and t1.STATE=2 ");
    
            //StringBuilder sql = new StringBuilder();
            //sql.Append("select t2.BUSUNITNAME, t2.BUSROUTENAME,t2.BUSSELFNO,t2.EMPNAME,t2.FIRSTTIME,t2.OUTINTIME,t2.LATETIME,t2.MODIFYRESULT");
            //sql.Append(" from GH_PI_PARKINOUTDETAIL_DAILY t2 left join GH_PI_PARKINOUTSTAT_DAILY t1 on t1.INOUTSTATDAILYID=t2.INOUTSTATDAILYID where 0=0");
            
           // DataSet ds = oh.GetData(sql.ToString() + strWhere.ToString());
            DataSet ds = pimsqry.GetPIMSParkInOutDataByWhereSelStr("t2.deptname, t1.BUSROUTENAME,t1.BUSSELFNO,t1.EMPNAME,t1.FIRSTTIME,t1.OUTINTIME,t1.LATETIME,t1.MODIFYRESULT", strWhere.ToString(), this);
            xrf.mDs = ds;
            xrf.ShowRepotDetail();
        

        this.rptViewDetail.Report = xrf;
    }
  
 

}