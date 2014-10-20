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
				
				<ul class="tree_p">
					<li onclick='show("tip1")'><img src="img/con1.gif"><a href="Index.aspx">首页</a>
				    <li onclick='show("tip2")'><img src="img/con1.gif"><a href="MonthReport.aspx">修理实时结算系统
					  <ul id="tip2"  style="display: none;" class="tree_c">
					  	<li><img src="img/con1.gif"><asp:LinkButton runat="server" onclick="ClickForSumEmp">公司汇总</asp:LinkButton></li>
					  	<li><img src="img/con1.gif"><asp:LinkButton runat="server" onclick="ClickForSumPla">工段汇总</asp:LinkButton></li>
					  	<li runat="server" onclick=""><img src="img/con1.gif"><a href="repairinfo.aspx">车辆查询</a></li>
					  </ul>
                      </li>
				    <li onclick='show("tip3")'><img src="img/con1.gif"><a href="parkinfo.aspx">停车场系统</a>
					  <ul id="tip3"  style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip4")'><img src="img/con1.gif"><a href="hdcbike.aspx?gettype=1">自行车租赁系统</a>
					  <ul id="tip4" style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip6")'><img src="img/con1.gif"><a href="ReportForms.aspx">人力资源管理系统</a>
					  <ul id="tip6"  style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				</ul>			
			</div><!-- end of left_nav-->

			
			<div id="r_body">

			   <asp:DataGrid 
                allowpaging="False"
                class="dataTable" 
                id="DataGrid1"
                pagesize="30"
                runat="server" Width="500px" Height="16px" >
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
            </div>

 
	
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
                       </td>
                   </tr>
              </table>
    </div>
</div>
</asp:Content>
