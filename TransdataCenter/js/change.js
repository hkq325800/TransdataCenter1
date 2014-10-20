$(document).ready(function(){
  	$(".day").click(function(){
  		if($(this).attr("id") == "inactDay")
  		{
			var t_id = $(this).attr("id");
  			var bro = $(this).parent().find("#actDay");
  			bro.attr("id", t_id);
  			$(this).attr("id", "actDay");
  			////////////////////////
  			if($(this).index() == 2)
  			{
  				$(this).parent().parent().find(".textField3").show();
  				$(this).parent().parent().find(".textField4").hide();
				$(this).parent().parent().find(".textField0").hide();
  			}
  			if($(this).index() == 3)
  			{
  				$(this).parent().parent().find(".textField3").hide();
  				$(this).parent().parent().find(".textField4").show();
				$(this).parent().parent().find(".textField0").hide();
  			}
			if($(this).index() == 4)
  			{
  				$(this).parent().parent().find(".textField3").hide();
  				$(this).parent().parent().find(".textField4").hide();
				$(this).parent().parent().find(".textField0").show();
  			}
  		}
  		if ($(this).attr("id") == "inactDay1") {
  		    var t_id = $(this).attr("id");
  		    var bro = $(this).parent().find("#actDay1");
  		    bro.attr("id", t_id);
  		    $(this).attr("id", "actDay1");
  		    ////////////////////////
  		    if ($(this).index() == 2) {
  		        $(this).parent().parent().find(".textField3").show();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 3) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").show();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 4) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").show();
  		    }
  		}
  		if ($(this).attr("id") == "spanCX" || $(this).attr("id") == "spanZT") {
  		    var t_id = $(this).attr("id");
  		    var bro = $(this).parent().find("#spanSQ");
  		    bro.attr("id", t_id);
  		    $(this).attr("id", "spanSQ");
  		    ////////////////////////
  		    if ($(this).index() == 2) {
  		        $(this).parent().parent().find(".textField3").show();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 3) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").show();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 4) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").show();
  		    }
  		}
  		if ($(this).attr("id") == "spanContractRunout" || $(this).attr("id") == "spanBirthRemind") {
  		    var t_id = $(this).attr("id");
  		    var bro = $(this).parent().find("#spanWorkFlow");
  		    bro.attr("id", t_id);
  		    $(this).attr("id", "spanWorkFlow");
  		    ////////////////////////
  		    if ($(this).index() == 2) {
  		        $(this).parent().parent().find(".textField3").show();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 3) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").show();
  		        $(this).parent().parent().find(".textField0").hide();
  		    }
  		    if ($(this).index() == 4) {
  		        $(this).parent().parent().find(".textField3").hide();
  		        $(this).parent().parent().find(".textField4").hide();
  		        $(this).parent().parent().find(".textField0").show();
  		    }
  		}
  	});
  	$(".textField4").hide();
});