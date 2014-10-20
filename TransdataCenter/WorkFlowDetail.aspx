<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkFlowDetail.aspx.cs" Inherits="TransdataCenter.WorkFlowDetail" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
    <div class="switch" style="float: right; margin-right :20px;margin-top: -37px">
        <span style="margin-left: 17%" runat="server" id="select">选择日期：
                    <asp:DropDownList ID="DDLyear" runat="server" Font-Size="Medium"
                    Height="22px"
                    Width="80px"
                    AutoPostBack="True" onselectedindexchanged="DDLyear_SelectedIndexChanged"></asp:DropDownList>年
                    <asp:DropDownList id="DDLmonth" runat ="server" font-size="Medium" onselectedindexchanged="DDLmonth_SelectedIndexChanged">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>月
                    <asp:DropDownList id="DDLday" runat ="server" font-size="Medium" onselectedindexchanged="DDLday_SelectedIndexChanged">
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                    </asp:DropDownList>日<asp:Button ID="Btnsearch" runat="server" Text="查询" AccessKey="1" Height="25px"
                    Width="68px"  onclick="QueryByDate"/></span>
                </div>
    <div>
    <div  class="container">
            <div id="main_body">
                <div id="l_body">
                    <span class="ceTitle">
                        <span class="ceTitle-l"></span>
                        <span class="ceTitle-m"></span>
                        <span class="ceTitle-r"></span>
                    </span>
                    <ul class="tree_p">
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
                                <li><a href="RepairSum.aspx">修理统计</a></li>
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
                </div>
                <!-- end of left_nav-->


                
                
                <%--<div style="clear: both"></div>--%>
                <div id="r_body" >
                    <asp:label ID="lblTitle" runat ="server" Font-Size="Large" style="margin-left:330px;display:block">调动人员详情</asp:label>
                    
                    <asp:DataGrid
                        class="dataTable"
                        AllowPaging="False"
                        ID="DGworkflow"
                        PageSize="30"
                        runat="server" Width="800px" Height="16px">


                        <HeaderStyle CssClass="dg_header" />
                        <AlternatingItemStyle CssClass="dg_alter" />
                        <PagerStyle CssClass="dg_page" />
                        <ItemStyle CssClass="dg_item" />
                    </asp:DataGrid>
                    <table id="Table2" style="FONT-SIZE: 9pt" cellspacing="1" cellpadding="1" width="800" align="center"
                        border="1" runat="server">
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
                </div><!-- end of right body-->
              </div><!-- end of main body-->
            </div><!-- end of container-->
        </div>
             </ContentTemplate>
         <Triggers>
             <asp:AsyncPostBackTrigger ControlID="DDLyear" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
             <asp:AsyncPostBackTrigger ControlID="DDLmonth" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
             <asp:AsyncPostBackTrigger ControlID="DDLday" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
             <asp:AsyncPostBackTrigger ControlID="Btnsearch" EventName="Click"></asp:AsyncPostBackTrigger>
         </Triggers>
             </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            var href = window.location.href;
            //alert(href);
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
