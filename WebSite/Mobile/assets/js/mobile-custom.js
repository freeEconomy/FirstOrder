function strip_tags(input, allowed) {
    input = input.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;nbsp;/g, ' ').replace(/&nbsp;/g, ' ').replace(/&quot;/g, '"');

    allowed = (((allowed || "") + "").toLowerCase().match(/<[a-z][a-z0-9]*>/g) || []).join(''); // making sure the allowed arg is a string containing only tags in lowercase (<a><b><c>)
    var tags = /<\/?([a-z][a-z0-9]*)\b[^>]*>/gi,
        commentsAndPhpTags = /<!--[\s\S]*?-->|<\?(?:php)?[\s\S]*?\?>/gi;
    return input.replace(commentsAndPhpTags, '').replace(tags, function ($0, $1) {
        return allowed.indexOf('<' + $1.toLowerCase() + '>') > -1 ? $0 : '';
    });
}

function setCommaHis(obj) {
    num = obj.value;
    var tmp = "";
    var resultStr = "";
    if (num != "") {
        tmp = num.split('.');
        var str = new Array();
        var v = tmp[0].replace(/,/gi, '');
        for (var i = 0; i <= v.length; i++) {
            str[str.length] = v.charAt(v.length - i);
            if (i % 3 == 0 && i != 0 && i != v.length) {
                str[str.length] = '.';
            }
        }
        str = str.reverse().join('').replace(/\./gi, ',');
        str = str.replace(/-,/gi, "-");
        resultStr = ((tmp.length == 2) ? str + '.' + tmp[1] : str);

        resultStr = resultStr.replace(/-,/gi, '-');
    }

    var frm = document.afrm;
    obj.value = resultStr;
}

function setCommaHisVal(num) {
    num = num + "";
    num = num.replace(/\,/gi, '');
    var len, point, str;

    point = num.length % 3;
    len = num.length;
    str = num.substring(0, point);
    while (point < len) {
        if (str != "") str += ",";
        str += num.substring(point, point + 3);
        point += 3;
    }

    return str;
}

function setCommaHis_Del(str) {
    var resultStr = "";
    if (str != "") {
        resultStr = str;
    }

    if (typeof (resultStr) == 'undefined') {
        resultStr = "";
    }

    resultStr = resultStr.replace(/\,/gi, '');

    return resultStr
}

// 숫자만 입력받는다. "-"도 제외
function fnNumberOnly(e) {
    var keyCode = "";

    if (window.event)
        keyCode = window.event.keyCode;
    else
        keyCode = e.which;

    if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || keyCode == 8 || keyCode == 9 || keyCode == 13 || keyCode == 46) {
        return true;
    }
    else {

        if (navigator.appName == "Opera") {
            e.preventDefault();
            return false;
        }

        if (window.event)
            window.event.returnValue = false;
        else {
            e.preventDefault();
        }
    }
}

function GetEmptyNumber(val) {
    if (isNaN(val) || val == '') {
        val = 0;
    }
    return parseFloat(val);
}

function showMessageToolTip(id, msg) {
    $('#' + id).tooltip('enable')
    .attr('data-original-title', msg)
    .tooltip('show');
    /*setTimeout(function () {
        $('#' + id).tooltip('hide');
        $('#' + id).tooltip('disable');
    }, 3000);*/
}

function showMessageScroll(id, msg) {
    var offset = $("#" + id).offset();
    $('html, body').animate({ scrollTop: offset.top }, 400);
    alert(msg);
}

function SetInputSelect(obj) {
    obj.select();
}