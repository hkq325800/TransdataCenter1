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

public partial class CollecterDayStatQryFrm : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {

        XtraReportFac xrf = new XtraReportFac();
        PIMSQuery pimsqry = new PIMSQuery();
        xrf.mHeaderTableColumnCount = 10;
        xrf.mDetailTableColumnCount = 10;

        xrf.ShowReportTitle("收银员工作日统计表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());

        xrf.ReportHeader = new string[] { "姓名", "一公司", "二公司", "三公司", "杭余公司", "中巴公司", "客服公司", "未知", "本日合计", "累计" };

        xrf.ReportHeaderFunc();

        StringBuilder strWhere = new StringBuilder();
        string StatSql = @"select count(*) from PI_COLLECT t1 where t1.statflag=0 ";
        StringBuilder strStatWhere = new StringBuilder();
        strStatWhere.Append(StatSql);
        strStatWhere.Append(" and trunc(entertime) ");
        strStatWhere.Append("between to_date('");
        strStatWhere.Append(uc_DayConvert1.BeginDate);
        strStatWhere.Append("','yyyy-mm-dd') AND to_date('");
        strStatWhere.Append(uc_DayConvert1.BeginDate);
        strStatWhere.Append("','yyyy-mm-dd')");
        strStatWhere.Append(" and t1.areaowner= ");
        strStatWhere.Append(" 0");
        DataTable statdt = oh.GetData(strStatWhere.ToString()).Tables[0];



        DataSet ds = new DataSet();
        //if (rbl.SelectedItem.Value == "stat")
        if (Convert.ToInt32(statdt.Rows[0][0]) == 0)  //全部统计过的
        {

            strWhere.Append(" and trunc(reportdate) ");
            strWhere.Append("between to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd') AND to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd')");
            strWhere.Append(" and areaowner= ");
            strWhere.Append(" 0");
            ds = pimsqry.GetPIMSCollecterDataByWhereSelStr(" t1.BAGNUMBER, t1.busunit, t1.busunitname, t1.EMPNAME ",
            strWhere.ToString(), this);
        }
        //查询未统计的
        else
        {
            string MainSql = @"
select Trunc(t1.entertime-2/24) as reportdate,
       t1.AREAOWNER,
       t6.ITEMNAME as areaownername,
       t4.deptid as busunit,
       t4.deptname as busunitname,
       --t7.linename as busroutename,--t2.BUSROUTENAME,
       t2.BUSSELFNO,
       t1.GROUPID,
       t5.GROUPNAME,
       t1.bagida,
       t1.bagidb,
       t1.entertime,
       t1.statflag
  from PI_COLLECT t1
       left join PI_BUSINFO t2 on t1.BUSID = t2.BUSID 
       left join PI_PARKDEPT t3 on t2.BUSUNIT = t3.DEPTID 
       left join pi_parkdept t4 on substr(t3.deptcode, 0, 2) = t4.deptcode and t4.AREAOWNER = t1.AREAOWNER
       left join PI_GROUP t5 on t5.GROUPID = t1.GROUPID 
       left join PI_SYSTEMCONFIG t6 on t6.ITEMCODE = 'ParkNo' and t6.ITEMVALUE = t1.AREAOWNER
      -- left join v_pi_dispatchplan_bus t7 on t7.busno=t2.busselfno and trunc(t1.entertime)=trunc(t7.adjustdate) and t7.firsttime is not null
 where 0 = 0 ";
            strWhere.Append(MainSql);
            strWhere.Append(" and entertime ");
            strWhere.Append("between to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate+" 02:00:00");
            strWhere.Append("','yyyy-mm-dd HH24:MI:SS') AND to_date('");
            strWhere.Append(uc_DayConvert1.BeginDate+" 01:59:59");
            strWhere.Append("','yyyy-mm-dd HH24:MI:SS')+1");
  	   
            strWhere.Append(" and t1.areaowner= ");
            strWhere.Append(" 0");
            DataTable dtColle = oh.GetData(strWhere.ToString()).Tables[0];

            //构造收银明细日报表    --用于收银员工作日统计表
            DataTable dt_Detail = new DataTable("gh_pi_collectdetail_daily");
            dt_Detail.Columns.Add("bagnumber", typeof(int));
            dt_Detail.Columns.Add("empid", typeof(string));
            dt_Detail.Columns.Add("empname", typeof(string));
            dt_Detail.Columns.Add("areaowner", typeof(string));
            dt_Detail.Columns.Add("areaownername", typeof(string));
            dt_Detail.Columns.Add("groupid", typeof(int));
            dt_Detail.Columns.Add("groupname", typeof(string));
            dt_Detail.Columns.Add("busunit", typeof(int));
            dt_Detail.Columns.Add("busunitname", typeof(string));
            dt_Detail.Columns.Add("reportdate", typeof(DateTime));
            dt_Detail.Columns.Add("statflag", typeof(int));
            foreach (DataRow dr in dtColle.Rows)
            {
                //车辆收银记录
                if (dr["AREAOWNER"] == DBNull.Value)
                {
                    dr["AREAOWNER"] = -1;
                    dr["AREAOWNERNAME"] = string.Empty;
                }
                if (dr["BUSUNIT"] == DBNull.Value)
                {
                    dr["BUSUNIT"] = -1;
                    dr["BUSUNITNAME"] = string.Empty;
                }
                bool mustAdd = true;


                int BagCount = 0;
                if (dr["bagida"].ToString() != string.Empty)
                    BagCount++;
                if (dr["bagidb"].ToString() != string.Empty)
                    BagCount++;

                //处理个人收银记录
                mustAdd = true;
                foreach (DataRow dr_detail in dt_Detail.Rows)
                    if (Convert.ToInt32(dr_detail["AREAOWNER"]) == Convert.ToInt32(dr["AREAOWNER"]) &&
                        Convert.ToInt32(dr_detail["BUSUNIT"]) == Convert.ToInt32(dr["BUSUNIT"]) &&
                        Convert.ToInt32(dr_detail["GROUPID"]) == Convert.ToInt32(dr["GROUPID"]) &&
                        dr["REPORTDATE"].ToString() == dr_detail["REPORTDATE"].ToString())
                    {
                        mustAdd = false;
                        dr_detail["BAGNUMBER"] = Convert.ToInt32(dr_detail["BAGNUMBER"]) + BagCount;
                        break;
                    }
                if (mustAdd)
                {
                    string[] empInfo = GetGroupMember(Convert.ToInt32(dr["GROUPID"]));
                    Insert(dt_Detail, BagCount, empInfo[0], empInfo[1], dr["AREAOWNER"], dr["AREAOWNERNAME"], dr["GROUPID"], dr["GROUPNAME"],
                         dr["BUSUNIT"], dr["BUSUNITNAME"], dr["REPORTDATE"],dr["statflag"]);
                }

            }
            ds.Tables.Add(dt_Detail);


        }

        //用于算累计数

        StringBuilder strWhere1 = new StringBuilder();
        strWhere1.Append(" and trunc(reportdate) ");
        strWhere1.Append("between trunc(add_months(last_day( ");
        strWhere1.Append("to_date('");
        strWhere1.Append(uc_DayConvert1.BeginDate);
        strWhere1.Append("','yyyy-mm-dd')) , -1) + 1)");

        strWhere1.Append(" AND to_date('");
        strWhere1.Append(uc_DayConvert1.BeginDate);
        strWhere1.Append("','yyyy-mm-dd')");
        DataSet ds1 = pimsqry.GetPIMSCollectSumNum(strWhere1.ToString(), this);


        DataSet dsCopy = new DataSet();

        //构造一个datatable
        DataTable dt = new DataTable();
        for (int i = 0; i < 10; i++)
            dt.Columns.Add(i.ToString());
        xrf.detailFields = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            int i;
            DataRow cdr;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == dr["EMPNAME"].ToString())
                    // cdr[8]=ds1.Tables[0].Rows[i][0];
                    break;
            }
            if (i >= dt.Rows.Count)
                cdr = dt.Rows.Add(dr["EMPNAME"].ToString(), 0, 0, 0, 0, 0, 0, 0, 0, 0);
            else cdr = dt.Rows[i];

