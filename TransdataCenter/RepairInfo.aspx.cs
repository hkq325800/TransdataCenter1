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
    public partial class RepairInfo : System.Web.UI.Page
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
                Session["searchtype"] = 1;
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
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
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            try
            {
                DataTable dt = new DataTable();
                dt = webBLL.getRepairInfo(Tebbusid.Text, flag);
                if (dt != null)
                {
                    this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                    this.LtlPageSize.Text = Session["pagesize"].ToString();
                    this.LtlPageIndex.Text = "1";//当前索引，从1开始
                    this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                    dt = new DataTable();
                    dt = webBLL.getRepairInfo(1, (int)Session["pagesize"], Tebbusid.Text, flag);
                    this.DGrepairinfo.DataSource = dt;
                    this.DGrepairinfo.DataBind();
                }
                DataTable type = new DataTable();
                type = webBLL.getRepairType(Tebbusid.Text);
                if (type != null)
                {

                    this.DDLType.DataSource = type;
                    this.DDLType.DataTextField = "itemvalue";
                    this.DDLType.DataBind();
                    DDLType.Items.Insert(0, "全部类别");
                    this.DDLType.SelectedIndex = 0;//默认选中“全部类别”筛选
                }
                DataTable remonth = new DataTable();
                remonth = webBLL.getRepairMonth(Tebbusid.Text);
                if (remonth != null)
                {
                    this.DDLMonth.DataSource = remonth;
                    this.DDLMonth.DataTextField = "balancemonth";
                    this.DDLMonth.DataBind();
                    DDLMonth.Items.Insert(0, "全部年月");
                    this.DDLMonth.SelectedIndex = 0;//默认选中“全部年份”筛选
                }
                this.RepairInfoAray.SelectedIndex = 0;//默认选中“维修年份”排序
                Session["searchtype"] = 1;
            }

            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }

        }

        protected void DDLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = new DataTable();
            dt = webBLL.filtrateRepairInfo(Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                Session["searchtype"] = 2;
                dt = new DataTable();
                dt = webBLL.filtrateRepairInfo(1, (int)Session["pagesize"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                this.DGrepairinfo.DataSource = dt;
                this.DGrepairinfo.DataBind();
            }
        }

        protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = new DataTable();
            dt = webBLL.filtrateRepairInfo(Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                Session["searchtype"] = 2;
                dt = new DataTable();
                dt = webBLL.filtrateRepairInfo(1, (int)Session["pagesize"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                this.DGrepairinfo.DataSource = dt;
                this.DGrepairinfo.DataBind();
            }
        }

        protected void RepairInfoAray_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = new DataTable();
            dt = webBLL.filtrateRepairInfo(1,(int)Session["pagesize"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
            if (dt != null)
            {
                this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                this.LtlPageSize.Text = Session["pagesize"].ToString();
                this.LtlPageIndex.Text = "1";//当前索引，从1开始
                this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                Session["searchtype"] = 2;
                dt = new DataTable();
                dt = webBLL.filtrateRepairInfo(1, (int)Session["pagesize"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                this.DGrepairinfo.DataSource = dt;
                this.DGrepairinfo.DataBind();
            }
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
            if ((int)Session["searchtype"] == 1)
            {
                DataTable dt = new DataTable();
                dt = webBLL.getRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, flag);
                if (dt != null)
                {
                    this.DGrepairinfo.DataSource = dt;
                    this.DGrepairinfo.DataBind();
                    this.LtlPageIndex.Text = "1";//当前索引，从1开始
                }
                
            }
            else if ((int)Session["searchtype"] == 2)
            {
                DataTable dt = new DataTable();
                dt = webBLL.filtrateRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                if (dt != null)
                {
                    this.DGrepairinfo.DataSource = dt;
                    this.DGrepairinfo.DataBind();
                    this.LtlPageIndex.Text = "1";//当前索引，从1开始
                }
            }
            else
            {

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
            if ((int)Session["searchtype"] == 1)
            {
                DataTable dt = new DataTable();
                dt = webBLL.getRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, flag);
                if (dt != null)
                {
                    this.DGrepairinfo.DataSource = dt;
                    this.DGrepairinfo.DataBind();
                    this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
                }
            }
            else if ((int)Session["searchtype"] == 2)
            {
                DataTable dt = new DataTable();
                dt = webBLL.filtrateRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                if (dt != null)
                {
                    this.DGrepairinfo.DataSource = dt;
                    this.DGrepairinfo.DataBind();
                    this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
                }
            }
            else
            {

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
                if ((int)Session["searchtype"] == 1)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, flag);
                    if (dt != null)
                    {
                        this.DGrepairinfo.DataSource = dt;
                        this.DGrepairinfo.DataBind();
                        this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
                    }
                }
                else if ((int)Session["searchtype"] == 2)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.filtrateRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGrepairinfo.DataSource = dt;
                        this.DGrepairinfo.DataBind();
                        this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
                    }
                }
                else
                {

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
                if ((int)Session["searchtype"] == 1)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, flag);
                    if (dt != null)
                    {
                        this.DGrepairinfo.DataSource = dt;
                        this.DGrepairinfo.DataBind();
                        this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                    }
                }
                else if ((int)Session["searchtype"] == 2)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.filtrateRepairInfo((int)Session["upperlimit"], (int)Session["lowlimit"], Tebbusid.Text, DDLType.SelectedIndex, DDLType.SelectedValue, DDLMonth.SelectedIndex, DDLMonth.SelectedValue, RepairInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGrepairinfo.DataSource = dt;
                        this.DGrepairinfo.DataBind();
                        this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                    }
                }
                else
                {

                }
            }
        }
    }
}