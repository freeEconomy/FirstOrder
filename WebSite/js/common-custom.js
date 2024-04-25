function showMessageDiv(id, msg) {
    var offset = $("#" + id).offset();
    $('html, body').animate({ scrollTop: offset.top }, 400);
    $("#" + id).html("<div id='msgDiv'>" + msg + "</div>");
    $("#" + id).delay(3000).fadeOut();
}

function showMessageToolTip(id, msg) {
    var element = $('#' + id);
    var title = element.attr('title'); // 클릭된 요소의 타이틀 속성 가져오기
    if (!title) {
        element.attr('title', msg); // 타이틀 속성이 없으면 메시지를 타이틀 속성으로 설정
    }

    element.tooltip({
        content: msg,
        show: { effect: 'slideDown', delay: 100, duration: 250 }, // 3초 동안 나타남
        // 툴팁이 열릴 때 실행되는 이벤트 핸들러
        open: function (event, ui) {
            // 3초 후에 툴팁을 자동으로 닫음
            setTimeout(function () {
                $(ui.tooltip).hide(); // 툴팁을 숨김
            }, 3000); // 3초 후에 숨김
        },
        // 툴팁이 닫힐 때 실행되는 이벤트 핸들러
        close: function (event, ui) {
            // 툴팁을 재활성화하여 다시 열 수 있도록 설정
            $(this).tooltip("disable").tooltip("enable");

        }
    });

    element.tooltip("close").tooltip("open");

    /*$('#' + id).tooltip('enable')
    .attr('data-original-title', msg)
    .tooltip('show');
    setTimeout(function () {
        $('#' + id).tooltip('hide');
        $('#' + id).tooltip('disable');
    }, 3000);*/
}

