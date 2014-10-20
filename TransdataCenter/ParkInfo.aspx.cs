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
    public partial class ParkInfo : SmartSessionPage
    {
        //private static string currentSql;
        //private void BindData()
        //{
        //    this.SqlDataSource1.SelectCommand = currentSql;
        //    this.GridView1.DataBind();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
                try
                {
                    string date = "";
                    if (Request.QueryString["date"] == null || "today".Equals(Request.QueryString["date"]))
                    {
                        this.DateDropDownList.SelectedIndex = 0;
                        date = " t.ADJUSTDATE = trunc(sysdate - 1.5 / 24)";//今日
                    }
                    else
                    {
                        this.DateDropDownList.SelectedIndex = 1;
                        date = " t.ADJUSTDATE = trunc(sysdate - 25.5 / 24)";//昨日
                    }
                    string filter = "";
                    if (Request.QueryString["filter"] == null || "0".Equals(Request.QueryString["filter"]))
                    {
                        this.DropDownList1.SelectedIndex = 0;
                        filter = "";
                    }
                    else if ("1".Equals(Request.QueryString["filter"]))
                    {
                        this.DropDownList1.SelectedIndex = 1;
                        filter = " AND t.outtime IS NULL";
                    }
                    else if ("2".Equals(Request.QueryString["filter"]))
                    {
                        this.DropDownList1.SelectedIndex = 2;
                        filter = " AND t.outtime IS NOT NULL";
                    }
                    else if ("3".Equals(Request.QueryString["filter"]))
                    {
                        this.DropDownList1.SelectedIndex = 3;
                        filter = " AND t.OUTDELAY > 0";
                    }
                    else if ("4".Equals(Request.QueryString["filter"]))
                    {
                        this.DropDownList1.SelectedIndex = 4;
                        filter = " AND t.OUTDELAY <= 0";
                    }
                    Session["date"] = date;
                    Session["filter"] = filter;
                    BtnSearch_Click(date,filter,sender, e);
                }
                catch (Exception ex)
                {
                    return;
                }
                //currentSql = sql;
                //BindData();
            }
        }

        protected void BtnSearch_Click(string date, string filter, object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = webBLL.GetPark(date, filter);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                dt = new DataTable();
                dt = webBLL.GetPark(1, (int)Session["pagesize"], date,filter);
                this.DGPark.DataSource = dt;
                this.DGPark.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string redirect = "~/parkInfo.aspx?date=today";
            if (DateDropDownList.SelectedValue.ToString() == "昨日")
                redirect = "~/parkInfo.aspx?date=yesterday";
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    redirect += "&filter=0";
                    break;
                case 1:
                    redirect += "&filter=1";
                    break;
                case 2:
                    redirect += "&filter=2";
                    break;
                case 3:
                    redirect += "&filter=3";
                    break;
                case 4:
                    redirect += "&filter=4";
                    break;
                default:
                    redirect += "&filter=0";
                    break;
            }
            Response.Redirect(redirect);
        }

        protected void DateDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList1_SelectedIndexChanged(sender, e);
        }


        #region[翻页]
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
            dt = webBLL.GetPark(1, (int)Session["pagesize"], (string)Session["date"], (string)Session["filter"]);
            if (dt != null)
            {
                this.DGPark.DataSource = dt;
                this.DGPark.DataBind();
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
            dt = webBLL.GetPark((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["filter"]);
            if (dt != null)
            {
                this.DGPark.DataSource = dt;
                this.DGPark.DataBind();
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
                dt = webBLL.GetPark((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["filter"]);
                if (dt != null)
                {
                    this.DGPark.DataSource = dt;
                    this.DGPark.DataBind();
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
                dt = webBLL.GetPark((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["filter"]);
                if (dt != null)
                {
                    this.DGPark.DataSource = dt;
                    this.DGPark.DataBind();
                    this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                }
            }
        }
        #endregion

        //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView1.PageIndex = e.NewPageIndex;
        //    BindData();
        //}

    }
}