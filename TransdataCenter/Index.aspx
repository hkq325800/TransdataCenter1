<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TransdataCenter.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class = "unit">
    <div id = "bikeinfo" class = "info">          
        <div class = "titleBar">
        <span class = "leftBlock"> 
        </span>         
        <a href = "hdcbike.aspx?gettype=1" id = "bikeTitle" class = "infoTitle">
            <span class = "infoTitle-l">
            </span>
            <span class = "infoTitle-m">
            自行车租赁信息
            </span>
            <span class = "infoTitle-r">
            </span>
        </a> 
        <span class = "day" id = "actDay1">
            <span class = "day-l">
            </span>
            <span class = "day-m">
            今日
            </span>
            <span class = "day-r">
            </span>
        </span>  
        <span class = "day" id = "inactDay1" onclick="Yesterday_onclick();">
            <span class = "day-l">
            </span>
            <span class = "day-m">
            昨日
            </span>
            <span class = "day-r">
            </span>
        </span>     
        <span class = "rightBlock">                       
        </span>              
        </div>           
        <div id = "bikeBody" class = "infoBody">  
            <div class = "textField3">
                当前共有站点数：<a id="A1" runat = "server" href = "hdcbike.aspx?gettype=1"><span ID = "bikeStation_Today" runat = "server"></span></a>个<br/> 
                自行车数：<span ID = "bikeNumber_Today" runat = "server"  ></span>辆<br/> 
                昨日共租车：
                <a id="A2" runat = "server" href="hdcbike.aspx?gettype=2"><span id = "bikeRentYesterday_Today1" runat = "server"></span></a>&nbsp;辆次 <br/>
                本月共租车：<span ID = "bikeRentThisMonth" runat = "server"  ></span>&nbsp;辆次 <br/>  
                上月共租车：<span ID = "bikeRentLastmonth" runat = "server"  ></span>&nbsp;辆次 <br/>
            </div>
            <div class="textField4" style="display:none"> 昨日共租车：<a id="A3" runat = "server" href="hdcbike.aspx?gettype=2"><span id="bikeRentYesterday_Today2" runat = "server"></span></a>辆次 <br>
            昨日租车量最大的五个站点：<br/>
             <div>
            <asp:DataGrid id="DGRentTopStation"
                runat="server" Width="290px" Height="16px" style="text-align:center;font-size:12px">

            </asp:DataGrid>
            </div>
          </div>
        </div>         
    </div> 
    <div id = "parkInfo" class = "info">          
        <div class = "titleBar">
        <span class = "leftBlock"> 
        </span>    
        <a href = "ParkInfo.aspx" id = "parkTitle" class = "infoTitle">
            <span class = "infoTitle-l">
            </span>
            <span class = "infoTitle-m">
            停车场信息
            </span>
            <span class = "infoTitle-r">
            </span>
        </a>
        <span class = "day" id = "actDay">
            <span class = "day-l">
            </span>
            <span class = "day-m">
            今日
            </span>
            <span class = "day-r">
            </span>
        </span>  
        <span class = "day" id = "inactDay">
            <span class = "day-l">
            </span>
            <span class = "day-m">
            昨日
            </span>
            <span class = "day-r">
            </span>
        </span>          
        <span class = "rightBlock"></span>
        </div>        
        <div id = "parkBody" class = "infoBody"> 
        <div class = "textField3">
            场内车辆数：<a name="anchorToday" runat = "server" href = "ParkInfo.aspx?date=today&filter=1"><span ID = "carNumIn_Today" runat = "server" ></span></a> 辆 <br/>
            总出场车辆：<a name="anchorToday" runat = "server" href = "ParkInfo.aspx?date=today&filter=2"><span ID = "carNumOut_Today" runat = "server" ></span></a> 辆<br/> 
            延误出场车辆：<a name="anchorToday" runat = "server" href = "ParkInfo.aspx?date=today&filter=4"><span ID = "carLate_Today" runat = "server" ></span></a> 辆 <br/>
            准时出场率：<a name="anchorToday" runat = "server" href = "ParkInfo.aspx?date=today&filter=3"><span ID = "carInTime_Today" runat = "server" ></span></a> <br/>
            洗车次数：<span ID = "wash_Today" runat = "server"></span>次 <br/>
            收取钱袋：<span ID = "collectNum_Today" runat = "server"></span>袋 <br/>  
            加油次数：<span ID = "oilNum_Today" runat = "server"></span>次<br/>
            总营运车数：<span ID = "CountBusToday" runat = "server"></span>辆<br/>
            总营运线路数：<span ID = "CountLineToday" runat = "server"></span>路<br/>
        </div>  
        <div class = "textField4">
            场内车辆数：<a name="anchorYestday" runat = "server" href = "ParkInfo.aspx?date=yesterday&filter=1"><span ID = "carNumIn_Yesterday" runat = "server"></span></a> 辆 <br/>
            总出场车辆：<a name="anchorYestday" runat = "server" href = "ParkInfo.aspx?date=yesterday&filter=2"><span ID = "carNumOut_Yesterday" runat = "server"></span></a> 辆 <br/> 
            延误出场车辆：<a name="anchorYestday" runat = "server" href = "ParkInfo.aspx?date=yesterday&filter=4"><span ID = "carLate_Yesterday" runat = "server"></span></a> 辆 <br/>
            准时出场率：<a name="anchorYestday" runat = "server" href = "ParkInfo.aspx?date=yesterday&filter=3"><span ID = "carInTime_Yesterday" runat = "server"></span></a> <br/>
            洗车次数：<span ID = "wash_Yesterday" runat = "server"></span>次 <br/>
            收取钱袋：<span ID = "collectNum_Yesterday" runat = "server"></span>袋 <br/>  
            加油次数：<span ID = "oilNum_Yesterday" runat = "server"></span>次<br/>
            总营运车数：<span ID = "CountBusYester" runat = "server"></span>辆<br/>
            总营运线路数：<span ID = "CountLineYester" runat = "server"></span>路<br/>
        </div>
        <%--<div class = "query">
            <b>车辆行为分析：</b>[<input id="busID" type = "text" runat = "server" value="填写车号" onfocus="if (this.value==&#39;填写车号&#39;) this.value=&#39;;" onblur="if(this.value==&#39;&#39;)this.value=&#39;填写车号&#39;" />]
            <input id="Submit1" type = "submit" value="查询" runat = "server" onserverclick = "carQueryClick" style = "color: #990000;"/>
        </div>  --%>    
        </div>        
    </div> 
    <div id="buttons">
        <div class="btn">
          <input id="Image1" type="image" src="images/btn-1.png" onclick="window.open(&#39;http://www.hzbus.com.cn&#39;)">
        </div>
        <div class="btn">
          <input id="Image2" type="image" src="images/btn-2.png" onclick="window.open(&#39;http://www.hzsggzxc.com&#39;)"/>
        </div>
        <div class="btn">
          <input id="Image3" type="image" src="images/btn-3.png" onclick="window.open(&#39;http://www.hzcb.gov.cn&#39;)"/>
        </div>
      </div>
    </div>
    <div class = "unit">  
    <div id = "fix" class = "info2">
        <div class = "titleBar">                                     
        <span class = "leftBlock">                       
        </span>               
        <span class = "midBlock"><a href = "RepairSum.aspx" class="infoTitle" >
            <span class = "infoTitle-l">
            </span>
            <span class = "infoTitle-m">
            修理信息系统
            </span>
            <span class = "infoTitle-r">
            </span></a>
        <span class="day" id="spanSQ" > 
            <span class="day-l"></span> 
            <span class="day-m" runat = "server" onclick = "SQ_onclick();">石桥</span> 
            <span class="day-r"></span> 
        </span> 
            <span class="day" id="spanCX"> 
            <span class="day-l"></span> 
            <span class="day-m " runat = "server" onclick ="CX_onclick();">城西</span> 
            <span class="day-r"></span> 
        </span> 
        <span class="day" id="spanZT" > 
            <span class="day-l"></span> 
            <span class="day-m" runat = "server" onclick ="ZT_onclick();">转塘</span> 
            <span class="day-r"></span> 
        </span> 
        </span> 
        <span class="rightBlock"> </span> 
        </div>
        <div id = "fixBody" class = "infoBody">  
        <div class = "textField3" style="display:inline-block" id="SQ" >
            今日石桥正进行小修<a><span id="littFix_SQ_now" runat ="server"></span></a>辆,
            正进行保养<a><span id="keepFix_SQ_now" runat ="server"></span></a>辆,
            <br />
            昨日石桥共进行小修<a href = "MonthReport.aspx?type=xx&date=day&unit=SQ"><span id = "littFix_SQ_day" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_SQ_day_xx" style="color:#990000;">定额材料费<span id = "materCost_SQ_day_xx" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_SQ_day_xx" style="color:#990000;">实际材料费<span id = "materCostReal_SQ_day_xx" runat = "server" ></span>,</asp:Label><br/>
            昨日石桥共进行保养<a href = "MonthReport.aspx?type=by&date=day&unit=SQ"><span id = "keepFix_SQ_day" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_SQ_day_by" style="color:#990000;">定额材料费<span id = "materCost_SQ_day_by" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_SQ_day_by" style="color:#990000;">实际材料费<span id = "materCostReal_SQ_day_by" runat = "server" ></span> 元,</asp:Label><br/>
            上月石桥共完成小修<a href = "MonthReport.aspx?type=xx&date=month&unit=SQ"><span id = "littFix_SQ_month" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_SQ_month_xx" style="color:#990000;">定额材料费<span id = "materCost_SQ_month_xx" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_SQ_month_xx" style="color:#990000;">实际材料费<span id = "materCostReal_SQ_month_xx" runat = "server" ></span> ,</asp:Label><br/>
            上月石桥共完成保养<a href = "MonthReport.aspx?type=by&date=month&unit=SQ"><span id = "keepFix_SQ_month" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_SQ_month_by" style="color:#990000;">定额材料费<span id = "materCost_SQ_month_by" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_SQ_month_by" style="color:#990000;">实际材料费<span id = "materCostReal_SQ_month_by" runat = "server" ></span> 元,</asp:Label><br/>
            <%--昨日石桥修理公司共完成小修<span id = "littFix_SQ" runat = "server" ></span> 辆--%> <br />
        </div>
        <div class = "textField4" style="display:none" id="CX" >
            今日城西正进行小修<a><span id="littFix_CX_now" runat ="server"></span></a>辆,
            正进行保养<a><span id="keepFix_CX_now" runat ="server"></span></a>辆,
            <br />
            昨日城西共进行小修<a href = "MonthReport.aspx?type=xx&date=day&unit=CX"><span id = "littFix_CX_day" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_CX_day_xx" style="color:#990000;">定额材料费<span id = "materCost_CX_day_xx" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_CX_day_xx" style="color:#990000;">实际材料费<span id = "materCostReal_CX_day_xx" runat = "server" ></span>,</asp:Label><br/>
            昨日城西共进行保养<a href = "MonthReport.aspx?type=by&date=day&unit=CX"><span id = "keepFix_CX_day" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_CX_day_by" style="color:#990000;">定额材料费<span id = "materCost_CX_day_by" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_CX_day_by" style="color:#990000;">实际材料费<span id = "materCostReal_CX_day_by" runat = "server" ></span> 元,</asp:Label><br/>
            上月城西共完成小修<a href = "MonthReport.aspx?type=xx&date=month&unit=CX"><span id = "littFix_CX_month" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_CX_month_xx" style="color:#990000;">定额材料费<span id = "materCost_CX_month_xx" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_CX_month_xx" style="color:#990000;">实际材料费<span id = "materCostReal_CX_month_xx" runat = "server" ></span> ,</asp:Label><br/>
            上月城西共完成保养<a href = "MonthReport.aspx?type=by&date=month&unit=CX"><span id = "keepFix_CX_month" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_CX_month_by" style="color:#990000;">定额材料费<span id = "materCost_CX_month_by" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_CX_month_by" style="color:#990000;">实际材料费<span id = "materCostReal_CX_month_by" runat = "server" ></span> 元,</asp:Label><br/>
            <br />
        </div>
        <div class = "textField0" style="display:none" id="ZT" >
            今日转塘正进行小修<a><span id="littFix_ZT_now" runat ="server"></span></a>辆,
            正进行保养<a><span id="keepFix_ZT_now" runat ="server"></span></a>辆,
            <br />
            昨日转塘共进行小修<a href = "MonthReport.aspx?type=xx&date=day&unit=ZT"><span id = "littFix_ZT_day" runat = "server" ></span></a> 辆，
            <br /><asp:Label runat="server" ID="lblmaterCost_ZT_day_xx" style="color:#990000;">定额材料费<span id = "materCost_ZT_day_xx" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_ZT_day_xx" style="color:#990000;">实际材料费<span id = "materCostReal_ZT_day_xx" runat = "server" ></span>,</asp:Label><br/>
            昨日转塘共进行保养<a href = "MonthReport.aspx?type=by&date=day&unit=ZT"><span id = "keepFix_ZT_day" runat = "server" ></span></a> 辆，
            <br/><asp:Label runat="server" ID="lblmaterCost_ZT_day_by" style="color:#990000;">定额材料费<span id = "materCost_ZT_day_by" runat = "server" ></span> 元,</asp:Label>
            <asp:Label runat="server" ID="lblmaterCostReal_ZT_day_by" style="color:#990000;">实际材料费<span id = "materCostReal_ZT_day_by" runat = "server" ></span> 元,</asp:Label><br/>
            上月转塘共完成小修<a href = "MonthReport.aspx?type=xx&date=month&unit=ZT"><span id = "littFix_ZT_month" runat = "server" ></span></a> 辆，
            <br/><asp:Label runat="server" ID="lblmaterCost_ZT_month_xx" style="color:#990000;">定额材料费<span id = "materCost_ZT_month_xx" runat = "server" ></span> 元,</asp:Label><asp:Label runat="server" ID="lblmaterCostReal_ZT_month_xx" style="color:#990000;">实际材料费<span id = "materCostReal_ZT_month_xx" runat = "server" ></span> ,</asp:Label><br/>
            上月转塘共完成保养<a href = "MonthReport.aspx?type=by&date=month&unit=ZT"><span id = "keepFix_ZT_month" runat = "server" ></span></a> 辆，
            <br/><asp:Label runat="server" ID="lblmaterCost_ZT_month_by" style="color:#990000;">定额材料费<span id = "materCost_ZT_month_by" runat = "server" ></span> 元,</asp:Label><asp:Label runat="server" ID="lblmaterCostReal_ZT_month_by" style="color:#990000;">实际材料费<span id = "materCostReal_ZT_month_by" runat = "server" ></span> 元,</asp:Label><br/>
            <br />
        </div>
        <div>
            <div class = "query" style = "position:relative;top:10px;left: 15px;bottom:-15px;">
            <b>车辆查询：</b>[<input id="repairID" type = "text" runat = "server" value="填写车号" onfocus="if (this.value==&#39;填写车号&#39;) this.value=&#39;&#39;;"  onblur="if(this.value==&#39;&#39;)this.value=&#39;填写车号&#39;" />]
            <input id="Submit2" type = "submit" value="查询" runat = "server" onserverclick = "repairQueryClick" style = "color: #990000;"/>
            </div>
            <br><br>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div> 
        </div>
    </div>
    <div id = "emp" class = "info2">
        <div class = "titleBar">                                     
        <span class = "leftBlock">                       
        </span>  
        <span class = "midBlock"> 
            <a href = "ReportForms.aspx?type=com" class="infoTitle">
                 <span class = "infoTitle-l">
            </span>
            <span class = "infoTitle-m">
            人力资源管理系统
            </span>
            <span class = "infoTitle-r">
            </span>
            </a>                     
            <span class="day" id="spanWorkFlow"> 
            <span class="day-l"></span> 
                <span class="day-m" runat = "server" onclick ="empChange_onclick();">人员变动</span> 
                <span class="day-r"></span> 
            </span> 
            <span class="day" id="spanContractRunout"> 
                <span class="day-l"></span> 
                <span class="day-m" runat = "server" onclick ="contractRunout_onclick();">合同过期人员</span> 
                <span class="day-r"></span> 
            </span> 
            <span class="day" id="spanBirthRemind"> 
                <span class="day-l"></span> 
                <span class="day-m" runat = "server" onclick ="birthRemind_onclick();">生日提醒</span> 
                <span class="day-r"></span> 
            </span> 
        </span>                        
        <span class = "rightBlock">                       
        </span>                            
        </div>
        <div id = "empBody" class = "infoBody">
        <div class="query" style=" margin-top:0px">  
            <div id = "empImg" style="float:left; margin-left:0px; margin-top:-20px"><img alt = "" src="images/img-2.png" /></div>
        <div style="clear:both"></div>
            <div style="margin-top:30px">截至当前</div>
			共有员工：<a <%--href = "ReportForms.aspx"--%>><span id = "allEmpNum" runat = "server"></span></a>人<br/>
			有劳工合同员工：<a <%--href = "ReportForms.aspx"--%>><span id = "conEmpNum" runat = "server"></span></a>人<br/>
			<div style="margin-top:20px"><b>人员查询：</b>[<input id="empName" type = "text" runat = "server" value="填写人名" onfocus="if (this.value==&#39;填写人名&#39;) this.value=&#39;&#39;;"  onblur="if(this.value==&#39;&#39;)this.value=&#39;填写人名&#39;"  />]
            <input id="Submit3" type = "submit" value="查询" runat = "server" onserverclick = "empQueryClick" style = "color: #990000;"/>
                <br />
            <asp:Label ID="Label1" runat="server"> </asp:Label>
            </div>
            </div>
            <div class = "textField3" style="float:right; margin-top:-240px; margin-right:20px" id="empChange">
                <%--<asp:DataGrid id="DGWorkFlow" runat="server" Width="300px" Height="16px">
                </asp:DataGrid>--%>
                昨日调动：<a runat = "server" id = "empChangeYester" href=""><span  runat  = "server" id = "empChangeYesterNum"></span></a>&nbsp;人
                今日调动：<a runat = "server" id = "empChangeToday" href=""><span  runat  = "server" id = "empChangeTodayNum"></span></a>&nbsp;人
			<br/>最近调动人员列表
                <table style="width:250px;font-size:12px">
            <tr>
                <td style="width:50px">姓名</td>
                <td style="width:100px">调出部门</td>
                <td style="width:100px">调入部门</td>
            </tr>
          	<tr>
            	<td><a runat = "server" id = "empAnchor1" href = ""><span  runat  = "server" id = "empName1"></span></a></td>
                <td><span  runat  = "server" id = "empOut1"></span></td>
                <td><span  runat  = "server" id = "empIn1"></span></td>
            </tr>
            <tr>
            	<td><a runat = "server" id = "empAnchor2" href = ""><span  runat  = "server" id = "empName2"></span></a></td>
                <td><span  runat  = "server" id = "empOut2"></span></td>
                <td><span  runat  = "server" id = "empIn2"></span></td>
            </tr>
            <tr>
            	<td><a runat = "server" id = "empAnchor3" href = ""><span  runat  = "server" id = "empName3"></span></a></td>
                <td><span  runat = "server" id = "empOut3"></span></td>
                <td><span  runat  = "server" id = "empIn3"></span></td>
            </tr>
            <tr>
            	<td><a runat = "server" id = "empAnchor4" href = ""><span  runat  = "server" id = "empName4"></span></a></td>
                <td><span  runat = "server" id = "empOut4"></span></td>
                <td><span  runat  = "server" id = "empIn4"></span></td>
            </tr>
          </table>
                </div>
        <div class="textField4" style="display:none; float:right; margin-top:-240px; margin-right:20px" id="contractRunout">
            <b>实习合同</b><br/>
            合同即将到期时间：<a runat = "server" id = "InfomalFutureHref" href=""><span  runat  = "server" id = "InfomalFutureDate"></span></a>
            <br/>
            到期人员名单：<a runat = "server" id = "InformalFuture1"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "InformalFuture2"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "InformalFuture3"></a>
            <br />
            今天：<a runat = "server" id = "InfomalTodayHref" href=""><span  runat  = "server" id = "InfomalTodayDate"></span></a>
            <br/>
            到期人员名单：<a runat = "server" id = "InformalToday1"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "InformalToday2"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "InformalToday3"></a>
            <br />
            <b>正式合同</b><br/>
            合同即将到期时间：<a runat = "server" id = "FomalFutureHref" href=""><span  runat  = "server" id = "FomalFutureDate"></span></a>
            <br/>
            到期人员名单：<a runat = "server" id = "FormalFuture1"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "FormalFuture2"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "FormalFuture3"></a>
            <br />
            今天：<a runat = "server" id = "FomalTodayHref" href=""><span  runat  = "server" id = "FomalTodayDate"></span></a>
            <br/>
            到期人员名单：<a runat = "server" id = "FormalToday1"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "FormalToday2"></a>&nbsp;&nbsp;&nbsp;<a runat = "server" id = "FormalToday3"></a>
            <br />
        </div>
        <div class="textField0" style="display:none; float:right; margin-top:-240px; margin-left:240px; margin-right:10px;font-size:12px" id="birthRemind">
            <asp:Label ID="BirthToday" runat="server" style="font-size:12px">今日寿星</asp:Label>
            <div style ="overflow:auto;width:280px; height:230px;font-size:12px" >
            <asp:DataGrid id="birthday"
                runat="server" Width="263px" Height="16px" style="font-size:12px">

            </asp:DataGrid>
            </div>
        </div>
      </div>
    </div>
  </div>
    <script type="text/javascript">
        function SQ_onclick(){
            $("#SQ").show();
            $("#CX").hide();
            $("#ZT").hide();
        }
        function CX_onclick(){
            $("#SQ").hide();
            $("#CX").show();
            $("#ZT").hide();
        }
        function ZT_onclick(){
            $("#SQ").hide();
            $("#CX").hide();
            $("#ZT").show();
        }
        function empChange_onclick(){
            $("#empChange").show();
            $("#contractRunout").hide();
            $("#birthRemind").hide();
        }
        function contractRunout_onclick(){
            $("#empChange").hide();
            $("#contractRunout").show();
            $("#birthRemind").hide();
        }
        function birthRemind_onclick(){
            $("#empChange").hide();
            $("#contractRunout").hide();
            $("#birthRemind").show();
        }
    </script>
    
</asp:Content>