// 숫자만 입력받는다. "-"는 입력받는다
function fnNumberCheck(obj) //
{
    event = event || window.event;

    var keyID = (event.which) ? event.which : event.keyCode;

    if ((keyID >= 48 && keyID <= 57) || (keyID >= 96 && keyID <= 105) || keyID == 8 || keyID == 46 || keyID == 37 || keyID == 39 || keyID == 188 || keyID == 13 || keyID == 9 || keyID == 109 || keyID == 189) {
        
    } else {
        event.returnValue = false;
    }
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

function OpenBaeSong(totalID, baeSongOptID, baeSongID, baeSongNameID, blju_date, blju_times, blju_mainbuyer, blju_sample) {
    if ($('#' + totalID + '').val() > 0) {
        OpenModal("/Page/BaeSong.aspx?baeSongOptID=" + baeSongOptID + "&baeSongID=" + baeSongID + "&baeSongNameID=" + baeSongNameID + "&blju_date=" + blju_date + "&blju_times=" + blju_times + "&blju_mainbuyer=" + blju_mainbuyer + "&blju_sample=" + blju_sample, '배송지 선택', '', '700', '840');
    }
    else {
        alert("스타일이 1건도 저장되지 않아서 '배송지'를 등록할 수 없습니다!");
    }

    return false;
}

function popupAddress(paramData) {
    if (paramData == '' || paramData == undefined) {
        new daum.Postcode({
            oncomplete: function (data) {
                console.log(data);
                $('.txtZipcode').val(data.zonecode);

                var addrList = data.sido + ' ' + data.sigungu;

                if (data.userSelectedType === 'R') { // 도로명 주소일 경우
                    /*
                    data.roadAddress = data.roadAddress.replace(data.sido, '').trim();
                    data.roadAddress = data.roadAddress.replace(data.sigungu, '').trim();
                    $('.txtDong').val(data.roadAddress);
                    */

                    addrList = addrList + ' ' + data.roadname;

                    if (data.buildingName != '') // 건물 이름이 있을 때
                    {
                        var number = data.roadAddress.split(' ');
                        $('.txtAddress1').val(addrList + ' ' + number[number.length - 1] + ' (' + data.buildingName + ') ');
                    }
                    else {
                        var number = data.roadAddress.split(' ');
                        $('.txtAddress1').val(addrList + ' ' + number[number.length - 1]);
                    }

                } else { // 지번 주소일 경우
                    /*
                    data.jibunAddress = data.jibunAddress.replace(data.sido, '').trim();
                    data.jibunAddress = data.jibunAddress.replace(data.sigungu, '').trim();
                    $('.txtDong').val(data.jibunAddress);
                    */
                    
                    //addrList = addrList + ' ' + data.bname;
                    //var number = data.jibunAddress.split(' ');
                    //$('.txtAddress1').val(number[number.length - 1]);

                    $('.txtAddress1').val(data.jibunAddress);
                }

                try {
                    $('.txtAddress1').selectRange($('.txtAddress1').val().length, $('.txtAddress1').val().length);
                } catch (e) { }
            }
        }).open();
    }

    return false;
}

function popupAddressSend(paramData) {
    if (paramData == '' || paramData == undefined) {
        new daum.Postcode({
            oncomplete: function (data) {
                console.log(data);
                $('.txtZipcodeSend').val(data.zonecode);

                var addrList = data.sido + ' ' + data.sigungu;

                if (data.userSelectedType === 'R') { // 도로명 주소일 경우
                    /*
                    data.roadAddress = data.roadAddress.replace(data.sido, '').trim();
                    data.roadAddress = data.roadAddress.replace(data.sigungu, '').trim();
                    $('.txtDong').val(data.roadAddress);
                    */

                    addrList = addrList + ' ' + data.roadname;

                    if (data.buildingName != '') // 건물 이름이 있을 때
                    {
                        var number = data.roadAddress.split(' ');
                        $('.txtAddress1Send').val(addrList + ' ' + number[number.length - 1] + ' (' + data.buildingName + ') ');
                    }
                    else {
                        var number = data.roadAddress.split(' ');
                        $('.txtAddress1Send').val(addrList + ' ' + number[number.length - 1]);
                    }

                } else { // 지번 주소일 경우
                    /*
                    data.jibunAddress = data.jibunAddress.replace(data.sido, '').trim();
                    data.jibunAddress = data.jibunAddress.replace(data.sigungu, '').trim();
                    $('.txtDong').val(data.jibunAddress);
                    */

                    //addrList = addrList + ' ' + data.bname;
                    //var number = data.jibunAddress.split(' ');
                    //$('.txtAddress1Send').val(number[number.length - 1]);

                    $('.txtAddress1Send').val(data.jibunAddress);
                }

                try {
                    $('.txtAddress1Send').selectRange($('.txtAddress1Send').val().length, $('.txtAddress1Send').val().length);
                } catch (e) { }
            }
        }).open();
    }

    return false;
}

function OpenChatingCheck(totalID, blju_date, blju_times, blju_mainbuyer, blju_sample, refresh) {
    if ($('#' + totalID + '').val() > 0) {
        OpenChating(blju_date, blju_times, blju_mainbuyer, blju_sample, refresh);
    }
    else {
        alert("스타일이 1건도 저장되지 않아서 'Q & A 대화방'에 접속할 수 없습니다!");
    }

    return false;
}

function OpenChating(blju_date, blju_times, blju_mainbuyer, blju_sample, refresh) {
    //window.open("/Page/Chat.aspx?blju_date=" + blju_date + "&blju_times=" + blju_times + "&blju_mainbuyer=" + blju_mainbuyer + "&blju_sample=" + blju_sample, "BaeSong", "width=500,height=700,top=100,left=500,scrollbars=yes,resizable=yes");
    OpenModal("/Page/Chat.aspx?blju_date=" + blju_date + "&blju_times=" + blju_times + "&blju_mainbuyer=" + blju_mainbuyer + "&blju_sample=" + blju_sample, 'Q & A 대화방', refresh, '560', '680');

    /*$("#dialog").html('');
    $("#dialog").append($('<iframe frameborder="0" width="100%" height="700"></iframe>').attr('src', '/Page/Chat.aspx?blju_date=' + blju_date + '&blju_times=' + blju_times + '&blju_mainbuyer=' + blju_mainbuyer + '&blju_sample=' + blju_sample)).dialog({
        title: "대화방",
        width: 500,
        autoOpen: true,
        resizable: false,
        modal: true,
        open: function () {
            $(".ui-widget-overlay").bind("click", function () {
                $("#dialog").dialog("close");
            })
        },
        buttons: {
            "닫 기": function () {
                $(this).dialog("close");
            }
        }
    });*/

    return false;
}

function OpenModal(siteUrl, viewTitle, refresh, w, h) {

    var nowUrl = $(location).attr('href');
    var addUrl = "";

    if (nowUrl.indexOf('hgubun=back') != -1) {
    }
    else if (nowUrl.indexOf('.aspx?') != -1) {
        addUrl = "&hgubun=back";
    }
    else if (nowUrl.indexOf('.aspx') != -1) {
        addUrl = "?hgubun=back";
    }

    $("#dialog").remove();

    $("body").append($("<div id='dialog'></div>"));

    $("#dialog").append($('<iframe frameborder="0" width="100%" height="98%"></iframe>').attr('src', siteUrl)).dialog({
        title: viewTitle,
        width: w,
        height: h,
        autoOpen: true,
        resizable: false,
        modal: true,
        open: function () {
        },
        close: function () {
            if (refresh == "ok") {
                //window.location.href = window.location.href;
                window.location.href = nowUrl + addUrl;
            }
        }
    });
}

function zoomViewImage(gubun, code, w, h) {
    var w2 = w;
    var h2 = h;
    if (w2 > 1200) {
        w2 = 1200;
    }
    if (h2 > 800) {
        h2 = 800;
    }

    var width = parseInt(w2) + 20;
    var height = parseInt(h2 + 30) + 10;

    window.open("/Common/ZoomImage.aspx?gubun=" + gubun + "&code=" + escape(code) + "&width=" + w + "&height=" + h, "", "top=50,left=150,width=" + width + ",height=" + height + ",resizable=yes,scrollbars=yes");
    return false;
}

function chkSpecWord(thisword) { // 특수문자체크 특수 문자가 있을시 true 리턴
    var flag = false;
    var specialChars = "~`!@#$%^&*-=+\|[](){};:'<.,>/?_";
    wordadded = thisword;
    for (i = 0; i < wordadded.length; i++) {
        for (j = 0; j < specialChars.length; j++) {
            if (wordadded.charAt(i) == specialChars.charAt(j)) {
                flag = true;
                true;
            }
        }
    }
    return flag;
}

function OpenKureSpecPrint(blju_date, blju_times, blju_mainbuyer, blju_sample) {
    var openValue = window.open("/Print/KureSpecPrint.aspx?blju_date=" + blju_date + "&blju_times=" + blju_times + "&blju_mainbuyer=" + blju_mainbuyer + "&blju_sample=" + blju_sample, "", "width=800,height=900,scrollbars=yes,resizable=yes");
    openValue.focus();

    return false;
}

function printPdf(url) {
    var linkAddress = "/Print/PDFPrint.aspx?url=";
    var link = encodeURIComponent(url);

    window.open(linkAddress + link, "printPdf", "width=1000,height=650,scrollbars=yes,menubars=no");

    return false;
}

//쿠키 저장
function setCookie(name, value, expiredays) {
    var todayDate = new Date();
    todayDate.setDate(todayDate.getDate() + expiredays);
    document.cookie = name + "=" + escape(value) + "; path=/; expires=" + todayDate.toGMTString() + ";"
}