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

public partial class ParkInStatQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();

        PIMSQuery pimsqry = new PIMSQuery();

        xrf.mHeaderTableColumnCount = 5;
        xrf.mDetailTableColumnCount = 5;
        xrf.detailFields = new string[] { "deptname", "PLANPARKNUMBER", "ACTUALPARKNUMBER", "BUSSELFNO", "remark" };

        xrf.MergeCells = new string[] { "deptname", "PLANPARKNUMBER", "ACTUALPARKNUMBER" }; //设置需要行单元格合并的字段

        xrf.ShowReportTitle("车辆进场汇总表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());
        xrf.ReportHeader = new string[] { "单位", "计划停车数", "实际停放数", "未进场车辆编号", "备注" };
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
            strWhere.Append(" and state=4 ");
         
            DataSet ds = pimsqry.GetPIMSParkInOutDataByWhereSelStr("t2.deptname, t2.PLANPARKNUMBER, t2.ACTUALPARKNUMBER,t1.BUSSELFNO,'' as remark", strWhere.ToString(), this);
            xrf.mDs = ds;
            xrf.ShowRepotDetail();


        

        this.rptViewDetail.Report = xrf;
    }
  
 

}