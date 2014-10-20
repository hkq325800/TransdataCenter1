<%@ Page Title="" Language="C#" MasterPageFile="~/PIMSSite.Master" AutoEventWireup="true"
    CodeFile="ParkinfoQuery.aspx.cs" Inherits="ParkinfoQuery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="d2" style="background-image: url('../images/泊位查询.gif'); background-repeat: no-repeat;
        height: 44px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td>
                        停车场:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                        泊位区域:
                        <asp:DropDownList ID="ddlParkArea" runat="server" AutoPostBack="True" />
                        车辆类型:
                        <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox>
                        泊位类型:
                        <asp:TextBox ID="TextBox7" runat="server" Width="100px"></asp:TextBox>
                        泊位状态:
                        <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ImageButton ID="btnQuery" runat="server" ImageUrl="../images/btn/查询.gif" OnClick="btnQuery_Click" />
                    </td>
                      <td><asp:Label ID="lblCount" runat="server"></asp:Label></td>
                </tr>
                </caption>
            </table>
            <center>
                <div>
                    <asp:GridView ID="GV" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333"
                        AutoGenerateColumns="False" BorderColor="#0099FF" BorderWidth="1px" AllowSorting="True"
                        OnRowDataBound="GV_RowDataBound" OnSorting="GV_Sorting" Width="100%" PageSize="32"
                        OnPageIndexChanging="GV_PageIndexChanging">
                        <Columns>
                            <%-- <asp:BoundField DataField="" HeaderText="编号" />--%>
                            <asp:BoundField DataField="PARKID" HeaderText="编号" SortExpression="PARKID" />
                            <asp:BoundField DataField="PARKNO" HeaderText="泊位编号" SortExpression="PARKNO" />
                            <asp:BoundField DataField="AREAOWNER" HeaderText="所属停车场" SortExpression="AREAOWNER" />
                            <asp:BoundField DataField="parkarea" HeaderText="泊位区域" SortExpression="parkarea" />
                            <asp:BoundField DataField="GARAGEOWNER" HeaderText="车库编号" SortExpression="GARAGEOWNER" />
                            <asp:BoundField DataField="PARKBUSTYPE" HeaderText="车辆类型" SortExpression="PARKBUSTYPE" />
                            <asp:BoundField DataField="PARKTYPE" HeaderText="泊位类型" SortExpression="PARKTYPE" />
                            <asp:BoundField DataField="PARKSTATE" HeaderText="泊位状态" SortExpression="PARKSTATE" />
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
