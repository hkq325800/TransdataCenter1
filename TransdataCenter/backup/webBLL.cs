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
        
        #region 欢迎信息
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
                string sql = "SELECT count(*),empname,depid FROM web_logininfo@ehr where empid='" + username + "' and userpassword='" + password + "' group by depid,empname";
                DataTable dt = webDAL.selectDataTable(sql);
                ls.Add(dt.Rows[0]["empname"].ToString());
                ls.Add(dt.Rows[0]["depid"].ToString());
                ls.Add(dt.Rows[0]["count(*)"].ToString());
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
                string sql = "SELECT * FROM web_empinfo@ehr";
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
                string sql = "SELECT COUNT(busid) FROM hdc.WEB_BUS";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getparkNum()
        {
            try
            {
                string sql = "";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        
        //#region 自行车租赁信息
        //public static string getBikeStation()
        //{
        //    try
        //    {
        //        string sql = "select count(*) from web_station@cbls t";
        //        return webDAL.selectFirstData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public static string getBikeRentYesterday()
        //{
        //    try
        //    {
        //        string sql = "select sum(t.rentcount) from b_stationrentcount_yestoday@cbls t";
        //        return webDAL.selectFirstData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public static string getBikeRentThisMonth()
        //{
        //    try
        //    {
        //        string sql = "select sum(t.rentcount) from b_stationrentcount_month@cbls t where t.rentmonth='201212'";
        //        return webDAL.selectFirstData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public static string getBikeRentLastMonth()
        //{
        //    try
        //    {
        //        string sql = "select sum(t.rentcount) from b_stationrentcount_month@cbls t where t.rentmonth='201211'";
        //        return webDAL.selectFirstData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //#endregion
        #region 停车场信息
        public static string getCarNumIn(string date)
        {
            try
            {
                string sql = "SELECT T.PARKNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('"+date+"','YYYY-MM-DD')";
                Console.WriteLine(sql);
                Console.WriteLine(webDAL.selectFirstData(sql));
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string getCarNumOut(string date)
        {
            try
            {
                string sql = "SELECT T.OUTNUM_REAL FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('" + date + "','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getCarLate(string date)
        {
            try
            {
                string sql = "SELECT T.OUTNUM_LATE FROM WEB_PIMSDIARY T WHERE T.DIARYDATE = TO_DATE('"+date+"','YYYY-MM-DD')";
                return webDAL.selectFirstData(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

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

        public static string getWarsh(string date)
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
        public static string getLittFix(string identity, int flag, string place, string date, string month)//rank为当前登录用户的Identity flag为用户权限0子1集团2修理 place为前端显示的修理公司
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

        /// <summary>
        /// 根据车号返回维修详细信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static DataTable getRepairInfo(string req, bool sign)
        {
            try
            {
                if (sign == false)
                {
                    string sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + req + "' order by balancemonth desc, amount";
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
        /// <param name="busid"></param>
        /// <param name="index1"></param>
        /// <param name="type"></param>
        /// <param name="index2"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DataTable filtrateRepairInfo(string busid, int index1, string type, int index2, string month)
        {
            string sql = "";
            if (index1 != 0 && index2 != 0)
            {
                sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' and balancemonth = '" + month + "' order by balancemonth desc, amount";
            }
            else if (index1 == 0 && index2 != 0)
            {
                sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and balancemonth = '" + month + "' order by balancemonth desc, amount";
            }
            else if (index1 != 0 && index2 == 0)
            {
                sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' and itemvalue = '" + type + "' order by balancemonth desc, amount";
            }
            else
            {
                sql = "select busid as 车号, balancemonth as 维修年份, itemvalue as 维修类别, amount as 维修次数, fixedfee as 计划费用, assfee+matlfee as 实际费用, milesum as 行驶公里 from hdc.s_monthreport a, (select itemid, itemvalue from hdc.g_dic where dicname = '维护修理类别') b where a.repairtype = b.itemid and amount > 0 and a.busid = '" + busid + "' order by balancemonth desc, amount";
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
        /// 公司维修汇总 用于RepairSum界面
        /// </summary>
        /// <param name="Identity"></param>
        /// <returns></returns>
        public static DataTable GetRepairSumEmp(string Identity)
        {
            try
            {
                string sql1 = "select m.rede                    as 维修公司, "+
                              "  m.lifix                        as 小修次数, "+
                              "  n.sumtimes - m.lifix           as 保养次数, " +
                              "  n.sumtimes                     as 总次数, " +
                              "  round(n.sumtimes*100/total, 4) as 百分比 " +
                              "from " +
                              "  (select rede, " +
                              "    count(retp) as lifix " +
                              "  from " +
                              "    (select repairtypeid as retp, " +
                              "      a.workteamid       as repl, " +
                              "      c.depname          as rede " +
                              "    from hdc.WEB_SHEETING a, " +
                              "      hdc.g_depinfo b, " +
                              "      hdc.g_depinfo c " +
                              "    where a.workteamid = b.depid " +
                              "    and b.parentid     = c.depid " +
                              "    ) " +
                              "  where retp = '10' " +
                              "  group by rede " +
                              "  ) m, " +
                              "  (select rede, " +
                              "    count(*) as sumtimes " +
                              "  from " +
                              "    (select repairtypeid as retp, " +
                              "      a.workteamid       as repl, " +
                              "      c.depname          as rede " +
                              "    from hdc.WEB_SHEETING a, " +
                              "      hdc.g_depinfo b, " +
                              "      hdc.g_depinfo c " +
                              "    where a.workteamid = b.depid " +
                              "    and b.parentid     = c.depid " +
                              "    ) " +
                              "  group by rede " +
                              "  ) n, " +
                              "  (select count(*) as total from hdc.WEB_SHEETING " +
                              "  ) p " +
                              "where m.rede = n.rede";
                switch (Identity)
                {
                    case "04":
                        {
                            sql1 += "and m.rede = '石桥修理(运营)分公司'";
                            break;
                        }
                    case "26":
                        {
                            sql1 += "and m.rede = '城西修理分公司'";
                            break;
                        }
                    //case "00":
                    //    {
                    //        break;
                    //    }
                    case "06"://?
                        {
                            sql1 += "and m.rede = '电车分公司'";
                            break;
                        }
                    case "27":
                        {
                            sql1 += "and m.rede = '拱北修理分公司'";
                            break;
                        }
                    default:
                        break;

                }
                return webDAL.selectDataTable(sql1);
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
                    case "27":
                        {
                            sql2 += "and w.parentid = '27'";
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

        //yhb
        public static DataTable GetBusstyle1()
        {

            try
            {
                string sql = "select busstylename,busstyleid from hdc.g_busstyle ";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable Getcom1()
        {

            try
            {
                string sql = "select DEPNAME from hdc.g_depinfo where depid=26 or depid=04 or depid=27";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable Getdetails(string a,bool b, string c, bool d,int e)//DDLbusid.Text, DDLbusstyle.Enabled, DDLcom.Text, DDLcom.Enabled ,id
        {

            try
            {
                string sql;
                if (a == "全部车型号" && c == "所有公司")
                {
                    sql = "select s.busstylename 车型, count(*) 该车型总数,  case     when round(m.repairdep/100)=4    then '石桥修理(运营)分公司'    when round(m.repairdep/100)=26  then '城西修理分公司' when round(m.repairdep/100)=27 then '拱北修理分公司'   else '不详'  end as 修理公司,  sum(amount) 修理的总次数,  sum(m.fixedfee) 总的预算修理费用,  sum(m.assfee+m.matlfee) 总的实际费用 from hdc.s_monthreport m join hdc.g_businfo b on m.busshowid= b.busshowid join hdc.g_busstyle s on b.busstyleid= s.busstyleid  group by s.busstylename,round(m.repairdep/100) order by s.busstylename";
                    return webDAL.selectDataTable(sql);
                }
                else if (a == "全部车型号" && c == "该公司所有工段")
                {
                    sql = "select s.busstylename 车型, count(*) 该车型总数,  case     when round(m.repairdep/100)=4    then '石桥修理(运营)分公司'    when round(m.repairdep/100)=26  then '城西修理分公司' when round(m.repairdep/100)=27 then '拱北修理分公司'   else '不详'  end as 修理公司,  sum(amount) 修理的总次数,  sum(m.fixedfee) 总的预算修理费用,  sum(m.assfee+m.matlfee) 总的实际费用 from hdc.s_monthreport m join hdc.g_businfo b on m.busshowid= b.busshowid join hdc.g_busstyle s on b.busstyleid= s.busstyleid  group by s.busstylename,round(m.repairdep/100) order by s.busstylename";
                    return webDAL.selectDataTable(sql);
                }
                else if (b)
                {
                    sql = "select s.busstylename 车型, count(*) 该车型总数,  case     when round(m.repairdep/100)=4    then '石桥修理(运营)分公司'    when round(m.repairdep/100)=26  then '城西修理分公司' when round(m.repairdep/100)=27 then '拱北修理分公司'   else '不详'  end as 修理公司,  sum(amount) 修理的总次数,  sum(m.fixedfee) 总的预算修理费用,  sum(m.assfee+m.matlfee) 总的实际费用 from hdc.s_monthreport m join hdc.g_businfo b on m.busshowid= b.busshowid join hdc.g_busstyle s on b.busstyleid= s.busstyleid where s.busstyleid='" + a + "' group by s.busstylename,round(m.repairdep/100) order by s.busstylename";
                    return webDAL.selectDataTable(sql);
                }
                else if (d)
                {
                    sql = "select s.busstylename 车型, count(*) 该车型总数,  case     when round(m.repairdep/100)=4    then '石桥修理(运营)分公司'    when round(m.repairdep/100)=26  then '城西修理分公司' when round(m.repairdep/100)=27 then '拱北修理分公司'   else '不详'  end as 修理公司,  sum(amount) 修理的总次数,  sum(m.fixedfee) 总的预算修理费用,  sum(m.assfee+m.matlfee) 总的实际费用 from hdc.s_monthreport m join hdc.g_businfo b on m.busshowid= b.busshowid join hdc.g_busstyle s on b.busstyleid= s.busstyleid join hdc.g_depinfo dp on dp.depid=b.repairdepid where round(m.repairdep/100)=" + e + " group by s.busstylename,round(m.repairdep/100) order by s.busstylename";
                    return webDAL.selectDataTable(sql);
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetBs1(string a)
        {

            try
            {
                string sql = "select busstylename from hdc.g_busstyle b join hdc.g_businfo s on b.busstyleid= s.busstyleid where repairdepid='" + a + "' ";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable Getrepairid(string a)
        {

            try
            {
                string sql = "select depname from hdc.g_depinfo d  where depname like '___工段' and parentid ='" + a + "%' ";
                return webDAL.selectDataTable(sql);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //yhb
        #endregion

        #region 人力资源管理系统
        public static string getAllEmpNum(string rank,int flag)
        {
            try
            {
                if (flag == 0)
                {
                    string sql = "select count(*) from web_empinfo@ehr v join hdc.g_depinfo d on v.depid= d.depid where d.parentid=" + rank + "";
                    return webDAL.selectFirstData(sql);
                }
                else
                {
                    string sql = "SELECT COUNT(*) FROM web_empinfo@ehr t";
                    return webDAL.selectFirstData(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string getConEmpNum(string rank,int flag)
        {
            try
            {
                if (flag == 0)
                {
                    string sql = "SELECT COUNT(*) FROM web_empinfo@ehr v join hdc.g_depinfo d on v.depid=d.depid WHERE v.empid IN (SELECT empid FROM web_contractinfo@ehr) and d.parentid=" + rank + " ";
                    return webDAL.selectFirstData(sql);
                }
                else
                {
                    string sql = "SELECT COUNT(*) FROM web_empinfo@ehr t WHERE t.empid IN (SELECT empid FROM web_contractinfo@ehr)";
                    return webDAL.selectFirstData(sql);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string[][] getWorkflow()
        {
            try
            {
                string sql = "SELECT t1.workflowtype, t1.empname, t1.content, t1.workflowid FROM (SELECT * FROM WEB_WORKFLOW@EHR T ORDER BY T.OPTTIME DESC) t1 WHERE ROWNUM < 5";
                DataTable dt = webDAL.selectDataTable(sql);
                string[][] result = new string[4][];
                for (int i = 0; i < 4; i++)
                {
                    DataRow row = dt.Rows[i];

                    if ("解除合同".Equals(row[0]) || "终止合同".Equals(row[0]))
                    {
                        result[i] = new string[3]{ row[3].ToString(), row[1].ToString(), "解除合同" };
                    }
                    else
                    {
                        string from = null;
                        string to = null;
                        bool flag1 = false;
                        bool flag2 = false;
                        string[] s = row[2].ToString().Split(new string[] {";","->"},StringSplitOptions.RemoveEmptyEntries);
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
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //游冰
        public static int a = 0, b = 6;
        public static string[][] getWorkflowmore(string identify)
        {
            try
            {
                string sql;
                DataTable dt;
                string[][] result = new string[6][];
                if (identify == "00")
                {
                    sql = "SELECT * FROM WEB_WORKFLOW@EHR WHERE ROWNUM < = ' " + b + " ' MINUS SELECT * FROM WEB_WORKFLOW@EHR where rownum <= ' " + a + " ' order by 8 desc";


                    //sql = "SELECT t1.workflowtype, t1.empname, t1.content, t1.workflowid FROM (SELECT * FROM WEB_WORKFLOW@EHR T ORDER BY T.OPTTIME DESC) t1 WHERE ROWNUM < = " + b + " and t1.empid like '" + identify.ToString() + "%' MINUS SELECT t1.workflowtype, t1.empname, t1.content, t1.workflowid FROM (SELECT * FROM WEB_WORKFLOW@EHR T ORDER BY T.OPTTIME DESC) t1 WHERE ROWNUM < = " + a + "";
                    dt = webDAL.selectDataTable(sql);
                }
                else if (identify == "04" || identify == "26" || identify == "27")
                {
                    sql = "SELECT * FROM WEB_WORKFLOW@EHR WHERE rownum <= ' " + b + " ' and ownerdep in('石桥修理(运营)分公司', '拱北修理分公司','城西修理分公司')  minus SELECT * FROM WEB_WORKFLOW@EHR where rownum <=' " + a + " ' and ownerdep in('第六汽车分公司', '拱北修理分公司','城西修理分公司') order by 8 desc";
                    dt = webDAL.selectDataTable(sql);
                }
                else
                {
                    sql = "SELECT * FROM WEB_WORKFLOW@EHR WHERE rownum <= ' " + b + " ' and ownerdep not in('石桥修理(运营)分公司', '拱北修理分公司','城西修理分公司','集团公司')  minus SELECT * FROM WEB_WORKFLOW@EHR where rownum <=' " + a + " ' and ownerdep not in('第六汽车分公司', '拱北修理分公司','城西修理分公司','集团公司管理部') order by 8 desc";
                    dt = webDAL.selectDataTable(sql);
                }
                for (int i = 0; i < 6; i++)
                {
                    DataRow row = dt.Rows[i];

                    if ("解除合同".Equals(row["workflowtype"]))
                    {
                        result[i] = new string[3] { row["workflowid"].ToString(), row["empname"].ToString(), "解除合同" };
                    }
                    else
                    {
                        string from = null;
                        string to = null;
                        bool flag1 = false;
                        bool flag2 = false;
                        string[] s = row["content"].ToString().Split(new string[] { ";", "->" }, StringSplitOptions.RemoveEmptyEntries);
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
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable getEmpInfosum(string ide)
        {
            try
            {
                string sql = "";
                if (ide == "00")
                {
                    sql = "select 公司,人数,round(人数*100/total,3) as 百分比 from(select nam as 公司,num  as 人数 from( (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '集团公司' group by ownerdep  )union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '第一汽车分公司' group by ownerdep  )  union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '第二汽车分公司' group by ownerdep  ) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '第三汽车分公司' group by ownerdep  ) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '石桥修理(运营)分公司' group by ownerdep)  union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '公交物业分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '电车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '第六汽车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '第五汽车分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '余杭区客运公交有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '金通汽车修理公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '杭州市萧山公共交通有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '白马安达公共交通客运服务有限公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '杭州天苑房产开发公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '公交培训中心' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '经营发展（广告）公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '物资燃料分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '城西修理分公司' group by ownerdep) union all (select ownerdep as nam,count(depid) as num from web_empinfo@ehr where ownerdep = '杭州汽车出租有限公司' group by ownerdep) )),(select count(*)total from web_empinfo@ehr )";
                    return webDAL.selectDataTable(sql);
                }
                else
                {
                    sql = "SELECT n.nam AS 部门, n.sumc AS 人数,round(n.sumc * 100 / m.total, 3) AS 百分比 FROM ( SELECT posttype AS nam, count(depid) sumc FROM (( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '管理干部' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '司机' AND depid LIKE '" + ide + "%' ) UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '服务人员' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '辅助工' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '乘务员' AND depid LIKE '" + ide + "%' ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '修理工' AND depid LIKE '" + ide + "%' )UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '管理人员' AND depid LIKE '" + ide + "%' )UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '医务人员' AND depid LIKE '" + ide + "%' )UNION  all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = 'null' AND depid LIKE '" + ide + "%' )UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '其他人员' AND depid LIKE '" + ide + "%' ) UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '站员' AND depid LIKE '" + ide + "%' )) GROUP BY posttype ) n, ( SELECT count(*) AS total FROM web_empinfo@ehr WHERE depid LIKE '" + ide + "%' ) m";
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
                string sql = "SELECT n.nam AS 部门, n.sumc AS 人数,round(n.sumc * 100 / m.total, 3) AS 百分比  FROM ( SELECT posttype AS nam, count(depid) sumc FROM (( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '管理干部'  ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '司机' ) UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '服务人员'  ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '辅助工'  ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '乘务员'  ) UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '修理工'  )UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '管理人员'  )UNION all ( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '医务人员'  )UNION  all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = 'null'  )UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '其他人员'  ) UNION all( SELECT posttype, depid FROM web_empinfo@ehr WHERE posttype = '站员'  )) GROUP BY posttype ) n, ( SELECT count(*) AS total FROM web_empinfo@ehr ) m";
                return webDAL.selectDataTable(sql);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public static DataTable getWorkflowDetail(string id)
        {
            try
            {//empid 员工号,empname 员工姓名,workflowtype 调动类别,ownerdep 原属单位,depname 单位名,content 调动,opttime 时间,workflowstatus 状态
                string sql = "SELECT empid ,empname ,workflowtype,ownerdep,depname ,content ,opttime ,workflowstatus  FROM WEB_WORKFLOW@EHR where workflowid =" + id;
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
                string sql = "select dep as 部门 , num as 人数, round(num*100/total,3) as 百分比 from (select dep, count(*) as num from web_empinfo@ehr where depid like '"+identity+"%' group by dep),(select count(depid)total from web_empinfo@ehr where depid like '"+identity+"%')";
                return webDAL.selectDataTable(sql);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //游冰
        

        public static DataTable getEmpInfo(string req,bool sign)
        {
            try
            {
                if (sign == false)
                {
                    string sql = "SELECT EMPID as 员工卡号,EMPNAME as 员工姓名,EMPSEX as 性别,EMPSELFID as 单位内部编号,IDCARD as 身份证号,OWNERDEP as 所属公司,DEP as 当前部门,POSTTYPE as 工种,POSTNAME as 岗位,NATIVEPLACE as 籍贯,LASTTITLE as 职称,EDUCATION as 学历,MARITALSTATUS as 婚姻状况,HOUSEHOLDPLACE as 户口所在地,TIMETOWORK as 工作开始时间,TIMETOENTER as 进单位时间,EMPLOYTYPE as 雇用类别 FROM web_empinfo@ehr " + req;
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

        public static string GetClientIp()
        {
            string l_ret = string.Empty;
            
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);

            if (string.IsNullOrEmpty(l_ret))
                l_ret = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return l_ret;
        }
    }
}
