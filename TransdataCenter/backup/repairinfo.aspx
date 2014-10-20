<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="repairinfo.aspx.cs" Inherits="TransdataCenter.repairinfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<asp:ScriptManager ID="ScriptManager1" runat="server">  
  </asp:ScriptManager> 
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">  
  <ContentTemplate>

  <div class="switch">
     <asp:Label ID="Label1" runat="server" Text="车号" class="drop"></asp:Label>
     <asp:TextBox ID="Tebbusid" runat="server" class="drop" Width="70px"></asp:TextBox>
     <asp:Button ID="BtnSearch" runat="server" Text="查询" Height="22px" Width="50px" 
            onclick="BtnSearch_Click"></asp:Button>
    <asp:label ID="label2" runat="server" Text="筛选类别" class="drop"></asp:label>
    <asp:DropDownList runat="server" ID="DDLType" class="drop" Width="140px" 
            AutoPostBack="True" 
          onselectedindexchanged="DDLType_SelectedIndexChanged">   
    <asp:ListItem>全部类别</asp:ListItem>
    </asp:DropDownList>
    <asp:label ID="label3" runat="server" Text="筛选年月" class="drop"></asp:label>
    <asp:DropDownList runat="server" ID="DDLMonth" class="drop" Width="130px" 
            AutoPostBack="True" 
          onselectedindexchanged="DDLMonth_SelectedIndexChanged">
        <asp:ListItem>全部年月</asp:ListItem>
    </asp:DropDownList>
</div>
    <div style = "clear:both">
        
                   <div class="repairTable">


            <div id="main_body">
			<div id="l_body">
				<%--<form id="search" action="#" name="search">
					<input class="s_form" type="text" name="" />
					<input class="s_botton" type="submit" value=" "/>/////////问题所在
				</form>--%>
				<div class="clearfix"> </div>
				
				<ul class="tree_p">
					<li onclick='show("tip1")'><img src="img/con1.gif"><a href="index.aspx">首页</a>
					  <ul id="tip1" style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip2")'><img src="img/con1.gif"><a href="Repairsum.aspx">修理实时结算系统</a>
					  <ul id="tip2"  style="display: none;" class="tree_c">
					  	
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip3")'><img src="img/con1.gif"><a href="parkInfo.aspx">停车场系统</a>
					  <ul id="tip3"  style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip4")'><img src="img/con1.gif"><a href="#">自行车租赁系统</a>
					  <ul id="tip4" style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip5")'><img src="img/con1.gif"><a href="#">计划统计系统</a>
					  <ul id="tip5"  style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip6")'><img src="img/con1.gif"><a href="Report_forms.aspx">人力资源管理系统</a>
					  <ul id="tip6"  style="display: none;" class="tree_c">
					  	
					  </ul>
				    </li>
				</ul>			
			</div><!-- end of left_nav-->

			
			<div id="r_body">

			   <asp:DataGrid 
                class="dataTable" 
                allowpaging="False"
                id="DGrepairinfo"
                pagesize="30"
                runat="server" Width="600px" Height="16px" >
                
			
                <HeaderStyle CssClass="dg_header" />
                <AlternatingItemStyle CssClass="dg_alter" />
                <PagerStyle CssClass="dg_page" />
                <ItemStyle CssClass="dg_item"/>
            </asp:DataGrid>

	
            </div>
</div>
    
</ContentTemplate>
</asp:UpdatePanel> 
    


 </asp:Content>