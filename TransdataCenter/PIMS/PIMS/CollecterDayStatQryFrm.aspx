<%@ Page Title="" Language="C#" MasterPageFile="~/PIMSSite.Master" AutoEventWireup="true"
    CodeFile="CollecterDayStatQryFrm.aspx.cs" Inherits="CollecterDayStatQryFrm" %>

<%@ Register Assembly="DevExpress.XtraReports.v9.1.Web, Version=9.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/CommCtrl/uc_DateConvert.ascx" TagName="DateConvert" TagPrefix="uc1" %>--%>
<%@ Register Src="../CommCtrl/uc_DayConvert.ascx" TagName="uc_DayConvert" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 310px;
        }
        .style2
        {
            width: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="d2" style="background-image: url('../images/����Ա������ͳ�Ʊ�.gif'); background-repeat: no-repeat;
        height: 44px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td class="style1">
                        <uc2:uc_DayConvert ID="uc_DayConvert1" runat="server" />
                    </td>
                    <%-- <uc1:DateConvert ID="DateConvert1" runat="server" />--%>
                    <%--<td class="style2">
                        <asp:RadioButtonList ID="rbl" runat="server" 
                            RepeatDirection="Horizontal" Width="156px">
                            <asp:ListItem Selected="True" Value="stat">��ͳ��</asp:ListItem>
                            <asp:ListItem Value="nostat">δͳ��</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                    <td>
                        <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="../images/btn/��ѯ.gif" CssClass="imagebtn" />
                    </td>
                </tr>
            </table>
            <center>
                <div>
                    <dxxr:ReportToolbar ID="rptToolDetail" runat='server' ShowDefaultButtons='False'
                        ReportViewer="<%# rptViewDetail %>">
                        <Items>
                            <dxxr:ReportToolbarButton ItemKind='PrintReport' ToolTip='��ӡ����' />
                            <dxxr:ReportToolbarButton ItemKind='PrintPage' ToolTip='��ӡ��ǰҳ' />
                            <dxxr:ReportToolbarSeparator />
                            <dxxr:ReportToolbarButton Enabled='False' ItemKind='FirstPage' ToolTip='��һҳ' />
                            <dxxr:ReportToolbarButton Enabled='False' ItemKind='PreviousPage' ToolTip='ǰһҳ' />
                            <dxxr:ReportToolbarLabel Text='��' />
                            <dxxr:ReportToolbarComboBox ItemKind='PageNumber' Width='65px'>
                            </dxxr:ReportToolbarComboBox>
                            <dxxr:ReportToolbarLabel Text='ҳ ��' />
                            <dxxr:ReportToolbarTextBox IsReadOnly='True' ItemKind='PageCount' />
                            <dxxr:ReportToolbarButton ItemKind='NextPage' ToolTip='��һҳ' />
                            <dxxr:ReportToolbarButton ItemKind='LastPage' ToolTip='���һҳ' />
                            <dxxr:ReportToolbarSeparator />
                            <dxxr:ReportToolbarButton ItemKind='SaveToDisk' ToolTip='�ѱ����������浽����' />
                            <dxxr:ReportToolbarButton ItemKind='SaveToWindow' ToolTip='���´����д򿪵����ı���' />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
