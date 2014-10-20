<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthReport.aspx.cs" Inherits="TransdataCenter.js.MonthReport" %>

<asp:Content ID="Content1" runat="server"
    ContentPlaceHolderID="ContentPlaceHolder1">

    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
           <div class="switch" style="width:700px;margin-top:-35px;margin-right:100px">
         
                <span style="margin-left: 17%">
                    <asp:Label ID="lbltime" runat="server" Font-Bold="False" Text="修理类型:" Width="64px"></asp:Label></span>
                <asp:DropDownList ID="DDLrepairstyle" runat="server" Font-Size="Medium"
                    Height="22px"
                    Width="100px"
                    AutoPostBack="True" OnSelectedIndexChanged="DDLrepairstyle_SelectedIndexChanged">
                    <asp:ListItem>小修</asp:ListItem>
                    <asp:ListItem>保养</asp:ListItem>
                </asp:DropDownList>
               <span >
                    <asp:Label ID="lbltype" runat="server" Font-Bold="False" Text="修理时间:" Width="64px"></asp:Label></span>
                <asp:DropDownList ID="DDLdatetype" runat="server" Font-Size="Medium"
                    Height="22px"
                    Width="100px"
                    AutoPostBack="True" OnSelectedIndexChanged="DDLdatetype_SelectedIndexChanged">
                    <asp:ListItem>昨日</asp:ListItem>
                    <asp:ListItem>上月</asp:ListItem>
                </asp:DropDownList>
                <%--<asp:Label ID="lblworker" runat="server" Text="工段:"></asp:Label>--%>
                <asp:Label ID="lblcom" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="DDLcom" runat="server" Font-Size="Medium" Height="22px"
                    Width="150px" AutoPostBack="True"
                    OnSelectedIndexChanged="DDLcom_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DDLcomID" runat="server" Visible="False">
                </asp:DropDownList>
                <asp:Button ID="Btnsearch" runat="server" Text="查询" AccessKey="1" Height="25px"
                    Width="68px" OnClick="BtnSearch_Click" Visible="False" />
                </div>
            <%--</div>--%>
           <div>
            <div  class="container">
                <div id="main_body">
                    <div id="l_body">

                        <span class="ceTitle">
                            <span class="ceTitle-l"></span>
                            <span class="ceTitle-m"></span>
                            <span class="ceTitle-r"></span>
                        </span>
                        <ul class="tree_p" id="menu_zzjsnet">
                            <li onclick='show("tip5")'>

                                <span class="menu-title home">
                                    <span class="img_left">
                                        <img src="images/home.png" /></span>
                                    <span class="img_middle"><a href="Index.aspx">首页</a></span>
                                    <span class="img_right">
                                        <img src="images/right.png" /></span>
                                </span>
                            </li>
                            <li onclick='show("tip1")'>

                                <span class="menu-title">
                                    <span class="img_left">
                                        <img src="images/fix.png"></img></span>
                                    <span class="img_middle"><a>修理信息系统</a></span>
                                    <span class="img_right">
                                        <!--MonthReport.aspx-->
                                        <img src="images/right.png" /></span>
                                </span>
                                <ul id="tip1" class="tree_c">
                                    <li id="repair"><a href= "RepairSum.aspx">修理统计</a></li>
                                    <li id="day"><a href="MonthReport.aspx?type=xx&date=day&unit=SQ">昨日修理信息</a></li>
                                    <li id="month"><a href="MonthReport.aspx?type=xx&date=month&unit=SQ">上月修理信息</a></li>
                                    <li id="repairinfo"><a href="repairinfo.aspx">车辆修理查询</a></li>
                                </ul>
                            </li>
                            <li onclick='show("tip2")'>

                                <span class="menu-title">
                                    <span class="img_left">
                                        <img src="images/park.png"></span>
                                    <span class="img_middle"><a>停车场系统</a></span>
                                    <span class="img_right">
                                        <img src="images/right.png" /></span><!--parkinfo.aspx-->
                                </span>
                                <ul id="tip2" style="display: none;" class="tree_c">
                                    <li><a href="parkInfo.aspx?date=today&filter=1">未出场表单</a></li>
                                    <li><a href="parkInfo.aspx?date=today&filter=2">出场表单</a></li>
                                    <li><a href="parkInfo.aspx?date=today&filter=3">准时出场表单</a></li>
                                    <li><a href="parkInfo.aspx?date=today&filter=4">延时出场表单</a></li>
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
                            <li onclick='show("tip4")'>

                                <span class="menu-title">
                                    <span class="img_left">
                                        <img src="images/resource.png"></span>
                                    <span class="img_middle"><a href="ReportForms.aspx">人力资源管理系统</a></span>
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
                                &nbsp;</li>
                        </ul>
                    </div>
                    <!-- end of left_nav-->

                    <div id="r_body" style="margin-top: auto">

                        <asp:DataGrid
                            class="dataTable"
                            AllowPaging="False"
                            ID="DGSum"
                            runat="server" Width="800px" Height="16px">



                            <HeaderStyle CssClass="dg_header" />
                            <AlternatingItemStyle CssClass="dg_alter" />
                            <PagerStyle CssClass="dg_page" />
                            <ItemStyle CssClass="dg_item" />
                        </asp:DataGrid>

                        <%--<table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="800" align="center"
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
                        </table>--%>
                    </div><!-- end of right body-->
                    </div><!-- end of main body-->
                </div><!-- end of container-->
                </div>
                <%--</TR>--%>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DDLrepairstyle" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <%--<asp:AsyncPostBackTrigger ControlID="DDLSearchType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>--%>
                <asp:AsyncPostBackTrigger ControlID="DDLdatetype" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="DDLcom" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="Btnsearch" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
   <script type="text/javascript">
       $(document).ready(function () {
           var href = window.location.href;
           if (href.indexOf("date=day") >= 0) {
               $("#day").addClass("active").children().addClass("active");
           } else if (href.indexOf("date=month") >= 0) {
               $("#month").addClass("active").children().addClass("active");
           } else if (href.indexOf("RepairInfo.aspx") >= 0) {
               $("#repairinfo").addClass("active").children().addClass("active");
           } else if (href.indexOf("RepairSum.aspx") >= 0) {
               $("#repair").addClass("active").children().addClass("active");
           }
       });
    </script>
</asp:Content>
 
