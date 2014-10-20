using System;
using System.Collections.Generic;
using System.Text;
using hdcweb.soc.DAL;
using System.Web;
using System.Data;

namespace hdcweb.soc.BLL
{
    public class webBLL
    {

        #region 欢迎信息用于模板页Site.Master
        /// <summary>
        /// 用户核实方法返回ls[0]empname ls[1]depid ls[2]匹配的用户数量
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static List<string> verifyIdentify(string username, string password)
        {
            try
            {
                List<string> ls = new List<string>();
                string sql = "SELECT count(*),empname,depid,ownerdep FROM web_logininfo@ehr where empid='" + username + "' and userpassword='" + password + "' group by depid,empname,ownerdep";
                DataTable dt = webDAL.selectDataTable(sql);
                if (dt.Rows.Count == 0)
                { ls.Add("nomatch"); }
                else
                {
                    ls.Add(dt.Rows[0]["empname"].ToString());
                    ls.Add(dt.Rows[0]["depid"].ToString());
                    ls.Add(dt.Rows[0]["count(*)"].ToString());
                    ls.Add(dt.Rows[0]["ownerdep"].ToString());
                }
                return ls;
            }
            catch
            {
                return null;
            }
        }
        public static string getEmpNum()
        {
            try
            {
                string sql = "SELECT * FROM web_EmpInfo@ehr";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getBikeNum()
        {
            try
            {
                string sql = "SELECT SUM(bikecount) from WEB_STATION@cbls";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getBusNum()
        {
            try
            {
                string sql = "select count(distinct busid) from dims.LINEBUS t where lineid<>0";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getLineNum()
        {
            try
            {
                string sql = "select count(distinct lineid) from dims.LINEBUS t where lineid<>0";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public static string getparkNum()
        //{
        //    try
        //    {
        //        string sql = "";
        //        return webDAL.selectFirstData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public static string GetClientIp()
        {
            string l_ret = string.Empty;

            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);

            if (string.IsNullOrEmpty(l_ret))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return l_ret;
        }

        #endregion

        #region 各项信息汇总用于index.aspx

        #region 自行车租赁信息
        /// <summary>
        /// 获取站点总数
        /// </summary>
        /// <returns></returns>
        public static string getBikeStation()
        {
            try
            {
                string sql = "select count(*) from web_station@cbls";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取昨天租车总数
        /// </summary>
        /// <returns></returns>
        public static string getBikeRentYesterday()
        {
            try
            {
                string sql = "select sum(t.rentcount) from b_stationrentcount_yestoday@cbls t";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取本月租车总数
        /// </summary>
        /// <returns></returns>
        public static string getBikeRentThisMonth()
        {
            try
            {
                DateTime dt = DateTime.Now;
                string date = dt.Year.ToString() + (dt.Month < 10 ? ("0" + dt.Month.ToString()) : dt.Month.ToString());
                string sql = "select sum(t.rentcount) from b_stationrentcount_month@cbls t where t.rentmonth='" + date + "'";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取上月租车总数
        /// </summary>
        /// <returns></returns>
        public static string getBikeRentLastMonth()
        {
            try
            {
                DateTime dt = DateTime.Now.AddMonths(-1);
                string date = dt.Year.ToString() + (dt.Month < 10 ? ("0" + dt.Month.ToString()) : dt.Month.ToString());
                string sql = "select sum(t.rentcount) from b_stationrentcount_month@cbls t where t.rentmonth='" + date + "'";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取上月前5个租车总数最多的 站点名称和租车总数
        /// </summary>
        /// <returns></returns>
        public static DataTable getTop5BikeRentYesterday()
        {
            try
            {
                string sql = "SELECT * FROM (SELECT ROWNUM 排名,A.* FROM( select b.stationname 站点名, sum(a.rentcount) 租车数 from b_stationrentcount_yestoday@cbls a, WEB_STATION@cbls b where a.stationid = b.stationid group by b.stationname  order by sum(a.rentcount) desc) A where ROWNUM <= 5)WHERE 排名 >= 1";
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 停车场信息
        /// <summary>
        /// 获取场内车辆数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getCarNumIn(string date)
        {
            try
            {
                //string sql = "SELECT T.PARKNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                //Console.WriteLine(sql);
                //Console.WriteLine(webDAL.selectFirstData(sql));
                string sql = "select count(*) from web_pimsdispatchwarning t where t.ADJUSTDATE = TO_DATE('" + date + "','YYYY-MM-DD') AND t.outtime IS NULL";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取总出场车辆
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getCarNumOut(string date)
        {
            try
            {
                //string sql = "SELECT T.OUTNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                string sql = "select count(*) from web_pimsdispatchwarning t where t.ADJUSTDATE = TO_DATE('" + date + "','YYYY-MM-DD') AND t.outtime IS NOT NULL";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取延误出场车辆
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getCarLate(string date)
        {
            try
            {
                //string sql = "SELECT T.OUTNUM_LATE FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                string sql = "select count(*) from web_pimsdispatchwarning t where t.ADJUSTDATE = TO_DATE('" + date + "','YYYY-MM-DD') AND t.OUTDELAY <= 0";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取准时出场率
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getCarInTime(string date)
        {
            try
            {
                string sql = "SELECT ROUND((T.OUTNUM_REAL - T.OUTNUM_LATE)/T.OUTNUM_REAL,4)*100||'%' FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取洗车次数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getWash(string date)
        {
            try
            {
                string sql = "SELECT T.PARKNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取钱袋数目
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getCollectNum(string date)
        {
            try
            {
                string sql = "SELECT T.COLLECTNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取加油次数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string getOilNum(string date)
        {
            try
            {
                string sql = "SELECT T.OILNUM FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region 修理实时结算信息

        /// <summary>
        /// 获取昨日/上月小修数量
        /// </summary>
        /// <param name="identity">用户depid</param>
        /// <param name="flag">用户权限</param>
        /// <param name="place">维修厂id【前端相关】</param>
        /// <param name="date">昨日日期</param>
        /// <param name="month">上月月份</param>
        /// <returns></returns>
        public static string getLittFix(string identity, int flag, string place, string date, string month)//flag为用户权限0子1集团2修理 place为前端显示的修理公司
        {
            try
            {
                if (month == "")//昨日
                {
                    if (flag == 1 || flag == 2)//不需要用到identity 为汇总信息//identity=place 
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t,hdc.g_depinfo d where t.workcorpname= d.depname and t.repairtypeid=10 and t.balancedate='" + date + "' and d.depid='" + place + "'";
                        return webDAL.selectFirstData(sql);
                    }
                    else//都需要
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t , hdc.g_depinfo d1 ,hdc.g_depinfo d2 where t.workcorpname= d1.depname and t.repairtypeid=10 and t.balancedate='" + date + "' and d1.depid= '" + place + "' and t.buscorpname= d2.depname and d2.depid='" + identity + "'";
                        return webDAL.selectFirstData(sql);
                    }
                }
                else//上月
                {
                    if (flag == 1 || flag == 2)//不需要用到identity 为汇总信息//identity=place
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t,hdc.g_depinfo d where t.workcorpname= d.depname and t.repairtypeid=10 and t.balancedate like '" + month + "%' and d.depid='" + place + "'";
                        return webDAL.selectFirstData(sql);
                    }
                    else//都需要
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t , hdc.g_depinfo d1 ,hdc.g_depinfo d2 where t.workcorpname= d1.depname and t.repairtypeid=10 and t.balancedate like '" + month + "%' and d1.depid= '" + place + "' and t.buscorpname= d2.depname and d2.depid='" + identity + "'";
                        return webDAL.selectFirstData(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取昨日/上月保养数量
        /// </summary>
        /// <param name="identity">用户depid</param>
        /// <param name="flag">用户权限</param>
        /// <param name="place">维修厂id【前端相关】</param>
        /// <param name="date">昨日日期</param>
        /// <param name="month">上月月份</param>
        /// <returns></returns>
        public static string getKeepFix(string identity, int flag, string place, string date, string month)//rank为当前登录用户的Identity flag为用户权限0子1集团2修理 place为前端显示的修理公司
        {
            try
            {
                if (month == "")//昨日
                {
                    if (flag == 1 || flag == 2)//不需要用到identity 为汇总信息//identity=place 
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t,hdc.g_depinfo d where t.workcorpname= d.depname and t.repairtypeid<>10 and t.balancedate='" + date + "' and d.depid='" + place + "'";
                        return webDAL.selectFirstData(sql);
                    }
                    else//都需要
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t , hdc.g_depinfo d1 ,hdc.g_depinfo d2 where t.workcorpname= d1.depname and t.repairtypeid<>10 and t.balancedate='" + date + "' and d1.depid= '" + place + "' and t.buscorpname= d2.depname and d2.depid='" + identity + "'";
                        return webDAL.selectFirstData(sql);
                    }
                }
                else//上月
                {
                    if (flag == 1 || flag == 2)//不需要用到identity 为汇总信息//identity=place
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t,hdc.g_depinfo d where t.workcorpname= d.depname and t.repairtypeid<>10 and t.balancedate like '" + month + "%' and d.depid='" + place + "'";
                        return webDAL.selectFirstData(sql);
                    }
                    else//都需要
                    {
                        string sql = "select count(distinct t.busshowid) from hdc.web_sheet t , hdc.g_depinfo d1 ,hdc.g_depinfo d2 where t.workcorpname= d1.depname and t.repairtypeid<>10 and t.balancedate like '" + month + "%' and d1.depid= '" + place + "' and t.buscorpname= d2.depname and d2.depid='" + identity + "'";
                        return webDAL.selectFirstData(sql);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 定额材料费获取
        /// </summary>
        /// <param name="identity">用户depid</param>
        /// <param name="flag">用户权限</param>
        /// <param name="place">维修厂id[前端相关]</param>
        /// <param name="date">昨日日期</param>
        /// <param name="month">上月月份</param>
        /// <param name="type">种类[小修还是保养]0小修1保养</param>
        /// <returns></returns>
        public static string getMaterCost(string identity, int flag, string place, string date, string month, bool type)
        {//and t.balancedate like to_char(TRUNC(SYSDATE, 'mm') - 1,'yyyymm') ||'%'
            try
            {
                if (month == "")//昨日定额
                {
                    if (type == false)//小修
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate = '" + date + "' " +
                                         "and t.repairtypeid=10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   = '" + date + "' " +
                                         "and t.repairtypeid  =10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                    else//保养
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate = '" + date + "' " +
                                         "and t.repairtypeid<>10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   = '" + date + "' " +
                                         "and t.repairtypeid  <>10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                }
                else//上月定额
                {
                    if (type == false)//小修
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate like '" + month + "%' " +
                                         "and t.repairtypeid=10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   like '" + month + "%' " +
                                         "and t.repairtypeid  =10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                    else//保养
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate like '" + month + "%' " +
                                         "and t.repairtypeid<>10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.matlfee+t.assfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   like '" + month + "%' " +
                                         "and t.repairtypeid  <>10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 实际材料费获取
        /// </summary>
        /// <param name="identity">用户depid</param>
        /// <param name="flag">用户权限</param>
        /// <param name="place">维修厂id【前端相关】</param>
        /// <param name="date">昨日日期</param>
        /// <param name="month">上月月份</param>
        /// <param name="type">种类[小修还是保养]0小修1保养</param>
        /// <returns></returns>
        public static string getMaterCostReal(string identity, int flag, string place, string date, string month, bool type)
        {
            try
            {
                if (month == "")//昨日实际
                {
                    if (type == false)//小修
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate = '" + date + "' " +
                                         "and t.repairtypeid=10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   = '" + date + "' " +
                                         "and t.repairtypeid  =10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                    else//保养
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate = '" + date + "' " +
                                         "and t.repairtypeid<>10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   = '" + date + "' " +
                                         "and t.repairtypeid  <>10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                }
                else//上月实际
                {
                    if (type == false)//小修
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate like '" + month + "%' " +
                                         "and t.repairtypeid=10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   like '" + month + "%' " +
                                         "and t.repairtypeid  =10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                    else//保养
                    {
                        if (flag == 1 || flag == 2)//集团或维修厂
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
                                         "and t.balancedate like '" + month + "%' " +
                                         "and t.repairtypeid<>10 and d.depid='" + place + "'";
                            return webDAL.selectFirstData(sql);
                        }
                        else//分公司
                        {
                            string sql = "SELECT sum(t.fixedfee) " +
                                         "FROM hdc.WEB_SHEET t , " +
                                         "  hdc.g_depinfo d1 , " +
                                         "  hdc.g_depinfo d2 " +
                                         "where t.workcorpname= d1.depname " +
                                         "and t.buscorpname   = d2.depname " +
                                         "and t.balancedate   like '" + month + "%' " +
                                         "and t.repairtypeid  <>10 " +
                                         "and d1.depid        ='" + place + "' " +
                                         "and d2.depid        ='" + identity + "' ";
                            return webDAL.selectFirstData(sql);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getDateNow(string Identity, int Flag, string place, string date, bool type)
        {
            string sql = "";
            try
            {
                if (Flag == 1)//总公司
                {
                    sql += "select count(busshowid) from hdc.web_sheeting where workteamid like '" + place + "%' and taketime like '" + date + "%' ";
                    if (!type)//小修
                    {
                        sql += "and repairtypeid=10";
                    }
                    else
                    {
                        sql += "and repairtypeid<>10";
                    }
                }
                else if (Flag == 0)//分公司
                {
                    sql += "select count(busshowid) from hdc.web_sheeting where workteamid like '" + place + "%' and taketime like '" + date + "%' ";
                    if (!type)//小修
                    {
                        sql += "and repairtypeid=10 ";
                    }
                    else
                    {
                        sql += "and repairtypeid<>10 ";
                    }
                    sql += "and busteamid like '" + Identity + "%'";
                }
                else//修理公司
                {
                    sql += "select count(busshowid) from hdc.web_sheeting where workteamid like '" + Identity + "%' and taketime like '" + date + "%' ";
                    if (!type)//小修
                    {
                        sql += "and repairtypeid=10";
                    }
                    else
                    {
                        sql += "and repairtypeid<>10";
                    }
                }
                return webDAL.selectFirstData(sql);
            }
            catch
            {
                return null;
            }

        }

        ///// <summary>
        ///// 定额材料费获取
        ///// </summary>
        ///// <param name="identity">用户depid</param>
        ///// <param name="flag">用户权限</param>
        ///// <param name="place">维修厂id[前端相关]</param>
        ///// <param name="date">昨日日期</param>
        ///// <param name="month">上月月份</param>
        ///// <param name="type">种类[小修还是保养]0小修1保养</param>
        ///// <returns></returns>
        //public static string getMaterCost(string identity, int flag, string place, string date, string month, bool type)
        //{//and t.balancedate like to_char(TRUNC(SYSDATE, 'mm') - 1,'yyyymm') ||'%'
        //    try
        //    {
        //        if (month == "")//昨日定额
        //        {
        //            if (type == false)//小修
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate = '" + date + "' " +
        //                                 "and t.repairtypeid=10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   = '" + date + "' " +
        //                                 "and t.repairtypeid  =10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //            else//保养
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate = '" + date + "' " +
        //                                 "and t.repairtypeid<>10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   = '" + date + "' " +
        //                                 "and t.repairtypeid  <>10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //        }
        //        else//上月定额
        //        {
        //            if (type == false)//小修
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate like '" + month + "%' " +
        //                                 "and t.repairtypeid=10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   like '" + month + "%' " +
        //                                 "and t.repairtypeid  =10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //            else//保养
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate like '" + month + "%' " +
        //                                 "and t.repairtypeid<>10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.matlfee+t.assfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   like '" + month + "%' " +
        //                                 "and t.repairtypeid  <>10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 实际材料费获取
        ///// </summary>
        ///// <param name="identity">用户depid</param>
        ///// <param name="flag">用户权限</param>
        ///// <param name="place">维修厂id【前端相关】</param>
        ///// <param name="date">昨日日期</param>
        ///// <param name="month">上月月份</param>
        ///// <param name="type">种类[小修还是保养]0小修1保养</param>
        ///// <returns></returns>
        //public static string getMaterCostReal(string identity, int flag, string place, string date, string month, bool type)
        //{
        //    try
        //    {
        //        if (month == "")//昨日实际
        //        {
        //            if (type == false)//小修
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate = '" + date + "' " +
        //                                 "and t.repairtypeid=10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   = '" + date + "' " +
        //                                 "and t.repairtypeid  =10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //            else//保养
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate = '" + date + "' " +
        //                                 "and t.repairtypeid<>10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   = '" + date + "' " +
        //                                 "and t.repairtypeid  <>10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //        }
        //        else//上月实际
        //        {
        //            if (type == false)//小修
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate like '" + month + "%' " +
        //                                 "and t.repairtypeid=10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   like '" + month + "%' " +
        //                                 "and t.repairtypeid  =10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //            else//保养
        //            {
        //                if (flag == 1 || flag == 2)//集团或维修厂
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , hdc.g_depinfo d where t.workcorpname= d.depname " +
        //                                 "and t.balancedate like '" + month + "%' " +
        //                                 "and t.repairtypeid<>10 and d.depid='" + place + "'";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //                else//分公司
        //                {
        //                    string sql = "SELECT sum(t.fixedfee) " +
        //                                 "FROM hdc.WEB_SHEET t , " +
        //                                 "  hdc.g_depinfo d1 , " +
        //                                 "  hdc.g_depinfo d2 " +
        //                                 "where t.workcorpname= d1.depname " +
        //                                 "and t.buscorpname   = d2.depname " +
        //                                 "and t.balancedate   like '" + month + "%' " +
        //                                 "and t.repairtypeid  <>10 " +
        //                                 "and d1.depid        ='" + place + "' " +
        //                                 "and d2.depid        ='" + identity + "' ";
        //                    return webDAL.selectFirstData(sql);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    } 
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity">用户depid</param>
        /// <param name="flag">用户权限</param>
        /// <param name="date">昨日日期</param>
        /// <param name="month">上月月份</param>
        /// <returns></returns>
        public static string[][] getCost(string identity, int flag, string date, string month)
        {
            int i = 0;
            string sql = "SELECT sum(t.matlfee + t.assfee) 计划, sum(t.fixedfee) 实际 FROM hdc.WEB_SHEET t, hdc.g_depinfo d WHERE t.workcorpname = d.depname AND t.balancedate = '" + date + "' AND t.repairtypeid = 10 AND d.depid IN ('04', '26', '21') GROUP BY d.depid UNION ALL SELECT sum(t.matlfee + t.assfee), sum(t.fixedfee) FROM hdc.WEB_SHEET t, hdc.g_depinfo d WHERE t.workcorpname = d.depname AND t.balancedate = '" + date + "' AND t.repairtypeid <> 10 AND d.depid IN ('04', '26', '21') GROUP BY d.depid UNION ALL SELECT sum(t.matlfee + t.assfee), sum(t.fixedfee) FROM hdc.WEB_SHEET t, hdc.g_depinfo d WHERE t.workcorpname = d.depname AND t.balancedate LIKE '" + month + "%' AND t.repairtypeid = 10 AND d.depid IN ('04', '26', '21') GROUP BY d.depid UNION ALL SELECT sum(t.matlfee + t.assfee), sum(t.fixedfee) FROM hdc.WEB_SHEET t, hdc.g_depinfo d WHERE t.workcorpname = d.depname AND t.balancedate LIKE '" + month + "%' AND t.repairtypeid <> 10 AND d.depid IN ('04', '26', '21') GROUP BY d.depid";
            DataTable dt = webDAL.selectDataTable(sql);
            i = dt.Rows.Count;
            string[][] result = new string[i][];//若
            for (int j = 0; j < i; j++)
            {
                DataRow row = dt.Rows[j];
                result[j] = new string[2] { row["计划"].ToString(), row["实际"].ToString() };
            }
            return result;
        }

        #endregion

        #region 人力资源管理系统
        /// <summary>
        /// 获取 总公司为总人数 分公司和修理公司为公司内员工数
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string getAllEmpNum(string identity, int flag)
        {
            try
            {
                if (flag == 0 || flag == 2)
                {
                    string sql = "select count(*) from web_EmpInfo@ehr v join hdc.g_depinfo d on v.depid= d.depid where d.parentid=" + identity + "";
                    return webDAL.selectFirstData(sql);
                }
                else
                {
                    string sql = "SELECT COUNT(*) FROM web_EmpInfo@ehr t";
                    return webDAL.selectFirstData(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 有劳工合同员工
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string getConEmpNum(string identity, int flag)
        {
            try
            {
                if (flag == 0)
                {
                    string sql = "SELECT COUNT(*) FROM web_EmpInfo@ehr v join hdc.g_depinfo d on v.depid=d.depid WHERE v.empid IN (SELECT empid FROM web_contractinfo@ehr) and d.parentid=" + identity + " ";
                    return webDAL.selectFirstData(sql);
                }
                else
                {
                    string sql = "SELECT COUNT(*) FROM web_EmpInfo@ehr t WHERE t.empid IN (SELECT empid FROM web_contractinfo@ehr)";
                    return webDAL.selectFirstData(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string[][] getWorkFlow()
        {
            try
            {
                string sql = "SELECT t1.workflowtype, t1.empname, t1.content, t1.workflowid FROM (SELECT * FROM WEB_WORKFLOW@EHR T ORDER BY T.OPTTIME DESC) t1 WHERE ROWNUM < 5";
                DataTable dt = webDAL.selectDataTable(sql);
                string[][] result = new string[5][];
                for (int i = 0; i < 4; i++)
                {
                    DataRow row = dt.Rows[i];

                    if ("解除合同".Equals(row[0]) || "终止合同".Equals(row[0]))
                    {
                        result[i] = new string[3] { row[3].ToString(), row[1].ToString(), "解除合同" };
                    }
                    else
                    {
                        string from = null;
                        string to = null;
                        bool flag1 = false;
                        bool flag2 = false;
                        string[] s = row[2].ToString().Split(new string[] { ";", "->" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < s[0].Length; j++)
                        {
                            if (s[0][j].ToString() == ".")
                            {
                                from = s[0].Split(new char[] { '.' })[1];
                                //to = s[1].Split(new char[] { '.' })[1];
                                flag1 = true;
                                //result[i] = new string[4] { row[3].ToString(), row[1].ToString(), from, to };
                                continue;
                            }
                        }
                        if (flag1 == false)
                        {
                            from = s[0];
                        }
                        for (int j = 0; j < s[1].Length; j++)
                        {
                            if (s[1][j].ToString() == ".")
                            {
                                to = s[1].Split(new char[] { '.' })[1];
                                flag2 = true;
                                continue;
                            }
                        }
                        if (flag2 == false)
                        {
                            to = s[1];//.Split(new char[] { '.' })[1];
                        }
                        result[i] = new string[4] { row["workflowid"].ToString(), row["empname"].ToString(), from, to };
                    }
                }
                string yester = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                sql = "SELECT count(*) FROM WEB_WORKFLOW@EHR T where T.opttime like'" + yester + "%' union all SELECT count(*) FROM WEB_WORKFLOW@EHR T where T.opttime like'" + today + "%'";
                dt = webDAL.selectDataTable(sql);
                result[4] = new string[2] { dt.Rows[0].ItemArray[0].ToString(), dt.Rows[1].ItemArray[0].ToString() };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取今日/未来有合同到期的一天（页面上仅显示3个名字最后一个为日期）
        /// </summary>
        /// <param name="date">日期格式‘日(2)-_月(1-2) -年(2)’</param>
        /// <param name="type">1为实习合同0为正式合同</param>
        /// <param name="flag">1为未来0为今日</param>
        /// <param name="identity">1为未来0为今日</param>
        /// <returns></returns>
        public static string[][] getContractDue(string date, bool type, bool flag, string identity)
        {
            string[][] result = new string[4][];
            string sql = "";
            if (identity == "00")
            {
                if (flag)//未来
                {
                    if (type)//实习合同//to_char(probationenddate,'yyyymmdd') as thisdate 
                    {
                        sql = "select e.empname ,to_char(probationenddate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con " +
                            "where e.empid             =con.empid " +
                            "and con.probationenddate in " +
                            "  (select min(probationenddate) " +
                            "  from web_contractinfo@ehr con " +
                            "  where to_char(probationenddate,'yyyyMMdd') > '" + date + "' " +
                            "  ) " +
                            "order by probationenddate";
                    }
                    else//正式合同
                    {
                        sql = "select e.empname ,to_char(terminatedate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con " +
                            "where e.empid          =con.empid " +
                            "and con.terminatedate in " +
                            "  (select min(terminatedate) " +
                            "  from web_contractinfo@ehr con " +
                            "  where to_char(terminatedate,'yyyyMMdd') > '" + date + "' " +
                            "  ) " +
                            "order by terminatedate";
                    }
                }
                else//今日
                {
                    if (type)//实习合同
                    {
                        sql = "select e.empname,to_char(probationenddate,'yyyyMMdd') as thisdate from web_EmpInfo@ehr e,web_contractinfo@ehr con where e.empid =con.empid and to_char(probationenddate,'yyyyMMdd')='" + date + "' ";
                    }
                    else//正式合同
                    {
                        sql = "select e.empname,to_char(terminatedate,'yyyyMMdd') as thisdate from web_EmpInfo@ehr e,web_contractinfo@ehr con where e.empid =con.empid and to_char(terminatedate,'yyyyMMdd')='" + date + "' ";
                    }
                }
            }
            else//没总公司权限
            {
                if (flag)//未来
                {
                    if (type)//实习合同//to_char(probationenddate,'yyyymmdd') as thisdate 
                    {
                        sql = "select e.empname , " +
                            "  to_char(probationenddate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "where e.empid             =con.empid " +
                            "and con.probationenddate in " +
                            "  (select min(probationenddate)  " +
                            "  from web_EmpInfo@ehr e,web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "  where con.probationenddate > '" + date + "' " +
                            "  and e.empid             =con.empid " +
                            "  and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "' " +
                            "  ) " +
                            "and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "'";
                    }
                    else//正式合同
                    {
                        sql = "select e.empname , " +
                            "  to_char(terminatedate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "where e.empid             =con.empid " +
                            "and con.terminatedate in " +
                            "  (select min(terminatedate)  " +
                            "  from web_EmpInfo@ehr e,web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "  where con.terminatedate > '" + date + "' " +
                            "  and e.empid             =con.empid " +
                            "  and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "' " +
                            "  ) " +
                            "and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "'";
                    }
                }
                else//今日
                {
                    if (type)//实习合同
                    {
                        sql = "select e.empname , " +
                            "  to_char(probationenddate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "where e.empid             =con.empid " +
                            "and con.probationenddate in " +
                            "  (select min(probationenddate)  " +
                            "  from web_EmpInfo@ehr e,web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "  where con.probationenddate = '" + date + "' " +
                            "  and e.empid             =con.empid " +
                            "  and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "' " +
                            "  ) " +
                            "and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "'";
                    }
                    else//正式合同
                    {
                        sql = "select e.empname , " +
                            "  to_char(terminatedate,'yyyymmdd') as thisdate " +
                            "from web_EmpInfo@ehr e, " +
                            "  web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "where e.empid             =con.empid " +
                            "and con.terminatedate in " +
                            "  (select min(terminatedate)  " +
                            "  from web_EmpInfo@ehr e,web_contractinfo@ehr con, " +
                            "  hdc.g_depinfo d " +
                            "  where con.terminatedate = '" + date + "' " +
                            "  and e.empid             =con.empid " +
                            "  and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "' " +
                            "  ) " +
                            "and e.ownerdep=d.depname " +
                            "and d.depid   ='" + identity + "'";
                    }
                }
            }
            DataTable dt = webDAL.selectDataTable(sql);
            int temp = dt.Rows.Count + 1;
            if (temp > 4)
                temp = 4;
            for (int i = 1; i < temp; i++)
            {
                DataRow row = dt.Rows[i - 1];
                result[i] = new string[2] { row["empname"].ToString(), row["thisdate"].ToString() };
            }
            result[0] = new string[2] { (temp - 1).ToString(), "" };
            return result;
        }

        /// <summary>
        /// 获取当天生日的人
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static DataTable getbirthdate(string identity)
        {
            try
            {
                string date = System.DateTime.Now.ToString("MM-dd");
                //string sql = "select ownerdep 所属公司, empname 姓名 from web_logininfo@ehr where (depid like '" + identity + "__' or depid='" + identity + "') and birthday like '%" + date + "' and ownerdep in (select depname from hdc.g_depinfo where depid = '" + identity + "')";
                //string sql = "select empid 工号, ownerdep 所属公司, empname 姓名 from web_logininfo@ehr where birthday like '%" + date + "' and ownerdep in (select depname from hdc.g_depinfo where depid = '" + identity + "')";
                string sql = "select empid 工号, ownerdep 所属公司, empname 姓名 from web_logininfo@ehr where  depid like '" + identity + "__' and birthday like '%" + date + "'";
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region repairinfo.aspx
        /// <summary>
        /// 车号查询详细
        /// </summary>
        /// <param name="req">车号</param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static DataTable getRepairInfo(string req, bool sign)
        {
            try
            {
                if (sign == false)
                {
                    string sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + req + "' order by balancemonth desc, amount";
                    DataTable d = webDAL.selectDataTable(sql);
                    return d;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 车号查询详细，翻页
        /// </summary>
        /// <param name="upperlimit">翻页上限</param>
        /// <param name="lowlimit">翻页下限</param>
        /// <param name="req">车号</param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static DataTable getRepairInfo(int upperlimit, int lowlimit, string req, bool sign)
        {
            try
            {
                if (sign == false)
                {
                    string sql = "SELECT * FROM (SELECT  ROWNUM 序号,A.* FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + req + "' order by balancemonth desc, amount) A WHERE ROWNUM <= " + lowlimit + ")WHERE 序号 >= " + upperlimit;
                    return webDAL.selectDataTable(sql);
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据车号返回维修类别
        /// </summary>
        /// <param name="busid"></param>
        /// <returns></returns>
        public static DataTable getRepairType(string busid)
        {
            try
            {
                string sql = "select itemvalue from hdc.g_dic where dicname = '维护修理类别' and itemid in (select DISTINCT repairtype from hdc.s_monthreport where busid = '" + busid + "' and amount > 0)";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据车号返回维修年月
        /// </summary>
        /// <param name="busid"></param>
        /// <returns></returns>
        public static DataTable getRepairMonth(string busid)
        {
            try
            {
                string sql = "select distinct balancemonth from hdc.s_monthreport where busid = '" + busid + "' and amount > 0 order by balancemonth desc";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据车号.类别和年月筛选信息
        /// </summary>
        /// <param name="busid">车号</param>
        /// <param name="index1">类别选项索引</param>
        /// <param name="type">类别</param>
        /// <param name="index2">年份选项索引</param>
        /// <param name="month">年份</param>
        /// <param name="arraytype">排序类别</param>
        /// <returns></returns>
        public static DataTable filtrateRepairInfo(string busid, int index1, string type, int index2, string month, string arraytype)
        {
            string sql = "";
            if (index1 != 0 && index2 != 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by amount desc";
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by fixedfee desc";
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by 实际费用 desc";
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by milesum desc";
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by balancemonth desc";
                        }
                        break;
                }
            }
            else if (index1 == 0 && index2 != 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by amount desc";
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by fixedfee desc";
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by 实际费用 desc";
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by milesum desc";
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by balancemonth desc";
                        }
                        break;
                }
            }
            else if (index1 != 0 && index2 == 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by amount desc";
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by fixedfee desc";
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by 实际费用 desc";
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by milesum desc";
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by balancemonth desc";
                        }
                        break;
                }
            }
            else
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by amount desc";
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by fixedfee desc, amount";
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by 实际费用 desc, amount";
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by milesum desc, amount";
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by balancemonth desc,";
                        }
                        break;
                }
            }
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据车号.类别和年月筛选信息，翻页
        /// </summary>
        /// <param name="busid">车号</param>
        /// <param name="index1">类别选项索引</param>
        /// <param name="type">类别</param>
        /// <param name="index2">年份选项索引</param>
        /// <param name="month">年份</param>
        /// <param name="arraytype">排序类别</param>
        /// <returns></returns>
        public static DataTable filtrateRepairInfo(int upperlimit, int lowlimit, string busid, int index1, string type, int index2, string month, string arraytype)
        {
            string sql = "";
            if (index1 != 0 && index2 != 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by amount desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by fixedfee desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by 实际费用 desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by milesum desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by balancemonth desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                }
            }
            else if (index1 == 0 && index2 != 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by amount desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by fixedfee desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by 实际费用 desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by milesum desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by balancemonth desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                }
            }
            else if (index1 != 0 && index2 == 0)
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by amount desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by fixedfee desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by 实际费用 desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by milesum desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by balancemonth desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                }
            }
            else
            {
                switch (arraytype)
                {
                    case "维修次数":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by amount desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "计划费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by fixedfee desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "实际费用":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by 实际费用 desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "行驶公里":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by milesum desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                    case "维修年份":
                        {
                            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by balancemonth desc) A WHERE ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                        }
                        break;
                }
            }
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region MonthReport.aspx

        public static string GetSumsql(bool datetype, bool repairstyle, string conid, string identity)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            string date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");//昨日
            string month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");//上月
            month = month.Substring(0, 6);
            if (identity == "00")
            {
                sql = "select s.buscorpname 分公司, " +
                    "  count(distinct s.busshowid) 车辆数, " +
                    "  sum(s.assfee + s.matlfee) 定额材料费, " +
                    "  sum(s.fixedfee) 实际材料费 " +
                    "from hdc.web_sheet s " +
                    "where ";
                if (repairstyle)
                {
                    sql += "s.repairtypeid<>10 ";
                }
                else sql += "s.repairtypeid=10 ";
                if (datetype)
                {
                    sql += "and s.balancedate like'" + month + "%' ";
                }
                else sql += "and s.balancedate ='" + date + "' ";
                sql += "and s.workcorpid='" + conid + "' ";
                sql += "group by s.buscorpname";
            }
            return sql;
        }

        /// <summary>
        /// 筛选月报表
        /// </summary>
        /// <param name="datetype">区分昨日上月</param>
        /// <param name="repairstyle">区分小修保养</param>
        /// <param name="conid">修理公司公司ID</param>
        /// <param name="arraytype">排序类别</param>
        /// <param name="identity">登录者公司ID</param>
        /// <returns></returns>
        public static DataTable GetSum(bool datetype, bool repairstyle, string conid, string identity)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            sql = GetSumsql(datetype, repairstyle, conid, identity);
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 筛选月报表，翻页
        /// </summary>
        /// <param name="upperlimit">翻页上限</param>
        /// <param name="lowlimit">翻页下限</param>
        /// <param name="datetype">区分昨日上月</param>
        /// <param name="repairstyle">区分小修保养</param>
        /// <param name="conid">公司ID</param>
        /// <param name="arraytype">排序类别</param>
        /// <param name="identity">登录者公司ID</param>
        /// <returns></returns>
        public static DataTable GetSum(int upperlimit, int lowlimit, bool datetype, bool repairstyle, string conid, string identity)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            sql = GetSumsql(datetype, repairstyle, conid, identity);
            sql = "SELECT * FROM (SELECT ROWNUM 序号, A.* FROM ( " + sql + " ) A WHERE ROWNUM <= " + lowlimit + ") WHERE 序号 >= " + upperlimit + "";
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region MonthDetails.aspx

        /// <summary>
        /// 获取公司ID和名称
        /// </summary>
        /// <returns></returns>
        public static DataTable GetComName()
        {

            try
            {
                string sql = "select depid, DEPNAME from hdc.g_depinfo where depid in (04,26,21) order by depid";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据修理公司ID获取该公司 工段ID和名称
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static DataTable Getrepairid(string a)
        {

            try
            {
                string sql = "select depid, depname from hdc.g_depinfo d  where depname like '___工段' and parentid = '" + a + "' order by depid";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取修理公司ID和名称
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static DataTable GetrepairCom()
        {

            try
            {
                string sql = "select depid, depname from hdc.g_depinfo where depid in( '04', '26', '21' ) order by depid";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 筛选月报表所需的sql语句
        /// </summary>
        /// <param name="datetype">区分昨日上月</param>
        /// <param name="repairstyle">区分小修保养</param>
        /// <param name="conid">修理公司公司ID</param>
        /// <param name="arraytype">排序类别</param>
        /// <param name="identity">登录者公司ID</param>
        /// <returns></returns>
        public static string Getdetailssql(bool datetype, bool repairstyle, string conid, string arraytype, string identity, string request, bool searchtype)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            string date = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");//昨日
            string month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");//上月
            month = month.Substring(0, 6);
            bool flag = false;//flag=true分公司 false总公司或修理公司
            if (identity != "00" && identity != "04" && identity != "26" && identity != "21")
            {
                flag = true;
            }
            sql = "select sheetid          as 订单号, " +
                "  busshowid               as 车号 , " +
                "  buscorpname             as 车队, " +
                "  trouitemname            as 故障原因, " +
                "  sum(m.assfee+m.matlfee) as 总计划费用, " +
                "  sum(m.fixedfee)         as 总实际费用 " +
                "from hdc.web_sheet m , " +
                "  hdc.g_depinfo d1 ";
            if (!datetype)//昨日
            {
                if (flag)
                { sql += ", hdc.g_depinfo d2 "; }
                sql += "where m.workcorpname= d1.depname " +
                        "and balancedate = '" + date + "' ";
            }
            else//上月
            {
                if (flag)
                {
                    sql += ", hdc.g_depinfo d2 ";
                }
                sql += "where m.workcorpname = d1.depname " +
                        "and balancedate like '" + month + "%' ";
            }
            if (flag)
            {
                sql += "and m.buscorpname = d2.depname and d2.depid = '" + identity + "' ";
            }
            if (request != "")
            {
                if (!searchtype)//订单号
                {
                    sql += " and sheetid = '" + request + "' ";
                }
                else
                {
                    sql += " and busshowid = '" + request + "' ";
                }
            }
            if (!repairstyle)//小修
            {
                sql += "and repairtypeid = 10 ";
            }
            else//保养
            {
                sql += "and repairtypeid <> 10 ";
            }
            sql += "and d1.depid='" + conid + "' " +
                "group by busshowid, " +
                "  buscorpname, " +
                "  trouitemname, " +
                "  sheetid ";
            switch (arraytype)
            {
                case "车号":
                    {
                        sql += "order by busshowid";
                    }
                    break;
                case "车队":
                    {
                        sql += "order by buscorpname";
                    }
                    break;
                case "总计划费用":
                    {
                        sql += "order by sum(m.assfee+m.matlfee) desc";
                    }
                    break;
                case "总实际费用":
                    {
                        sql += "order by sum(m.fixedfee) desc";
                    }
                    break;
            }
            return sql;
        }

        /// <summary>
        /// 筛选月报表
        /// </summary>
        /// <param name="datetype">区分昨日上月</param>
        /// <param name="repairstyle">区分小修保养</param>
        /// <param name="conid">修理公司公司ID</param>
        /// <param name="arraytype">排序类别</param>
        /// <param name="identity">登录者公司ID</param>
        /// <returns></returns>
        public static DataTable Getdetails(bool datetype, bool repairstyle, string conid, string arraytype, string identity, string request, bool searchtype)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            sql = Getdetailssql(datetype, repairstyle, conid, arraytype, identity, request, searchtype);
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 筛选月报表，翻页
        /// </summary>
        /// <param name="upperlimit">翻页上限</param>
        /// <param name="lowlimit">翻页下限</param>
        /// <param name="datetype">区分昨日上月</param>
        /// <param name="repairstyle">区分小修保养</param>
        /// <param name="conid">公司ID</param>
        /// <param name="arraytype">排序类别</param>
        /// <param name="identity">登录者公司ID</param>
        /// <returns></returns>
        public static DataTable Getdetails(int upperlimit, int lowlimit, bool datetype, bool repairstyle, string conid, string arraytype, string identity, string request, bool searchtype)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {
            string sql = "";
            sql = Getdetailssql(datetype, repairstyle, conid, arraytype, identity, request, searchtype);
            sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM ( " + sql + " ) A WHERE ROWNUM <= " + lowlimit + ") WHERE RN >= " + upperlimit + "";
            try
            {
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region RepairSum.aspx

        /// <summary>
        /// 修理公司汇总
        /// </summary>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public static DataTable GetForRepair(string identity, int flag, string month, string date)
        {
            try
            {
                string sql = "";
                if (month == "" && date != "")//昨日
                {
                    sql = "SELECT s.workcorpname 修理公司, " +
                        "  sum(s.assfee + s.matlfee) 定额材料费, " +
                        "  sum(s.fixedfee) 实际材料费 " +
                        "FROM hdc.web_sheet s " +
                        "WHERE s.balancedate ='" + date + "' " +
                        "AND s.workcorpid IN (04, 26, 21) ";
                    if (flag == 0)//子公司
                    { sql += "AND s.buscorpid  ='" + identity + "' "; }
                    sql += "GROUP BY s.workcorpname";
                }
                else//上月
                {
                    sql = "SELECT s.workcorpname 修理公司, " +
                        "  sum(s.assfee + s.matlfee) 定额材料费, " +
                        "  sum(s.fixedfee) 实际材料费 " +
                        "FROM hdc.web_sheet s " +
                        "WHERE s.balancedate LIKE '" + month + "%' " +
                        "AND s.workcorpid IN (04, 26, 21) ";
                    if (flag == 0)//子公司
                    { sql += "AND s.buscorpid  ='" + identity + "' "; }
                    sql += "GROUP BY s.workcorpname";
                }
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 修理公司汇总 用于RepairSum界面
        /// </summary>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public static DataTable GetForCom(string identity, int flag, string month, string date)
        {
            try
            {
                string sql = "";
                if (month == "" && date != "")//昨日
                {
                    sql = "SELECT s.buscorpname 分公司, " +
                        "  sum(s.assfee + s.matlfee) 定额材料费, " +
                        "  sum(s.fixedfee) 实际材料费 " +
                        "FROM hdc.web_sheet s " +
                        "WHERE s.balancedate = '" + date + "' " +
                        "AND s.buscorpid  in(select DISTINCT buscorpid from hdc.web_sheet) ";
                    if (flag == 2)//修理公司
                    { sql += "AND and s.workcorpid='" + identity + "' "; }
                    sql += "GROUP BY s.buscorpname,buscorpid order by buscorpid";
                }
                else//上月
                {
                    sql = "SELECT s.buscorpname 分公司, " +
                        "  sum(s.assfee + s.matlfee) 定额材料费, " +
                        "  sum(s.fixedfee) 实际材料费 " +
                        "FROM hdc.web_sheet s " +
                        "WHERE s.balancedate like '" + month + "%' " +
                        "AND s.buscorpid  in(select DISTINCT buscorpid from hdc.web_sheet) ";
                    if (flag == 2)//修理公司
                    { sql += "AND and s.workcorpid='" + identity + "' "; }
                    sql += "GROUP BY s.buscorpname,buscorpid order by buscorpid";
                }
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 工段维修汇总
        /// </summary>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public static DataTable GetRepairSumPla(string Identity)
        {
            string sql2 = "select m.repairdepname as 维修工段, m.lifix as 小修次数, n.sumtimes - m.lifix as 保养次数, n.sumtimes as 总次数, round(n.sumtimes*100/total, 4) as 百分比 from (select repairdepid, repairdepname, count(reprtype) as lifix from hdc.WEB_SHEETING where reprtype = '10' group by repairdepid, repairdepname order by repairdepid) m, (select repairdepid, repairdepname, count(*) as sumtimes from hdc.WEB_SHEETING group by repairdepid, repairdepname order by repairdepid) n, (select count(*) as total from hdc.WEB_SHEETING) p, (select depid, parentid from hdc.g_depinfo) w   where m.repairdepname = n.repairdepname and n.repairdepid =w.depid ";
            try
            {
                switch (Identity)
                {
                    case "04":
                        {
                            sql2 += "and w.parentid = '04'";
                            break;
                        }
                    case "26":
                        {
                            sql2 += "and w.parentid = '26'";
                            break;
                        }
                    //case "00":
                    //    {
                    //        break;
                    //    }
                    case "06":
                        {
                            sql2 += "and w.parentid = '06'";
                            break;
                        }
                    case "21":
                        {
                            sql2 += "and w.parentid = '21'";
                            break;
                        }
                    default:
                        break;
                }
                return webDAL.selectDataTable(sql2);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region hdcbike.aspx
        /// <summary>
        /// 根据 站点号 获取站点信息
        /// </summary>
        /// <param name="stationid">站点号</param>
        /// <returns></returns>
        public static DataTable getStationData(int stationid)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, address 地址, bikecount 现有车辆数 from WEB_STATION@cbls where stationid=" + stationid;
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 站点名 获取站点信息
        /// </summary>
        /// <param name="stationid">站点名</param>
        /// <returns></returns>
        public static DataTable getStationData(string stationname)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, address 地址, bikecount 现有车辆数 from WEB_STATION@cbls where stationname like '%" + stationname + "%' order by stationid";
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 排序类别 获取站点信息（默认全部）
        /// </summary>
        /// <param name="array">排序类别</param>
        /// <returns></returns>
        public static DataTable getSatationBike(string array)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, address 地址, bikecount 现有车辆数 from WEB_STATION@cbls order by " + array;
                if (array == "现有车辆数")
                {
                    sql += " desc";
                }
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 排序类别和翻页上下限 获取站点信息（默认全部）
        /// </summary>
        /// <param name="upperlimit">翻页上限</param>
        /// <param name="lowlimit">翻页下限</param>
        /// <param name="array">排序类别</param>
        /// <returns></returns>
        public static DataTable getSatationBike(int upperlimit, int lowlimit, string array)
        {
            try
            {
                string sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select stationid 站点号, stationname 站点名, address 地址, bikecount 现有车辆数 from WEB_STATION@cbls order by " + array + " ) A where ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                if (array == "现有车辆数")
                {
                    sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select stationid 站点号, stationname 站点名, address 地址, bikecount 现有车辆数 from WEB_STATION@cbls order by " + array + " desc ) A where ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit;
                }
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 站点ID 获取站点租车还车信息
        /// </summary>
        /// <param name="stationid">站点ID</param>
        /// <returns></returns>
        public static DataTable getStationRent(int stationid)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, rentcount 租车总数, returncount 还车总数 from web_station_yestoday@cbls where stationid=" + stationid;
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 站点名 获取站点租车还车信息
        /// </summary>
        /// <param name="stationname">站点名</param>
        /// <returns></returns>
        public static DataTable getStationRent(string stationname)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, rentcount 租车总数, returncount 还车总数 from web_station_yestoday@cbls where stationname like '%" + stationname + "%' order by stationid";
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 排序类别 获取站点租车还车信息
        /// </summary>
        /// <param name="array">排序类别</param>
        /// <returns></returns>
        public static DataTable getRentReturn(string array)
        {
            try
            {
                string sql = "select stationid 站点号, stationname 站点名, rentcount 租车总数, returncount 还车总数 from web_station_yestoday@cbls order by " + array;
                if (array == "租车总数" || array == "还车总数")
                {
                    sql += " desc";
                }
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据 排序类别和翻页上下限 获取站点租车还车信息
        /// </summary>
        /// <param name="upperlimit">翻页上限</param>
        /// <param name="lowlimit">翻页下限</param>
        /// <param name="array">排序类别</param>
        /// <returns></returns>
        public static DataTable getRentReturn(int upperlimit, int lowlimit, string array)
        {
            try
            {
                string sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select stationid 站点号, stationname 站点名, rentcount 租车总数, returncount 还车总数 from web_station_yestoday@cbls order by " + array + ") A where ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit; ;
                if (array == "租车总数" || array == "还车总数")
                {
                    sql = "SELECT * FROM (SELECT A.*, ROWNUM RN FROM( select stationid 站点号, stationname 站点名, rentcount 租车总数, returncount 还车总数 from web_station_yestoday@cbls order by " + array + " desc) A where ROWNUM <= " + lowlimit + ")WHERE RN >= " + upperlimit; ;
                }
                DataTable dt = webDAL.selectDataTable(sql);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region ReportForms.aspx

        public static DataTable getEmpInfosum(string ide)
        {
            try
            {
                string sql = "";
                if (ide == "00")
                {
                    sql = "select 公司,人数,round(人数*100/total,3) as 百分比 from(select nam as 公司,num  as 人数 from( (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '集团公司' group by ownerdep  )union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '第一汽车分公司' group by ownerdep  )  union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '第二汽车分公司' group by ownerdep  ) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '第三汽车分公司' group by ownerdep  ) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '石桥修理(运营)分公司' group by ownerdep)  union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '公交物业分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '电车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '第六汽车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '第五汽车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '余杭区客运公交有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '金通汽车修理公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '杭州市萧山公共交通有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '白马安达公共交通客运服务有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '杭州天苑房产开发公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '公交培训中心' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '经营发展（广告）公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '物资燃料分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '城西修理分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_EmpInfo@ehr where ownerdep = '杭州汽车出租有限公司' group by ownerdep) )),(select count(*)total from web_EmpInfo@ehr )";
                    return webDAL.selectDataTable(sql);
                }
                else
                {
                    sql = "SELECT n.nam AS 部门, n.sumc AS 人数,round(n.sumc * 100 / m.total, 3) AS 百分比 FROM ( SELECT posttype AS nam, count(depid) sumc FROM (( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '管理干部' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '司机' AND depid LIKE '" + ide + "%' ) UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '服务人员' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '辅助工' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '乘务员' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '修理工' AND depid LIKE '" + ide + "%' )UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '管理人员' AND depid LIKE '" + ide + "%' )UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '医务人员' AND depid LIKE '" + ide + "%' )UNION  all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = 'null' AND depid LIKE '" + ide + "%' )UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '其他人员' AND depid LIKE '" + ide + "%' ) UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '站员' AND depid LIKE '" + ide + "%' )) GROUP BY posttype ) n, ( SELECT count(*) AS total FROM web_EmpInfo@ehr WHERE depid LIKE '" + ide + "%' ) m";
                    return webDAL.selectDataTable(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getEmpInfotype()
        {
            try
            {
                string sql = "SELECT n.nam AS 工种, n.sumc AS 人数,round(n.sumc * 100 / m.total, 3) AS 百分比  FROM ( SELECT posttype AS nam, count(depid) sumc FROM (( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '管理干部'  ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '司机' ) UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '服务人员'  ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '辅助工'  ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '乘务员'  ) UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '修理工'  )UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '管理人员'  )UNION all ( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '医务人员'  )UNION  all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = 'null'  )UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '其他人员'  ) UNION all( SELECT posttype, depid FROM web_EmpInfo@ehr WHERE posttype = '站员'  )) GROUP BY posttype ) n, ( SELECT count(*) AS total FROM web_EmpInfo@ehr ) m";
                return webDAL.selectDataTable(sql);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getEmpInfodep(string identity)
        {
            try
            {
                string sql = "select dep as 部门 , num as 人数, round(num*100/total,3) as 百分比 from (select dep, count(*) as num from web_EmpInfo@ehr where depid like '" + identity + "%' group by dep),(select count(depid)total from web_EmpInfo@ehr where depid like '" + identity + "%')";
                return webDAL.selectDataTable(sql);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region WorkFlowDetail.aspx
        public static DataTable getWorkflowDetail(string id)
        {
            try
            {//empid 员工号,empname 员工姓名,workflowtype 调动类别,ownerdep 原属单位,depname 单位名,content 调动,opttime 时间,workflowstatus 状态
                string sql = "SELECT empid 员工号,empname 员工姓名,workflowtype 调动类别,ownerdep 所属公司,depname 现单位,content 调动,SUBSTR(opttime,0,10) 时间,workflowstatus 状态 FROM WEB_WORKFLOW@EHR where workflowid =" + id;
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getWorkflowYear()
        {
            try 
            {
                string sql = "select distinct SUBSTR(opttime,0,4) year FROM WEB_WORKFLOW@EHR order by year";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getWorkflow(string date)
        {
            try
            {
                string sql = "SELECT empid 员工号,empname 员工姓名,workflowtype 调动类别,ownerdep 所属公司,depname 现单位,content 调动,SUBSTR(opttime,0,10) 时间,workflowstatus 状态 FROM WEB_WORKFLOW@EHR where opttime like'" + date + "%'";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getWorkflow(int upperlimit, int lowlimit, string date)
        {
            try
            {
                string sql = "SELECT empid 员工号,empname 员工姓名,workflowtype 调动类别,ownerdep 所属公司,content 原单位,depname 现单位,SUBSTR(opttime,0,10) 时间,workflowstatus 状态 FROM WEB_WORKFLOW@EHR where opttime like'" + date + "%'";
                sql = "SELECT * FROM (SELECT ROWNUM 序号, A.* FROM ( " + sql + " ) A WHERE ROWNUM <= " + lowlimit + ") WHERE 序号 >= " + upperlimit + "";
                DataTable dt= webDAL.selectDataTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][3].ToString() != "解除合同")
                    { 
                        string[] s = dt.Rows[i][5].ToString().Split(new string[] { ";", "->" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < s[0].Length; j++)
                        {
                            if (s[0][j].ToString() == ".")
                            {
                                dt.Rows[i][5] = s[0].Split(new char[] { '.' })[1];
                                break ;
                            }
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getEmpInfo(string req, bool sign)
        {
            try
            {
                if (sign == false)
                {//IDCARD as 身份证号,,TIMETOENTER as 进单位时间
                    string sql = "SELECT EMPID as 卡号,EMPNAME as 姓名,EMPSEX as 性别,EMPSELFID as 内部编号,OWNERDEP as 所属公司,DEP as 部门,POSTTYPE as 工种,POSTNAME as 岗位,NATIVEPLACE as 籍贯,LASTTITLE as 职称,EDUCATION as 学历,MARITALSTATUS as 婚姻状况,HOUSEHOLDPLACE as 户籍,TIMETOWORK as 工作始于,EMPLOYTYPE as 雇用类别 FROM web_EmpInfo@ehr " + req;
                    return webDAL.selectDataTable(sql);
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ParkInfo.aspx
        public static DataTable GetPark(string date,string filter)
        {
            try
            {
                string sql = "SELECT t.ORDERID 单号, t.TEAMNAME 车队, t.LINENAME 线路, t.BUSCLASS, t.BUSID 车号, t.BUSTIME 班次, t.DRIVERCODE 司机号, t.DRIVERNAME 司机名, to_char ( t.PLANENTERTIME, 'hh24:mi:ss' ) AS 计划报到, to_char (t.ENTERTIME, 'hh24:mi:ss') AS 实际报到, - t.ENTERDELAY 报到延误, to_char (t.PLANOUTTIME, 'hh24:mi:ss') AS 计划出车, to_char (t.OUTTIME, 'hh24:mi:ss') AS 实际出车, - t.OUTDELAY 出车延误 FROM web_pimsdispatchwarning t ";
                sql += "where" + date + filter;
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public static DataTable GetPark(int upperlimit, int lowlimit, string date, string filter)
        {
            try
            {
                string sql = "SELECT ORDERID 单号, TO_CHAR(ADJUSTDATE, 'yyyy-mm-dd') AS 日期, TEAMNAME 车队, LINENAME 线路, BUSID 车号, BUSTIME 班次, DRIVERCODE 司机号, DRIVERNAME 司机名, TO_CHAR(PLANENTERTIME, 'hh24:mi:ss') AS 计划报到, TO_CHAR(ENTERTIME, 'hh24:mi:ss') AS 实际报到, ENTERDELAY 报到延误, TO_CHAR(PLANOUTTIME, 'hh24:mi:ss') AS 计划出车, TO_CHAR(OUTTIME, 'hh24:mi:ss') AS 实际出车, OUTDELAY 出车延误 " +
                           "FROM WEB_PIMSDISPATCH T ";
                sql += "where" + date + filter;
                sql = "SELECT * FROM (SELECT ROWNUM 序号, A.* FROM ( " + sql + " ) A WHERE ROWNUM <= " + lowlimit + ") WHERE 序号 >= " + upperlimit + "";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        #endregion

        #region ContractMore.aspx
        public static DataTable getContractYear(string flag)
        {
            try
            {
                string date = DateTime.Now.ToString("dd-M月-yy");
                string sql = "";
                string type = "";
                if (flag == "0")//实习
                {
                    type = "probationenddate";
                }
                else//正式
                {
                    type = "terminatedate";
                }
                  sql = "select enddate " +
                        "from " +
                        "  (select distinct to_char(" + type + ",'yyyyMMdd') enddate " +
                        "  from web_contractinfo@ehr " +
                        "  where " + type + "<='" + date + "' " +
                        "  order by enddate desc " +
                        "  ) " +
                        "where rownum<=10 " +
                        "UNION ALL " +
                        "select enddate " +
                        "from " +
                        "  (select distinct to_char(" + type + ",'yyyyMMdd') enddate " +
                        "  from web_contractinfo@ehr " +
                        "  where " + type + ">'" + date + "' " +
                        "  order by enddate " +
                        "  ) " +
                        "where rownum<=10 " +
                        "order by enddate";
                
                return webDAL.selectDataTable(sql);
            }
            catch
            {
                return null;
            }
        }

        public static DataTable getContract(string date,string flag)
        {
            try
            {
                string sql ;
                string type ;
                if (flag == "0")//实习
                {
                    type = "probationenddate";
                }
                else//正式
                {
                    type = "terminatedate";
                }
                sql = "select con.empid 工号,emp.empname 姓名,con.contracttype 合同类型,emp.ownerdep 所属公司,emp.dep 所属部门,emp.posttype 工种,emp.state 状态 from web_contractinfo@ehr con,web_logininfo@ehr emp where con.empid=emp.empid and to_char(" + type + ",'yyyyMMdd')='" + date + "'";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable getContract(int upperlimit, int lowlimit, string date, string flag)
        {
            try
            {
                string sql;
                string type;
                if (flag == "0")//实习
                {
                    type = "probationenddate";
                }
                else//正式
                {
                    type = "terminatedate";
                }
                sql = "select con.empid 工号,emp.empname 姓名,con.contracttype 合同类型,emp.ownerdep 所属公司,emp.dep 所属部门,emp.posttype 工种,emp.state 状态 from web_contractinfo@ehr con,web_logininfo@ehr emp where con.empid=emp.empid and to_char(" + type + ",'yyyyMMdd')='" + date + "'";
                sql = "SELECT * FROM (SELECT ROWNUM 序号, A.* FROM ( " + sql + " ) A WHERE ROWNUM <= " + lowlimit + ") WHERE 序号 >= " + upperlimit + "";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
