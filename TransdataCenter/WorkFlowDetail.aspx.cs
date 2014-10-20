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
    public partial class WorkFlowDetail :SmartSessionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
                if (Request.QueryString["id"].ToString() != "")
                {
                    this.lblTitle.Visible = true;
                    this.select.Visible = false;
                    QueryById(sender, e);
                }
                else if (Request.QueryString["date"].ToString() != "")
                {
                    this.lblTitle.Visible = false;
                    this.select.Visible = true;
                    DataTable year = webBLL.getWorkflowYear();
                    if (year != null)
                    {
                        this.DDLyear.DataSource = year;
                        this.DDLyear.DataTextField = "year";
                        this.DDLyear.DataBind();
                    }
                    if (Request.QueryString["date"].ToString() != "today" && Request.QueryString["date"].ToString() != "yester")
                    {
                        this.DDLyear.Text = Request.QueryString["date"].ToString().Substring(0, 4);
                        this.DDLmonth.Text = Request.QueryString["date"].ToString().Substring(4, 2);
                        this.DDLday.Text = Request.QueryString["date"].ToString().Substring(6, 2);
                        Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
                    }
                    else if (Request.QueryString["date"].ToString() == "today")
                    {
                        this.DDLyear.Text = DateTime.Now.Year.ToString();
                        this.DDLmonth.Text = DateTime.Now.ToString("MM");
                        this.DDLday.Text = DateTime.Now.Day.ToString();
                        Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
                    }
                    else
                    {
                        this.DDLyear.Text = DateTime.Now.AddDays(-1).Year.ToString();
                        this.DDLmonth.Text = DateTime.Now.AddDays(-1).Month.ToString();
                        this.DDLday.Text = DateTime.Now.AddDays(-1).Day.ToString();
                        Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
                    }
                    QueryByDate(sender, e);
                }
            }
        }

        protected void QueryById(object sender, EventArgs e)
        {
            DataTable dt;
            this.Table2.Visible = false;
            dt = webBLL.getWorkflowDetail(Request.QueryString["id"].ToString());
            this.DGworkflow.DataSource = dt;
            this.DGworkflow.DataBind();
            //for(int i=0;i<20;i++)
            //{
            //    if (dt.Rows[i].ToString() != "")
            //    { }
            //}
        }

        protected void QueryByDate(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt;
            dt = webBLL.getWorkflow((string)Session["date"]);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                dt = webBLL.getWorkflow(1, (int)Session["pagesize"], (string)Session["date"]);
                this.DGworkflow.DataSource = dt;
                this.DGworkflow.DataBind();
            }
        }

        protected void DDLyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
        }

        protected void DDLmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
        }

        protected void DDLday_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["date"] = this.DDLyear.Text + "-" + this.DDLmonth.Text + "-" + this.DDLday.Text;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FirstPage(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = new DataTable();
            dt = webBLL.getWorkflow(1, (int)Session["pagesize"], (string)Session["date"]);
            if (dt != null)
            {
                this.DGworkflow.DataSource = dt;
                this.DGworkflow.DataBind();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
            }
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LastPage(object sender, EventArgs e)
        {
            Session["upperlimit"] = (Convert.ToInt32(this.LtlPageCount.Text) - 1) * (int)Session["pagesize"] + 1;
            Session["lowlimit"] = Convert.ToInt32(this.LtlPageCount.Text) * (int)Session["pagesize"];
            DataTable dt = new DataTable();
            dt = webBLL.getWorkflow((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"]);
            if (dt != null)
            {
                this.DGworkflow.DataSource = dt;
                this.DGworkflow.DataBind();
                this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PrePage(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.LtlPageIndex.Text) != 1)//如果此时不是第一页，则点击“上一页”时执行下面
            {
                Session["upperlimit"] = (int)Session["upperlimit"] - (int)Session["pagesize"];
                Session["lowlimit"] = (int)Session["lowlimit"] - (int)Session["pagesize"];
                DataTable dt = new DataTable();
                dt = webBLL.getWorkflow((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"]);
                if (dt != null)
                {
                    this.DGworkflow.DataSource = dt;
                    this.DGworkflow.DataBind();
                    this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
                }
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NextPage(object sender, EventArgs e)
        {
            //如果此时页面不是最后一页，则执行下面
            if (Convert.ToInt32(this.LtlPageIndex.Text) != Convert.ToInt32(this.LtlPageCount.Text))
            {
                Session["upperlimit"] = (int)Session["upperlimit"] + (int)Session["pagesize"];
                Session["lowlimit"] = (int)Session["lowlimit"] + (int)Session["pagesize"];
                DataTable dt = new DataTable();
                dt = webBLL.getWorkflow((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"]);
                if (dt != null)
                {
                    this.DGworkflow.DataSource = dt;
                    this.DGworkflow.DataBind();
                    this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                }
            }
        }
    }
}