using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommCtrl_uc_DateConvert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (Session["BeginDate"] != null)
                txtBeginDate.Text = Session["BeginDate"].ToString();
            if (Session["EndDate"] != null)
                txtEndDate.Text = Session["EndDate"].ToString();
        }
        else //if (Session["BeginDate"] == null && Session["EndDate"] == null)
        {
            Session["BeginDate"] = txtBeginDate.Text;
            Session["EndDate"] = txtEndDate.Text;
        }
        Page.ClientScript.RegisterClientScriptInclude("DateQuantumFile", "/JSCode/DateQuantum.js");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "DateQuantum", string.Format("SetDateQuantumControl('{0}','{1}','{2}');", dlist.ClientID, txtBeginDate.ClientID, txtEndDate.ClientID), true);
    }

    public string BeginDate
    {
        get
        {
            return txtBeginDate.Text;
        }
    }
    public string EndDate
    {
        get
        {
            return txtEndDate.Text;
        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (dlist.SelectedIndex == 1)  //昨天
    //    {
    //        txtBeginDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
    //        txtEndDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
    //    }
    //    if (dlist.SelectedIndex == 2) //上周
    //    {
    //        txtBeginDate.Text = DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();
    //        txtEndDate.Text = DateTime.Now.AddDays(Convert.ToDouble((6 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString();

    //    }
    //    if (dlist.SelectedIndex == 3)  //上个月，自然月
    //    {
    //        txtBeginDate.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1).ToShortDateString();
    //        txtEndDate.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();

    //    }
    //    if (dlist.SelectedIndex == 4)  //上个季度，自然月
    //    {
    //        txtBeginDate.Text = DateTime.Now.AddMonths(-3 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01");
    //        txtEndDate.Text = DateTime.Parse(DateTime.Now.AddMonths(0 - ((DateTime.Now.Month - 1) % 3)).ToString("yyyy-MM-01")).AddDays(-1).ToShortDateString();
    //    }
    //}

}
