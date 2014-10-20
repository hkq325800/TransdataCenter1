using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
public partial class CommCtrl_BusTeamDetailQryFrm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        XtraReportFac xrf = new XtraReportFac();
        xrf.linkUrl = "./RouteDetialQryFrm.aspx?RouteName=";
        xrf.linkText = "routename";
        xrf.mHeaderTableColumnCount = 4;
        xrf.mDetailTableColumnCount = 4;
        xrf.detailFields = new string[] { "USETEAMNUM", "routename", "matname", "soilcount" };
        xrf.ShowReportTitle("车队加油统计", Employee.GetSessionEmp(this).EMPNAME, System.DateTime.Now.ToLongDateString());
        xrf.ReportHeader = new string[] { "车队编号", "线路", "加油种类", "加油数量" };
        xrf.ReportHeaderFunc();
        if (!IsPostBack)
        {
            txtCompany.Text = Request["UnitName"];
        }
        else
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" and trunc(oildate) ");
            strWhere.Append("between to_date('");
            strWhere.Append(uc_DateConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd') AND to_date('");
            strWhere.Append(uc_DateConvert1.EndDate);
            strWhere.Append("','yyyy-mm-dd')");
            
            //xrf.mDs = ds;
            xrf.ShowRepotDetail();
        }
        this.rptViewDetail.Report = xrf;
    }
}
