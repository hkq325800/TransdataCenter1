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

public partial class ParkOutUnusualDetailQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XtraReportFac xrf = new XtraReportFac();
        PIMSQuery pimsqry = new PIMSQuery();
        xrf.mHeaderTableColumnCount = 5;
        xrf.mDetailTableColumnCount = 5;
        xrf.detailFields = new string[] { "project", "deptname", "count", "BUSSELFNO", "MODIFYRESULT" };

        xrf.ShowReportTitle("车辆非正常出场明细表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "项目", "单位", "车辆数", "车辆编号", "原因", };

        xrf.ReportHeaderFunc();

        // xrf.MergeCells = new string[] { "project", "deptname","count" }; //设置需要行单元格合并的字段


        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(t2.reportdate) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");


        strWhere.Append(" and t1.STATE = 2");
        DataSet ds = pimsqry.GetPIMSParkInOutDataByWhereSelStr(" '延误出场车辆' as project, t2.deptname,t1.BUSSELFNO ,t1.MODIFYRESULT,t2.LATEAPPEARANCENUMBER as count",
            strWhere.ToString(), this);
        xrf.mDs = ds;
        xrf.ShowRepotDetail();


        StringBuilder strWhere1 = new StringBuilder();
        strWhere1.Append(" and trunc(t2.reportdate) ");
        strWhere1.Append("between to_date('");
        strWhere1.Append(DateConvert1.BeginDate);
        strWhere1.Append("','yyyy-mm-dd') AND to_date('");
        strWhere1.Append(DateConvert1.EndDate);
        strWhere1.Append("','yyyy-mm-dd')");

        strWhere1.Append(" and t1.STATE = 3");
        DataSet ds1 = pimsqry.GetPIMSParkInOutDataByWhereSelStr(" '未出场车辆' as project, t2.deptname,t1.BUSSELFNO ,t1.MODIFYRESULT,t2.NOAPPEARANCENUMBER as count",
            strWhere1.ToString(), this);
        xrf.mDs = ds1;
        xrf.ShowRepotDetail();



        this.rptViewDetail.Report = xrf;
    }



}