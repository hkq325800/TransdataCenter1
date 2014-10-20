using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace TransdataCenter
{
    public partial class carQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string qs = Request.QueryString["BUSID"].ToString();
                if (qs == null || qs.Trim() == "")
                {
                    this.alert();
                    //Response.Redirect("~/index.aspx");
                }
            }
            catch (Exception ex)
            {
                this.alert();
                //Response.Redirect("~/index.aspx");
            }
        }

        protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.AffectedRows <= 0)
            {
                this.alert();
                //Response.Redirect("~/index.aspx");
            }
        }

        private void alert()
        {
            Type masterType = this.Master.GetType();//先获取模板页类型
            MethodInfo mi = masterType.GetMethod("hideFooter");//再利用反射获取模板页中的方法
            mi.Invoke(this.Master, null);//执行方法
            ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('车辆查询错误');</script>");
        }
    }
}