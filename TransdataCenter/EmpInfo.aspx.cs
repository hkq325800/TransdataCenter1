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
    public partial class EmpInfo : System.Web.UI.Page
    {

        private int recordCount;
        private int pageCount;
        bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)//首次加载访问
            {
                DataGridDataBind();
            }
        }

       protected void DDLFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "EmpInfo.aspx?";
            if (DDLFilter.SelectedIndex == 0)
            {
                query += "filter=all";
            }
            else
            {
                query += "filter=con";
            }

            if (TextBox1.Text == null || TextBox1.Text == "")
            {
                return;
            }
            else
            {
                query += "&name=" + Server.UrlEncode(TextBox1.Text);
            }
            Response.Redirect(query);
         
        }

       protected void DDLSearch_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (DDLSearch.Text == "姓名")
           {
               DDLFilter_SelectedIndexChanged(sender, e);
           }
           else
           {
               string query = "EmpInfo.aspx?";
               if (DDLFilter.SelectedIndex == 0)
               {
                   query += "filter=all";
               }
               else
               {
                   query += "filter=con";
               }

               if (TextBox1.Text == null || TextBox1.Text == "")
               {
                   return;
               }
               else
               {
                   query += "&num=" + Server.UrlEncode(TextBox1.Text);
               }
               Response.Redirect(query);
           }
       }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == null || TextBox1.Text == "")
            {
                return;
            }
            else if (DDLSearch.Text == "姓名")
            {
                DDLFilter_SelectedIndexChanged(sender, e);
            }
            else
            {
                DDLSearch_SelectedIndexChanged(sender, e);
            }
        }
        //绑定数据
        private void DataGridDataBind()
        {
            string req = "";
            DataTable dt=null;
            flag = false;
            try
            {
                string filter = null;
                string name = null;
                string number = null;
                
                filter = Request.QueryString["filter"];
                name = Request.QueryString["name"];
                number = Request.QueryString["num"];
                
                if (filter == null || filter == "all")
                {
                    this.DDLFilter.SelectedIndex = 0;
                }
                else
                {
                    this.DDLFilter.SelectedIndex = 1;
                    req += " where empid IN (SELECT empid FROM web_contractinfo@ehr) ";
                }
                if (name != null && name != "")
                {
                    req = " where empname = '" + Server.UrlDecode(name) + "'";
                }
                if (number != null && number != "")
                {
                    req = " where empid ='" + Server.UrlDecode(number) + "'";
                }
                if (req == null || req == "") 
                {
                    //req = "where rownum<=10";
                    flag = true;
                }
                if (flag == false)
                {
                    dt = webBLL.getEmpInfo(req, flag);
                    recordCount = dt.Rows.Count;
                    //获取当前的页数
                    pageCount = (int)Math.Ceiling(recordCount * 1.0 / PageSize);
                    //避免纪录从有到无时，并且已经进行过反页的情况下CurrentPageIndex > PageCount出错
                    if (recordCount == 0)
                    {
                        this.DGEmpInfo.CurrentPageIndex = 0;
                    }
                    else if (this.DGEmpInfo.CurrentPageIndex >= pageCount)
                    {
                        this.DGEmpInfo.CurrentPageIndex = pageCount - 1;
                    }
                    this.DGEmpInfo.DataSource = dt;
                    this.DGEmpInfo.DataBind();
                    //NavigationStateChange();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }

        }


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            //InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        //private void InitializeComponent()
        //{
        //    this.LBtnFirst.Click += new System.EventHandler(this.LBtnNavigation_Click);
        //    this.LBtnPrev.Click += new System.EventHandler(this.LBtnNavigation_Click);
        //    this.LBtnNext.Click += new System.EventHandler(this.LBtnNavigation_Click);
        //    this.LBtnLast.Click += new System.EventHandler(this.LBtnNavigation_Click);
        //    this.Load += new System.EventHandler(this.Page_Load);
        //}
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

        /// <summary>
        /// 控制导航按钮或数字的状态
        /// </summary>
        //public void NavigationStateChange()
        //{
        //    if (PageCount <= 1)//( RecordCount <= PageSize )//小于等于一页
        //    {
        //        this.LBtnFirst.Enabled = false;
        //        this.LBtnPrev.Enabled = false;
        //        this.LBtnNext.Enabled = false;
        //        this.LBtnLast.Enabled = false;
        //    }
        //    else //有多页
        //    {
        //        if (PageIndex == 0)//当前为第一页
        //        {
        //            this.LBtnFirst.Enabled = false;
        //            this.LBtnPrev.Enabled = false;
        //            this.LBtnNext.Enabled = true;
        //            this.LBtnLast.Enabled = true;

        //        }
        //        else if (PageIndex == PageCount - 1)//当前为最后页 
        //        {
        //            this.LBtnFirst.Enabled = true;
        //            this.LBtnPrev.Enabled = true;
        //            this.LBtnNext.Enabled = false;
        //            this.LBtnLast.Enabled = false;

        //        }
        //        else //中间页
        //        {
        //            this.LBtnFirst.Enabled = true;
        //            this.LBtnPrev.Enabled = true;
        //            this.LBtnNext.Enabled = true;
        //            this.LBtnLast.Enabled = true;
        //        }

        //    }
        //    if (RecordCount == 0)//当没有纪录时DataGrid.PageCount会显示1页
        //        this.LtlPageCount.Text = "0";
        //    else
        //        this.LtlPageCount.Text = PageCount.ToString();
        //    if (RecordCount == 0)
        //        this.LtlPageIndex.Text = "0";
        //    else
        //        this.LtlPageIndex.Text = (PageIndex + 1).ToString();//在有页数的情况下前台显示页数加1
        //    this.LtlPageSize.Text = PageSize.ToString();
        //    this.LtlRecordCount.Text = RecordCount.ToString();
        //}
        // 总页数
        public int PageCount
        {
            get { return this.DGEmpInfo.PageCount; }
        }
        //页大小
        public int PageSize
        {
            get { return this.DGEmpInfo.PageSize; }
        }
        //页索引，从零开始
        public int PageIndex
        {
            get { return this.DGEmpInfo.CurrentPageIndex; }
            set { this.DGEmpInfo.CurrentPageIndex = value; }
        }
        // 纪录总数
        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

    }
}