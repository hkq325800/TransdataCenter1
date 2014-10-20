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
                    string date;
                    string month;
                    DataTable dt;
                    date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                    month = "";
                    if (Flag != 0)
                    {
                        dt = webBLL.GetForCom(Identity, Flag, month, date);
                        this.DGForComDay.DataSource = dt;
                        this.DGForComDay.DataBind();
                    }
                    else this.DGForComDay.Visible = false;
                    if (Flag != 2)
                    {
                        dt = webBLL.GetForRepair(Identity, Flag, month, date);
                        this.DGForRepairDay.DataSource = dt;
                        this.DGForRepairDay.DataBind();
                    }
                    else this.DGForRepairDay.Visible = false;
                    date = "";
                    month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                    month = month.Substring(0, 6);
                    if (Flag != 0)
                    {
                        dt = webBLL.GetForCom(Identity, Flag, month, date);
                        this.DGForComMonth.DataSource = dt;
                        this.DGForComMonth.DataBind();
                    }
                    else this.DGForComMonth.Visible = false;
                    if (Flag != 2)
                    {
                        dt = webBLL.GetForRepair(Identity, Flag, month, date);
                        this.DGForRepairMonth.DataSource = dt;
                        this.DGForRepairMonth.DataBind();
                    }
                    else this.DGForRepairMonth.Visible = false;
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }
    }
}