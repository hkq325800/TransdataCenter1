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

public partial class SSO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Token"] != null)
        {
            SSOService service = new SSOService();
            ClientEmployee cEmp = service.Validate(Request["Token"], 102);
            if (cEmp != null)
            {
                Employee emp = new Employee(cEmp);
                Employee.SetSessionEmp(this, emp);
                Session["Token"] = Request["Token"];
                Response.Redirect("./");
                return;
            }
            Session.Clear();
            Common.RedirectToSSO(Response, 1);
            return;
        }
        Session.Clear();
        Common.RedirectToSSO(Response, 0);
    }
}
