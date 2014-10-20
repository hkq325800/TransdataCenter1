using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommCtrl_uc_DayConvert : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
          
            if (Session["BeginDate"] != null)
                txtBeginDate.Text = Session["BeginDate"].ToString();
           
        }
        else //if (Session["BeginDate"] == null && Session["EndDate"] == null)
        {
            Session["BeginDate"] = txtBeginDate.Text;
           
        }
        Page.ClientScript.RegisterClientScriptInclude("DateQuantumFile", "/JSCode/DateQuantum.js");
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "DateQuantum", string.Format("SetDayQuantumControl('{0}','{1}');", dlist.ClientID, txtBeginDate.ClientID), true);
    }

    public string BeginDate
    {
        get
        {
            return txtBeginDate.Text;
        }
    }
  

}
