<%@ Page Title="" Language="C#" MasterPageFile="~/PIMSSite.Master" AutoEventWireup="true"
    CodeFile="DispatchPlan.aspx.cs" Inherits="DispatchPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CommCtrl/uc_DateConvert.ascx" TagName="DateConvert" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="d2" style="background-image: url('../images/调度计划.gif'); background-repeat: no-repeat;
        height: 44px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
               
                <tr>
                    <td>
                        <uc1:DateConvert ID="DateConvert1" runat="server" />
                        <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="../images/btn/查询.gif" 
                            onclick="btnQuery_Click" />
                    </td>
                      <td><asp:Label ID="lblCount" runat="server"></asp:Label></td>
                </tr>
            </table>
            <center>
                <div style="overflow-x: scroll; ">
                    <asp:GridView ID="GV" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                         AutoGenerateColumns="False" BorderColor="#0099FF" BorderWidth="1px"
                        AllowSorting="True" OnRowDataBound="GV_RowDataBound" OnSorting="GV_Sorting" Width="100%"
                        PageSize="32" OnPageIndexChanging="GV_PageIndexChanging">
                        <Columns>
                            <%-- <asp:BoundField DataField="" HeaderText="编号" />--%>
                            <asp:BoundField DataField="ADJUSTDATE" HeaderText="日期" SortExpression="ADJUSTDATE" />
                            <asp:BoundField DataField="LINENAME" HeaderText="线路名称" SortExpression="LINENAME" />
                            <asp:BoundField DataField="BUSSELFNO" HeaderText="车辆编号" SortExpression="BUSSELFNO" />
                            <asp:BoundField DataField="BUSCLASS" HeaderText="班别" SortExpression="BUSCLASS" />
                            <asp:BoundField DataField="DRIVERACODE" HeaderText="司机A编号" />
                            <asp:BoundField DataField="DRIVERANAME" HeaderText="司机A姓名" />
                            <asp:BoundField DataField="STEWARDACODE" HeaderText="乘务员A编号" />
                            <asp:BoundField DataField="STEWARDANAME" HeaderText="乘务员A姓名" />
                            <asp:BoundField DataField="STEWARDBCODE" HeaderText="乘务员B编号" />
                            <asp:BoundField DataField="STEWARDBNAME" HeaderText="乘务员B姓名" />
                            <asp:BoundField DataField="DRIVERBCODE" HeaderText="司机B编号" />
                            
                             <asp:BoundField DataField="DRIVERBNAME" HeaderText="司机B姓名" />
                            <asp:BoundField DataField="FIRSTTIME" HeaderText="A班计划出场时间" />
                            <asp:BoundField DataField="CHANGETIME" HeaderText="计划交接班时间" />
                            <asp:BoundField DataField="ABACKTIME" HeaderText="A班计划回场时间" />
                            <asp:BoundField DataField="BBACKTIME" HeaderText="B班计划回场时间" />
                            
                            <asp:BoundField DataField="AREA_OWNER" HeaderText="停车场" />
                            <asp:BoundField DataField="OPTID" HeaderText="操作员ID" />
                            <asp:BoundField DataField="MODIFYTIME" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                            <asp:BoundField DataField="SW_STATUS" HeaderText="排班状态" />
                            <asp:BoundField DataField="REMARK" HeaderText="备注" />
                        </Columns>
                        <RowStyle ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <PagerTemplate>
                            <div style="text-align: center; color: Blue">
                                <asp:LinkButton ID="cmdFirstPage" runat="server" CommandName="Page" CommandArgument="First"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">首页</asp:LinkButton>
                                <asp:LinkButton ID="cmdPreview" runat="server" CommandArgument="Prev" CommandName="Page"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=0 %>">前页</asp:LinkButton>
                                第<asp:Label ID="lblcurPage" ForeColor="Blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1  %>'></asp:Label>页/共<asp:Label
                                    ID="lblPageCount" ForeColor="blue" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount %>'></asp:Label>页
                                <asp:LinkButton ID="cmdNext" runat="server" CommandName="Page" CommandArgument="Next"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">后页</asp:LinkButton>
                                <asp:LinkButton ID="cmdLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                    Enabled="<%# ((GridView)Container.Parent.Parent).PageIndex!=((GridView)Container.Parent.Parent).PageCount-1 %>">尾页</asp:LinkButton>
                                &nbsp;<asp:TextBox ID="txtGoPage" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex+1 %>'
                                    Width="32px"></asp:TextBox>页<asp:Button ID="TurnBtn" runat="server" OnClick="Turn_Click"
                                        Text="转到" /></div>
                        </PagerTemplate>
                    </asp:GridView>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
