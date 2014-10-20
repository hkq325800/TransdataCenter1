﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BusTeamDetailQryFrm.ascx.cs"
    Inherits="CommCtrl_BusTeamDetailQryFrm" %>
<%@ Register Src="uc_DateConvert.ascx" TagName="uc_DateConvert" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.XtraReports.v9.1.Web, Version=9.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<table style="width: 100%;">
    <tr>
        <td>
            <span class="fv_Text">分公司:&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <asp:TextBox ID="txtCompany" runat="server" Width="100px"></asp:TextBox>
            <span class="fv_Text">车队:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
            <asp:TextBox ID="txtRoute" runat="server" Width="100px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:uc_DateConvert ID="uc_DateConvert1" runat="server" />
            <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="../images/btn/查询.gif" CssClass="imagebtn" />
        </td>
    </tr>
</table>
<center>
    <div>
        <dxxr:ReportToolbar ID="rptToolDetail" runat='server' ShowDefaultButtons='False'
            ReportViewer="<%# rptViewDetail %>">
            <Items>
                <dxxr:ReportToolbarButton ItemKind='PrintReport' ToolTip='打印报表' />
                <dxxr:ReportToolbarButton ItemKind='PrintPage' ToolTip='打印当前页' />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton Enabled='False' ItemKind='FirstPage' ToolTip='第一页' />
                <dxxr:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' ToolTip='前一页' />
                <dxxr:ReportToolbarLabel Text='第' />
                <dxxr:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                </dxxr:ReportToolbarComboBox>
                <dxxr:ReportToolbarLabel Text='页 共' />
                <dxxr:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                <dxxr:ReportToolbarButton ItemKind='NextPage' ToolTip='下一页' />
                <dxxr:ReportToolbarButton ItemKind='LastPage' ToolTip='最后一页' />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind='SaveToDisk' ToolTip='把报表导出并保存到磁盘' />
                <dxxr:ReportToolbarButton ItemKind='SaveToWindow' ToolTip='在新窗口中打开导出的报表' />
                <dxxr:ReportToolbarComboBox ItemKind='SaveFormat' Width='70px'>
                    <Elements>
                        <dxxr:ListElement Text='Xls' Value='xls' />
                        <dxxr:ListElement Text='Rtf' Value='rtf' />
                        <dxxr:ListElement Text='Mht' Value='mht' />
                        <dxxr:ListElement Text='Text' Value='txt' />
                        <dxxr:ListElement Text='Csv' Value='csv' />
                        <dxxr:ListElement Text='Image' Value='png' />
                    </Elements>
                </dxxr:ReportToolbarComboBox>
            </Items>
            <Styles>
                <LabelStyle>
                    <Margins MarginLeft='3px' MarginRight='3px' />
                </LabelStyle>
            </Styles>
        </dxxr:ReportToolbar>
        <dxxr:ReportViewer ID="rptViewDetail" runat="server">
        </dxxr:ReportViewer>
    </div>
</center>
