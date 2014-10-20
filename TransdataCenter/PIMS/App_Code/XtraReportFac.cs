using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

/// <summary>
/// Summary description for XtraReportFac
/// </summary>
public class XtraReportFac : DevExpress.XtraReports.UI.XtraReport
{
    private DetailBand Detail;
    private PageHeaderBand PageHeader;
    private PageFooterBand PageFooter;

    /// <summary>
    /// 页面宽度(默认650)
    /// </summary>
    private int pageWidth = 650;

    /// <summary>
    /// 报表头列数(默认5)
    /// </summary>
    public int mHeaderTableColumnCount = 5;
    const int mHeaderRowHeight = 30;
    /// <summary>
    /// 明细表列数(默认5)
    /// </summary>
    public int mDetailTableColumnCount = 5;
    private int mDetailRowHeight = 30;
    //private int mReportLayout;
    /// <summary>
    /// 报表脚列数（用于统计用 一般默认2）
    /// </summary>
    //private int mReportFooterColumnCount = 2;
    /// <summary>
    /// 报表详细要显示的字段
    /// </summary>
    public string[] detailFields = null;
    /// <summary>
    /// 页脚列数(默认5)
    /// </summary>
    //private int mFooterTableColumnCount = 5;


    //this.xrControlStyle2.BackColor = System.Drawing.Color.Gainsboro;

    private int columnWidth;
    //private int tempHight = 0;
    // private int i, j, k;
    /// <summary>
    /// 每个要显示的字段的位置大小
    /// 表示报表布局的数组[i][j][k]
    /// i=0:页头布局，i=1:明细布局，i=2:页脚布局
    /// j:按顺序显示的字段(j=0:报表标题字段)
    /// k=0:字段名，k=1:标题，k=2:列跨度，k=3:格式化字符串(可选)
    /// </summary>
    public string[][][] ReportLayout = null;

    public string[] ColumnCount = null;
    public string[] DetailRowCount = null;
    public string[] MergeCells = null;
    public string linkUrl = "";
    public string linkText = "";
    public bool linkNewWindow = false;
    XRLabel xrLblTitle = new XRLabel();
    XRLabel xrLblOptName = new XRLabel();
    XRLabel xrLblOptDate = new XRLabel();
    XRTable headerTable = new XRTable();
    XRTable detailTable = new XRTable();
    XRTable sumTable = new XRTable();
    DevExpress.XtraReports.UI.XRControlStyle xrheaderStyle, xrDetailStyle, xrSumbandStyle;
    XRPageInfo xrpi = new XRPageInfo();
    XRChart chart = new XRChart();

    //  XRPageBreak xrpb = new XRPageBreak();

    private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
    private GroupFooterBand groupFootBand;
    public DevExpress.XtraCharts.ChartTitle chartTitle;
    /// <summary>
    /// 报表头列
    /// </summary>
    public string[] ReportHeader = null;
    /// <summary>
    /// 报表统计列
    /// </summary>
    public string[] ReportSum = null;
    /// <summary>
    /// 图表数据
    /// </summary>
    public string[] ChartData = null;


    public string[] DataBindingSum = null;

