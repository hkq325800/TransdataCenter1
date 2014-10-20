<%@ Page Title="" Language="C#" MasterPageFile="~/PIMSSite.Master" AutoEventWireup="true"
    CodeFile="ParkWatch.aspx.cs" Inherits="ParkWatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/CommCtrl/uc_DateConvert.ascx" TagName="DateConvert" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="d2" style="background-image: url('../images/泊位巡更.gif'); background-repeat: no-repeat;
        height: 44px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                
              <tr>
                    <td>
                        车辆编号:
                        <asp:TextBox ID="txtBusSelfNo" runat="server" Width="100px"></asp:TextBox>
                        车牌号:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtBusNo" runat="server" Width="100px"></asp:TextBox>
                        巡更员卡号:
                        <asp:TextBox ID="txtWatchId" runat="server" Width="100px"></asp:TextBox>
                        巡更人员:
                        <asp:TextBox ID="txtWatchName" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        泊位编号:
                        <asp:TextBox ID="txtParkNo" runat="server" Width="100px"></asp:TextBox>
                        设备编号:
                        <asp:TextBox ID="txtEquipNo" runat="server" Width="100px"></asp:TextBox>
                        停车场:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtAreaOwner" runat="server" Width="100px"></asp:TextBox>
                    </td>
                      <td><asp:Label ID="lblCount" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <uc1:DateConvert ID="DateConvert1" runat="server" />
                        <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="../images/btn/查询.gif" OnClick="btnQuery_Click" />
                    </td>
                </tr>
            </table>
            <center>
                <div>
                    <asp:GridView ID="GV" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                         AutoGenerateColumns="False" BorderColor="#0099FF" BorderWidth="1px"
                        AllowSorting="True" OnRowDataBound="GV_RowDataBound" OnPageIndexChanging="GV_PageIndexChanging" OnSorting="GV_Sorting" Width="100%" PageSize="32">
                         <Columns>
                            <%-- <asp:BoundField DataField="" HeaderText="编号" />--%>
                            <asp:BoundField DataField="BusSelfNo" HeaderText="车辆编号" SortExpression="BusSelfNo" />
                            <asp:BoundField DataField="BusNo" HeaderText="车牌号" SortExpression="BusNo" />
                            <asp:BoundField DataField="parkno" HeaderText="泊位编号" SortExpression="parkno" />
                            <asp:BoundField DataField="WATCHERID" HeaderText="巡更员卡号" SortExpression="empno" />
                            <asp:BoundField DataField="WATCHERName" HeaderText="巡更人员" />
                            <asp:BoundField DataField="WATCHDATE" HeaderText="巡更时间" />
                            <asp:BoundField DataField="AREAOWNER" HeaderText="所属停车场" />
                            <asp:BoundField DataField="equipNo" HeaderText="巡更设备编号" />
                            <asp:BoundField DataField="OPTID" HeaderText="操作员ID" />
                            <asp:BoundField DataField="MODIFYTIME" HeaderText="修改时间" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
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
