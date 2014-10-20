<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepairInfo.aspx.cs" Inherits="TransdataCenter.RepairInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server">  
  </asp:ScriptManager> 
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
  <ContentTemplate>

  <div class="switch" style="width:780px;margin-right:10px;margin-top:-35px">
     <asp:Label ID="Label1" runat="server" Text="车号" class="drop"></asp:Label>
     <asp:TextBox ID="Tebbusid" runat="server" class="drop" Width="70px" Font-Size="Medium"></asp:TextBox>
     <asp:Button ID="BtnSearch" runat="server" Text="查询" Height="22px" Width="50px" 
            onclick="BtnSearch_Click"></asp:Button>
    <asp:label ID="label2" runat="server" Text="筛选类别" class="drop" ></asp:label>
    <asp:DropDownList runat="server" ID="DDLType" class="drop" Width="140px" Font-Size="Medium"
            AutoPostBack="True" 
          onselectedindexchanged="DDLType_SelectedIndexChanged">   
    <asp:ListItem>全部类别</asp:ListItem>
    </asp:DropDownList>
    <asp:label ID="label3" runat="server" Text="筛选年月" class="drop"></asp:label>
    <asp:DropDownList runat="server" ID="DDLMonth" class="drop" Width="130px" Font-Size="Medium"
            AutoPostBack="True" 
          onselectedindexchanged="DDLMonth_SelectedIndexChanged">
        <asp:ListItem>全部年月</asp:ListItem>
    </asp:DropDownList>
      <asp:label ID="label4" runat="server" Text="降序排序" class="drop"></asp:label>
    <asp:DropDownList runat="server" ID="RepairInfoAray" class="drop" Width="85px" 
            AutoPostBack="True" Font-Size="Medium"
          onselectedindexchanged="DDLMonth_SelectedIndexChanged">
        <asp:ListItem>维修次数</asp:ListItem>
        <asp:ListItem>计划费用</asp:ListItem>
        <asp:ListItem>实际费用</asp:ListItem>
        <asp:ListItem>行驶公里</asp:ListItem>
    </asp:DropDownList>
</div>
<%--    <div style = "clear:both">
        
                   <div class="repairTable">--%>
      <div>
      <div class="container">
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
                        <li onclick='show("tip5")'>

                            <span class="menu-title home">
                                <span class="img_left"><img src="images/home.png" /></span>
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

			   <asp:DataGrid 
                class="dataTable" 
                allowpaging="False"
                id="DGrepairinfo"
                runat="server" Width="800" Height="16px" >
                
			
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>

	<table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="800" align="center" border="1" >
                   <tr>
                       <td style="WIDTH: 207px">
                            <asp:linkbutton id="LBtnFirst" runat="server" onclick="FirstPage" CommandName="First">首页</asp:linkbutton> 
                            <asp:linkbutton id="LBtnPrev" runat="server" onclick="PrePage" CommandName="Prev">上一页</asp:linkbutton>  
                            <asp:linkbutton id="LBtnNext" runat="server" onclick="NextPage" CommandName="Next">下一页</asp:linkbutton>
                            <asp:linkbutton id="LBtnLast" runat="server" onclick="LastPage" CommandName="Last">尾页</asp:linkbutton> </TD>
                       <td>第
                            <asp:literal id="LtlPageIndex" runat="server"></asp:literal>页__共 
                            <asp:literal id="LtlPageCount" runat="server"></asp:literal>页__每页
                            <asp:literal id="LtlPageSize" runat="server"></asp:literal>条__共
                            <asp:literal id="LtlRecordCount" runat="server"></asp:literal>条
                       </td>
                   </tr>
              </table>
            </div><!-- end of right body-->
          </div>
        </div>
    
</ContentTemplate>
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
