﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hdcweb.soc.BLL;
using System.Data;

namespace TransdataCenter.js
{
    public partial class MonthReport : SmartSessionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//首次加载访问
            {
                //Session["date"] = false;//date 0为昨日1为上月 
                //Session["repair"] = false;//repair 0为小修1为保养
                //Session["unit"] = "";
                Session["pagesize"] = 20;
                Session["upperlimit"] = 1;
                Session["lowlimit"] = (int)Session["pagesize"];
                if (Flag == 1)
                {
                    DataTable com = webBLL.GetComName();
                    if (com != null)
                    {
                        this.DDLcom.DataSource = com;
                        this.DDLcom.DataTextField = "depname";
                        this.DDLcom.DataBind();
                        this.DDLcomID.DataSource = com;
                        this.DDLcomID.DataTextField = "depid";
                        this.DDLcomID.DataBind();
                    }
                    //lblworker.Visible = false;
                    this.lblcom.Text = "修理公司：";
                    DDLcom.SelectedIndex = 0;
                    DDLcomID.SelectedIndex = 0;
                    //MonthreportAray.SelectedIndex = 0;
                }
                else if (Flag == 2)
                {
                    DataTable com = webBLL.GetComName();
                    if (com != null)
                    {
                        this.DDLcom.DataSource = com;
                        this.DDLcom.DataTextField = "depname";
                        this.DDLcom.DataBind();
                        this.DDLcomID.DataSource = com;
                        this.DDLcomID.DataTextField = "depid";
                        this.DDLcomID.DataBind();
                    }
                    this.lblcom.Text = "维修工段：";
                    lblcom.Visible = false;
                    DDLcom.Visible = false;
                    DDLcom.SelectedIndex = 0;
                    DDLcomID.SelectedIndex = 0;
                    //MonthreportAray.SelectedIndex = 0;
                }
                else
                {
                    DataTable com = webBLL.GetComName();
                    if (com != null)
                    {
                        this.DDLcom.DataSource = com;
                        this.DDLcom.DataTextField = "depname";
                        this.DDLcom.DataBind();
                        this.DDLcomID.DataSource = com;
                        this.DDLcomID.DataTextField = "depid";
                        this.DDLcomID.DataBind();
                    }
                    this.lblcom.Text = "修理公司：";
                    //lblworker.Visible = false;
                    DDLcom.SelectedIndex = 0;
                    DDLcomID.SelectedIndex = 0;
                    //MonthreportAray.SelectedIndex = 0;
                }
                try
                {
                    if (Request.QueryString["unit"] == "SQ")
                    {
                        if (Flag != 2)
                        {
                            this.DDLcom.SelectedIndex = 0;
                            DDLcomID.SelectedValue = "04";
                            Session["unit"] = "SQ";
                        }
                        else//修理公司进入unit转为记录工段 
                        { }
                    }
                    else if (Request.QueryString["unit"] == "CX")
                    {
                        if (Flag != 2)
                        {
                            this.DDLcom.SelectedIndex = 2;
                            DDLcomID.SelectedValue = "26";
                            Session["unit"] = "CX";
                        }
                        else//修理公司进入unit转为记录工段 
                        { }
                    }
                    else if (Request.QueryString["unit"] == "ZT")
                    {
                        if (Flag != 2)
                        {
                            this.DDLcom.SelectedIndex = 1;
                            DDLcomID.SelectedValue = "21";
                            Session["unit"] = "ZT";
                        }
                        else//修理公司进入unit转为记录工段 
                        { }
                    }
                    else { }
                    if (Request.QueryString["type"] == "xx")//小修
                    {
                        this.DDLrepairstyle.SelectedIndex = 0;
                        Session["repair"] = false;
                    }
                    else if (Request.QueryString["type"] == "by")//保养
                    {
                        this.DDLrepairstyle.SelectedIndex = 1;
                        Session["repair"] = true;
                    }
                    else { }
                    if (Request.QueryString["date"] == "day")//昨日
                    {
                        this.DDLdatetype.SelectedIndex = 0;
                        Session["date"] = false;
                    }
                    else if (Request.QueryString["date"] == "month")//上月
                    {
                        this.DDLdatetype.SelectedIndex = 1;
                        Session["date"] = true;
                    }
                    else { }
                    BtnSearch_Click(sender, e);
                }
                catch
                {
                    return;
                }
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DataTable dt = webBLL.GetSum((bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue, Identity);
            if (dt != null)
            {
                //this.LtlPageCount.Text = ((dt.Rows.Count / (int)Session["pagesize"]) + (dt.Rows.Count % (int)Session["pagesize"] > 0 ? 1 : 0)).ToString();
                //this.LtlPageSize.Text = Session["pagesize"].ToString();
                //this.LtlPageIndex.Text = "1";//当前索引，从1开始
                //this.LtlRecordCount.Text = dt.Rows.Count.ToString();//总条数
                dt = new DataTable();
                dt = webBLL.GetSum(1, (int)Session["pagesize"], (bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue, Identity);
                this.DGSum.DataSource = dt;
                this.DGSum.DataBind();
            }
        }

        protected void DDLrepairstyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            if (DDLrepairstyle.SelectedIndex == 0)
            { Session["repair"] = false; }
            else Session["repair"] = true;
            ////DDLbusid.SelectedIndex = DDLrepairstyle.SelectedIndex;
            //BtnSearch_Click(sender, e);
            string query = "MonthReport.aspx?";
            if (DDLrepairstyle.Text == "小修")
            {
                query += "type=xx";
            }
            else
            {
                query += "type=by";
            }
            if (DDLdatetype.Text == "昨日")
            {
                query += "&date=day";
            }
            else
            {
                query += "&date=month";
            }
            if (DDLcom.Text == "石桥修理(运营)分公司")
            {
                query += "&unit=SQ";
            }
            else if (DDLcom.Text == "转塘修理分公司")
            {
                query += "&unit=ZT";
            }
            else
            {
                query += "&unit=CX";
            }
            Response.Redirect(query);
        }

