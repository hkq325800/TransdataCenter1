using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hdcweb.soc.BLL;
using System.Data;

namespace TransdataCenter
{
    public partial class RepairSum : SmartSessionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    DataTable dt = webBLL.GetRepairSumEmp(Identity);
                    this.DataGrid1.DataSource = dt;
                    this.DataGrid1.DataBind();
                    //DataTable dt2 = webBLL.GetRepairSum(Identity, 2);
                    //this.DataGrid2.DataSource = dt2;
                    //this.DataGrid2.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/index.aspx");
                    return;
                }
            }
        }

        protected void ClickForSumEmp(object sender, EventArgs e)
        {
            DataTable dt = webBLL.GetRepairSumEmp(Identity);
            this.DataGrid1.DataSource = dt;
            this.DataGrid1.DataBind();
        }
        protected void ClickForSumPla(object sender, EventArgs e)
        {
            DataTable dt = webBLL.GetRepairSumPla(Identity);
            this.DataGrid1.DataSource = dt;
            this.DataGrid1.DataBind();
        }
    }
}