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
    public partial class Report_forms : SmartSessionPage
    {
        private int recordCount;
        private int pageCount;
        bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["type"] == "com")
                    {
                        lblTitle.Text = "公交公司人员情况";
                        DataGridDataBind2();
                    }
                    else if (Request.QueryString["type"] == "work")
                    {
                        lblTitle.Text = "公交公司人员工种情况";
                        DataGridDataBind1();
                    }
                    else { DataGridDataBind(); }
                }
                catch
                { 
                    return;
                }
            }
                
            //if (Identity == "00")
            //{
            //    LBcompany.Visible = true;
            //    LBtype.Visible = true;
            //}
            //else
            //{
            //    LinkButton1.Visible = true;
            //    LinkButton2.Visible = true;
            //}
        }

        private void DataGridDataBind()
        {
            string req = Identity;
            flag = false;
            try
            {
                DataTable dt = webBLL.getEmpInfosum(req);
                if (recordCount == 0)
                {
                    this.DataGrid2.CurrentPageIndex = 0;
                }
                else if (this.DataGrid2.CurrentPageIndex >= pageCount)
                {
                    this.DataGrid2.CurrentPageIndex = pageCount - 1;
                }
                this.DataGrid2.DataSource = dt;
                this.DataGrid2.DataBind();
                //NavigationStateChange();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }

        }

        /// <summary>
        /// 人员情况（按工种）
        /// </summary>
        private void DataGridDataBind1()
        {
            string req = Identity;
            flag = false;
            try
            {
                DataTable dt = webBLL.getEmpInfotype();
                if (recordCount == 0)
                {
                    this.DataGrid2.CurrentPageIndex = 0;
                }
                else if (this.DataGrid2.CurrentPageIndex >= pageCount)
                {
                    this.DataGrid2.CurrentPageIndex = pageCount - 1;
                }
                this.DataGrid2.DataSource = dt;
                this.DataGrid2.DataBind();
                //NavigationStateChange();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }
        }

        /// <summary>
        /// 人员情况（按公司）
        /// </summary>
        private void DataGridDataBind2()
        {
            string req = Identity;
            flag = false;
            try
            {
                DataTable dt = webBLL.getEmpInfodep(Identity);
                if (recordCount == 0)
                {
                    this.DataGrid2.CurrentPageIndex = 0;
                }
                else if (this.DataGrid2.CurrentPageIndex >= pageCount)
                {
                    this.DataGrid2.CurrentPageIndex = pageCount - 1;
                }
                this.DataGrid2.DataSource = dt;
                this.DataGrid2.DataBind();
                //NavigationStateChange();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/index.aspx");
            }
        }
        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    DataGridDataBind();
        //}

     
        //protected void LBtype_Click1(object sender, EventArgs e)
        //{
        //    DataGridDataBind1();
        //}

        //protected void LinkButton1_Click1(object sender, EventArgs e)
        //{
        //    DataGridDataBind2();
        //}

        //protected void LinkButton2_Click(object sender, EventArgs e)
        //{
        //    DataGridDataBind();
        //}
    }
}