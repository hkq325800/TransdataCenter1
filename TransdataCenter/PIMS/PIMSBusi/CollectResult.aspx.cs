using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OracleClient;
using System.Text;
public partial class CollectResult : System.Web.UI.Page
{
    OracleHelper oh = DBFactory.GetObject("PIMSConnString");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SortOrder"] = "DRIVERName";
            ViewState["OrderDire"] = "ASC";
           // bind();

        }

        

       

    }
    public void bind()
    {
        StringBuilder strWhere = new StringBuilder();
        //strWhere.Append(" and trunc(t1.ENTERTIME) ");
        //strWhere.Append("between to_date('");
        //strWhere.Append(DateConvert1.BeginDate);
        //strWhere.Append("','yyyy-mm-dd') AND to_date('");
        //strWhere.Append(DateConvert1.EndDate);
        //strWhere.Append("','yyyy-mm-dd')");
        if (DateConvert1.BeginDate != string.Empty && DateConvert1.EndDate!= string.Empty)
        {
            strWhere.Append(" and t1.ENTERTIME ");
            strWhere.Append("between to_date('");
            strWhere.Append(DateConvert1.BeginDate + " 02:00:00");
            strWhere.Append("','yyyy-mm-dd HH24:MI:SS') AND to_date('");
            strWhere.Append(DateConvert1.EndDate + " 01:59:59");
            strWhere.Append("','yyyy-mm-dd HH24:MI:SS')+1");
        }
        strWhere.Append(" and t1.areaowner= 0");
        if (txtDriverId.Text != string.Empty)
        {
            strWhere.Append(" and t8.empno='");
            strWhere.Append(txtDriverId.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtBusSelfNo.Text != string.Empty)
        {
            strWhere.Append(" and t2.BusSelfNo='");
            strWhere.Append(txtBusSelfNo.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtBusNo.Text != string.Empty)
        {
            strWhere.Append(" and t2.BusNo='");
            strWhere.Append(txtBusNo.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtDriverName.Text != string.Empty)
        {
            strWhere.Append(" and t3.EmpName='");
            strWhere.Append(txtDriverName.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtCollecter.Text != string.Empty)
        {
            strWhere.Append(" and t3.EmpName='");
            strWhere.Append(txtCollecter.Text.ToString().Trim());
            strWhere.Append("'");
        }
        if (txtEnsureName.Text != string.Empty)
        {
            strWhere.Append(" and t7.EmpName='");
            strWhere.Append(txtEnsureName.Text.ToString().Trim());
            strWhere.Append("'");
        }
        DataSet ds = oh.GetData(@"select t1.RECID,
       t2.BusSelfNo,
       t2.BusNo,
       t8.EmpNo as DRIVERID,
       t8.EmpName as DRIVERName,
       t5.BagNo as BAGIDA,
       t6.BagNo as BAGIDB,
       t1.AREAOWNER,
       t1.ENTERTIME,
       t4.EquipNo as EQUIPID,
       t3.EmpName as EMPName,
       t7.EmpName as ENSUREName,
       t1.ENSURETIME,
       t1.OPTID,
       t1.MODIFYTIME,
       case when t1.STATFLAG = 0 then '收取' when t1.STATFLAG = 2 then '统计' end as STATFLAG,
       t1.STATDATE,
       t1.REMARK
  from PI_COLLECT t1
       left join pi_BusInfo t2 on t2.BusID = t1.BUSID 
       left join pi_EmployeeInfo t3 on t3.EmpID = t1.empid 
       left join pi_EmployeeInfo t8 on t8.EmpID = t1.DRIVERID 
       left join pi_EmployeeInfo t7 on t7.EmpID = t1.ENSUREID 
       left join pi_Equipment t4 on t4.EquipID = t1.EQUIPID 
       left join pi_CashBag t5 on t5.BagID = t1.BAGIDA 
       left join pi_CashBag t6 on t6.BagID = t1.BAGIDB
 where   0 = 0" + strWhere.ToString());

        DataView dv = ds.Tables[0].DefaultView;
        string sort = (string)ViewState["SortOrder"] + " " + (string)ViewState["OrderDire"];
        dv.Sort = sort;
        GV.DataSource = dv;
        GV.DataBind();
        lblCount.Text = string.Format("共 {0} 条记录", ds.Tables[0].Rows.Count);
    }
    protected void GV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //鼠标经过时，行背景色变 
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
            //鼠标移出时，行背景色变 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
        }

        //给第一列设置自动增长的编号
        //if (e.Row.RowIndex != -1)
        //{
        //    int id = e.Row.RowIndex + 1;
        //    e.Row.Cells[0].Text = id.ToString();
        //}
    }
    protected void GV_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sPage = e.SortExpression;
        if (ViewState["SortOrder"].ToString() == sPage)
        {
            if (ViewState["OrderDire"].ToString() == "Desc")
                ViewState["OrderDire"] = "ASC";
            else
                ViewState["OrderDire"] = "Desc";
        }
        else
        {
            ViewState["SortOrder"] = e.SortExpression;
        }

        bind();

    }
    /// <summary>
    /// 分页事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV.PageIndex = e.NewPageIndex;
        bind();//对GridView进行再次绑定

    }
    /// <summary>
    /// 分页跳转
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Turn_Click(object sender, EventArgs e)
    {

        GV.PageIndex = int.Parse(((TextBox)GV.BottomPagerRow.FindControl("txtGoPage")).Text) - 1;
        bind();//对GridView进行再次绑定
    }

    protected void btnQuery_Click(object sender, ImageClickEventArgs e)
    {
        bind();       
    }
}
