<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="carQuery.aspx.cs" Inherits="TransdataCenter.carQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" DataSourceID="SqlDataSource1" PageSize="30">
            <Columns>
                <asp:BoundField DataField="ORDERID" HeaderText="序号" 
                    SortExpression="ORDERID" />
                <asp:BoundField DataField="ADJUSTDATE" HeaderText="日期" 
                    SortExpression="ADJUSTDATE" />
                <asp:BoundField DataField="TEAMNAME" HeaderText="车队名" 
                    SortExpression="TEAMNAME" />
                <asp:BoundField DataField="LINENAME" HeaderText="线路名" 
                    SortExpression="LINENAME" />
                <asp:BoundField DataField="BUSCLASS" HeaderText="BUSCLASS" 
                    SortExpression="BUSCLASS" />
                <asp:BoundField DataField="BUSID" HeaderText="公交车编号" SortExpression="BUSID" />
                <asp:BoundField DataField="BUSTIME" HeaderText="班次" 
                    SortExpression="BUSTIME" />
                <asp:BoundField DataField="DRIVERCODE" HeaderText="驾驶员编号" 
                    SortExpression="DRIVERCODE" />
                <asp:BoundField DataField="DRIVERNAME" HeaderText="驾驶员名字" 
                    SortExpression="DRIVERNAME" />
                <asp:BoundField DataField="PLANENTERTIME" HeaderText="计划报到时间" 
                    SortExpression="PLANENTERTIME" />
                <asp:BoundField DataField="ENTERTIME" HeaderText="实际报到时间" 
                    SortExpression="ENTERTIME" />
                <asp:BoundField DataField="ENTERDELAY" HeaderText="报到延误时间（秒）" 
                    SortExpression="ENTERDELAY" />
                <asp:BoundField DataField="PLANOUTTIME" HeaderText="计划出车时间" 
                    SortExpression="PLANOUTTIME" />
                <asp:BoundField DataField="OUTTIME" HeaderText="实际出车时间" 
                    SortExpression="OUTTIME" />
                <asp:BoundField DataField="OUTDELAY" HeaderText="出车延误时间（秒）" 
                    SortExpression="OUTDELAY" />
            </Columns>
        </asp:GridView>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>" 
            ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>" 
            
            SelectCommand="SELECT ORDERID, TO_CHAR(ADJUSTDATE, 'yyyy-mm-dd') AS ADJUSTDATE, TEAMNAME, LINENAME, BUSCLASS, BUSID, BUSTIME, DRIVERCODE, DRIVERNAME, TO_CHAR(PLANENTERTIME, 'hh24:mi:ss') AS PLANENTERTIME, TO_CHAR(ENTERTIME, 'hh24:mi:ss') AS ENTERTIME, ENTERDELAY, TO_CHAR(PLANOUTTIME, 'hh24:mi:ss') AS PLANOUTTIME, TO_CHAR(OUTTIME, 'hh24:mi:ss') AS OUTTIME, OUTDELAY FROM WEB_PIMSDISPATCH T WHERE (&quot;BUSID&quot; = :BUSID)" 
            onselected="SqlDataSource1_Selected">
            <SelectParameters>
                <asp:QueryStringParameter Name="BUSID" QueryStringField="BUSID" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </center>
</asp:Content>
