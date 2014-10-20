<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepairSum.aspx.cs" Inherits="TransdataCenter.RepairSum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<asp:ScriptManager ID="ScriptManager1" runat="server">  
  </asp:ScriptManager> 
   
  

    <%--<div style = "margin: auto auto;width:999px;">--%>
    <div style = "clear:both">
        <div id="main_body">
			<div id="l_body">
				<%--<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
	<link rel="stylesheet" type="text/css" href="css/templatestyle.css">
	<script type="text/javascript" src="js/home.js"></script>
	<script type="text/javascript" src="js/jquery-1.10.2.min.js"></script>--%>
				
				<span class="ceTitle">
                    <span class="ceTitle-l"></span>
                    <span class="ceTitle-m"></span>
                    <span class="ceTitle-r"></span>
                </span>
                <ul class="tree_p" id="menu_zzjsnet">
                    <li onclick='show("tip5")'>

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
                            <span class="img_left">
                                <img src="images/fix.png"></img></span>
                            <span class="img_middle"><a>修理信息系统</a></span>
                            <span class="img_right"><!--MonthReport.aspx-->
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
                        <ul id="tip4" style="display: none;" class="tree_c">
                            <li><a href="ReportForms.aspx?type=com">人员情况（按公司）</a></li>
                            <li><a href="ReportForms.aspx?type=work">人员情况（按工种）</a></li>
                            <li><a href="ContractMore.aspx">合同到期人员</a></li>
                            <li><a href="WorkFlowDetail.aspx?date=today&id=">人员变动情况</a></li>
                            <li><a href="EmpInfo.aspx">员工信息查询</a></li>
                        </ul>
                        &nbsp;</li>
                </ul>			
			</div><!-- end of left_nav-->

			
			<div id="r_body">
                <asp:label runat ="server" Font-Size="Large" style="margin-left:330px;display:block">昨日修理厂数据</asp:label>
			   <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DGForRepairDay"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
                <asp:label runat ="server" Font-Size="Large"  style="margin-left:330px;display:block">上月修理厂数据</asp:label>
                <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DGForRepairMonth"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
                <asp:label runat ="server" Font-Size="Large"  style="margin-left:330px;display:block">昨日分公司数据</asp:label>
                <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DGForComDay"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
                <asp:label runat ="server" Font-Size="Large"  style="margin-left:330px;display:block">上月分公司数据</asp:label>
                <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DGForComMonth"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
                <%--<asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DGFor"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
                <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DataGrid2"
                pagesize="30"
                runat="server" Width="800px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>--%>
            </div>

 
	
            <%--<table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="450" align="center"
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
            if (href.indexOf("date=day") >= 0) {
                $("#day").addClass("active").children().addClass("active");
            } else if (href.indexOf("date=month") >= 0) {
                $("#month").addClass("active").children().addClass("active");
            } else if (href.indexOf("RepairInfo.aspx") >= 0) {
                $("#repairinfo").addClass("active").children().addClass("active");
            } else if (href.indexOf("RepairSum.aspx") >= 0){
                $("#repair").addClass("active").children().addClass("active");
            }
        });
    </script>
</asp:Content>
