<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParkInfo.aspx.cs" Inherits="TransdataCenter.ParkInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="switch" style="width:auto;margin-top:-35px;margin-right:150px" >
                日期：
        <asp:DropDownList ID="DateDropDownList" runat="server" AutoPostBack="True" Font-Size="Medium"
            OnSelectedIndexChanged="DateDropDownList_SelectedIndexChanged">
            <asp:ListItem>今日</asp:ListItem>
            <asp:ListItem>昨日</asp:ListItem>
        </asp:DropDownList>
                    筛选：
        <asp:DropDownList ID="DropDownList1" runat="server"
            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Font-Size="Medium"
            AutoPostBack="True">
            <asp:ListItem>显示全部车辆</asp:ListItem>
            <asp:ListItem>只显示未出场车辆</asp:ListItem>
            <asp:ListItem>只显示出场车辆</asp:ListItem>
            <asp:ListItem>只显示准时出场车辆</asp:ListItem>
            <asp:ListItem>只显示延迟出场车辆</asp:ListItem>
        </asp:DropDownList>
                    </div>
        <div class="container">
            <div id="main_body">
                <div id="l_body">
                <%--<form id="search" action="#" name="search">
				<input class="s_form" type="text" name="" />文本框
				<input class="s_botton" type="submit" value=" "/>搜索按钮
			</form>--%>
                <span class="ceTitle">
                    <span class="ceTitle-l"></span>
                    <span class="ceTitle-m"></span>
                    <span class="ceTitle-r"></span>
                </span>
                <ul class="tree_p">
                    <li>

                        <span class="menu-title home">
                            <span class="img_left">
                                <img src="images/home.png"/></span>
                            <span class="img_middle"><a href="Index.aspx">首页</a></span>
                            <span class="img_right">
                                <img src="images/right.png" /></span>
                        </span>
                    </li>
                    <li onclick='show("tip1")'>

                        <span class="menu-title">
                            <span class="img_left"><img src="images/fix.png"></img></span>
                            <span class="img_middle"><a>修理信息系统</a></span>
                            <span class="img_right"><!--MonthReport.aspx-->
                                <img src="images/right.png" /></span>
                        </span>
                        <ul id="tip1" style="display: none;" class="tree_c">
                            <li><a href= "RepairSum.aspx">修理统计</a></li>
                            <li><a href="MonthReport.aspx?type=xx&date=day&unit=SQ">昨日修理信息</a></li>
                                <li><a href="MonthReport.aspx?type=xx&date=month&unit=SQ">上月修理信息</a></li>
                                <li><a href="repairinfo.aspx">车辆修理查询</a></li>
                        </ul>
                    </li>
                    <li onclick='show("tip2")'>

                        <span class="menu-title">
                            <span class="img_left"><img src="images/park.png"></span>
                                <span class="img_middle"><a>停车场系统</a></span>
                                <span class="img_right">
                                <img src="images/right.png" /></span><!--parkinfo.aspx-->
                        </span>
                        <ul id="tip2" class="tree_c">
                            <li id="today1"><a href="parkInfo.aspx?date=today&filter=1">未出场表单</a></li>
                            <li id="today2"><a href="parkInfo.aspx?date=today&filter=2">出场表单</a></li>
                            <li id="today3"><a href="parkInfo.aspx?date=today&filter=3">准时出场表单</a></li>
                            <li id="today4"><a href="parkInfo.aspx?date=today&filter=4">延时出场表单</a></li>
                        </ul>
                    </li>
                    <li onclick='show("tip3")'>

                        <span class="menu-title">
                            <span class="img_left">
                                <img src="images/bike.png"></span>
                            <span class="img_middle"><a>自行车租赁系统</a></span>
                            <span class="img_right">
                                <img src="images/right.png" /></span><!--hdcbike.aspx?gettype=1-->
                        </span>
                        <ul id="tip3" style="display: none;" class="tree_c">
                            <li><a href="hdcbike.aspx?gettype=1">网点信息</a></li>
                            <li><a href="hdcbike.aspx?gettype=2">昨日租还报表</a></li>
                        </ul>
                    </li>
                    <li onclick ='show("tip4")'>

                        <span class="menu-title">
                            <span class="img_left">
                                <img src="images/resource.png"></span>
                            <span class="img_middle"><a>人力资源管理系统</a></span>
                            <span class="img_right">
                                <img src="images/right.png" /></span><!--ReportForms.aspx-->
                        </span>
                        <ul id="tip4" style="display: none;" class="tree_c">
                            <li><a href="ReportForms.aspx?type=com">人员情况（按公司）</a></li>
                            <li><a href="ReportForms.aspx?type=work">人员情况（按工种）</a></li>
                            <li><a href="ContractMore.aspx">合同到期人员</a></li>
                            <li><a href="WorkFlowDetail.aspx?date=today&id=">人员变动情况</a></li>
                            <li><a href="EmpInfo.aspx">员工信息查询</a></li>
                        </ul>
                        &nbsp;
                    </li>
                </ul>
                </div><!-- end of left_nav-->
                
                <div id="r_body">
                    

                    <%--<asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                        EnableModelValidation="True" AllowPaging="True"
                        AutoGenerateColumns="False" CellPadding="4" PageSize="30"
                        OnPageIndexChanging="GridView1_PageIndexChanging" Width="800px" align="center>
                        <Columns>
                            <asp:BoundField DataField="ORDERID" HeaderText="序号" SortExpression="ORDERID" />
                            <asp:BoundField DataField="ADJUSTDATE" HeaderText="日期"
                                SortExpression="ADJUSTDATE" />
                            <asp:BoundField DataField="TEAMNAME" HeaderText="车队名"
                                SortExpression="TEAMNAME" />
                            <asp:BoundField DataField="LINENAME" HeaderText="线路名"
                                SortExpression="LINENAME" />
                            <asp:BoundField DataField="BUSID" HeaderText="公交车编号" SortExpression="BUSID" />
                            <asp:BoundField DataField="BUSTIME" HeaderText="班次" SortExpression="BUSTIME" />
                            <asp:BoundField DataField="DRIVERCODE" HeaderText="驾驶员编号"
                                SortExpression="DRIVERCODE" />
                            <asp:BoundField DataField="DRIVERNAME" HeaderText="驾驶员姓名"
                                SortExpression="DRIVERNAME" />
                            <asp:BoundField DataField="PLANENTERTIME" HeaderText="计划报到时间"
                                SortExpression="PLANENTERTIME" />
                            <asp:BoundField DataField="ENTERTIME" HeaderText="实际报到时间"
                                SortExpression="ENTERTIME" />
                            <asp:BoundField DataField="ENTERDELAY" HeaderText="报道延误(s)"
                                SortExpression="ENTERDELAY" />
                            <asp:BoundField DataField="PLANOUTTIME" HeaderText="计划出车时间"
                                SortExpression="PLANOUTTIME" />
                            <asp:BoundField DataField="OUTTIME" HeaderText="实际出车时间"
                                SortExpression="OUTTIME" />
                            <asp:BoundField DataField="OUTDELAY" HeaderText="出车延误(s)"
                                SortExpression="OUTDELAY" />
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#330099" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    </asp:GridView>--%>
                    <asp:DataGrid
                            class="dataTable"
                            AllowPaging="False"
                            ID="DGPark"
                            runat="server" Width="800px" Height="16px">



                            <HeaderStyle CssClass="dg_header" />
                            <AlternatingItemStyle CssClass="dg_alter" />
                            <PagerStyle CssClass="dg_page" />
                            <ItemStyle CssClass="dg_item" />
                        </asp:DataGrid>

                        <table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="800px" align="center"
                            border="1">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LBtnFirst" runat="server" OnClick="FirstPage" CommandName="First">首页</asp:LinkButton>
                                    <asp:LinkButton ID="LBtnPrev" runat="server" OnClick="PrePage" CommandName="Prev">上一页</asp:LinkButton>
                                    <asp:LinkButton ID="LBtnNext" runat="server" OnClick="NextPage" CommandName="Next">下一页</asp:LinkButton>
                                    <asp:LinkButton ID="LBtnLast" runat="server" OnClick="LastPage" CommandName="Last">尾页</asp:LinkButton>

                                </td>
                                <td>第
                            <asp:Literal ID="LtlPageIndex" runat="server"></asp:Literal>页__共 
                            <asp:Literal ID="LtlPageCount" runat="server"></asp:Literal>页__每页
                            <asp:Literal ID="LtlPageSize" runat="server"></asp:Literal>条__共
                            <asp:Literal ID="LtlRecordCount" runat="server"></asp:Literal>条
                                </td>
                            </tr>
                        </table>
                </div>

                <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionString2 %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionString2.ProviderName %>"></asp:SqlDataSource>--%>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var href = window.location.href;
            if (href.indexOf("parkInfo.aspx?") >= 0 && href.indexOf("filter=1") >= 0) {
                $("#today1").addClass("active").children().addClass("active");
            } else if (href.indexOf("parkInfo.aspx?") >= 0 && href.indexOf("filter=2") >= 0) {
                $("#today2").addClass("active").children().addClass("active");
            } else if (href.indexOf("parkInfo.aspx?") >= 0 && href.indexOf("filter=3") >= 0) {
                $("#today3").addClass("active").children().addClass("active");
            } else if (href.indexOf("parkInfo.aspx?") >= 0 && href.indexOf("filter=4") >= 0) {
                $("#today4").addClass("active").children().addClass("active");
            }
        });
    </script>

</asp:Content>
