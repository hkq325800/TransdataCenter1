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

public partial class BusCheckDailyQryFrm : System.Web.UI.Page
{
    PIMSQuery pimsqry = new PIMSQuery();
    string checkname = "";
    string groupname = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        OracleHelper oh = DBFactory.GetObject("PIMSConnString");

        XtraReportFac xrf = new XtraReportFac();

        xrf.mHeaderTableColumnCount = 5;
        xrf.mDetailTableColumnCount = 5;
      

        if (!IsPostBack)
        {
            ddlCheckGroup.DataSource = pimsqry.GetGroup(" and areaowner=0 and typeid=0", this);  //编号为0的停车场 typeid=0为安检组;
            ddlCheckGroup.DataValueField = "groupid";
            ddlCheckGroup.DataTextField = "groupname";
            ddlCheckGroup.DataBind();
           // ddlCheckGroup.Items.Insert(0, "所有分组");



        }
        DataTable dt = pimsqry.GetGroupMenber(" and groupid=" + ddlCheckGroup.SelectedItem.Value, this);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            checkname += dt.Rows[i]["empname"].ToString() + "  ";
        }
        groupname = (ddlCheckGroup.SelectedItem.Value == "所有分组") ? " " : ddlCheckGroup.SelectedItem.Text;

        
        xrf.ShowReportTitle("停车场车辆安检日报表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "安检组:", groupname, "安检人姓名:", checkname };
        xrf.ColumnCount = new string[] { "1", "1", "1", "2" };
        xrf.ReportHeaderFunc();

        xrf.ReportHeader = new string[] { "单位", "安检车辆数", "安检车辆编号", "安检结果" };
        xrf.ColumnCount = new string[] { "1", "1", "1", "2" };
        xrf.ReportHeaderFunc();

        xrf.detailFields = new string[] { "busunitname", "buschecknumber", "BUSSELFNO", "CHECKRESULT" };
        xrf.ColumnCount = new string[] { "1", "1", "1", "2" };

       
     

        xrf.MergeCells = new string[] { "busunitname", "buschecknumber" }; //设置需要行单元格合并的字段

        StringBuilder strWhere = new StringBuilder();
        strWhere.Append(" and trunc(reportdate) ");
        strWhere.Append("between to_date('");
        strWhere.Append(DateConvert1.BeginDate);
        strWhere.Append("','yyyy-mm-dd') AND to_date('");
        strWhere.Append(DateConvert1.EndDate);
        strWhere.Append("','yyyy-mm-dd')");
        strWhere.Append(" and groupid= ");
        strWhere.Append(ddlCheckGroup.SelectedItem.Value);
        strWhere.Append(" and areaowner= ");
        strWhere.Append(" 0");

        DataSet ds = pimsqry.GetPIMSCheckDataByWhereStr(strWhere.ToString(), this);

       
        xrf.mDs = ds;
        xrf.ShowRepotDetail();

     
        xrf.ReportSum = new string[] { "合计", ds.Tables[0].Rows.Count.ToString(), "", "" };   //统计值通过程序处理直接得到传给报表，并非通过报表类处理
        xrf.ColumnCount = new string[] { "1", "1", "1", "2" };

        xrf.ShowMergeSumBand();

        this.rptViewDetail.Report = xrf;
    }



    protected void ddlCheckGroup_SelectedIndexChanged(object sender, EventArgs e)
    {


        //ddlCheckMenber.DataSource = pimsqry.GetGroupMenber(" and groupid=" + ddlCheckGroup.SelectedItem.Value, this);

        //ddlCheckMenber.DataValueField = "empid";
        //ddlCheckMenber.DataTextField = "empname";
        //ddlCheckMenber.DataBind();


    }
}