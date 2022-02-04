<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="AsRequest_Bak.aspx.cs" Inherits="Page_AsRequest_Bak" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	    
        var rowNum = 0;

        $(document).ready(function () {
            insRow();

            $('#<%= Upload.ClientID %>').change(function () {
                var files = $(this)[0].files;
                var array = [];

                $('#<%= lblUploadFile.ClientID %>').empty();

                if (files.length > 1) {
                    for (var i = 0; i < files.length; i++) {
                        array.push(files[i].name);
                    }

                    $('#<%= lblUploadFile.ClientID %>').append(array.join(", "));
                }

                $('#divFileHelp').hide();
            });
        });

        function CheckProduct() {
            var searchValue = $("#<%=this.txtProduct.ClientID%>").val();

            if (searchValue == "") {
                alert("스타일을 입력해주세요.");
                $("#<%=this.txtProduct.ClientID%>").focus();
                return false;
            }

            <%= Page.GetPostBackEventReference(btnSearch) %>
        }
        
        function selectStyle(obj, date, times, sample, style, BnpmDQty01, BnpmDQty02, BnpmDQty03, BnpmDQty04, BnpmDQty05, BnpmDQty06, BnpmDQty07, BnpmDQty08, BnpmDQty09, BnpmDQty10, BnpmDQty11, BnpmDQty12, BnpmDQty13, BnpmDQty14, BnpmDQty15, BnpmDQty16, BnpmDQty17) {

            $("#<%=this.hidDate.ClientID%>").val(date);
            $("#<%=this.hidTimes.ClientID%>").val(times);
            $("#<%=this.hidSample.ClientID%>").val(sample);
            $("#<%=this.hidStyle.ClientID%>").val(style);
            $("#<%=this.hidQty01.ClientID%>").val(BnpmDQty01);
            $("#<%=this.hidQty02.ClientID%>").val(BnpmDQty02);
            $("#<%=this.hidQty03.ClientID%>").val(BnpmDQty03);
            $("#<%=this.hidQty04.ClientID%>").val(BnpmDQty04);
            $("#<%=this.hidQty05.ClientID%>").val(BnpmDQty05);
            $("#<%=this.hidQty06.ClientID%>").val(BnpmDQty06);
            $("#<%=this.hidQty07.ClientID%>").val(BnpmDQty07);
            $("#<%=this.hidQty08.ClientID%>").val(BnpmDQty08);
            $("#<%=this.hidQty09.ClientID%>").val(BnpmDQty09);
            $("#<%=this.hidQty10.ClientID%>").val(BnpmDQty10);
            $("#<%=this.hidQty11.ClientID%>").val(BnpmDQty11);
            $("#<%=this.hidQty12.ClientID%>").val(BnpmDQty12);
            $("#<%=this.hidQty13.ClientID%>").val(BnpmDQty13);
            $("#<%=this.hidQty14.ClientID%>").val(BnpmDQty14);
            $("#<%=this.hidQty15.ClientID%>").val(BnpmDQty15);
            $("#<%=this.hidQty16.ClientID%>").val(BnpmDQty16);
            $("#<%=this.hidQty17.ClientID%>").val(BnpmDQty17);

            $("#selTable .selData").css("background-color", "#FFF");
            obj.style.backgroundColor = '#9ecef7'
            $("#asTable").show();

            var offset = $("#offsetTable").offset();
            $('html, body').animate({ scrollTop: offset.top - 100 });

            $('.asRow').remove();

            $("input[id^=Size_Total]").each(function () {
                $(this).val('');
            });

            rowNum = 0;
            insRow();
        }

	    function insRow() {
	        
		    var items = [];
		    var html = '';
		    items.push('<tr class="defaultDataTR asRow" id="asRow_' + rowNum + '">');
		    items.push('    <td>');
		    items.push('        <input type="hidden" name="rowNum" value="' + rowNum + '" />');
            items.push('        <a style="cursor:pointer" onclick="insRow();"><i class="fa fa-plus" aria-hidden="true"></i></a> ');
            items.push('        &nbsp;&nbsp;<a style="cursor:pointer" onclick="delRow(' + rowNum + ');"><i class="fa fa-minus" aria-hidden="true"></i></a> ');
            items.push('    </td>');
            items.push('    <td>');

            <% if (valPreVal.Text == "tbl") { %>
            items.push('        <select name="BnpmD_BnpmCode_' + rowNum + '">');
            items.push('            <option value="MA1">색상</option>');
            items.push('            <option value="MA2">오염</option>');
            items.push('            <option value="MA3">이염</option>');
            items.push('            <option value="MA4">원단</option>');
            items.push('            <option value="">--------------</option>');
            items.push('            <option value="MB1">찍찍이</option>');
            items.push('            <option value="MB2">단추</option>');
            items.push('            <option value="MB3">지퍼</option>');
            items.push('            <option value="MB4">충전재</option>');
            items.push('            <option value="MB5">라벨</option>');
            items.push('            <option value="MB6">주머니</option>');
            items.push('            <option value="MB7">장식물</option>');
            items.push('            <option value="MB8">반사띠</option>');
            items.push('            <option value="">--------------</option>');
            items.push('            <option value="MC1">사이즈오표기</option>');
            items.push('            <option value="MC2">박음질</option>');
            items.push('            <option value="MC3">사이즈스팩</option>');
            items.push('            <option value="">--------------</option>');
            items.push('            <option value="MD1">오염</option>');
            items.push('            <option value="MD2">냄새</option>');
            items.push('            <option value="">--------------</option>');
            items.push('            <option value="MZ1">곰팡이</option>');
            items.push('            <option value="MZZ">기타</option>');
            items.push('        </select>');
	        <% } else { %>
	        items.push('        <select name="BnpmD_BnpmCode_' + rowNum + '">');
	        items.push('            <option value="TA1">패턴</option>');
	        items.push('            <option value="TA2">오염</option>');
	        items.push('            <option value="TA3">조립</option>');
	        items.push('            <option value="TA4">갑피</option>');
	        items.push('            <option value="TA5">짝발</option>');
	        items.push('            <option value="TA6">깔창</option>');
	        items.push('            <option value="TA7">끈고리/구멍</option>');
	        items.push('            <option value="TA8">다이얼</option>');
	        items.push('            <option value="TA9">지퍼</option>');
	        items.push('            <option value="TAA">찍찍이</option>');
	        items.push('            <option value="TAB">토캡</option>');
	        items.push('            <option value="TAC">내답판</option>');
	        items.push('            <option value="TAD">스냅단추</option>');
	        items.push('            <option value="TAE">인/미드/아웃솔</option>');
	        items.push('            <option value="TAZ">기타</option>');
	        items.push('        </select>');
            <% } %>

            items.push('    </td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty01_' + rowNum + '" name="BnpmD_Qty01_' + rowNum + '" class="tdMoney2 size_01" value="" onkeyup="CalQty(\'01\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty02_' + rowNum + '" name="BnpmD_Qty02_' + rowNum + '" class="tdMoney2 size_02" value="" onkeyup="CalQty(\'02\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty03_' + rowNum + '" name="BnpmD_Qty03_' + rowNum + '" class="tdMoney2 size_03" value="" onkeyup="CalQty(\'03\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty04_' + rowNum + '" name="BnpmD_Qty04_' + rowNum + '" class="tdMoney2 size_04" value="" onkeyup="CalQty(\'04\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty05_' + rowNum + '" name="BnpmD_Qty05_' + rowNum + '" class="tdMoney2 size_05" value="" onkeyup="CalQty(\'05\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty06_' + rowNum + '" name="BnpmD_Qty06_' + rowNum + '" class="tdMoney2 size_06" value="" onkeyup="CalQty(\'06\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty07_' + rowNum + '" name="BnpmD_Qty07_' + rowNum + '" class="tdMoney2 size_07" value="" onkeyup="CalQty(\'07\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty08_' + rowNum + '" name="BnpmD_Qty08_' + rowNum + '" class="tdMoney2 size_08" value="" onkeyup="CalQty(\'08\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty09_' + rowNum + '" name="BnpmD_Qty09_' + rowNum + '" class="tdMoney2 size_09" value="" onkeyup="CalQty(\'09\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty10_' + rowNum + '" name="BnpmD_Qty10_' + rowNum + '" class="tdMoney2 size_10" value="" onkeyup="CalQty(\'10\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty11_' + rowNum + '" name="BnpmD_Qty11_' + rowNum + '" class="tdMoney2 size_11" value="" onkeyup="CalQty(\'11\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty12_' + rowNum + '" name="BnpmD_Qty12_' + rowNum + '" class="tdMoney2 size_12" value="" onkeyup="CalQty(\'12\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty13_' + rowNum + '" name="BnpmD_Qty13_' + rowNum + '" class="tdMoney2 size_13" value="" onkeyup="CalQty(\'13\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty14_' + rowNum + '" name="BnpmD_Qty14_' + rowNum + '" class="tdMoney2 size_14" value="" onkeyup="CalQty(\'14\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty15_' + rowNum + '" name="BnpmD_Qty15_' + rowNum + '" class="tdMoney2 size_15" value="" onkeyup="CalQty(\'15\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty16_' + rowNum + '" name="BnpmD_Qty16_' + rowNum + '" class="tdMoney2 size_16" value="" onkeyup="CalQty(\'16\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty17_' + rowNum + '" name="BnpmD_Qty17_' + rowNum + '" class="tdMoney2 size_17" value="" onkeyup="CalQty(\'17\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_QtyTotal_' + rowNum + '" name="BnpmD_QtyTotal_' + rowNum + '" class="tdMoney2" value="" disabled="disabled" /></td>');
            items.push('    <td><input type="text" id="BnpmD_UsedRemark_' + rowNum + '" name="BnpmD_UsedRemark_' + rowNum + '" style="width: 170px;" maxlength="30" value="" /></td>');
            items.push('    <td><input type="text" id="BnpmD_ReasonRemark_' + rowNum + '" name="BnpmD_ReasonRemark_' + rowNum + '" style="width: 250px;" maxlength="30" value="" /></td>');
            items.push('</tr>');
            
		    html = items.join('');

		    $('#insTable').append($(html));

		    rowNum++;
	    }

	    function delRow(row) {
	        if ($('.asRow').length > 1) {
	            $('#asRow_' + row).remove();
	        }
	        else {
	            alert("1개의 행은 남아 있어야 합니다!");
	        }
	    }

	    function GetEmptyNumber(val) {
	        if (isNaN(val) || val == '') {
	            val = 0;
	        }
	        return parseFloat(val);
	    }

	    function CalQty(num, rowNum) {

	        var bnpmCode = $("#BnpmD_Code_" + rowNum).val();
	        if (bnpmCode == "") {
	            showMessageToolTip('BnpmD_Code_' + rowNum, '불량사유를 선택해주셔야 합니다.');
	            $('#BnpmD_Qty' + num + '_' + rowNum).val('0');
	            $('#BnpmD_Qty' + num + '_' + rowNum).focus();
	        }

	        var order1 = setCommaHis_Del($("#<%=this.hidQty01.ClientID%>").val());
	        var order2 = setCommaHis_Del($("#<%=this.hidQty02.ClientID%>").val());
	        var order3 = setCommaHis_Del($("#<%=this.hidQty03.ClientID%>").val());
	        var order4 = setCommaHis_Del($("#<%=this.hidQty04.ClientID%>").val());
	        var order5 = setCommaHis_Del($("#<%=this.hidQty05.ClientID%>").val());
	        var order6 = setCommaHis_Del($("#<%=this.hidQty06.ClientID%>").val());
	        var order7 = setCommaHis_Del($("#<%=this.hidQty07.ClientID%>").val());
	        var order8 = setCommaHis_Del($("#<%=this.hidQty08.ClientID%>").val());
	        var order9 = setCommaHis_Del($("#<%=this.hidQty09.ClientID%>").val());
	        var order10 = setCommaHis_Del($("#<%=this.hidQty10.ClientID%>").val());
	        var order11 = setCommaHis_Del($("#<%=this.hidQty11.ClientID%>").val());
	        var order12 = setCommaHis_Del($("#<%=this.hidQty12.ClientID%>").val());
	        var order13 = setCommaHis_Del($("#<%=this.hidQty13.ClientID%>").val());
	        var order14 = setCommaHis_Del($("#<%=this.hidQty14.ClientID%>").val());
	        var order15 = setCommaHis_Del($("#<%=this.hidQty15.ClientID%>").val());
	        var order16 = setCommaHis_Del($("#<%=this.hidQty16.ClientID%>").val());
	        var order17 = setCommaHis_Del($("#<%=this.hidQty17.ClientID%>").val());

	        order1 = GetEmptyNumber(order1);
	        order2 = GetEmptyNumber(order2);
	        order3 = GetEmptyNumber(order3);
	        order4 = GetEmptyNumber(order4);
	        order5 = GetEmptyNumber(order5);
	        order6 = GetEmptyNumber(order6);
	        order7 = GetEmptyNumber(order7);
	        order8 = GetEmptyNumber(order8);
	        order9 = GetEmptyNumber(order9);
	        order10 = GetEmptyNumber(order10);
	        order11 = GetEmptyNumber(order11);
	        order12 = GetEmptyNumber(order12);
	        order13 = GetEmptyNumber(order13);
	        order14 = GetEmptyNumber(order14);
	        order15 = GetEmptyNumber(order15);
	        order16 = GetEmptyNumber(order16);
	        order17 = GetEmptyNumber(order17);
	        
	        var req1 = setCommaHis_Del($("#BnpmD_Qty01_" + rowNum).val());
	        var req2 = setCommaHis_Del($("#BnpmD_Qty02_" + rowNum).val());
	        var req3 = setCommaHis_Del($("#BnpmD_Qty03_" + rowNum).val());
	        var req4 = setCommaHis_Del($("#BnpmD_Qty04_" + rowNum).val());
	        var req5 = setCommaHis_Del($("#BnpmD_Qty05_" + rowNum).val());
	        var req6 = setCommaHis_Del($("#BnpmD_Qty06_" + rowNum).val());
	        var req7 = setCommaHis_Del($("#BnpmD_Qty07_" + rowNum).val());
	        var req8 = setCommaHis_Del($("#BnpmD_Qty08_" + rowNum).val());
	        var req9 = setCommaHis_Del($("#BnpmD_Qty09_" + rowNum).val());
	        var req10 = setCommaHis_Del($("#BnpmD_Qty10_" + rowNum).val());
	        var req11 = setCommaHis_Del($("#BnpmD_Qty11_" + rowNum).val());
	        var req12 = setCommaHis_Del($("#BnpmD_Qty12_" + rowNum).val());
	        var req13 = setCommaHis_Del($("#BnpmD_Qty13_" + rowNum).val());
	        var req14 = setCommaHis_Del($("#BnpmD_Qty14_" + rowNum).val());
	        var req15 = setCommaHis_Del($("#BnpmD_Qty15_" + rowNum).val());
	        var req16 = setCommaHis_Del($("#BnpmD_Qty16_" + rowNum).val());
	        var req17 = setCommaHis_Del($("#BnpmD_Qty17_" + rowNum).val());

	        req1 = GetEmptyNumber(req1);
	        req2 = GetEmptyNumber(req2);
	        req3 = GetEmptyNumber(req3);
	        req4 = GetEmptyNumber(req4);
	        req5 = GetEmptyNumber(req5);
	        req6 = GetEmptyNumber(req6);
	        req7 = GetEmptyNumber(req7);
	        req8 = GetEmptyNumber(req8);
	        req9 = GetEmptyNumber(req9);
	        req10 = GetEmptyNumber(req10);
	        req11 = GetEmptyNumber(req11);
	        req12 = GetEmptyNumber(req12);
	        req13 = GetEmptyNumber(req13);
	        req14 = GetEmptyNumber(req14);
	        req15 = GetEmptyNumber(req15);
	        req16 = GetEmptyNumber(req16);
	        req17 = GetEmptyNumber(req17);


	        // 세로 계산
            var sumVal = 0;
            $(".size_" + num).each(function () {
                sumVal += eval(GetEmptyNumber($(this).val()));
            });            
            $("#Size_Total" + num).val(sumVal);

            var reqT1 = setCommaHis_Del($("#Size_Total01").val());
            var reqT2 = setCommaHis_Del($("#Size_Total02").val());
            var reqT3 = setCommaHis_Del($("#Size_Total03").val());
            var reqT4 = setCommaHis_Del($("#Size_Total04").val());
            var reqT5 = setCommaHis_Del($("#Size_Total05").val());
            var reqT6 = setCommaHis_Del($("#Size_Total06").val());
            var reqT7 = setCommaHis_Del($("#Size_Total07").val());
            var reqT8 = setCommaHis_Del($("#Size_Total08").val());
            var reqT9 = setCommaHis_Del($("#Size_Total09").val());
            var reqT10 = setCommaHis_Del($("#Size_Total10").val());
            var reqT11 = setCommaHis_Del($("#Size_Total11").val());
            var reqT12 = setCommaHis_Del($("#Size_Total12").val());
            var reqT13 = setCommaHis_Del($("#Size_Total13").val());
            var reqT14 = setCommaHis_Del($("#Size_Total14").val());
            var reqT15 = setCommaHis_Del($("#Size_Total15").val());
            var reqT16 = setCommaHis_Del($("#Size_Total16").val());
            var reqT17 = setCommaHis_Del($("#Size_Total17").val());

            reqT1 = GetEmptyNumber(reqT1);
            reqT2 = GetEmptyNumber(reqT2);
            reqT3 = GetEmptyNumber(reqT3);
            reqT4 = GetEmptyNumber(reqT4);
            reqT5 = GetEmptyNumber(reqT5);
            reqT6 = GetEmptyNumber(reqT6);
            reqT7 = GetEmptyNumber(reqT7);
            reqT8 = GetEmptyNumber(reqT8);
            reqT9 = GetEmptyNumber(reqT9);
            reqT10 = GetEmptyNumber(reqT10);
            reqT11 = GetEmptyNumber(reqT11);
            reqT12 = GetEmptyNumber(reqT12);
            reqT13 = GetEmptyNumber(reqT13);
            reqT14 = GetEmptyNumber(reqT14);
            reqT15 = GetEmptyNumber(reqT15);
            reqT16 = GetEmptyNumber(reqT16);
            reqT17 = GetEmptyNumber(reqT17);

	        // 수량 체크 (T-BUK는 패스 필요)
            if (order1 < reqT1) {
                showMessageToolTip('BnpmD_Qty01_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty01_' + rowNum).val('0');
                $('#BnpmD_Qty01_' + rowNum).focus();
                req1 = 0;
            }
            if (order2 < reqT2) {
                showMessageToolTip('BnpmD_Qty02_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty02_' + rowNum).val('0');
                $('#BnpmD_Qty02_' + rowNum).focus();
                req2 = 0;
            }
            if (order3 < reqT3) {
                showMessageToolTip('BnpmD_Qty03_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty03_' + rowNum).val('0');
                $('#BnpmD_Qty03_' + rowNum).focus();
                req3 = 0;
            }
            if (order4 < reqT4) {
                showMessageToolTip('BnpmD_Qty04_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty04_' + rowNum).val('0');
                $('#BnpmD_Qty04_' + rowNum).focus();
                req4 = 0;
            }
            if (order5 < reqT5) {
                showMessageToolTip('BnpmD_Qty05_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty05_' + rowNum).val('0');
                $('#BnpmD_Qty05_' + rowNum).focus();
                req5 = 0;
            }
            if (order6 < reqT6) {
                showMessageToolTip('BnpmD_Qty06_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty06_' + rowNum).val('0');
                $('#BnpmD_Qty06_' + rowNum).focus();
                req6 = 0;
            }
            if (order7 < reqT7) {
                showMessageToolTip('BnpmD_Qty07_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty07_' + rowNum).val('0');
                $('#BnpmD_Qty07_' + rowNum).focus();
                req7 = 0;
            }
            if (order8 < reqT8) {
                showMessageToolTip('BnpmD_Qty08_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty08_' + rowNum).val('0');
                $('#BnpmD_Qty08_' + rowNum).focus();
                req8 = 0;
            }
            if (order9 < reqT9) {
                showMessageToolTip('BnpmD_Qty09_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty09_' + rowNum).val('0');
                $('#BnpmD_Qty09_' + rowNum).focus();
                req9 = 0;
            }
            if (order10 < reqT10) {
                showMessageToolTip('BnpmD_Qty10_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty10_' + rowNum).val('0');
                $('#BnpmD_Qty10_' + rowNum).focus();
                req10 = 0;
            }
            if (order11 < reqT11) {
                showMessageToolTip('BnpmD_Qty11_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty11_' + rowNum).val('0');
                $('#BnpmD_Qty11_' + rowNum).focus();
                req11 = 0;
            }
            if (order12 < reqT12) {
                showMessageToolTip('BnpmD_Qty12_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty12_' + rowNum).val('0');
                $('#BnpmD_Qty12_' + rowNum).focus();
                req12 = 0;
            }
            if (order13 < reqT13) {
                showMessageToolTip('BnpmD_Qty13_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty13_' + rowNum).val('0');
                $('#BnpmD_Qty13_' + rowNum).focus();
                req13 = 0;
            }
            if (order14 < reqT14) {
                showMessageToolTip('BnpmD_Qty14_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty14_' + rowNum).val('0');
                $('#BnpmD_Qty14_' + rowNum).focus();
                req14 = 0;
            }
            if (order15 < reqT15) {
                showMessageToolTip('BnpmD_Qty15_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty15_' + rowNum).val('0');
                $('#BnpmD_Qty15_' + rowNum).focus();
                req15 = 0;
            }
            if (order16 < reqT16) {
                showMessageToolTip('BnpmD_Qty16_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty16_' + rowNum).val('0');
                $('#BnpmD_Qty16_' + rowNum).focus();
                req16 = 0;
            }
            if (order17 < reqT17) {
                showMessageToolTip('BnpmD_Qty17_' + rowNum, '발주 수량보다 클 수 없습니다.');
                $('#BnpmD_Qty17_' + rowNum).val('0');
                $('#BnpmD_Qty17_' + rowNum).focus();
                req17 = 0;
            }


	        // 세로 계산2
            var sumVal = 0;
            $(".size_" + num).each(function () {
                sumVal += eval(GetEmptyNumber($(this).val()));
            });
            $("#Size_Total" + num).val(sumVal);

            // 가로 계산
            var reqT = eval(req1) + eval(req2) + eval(req3) + eval(req4) + eval(req5) + eval(req6) + eval(req7) + eval(req8) + eval(req9) + eval(req10) + eval(req11) + eval(req12) + eval(req13) + eval(req14) + eval(req15) + eval(req16) + eval(req17);

            reqT = GetEmptyNumber(reqT);

            document.getElementById("BnpmD_QtyTotal_" + rowNum).value = setCommaHisVal(reqT);

            var sumTotal = 0;

            $("input[id^=BnpmD_QtyTotal_]").each(function () {
                sumTotal += eval(GetEmptyNumber($(this).val()));
            });
            $("#Size_TotalSum").val(sumTotal);
        }

        function CheckAS(chk) {

            var check = GetEmptyNumber($("#Size_TotalSum").val());
            var buttonId = "";
            if (chk == 0) {
                buttonId = '<%=this.btnWrite.ClientID%>';
            }
            else if (chk == 1) {
                buttonId = '<%=this.btnComplete.ClientID%>';
            }

            if (check == 0) {
                showMessageToolTip(buttonId, '접수 요청한 수량이 없습니다.');
                return false;
            }

            return true;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="valPreVal" Visible="false" runat="server"></asp:Label>
        
    <asp:HiddenField ID="hidNowDate" runat="server" />
    <asp:HiddenField ID="hidNowTime" runat="server" />

    <asp:HiddenField ID="hidDate" runat="server" />
    <asp:HiddenField ID="hidTimes" runat="server" />
    <asp:HiddenField ID="hidSample" runat="server" />
    <asp:HiddenField ID="hidStyle" runat="server" />

    <asp:HiddenField ID="hidQty01" runat="server" />
    <asp:HiddenField ID="hidQty02" runat="server" />
    <asp:HiddenField ID="hidQty03" runat="server" />
    <asp:HiddenField ID="hidQty04" runat="server" />
    <asp:HiddenField ID="hidQty05" runat="server" />
    <asp:HiddenField ID="hidQty06" runat="server" />
    <asp:HiddenField ID="hidQty07" runat="server" />
    <asp:HiddenField ID="hidQty08" runat="server" />
    <asp:HiddenField ID="hidQty09" runat="server" />
    <asp:HiddenField ID="hidQty10" runat="server" />
    <asp:HiddenField ID="hidQty11" runat="server" />
    <asp:HiddenField ID="hidQty12" runat="server" />
    <asp:HiddenField ID="hidQty13" runat="server" />
    <asp:HiddenField ID="hidQty14" runat="server" />
    <asp:HiddenField ID="hidQty15" runat="server" />
    <asp:HiddenField ID="hidQty16" runat="server" />
    <asp:HiddenField ID="hidQty17" runat="server" />

    <% if (valPreVal.Text == "tbl") { %>
    <style>
        .sizeHideTd {display: none;}
    </style>
    <% } %>

    <style>
        .minWidth {
            min-width:40px;
        }
        .tdMoney2 {
            width: 98%;
            text-align: right;
        }
    </style>

    <div class="container-fluid">

        <!-- Content Row -->
        <div class="row">

            <div class="card-body">
                <div class="table-responsive" runat="server" id="divBrand">
                    <table class="table table-bordered" width="100%" cellspacing="0">
                        <thead>
                        </thead>
                        <tbody>
                            <tr>
                                <th class="tdtitleW150">브랜드 선택</th>
                                <td class="td-checkbox">
                                    <asp:CheckBox ID="cboTBuk" runat="server" Text=" T-BUK 여부" />
                                </td>
                            </tr>
                        </tbody>                                        
                    </table>
                </div>
                <div style="height: 10px"></div>

                <div class="table-responsive">
                    <table class="table table-bordered" width="100%" cellspacing="0">
                        <thead>
                        </thead>
                        <tbody>
                            <tr>
                                <th class="tdtitleW150">AS요청스타일</th>
                                <td>
                                    <asp:TextBox ID="txtProduct" runat="server" autocomplete="off" CssClass="form-control inputProduct" Width="200" MaxLength="30" OnKeyDown="if(event.keyCode == 13){return CheckProduct();}" placeholder="스타일 입력후 엔터를 치세요."></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" Visible="false" />
                                </td>
                            </tr>
                        </tbody>                                        
                    </table>
                </div>
                <div style="height: 10px"></div>
                <div style="text-align: right">
                    <asp:Label ID="lblTotCount" runat="server"></asp:Label>
                </div>
                <div>
                    <asp:Panel runat="server" ID="pn01" GroupingText="발주이력조회">
                        <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable" id="selTable">
                    <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvMain_ItemDataBound" OnLayoutCreated="lvMain_LayoutCreated" onpagepropertieschanging="lvMain_PagePropertiesChanging">
                        <LayoutTemplate>
                            <tr class="defaultTitleTR">
                                <td width="40">NO</td>
                                <td>발주의뢰일자</td>
                                <td>스타일</td>
                                <td><asp:Literal ID="ltlSize1" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize2" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize3" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize4" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize5" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize6" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize7" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize8" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize9" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltlSize10" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></td>
                                <td class="sizeHideTd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></td>
                                <td>TOTAL</td>
                                <td>공급가액</td>
                                <td>부가세</td>
                                <td>합계금액</td>
                                <td>기타사항</td>
                            </tr>
                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                            <tr class="defaultSumTR">
                                <td colspan="3">합 계</td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum1" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum2" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum3" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum4" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum5" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum6" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum7" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum8" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum9" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSum10" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum11" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum12" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum13" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum14" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum15" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum16" runat="server"></asp:Literal></td>
                                <td class="tdMoney sizeHideTd"><asp:Literal ID="ltlSum17" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSumTotal" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSumNet" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSumVat" runat="server"></asp:Literal></td>
                                <td class="tdMoney"><asp:Literal ID="ltlSumHap" runat="server"></asp:Literal></td>
                                <td></td>
                            </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="defaultDataTR selData" style="cursor:pointer" 
                                onclick="selectStyle(this, '<%# Eval("Blju_date") %>','<%# Eval("Blju_times") %>','<%# Eval("Blju_sample") %>','<%# Eval("Blju_StyleNox") %>'
                                ,'<%# Eval("Blju_Qty01") %>','<%# Eval("Blju_Qty02") %>','<%# Eval("Blju_Qty03") %>','<%# Eval("Blju_Qty04") %>','<%# Eval("Blju_Qty05") %>','<%# Eval("Blju_Qty06") %>'
                                ,'<%# Eval("Blju_Qty07") %>','<%# Eval("Blju_Qty08") %>','<%# Eval("Blju_Qty09") %>','<%# Eval("Blju_Qty10") %>','<%# Eval("Blju_Qty11") %>','<%# Eval("Blju_Qty12") %>'
                                ,'<%# Eval("Blju_Qty13") %>','<%# Eval("Blju_Qty14") %>','<%# Eval("Blju_Qty15") %>','<%# Eval("Blju_Qty16") %>','<%# Eval("Blju_Qty17") %>'
                                );"
                                >
                                <td>
                                    <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                                </td>
                                <td><%# Eval("Blju_date") %> <%# Eval("Blju_times") %></td>
                                <td><%# Eval("Blju_styleNox") %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty01")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty02")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty03")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty04")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty05")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty06")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty07")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty08")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty09")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_Qty10")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty11")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty12")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty13")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty14")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty15")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty16")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Blju_Qty17")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Blju_QtyTotal")) %></td>
                                <td style="text-align: right; padding-right: 5px;">
                                    <%# GetAmountFormat(Eval("Blju_JustAmount")) %>
                                </td>
                                <td style="text-align: right; padding-right: 5px;">
                                    <asp:Literal ID="ltlVatAmount" runat="server"></asp:Literal>
                                </td>
                                <td style="text-align: right; padding-right: 5px;">
                                    <asp:Literal ID="ltlHapAmount" runat="server"></asp:Literal>
                                </td>
                                <td style="text-align: left; padding-left: 5px;">
                                    <%# Eval("Etc") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <tr class="defaultDataTR">
                                <td class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                            </tr>
                        </EmptyDataTemplate>
                    </asp:ListView>
                        </table>
                        
                        <div style="height:10px;"></div>

		                <div>
			                <table width="100%">
				                <tr>
					                <td align="center" valign="middle">
						                <asp:DataPager ID="dpMain" runat="server" PagedControlID="lvMain" PageSize="20" >
							                <Fields>
								                <asp:NextPreviousPagerField
								                ShowFirstPageButton="True"
								                ShowNextPageButton="False"
								                ShowPreviousPageButton="False"
								                ButtonCssClass="PrevNext"
								                FirstPageText="처&nbsp;&nbsp;&nbsp;음" />
								                <asp:NumericPagerField
									                PreviousPageText="&lt;"
									                NextPageText="&gt;"
									                ButtonCount="10"
									                NextPreviousButtonCssClass="PrevNext"
									                CurrentPageLabelCssClass="CurrentPage"
									                NumericButtonCssClass="PageNumber" />
								                <asp:NextPreviousPagerField
								                ShowLastPageButton="True"
								                ShowNextPageButton="False"
								                ShowPreviousPageButton="False"
								                ButtonCssClass="PrevNext"
									                />
							                </Fields>
					                    </asp:DataPager>					
					                </td>
				                </tr>
			                </table>	
		                </div>

                    </asp:Panel>
                </div>
                <div style="height: 10px"></div>

                <div id="offsetTable"></div>
                <div id="asTable" style="display:none">
                    <asp:Panel runat="server" ID="pn02" GroupingText="불량사유 및 수량 작성">
                        <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable" id="insTable">
                            <tr class="defaultTitleTR">
                                <td style="width:80px"></td>
                                <td style="width:100px">불량사유</td>
                                <td style="width:60px"><asp:Literal ID="ltlSize1" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize2" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize3" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize4" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize5" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize6" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize7" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize8" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize9" runat="server"></asp:Literal></td>
                                <td style="width:60px"><asp:Literal ID="ltlSize10" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></td>
                                <td style="width:60px">TOTAL</td>
                                <td style="width:180px">사용장소</td>
                                <td style="width:250px">사유</td>
                            </tr>
                            <tr class="defaultDataTR">
                                <td></td>
                                <td>총계</td>
                                <td class="minWidth"><input type="text" id="Size_Total01" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total02" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total03" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total04" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total05" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total06" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total07" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total08" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total09" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total10" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total11" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total12" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total13" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total14" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total15" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total16" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total17" class="tdMoney2 size" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_TotalSum" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <div style="height: 10px"></div>
                <div>
                    <div style="float: left; padding-left: 5px; width: 600px;">
                        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="defaultTable">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="upDetailView" runat="server">
                                        <ContentTemplate>
                                            <div style="position:absolute;padding-left: 200px;padding-top: 5px;font-size: 14px; display:none" id="divFileHelp">
                                                (이 영역에 파일을 드래그해주세요.)
                                            </div>
                                            <div>
                                                <asp:FileUpload runat="server" ID="Upload" AllowMultiple="true" Height="25px" Width="100%" BackColor="#bec8d2" />
                                                <asp:Label runat="server" ID="lblUploadFile"></asp:Label>
                                                <p></p>
                                                <asp:ListBox ID="lstFile" runat="server" Width="100%" AutoPostBack="true" CssClass="filelist"></asp:ListBox>
                                                <p></p>
                                                <asp:Button ID="btnFileOpen" runat="server" Text="파일열기" Visible="false" />
                                                <asp:Button ID="btnFileDownload" runat="server" Text="다운로드" />
                                                <asp:Button ID="btnFileRemove" runat="server" Text="삭제" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: right; padding-right: 15px; padding-top: 10px;">
                        <div style="margin-bottom:10px;">
                            <asp:Button ID="btnWrite" runat="server" Text="저 장" Width="100" CssClass="btn btn-primary btn-sm writebtn" OnClientClick="return CheckAS(0);" OnClick="btnWrite_Click" />
                        </div>
                        <div>
                            <asp:Button ID="btnComplete" runat="server" Text="최종본사전송" Width="100" CssClass="btn btn-info btn-sm writebtn" OnClientClick="return CheckAS(1);" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
