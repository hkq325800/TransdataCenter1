

//左侧导航展开
function show(c_Str){
	if(document.all(c_Str).style.display=='none'){
		document.all(c_Str).style.display='block';
	}else{
		document.all(c_Str).style.display='none';
	}
}

// 全选操作
function checkAll(obj, checkboxObj) {
	var o = document.getElementById(obj);
	var tableObj = $(checkboxObj).parents("table").first();
	if(o.checked)
	{
		checkboxObj.style.backgroundImage="url(img/check.gif)";
		$("div.cdiv").css("backgroundImage","url(img/check.gif)");
		o.checked = false;
	}
	else
	{
		checkboxObj.style.backgroundImage="url(img/checked.gif)";
		$("div.cdiv").css("backgroundImage","url(img/checked.gif)");
		o.checked = true;
	}
	$(tableObj).find("tr td input:checkbox").prop("checked", $(o).prop("checked"));
}

// 选中一个操作
function checkItem(obj, checkboxObj) {
	var o = document.getElementById(obj);
	var tableObj = $(checkboxObj).parents("table").first();

	if(o.checked)
	{
		checkboxObj.style.backgroundImage="url(img/check.gif)";
		o.checked = false;
	}
	else
	{
		checkboxObj.style.backgroundImage="url(img/checked.gif)";
		o.checked = true;
	}
	
	if ($(tableObj).find("tr:gt(0)").size() == 0) {
		$(tableObj).find("tr th input:checkbox").prop("checked", false);
		return;
	}

	var isCheckedAll = true;

	$(tableObj).find("tr td input:checkbox").each(function() {
		if (!isCheckedAll) {
			return;
		}

		if (!$(this).prop("checked")) {
			isCheckedAll = false;
		}
	});

	$(tableObj).find("tr th input:checkbox").prop("checked", isCheckedAll);
	if(isCheckedAll){
		$("div.cdiv0").css("backgroundImage","url(img/checked.gif)");
	}else{
		$("div.cdiv0").css("backgroundImage","url(img/check.gif)");
	}
}