    //所有数据存储在一个DataSet中，DataSet里的第一个Table是页头数据，第二个是明细数据，第三个是页脚数据。
    /// <summary>
    /// 报表数据源,0页头表1明细表2页脚表
    /// </summary>
    public DataSet mDs = null;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReportFac()
    {
        InitializeComponent();

        this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { xrLblTitle, xrLblOptName, xrLblOptDate, headerTable });
        //设置样式
        xrheaderStyle = new DevExpress.XtraReports.UI.XRControlStyle(Color.FromArgb(30, 138, 195), Color.Black, DevExpress.XtraPrinting.BorderSide.All, 1, new System.Drawing.Font("宋体", 12F, FontStyle.Bold), Color.White, DevExpress.XtraPrinting.TextAlignment.MiddleCenter);
        xrheaderStyle.Name = "xrheaderStyle";
        xrDetailStyle = new DevExpress.XtraReports.UI.XRControlStyle(Color.Gainsboro, Color.Black, DevExpress.XtraPrinting.BorderSide.All, 1, new System.Drawing.Font("宋体", 9F, FontStyle.Regular), Color.Black, DevExpress.XtraPrinting.TextAlignment.MiddleCenter);
        xrDetailStyle.Name = "xrDetailStyle";
        xrSumbandStyle = new DevExpress.XtraReports.UI.XRControlStyle(Color.Wheat, Color.Black, BorderSide.Left | BorderSide.Right | BorderSide.Bottom, 1, new System.Drawing.Font("宋体", 12F, FontStyle.Bold), Color.Black, DevExpress.XtraPrinting.TextAlignment.MiddleCenter);
        xrSumbandStyle.Name = "xrSumbandStyle";
        this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            xrheaderStyle,xrDetailStyle,xrSumbandStyle
              });
        this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { detailTable });
        //画页脚（页数）
        this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {xrpi
            });
        xrpi.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        xrpi.Location = new System.Drawing.Point(0, 0);
        xrpi.Name = "PageInfo";
        xrpi.Size = new System.Drawing.Size(100, 25);

        //调用样式
        headerTable.Styles.Style = xrheaderStyle;
        detailTable.Styles.OddStyle = xrDetailStyle;
        sumTable.Styles.Style = xrSumbandStyle;
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //string resourceFileName = "XtraReportFac.resx";
        this.Detail = new DevExpress.XtraReports.UI.DetailBand();
        this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
        this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
        ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
        // 
        // Detail
        // 
        this.Detail.Height = 0;
        this.Detail.Name = "Detail";
        this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageHeader
        // 
        this.PageHeader.Height = 0;
        this.PageHeader.Name = "PageHeader";
        this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // PageFooter
        // 
        this.PageFooter.Height = 30;
        this.PageFooter.Name = "PageFooter";
        this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
        this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
        // 
        // XtraReportFac
        // 
        this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter});
        this.Version = "9.1";
        ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    /// <summary>画报表标题，操作员，操作日期
    /// 画报表标题，操作员，操作日期
    /// </summary>
    /// <param name="LblTitle"></param>
    /// <param name="LblOptName"></param>
    /// <param name="LblOptDate"></param>
    public void ShowReportTitle(string LblTitle, string LblOptName, string LblOptDate)
    {

        if (LblTitle.Length > 10 && LblTitle.Length <= 20)
        {
            xrLblTitle.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }
        if (LblTitle.Length > 20)
        {
            xrLblTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }
        else
        {
            xrLblTitle.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }
        xrLblTitle.Size = new System.Drawing.Size(400, 50);
        xrLblTitle.Location = new System.Drawing.Point(pageWidth / 2 - xrLblTitle.Size.Width / 2, 0);
        xrLblTitle.Name = "xrLblTitle";
        xrLblTitle.Text = LblTitle;
        xrLblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

        xrLblOptName.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
        xrLblOptName.Size = new System.Drawing.Size(150, 35);
        xrLblOptName.Location = new System.Drawing.Point(0, 50);
        xrLblOptName.Name = "xrLblOptName";
        xrLblOptName.Text = LblOptName;
        xrLblOptName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

        xrLblOptDate.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
        xrLblOptDate.Size = new System.Drawing.Size(150, 35);
        xrLblOptDate.Location = new System.Drawing.Point(500, 50);
        xrLblOptDate.Name = "xrLblOptDate";
        xrLblOptDate.Text = LblOptDate;
        xrLblOptDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;

    }
    /// <summary>
    /// 画报表头字段
    /// </summary>
    public void ReportHeaderFunc()
    {


        columnWidth = pageWidth / mHeaderTableColumnCount;
        headerTable.Width = columnWidth * mHeaderTableColumnCount;
        headerTable.Height = mHeaderRowHeight;
        headerTable.Location = new System.Drawing.Point(0, 90);
        XRTableRow tr = new XRTableRow();
        for (int i = 0; i < ReportHeader.Length; i++)
        {
            XRTableCell tc = new XRTableCell();
            tc.Size = new System.Drawing.Size(ColumnCount != null ? (columnWidth * Convert.ToInt32(ColumnCount[i])) : columnWidth, mHeaderRowHeight);
            tc.Text = ReportHeader[i].ToString();
            tr.Cells.Add(tc);
        }
        headerTable.Rows.Add(tr);
    }
    /// <summary>
    /// 画报表详细
    /// </summary>
    public void ShowRepotDetail()
    {
        // this.Detail.PageBreak = new PageBreak();
        //  xrpb.Location = new System.Drawing.Point(150, 0);

        columnWidth = pageWidth / mDetailTableColumnCount;
        detailTable.Width = columnWidth * mDetailTableColumnCount;
        detailTable.Location = new System.Drawing.Point(0, 0);
        //this.DataSource = mDs;
        // int a = this.RowCount;
        //XRTableRow tr = new XRTableRow();

        //for (int i = 0; i < detailFields.Length; i++)
        //{
        //    XRTableCell tc = new XRTableCell();
        //    tc.Size = new System.Drawing.Size(ColumnCount != null ? (columnWidth * Convert.ToInt32(ColumnCount[i])) : columnWidth, DetailRowCount != null ? (mHeaderRowHeight * Convert.ToInt32(DetailRowCount[i])) : mDetailRowHeight);
        //    tc.TextAlignment = TextAlignment.MiddleCenter;
        //    tc.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
        //    tc.Font = new System.Drawing.Font("宋体", 9F);
        //    tc.DataBindings.Add(new XRBinding("Text", null, detailFields[i], ""));
        //    if (detailFields[i].ToLower() == linkText.ToLower())
        //        tc.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.tcLink_HtmlItemCreated);
        //    tr.Controls.Add(tc);

        //}

        for (int i = 0; i < mDs.Tables[0].Rows.Count; i++)
        {
            XRTableRow tr = new XRTableRow();
            tr.Size = new System.Drawing.Size(pageWidth, mDetailRowHeight);
            //if (i % 2 == 0)
            //{
            //    tr.StyleName = "xrDetailStyle";
            //}
            bool isSame = true;
            for (int j = 0; j < detailFields.Length; j++)
            {

                XRTableCell tc = new XRTableCell();
                tc.Size = new System.Drawing.Size(ColumnCount != null ? (columnWidth * Convert.ToInt32(ColumnCount[j])) : columnWidth, mDetailRowHeight);
                tc.TextAlignment = TextAlignment.MiddleCenter;
                tc.Font = new System.Drawing.Font("宋体", 9F);

                if (isSame && i < mDs.Tables[0].Rows.Count - 1 && MergeCells != null)
                {
                    if (j < MergeCells.Length)
                    {
                        if (mDs.Tables[0].Rows[i][MergeCells[j]].ToString() == mDs.Tables[0].Rows[i + 1][MergeCells[j]].ToString())
                        {
                            tc.Text = "";

                            tc.Borders = BorderSide.Left | BorderSide.Right;
                        }
                        else
                        {
                            isSame = false;
                            tc.TextAlignment = TextAlignment.MiddleCenter;
                            tc.Text = mDs.Tables[0].Rows[i][detailFields[j]].ToString();
                            tc.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                        }
                    }
                    else
                    {
                        tc.Text = mDs.Tables[0].Rows[i][detailFields[j]].ToString();
                        tc.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                    }
                }
                else
                {
                    tc.Text = mDs.Tables[0].Rows[i][detailFields[j]].ToString();
                    tc.Borders = BorderSide.Left | BorderSide.Right | BorderSide.Bottom;
                }
                if (detailFields[j].ToLower() == linkText.ToLower())
                    tc.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.tcLink_HtmlItemCreated);
                tr.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] { tc });
            }


            detailTable.Controls.Add(tr);
        }

        //  detailTable.Controls.Add(tr);


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tcLink_HtmlItemCreated(object sender, HtmlEventArgs e)
    {
        System.Web.UI.WebControls.HyperLink hypLink = new System.Web.UI.WebControls.HyperLink();
        hypLink.Text = e.ContentCell.InnerText;
        hypLink.NavigateUrl = linkUrl + hypLink.Text;
        if (linkNewWindow)
            hypLink.Target = "_blank";
        e.ContentCell.InnerText = "";
        e.ContentCell.Controls.Add(hypLink);
    }
    /// <summary>
    /// 显示图表方法
    /// </summary>
    public void ShowChart()
    {
        // tempHight = 350;
        ReportFooter = new ReportFooterBand();

        this.ReportFooter.Height = 350;
        this.ReportFooter.Name = "ReportFooter";
        this.Bands.Add(this.ReportFooter);

        // chart = new XRChart();
        DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
        this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            chart});


        chartTitle = new DevExpress.XtraCharts.ChartTitle();

        chart.BorderColor = System.Drawing.SystemColors.ControlText;
        chart.Borders = DevExpress.XtraPrinting.BorderSide.None;
        chart.Location = new System.Drawing.Point(0, 0);
        chart.Name = "xrChart";

        chart.Size = new System.Drawing.Size(650, 350);

        chart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle});
        DataView dv = new System.Data.DataView(mDs.Tables[0]);
        chart.DataSource = new System.Data.DataView(mDs.Tables[0]);



        chart.SeriesDataMember = dv.Table.Columns[ChartData[0]].Caption;     // "DBUTable.matname";
        chart.SeriesTemplate.ArgumentDataMember = dv.Table.Columns[ChartData[1]].Caption;
        chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { dv.Table.Columns[ChartData[2]].Caption });

    }
    public void ShowSumBand()
    {
        groupFootBand = new GroupFooterBand();

        this.Bands.Add(groupFootBand);
        groupFootBand.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            sumTable});

        columnWidth = pageWidth / mHeaderTableColumnCount;
        sumTable.Width = columnWidth * mHeaderTableColumnCount;
        sumTable.Height = mHeaderRowHeight;
        sumTable.Location = new System.Drawing.Point(0, 0);
        XRTableRow tr = new XRTableRow();
        this.DataSource = mDs;

        for (int i = 0; i < ReportSum.Length; i++)
        {
            XRTableCell tc = new XRTableCell();
            tc.Size = new System.Drawing.Size(ColumnCount != null ? (columnWidth * Convert.ToInt32(ColumnCount[i])) : columnWidth, mHeaderRowHeight);
            tc.Text = ReportSum[i].ToString();
            //if (tc.Text == "tcSum")
            if (i > 0)
            {
                XRSummary xrye = new XRSummary();
                //指定对该列进行什么操作
                xrye.Func = DevExpress.XtraReports.UI.SummaryFunc.Sum;
                //指定以什么方式显示结果
                xrye.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
                //显示格式
                //xrye.FormatString = "{0:N2}";
                // tc.Name = i.ToString();

                tc.Summary = xrye;

                tc.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, DataBindingSum[i-1], "")});
            }

            tr.Cells.Add(tc);
        }

        sumTable.Rows.Add(tr);


    }
    /// <summary>
    /// 行单元格合并过后的统计计算
    /// </summary>
    public void ShowMergeSumBand()
    {
        groupFootBand = new GroupFooterBand();

        this.Bands.Add(groupFootBand);
        groupFootBand.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            sumTable});

        columnWidth = pageWidth / mHeaderTableColumnCount;
        sumTable.Width = columnWidth * mHeaderTableColumnCount;
        sumTable.Height = mHeaderRowHeight;
        sumTable.Location = new System.Drawing.Point(0, 0);
        XRTableRow tr = new XRTableRow();


        for (int i = 0; i < ReportSum.Length; i++)
        {
            XRTableCell tc = new XRTableCell();
            tc.Size = new System.Drawing.Size(ColumnCount != null ? (columnWidth * Convert.ToInt32(ColumnCount[i])) : columnWidth, mHeaderRowHeight);
            tc.Text = ReportSum[i].ToString();
            tr.Cells.Add(tc);
        }

        sumTable.Rows.Add(tr);


    }
}
