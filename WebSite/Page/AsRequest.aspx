<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="AsRequest.aspx.cs" Inherits="Page_AsRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        //<![CDATA[
	    
        $(document).ready(function () {

            //$("#<%=this.Upload.ClientID%>").on("change", handleImgsFilesSelect);

            var mode = $("#<%=this.hidMode.ClientID%>").val();

            if (mode == "") {
                insRow();
                console.log(11);
            }
            else {
                FileList();
            }

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

                UploadFiles();
            });
        });

        function UploadFiles() {
            var fname = "";
            var fileUpload = $('#<%= Upload.ClientID %>').get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            var paramDate = $("#<%=this.hidAsDate.ClientID%>").val();
            var paramTimes = $("#<%=this.hidAsTimes.ClientID%>").val();
            var paramMainbuyer = $("#<%=this.hidAsMainbuyer.ClientID%>").val();
            var paramSample = $("#<%=this.hidAsSample.ClientID%>").val();
            var ioTable = $("#<%=this.hidIoTable.ClientID%>").val();

            $.ajax({
                url: "/Handler/FileUploadHandler.ashx?date=" + paramDate + "&times=" + paramTimes + "&mainbuyer=" + paramMainbuyer + "&sample=" + paramSample + "&ioTable=" + ioTable,
                type: "POST",
                data: data,
                async: false,
                contentType: false,
                processData: false,
                success: function (result) {
                    fname = result;

                    // 파일 리스트 변경
                    FileList();
                },
                error: function (err, o, e) {
                    //alert(err.status + ":" + o + ":" + e + ":" + err.responseText);
                }
            });

            return fname;
        }

        function FileList() {
            
            var mode = $("#<%=this.hidMode.ClientID%>").val();
            var paramDate = $("#<%=this.hidAsDate.ClientID%>").val();
            var paramTimes = $("#<%=this.hidAsTimes.ClientID%>").val();
            var paramMainbuyer = $("#<%=this.hidAsMainbuyer.ClientID%>").val();
            var paramSample = $("#<%=this.hidAsSample.ClientID%>").val();

            $(".imgs_wrap").html('');

            $.ajax({ //검색결과 바인딩
                type: "POST",
                url: "/Handler/WebService_common.asmx/GetAsFileList",
                data: "{'paramDate':'" + paramDate + "','paramTimes':'" + paramTimes + "','paramMainbuyer':'" + paramMainbuyer + "','paramSample':'" + paramSample + "'}",
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    var items = [];
                    var html = '';
                    var data = $.parseJSON(msg.d);
                    var totCnt = data.length;

                    if (totCnt > 0) {
                        $.each(data, function (key, val) {
                            items.push('<div class=\"img-list\">');
                            items.push('    <div class=\"img-view\">');

                            items.push('        <img src=\"/Upload/AS/' + val.bnpmImageFile + '\" onclick=\"zoomViewImage(\'as\',\'' + val.bnpmImageFile + '\',\'' + val.width + '\',\'' + val.height + '\');\" />');
                            items.push('    </div>');
                            if (mode == "edit") {
                                items.push('    <div class=\"img-cancel\">');
                                items.push('        <img src=\"/img/imgDelete.gif\" title=\"삭제\" onclick=\"DeleteFile(\'' + val.bnpmFileLine + '\');\" />');
                                items.push('    </div>');
                            }
                            items.push('</div>');
                        });
                    }

                    if (totCnt > 0) {
                        html = items.join('');
                        $(".imgs_wrap").append($(html));
                    }

                    $("#hidFileCount").val(totCnt);
                }
            });
        }

        function DeleteFile(line) {
            
            var data = new FormData();
            var paramDate = $("#<%=this.hidAsDate.ClientID%>").val();
            var paramTimes = $("#<%=this.hidAsTimes.ClientID%>").val();
            var paramMainbuyer = $("#<%=this.hidAsMainbuyer.ClientID%>").val();
            var paramSample = $("#<%=this.hidAsSample.ClientID%>").val();
            var ioTable = $("#<%=this.hidIoTable.ClientID%>").val();

            $.ajax({
                url: "/Handler/FileUploadHandler.ashx?date=" + paramDate + "&times=" + paramTimes + "&mainbuyer=" + paramMainbuyer + "&sample=" + paramSample + "&line=" + line + "&delChk=1&ioTable=" + ioTable,
                type: "POST",
                data: data,
                async: false,
                contentType: false,
                processData: false,
                success: function (result) {
                    // 파일 리스트 변경
                    FileList();
                },
                error: function (err, o, e) {
                    //alert(err.status + ":" + o + ":" + e + ":" + err.responseText);
                }
            });
        }

        function CheckProduct() {
            var searchValue = $("#<%=this.txtProduct.ClientID%>").val();

            if (searchValue == "") {
                alert("스타일을 입력해주세요.");
                $("#<%=this.txtProduct.ClientID%>").focus();
                return false;
            }

            <%= Page.GetPostBackEventReference(btnSearch) %>
        }

        function EmptyMessage() {
            var searchValue = $("#<%=this.txtProduct.ClientID%>").val();
            alert(searchValue + "는 없는 스타일입니다. 다시 입력해주세요.");
            $('#<%=this.txtProduct.ClientID%>').focus();
            $('#<%=this.txtProduct.ClientID%>').select();
        }
        
        function ChangeZeroEmpty(val) {
            if (val == "0")
            {
                val = "";
            }
            return val;
        }

        function selectStyle(obj, date, times, sample, style, BnpmDQty01, BnpmDQty02, BnpmDQty03, BnpmDQty04, BnpmDQty05, BnpmDQty06, BnpmDQty07, BnpmDQty08, BnpmDQty09, BnpmDQty10, BnpmDQty11, BnpmDQty12, BnpmDQty13, BnpmDQty14, BnpmDQty15, BnpmDQty16, BnpmDQty17, BnpmDQtyTotal) {

            $("#<%=this.txtStyle.ClientID%>").val(style);

            $("#<%=this.hidBljuDate.ClientID%>").val(date);
            $("#<%=this.hidBljuTimes.ClientID%>").val(times);
            $("#<%=this.hidBljuSample.ClientID%>").val(sample);
            $("#<%=this.hidBljuStyle.ClientID%>").val(style);
            $("#<%=this.Blju_Qty01.ClientID%>").val(ChangeZeroEmpty(BnpmDQty01));
            $("#<%=this.Blju_Qty02.ClientID%>").val(ChangeZeroEmpty(BnpmDQty02));
            $("#<%=this.Blju_Qty03.ClientID%>").val(ChangeZeroEmpty(BnpmDQty03));
            $("#<%=this.Blju_Qty04.ClientID%>").val(ChangeZeroEmpty(BnpmDQty04));
            $("#<%=this.Blju_Qty05.ClientID%>").val(ChangeZeroEmpty(BnpmDQty05));
            $("#<%=this.Blju_Qty06.ClientID%>").val(ChangeZeroEmpty(BnpmDQty06));
            $("#<%=this.Blju_Qty07.ClientID%>").val(ChangeZeroEmpty(BnpmDQty07));
            $("#<%=this.Blju_Qty08.ClientID%>").val(ChangeZeroEmpty(BnpmDQty08));
            $("#<%=this.Blju_Qty09.ClientID%>").val(ChangeZeroEmpty(BnpmDQty09));
            $("#<%=this.Blju_Qty10.ClientID%>").val(ChangeZeroEmpty(BnpmDQty10));
            $("#<%=this.Blju_Qty11.ClientID%>").val(ChangeZeroEmpty(BnpmDQty11));
            $("#<%=this.Blju_Qty12.ClientID%>").val(ChangeZeroEmpty(BnpmDQty12));
            $("#<%=this.Blju_Qty13.ClientID%>").val(ChangeZeroEmpty(BnpmDQty13));
            $("#<%=this.Blju_Qty14.ClientID%>").val(ChangeZeroEmpty(BnpmDQty14));
            $("#<%=this.Blju_Qty15.ClientID%>").val(ChangeZeroEmpty(BnpmDQty15));
            $("#<%=this.Blju_Qty16.ClientID%>").val(ChangeZeroEmpty(BnpmDQty16));
            $("#<%=this.Blju_Qty17.ClientID%>").val(ChangeZeroEmpty(BnpmDQty17));
            $("#<%=this.Blju_QtyTotal.ClientID%>").val(ChangeZeroEmpty(BnpmDQtyTotal));

            $("#selTable .selData").css("background-color", "#FFF");
            obj.style.backgroundColor = '#9ecef7'
            $("#asTableTop").show();
            $("#asTable").show();
            $("#asTableFoot").show();

            var offset = $("#offsetTable").offset();
            $('html, body').animate({ scrollTop: offset.top - 100 });

            $('.asRow').remove();

            $("input[id*=Size_Total]").each(function () {
                $(this).val('');
            });

            $('#hidTotal').val(0);
            insRow();
            console.log(22);
        }

        function SetTBuc() {
            $('#hidTotal').val(0);
            //insRow();
        }

	    function insRow() {
	        var rowNum = $('#hidTotal').val();
	        
	        var bnpmSelect = '<%=valBnpmSelect.Text%>';

		    var items = [];
		    var html = '';
		    items.push('<tr class="defaultDataTR asRow" id="asRow_' + rowNum + '">');
		    items.push('    <td>');
		    items.push('        <input type="hidden" name="rowNum" value="' + rowNum + '" />');
            items.push('        <a style="cursor:pointer" onclick="insRow();"><i class="fa fa-plus" aria-hidden="true"></i></a> ');
            items.push('        &nbsp;&nbsp;<a style="cursor:pointer" onclick="delRow(' + rowNum + ');"><i class="fa fa-minus" aria-hidden="true"></i></a> ');
            items.push('    </td>');
            items.push('    <td>');
            items.push('        <select name="BnpmD_BnpmCode_' + rowNum + '" class="BnpmCode">');
            items.push('        ' + bnpmSelect + '');
            items.push('        </select>');
            items.push('    </td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty01_' + rowNum + '" name="BnpmD_Qty01_' + rowNum + '" class="tdMoney2 size_01" value="" onkeyup="CalQty(\'01\',' + rowNum + ');" onfocus="CalQty(\'01\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty02_' + rowNum + '" name="BnpmD_Qty02_' + rowNum + '" class="tdMoney2 size_02" value="" onkeyup="CalQty(\'02\',' + rowNum + ');" onfocus="CalQty(\'02\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty03_' + rowNum + '" name="BnpmD_Qty03_' + rowNum + '" class="tdMoney2 size_03" value="" onkeyup="CalQty(\'03\',' + rowNum + ');" onfocus="CalQty(\'03\',' + rowNum + ');" onfocus="CalQty(\'03\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty04_' + rowNum + '" name="BnpmD_Qty04_' + rowNum + '" class="tdMoney2 size_04" value="" onkeyup="CalQty(\'04\',' + rowNum + ');" onfocus="CalQty(\'04\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty05_' + rowNum + '" name="BnpmD_Qty05_' + rowNum + '" class="tdMoney2 size_05" value="" onkeyup="CalQty(\'05\',' + rowNum + ');" onfocus="CalQty(\'05\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty06_' + rowNum + '" name="BnpmD_Qty06_' + rowNum + '" class="tdMoney2 size_06" value="" onkeyup="CalQty(\'06\',' + rowNum + ');" onfocus="CalQty(\'06\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty07_' + rowNum + '" name="BnpmD_Qty07_' + rowNum + '" class="tdMoney2 size_07" value="" onkeyup="CalQty(\'07\',' + rowNum + ');" onfocus="CalQty(\'07\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty08_' + rowNum + '" name="BnpmD_Qty08_' + rowNum + '" class="tdMoney2 size_08" value="" onkeyup="CalQty(\'08\',' + rowNum + ');" onfocus="CalQty(\'08\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty09_' + rowNum + '" name="BnpmD_Qty09_' + rowNum + '" class="tdMoney2 size_09" value="" onkeyup="CalQty(\'09\',' + rowNum + ');" onfocus="CalQty(\'09\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty10_' + rowNum + '" name="BnpmD_Qty10_' + rowNum + '" class="tdMoney2 size_10" value="" onkeyup="CalQty(\'10\',' + rowNum + ');" onfocus="CalQty(\'10\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="BnpmD_Qty11_' + rowNum + '" name="BnpmD_Qty11_' + rowNum + '" class="tdMoney2 size_11" value="" onkeyup="CalQty(\'11\',' + rowNum + ');" onfocus="CalQty(\'11\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="BnpmD_Qty12_' + rowNum + '" name="BnpmD_Qty12_' + rowNum + '" class="tdMoney2 size_12" value="" onkeyup="CalQty(\'12\',' + rowNum + ');" onfocus="CalQty(\'12\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty13_' + rowNum + '" name="BnpmD_Qty13_' + rowNum + '" class="tdMoney2 size_13" value="" onkeyup="CalQty(\'13\',' + rowNum + ');" onfocus="CalQty(\'13\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty14_' + rowNum + '" name="BnpmD_Qty14_' + rowNum + '" class="tdMoney2 size_14" value="" onkeyup="CalQty(\'14\',' + rowNum + ');" onfocus="CalQty(\'14\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty15_' + rowNum + '" name="BnpmD_Qty15_' + rowNum + '" class="tdMoney2 size_15" value="" onkeyup="CalQty(\'15\',' + rowNum + ');" onfocus="CalQty(\'15\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty16_' + rowNum + '" name="BnpmD_Qty16_' + rowNum + '" class="tdMoney2 size_16" value="" onkeyup="CalQty(\'16\',' + rowNum + ');" onfocus="CalQty(\'16\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty17_' + rowNum + '" name="BnpmD_Qty17_' + rowNum + '" class="tdMoney2 size_17" value="" onkeyup="CalQty(\'17\',' + rowNum + ');" onfocus="CalQty(\'17\',' + rowNum + ');" /></td>');
            items.push('    <td class="minWidth"><input type="text" id="BnpmD_QtyTotal_' + rowNum + '" name="BnpmD_QtyTotal_' + rowNum + '" class="tdMoney2" value="" disabled="disabled" /></td>');
            items.push('    <td><input type="text" id="BnpmD_UsedRemark_' + rowNum + '" name="BnpmD_UsedRemark_' + rowNum + '" style="width: 170px;" maxlength="30" value="" /></td>');
            items.push('    <td><input type="text" id="BnpmD_ReasonRemark_' + rowNum + '" name="BnpmD_ReasonRemark_' + rowNum + '" style="width: 250px;" maxlength="30" value="" /></td>');
            items.push('</tr>');
            
		    html = items.join('');

		    $('#insTable').append($(html));

		    rowNum++;

		    $('#hidTotal').val(rowNum);
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

	        var isCheck = "";
	        var idVal = '<%=this.cboTBuc.ClientID%>';
	        if ($("input:checkbox[id='" + idVal + "']").is(":checked") == true) {
	            isCheck = "1";
	        }

	        var bnpmCode = $("#BnpmD_Code_" + rowNum).val();
	        if (bnpmCode == "") {
	            showMessageToolTip('BnpmD_Code_' + rowNum, '불량사유를 선택해주셔야 합니다.');
	            $('#BnpmD_Qty' + num + '_' + rowNum).val('0');
	            $('#BnpmD_Qty' + num + '_' + rowNum).focus();
	        }

	        var order1 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty01.ClientID%>").val()));
	        var order2 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty02.ClientID%>").val()));
	        var order3 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty03.ClientID%>").val()));
	        var order4 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty04.ClientID%>").val()));
	        var order5 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty05.ClientID%>").val()));
	        var order6 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty06.ClientID%>").val()));
	        var order7 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty07.ClientID%>").val()));
	        var order8 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty08.ClientID%>").val()));
	        var order9 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty09.ClientID%>").val()));
	        var order10 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty10.ClientID%>").val()));
	        var order11 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty11.ClientID%>").val()));
	        var order12 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty12.ClientID%>").val()));
	        var order13 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty13.ClientID%>").val()));
	        var order14 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty14.ClientID%>").val()));
	        var order15 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty15.ClientID%>").val()));
	        var order16 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty16.ClientID%>").val()));
	        var order17 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Blju_Qty17.ClientID%>").val()));

	        var req1 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty01_" + rowNum).val()));
	        var req2 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty02_" + rowNum).val()));
	        var req3 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty03_" + rowNum).val()));
	        var req4 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty04_" + rowNum).val()));
	        var req5 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty05_" + rowNum).val()));
	        var req6 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty06_" + rowNum).val()));
	        var req7 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty07_" + rowNum).val()));
	        var req8 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty08_" + rowNum).val()));
	        var req9 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty09_" + rowNum).val()));
	        var req10 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty10_" + rowNum).val()));
	        var req11 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty11_" + rowNum).val()));
	        var req12 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty12_" + rowNum).val()));
	        var req13 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty13_" + rowNum).val()));
	        var req14 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty14_" + rowNum).val()));
	        var req15 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty15_" + rowNum).val()));
	        var req16 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty16_" + rowNum).val()));
	        var req17 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty17_" + rowNum).val()));

	        // 세로 계산1
            var sumVal = 0;
            $(".size_" + num).each(function () {
                sumVal += eval(GetEmptyNumber($(this).val()));
            });
            $("input[id*=Size_Total" + num + "]").each(function () {
                $(this).val(sumVal);
            });

            var reqT1 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total01.ClientID%>").val()));
	        var reqT2 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total02.ClientID%>").val()));
	        var reqT3 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total03.ClientID%>").val()));
	        var reqT4 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total04.ClientID%>").val()));
	        var reqT5 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total05.ClientID%>").val()));
	        var reqT6 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total06.ClientID%>").val()));
	        var reqT7 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total07.ClientID%>").val()));
	        var reqT8 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total08.ClientID%>").val()));
	        var reqT9 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total09.ClientID%>").val()));
	        var reqT10 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total10.ClientID%>").val()));
	        var reqT11 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total11.ClientID%>").val()));
	        var reqT12 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total12.ClientID%>").val()));
	        var reqT13 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total13.ClientID%>").val()));
	        var reqT14 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total14.ClientID%>").val()));
	        var reqT15 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total15.ClientID%>").val()));
	        var reqT16 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total16.ClientID%>").val()));
	        var reqT17 = GetEmptyNumber(setCommaHis_Del($("#<%=this.Size_Total17.ClientID%>").val()));

	        // 수량 체크 (T-BUK는 패스 필요)
            if (isCheck == "") {
                var checkCount = false;
                if (order1 < reqT1) {
                    //showMessageToolTip('BnpmD_Qty01_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty01_' + rowNum).val('0');
                    //$('#BnpmD_Qty01_' + rowNum).focus();
                    //req1 = 0;
                    checkCount = true;
                }
                if (order2 < reqT2) {
                    //showMessageToolTip('BnpmD_Qty02_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty02_' + rowNum).val('0');
                    //$('#BnpmD_Qty02_' + rowNum).focus();
                    //req2 = 0;
                    checkCount = true;
                }
                if (order3 < reqT3) {
                    //showMessageToolTip('BnpmD_Qty03_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty03_' + rowNum).val('0');
                    //$('#BnpmD_Qty03_' + rowNum).focus();
                    //req3 = 0;
                    checkCount = true;
                }
                if (order4 < reqT4) {
                    //showMessageToolTip('BnpmD_Qty04_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty04_' + rowNum).val('0');
                    //$('#BnpmD_Qty04_' + rowNum).focus();
                    //req4 = 0;
                    checkCount = true;
                }
                if (order5 < reqT5) {
                    //showMessageToolTip('BnpmD_Qty05_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty05_' + rowNum).val('0');
                    //$('#BnpmD_Qty05_' + rowNum).focus();
                    //req5 = 0;
                    checkCount = true;
                }
                if (order6 < reqT6) {
                    //showMessageToolTip('BnpmD_Qty06_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty06_' + rowNum).val('0');
                    //$('#BnpmD_Qty06_' + rowNum).focus();
                    //req6 = 0;
                    checkCount = true;
                }
                if (order7 < reqT7) {
                    //showMessageToolTip('BnpmD_Qty07_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty07_' + rowNum).val('0');
                    //$('#BnpmD_Qty07_' + rowNum).focus();
                    //req7 = 0;
                    checkCount = true;
                }
                if (order8 < reqT8) {
                    //showMessageToolTip('BnpmD_Qty08_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty08_' + rowNum).val('0');
                    //$('#BnpmD_Qty08_' + rowNum).focus();
                    //req8 = 0;
                    checkCount = true;
                }
                if (order9 < reqT9) {
                    //showMessageToolTip('BnpmD_Qty09_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty09_' + rowNum).val('0');
                    //$('#BnpmD_Qty09_' + rowNum).focus();
                    //req9 = 0;
                    checkCount = true;
                }
                if (order10 < reqT10) {
                    //showMessageToolTip('BnpmD_Qty10_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty10_' + rowNum).val('0');
                    //$('#BnpmD_Qty10_' + rowNum).focus();
                    //req10 = 0;
                    checkCount = true;
                }
                if (order11 < reqT11) {
                    //showMessageToolTip('BnpmD_Qty11_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty11_' + rowNum).val('0');
                    //$('#BnpmD_Qty11_' + rowNum).focus();
                    //req11 = 0;
                    checkCount = true;
                }
                if (order12 < reqT12) {
                    //showMessageToolTip('BnpmD_Qty12_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty12_' + rowNum).val('0');
                    //$('#BnpmD_Qty12_' + rowNum).focus();
                    //req12 = 0;
                    checkCount = true;
                }
                if (order13 < reqT13) {
                    //showMessageToolTip('BnpmD_Qty13_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty13_' + rowNum).val('0');
                    //$('#BnpmD_Qty13_' + rowNum).focus();
                    //req13 = 0;
                    checkCount = true;
                }
                if (order14 < reqT14) {
                    //showMessageToolTip('BnpmD_Qty14_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty14_' + rowNum).val('0');
                    //$('#BnpmD_Qty14_' + rowNum).focus();
                    //req14 = 0;
                    checkCount = true;
                }
                if (order15 < reqT15) {
                    //showMessageToolTip('BnpmD_Qty15_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty15_' + rowNum).val('0');
                    //$('#BnpmD_Qty15_' + rowNum).focus();
                    //req15 = 0;
                    checkCount = true;
                }
                if (order16 < reqT16) {
                    //showMessageToolTip('BnpmD_Qty16_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty16_' + rowNum).val('0');
                    //$('#BnpmD_Qty16_' + rowNum).focus();
                    //req16 = 0;
                    checkCount = true;
                }
                if (order17 < reqT17) {
                    //showMessageToolTip('BnpmD_Qty17_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    //$('#BnpmD_Qty17_' + rowNum).val('0');
                    //$('#BnpmD_Qty17_' + rowNum).focus();
                    //req17 = 0;
                    checkCount = true;
                }

                if (checkCount) {
                    showMessageToolTip('BnpmD_Qty' + num + '_' + rowNum, '발주 수량보다 클 수 없습니다.');
                    $('#BnpmD_Qty' + num + '_' + rowNum).val('0');
                    $('#BnpmD_Qty' + num + '_' + rowNum).focus();
                }
            }


	        // 세로 계산1-2
            var sumVal = 0;
            $(".size_" + num).each(function () {
                sumVal += eval(GetEmptyNumber($(this).val()));
            });
            $("input[id*=Size_Total" + num + "]").each(function () {
                $(this).val(sumVal);
            });

	        // 세로 계산2
            var sumVal = 0;
            $(".size_" + num).each(function () {
                sumVal += eval(GetEmptyNumber($(this).val()));
            });
            $("#Size_Total" + num).val(sumVal);


	        // 가로 계산2
            var req1 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty01_" + rowNum).val()));
            var req2 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty02_" + rowNum).val()));
            var req3 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty03_" + rowNum).val()));
            var req4 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty04_" + rowNum).val()));
            var req5 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty05_" + rowNum).val()));
            var req6 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty06_" + rowNum).val()));
            var req7 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty07_" + rowNum).val()));
            var req8 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty08_" + rowNum).val()));
            var req9 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty09_" + rowNum).val()));
            var req10 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty10_" + rowNum).val()));
            var req11 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty11_" + rowNum).val()));
            var req12 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty12_" + rowNum).val()));
            var req13 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty13_" + rowNum).val()));
            var req14 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty14_" + rowNum).val()));
            var req15 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty15_" + rowNum).val()));
            var req16 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty16_" + rowNum).val()));
            var req17 = GetEmptyNumber(setCommaHis_Del($("#BnpmD_Qty17_" + rowNum).val()));
            var reqT = eval(req1) + eval(req2) + eval(req3) + eval(req4) + eval(req5) + eval(req6) + eval(req7) + eval(req8) + eval(req9) + eval(req10) + eval(req11) + eval(req12) + eval(req13) + eval(req14) + eval(req15) + eval(req16) + eval(req17);

            reqT = GetEmptyNumber(reqT);

            $("#BnpmD_QtyTotal_" + rowNum).val(setCommaHisVal(reqT));

            var sumTotal = 0;

            $("input[id^=BnpmD_QtyTotal_]").each(function () {
                sumTotal += eval(GetEmptyNumber($(this).val()));
            });
            $("#<%=this.Size_TotalSum.ClientID%>").val(sumTotal);
        }

        function CheckAS(chk) {

            var duple = true;
            var preCode = "";
            $("select[name^=BnpmD_BnpmCode_]").each(function (i) {

                var thisCode = $(this).val();

                if (thisCode == "") {
                    alert("불량사유를 모두 선택하셔야 합니다.");
                    $(this).focus();
                    duple = false;
                    return false;
                }

                var k = 0;
                $("select[name^=BnpmD_BnpmCode_]").each(function (i) {
                    if (thisCode == $(this).val()) {
                        k = k + 1;
                    }
                });

                if (k > 1) {
                    alert("동일한 불량사유가 2개 이상 선택되었습니다.");
                    $(this).focus();
                    duple = false;
                    return false;
                }
            });

            if (duple) {
                var check = GetEmptyNumber($("#<%=this.Size_TotalSum.ClientID%>").val());
                var buttonId = "";
                if (chk == 0) {
                    buttonId = '<%=this.btnSave.ClientID%>';
                }
                else if (chk == 1) {
                    buttonId = '<%=this.btnComplete.ClientID%>';

                    var filecount = $("#hidFileCount").val();
                    if (filecount == 0) {
                        showMessageToolTip(buttonId, '파일을 첨부하셔야 합니다.');
                        return false;
                    }
                }

                if (check == 0) {
                    showMessageToolTip(buttonId, '접수 요청한 수량이 없습니다.');
                    return false;
                }
                
                $("#<%=this.hidBonsaCheck.ClientID%>").val(chk);
            }
            else {
                return false;
            }

            if (chk == 1) {
                return confirm("최종 전송하시겠습니까?");
            }
            else {
                return true;
            }
        }

        function BindAsDetailList(mode, paramDate, paramTimes, paramMainbuyer, paramSample) {
            
            //$('#insTable').html('');

            $.ajax({ //검색결과 바인딩
                type: "POST",
                url: "/Handler/WebService_common.asmx/GetAsDetailList",
                data: "{'paramDate':'" + paramDate + "','paramTimes':'" + paramTimes + "','paramMainbuyer':'" + paramMainbuyer + "','paramSample':'" + paramSample + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (msg) {

                    var bnpmSelect = '<%=valBnpmSelect.Text%>';

                    var items = [];
                    var html = '';
                    var data = $.parseJSON(msg.d);
                    var totCnt = data.length;

                    var rowNum = 0;
                    
                    var enabled = "";

                    if (mode == "view") {
                        enabled = "disabled=\"disabled\"";
                    }

                    if (totCnt > 0) {
                        $.each(data, function (key, val) {
                            
                            items.push('<tr class="defaultDataTR asRow" id="asRow_' + rowNum + '">');
                            items.push('    <td>');
                            if (mode == "view") {
                                items.push('        ' + (Number(rowNum) + 1) + '');
                            }
                            else {
                                items.push('        <input type="hidden" name="rowNum" value="' + rowNum + '" />');
                                items.push('        <a style="cursor:pointer" onclick="insRow();"><i class="fa fa-plus" aria-hidden="true"></i></a> ');
                                items.push('        &nbsp;&nbsp;<a style="cursor:pointer" onclick="delRow(' + rowNum + ');"><i class="fa fa-minus" aria-hidden="true"></i></a> ');
                            }
                            items.push('    </td>');
                            items.push('    <td>');
                            items.push('        <select name="BnpmD_BnpmCode_' + rowNum + '" class="BnpmCode" ' + enabled + '>');
                            items.push('        ' + bnpmSelect + '');
                            items.push('        </select>');
                            items.push('        <input type="hidden" id="hid_BnpmCode_' + rowNum + '" value="' + val.bnpmCode + '" />');
                            items.push('    </td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty01_' + rowNum + '" name="BnpmD_Qty01_' + rowNum + '" class="tdMoney2 size_01" value="' + val.bnpmQty01 + '" onkeyup="CalQty(\'01\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty02_' + rowNum + '" name="BnpmD_Qty02_' + rowNum + '" class="tdMoney2 size_02" value="' + val.bnpmQty02 + '" onkeyup="CalQty(\'02\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty03_' + rowNum + '" name="BnpmD_Qty03_' + rowNum + '" class="tdMoney2 size_03" value="' + val.bnpmQty03 + '" onkeyup="CalQty(\'03\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty04_' + rowNum + '" name="BnpmD_Qty04_' + rowNum + '" class="tdMoney2 size_04" value="' + val.bnpmQty04 + '" onkeyup="CalQty(\'04\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty05_' + rowNum + '" name="BnpmD_Qty05_' + rowNum + '" class="tdMoney2 size_05" value="' + val.bnpmQty05 + '" onkeyup="CalQty(\'05\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty06_' + rowNum + '" name="BnpmD_Qty06_' + rowNum + '" class="tdMoney2 size_06" value="' + val.bnpmQty06 + '" onkeyup="CalQty(\'06\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty07_' + rowNum + '" name="BnpmD_Qty07_' + rowNum + '" class="tdMoney2 size_07" value="' + val.bnpmQty07 + '" onkeyup="CalQty(\'07\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty08_' + rowNum + '" name="BnpmD_Qty08_' + rowNum + '" class="tdMoney2 size_08" value="' + val.bnpmQty08 + '" onkeyup="CalQty(\'08\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty09_' + rowNum + '" name="BnpmD_Qty09_' + rowNum + '" class="tdMoney2 size_09" value="' + val.bnpmQty09 + '" onkeyup="CalQty(\'09\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_Qty10_' + rowNum + '" name="BnpmD_Qty10_' + rowNum + '" class="tdMoney2 size_10" value="' + val.bnpmQty10 + '" onkeyup="CalQty(\'10\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="BnpmD_Qty11_' + rowNum + '" name="BnpmD_Qty11_' + rowNum + '" class="tdMoney2 size_11" value="' + val.bnpmQty11 + '" onkeyup="CalQty(\'11\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="BnpmD_Qty12_' + rowNum + '" name="BnpmD_Qty12_' + rowNum + '" class="tdMoney2 size_12" value="' + val.bnpmQty12 + '" onkeyup="CalQty(\'12\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty13_' + rowNum + '" name="BnpmD_Qty13_' + rowNum + '" class="tdMoney2 size_13" value="' + val.bnpmQty13 + '" onkeyup="CalQty(\'13\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty14_' + rowNum + '" name="BnpmD_Qty14_' + rowNum + '" class="tdMoney2 size_14" value="' + val.bnpmQty14 + '" onkeyup="CalQty(\'14\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty15_' + rowNum + '" name="BnpmD_Qty15_' + rowNum + '" class="tdMoney2 size_15" value="' + val.bnpmQty15 + '" onkeyup="CalQty(\'15\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty16_' + rowNum + '" name="BnpmD_Qty16_' + rowNum + '" class="tdMoney2 size_16" value="' + val.bnpmQty16 + '" onkeyup="CalQty(\'16\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth sizeHideTd"><input type="text" id="BnpmD_Qty17_' + rowNum + '" name="BnpmD_Qty17_' + rowNum + '" class="tdMoney2 size_17" value="' + val.bnpmQty17 + '" onkeyup="CalQty(\'17\',' + rowNum + ');" ' + enabled + ' /></td>');
                            items.push('    <td class="minWidth"><input type="text" id="BnpmD_QtyTotal_' + rowNum + '" name="BnpmD_QtyTotal_' + rowNum + '" class="tdMoney2" value="' + val.bnpmQtyTotal + '" disabled="disabled" /></td>');
                            if (mode == "view") {
                                items.push('    <td>' + val.bnpmUsedRemark + '</td>');
                                items.push('    <td>' + val.bnpmReasonRemark + '</td>');
                            } else {
                                items.push('    <td><input type="text" id="BnpmD_UsedRemark_' + rowNum + '" name="BnpmD_UsedRemark_' + rowNum + '" style="width: 170px;" maxlength="30" value="' + val.bnpmUsedRemark + '" /></td>');
                                items.push('    <td><input type="text" id="BnpmD_ReasonRemark_' + rowNum + '" name="BnpmD_ReasonRemark_' + rowNum + '" style="width: 250px;" maxlength="30" value="' + val.bnpmReasonRemark + '" /></td>');
                            }
                            items.push('</tr>');

                            rowNum++;

                            $('#hidTotal').val(rowNum);
                        });
                    }

                    if (totCnt > 0) {
                        html = items.join('');
                        $('#insTable').append($(html));
                    }
                    else {
                        $('#hidTotal').val(0);
                        insRow();
                        console.log(44);
                    }

                    $("select[name^=BnpmD_BnpmCode_]").each(function (i) {
                        var temp = $(this).attr('name');
                        var num = temp.replace('BnpmD_BnpmCode_', '');
                        $(this).val($("#hid_BnpmCode_" + num).val());
                    });
                }
            });
        }

        var sel_files = [];

        function handleImgsFilesSelect(e) {
            var files = e.target.files;
            var filesArr = Array.prototype.slice.call(files);

            filesArr.forEach(function (f) {
                if (!f.type.match("image.*")) {
                    alert("확장자는 이미지 확장자만 가능합니다.");
                    return;
                }

                sel_files.push(f);

                var reader = new FileReader();
                reader.onload = function (e) {
                    var img_html = "<img src=\"" + e.target.result + "\" />";
                    $(".imgs_wrap").append(img_html);
                }
                reader.readAsDataURL(f);
            });

            $("#hidFiles").val(sel_files);
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="valPreVal" Visible="false" runat="server"></asp:Label>
    <asp:Label ID="isSizeNum" Visible="false" runat="server"></asp:Label>

    <asp:Label ID="valBnpmSelect" Visible="false" runat="server"></asp:Label>
    
    <asp:HiddenField ID="hidAsMainbuyer" runat="server" />
    <asp:HiddenField ID="hidAsDate" runat="server" />
    <asp:HiddenField ID="hidAsTimes" runat="server" />
    <asp:HiddenField ID="hidAsSample" runat="server" />
    
    <asp:HiddenField ID="hidIoTable" runat="server" />

    <asp:HiddenField ID="hidBljuDate" runat="server" />
    <asp:HiddenField ID="hidBljuTimes" runat="server" />
    <asp:HiddenField ID="hidBljuSample" runat="server" />
    <asp:HiddenField ID="hidBljuStyle" runat="server" />

    <asp:HiddenField ID="hidBonsaCheck" runat="server" />

    <asp:HiddenField ID="hidMode" runat="server" />

    <input id="hidFiles" name="hidFiles" type="hidden" />

    <input id="hidTotal" type="hidden" value="0" />
    <input id="hidFileCount" type="hidden" value="0" />

    <% if (valPreVal.Text == "tbl") { %>
    <style>
        .sizeHideTd {display: none;}
    </style>
        <% if (isSizeNum.Text == "12") { %>
        <style>
            .sizeHideDiv {display: table-cell;}
        </style>
        <% } %>
    <% } %>

    <style>
        .minWidth {
            min-width:40px;
        }
        .tdMoney2 {
            width: 98%;
            text-align: right;
        }

        .imgs_wrap {
            background: #fff;
            padding: 0 0 0 0;
            overflow-x: auto;
            max-height: 437px;
        }

        .imgs_wrap img {
            float: left;
            max-width: 190px;
            margin-right: 5px;
            margin-bottom: 5px;
        }

        .img-list {
            
        }

        .img-view img{
            cursor: pointer;
        }

        .img-cancel {
            position: relative;
            left: -21px;
        }

        .img-cancel img {
            box-shadow: -3px 3px 3px 0px #000;
            cursor: pointer;
        }

        .as-history {
            overflow-x: auto;
            max-height: 460px;
        }

    </style>

    <div class="container-fluid">

        <!-- Content Row -->
        <div class="row">

            <div class="card-body">
                <div runat="server" id="divReg">
                    <div class="table-responsive" runat="server" id="divBrand">
                        <table class="table table-bordered" width="100%" cellspacing="0">
                            <thead>
                            </thead>
                            <tbody>
                                <tr>
                                    <th class="tdtitleW150">브랜드 선택</th>
                                    <td class="td-checkbox">
                                        <asp:CheckBox ID="cboTBuc" runat="server" Text=" T-BUC 여부" />
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
                    <div id="asTableTop">
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
                                    <td class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                    <td class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
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
                                    <td class="tdMoney sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSum11" runat="server"></asp:Literal></td>
                                    <td class="tdMoney sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSum12" runat="server"></asp:Literal></td>
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
                                    ,'<%# Eval("Blju_Qty13") %>','<%# Eval("Blju_Qty14") %>','<%# Eval("Blju_Qty15") %>','<%# Eval("Blju_Qty16") %>','<%# Eval("Blju_Qty17") %>','<%# Eval("Blju_QtyTotal") %>'
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
                                    <td class="tdMoney minWidth sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Blju_Qty11")) %></td>
                                    <td class="tdMoney minWidth sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Blju_Qty12")) %></td>
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
                </div>

                <div id="offsetTable"></div>
                <div id="asTable" <asp:Literal ID="ltlView" runat="server"></asp:Literal>>
                    <asp:Panel runat="server" ID="pn02" GroupingText="불량사유 및 수량 작성">
                        <div style="height: 35px; font-size: 14px;">
                            선택된 스타일 : <input type="text" id="txtStyle" runat="server" value="" style="width:100px" disabled="disabled" />
                        </div>
                        <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable" id="insTable">
                            <tr class="defaultTitleTR">
                                <td style="width:80px">
                                    <asp:Literal ID="ltlNumber" runat="server" Text="No"></asp:Literal>
                                </td>
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
                                <td style="width:60px" class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></td>
                                <td style="width:60px" class="sizeHideTd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></td>
                                <td style="width:60px">TOTAL</td>
                                <td style="width:180px">사용장소</td>
                                <td style="width:250px">사유</td>
                            </tr>
                            <tr class="defaultDataTR" runat="server" id="bljuHistory">
                                <td></td>
                                <td>발주내역</td>
                                <td class="minWidth"><input type="text" id="Blju_Qty01" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty02" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty03" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty04" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty05" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty06" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty07" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty08" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty09" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_Qty10" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="Blju_Qty11" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="Blju_Qty12" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Blju_Qty13" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Blju_Qty14" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Blju_Qty15" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Blju_Qty16" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Blju_Qty17" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Blju_QtyTotal" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr class="defaultDataTR">
                                <td></td>
                                <td>총계</td>
                                <td class="minWidth"><input type="text" id="Size_Total01" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total02" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total03" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total04" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total05" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total06" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total07" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total08" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total09" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_Total10" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="Size_Total11" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd sizeHideDiv"><input type="text" id="Size_Total12" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total13" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total14" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total15" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total16" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth sizeHideTd"><input type="text" id="Size_Total17" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td class="minWidth"><input type="text" id="Size_TotalSum" runat="server" class="tdMoney2" value="" disabled="disabled" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                        <div id="empTable"></div>
                    </asp:Panel>
                </div>
                <div style="height: 10px"></div>
                <div id="asTableFoot" class="row" <asp:Literal ID="ltlViewFoot" runat="server"></asp:Literal>>
                    <div class="col-md-5">
                        <asp:Panel runat="server" ID="Panel2" GroupingText="A/S의뢰 사진내역">
                            <div runat="server" id="divFileEdit">
                                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="defaultTable">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upDetailView" runat="server">
                                                <ContentTemplate>
                                                    <div style="background-color: #fff; padding: 10px;">
                                                        <asp:FileUpload runat="server" ID="Upload" AllowMultiple="true" Height="25px" Width="100%" BackColor="#bec8d2" />
                                                        <asp:Label runat="server" ID="lblUploadFile"></asp:Label>
                                                        <div runat="server" id="fileMargin" style="height:10px"></div>
                                                        <div class="imgs_wrap"></div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div runat="server" id="divFileView">
                                <table width="100%" border="0" cellpadding="1" cellspacing="1" class="defaultTableNoTopLine">
                                    <tr class="defaultTitleTR" id="fileTR" runat="server">
                                        <td class="tdtitle">첨부파일</td>
                                    </tr>
                                    <tr class="defaultDataTR">
                                        <td style="text-align: left; padding: 10px;">
                                            <asp:Repeater ID="rptDownload" runat="server" OnItemCommand="rptDownload_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDownload" runat="server" Text='<%# Eval("bnpmX_imageFileName") %>' CommandArgument='<%# Eval("bnpmX_imageFileName") %>' CommandName="DownloadFile" />
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-5" style="padding: 0;">
                        <asp:Panel runat="server" ID="Panel1" GroupingText="A/S처리 내역">
                            <div class="as-history">
                                <asp:ListView ID="lvResult" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvResult_ItemDataBound">
                                    <LayoutTemplate>
                            	        <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                            <tr class="defaultTitleTR">
                                                <td style="width: 40px;">순번</td>
                                                <td style="width: 40px;">번호</td>
                                                <td style="width: 85px;">A/S사유</td>
                                                <td style="width: 55px;">싸이즈</td>
                                                <td style="width: 55px;">A/S수량</td>
                                                <td style="width: 35px;">수리</td>
                                                <td style="width: 35px;">반품</td>
                                                <td style="width: 35px;">교환</td>
                                                <td style="width: 35px;">반려</td>
                                                <td style="width: 35px;">기타</td>
                                                <td style="">처리내역</td>
                                            </tr>
                                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                            <tr class="defaultTitleTR">
                                                <td colspan="5">Total</td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResult01" runat="server"></asp:Literal></td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResult02" runat="server"></asp:Literal></td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResult03" runat="server"></asp:Literal></td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResult04" runat="server"></asp:Literal></td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResult05" runat="server"></asp:Literal></td>
                                                <td style="text-align: right; padding-right: 5px;"><asp:Literal ID="ltlResultTotal" runat="server"></asp:Literal></td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="defaultDataTR">
                                            <td><%# Eval("BnpmR_Line") %></td>
                                            <td><%# Eval("BnpmR_LineSeqx") %></td>
                                            <td><%# Eval("BnpmR_ResonName") %></td>
                                            <td><%# Eval("BnpmR_SizeName") %></td>
                                            <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("BnpmR_AsQty")) %></td>
                                            <td><asp:Literal ID="ltlResult01" runat="server"></asp:Literal></td>
                                            <td><asp:Literal ID="ltlResult02" runat="server"></asp:Literal></td>
                                            <td><asp:Literal ID="ltlResult03" runat="server"></asp:Literal></td>
                                            <td><asp:Literal ID="ltlResult04" runat="server"></asp:Literal></td>
                                            <td><asp:Literal ID="ltlResult05" runat="server"></asp:Literal></td>
                                            <td style="text-align: left; padding-left: 5px;">
                                                <%# Eval("BnpmR_ResultRemark") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-2" style="text-align:right">
                        <div style="margin-bottom:10px;">
                            <asp:Button ID="btnSave" runat="server" Text="저 장" Width="100" CssClass="btn btn-primary btn-sm" OnClientClick="return CheckAS(0);" OnClick="btnSave_Click" />
                            <asp:Button ID="btnEdit" runat="server" Text="수 정" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnList" runat="server" Text="리스트" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnList_Click" />
                        </div>
                        <div>
                            <asp:Button ID="btnComplete" runat="server" Text="최종본사전송" Width="100" CssClass="btn btn-info btn-sm" OnClientClick="return CheckAS(1);" OnClick="btnSave_Click" />
                        </div>
                    </div>
                    <div class="col-md-10" style="margin-top: 10px;">
                        <table class="defaultTable" width="100%" cellspacing="0">
                            <tbody>
                                <tr>
                                    <th class="tdtitleW150">기타사항</th>
                                    <td>
                                        <asp:TextBox ID="txtEtc" runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>                                        
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
