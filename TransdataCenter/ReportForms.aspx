<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportForms.aspx.cs" Inherits="TransdataCenter.Report_forms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <%--<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">--%>
    <%--<link rel="stylesheet" type="text/css" href="css/templatestyle.css">--%>
    <%--<script type="text/javascript" src="js/home.js"></script>
	<script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>--%>
    <%--<div class="container">--%>
    <body>
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
                    <li onclick ='show("tip4")'>

                        <span class="menu-title">
                            <span class="img_left">
                                <img src="images/resource.png"></span>
                            <span class="img_middle"><a>人力资源管理系统</a></span>
                            <span class="img_right">
                                <img src="images/right.png" /></span><!--ReportForms.aspx-->
                        </span>
                        <ul id="tip4" class="tree_c">
                            <li id="form1"><a href="ReportForms.aspx?type=com">人员情况（按公司）</a></li>
                            <li id="form2"><a href="ReportForms.aspx?type=work">人员情况（按工种）</a></li>
                            <li id="form3"><a href="ContractMore.aspx">合同到期人员</a></li>
                            <li id="form4"><a href="WorkFlowDetail.aspx?date=today&id=">人员变动情况</a></li>
                            <li id="form5"><a href="EmpInfo.aspx">员工信息查询</a></li>
                        </ul>
                        &nbsp;</li>
                </ul>
                </div><!-- end of left_nav-->
                <div id="r_body">
                    <asp:label runat ="server" ID="lblTitle" Text="" Font-Size="Large" style="margin-left:330px;display:block" ></asp:label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:DataGrid
                                class="dataTable"
                                ID="DataGrid2"
                                PageSize="30"
                                runat="server" Width="800px">


                                <HeaderStyle CssClass="dg_header" />
                                <AlternatingItemStyle CssClass="dg_alter" />
                                <PagerStyle CssClass="dg_page" />
                                <ItemStyle CssClass="dg_item" BorderStyle="None" />
                            </asp:DataGrid>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="LBcompany" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="LBtype" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>

                    
                    <div class="clearfix"></div>
                </div>
                <!-- end of right body-->
        </div>
    </body>
    <script type="text/javascript">
        $(document).ready(function () {
            var href = window.location.href;
            if (href.indexOf("ReportForms.aspx?type=com") >= 0) {
                $("#form1").addClass("active").children().addClass("active");
            } else if (href.indexOf("ReportForms.aspx?type=work") >= 0) {
                $("#form2").addClass("active").children().addClass("active");
            } else if (href.indexOf("EmpInfo.aspx") >= 0) {
                $("#form5").addClass("active").children().addClass("active");
            } else if (href.indexOf("WorkFlowDetail.aspx") >= 0) {
                $("#form4").addClass("active").children().addClass("active");
            } else if (href.indexOf("ContractMore.aspx") >= 0) {
                $("#form3").addClass("active").children().addClass("active");
            }
        });
    </script>
</asp:Content>
