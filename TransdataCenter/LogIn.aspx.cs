using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using hdcweb.soc.BLL;

namespace TransdataCenter
{
    public partial class LogIn : SmartSessionPage
    {
        public List<string> ls;
        bool isCookie = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "logout")//若是通过退出登录按钮转到该界面则清除cookies
                {
                    Request.Cookies.Remove("UserNameCookie");
                    Request.Cookies.Remove("UserPasswordCookie");
                }
                HttpCookie UserNameCookie = Request.Cookies["UserNameCookie"];
                HttpCookie UserPasswordCookie = Request.Cookies["UserPasswordCookie"];
                //判断是否有cookie值，有的话就读取出来
                if (UserNameCookie != null && UserPasswordCookie != null)
                {
                    CheckForRem.Checked = true;
                    isCookie = true;
                    TextUser.Text = UserNameCookie["UserName"];
                    TextPassword.Text=UserPasswordCookie["UserPassword"];
                    //TextPassword.Attributes.Add("value", UserPasswordCookie["UserPassword"]);
                    BtnLogIn_Click(sender, e);//记住密码如需自动登录则取消
                }
            }
        }

        public static string Encrypt(string str)//MD5加密方法
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }

        public void SetToCookie(HttpCookie httpcookie, string cookiename, string cookievalue)
        {
            httpcookie.Values[cookiename] = cookievalue;
            httpcookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(httpcookie);
        }

        protected void BtnLogIn_Click(object sender, EventArgs e)
        {
            if (TextUser.Text == "" || TextPassword.Text == "")
            {
                this.Response.Write("<script LANGUAGE=JavaScript >" + " alert('请填写用户名和密码!');" + " window.location=('Login.aspx?type=logout');" + "</script>");
                return;
            }
            HttpCookie UserNameCookie = Request.Cookies["UserNameCookie"];
            HttpCookie UserPasswordCookie = Request.Cookies["UserPasswordCookie"];
            ls = webBLL.verifyIdentify(TextUser.Text.Trim(), Encrypt(TextPassword.Text.Trim()));
            bool login = false;
            if (ls == null)//先行判空
            {
                this.Response.Write("<script LANGUAGE=JavaScript >" + " alert('服务器正在维护 请稍后重试!');" + " window.location=('Login.aspx?type=logout');" + "</script>");
                //this.Response.Write("<script>alert('请填写用户名和密码!')</script>");
                return;
            }
            else if (ls.Count == 1)
            {
                this.Response.Write("<script LANGUAGE=JavaScript >" + " alert('用户名密码错误!');" + " window.location=('Login.aspx?type=logout');" + "</script>");
                return;
            }
            login = ls[2] == "1";
            if (CheckForRem.Checked)//勾选记住密码
            {
                if (UserNameCookie == null)
                {
                    UserNameCookie = new HttpCookie("UserNameCookie");
                    UserNameCookie.Values.Add("UserName", TextUser.Text);
                    UserNameCookie.Values.Add("UserPassword", TextPassword.Text);
                    UserNameCookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(UserNameCookie);
                }
                else if (UserNameCookie.Values["UserName"] != TextUser.Text)
                {
                    SetToCookie(UserNameCookie, "UserName", TextUser.Text);
                }
                if (UserPasswordCookie == null)
                {
                    UserPasswordCookie = new HttpCookie("UserPassWordCookie");
                    UserPasswordCookie.Values.Add("UserPassword", TextPassword.Text);
                    UserPasswordCookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(UserPasswordCookie);
                }
                else if (UserPasswordCookie.Values["UserPassword"] != TextPassword.Text)
                {
                    SetToCookie(UserPasswordCookie, "UserPassword", TextPassword.Text);
                }
            }
            else
            {
                Request.Cookies.Remove("UserNameCookie");
                Request.Cookies.Remove("UserPasswordCookie");
            }
            //FormsAuthentication.RedirectFromLoginPage(TextUser.Text, true);
            //Response.Redirect("~/index.aspx");
            //if (isCookie)
            //    login = webBLL.verifyIdentify(Request.Cookies["UserNameCookie"]["UserName"], Encrypt(Request.Cookies["UserPasswordCookie"]["UserPassword"])) == "1";
            //else
            if (login)
            {
                FormsAuthentication.RedirectFromLoginPage(ls[0], true);
                Name = ls[0];
                Identity = ls[1].Substring(0, 2);
                Dep = ls[3].ToString();
                Response.Redirect("~/Index.aspx");
            }
            else
            {
                this.Response.Write("<script LANGUAGE=JavaScript >" + " alert('请填写用户名和密码!');" + " window.location=('Login.aspx?type=logout');" + "</script>");
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            isCookie = false;
        }
    }
}