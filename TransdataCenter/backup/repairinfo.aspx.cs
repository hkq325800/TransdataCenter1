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
    public partial class repairinfo : System.Web.UI.Page
    {
        bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        { 
            string req = "";
            string repairID = "";
            //在首次访问时执行
            //在page_load中用this.IsPostBack来判断page_load激发原因，如果为true表示是通过Button加载，false则表示刷新加载.
            if (!this.IsPostBack)
            {
                try
                {
                    repairID = Request.QueryString["repairID"];

                    if (repairID != null && repairID != "")
                    {
                        req = Server.UrlDecode(repairID);
                    }
                    if (req == null || req == "")
                    {
                        //req = "where rownum<=10";
                        flag = true;
                    }
                    if (flag == false)
                    {
                        this.Tebbusid.Text = repairID.ToString();
                        this.BtnSearch_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/index.aspx");
                    return;
                }
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = webBLL.getRepairInfo(Tebbusid.Text,flag);
                this.DGrepairinfo.DataSource = dt;
                this.DGrepairinfo.DataBind();
                DataTable type = webBLL.getRepairType(Tebbusid.Text);
                this.DDLType.DataSource = type;
                this.DDLType.DataTextField = "itemvalue";
                this.DDLType.DataBind();
                DDLType.Items.Insert(0, "全部类别");
                DataTable remonth = webBLL.getRepairMonth(Tebbusid.Text);
                this.DDLMonth.DataSource = remonth;
                this.DDLMonth.DataTextField = "balancemonth";
                this.DDLMonth.DataBind();
                DDLMonth.Items.Insert(0, "全部年月");
            }

            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }

        }

        protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = webBLL.filtrateRepairInfo(Tebbusid.Text, DDLType.SelectedIndex, DDLType.Text, DDLMonth.SelectedIndex, DDLMonth.Text);
            this.DGrepairinfo.DataSource = dt;
            this.DGrepairinfo.DataBind();
        }

        protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = webBLL.filtrateRepairInfo(Tebbusid.Text, DDLType.SelectedIndex, DDLType.Text, DDLMonth.SelectedIndex, DDLMonth.Text);
            this.DGrepairinfo.DataSource = dt;
            this.DGrepairinfo.DataBind();
        }
    }
}