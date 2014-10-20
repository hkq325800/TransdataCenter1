using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommCtrl_uc_MonthConvert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBeginDate.Text = DateTime.Now.ToString("yyyy-01-01");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-01-31");
        
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
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "DateQuantum", string.Format("SetMonthQuantumControl('{0}','{1}','{2}');", dlist.ClientID, txtBeginDate.ClientID, txtEndDate.ClientID), true);
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

}
