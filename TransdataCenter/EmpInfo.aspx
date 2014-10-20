<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpInfo.aspx.cs" Inherits="TransdataCenter.EmpInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div style = "margin: auto auto;width:999px;">--%>
    <div style = "float:right;margin-right:150px;margin-top:-37px;">
        <asp:DropDownList ID="DDLSearch" runat="server" AutoPostBack="True" Font-Size="Medium"
            onselectedindexchanged="DDLSearch_SelectedIndexChanged">
            <asp:ListItem>姓名</asp:ListItem>
            <asp:ListItem>工号</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TextBox1" runat="server" Width="91px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="查询" onclick="Button1_Click" />

        &nbsp;

        筛选：
        <asp:DropDownList ID="DDLFilter" runat="server" AutoPostBack="True" Font-Size="Medium"
            onselectedindexchanged="DDLFilter_SelectedIndexChanged">
            <asp:ListItem>所有员工</asp:ListItem>
            <asp:ListItem>有合同的员工</asp:ListItem>
        </asp:DropDownList>
        
    </div>
    <%--<div style = "clear:both">--%>
        <div id="main_body">
			<div id="l_body">
				<%--<form id="search" action="#" name="search">
					<input class="s_form" type="text" name="" />
					<input class="s_botton" type="submit" value=" "/>/////////问题所在
				</form>--%>
				<span class="ceTitle">
                        <span class="ceTitle-l"></span>
                        <span class="ceTitle-m"></span>
                        <span class="ceTitle-r"></span>
                    </span>
                    <ul class="tree_p" id="menu_zzjsnet">
                        <li>

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
                            <ul id="tip1" style="display: none;" class="tree_c">
                            <li><a href= "RepairSum.aspx">修理统计</a></li>
                                <li><a href="MonthReport.aspx?type=xx&date=day&unit=SQ">昨日修理信息</a></li>
                                <li><a href="MonthReport.aspx?type=xx&date=month&unit=SQ">上月修理信息</a></li>
                                <li><a href="repairinfo.aspx">车辆修理查询</a></li>
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

			   <asp:DataGrid 
                class="dataTable" 
                allowpaging="False"
                id="DGEmpInfo"
                pagesize="20"
                runat="server" Width="800px"  >
                
			
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>


            <%--<table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="800" align="center"
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
                       </td>
                   </tr>
              </table>--%>
    </div>
</div>
    <script type="text/javascript">
        $(document).ready(function () {
            var href = window.location.href;
            if (href.indexOf("ReportForms.aspx?type=com") >= 0) {
                $("#form1").addClass("active").children().addClass("active");
            } else if (href.indexOf("ReportForms.aspx?type=work") >= 0) {
                $("#form2").addClass("active").children().addClass("active");
            } else if (href.indexOf("WorkFlowDetail.aspx") >= 0) {
                $("#form4").addClass("active").children().addClass("active");
            } else if (href.indexOf("EmpInfo.aspx") >= 0) {
                $("#form5").addClass("active").children().addClass("active");
            } else if (href.indexOf("ContractMore.aspx") >= 0) {
                $("#form3").addClass("active").children().addClass("active");
            }
        });
    </script>
</asp:Content>
