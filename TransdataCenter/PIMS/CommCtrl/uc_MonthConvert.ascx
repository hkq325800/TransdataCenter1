<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_MonthConvert.ascx.cs"
    Inherits="CommCtrl_uc_MonthConvert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<span class="fv_Text">开始时间:</span>
<asp:TextBox ID="txtBeginDate" runat="server" Width="100px"></asp:TextBox>
<cc1:CalendarExtender ID="txtBeginDate_CalendarExtender" Format="yyyy-MM-dd" runat="server"
    Enabled="True" TargetControlID="txtBeginDate">
</cc1:CalendarExtender>
<span class="fv_Text">结束时间:</span>
<asp:TextBox ID="txtEndDate" runat="server" Width="100px" Visible="True"></asp:TextBox>
<cc1:CalendarExtender ID="txtEndDate_CalendarExtender" Format="yyyy-MM-dd" runat="server"
    Enabled="True" TargetControlID="txtEndDate">
</cc1:CalendarExtender>
<asp:DropDownList ID="dlist" runat="server" CssClass="fv_Text" Width="120px">
    <asp:ListItem Selected="true">1月</asp:ListItem> 
    <asp:ListItem>2月</asp:ListItem>
        <asp:ListItem>3月</asp:ListItem> 
    <asp:ListItem>4月</asp:ListItem>
        <asp:ListItem>5月</asp:ListItem> 
    <asp:ListItem>6月</asp:ListItem>
        <asp:ListItem>7月</asp:ListItem> 
    <asp:ListItem>8月</asp:ListItem>
        <asp:ListItem>9月</asp:ListItem> 
    <asp:ListItem>10月</asp:ListItem>
        <asp:ListItem>11月</asp:ListItem> 
    <asp:ListItem>12月</asp:ListItem>

</asp:DropDownList>
