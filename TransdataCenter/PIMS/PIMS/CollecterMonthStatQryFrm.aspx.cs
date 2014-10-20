using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;

public partial class CollecterMonthStatQryFrm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            XtraReportFac xrf = new XtraReportFac();
            PIMSQuery pimsqry = new PIMSQuery();
            DateTime SelectedMonth = Convert.ToDateTime(uc_MonthConvert1.BeginDate);

            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" and trunc(reportdate) ");
            strWhere.Append("between to_date('");
            strWhere.Append(uc_MonthConvert1.BeginDate);
            strWhere.Append("','yyyy-mm-dd') AND to_date('");
            strWhere.Append(uc_MonthConvert1.EndDate);
            strWhere.Append("','yyyy-mm-dd')");

            DataSet dsCollector = pimsqry.GetPIMSCollecter(strWhere.ToString(), this);
            xrf.mHeaderTableColumnCount = dsCollector.Tables[0].Rows.Count + 1;
            xrf.mDetailTableColumnCount = dsCollector.Tables[0].Rows.Count + 1;


            xrf.ShowReportTitle("收银员工作月统计表", "集团公司场站管理处", System.DateTime.Now.ToLongDateString());
            List<string> reportheader = new List<string>();
            reportheader.Add("日期");
            for (int i = 0; i < dsCollector.Tables[0].Rows.Count; i++)
            {
                reportheader.Add(dsCollector.Tables[0].Rows[i][0].ToString());
            }
            xrf.ReportHeader = reportheader.ToArray();
            string[] detailFieldNames = new string[reportheader.Count];
            for (int i = 0; i < reportheader.Count; i++)
                detailFieldNames[i] = i.ToString();
            xrf.detailFields = detailFieldNames;

            xrf.ReportHeaderFunc();


            DataSet ds = pimsqry.GetPIMSCollectMonthNum(strWhere.ToString(), this);
            DataSet stat_ds = new DataSet();
            DataTable dt = stat_ds.Tables.Add();
            int dayinMonth = DateTime.DaysInMonth(SelectedMonth.Year, SelectedMonth.Month);
            for (int i = 0; i < reportheader.Count; i++)
                dt.Columns.Add(i.ToString());
            for (int i = 0; i < dayinMonth; i++)
                dt.Rows.Add((i + 1).ToString() + " 日");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int i = 1; i < reportheader.Count; i++)
                {
                    if (reportheader[i] == dr[1].ToString())
                    {
                        int rowIndex = Convert.ToDateTime(dr[0]).Day - 1;
                        dt.Rows[rowIndex][i] = dr[2];
                    }
                }
            }

            xrf.mDs = stat_ds;
            xrf.ShowRepotDetail();


            //begin 生成最终的合计
            DataSet sumds = new DataSet();
            DataTable sumdt = new DataTable();
            for (int i = 0; i < reportheader.Count; i++)
                sumdt.Columns.Add(i.ToString());
            xrf.detailFields = detailFieldNames;

            object[] SumFieldNames = new object[reportheader.Count];
            SumFieldNames[0] = "合计";
            for (int i = 1; i < reportheader.Count; i++)
                SumFieldNames[i] = 0;



            for (int j = 1; j < reportheader.Count; j++)
            {
                int sum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int num = 0;
                    Int32.TryParse(dt.Rows[i][j].ToString(), out num);
                    sum += num;
                }
                SumFieldNames[j] = sum;
            }
            DataRow ndr = sumdt.Rows.Add(SumFieldNames);
            sumds.Tables.Add(sumdt);
            xrf.mDs = sumds;

            xrf.ShowRepotDetail();
            //end


            this.rptViewDetail.Report = xrf;
        }
        catch
        { }
    }


}