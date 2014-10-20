using hdcweb.soc.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransdataCenter
{
    public partial class hdcbike : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
                //记录从首页传过来的参数，即要显示的内容类型
                Session["stationtype"] = Convert.ToInt32(Request["gettype"].ToString());
                if ((int)Session["stationtype"] == 1)
                {
                    this.DDLSearchType.SelectedIndex = 0;
                    this.StationInfoAray.SelectedIndex = 0;
                    this.StationInfoAray.Items.Add("现有车辆数");
                }
                else if ((int)Session["stationtype"] == 2)
                {
                    this.DDLSearchType.SelectedIndex = 0;
                    this.StationInfoAray.SelectedIndex = 0;
                    this.StationInfoAray.Items.Add("租车总数");
                    this.StationInfoAray.Items.Add("还车总数");
                }
                else
                {

                }
                BtnSearch_Click(sender, e);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            if ((int)Session["stationtype"] == 1)
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        dt = webBLL.getSatationBike(this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                            this.LtlPageSize.Text = Session["pagesize"].ToString();
                            this.LtlPageIndex.Text = "1";//当前索引，从1开始
                            this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                            dt = new DataTable();
                            dt = webBLL.getSatationBike(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                        }
                    }
                    else if (this.DDLSearchType.SelectedIndex == 1)
                    {
                        if (!string.IsNullOrEmpty(this.Tebstation.Text.Trim()))
                        {
                            dt = webBLL.getStationData(Convert.ToInt32(this.Tebstation.Text));
                            if (dt != null)
                            {
                                this.DGStation.DataSource = dt;
                                this.DGStation.DataBind();
                            }
                        }
                    }
                    else if (this.DDLSearchType.SelectedIndex == 2)
                    {
                        dt = webBLL.getStationData(this.Tebstation.Text);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                        }
                    }
                    else
                    {

                    }
                }

                catch (Exception ex)
                {
                    Response.Redirect("~/index.aspx");
                }
            }
            else if ((int)Session["stationtype"] == 2)
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        dt = webBLL.getRentReturn(this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                            this.LtlPageSize.Text = Session["pagesize"].ToString();
                            this.LtlPageIndex.Text = "1";//当前索引，从1开始
                            this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                            dt = new DataTable();
                            dt = webBLL.getRentReturn(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                        }
                    }
                    else if (this.DDLSearchType.SelectedIndex == 1)
                    {
                        if (!string.IsNullOrEmpty(this.Tebstation.Text.Trim()))
                        {
                            dt = webBLL.getStationRent(Convert.ToInt32(this.Tebstation.Text));
                            if (dt != null)
                            {
                                this.DGStation.DataSource = dt;
                                this.DGStation.DataBind();
                            }
                        }
                    }
                    else if (this.DDLSearchType.SelectedIndex == 2)
                    {
                        dt = webBLL.getStationRent(this.Tebstation.Text);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                        }
                    }
                    else
                    {

                    }
                }

                catch (Exception ex)
                {
                    Response.Redirect("~/index.aspx");
                }
            }
            else
            { }
        }

        protected void DDLSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DDLSearchType.SelectedIndex == 0)
            {
                this.TabTurnPages.Visible = true;
                this.StationInfoAray.Visible = true;
                this.labStationInfoAray.Visible = true;
                BtnSearch_Click(sender, e);
            }
            else
            {
                this.TabTurnPages.Visible = false;
                this.StationInfoAray.Visible = false;
                this.labStationInfoAray.Visible = false;
            }
        }

        protected void StationInfoAray_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if ((int)Session["stationtype"] == 1)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    dt = webBLL.getSatationBike(this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                        this.LtlPageSize.Text = Session["pagesize"].ToString();
                        this.LtlPageIndex.Text = "1";//当前索引，从1开始
                        this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                        dt = new DataTable();
                        dt = webBLL.getSatationBike(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                    }
                }
            }
            else if ((int)Session["stationtype"] == 2)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    dt = webBLL.getRentReturn(this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                        this.LtlPageSize.Text = Session["pagesize"].ToString();
                        this.LtlPageIndex.Text = "1";//当前索引，从1开始
                        this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                        dt = new DataTable();
                        dt = webBLL.getRentReturn(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                    }
                }
            }
            else
            { }
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
            if ((int)Session["stationtype"] == 1)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getSatationBike(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                        this.LtlPageIndex.Text = "1";//当前索引，从1开始
                    }
                }
            }
            else if ((int)Session["stationtype"] == 2)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getRentReturn(1, (int)Session["pagesize"], this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                        this.LtlPageIndex.Text = "1";//当前索引，从1开始
                    }
                }
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
            if ((int)Session["stationtype"] == 1)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getSatationBike((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                        this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
                    }
                }
            }
            else if ((int)Session["stationtype"] == 2)
            {
                if (this.DDLSearchType.SelectedIndex == 0)
                {
                    DataTable dt = new DataTable();
                    dt = webBLL.getRentReturn((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                    if (dt != null)
                    {
                        this.DGStation.DataSource = dt;
                        this.DGStation.DataBind();
                        this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
                    }
                }
            }
            else
            { }
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
                if ((int)Session["stationtype"] == 1)
                {
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = webBLL.getSatationBike((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
                        }
                    }
                }
                else if ((int)Session["stationtype"] == 2)
                {
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = webBLL.getRentReturn((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
                        }
                    }
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
                if ((int)Session["stationtype"] == 1)
                {
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = webBLL.getSatationBike((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                        }
                    }
                }
                else if ((int)Session["stationtype"] == 2)
                {
                    if (this.DDLSearchType.SelectedIndex == 0)
                    {
                        DataTable dt = new DataTable();
                        dt = webBLL.getRentReturn((int)Session["upperlimit"], (int)Session["lowlimit"], this.StationInfoAray.SelectedValue);
                        if (dt != null)
                        {
                            this.DGStation.DataSource = dt;
                            this.DGStation.DataBind();
                            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
                        }
                    }
                }
            }
        }
    }
}