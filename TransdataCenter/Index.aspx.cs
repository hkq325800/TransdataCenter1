using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using hdcweb.soc.BLL;
using System.Web.UI.HtmlControls;
using System.Data;

namespace TransdataCenter
{
    public partial class Index : SmartSessionPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Identity == "")
                {
                    Response.Write("<script LANGUAGE=JavaScript >" + " alert('该用户不存在！');" + " window.location=('Login.aspx');" + "</script>");
                    //Response.Redirect("~/Login.aspx");
                }
                if (Identity == "04" || Identity == "26" || Identity == "21")
                {
                    Flag = 2;
                    if (Identity == "04")
                    {
                        //this.spanCX.Visible = false;
                        //this.spanZT.Visible = false;
                        this.littFix_CX_day.Visible = false;
                        this.keepFix_CX_day.Visible = false;
                        this.littFix_CX_month.Visible = false;
                        this.keepFix_CX_month.Visible = false;
                        this.lblmaterCost_CX_day_by.Visible = false;
                        this.lblmaterCost_CX_day_xx.Visible = false;
                        this.lblmaterCost_CX_month_by.Visible = false;
                        this.lblmaterCost_CX_month_xx.Visible = false;
                        this.lblmaterCostReal_CX_day_by.Visible = false;
                        this.lblmaterCostReal_CX_day_xx.Visible = false;
                        this.lblmaterCostReal_CX_month_by.Visible = false;
                        this.lblmaterCostReal_CX_month_xx.Visible = false;
                        this.littFix_ZT_day.Visible = false;
                        this.keepFix_ZT_day.Visible = false;
                        this.littFix_ZT_month.Visible = false;
                        this.keepFix_ZT_month.Visible = false;
                        this.lblmaterCost_ZT_day_by.Visible = false;
                        this.lblmaterCost_ZT_day_xx.Visible = false;
                        this.lblmaterCost_ZT_month_by.Visible = false;
                        this.lblmaterCost_ZT_month_xx.Visible = false;
                        this.lblmaterCostReal_ZT_day_by.Visible = false;
                        this.lblmaterCostReal_ZT_day_xx.Visible = false;
                        this.lblmaterCostReal_ZT_month_by.Visible = false;
                        this.lblmaterCostReal_ZT_month_xx.Visible = false;
                    }
                    else if (Identity == "26")
                    {
                        //this.spanSQ.Visible = false;
                        //this.spanZT.Visible = false;
                        this.littFix_SQ_day.Visible = false;
                        this.keepFix_SQ_day.Visible = false;
                        this.littFix_SQ_month.Visible = false;
                        this.keepFix_SQ_month.Visible = false;
                        this.lblmaterCost_SQ_day_by.Visible = false;
                        this.lblmaterCost_SQ_day_xx.Visible = false;
                        this.lblmaterCost_SQ_month_by.Visible = false;
                        this.lblmaterCost_SQ_month_xx.Visible = false;
                        this.lblmaterCostReal_SQ_day_by.Visible = false;
                        this.lblmaterCostReal_SQ_day_xx.Visible = false;
                        this.lblmaterCostReal_SQ_month_by.Visible = false;
                        this.lblmaterCostReal_SQ_month_xx.Visible = false;
                        this.littFix_ZT_day.Visible = false;
                        this.keepFix_ZT_day.Visible = false;
                        this.littFix_ZT_month.Visible = false;
                        this.keepFix_ZT_month.Visible = false;
                        this.lblmaterCost_ZT_day_by.Visible = false;
                        this.lblmaterCost_ZT_day_xx.Visible = false;
                        this.lblmaterCost_ZT_month_by.Visible = false;
                        this.lblmaterCost_ZT_month_xx.Visible = false;
                        this.lblmaterCostReal_ZT_day_by.Visible = false;
                        this.lblmaterCostReal_ZT_day_xx.Visible = false;
                        this.lblmaterCostReal_ZT_month_by.Visible = false;
                        this.lblmaterCostReal_ZT_month_xx.Visible = false;
                    }
                    else
                    {
                        //this.spanCX.Visible = false;
                        //this.spanSQ.Visible = false;
                        this.littFix_CX_day.Visible = false;
                        this.keepFix_CX_day.Visible = false;
                        this.littFix_CX_month.Visible = false;
                        this.keepFix_CX_month.Visible = false;
                        this.lblmaterCost_CX_day_by.Visible = false;
                        this.lblmaterCost_CX_day_xx.Visible = false;
                        this.lblmaterCost_CX_month_by.Visible = false;
                        this.lblmaterCost_CX_month_xx.Visible = false;
                        this.lblmaterCostReal_CX_day_by.Visible = false;
                        this.lblmaterCostReal_CX_day_xx.Visible = false;
                        this.lblmaterCostReal_CX_month_by.Visible = false;
                        this.lblmaterCostReal_CX_month_xx.Visible = false;
                        this.littFix_SQ_day.Visible = false;
                        this.keepFix_SQ_day.Visible = false;
                        this.littFix_SQ_month.Visible = false;
                        this.keepFix_SQ_month.Visible = false;
                        this.lblmaterCost_SQ_day_by.Visible = false;
                        this.lblmaterCost_SQ_day_xx.Visible = false;
                        this.lblmaterCost_SQ_month_by.Visible = false;
                        this.lblmaterCost_SQ_month_xx.Visible = false;
                        this.lblmaterCostReal_SQ_day_by.Visible = false;
                        this.lblmaterCostReal_SQ_day_xx.Visible = false;
                        this.lblmaterCostReal_SQ_month_by.Visible = false;
                        this.lblmaterCostReal_SQ_month_xx.Visible = false;
                    }
                }
                else if (Identity == "00")
                {
                    Flag = 1;
                }
                else
                {
                    Flag = 0;
                    //lblmore1.Visible = false;//月报
                }



                #region/*今日自行车租赁信息*/

                this.bikeStation_Today.InnerText = webBLL.getBikeStation();
                this.bikeNumber_Today.InnerText = webBLL.getBikeNum();
                //this.yesterday_Today1.Text = DateTime.Now.AddDays(-1).ToShortDateString().ToString();
                this.bikeRentYesterday_Today1.InnerText = webBLL.getBikeRentYesterday();
                this.bikeRentThisMonth.InnerText = webBLL.getBikeRentThisMonth();
                this.bikeRentLastmonth.InnerText = webBLL.getBikeRentLastMonth();

                #endregion

                #region/*昨日自行车租赁信息*/

                //this.yesterday_Today2.InnerText = this.yesterday_Today1.Text;
                this.bikeRentYesterday_Today2.InnerText = this.bikeRentYesterday_Today1.InnerText;
                DataTable dt = webBLL.getTop5BikeRentYesterday();
                if (dt != null)
                {
                    this.DGRentTopStation.DataSource = dt;
                    this.DGRentTopStation.DataBind();
                }
                #endregion

                #region/*今日停车场信息*/
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                this.carNumIn_Today.InnerText = webBLL.getCarNumIn(date);
                this.carNumOut_Today.InnerText = webBLL.getCarNumOut(date);
                this.carLate_Today.InnerText = webBLL.getCarLate(date);
                this.carInTime_Today.InnerText = webBLL.getCarInTime(date);
                this.wash_Today.InnerText = webBLL.getWash(date);
                this.collectNum_Today.InnerText = webBLL.getCollectNum(date);
                this.oilNum_Today.InnerText = webBLL.getOilNum(date);
                this.CountBusToday.InnerText = webBLL.getBusNum();
                this.CountLineToday.InnerText = webBLL.getLineNum();
                #endregion

                #region/*昨日停车场信息*/
                date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                this.carNumIn_Yesterday.InnerText = webBLL.getCarNumIn(date);
                this.carNumOut_Yesterday.InnerText = webBLL.getCarNumOut(date);
                this.carLate_Yesterday.InnerText = webBLL.getCarLate(date); ;
                this.carInTime_Yesterday.InnerText = webBLL.getCarInTime(date);
                this.wash_Yesterday.InnerText = webBLL.getWash(date);
                this.collectNum_Yesterday.InnerText = webBLL.getCollectNum(date);
                this.oilNum_Yesterday.InnerText = webBLL.getOilNum(date);
                this.CountBusYester.InnerText = CountBusToday.InnerText;
                this.CountLineYester.InnerText = CountLineToday.InnerText;
                #endregion

                #region/*修理实时结算信息*/
                bool type = false;
                string place = "";
                string month = "";
                //今日信息

                date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                month = month.Substring(0, 6);
                //bool confirm = false;//confirm为false只显示石桥城西为true显示石桥城西转塘
                string[][] cost = webBLL.getCost(Identity, Flag, date, month);
                //if (cost.Length == 12)
                //{ confirm = true; }
                //else this.spanZT.Visible = false;
                //cost为四段sql语句union all的结果 
                //cost[0][0]昨日石桥小修计划cost[0][1]昨日石桥小修实际
                //cost[1][0]昨日城西小修计划cost[1][1]昨日城西小修实际
                //cost[2][0]昨日石桥保养计划cost[2][1]昨日石桥保养实际
                //cost[3][0]昨日城西保养计划cost[3][1]昨日城西保养实际
                //cost[4][0]上月石桥小修计划cost[4][1]上月石桥小修实际
                //cost[5][0]上月城西小修计划cost[5][1]上月城西小修实际
                //cost[6][0]上月石桥保养计划cost[6][1]上月石桥保养实际
                //cost[7][0]上月城西保养计划cost[7][1]上月城西保养实际
                //if (!confirm)
                //{
                //    //昨日信息
                //    date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                //    month = "";
                //    string temp = "";
                //    place = "04";
                //    this.littFix_SQ_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //    //type = false;
                //    this.materCost_SQ_day_xx.InnerText = cost[0][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    if (cost[0][1] == "0") { temp = "尚未统计"; } else temp = cost[0][1] + " 元,";
                //    this.materCostReal_SQ_day_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    this.keepFix_SQ_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //    //type = true;
                //    this.materCost_SQ_day_by.InnerText = cost[2][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_SQ_day_by.InnerText = cost[2][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    place = "26";
                //    this.littFix_CX_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //    //type = false;
                //    this.materCost_CX_day_xx.InnerText = cost[1][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    if (cost[1][1] == "0") { temp = "尚未统计"; } else temp = cost[1][1] + " 元,";
                //    this.materCostReal_CX_day_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    this.keepFix_CX_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //    //type = true;
                //    this.materCost_CX_day_by.InnerText = cost[3][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_CX_day_by.InnerText = cost[3][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    //上月信息
                //    date = "";
                //    month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                //    month = month.Substring(0, 6);
                //    place = "04";
                //    this.littFix_SQ_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //    //type = false;
                //    this.materCost_SQ_month_xx.InnerText = cost[4][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_SQ_month_xx.InnerText = cost[4][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    this.keepFix_SQ_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //    //type = true;
                //    this.materCost_SQ_month_by.InnerText = cost[6][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_SQ_month_by.InnerText = cost[6][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    place = "26";
                //    this.littFix_CX_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //    //type = false;
                //    this.materCost_CX_month_xx.InnerText = cost[5][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_CX_month_xx.InnerText = cost[5][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //    this.keepFix_CX_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //    //type = true;
                //    this.materCost_CX_month_by.InnerText = cost[7][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //    this.materCostReal_CX_month_by.InnerText = cost[7][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //}
                //cost[0][0]昨日石桥小修计划cost[0][1]昨日石桥小修实际
                //cost[1][0]昨日城西小修计划cost[1][1]昨日城西小修实际
                //cost[2][0]昨日转塘小修计划cost[2][1]昨日转塘小修实际
                //cost[3][0]昨日石桥保养计划cost[3][1]昨日石桥保养实际
                //cost[4][0]昨日城西保养计划cost[4][1]昨日城西保养实际
                //cost[5][0]昨日转塘保养计划cost[5][1]昨日转塘保养实际
                //cost[6][0]上月石桥小修计划cost[6][1]上月石桥小修实际
                //cost[7][0]上月城西小修计划cost[7][1]上月城西小修实际
                //cost[8][0]上月转塘小修计划cost[8][1]上月转塘小修实际
                //cost[9][0]上月石桥保养计划cost[9][1]上月石桥保养实际
                //cost[10][0]上月城西保养计划cost[10][1]上月城西保养实际
                //cost[11][0]上月转塘保养计划cost[11][1]上月转塘保养实际
                //else
                //{
                //昨日信息
                string temp = "";
                date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                month = "";
                place = "04";
                this.littFix_SQ_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_SQ_day_xx.InnerText = cost[0][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[0][1] == "0") { temp = "尚未统计"; } else temp = cost[0][1] + " 元,";
                this.materCostReal_SQ_day_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_SQ_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_SQ_day_by.InnerText = cost[3][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_SQ_day_by.InnerText = cost[3][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                place = "26";
                this.littFix_CX_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_CX_day_xx.InnerText = cost[1][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[1][1] == "0") { temp = "尚未统计"; } else temp = cost[1][1] + " 元,";
                this.materCostReal_CX_day_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_CX_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_CX_day_by.InnerText = cost[4][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_CX_day_by.InnerText = cost[4][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                place = "21";
                this.littFix_ZT_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_ZT_day_xx.InnerText = cost[2][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[2][1] == "0") { temp = "尚未统计"; } else temp = cost[2][1] + " 元,";
                this.materCostReal_ZT_day_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_ZT_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_ZT_day_by.InnerText = cost[5][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_ZT_day_by.InnerText = cost[5][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //上月信息
                date = "";
                month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                month = month.Substring(0, 6);
                place = "04";
                this.littFix_SQ_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_SQ_month_xx.InnerText = cost[6][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[6][1] == "0") { temp = "尚未统计"; } else temp = cost[6][1] + " 元,"; 
                this.materCostReal_SQ_month_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_SQ_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_SQ_month_by.InnerText = cost[9][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_SQ_month_by.InnerText = cost[9][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                place = "26";
                this.littFix_CX_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_CX_month_xx.InnerText = cost[7][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[7][1] == "0") { temp = "尚未统计"; } else temp = cost[7][1] + " 元,"; 
                this.materCostReal_CX_month_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_CX_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_CX_month_by.InnerText = cost[10][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_CX_month_by.InnerText = cost[10][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                place = "21";
                this.littFix_ZT_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                this.materCost_ZT_month_xx.InnerText = cost[8][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                if (cost[8][1] == "0") { temp = "尚未统计"; } else temp = cost[8][1] + " 元,"; 
                this.materCostReal_ZT_month_xx.InnerText = temp;//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                this.keepFix_ZT_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                this.materCost_ZT_month_by.InnerText = cost[11][0];//webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                this.materCostReal_ZT_month_by.InnerText = cost[11][1];//webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //}


                //string place = "";
                //string month = "";
                //bool type = false;//false小修 true保养
                ////昨日信息
                //date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                //month = "";
                //place = "04";
                //this.littFix_SQ_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_SQ_day_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_SQ_day_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_SQ_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_SQ_day_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_SQ_day_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //place = "26";
                //this.littFix_CX_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_CX_day_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_CX_day_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_CX_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_CX_day_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_CX_day_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //place = "21";
                //this.littFix_ZT_day.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_ZT_day_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_ZT_day_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_ZT_day.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_ZT_day_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_ZT_day_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                ////上月信息
                //date = "";
                //month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
                //month = month.Substring(0, 6);
                ////month = DateTime.Now.ToString("MM-DD");
                //place = "04";
                //this.littFix_SQ_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_SQ_month_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_SQ_month_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_SQ_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_SQ_month_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_SQ_month_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //place = "26";
                //this.littFix_CX_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_CX_month_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_CX_month_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_CX_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_CX_month_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_CX_month_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //place = "21";
                //this.littFix_ZT_month.InnerText = webBLL.getLittFix(Identity, Flag, place, date, month);
                //type = false;
                //this.materCost_ZT_month_xx.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_ZT_month_xx.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //this.keepFix_ZT_month.InnerText = webBLL.getKeepFix(Identity, Flag, place, date, month);
                //type = true;
                //this.materCost_ZT_month_by.InnerText = webBLL.getMaterCost(Identity, Flag, place, date, month, type);//计划花费
                //this.materCostReal_ZT_month_by.InnerText = webBLL.getMaterCostReal(Identity, Flag, place, date, month, type);//实际花费
                //今日信息
                date = DateTime.Now.ToString("yyyy-MM-dd");
                place = "04";
                type = false;
                this.littFix_SQ_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                type = true;
                this.keepFix_SQ_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                place = "26";
                type = false;
                this.littFix_CX_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                type = true;
                this.keepFix_CX_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                place = "21";
                type = false;
                this.littFix_ZT_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                type = true;
                this.keepFix_ZT_now.InnerText = webBLL.getDateNow(Identity, Flag, place, date, type);
                #endregion

                #region/*人力资源管理系统*/

                #region/*最新人员变动*/
                string[][] result = webBLL.getWorkFlow();
                this.allEmpNum.InnerText = webBLL.getAllEmpNum(Identity, Flag);
                this.conEmpNum.InnerText = webBLL.getConEmpNum(Identity, Flag);
                this.empName1.InnerText = result[0][1];
                this.empAnchor1.HRef = "WorkFlowDetail.aspx?date=&id=" + result[0][0];
                this.empName2.InnerText = result[1][1];
                this.empAnchor2.HRef = "WorkFlowDetail.aspx?date=&id=" + result[1][0];
                this.empName3.InnerText = result[2][1];
                this.empAnchor3.HRef = "WorkFlowDetail.aspx?date=&id=" + result[2][0];
                this.empName4.InnerText = result[3][1];
                this.empAnchor4.HRef = "WorkFlowDetail.aspx?date=&id=" + result[3][0];
                this.empOut1.InnerText = getEmpOut(result[0]);
                this.empOut2.InnerText = getEmpOut(result[1]);
                this.empOut3.InnerText = getEmpOut(result[2]);
                this.empOut4.InnerText = getEmpOut(result[3]);
                this.empIn1.InnerText = getEmpIn(result[0]);
                this.empIn2.InnerText = getEmpIn(result[1]);
                this.empIn3.InnerText = getEmpIn(result[2]);
                this.empIn4.InnerText = getEmpIn(result[3]);
                this.empChangeYesterNum.InnerText = result[4][0];
                this.empChangeTodayNum.InnerText = result[4][1];
                date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                this.empChangeYester.HRef = "WorkFlowDetail.aspx?date=" + date + "&id=";
                date = DateTime.Now.ToString("yyyyMMdd");
                this.empChangeToday.HRef = "WorkFlowDetail.aspx?date=" + date + "&id=";
                #endregion

                #region/*合同变动人员*/
                date = DateTime.Now.ToString("yyyyMMdd");
                string rows = "";
                bool flag = false;
                type = true;
                string[][] contractresult;
                flag = true;//type 为实习与否 flag 为下次与否 
                contractresult = webBLL.getContractDue(date, type, flag, Identity);
                rows = contractresult[0][0];
                if (rows == "0")
                {
                    this.InformalFuture1.InnerText = "______";
                    this.InformalFuture2.InnerText = "______";
                    this.InformalFuture3.InnerText = "______";
                }
                else
                {
                    this.InfomalFutureDate.InnerText = contractresult[1][1];
                    this.InfomalFutureHref.HRef = "ContractMore.aspx?date=" + contractresult[1][1] + "&type=0";
                    switch (rows)
                    {
                        case "": break;
                        case "1": this.InformalFuture1.InnerText = contractresult[1][0]; break;
                        case "2": this.InformalFuture1.InnerText = contractresult[1][0];
                            this.InformalFuture2.InnerText = contractresult[2][0]; break;
                        case "3": this.InformalFuture1.InnerText = contractresult[1][0];
                            this.InformalFuture2.InnerText = contractresult[2][0];
                            this.InformalFuture3.InnerText = contractresult[3][0] + " 等"; break;
                    }
                }
                flag = false; type = true;
                contractresult = webBLL.getContractDue(date, type, flag, Identity);
                rows = contractresult[0][0];
                this.InfomalTodayDate.InnerText = DateTime.Now.ToString("yyyyMMdd");
                this.InfomalTodayHref.HRef = "ContractMore.aspx?date=" + DateTime.Now.ToString("yyyyMMdd") + "&type=0";
                switch (rows)
                {
                    case "0": this.InformalToday1.InnerText = "无"; break;
                    case "1": this.InformalToday1.InnerText = contractresult[1][0]; break;
                    case "2": this.InformalToday1.InnerText = contractresult[1][0];
                        this.InformalToday2.InnerText = contractresult[2][0]; break;
                    case "3": this.InformalToday1.InnerText = contractresult[1][0];
                        this.InformalToday2.InnerText = contractresult[2][0];
                        this.InformalToday3.InnerText = contractresult[3][0] + " 等"; break;
                }
                flag = true; type = false;
                contractresult = webBLL.getContractDue(date, type, flag, Identity);
                rows = contractresult[0][0];
                if (rows == "0")
                {
                    this.FormalFuture1.InnerText = "______";
                    this.FormalFuture2.InnerText = "______";
                    this.FormalFuture3.InnerText = "______";
                }
                else
                {
                    this.FomalFutureDate.InnerText = contractresult[1][1];
                    this.FomalFutureHref.HRef = "ContractMore.aspx?date=" + contractresult[1][1] + "&type=1";
                    switch (rows)
                    {
                        case "":  break;
                        case "1": this.FormalFuture1.InnerText = contractresult[1][0]; break;
                        case "2": this.FormalFuture1.InnerText = contractresult[1][0];
                            this.FormalFuture2.InnerText = contractresult[2][0]; break;
                        case "3": this.FormalFuture1.InnerText = contractresult[1][0];
                            this.FormalFuture2.InnerText = contractresult[2][0];
                            this.FormalFuture3.InnerText = contractresult[3][0] + " 等"; break;
                    }
                }
                flag = false; type = false;
                contractresult = webBLL.getContractDue(date, type, flag, Identity);
                rows = contractresult[0][0];
                this.FomalTodayDate.InnerText = DateTime.Now.ToString("yyyyMMdd");
                this.FomalTodayHref.HRef = "ContractMore.aspx?date=" + DateTime.Now.ToString("yyyyMMdd") + "&type=1";
                switch (rows)
                {
                    case "0": this.FormalToday1.InnerText = "无"; break;
                    case "1": this.FormalToday1.InnerText = contractresult[1][0]; break;
                    case "2": this.FormalToday1.InnerText = contractresult[1][0];
                        this.FormalToday2.InnerText = contractresult[2][0]; break;
                    case "3": this.FormalToday1.InnerText = contractresult[1][0];
                        this.FormalToday2.InnerText = contractresult[2][0];
                        this.FormalToday3.InnerText = contractresult[3][0] + " 等"; break;
                }
                #endregion

                #region/*今日寿星*/
                this.BirthToday.Text = DateTime.Now.ToShortDateString() + " 寿星榜";
                DataTable birthday = webBLL.getbirthdate(Identity);
                if (birthday != null)
                {
                    this.birthday.DataSource = birthday;
                    this.birthday.DataBind();
                }
                #endregion

                #endregion
            }
        }


        /// <summary>
        /// 人员调动状况获取
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string getEmpOut(string[] array)
        {
            string result = "";
            if ("解除合同".Equals(array[2]))
            {
                result += "解除合同";
            }
            else
            {
                result += array[2];
            }
            return result;
        }

        /// <summary>
        /// 人员调动状况获取
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string getEmpIn(string[] array)
        {
            string result = "";
            if ("解除合同".Equals(array[2]))
            {
                result += "解除合同";
            }
            else
            {
                result += array[3];
            }
            return result;
        }

        /// <summary>
        /// 车辆查询按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void carQueryClick(object sender, EventArgs e)
        {
            //Response.Write("<script LANGUAGE=JavaScript >" +" alert('该用户不存在，请先注册！');" +" window.location=('Login.aspx');" +"</script>");//弹出消息框+response重定向
            //bool flag = false;
            //if (this.busID.Value == null || this.busID.Value.Trim() == "")
            //{
            //    this.Label2.Text = "请填写车辆名！";
            //}
            //if (flag == false)
            //{
            //    Response.Redirect("~/carQuery.aspx?BUSID=" + Server.UrlEncode(this.busID.Value));
            //}
        }

        /// <summary>
        /// 维修查询按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repairQueryClick(object sender, EventArgs e)
        {
            bool flag = false;
            if (this.repairID.Value == null || this.repairID.Value.Trim() == "" || this.repairID.Value.Trim() == "填写车号")
            {
                Response.Write("<script LANGUAGE=JavaScript >" + " alert('请填写车号！');" + "</script>");
            }
            else flag = true;
            if (flag == true)
            {
                Response.Redirect("~/RepairInfo.aspx?repairID=" + Server.UrlEncode(this.repairID.Value));
            }
        }

        /// <summary>
        /// 人员查询按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void empQueryClick(object sender, EventArgs e)
        {
            bool flag = false;
            if (this.empName.Value == null || this.empName.Value.Trim() == "" || this.empName.Value.Trim() == "填写人名")
            {
                Response.Write("<script LANGUAGE=JavaScript >" + " alert('请填写人名！');" + "</script>");
                flag = true;
            }
            if (flag == false)
            {
                Response.Redirect("~/EmpInfo.aspx?name=" + Server.UrlEncode(this.empName.Value));
            }
        }

    }
}