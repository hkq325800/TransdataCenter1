<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_DayConvert.ascx.cs"
    Inherits="CommCtrl_uc_DayConvert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<span class="fv_Text">开始时间:</span>
<asp:TextBox ID="txtBeginDate" runat="server" Width="100px"></asp:TextBox>
<cc1:CalendarExtender ID="txtBeginDate_CalendarExtender" Format="yyyy-MM-dd" runat="server"
    Enabled="True" TargetControlID="txtBeginDate">
</cc1:CalendarExtender>

<asp:DropDownList ID="dlist" runat="server" CssClass="fv_Text" Width="120px">
    <asp:ListItem>自定义</asp:ListItem>
    <asp:ListItem>今天</asp:ListItem>
    <asp:ListItem>昨天</asp:ListItem>
    
</asp:DropDownList>
