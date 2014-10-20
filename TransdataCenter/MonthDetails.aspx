<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthDetails.aspx.cs" Inherits="TransdataCenter.js.MonthDetails" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">

    <center dir="ltr">
  
        <asp:ScriptManager ID="ScriptManager1" runat="server">  
        </asp:ScriptManager>  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
        <ContentTemplate>
        <div class="switch">
        <asp:DropDownList runat="server" ID="DDLSearchType" class="drop" Width="80px" 
            AutoPostBack="True"  onselectedindexchanged="DDLSearchType_SelectedIndexChanged">
        <asp:ListItem>订单号</asp:ListItem>
        <asp:ListItem>车号</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TxtRequest" runat="server" class="drop" Width="70px"></asp:TextBox>
        <asp:Button ID="BtnSearch" runat="server" Text="查询" Height="22px" Width="70px" onclick="BtnSearch_Click"></asp:Button>
        </div>
        <span style="margin-left:17%"><asp:Label ID="lbltype" runat="server" Font-Bold="False" Text="修理类型:" Width="64px"></asp:Label></span>
       <asp:DropDownList ID="DDLdatetype" runat="server" Font-Size="Medium" 
            Height="22px"  
            Width="100px"  
            AutoPostBack="True" onselectedindexchanged="DDLdatetype_SelectedIndexChanged">
            <asp:ListItem>昨日</asp:ListItem>
            <asp:ListItem>上月</asp:ListItem>
       </asp:DropDownList>
       <asp:DropDownList ID="DDLrepairstyle" runat="server" Font-Size="Medium" 
            Height="22px"  
            Width="100px"  
            AutoPostBack="True" onselectedindexchanged="DDLrepairstyle_SelectedIndexChanged">
            <asp:ListItem>小修</asp:ListItem>
            <asp:ListItem>保养</asp:ListItem>
       </asp:DropDownList>
       <%--<asp:Label ID="lblworker" runat="server" Text="工段:"></asp:Label>--%>
       <asp:Label ID="lblcom" runat="server" Text="修理公司:"></asp:Label>
        <asp:DropDownList ID="DDLcom" runat="server" Font-Size="Medium" Height="22px" 
            Width="150px" AutoPostBack="True" 
            onselectedindexchanged="DDLcom_SelectedIndexChanged">
        </asp:DropDownList>
            <asp:DropDownList ID="DDLcomID" runat="server" Visible="False">
            </asp:DropDownList>
     <asp:Label ID="Label1" runat="server" Text="降序排序:"></asp:Label>
     <asp:DropDownList runat="server" ID="MonthreportAray" class="drop" Width="110px" Font-Size="Medium" Height="22px"
            AutoPostBack="True" 
          onselectedindexchanged="MonthreportAray_SelectedIndexChanged">
         <asp:ListItem>车号</asp:ListItem>
        <asp:ListItem>车队</asp:ListItem>
        <asp:ListItem>总计划费用</asp:ListItem>
        <asp:ListItem>总实际费用</asp:ListItem>
    </asp:DropDownList>
        <asp:Button ID="Butsearch" runat="server" Text="查询" AccessKey="1" Height="25px" 
            Width="68px" onclick="BtnSearch_Click" Visible="False" />

        <%--<asp:Button ID="Butreset" runat="server" Height="25px" onclick="Butreset_Click" 
            Text="重置" Width="58px" />--%>	
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

			   <asp:DataGrid 
                class="dataTable" 
                allowpaging="False"
                id="DGdetails"
                runat="server" Width="800px" Height="16px" >
                
			
                
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>
            
			<table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="450" align="center"
                   border="1">
                   <tr>
                       <td>
                            <asp:linkbutton id="LBtnFirst" runat="server" onclick="FirstPage" CommandName="First">首页</asp:linkbutton> 
                            <asp:linkbutton id="LBtnPrev" runat="server" onclick="PrePage" CommandName="Prev">上一页</asp:linkbutton>  
                            <asp:linkbutton id="LBtnNext" runat="server" onclick="NextPage" CommandName="Next">下一页</asp:linkbutton>
                            <asp:linkbutton id="LBtnLast" runat="server" onclick="LastPage" CommandName="Last">尾页</asp:linkbutton>

                       </td>
                       <td>第
                            <asp:literal id="LtlPageIndex" runat="server"></asp:literal>页__共 
                            <asp:literal id="LtlPageCount" runat="server"></asp:literal>页__每页
                            <asp:literal id="LtlPageSize" runat="server"></asp:literal>条__共
                            <asp:literal id="LtlRecordCount" runat="server"></asp:literal>条
                       </td>
                   </tr>
              </table>
    </div><!-- end of right body-->
        </TR>
            <%--</TABLE>
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
              </TABLE>--%>

        </ContentTemplate>
        <Triggers>  
        <asp:AsyncPostBackTrigger ControlID="DDLrepairstyle" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger> 
        <%--<asp:AsyncPostBackTrigger ControlID="DDLSearchType" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>--%> 
        <asp:AsyncPostBackTrigger ControlID="DDLdatetype" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="DDLcom" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="Butsearch" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>  
        </asp:UpdatePanel> 
        <br />           
  









        <br />
        <br />









        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;
        <br />









    </center>
</asp:Content>

