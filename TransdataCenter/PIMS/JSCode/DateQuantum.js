function ConvertDateToString(aDate) {
    return aDate.getFullYear().toString() + "-" + appendZero((aDate.getMonth() + 1).toString()) + "-" +appendZero( aDate.getDate().toString());
}
function appendZero(s) { return ("00" + s).substr((s + "").length); }

function ConvertMonthToString(aDate,month,day) {
    return aDate.getFullYear().toString() + "-" +appendZero( month.toString()) + "-" + appendZero(day.toString());
}


var DateQuantumComboBox, DateQuantumBeginEdit, DateQuantumEndEdit;
function OnDateQuantum() {
    var obj = event.srcElement;
    var today = new Date();
    var aDay = 86400000;
    for (var i = 0; i < obj.length; i++)
        if (obj[i].selected)
        switch (i) {
        case 1: //今天
            DateQuantumBeginEdit.value = ConvertDateToString(today);
            DateQuantumEndEdit.value = ConvertDateToString(today);
            break;
        case 2: //昨天
            var resultDay = new Date(today - aDay);
            DateQuantumBeginEdit.value = ConvertDateToString(resultDay);
            DateQuantumEndEdit.value = ConvertDateToString(resultDay);
            break;
        case 3: //上一周
            var bday, eday;
            bday = new Date(today - aDay * (today.getDay() + 6));
            DateQuantumBeginEdit.value = ConvertDateToString(bday);
            eday = new Date(today - aDay * today.getDay());
            DateQuantumEndEdit.value = ConvertDateToString(eday);
            break;
        case 4: //上个月
            var bday, eday;
            eday = new Date(today - aDay * (today.getDate()));
            bday = new Date(eday - aDay * (eday.getDate() - 1))
            DateQuantumBeginEdit.value = ConvertDateToString(bday);
            DateQuantumEndEdit.value = ConvertDateToString(eday);
            break;
        case 5: //上个季度
            switch (today.getMonth()) {
                case 0:
                case 1:
                case 2:
                    DateQuantumBeginEdit.value = (today.getFullYear() - 1) + "-10-1";
                    DateQuantumEndEdit.value = (today.getFullYear() - 1) + "-12-31";
                    break;
                case 3:
                case 4:
                case 5:
                    DateQuantumBeginEdit.value = today.getFullYear().toString() + "-01-1";
                    DateQuantumEndEdit.value = today.getFullYear().toString() + "-03-31";
                    break;
                case 6:
                case 7:
                case 8:
                    DateQuantumBeginEdit.value = today.getFullYear().toString() + "-04-1";
                    DateQuantumEndEdit.value = today.getFullYear().toString() + "-06-30";
                    break;
                case 9:
                case 10:
                case 11:
                    DateQuantumBeginEdit.value = today.getFullYear().toString() + "-07-1";
                    DateQuantumEndEdit.value = today.getFullYear().toString() + "-09-30";
                    break;

            }
            break;
        default:

    }
}
function OnDayQuantum() {
    var obj = event.srcElement;
    var today = new Date();
    var aDay = 86400000;
    for (var i = 0; i < obj.length; i++)
        if (obj[i].selected)
        switch (i) {
        case 1: //今天
            DateQuantumBeginEdit.value = ConvertDateToString(today);
        
            break;
        case 2: //昨天
            var resultDay = new Date(today - aDay);
            DateQuantumBeginEdit.value = ConvertDateToString(resultDay);
          
            break;
    
            
        default:

    }
}
//获取每个月的天数 month=1..12
function DayNumOfMonth(Month) {
    var today = new Date();
    var d = new Date(today.getFullYear(), Month, 0);
    return d.getDate();
} 

function OnMonthQuantum() {
    var obj = event.srcElement;
    var today = new Date();
    var aDay = 86400000;
    var eday;
    for (var i = 0; i < obj.length; i++)
        if (obj[i].selected)
        switch (i) {
            case 0: //一月   
                eday = DayNumOfMonth(1)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 01, 1);  //1月1号
                DateQuantumEndEdit.value = ConvertMonthToString(today, 01, eday); //1月eday号
                break;
            case 1:
                eday = DayNumOfMonth(2)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 02, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 02, eday);
                break;
            case 2:
                eday = DayNumOfMonth(3)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 03, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 03, eday);
                break;
            case 3:
                eday = DayNumOfMonth(4)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 4, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 4, eday);
                break;
            case 4:
                eday = DayNumOfMonth(5)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 05, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 05, eday);
                break;
            case 5:
                eday = DayNumOfMonth(6)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 06, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 06, eday);
                break;
            case 6:
                eday = DayNumOfMonth(7)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 07, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 07, eday);
                break;
            case 7:
                eday = DayNumOfMonth(8)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 08, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 08, eday);
                break;
            case 8:
                eday = DayNumOfMonth(9)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 09, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today,09, eday);
                break;
            case 9:
                eday = DayNumOfMonth(10)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 10, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 10, eday);
                break;
            case 10:
                eday = DayNumOfMonth(11)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 11, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 11, eday);
                break;
            case 11:
                eday = DayNumOfMonth(12)
                DateQuantumBeginEdit.value = ConvertMonthToString(today, 12, 1);
                DateQuantumEndEdit.value = ConvertMonthToString(today, 12, eday);
                break;

        default:

    }
}

function OnDateQuantumEditChange() {
    DateQuantumComboBox.selectedIndex = 0;
}

function SetDateQuantumControl(aComboBoxID, aBeginEditID, aEndEditID) {
    DateQuantumComboBox = document.getElementById(aComboBoxID);
    DateQuantumBeginEdit = document.getElementById(aBeginEditID);
    DateQuantumEndEdit = document.getElementById(aEndEditID);
    if (DateQuantumComboBox != null && DateQuantumBeginEdit != null && DateQuantumEndEdit != null) {
        DateQuantumComboBox.onchange = OnDateQuantum;
        DateQuantumBeginEdit.onchange = OnDateQuantumEditChange;
        DateQuantumEndEdit.onchange = OnDateQuantumEditChange;
    }
    else {
        DateQuantumComboBox.onchange = null;
        DateQuantumBeginEdit.onchange = null;
        DateQuantumEndEdit.onchange = null;
    }
}

function SetDayQuantumControl(aComboBoxID, aBeginEditID) {
    DateQuantumComboBox = document.getElementById(aComboBoxID);
    DateQuantumBeginEdit = document.getElementById(aBeginEditID);

    if (DateQuantumComboBox != null && DateQuantumBeginEdit != null) {
        DateQuantumComboBox.onchange = OnDayQuantum;
        DateQuantumBeginEdit.onchange = OnDateQuantumEditChange;

    }
    else {
        DateQuantumComboBox.onchange = null;
        DateQuantumBeginEdit.onchange = null;

    }
}
function SetMonthQuantumControl(aComboBoxID, aBeginEditID, aEndEditID) {
    DateQuantumComboBox = document.getElementById(aComboBoxID);
    DateQuantumBeginEdit = document.getElementById(aBeginEditID);
    DateQuantumEndEdit = document.getElementById(aEndEditID);
    if (DateQuantumComboBox != null && DateQuantumBeginEdit != null && DateQuantumEndEdit != null) {
        DateQuantumComboBox.onchange = OnMonthQuantum;
        DateQuantumBeginEdit.onchange = OnDateQuantumEditChange;
        DateQuantumEndEdit.onchange = OnDateQuantumEditChange;
    }
    else {
        DateQuantumComboBox.onchange = null;
        DateQuantumBeginEdit.onchange = null;
        DateQuantumEndEdit.onchange = null;
    }
}