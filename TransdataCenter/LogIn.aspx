<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TransdataCenter.LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/><title>

</title>
    <style type="text/css">
        .style1
        {
            width: 76px;
        }
        
    </style>
    </head>
<body style = "margin: 0 auto; font-size:14px; font-family: 宋体; color:#545454; text-align: center;">
    <form runat = "server">
    <center>
        <div style = "width:1002px;height: 385px;background-image:url('/images/Login.png')" id="pic" runat ="server" >
            <table style="width:265px; position:relative; top:186px; left: 148px; height: 139px; margin-top: 0px; text-align: center">
                <tbody><tr>
                    <td class="style1">
                        用户名</td>
                    <td>
                        <asp:TextBox ID="TextUser" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        密码</td>
                    <td><asp:TextBox ID="TextPassword" runat="server" TextMode="Password" OnTextChanged="TextBox1_TextChanged"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:CheckBox ID="CheckForRem" runat="server" Text="记住密码" />
                    </td>
                    <td class="style1">
                        <asp:Button ID="BtnLogIn" runat="server" Text="登录" BackColor="#385199" 
                            Font-Bold="True" Font-Names="微软雅黑" Font-Overline="False" Font-Size="12pt" 
                            Font-Strikeout="False" ForeColor="#CED6DD" Height="25px" 
                            onclick="BtnLogIn_Click" Width="110px" BorderStyle="None" />
                    </td>
                </tr>
            </tbody></table>
        </div>
        <div style = "position:relative; top: 120px; width: 100%; bottom:0px;float:left">
            <hr style = "float:left;width: 100%"/>
            <div style = "font-size:12px;font-family: 微软雅黑; margin: auto auto;">
             © 2009 杭州市公共交通集团有限公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         杭州市公共自行车服务发展有限公司 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
         版权所有<div></div>
         联系方式：400-816-0085网站备案号：浙ICP备 11063429号-1 
            </div>
        </div>
    </center>
    </form>    
</body>
</html>
