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
    public partial class ContractMore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//首次加载访问
            {
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
                string type = Request.QueryString["type"];
                string date = Request.QueryString["date"];
                if (type == "0")
                {
                    this.DDLtype.SelectedIndex = 0;
                }
                else this.DDLtype.SelectedIndex = 1;
                DataTable dt = webBLL.getContractYear(type);
                if (dt != null)
                {
                    this.DDLdate.DataSource = dt;
                    this.DDLdate.DataTextField = "enddate";
                    this.DDLdate.DataBind();
                }
                ListItem li = new ListItem(date);
                if (this.DDLdate.Items.Contains(li))
                {
                    this.DDLdate.Text = date;
                }
                else
                {
                    this.DDLdate.SelectedIndex = 10;
                    date = this.DDLdate.Text;
                }
                Session["date"] = date;
                Session["type"] = type;
                Btnsearch(sender, e);
            }
        }

        protected void Btnsearch(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt;
            dt = webBLL.getContract((string)Session["date"],(string)Session["type"]);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                dt = webBLL.getContract(1, (int)Session["pagesize"], (string)Session["date"], (string)Session["type"]);
                this.DGContract.DataSource = dt;
                this.DGContract.DataBind();
            }
        }

        protected void DDLdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["date"] = this.DDLdate.Text;
            string query = "ContractMore.aspx?";
            query += "type=" + (string)Session["type"] + "&date=" + (string)Session["date"];
            Response.Redirect(query);
        }

        protected void DDLtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["type"] = this.DDLtype.SelectedIndex.ToString();
            string query = "ContractMore.aspx?";
            query += "type=" + (string)Session["type"] + "&date=" + (string)Session["date"];
            Response.Redirect(query);
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
            dt = webBLL.getContract(1, (int)Session["pagesize"], (string)Session["date"], (string)Session["type"]);
            if (dt != null)
            {
                this.DGContract.DataSource = dt;
                this.DGContract.DataBind();
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
            dt = webBLL.getContract((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["type"]);
            if (dt != null)
            {
                this.DGContract.DataSource = dt;
                this.DGContract.DataBind();
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
                dt = webBLL.getContract((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["type"]);
                if (dt != null)
                {
                    this.DGContract.DataSource = dt;
                    this.DGContract.DataBind();
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
                dt = webBLL.getContract((int)Session["upperlimit"], (int)Session["lowlimit"], (string)Session["date"], (string)Session["type"]);
                if (dt != null)
                {
                    this.DGContract.DataSource = dt;
                    this.DGContract.DataBind();
                    this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                }
            }
        }
        #endregion
    }
}