            switch (dr["busunit"].ToString())
            {
                case "1"://一公司
                    cdr[1] = dr["BAGNUMBER"];

                    break;
                case "2"://二公司
                    cdr[2] = dr["BAGNUMBER"];
                    break;
                case "3"://三公司
                    cdr[3] = dr["BAGNUMBER"];
                    break;
                case "4"://杭余公司
                    cdr[4] = dr["BAGNUMBER"];
                    break;
                case "5"://中巴公司
                    cdr[5] = dr["BAGNUMBER"];
                    break;
                case "6"://客服公司
                    cdr[6] = dr["BAGNUMBER"];
                    break;
                default:
                    cdr[7] = dr["BAGNUMBER"];
                    break;

            }

        }
        for (int i = 0; i < dt.Rows.Count; i++)//当天收银员的一个DT
        {
            //统计本日合计数
            for (int j = 1; j < dt.Columns.Count - 2; j++)
            {
                dt.Rows[i][dt.Columns.Count - 2] = Int32.Parse(dt.Rows[i][dt.Columns.Count - 2].ToString()) + Int32.Parse(dt.Rows[i][j].ToString());
            }
            //统计累计数  ds1为所有收银员累计数的一个dataset  
            for (int k = 0; k < ds1.Tables[0].Rows.Count; k++)
            {
                bool ischeck = false;
                if (Convert.ToInt32(statdt.Rows[0][0]) == 0)  //全部统计过的
                {
                    if (dt.Rows[i][0].ToString() == ds1.Tables[0].Rows[k][1].ToString() )//rbl.SelectedItem.Value == "stat")
                    {
                        dt.Rows[i][dt.Columns.Count - 1] = ds1.Tables[0].Rows[k][0];
                    }
                }
                else
                {
                   
                    //日报表统计过的，且当天的收银员姓名已经在当月有收过了
                    if (dt.Rows[i][0].ToString() == ds1.Tables[0].Rows[k][1].ToString() && ds.Tables[0].Rows[i]["statflag"].ToString() == "2")//rbl.SelectedItem.Value == "stat")
                    {
                        dt.Rows[i][dt.Columns.Count - 1] = ds1.Tables[0].Rows[k][0];
                        ischeck=true;
                    }
                    //日报表未统计过的，且当天的收银员姓名已经在当月有收过了
                    if (dt.Rows[i][0].ToString() == ds1.Tables[0].Rows[k][1].ToString() && ds.Tables[0].Rows[i]["statflag"].ToString() == "0")//rbl.SelectedItem.Value == "nostat") 
                    {
                        dt.Rows[i][dt.Columns.Count - 1] = Convert.ToInt32(dt.Rows[i][dt.Columns.Count - 2].ToString()) + Convert.ToInt32(ds1.Tables[0].Rows[k][0].ToString());
                        ischeck = true;
                    }
                    //日报表未统计过的，且当天的收银员姓名在当月还没有收过
                    if (dt.Rows[i][0].ToString() != ds1.Tables[0].Rows[k][1].ToString() && ds.Tables[0].Rows[i]["statflag"].ToString() == "0")//rbl.SelectedItem.Value == "nostat") 
                    {
                        int temp = k;
                        if (temp < ds1.Tables[0].Rows.Count-1)
                        {  }
                        else
                        {
                            dt.Rows[i][dt.Columns.Count - 1] = Convert.ToInt32(dt.Rows[i][dt.Columns.Count - 2].ToString());
                            ischeck = true;
                        }
                    }

                }
                if (ischeck == true)
                    break;

            }


        }

        dsCopy.Tables.Add(dt);

        xrf.mDs = dsCopy;

        xrf.ShowRepotDetail();


        //begin 生成最终的合计
        DataSet sumds = new DataSet();
        DataTable sumdt = new DataTable();
        for (int i = 0; i < 10; i++)
            sumdt.Columns.Add(i.ToString());
        xrf.detailFields = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        DataRow ndr = sumdt.Rows.Add("合计", 0, 0, 0, 0, 0, 0, 0, 0, 0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {


            for (int j = 1; j < dt.Columns.Count; j++)
            {

                ndr[j] = Int32.Parse(ndr[j].ToString()) + Int32.Parse(dt.Rows[i][j].ToString());
            }

        }
        sumds.Tables.Add(sumdt);
        xrf.mDs = sumds;

        xrf.ShowRepotDetail();
        //end


        this.rptViewDetail.Report = xrf;
    }

    protected DataRow Insert(DataTable dt, params object[] Values)
    {
        DataRow dr = dt.NewRow();
        dr.ItemArray = Values;
        dt.Rows.Add(dr);
        return dr;
    }
    protected string[] GetGroupMember(int GROUPID)
    {
        string result_EmpID = "", result_EmpName = "";
        DataTable dt = oh.GetData(@"
select t1.EMPID, t2.EMPNAME
  from PI_USERGROUP t1
       left join PI_EMPLOYEEINFO t2 on t1.EMPID = t2.EMPID
 where t1.GROUPID = :0", GROUPID).Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            if (result_EmpID != string.Empty)
            {
                result_EmpID += "|";
                result_EmpName += "|";
            }
            result_EmpID += dr["EMPID"].ToString();
            result_EmpName += dr["EMPName"].ToString();
        }
        return new string[2] { result_EmpID, result_EmpName };
    }
}