<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Repairdetails.aspx.cs" Inherits="TransdataCenter.js.WebForm1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <center dir="ltr">

        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="Lbusstyle" runat="server" Font-Bold="False" Text="车型:" 
            Width="64px"></asp:Label>
&nbsp;&nbsp;
        <asp:DropDownList ID="DDLbusstyle" runat="server" Font-Size="Medium" 
            Height="22px"  
            Width="100px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>全选</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Lcom" runat="server" Text="修理公司:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DDLcom" runat="server" Font-Size="Medium" Height="22px" 
            Width="100px">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>全选</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;

        <asp:Button ID="Butsearch" runat="server" Text="查询" AccessKey="1" Height="25px" 
            Width="65px" />









        <br />
        <br />        
    </div>
    <div style = "clear:both">
        <table id="Table1" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="450" align="center"
                   border="1">
                   <tr>

            <asp:DataGrid 
                class="dataTable" 
                allowpaging="False"
                id="DataGrid1"
                pagesize="30"
                runat="server" Width="1300" ItemStyle-VerticalAlign="NotSet" EditItemStyle-VerticalAlign="NotSet" EditItemStyle-HorizontalAlign="NotSet" Enabled="True" ItemStyle-HorizontalAlign="NotSet" PagerStyle-VerticalAlign="NotSet" PagerStyle-HorizontalAlign="NotSet" FooterStyle-HorizontalAlign="NotSet" FooterStyle-VerticalAlign="NotSet" FooterStyle-BorderStyle="NotSet" AllowSorting="False" AlternatingItemStyle-HorizontalAlign="NotSet" AlternatingItemStyle-VerticalAlign="NotSet" BorderStyle="NotSet" CaptionAlign="NotSet" SelectedItemStyle-VerticalAlign="NotSet" SelectedItemStyle-HorizontalAlign="NotSet" HorizontalAlign="NotSet" HeaderStyle-HorizontalAlign="NotSet" HeaderStyle-VerticalAlign="NotSet">
                <%--<Columns>
                    <asp:BoundColumn DataField="EMPID" HeaderText="员工卡号" />
                    <asp:BoundColumn DataField="EMPNAME" HeaderText="员工姓名" />
                    <asp:BoundColumn DataField="EMPSEX" HeaderText="性别" />
                    <asp:BoundColumn DataField="EMPSELFID" HeaderText="单位内部编号" />
                    <asp:BoundColumn DataField="IDCARD" HeaderText="身份证号" />
                    <asp:BoundColumn DataField="BIRTHDAY" HeaderText="生日" />
                    <asp:BoundColumn DataField="OWNERDEP" HeaderText="所属公司" />
                    <asp:BoundColumn DataField="DEP" HeaderText="当前部门" />
                    <asp:BoundColumn DataField="POSTTYPE" HeaderText="工种" />
                    <asp:BoundColumn DataField="POSTNAME" HeaderText="岗位" />1174
                    <asp:BoundColumn DataField="NATIVEPLACE" HeaderText="籍贯" />
                    <asp:BoundColumn DataField="LASTTITLE" HeaderText="职称" />
                    <asp:BoundColumn DataField="EDUCATION" HeaderText="学历" />
                    <asp:BoundColumn DataField="MARITALSTATUS" HeaderText="婚姻状况" />
                    <asp:BoundColumn DataField="HOUSEHOLDREG" HeaderText="户口类型" />
                    <asp:BoundColumn DataField="HOUSEHOLDPLACE" HeaderText="户口所在地" />
                    <asp:BoundColumn DataField="TIMETOWORK" HeaderText="工作开始时间" />
                    <asp:BoundColumn DataField="TIMETOENTER" HeaderText="进单位时间" />
                    <asp:BoundColumn DataField="EMPLOYTYPE" HeaderText="雇用类别" />
                </Columns>--%>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            </asp:DataGrid>
            </TR>
            </TABLE>
            <table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="450" align="center"
                   border="1">
                   <tr>
                       <td style="WIDTH: 207px">
                            <asp:linkbutton id="LBtnFirst" runat="server" CommandName="First">首页</asp:linkbutton> 
                            <asp:linkbutton id="LBtnPrev" runat="server" CommandName="Prev">上一页</asp:linkbutton>  
                            <asp:linkbutton id="LBtnNext" runat="server" CommandName="Next">下一页</asp:linkbutton>
                            <asp:linkbutton id="LBtnLast" runat="server" CommandName="Last">尾页</asp:linkbutton> </TD>
                       <td>第
                            <asp:literal id="LtlPageIndex" runat="server"></asp:literal>页 共 
                            <asp:literal id="LtlPageCount" runat="server"></asp:literal>页 每页
                            <asp:literal id="LtlPageSize" runat="server"></asp:literal>条 共
                            <asp:literal id="LtlRecordCount" runat="server"></asp:literal>条
                       </TD>
                   </TR>
              </TABLE>
    </div>
</div>









        <br />
        <br />









        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;
        <br />









    </center>
</asp:Content>

