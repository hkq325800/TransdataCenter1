<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PIMS.aspx.cs" Inherits="TransdataCenter.PIMS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div id = "main_body">
			<div id="l_body">
                
                	<input class="s_form" type="text" name="" />
					<input class="s_botton" type="submit" value=" "/>
				
				<div class="clearfix"> </div>
				<ul class="tree_p">
					<li onclick='show("tip1")'><img src="img/con1.gif"><a href="#">主菜单1</a>
					  <ul id="tip1" style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	<li><a href="#" >子菜单2</a></li>
					  </ul>
				    </li>
				    <li onclick='show("tip2")'><img src="img/con1.gif"><a href="#">主菜单2</a>
					  <ul id="tip2"  style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	
					  </ul>
				    </li>
				    <li onclick='show("tip3")'><img src="img/con1.gif"><a href="#">主菜单3</a>
					  <ul id="tip3"  style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	<li><a href="#" >子菜单2</a></li>
					  </ul>
				    </li>
				    <li onclick='show("tip4")'><img src="img/con1.gif"><a href="#">主菜单4</a>
					  <ul id="tip4" style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	<li><a href="#" >子菜单2</a></li>
					  </ul>
				    </li>
				    <li onclick='show("tip5")'><img src="img/con1.gif"><a href="#">主菜单5</a>
					  <ul id="tip5"  style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	<li><a href="#" >子菜单2</a></li>
					  </ul>
				    </li>
				    <li onclick='show("tip6")'><img src="img/con1.gif"><a href="#">主菜单6</a>
					  <ul id="tip6"  style="display: none;" class="tree_c">
					  	<li><a href="#" >子菜单1</a></li>
					  	<li><a href="#" >子菜单2</a></li>
					  </ul>
				    </li>
				</ul>			
			</div><!-- end of left_nav-->

			<div id="r_body">
            <TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="450" align="center"
                   border="1">
                   <TR>

            <asp:DataGrid 
                class="dataTable" 
                allowpaging="true"
                id="DataHelp"
                pagesize="5"
                runat="server">

                <HeaderStyle Font-Size="9pt"></HeaderStyle>
                <FooterStyle Font-Size="9pt"></FooterStyle>
                <PagerStyle Visible="False" Font-Size="9pt" Mode="NumericPages"></PagerStyle>
 

            </asp:DataGrid>
            </TR>
            </TABLE>
            <TABLE id="Table2" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="450" align="center"
                   border="1">
                   <TR>
                       <TD style="WIDTH: 207px">
                            <asp:linkbutton id="LBtnFirst" runat="server" CommandName="First">首页</asp:linkbutton> 
                            <asp:linkbutton id="LBtnPrev" runat="server" CommandName="Prev">上一页</asp:linkbutton>  
                            <asp:linkbutton id="LBtnNext" runat="server" CommandName="Next">下一页</asp:linkbutton>
                            <asp:linkbutton id="LBtnLast" runat="server" CommandName="Last">尾页</asp:linkbutton> </TD>
                       <TD>第
                            <asp:literal id="LtlPageIndex" runat="server"></asp:literal>页 共 
                            <asp:literal id="LtlPageCount" runat="server"></asp:literal>页 每页
                            <asp:literal id="LtlPageSize" runat="server"></asp:literal>条 共
                            <asp:literal id="LtlRecordCount" runat="server"></asp:literal>条
                       </TD>
                   </TR>
              </TABLE>

			   <%--<div class="clearfix"> </div>
				<div class="page">
					<a href=""><p>上一页</p></a>
					<a href=""><p>下一页</p></a>
				</div>--%>
			</div><!-- end of right body-->
       </div>
</div>
</asp:Content>