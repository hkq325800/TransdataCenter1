using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.OracleClient;

namespace TransdataCenter
{
    public partial class PIMS : System.Web.UI.Page
    {
        private static string connstring = "Data Source=wcDATABASE;User Id=system;Password=123;";
        private int recordCount;
        private int pageCount;

        void Page_Load(Object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataGridDataBind();
            }
        }
        //绑定数据
        private void DataGridDataBind()
        {
            DataSet ds = GetCustomersData();
            recordCount = ds.Tables[0].Rows.Count;
            //获取当前的页数
            pageCount = (int)Math.Ceiling(recordCount * 1.0 / PageSize);
            //避免纪录从有到无时，并且已经进行过反页的情况下CurrentPageIndex > PageCount出错
            if (recordCount == 0)
            {
                this.DataHelp.CurrentPageIndex = 0;
            }
            else if (this.DataHelp.CurrentPageIndex >= pageCount)
            {
                this.DataHelp.CurrentPageIndex = pageCount - 1;
            }
            this.DataHelp.DataSource = ds.Tables["data"];
            this.DataHelp.DataBind();
            NavigationStateChange();
        }


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LBtnFirst.Click += new System.EventHandler(this.LBtnNavigation_Click);
            this.LBtnPrev.Click += new System.EventHandler(this.LBtnNavigation_Click);
            this.LBtnNext.Click += new System.EventHandler(this.LBtnNavigation_Click);
            this.LBtnLast.Click += new System.EventHandler(this.LBtnNavigation_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion


        private void LBtnNavigation_Click(object sender, System.EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            switch (btn.CommandName)
            {
                case "First":
                    PageIndex = 0;
                    break;
                case "Prev"://if( PageIndex > 0 )
                    PageIndex = PageIndex - 1;
                    break;
                case "Next"://if( PageIndex < PageCount -1)
                    PageIndex = PageIndex + 1;
                    break;
                case "Last":
                    PageIndex = PageCount - 1;
                    break;
            }
            DataGridDataBind();
        }
        //数据绑定
        public static DataSet GetCustomersData()
        {
            OracleConnection conn = new OracleConnection(connstring);
            string sql = "select * from help";
            OracleCommand cmd = new OracleCommand(sql, conn);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "data");
            return ds;
        }


        /// <summary>
        /// 控制导航按钮或数字的状态
        /// </summary>
        public void NavigationStateChange()
        {
            if (PageCount <= 1)//( RecordCount <= PageSize )//小于等于一页
            {
                this.LBtnFirst.Enabled = false;
                this.LBtnPrev.Enabled = false;
                this.LBtnNext.Enabled = false;
                this.LBtnLast.Enabled = false;
            }
            else //有多页
            {
                if (PageIndex == 0)//当前为第一页
                {
                    this.LBtnFirst.Enabled = false;
                    this.LBtnPrev.Enabled = false;
                    this.LBtnNext.Enabled = true;
                    this.LBtnLast.Enabled = true;

                }
                else if (PageIndex == PageCount - 1)//当前为最后页 
                {
                    this.LBtnFirst.Enabled = true;
                    this.LBtnPrev.Enabled = true;
                    this.LBtnNext.Enabled = false;
                    this.LBtnLast.Enabled = false;

                }
                else //中间页
                {
                    this.LBtnFirst.Enabled = true;
                    this.LBtnPrev.Enabled = true;
                    this.LBtnNext.Enabled = true;
                    this.LBtnLast.Enabled = true;
                }

            }
            if (RecordCount == 0)//当没有纪录时DataGrid.PageCount会显示1页
                this.LtlPageCount.Text = "0";
            else
                this.LtlPageCount.Text = PageCount.ToString();
            if (RecordCount == 0)
                this.LtlPageIndex.Text = "0";
            else
                this.LtlPageIndex.Text = (PageIndex + 1).ToString();//在有页数的情况下前台显示页数加1
            this.LtlPageSize.Text = PageSize.ToString();
            this.LtlRecordCount.Text = RecordCount.ToString();
        }

        protected void DataHelp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        // 总页数
        public int PageCount
        {
            get { return this.DataHelp.PageCount; }
        }
        //页大小
        public int PageSize
        {
            get { return this.DataHelp.PageSize; }
        }
        //页索引，从零开始
        public int PageIndex
        {
            get { return this.DataHelp.CurrentPageIndex; }
            set { this.DataHelp.CurrentPageIndex = value; }
        }
        // 纪录总数
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

    }
}