        protected void DDLcom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            DDLcomID.SelectedIndex = DDLcom.SelectedIndex;
            //BtnSearch_Click(sender, e);
            string query = "MonthReport.aspx?";
            if (DDLrepairstyle.Text == "小修")
            {
                query += "type=xx";
            }
            else
            {
                query += "type=by";
            }
            if (DDLdatetype.Text == "昨日")
            {
                query += "&date=day";
            }
            else
            {
                query += "&date=month";
            }
            if (DDLcom.Text == "石桥修理(运营)分公司")
            {
                query += "&unit=SQ";
            }
            else if (DDLcom.Text == "转塘修理分公司")
            {
                query += "&unit=ZT";
            }
            else
            {
                query += "&unit=CX";
            }
            Response.Redirect(query);
        }

        protected void DDLdatetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLdatetype.SelectedIndex == 0)
            { Session["date"] = false; }
            else Session["date"] = true;
            Session["upperlimit"] = 1;
            Session["lowlimit"] = (int)Session["pagesize"];
            //BtnSearch_Click(sender, e);
            string query = "MonthReport.aspx?";
            if (DDLrepairstyle.Text == "小修")
            {
                query += "type=xx";
            }
            else
            {
                query += "type=by";
            }
            if (DDLdatetype.Text == "昨日")
            {
                query += "&date=day";
            }
            else
            {
                query += "&date=month";
            }
            if (DDLcom.Text == "石桥修理(运营)分公司")
            {
                query += "&unit=SQ";
            }
            else if (DDLcom.Text == "转塘修理分公司")
            {
                query += "&unit=ZT";
            }
            else
            {
                query += "&unit=CX";
            }
            Response.Redirect(query);
        }

        //#region[翻页]
        ///// <summary>
        ///// 首页
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void FirstPage(object sender, EventArgs e)
        //{
        //    Session["upperlimit"] = 1;
        //    Session["lowlimit"] = (int)Session["pagesize"];
        //    DataTable dt = new DataTable();
        //    dt = webBLL.GetSum(1, (int)Session["pagesize"], (bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue,Identity);
        //    if (dt != null)
        //    {
        //        this.DGSum.DataSource = dt;
        //        this.DGSum.DataBind();
        //        this.LtlPageIndex.Text = "1";//当前索引，从1开始
        //    }
        //}
        ///// <summary>
        ///// 尾页
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void LastPage(object sender, EventArgs e)
        //{
        //    Session["upperlimit"] = (Convert.ToInt32(this.LtlPageCount.Text) - 1) * (int)Session["pagesize"] + 1;
        //    Session["lowlimit"] = Convert.ToInt32(this.LtlPageCount.Text) * (int)Session["pagesize"];
        //    DataTable dt = new DataTable();
        //    dt = webBLL.GetSum((int)Session["upperlimit"], (int)Session["lowlimit"], (bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue, Identity);
        //    if (dt != null)
        //    {
        //        this.DGSum.DataSource = dt;
        //        this.DGSum.DataBind();
        //        this.LtlPageIndex.Text = this.LtlPageCount.Text;//当前索引，从1开始
        //    }
        //}
        ///// <summary>
        ///// 上一页
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void PrePage(object sender, EventArgs e)
        //{
        //    if (Convert.ToInt32(this.LtlPageIndex.Text) != 1)//如果此时不是第一页，则点击“上一页”时执行下面
        //    {
        //        Session["upperlimit"] = (int)Session["upperlimit"] - (int)Session["pagesize"];
        //        Session["lowlimit"] = (int)Session["lowlimit"] - (int)Session["pagesize"];
        //        DataTable dt = new DataTable();
        //        dt = webBLL.GetSum((int)Session["upperlimit"], (int)Session["lowlimit"], (bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue, Identity);
        //        if (dt != null)
        //        {
        //            this.DGSum.DataSource = dt;
        //            this.DGSum.DataBind();
        //            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) - 1).ToString();//当前索引，从1开始
        //        }
        //    }
        //}
        ///// <summary>
        ///// 下一页
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void NextPage(object sender, EventArgs e)
        //{
        //    //如果此时页面不是最后一页，则执行下面
        //    if (Convert.ToInt32(this.LtlPageIndex.Text) != Convert.ToInt32(this.LtlPageCount.Text))
        //    {
        //        Session["upperlimit"] = (int)Session["upperlimit"] + (int)Session["pagesize"];
        //        Session["lowlimit"] = (int)Session["lowlimit"] + (int)Session["pagesize"];
        //        DataTable dt = new DataTable();
        //        dt = webBLL.GetSum((int)Session["upperlimit"], (int)Session["lowlimit"], (bool)Session["date"], (bool)Session["repair"], DDLcomID.SelectedValue, Identity);
        //        if (dt != null)
        //        {
        //            this.DGSum.DataSource = dt;
        //            this.DGSum.DataBind();
        //            this.LtlPageIndex.Text = (Convert.ToInt32(this.LtlPageIndex.Text) + 1).ToString();//当前索引，从1开始
        //        }
        //    }
        //}
        //#endregion
    }

}