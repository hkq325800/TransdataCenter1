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
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    //注册Google翻译脚本
        //    ClientScript.RegisterClientScriptInclude("GoogleAPI", "http://www.google.com/jsapi");
        //    ClientScript.RegisterClientScriptInclude("GoogleTrans", "/JSCode/GoogleTrans.js");
        //}
        //Widget列表
        bool debug = true;
        if (!debug)
        {
            List<WidgetList> rightList = Employee.GetSessionEmp(this).GetShowWidgetList();
            foreach (WidgetList temp in rightList)
            {
                Literal lt = new Literal();
                lt.Mode = LiteralMode.PassThrough;
                lt.Text = @"<div class=""contenTitle"">" + temp.MODULENAME + @"</div> <div>";
                ContentPanel.Controls.Add(lt);
                string ctrlnName = temp.CONTROLNAME;
                Control tmp = LoadControl(ctrlnName + ".ascx");
                ContentPanel.Controls.Add(tmp);
                Literal lt2 = new Literal();
                lt2.Text = "</div><br />";
                ContentPanel.Controls.Add(lt2);
            }
        }
    }
}
