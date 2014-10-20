using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hdcweb.soc.BLL;
using System.Web.Security;

namespace TransdataCenter
{
    public partial class Site : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                try
                {
                    lblDep.Text = Session["dep"].ToString()+"的";
                    lblUser.Text = Session["name"].ToString();
                }
                catch
                {
                    Response.Redirect("~/Login.aspx");
                }
                /*欢迎信息*/
                int flag=1;
                //if((Session["id"].ToString())=="00")
                //{
                //    flag=true;
                //}
                this.empNum.InnerText = webBLL.getAllEmpNum(Session["id"].ToString(),flag);
                this.bikeNum.InnerText = webBLL.getBikeNum();
                this.busNum.InnerText = webBLL.getBusNum();
                this.parkNum.InnerText = "1";
                this.todayDate.Text = DateTime.Now.ToString("yyyy-MM-dd   dddd");
                //this.lastLoginTime.Text = "";
                this.lastLoginIPAdd.Text = webBLL.GetClientIp();
                //this.footer.ID = "footer";
            }
        }

        //public void hideFooter()
        //{
        //    this.footer.Visible = false;
        //}

        protected void lblExit_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut(); 
            Response.Redirect("~/Login.aspx?type=logout");
            //FormsAuthentication.RedirectToLoginPage();
        }
    }
}