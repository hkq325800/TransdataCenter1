using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class PIMSSite : System.Web.UI.MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        CheckLoginSession();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //显示用户姓名
        Employee emp = Employee.GetSessionEmp(this);
        Label_UserInfo.Text = string.Format("※用户名：{0} ※性别：{1} ※部门：{2}", emp.EMPNAME, emp.EMPSEX, emp.DEPNAME);
        //生成左边菜单
        GetNavigateTable();
    }

    protected void CheckLoginSession()
    {
        if (Request["Token"] != null)
            Response.Redirect("./SSO.aspx?Token=" + Request["Token"]);
        else if (Employee.GetSessionEmp(this) == null)
        {
            string RedirUrl = (Common.SSOAddress() + "?AppID=102");
            Response.Redirect(RedirUrl);
        }
    }

    protected void GetNavigateTable()
    {
        Table cacheTable = (Table)Session["LeftMenu"];
        if (cacheTable == null)
        {
            Employee emp = Employee.GetSessionEmp(this);
            cacheTable = new Table();
            cacheTable.CellPadding = 0;
            cacheTable.CellSpacing = 0;
            cacheTable.Width = 140;
            foreach (ModuleInfo mi in emp.MODULES)
                if (mi.fatherID == "0")
                {
                    //第一层菜单
                    AddNavigateMenu(mi, cacheTable, "LeftMenuPanel_AppTD", "");
                    //第二层菜单
                    foreach (ModuleInfo submi in emp.MODULES)
                        if (submi.fatherID == mi.moduleID)
                            AddNavigateMenu(submi, cacheTable, "", "※ {0}");
                }
            Session["LeftMenu"] = cacheTable;
        }
        //设定选中项
        string url = Request.Url.ToString();
        for (int i = 0; i < cacheTable.Rows.Count; i++)
        {
            TableCell tc = cacheTable.Rows[i].Cells[0];
            HyperLink lbItem = (HyperLink)tc.Controls[0];
            //if (lbItem.CommandName == "4")
            //    lbItem.Click += new EventHandler(lbItem_Click);
            if (tc.CssClass != "LeftMenuPanel_AppTD")
            {
                //if (lbItem.CommandName == "2" && lbItem.PostBackUrl != string.Empty && url.IndexOf(lbItem.PostBackUrl.Replace("\\", "/")) > -1)
                if (lbItem.NavigateUrl != string.Empty && url.IndexOf(lbItem.NavigateUrl) > -1)
                    tc.CssClass = "LeftMenuPanel_SelectTD";
                else tc.CssClass = "";
            }
        }
        //将列表加入页面中
        LeftMenuPanel.Controls.Add(cacheTable);
    }

    private void AddNavigateMenu(ModuleInfo mi, Table table, string tdcssClass, string StrFormat)
    {
        HyperLink lbItem = new HyperLink();
        TableCell tc = new TableCell();
        TableRow tr = new TableRow();
        lbItem.Text = StrFormat.Trim() == "" ? mi.moduleName : string.Format(StrFormat, mi.moduleName);
        //switch (mi.moduleType)
        //{
        //    case 4:
        //        lbItem.CommandName = "4";
        //        lbItem.CommandArgument = mi.moduleAddress.Replace("\\", "/");
        //        break;
        //    default:
        //        lbItem.CommandName = "2";
        //        lbItem.PostBackUrl = mi.moduleAddress.Replace("\\", "/");
        //        break;
        //}
        lbItem.NavigateUrl = mi.moduleAddress.Replace("\\", "/");
        tc.CssClass = tdcssClass;
        tc.Controls.Add(lbItem);
        tr.Controls.Add(tc);
        table.Rows.Add(tr);
    }

    protected void lbItem_Click(object sender, EventArgs e)
    {
        LinkButton obj = (LinkButton)sender;
        switch (obj.CommandName)
        {
            case "4":
                string url = obj.CommandArgument;
                //SingleSign.Redirect(Response, url, Employee.GetSessionEmp(this).EMPID);
                break;
        }
    }

    protected void btn_Logout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Clear();
        Common.RedirectToSSO(Response, 0);
    }
}
