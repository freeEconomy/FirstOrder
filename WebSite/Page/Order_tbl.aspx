<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="Order_tbl.aspx.cs" Inherits="Page_Order_tbl" %>

<%@ Register Src="~/Control/Trader.ascx" TagName="Trader" TagPrefix="uc1" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>
<%@ Register Src="~/Control/CalendarDate.ascx" TagName="CalendarDate" TagPrefix="cal" %>
<%@ Register Src="~/Control/LayerProductList.ascx" TagName="LayerProductList" TagPrefix="lp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
        /*
        $(function () {
            $(".orderInput >input").on('keyup', function (e) {
                CalQty();
                setCommaHis(this);
                if (e.which === 13) {
                    var id = this.id;
                    var idnum = id.replace("ctl00_cph1_txtOrder", "");
                    idnum = Number(idnum) + 1;
                    $("#ctl00_cph1_txtOrder" + idnum).focus();
                }
            });
        });
        */

        function ChangeEnterTab(obj)
        {
            var id = obj.id;
            var idnum = id.replace("ctl00_cph1_txtOrder", "");
            idnum = Number(idnum) + 1;

            for (var i = 1; i < 17; i++) {
                if ($("#ctl00_cph1_txtOrder" + idnum).attr('disabled') == "disabled") {
                    idnum = Number(idnum) + 1;
                }
                else {
                    break;
                }
            }

            $("#ctl00_cph1_txtOrder" + idnum).focus();
        }

        function CalQty() {
            var jego1 = setCommaHis_Del(document.getElementById("<%=this.txtJego1.ClientID %>").value);
            var jego2 = setCommaHis_Del(document.getElementById("<%=this.txtJego2.ClientID %>").value);
            var jego3 = setCommaHis_Del(document.getElementById("<%=this.txtJego3.ClientID %>").value);
            var jego4 = setCommaHis_Del(document.getElementById("<%=this.txtJego4.ClientID %>").value);
            var jego5 = setCommaHis_Del(document.getElementById("<%=this.txtJego5.ClientID %>").value);
            var jego6 = setCommaHis_Del(document.getElementById("<%=this.txtJego6.ClientID %>").value);
            var jego7 = setCommaHis_Del(document.getElementById("<%=this.txtJego7.ClientID %>").value);
            var jego8 = setCommaHis_Del(document.getElementById("<%=this.txtJego8.ClientID %>").value);
            var jego9 = setCommaHis_Del(document.getElementById("<%=this.txtJego9.ClientID %>").value);
            var jego10 = setCommaHis_Del(document.getElementById("<%=this.txtJego10.ClientID %>").value);
            var jego11 = setCommaHis_Del(document.getElementById("<%=this.txtJego11.ClientID %>").value);
            var jego12 = setCommaHis_Del(document.getElementById("<%=this.txtJego12.ClientID %>").value);

            jego1 = GetEmptyNumber(jego1);
            jego2 = GetEmptyNumber(jego2);
            jego3 = GetEmptyNumber(jego3);
            jego4 = GetEmptyNumber(jego4);
            jego5 = GetEmptyNumber(jego5);
            jego6 = GetEmptyNumber(jego6);
            jego7 = GetEmptyNumber(jego7);
            jego8 = GetEmptyNumber(jego8);
            jego9 = GetEmptyNumber(jego9);
            jego10 = GetEmptyNumber(jego10);
            jego11 = GetEmptyNumber(jego11);
            jego12 = GetEmptyNumber(jego12);

            var order1 = setCommaHis_Del(document.getElementById("<%=this.txtOrder1.ClientID %>").value);
            var order2 = setCommaHis_Del(document.getElementById("<%=this.txtOrder2.ClientID %>").value);
            var order3 = setCommaHis_Del(document.getElementById("<%=this.txtOrder3.ClientID %>").value);
            var order4 = setCommaHis_Del(document.getElementById("<%=this.txtOrder4.ClientID %>").value);
            var order5 = setCommaHis_Del(document.getElementById("<%=this.txtOrder5.ClientID %>").value);
            var order6 = setCommaHis_Del(document.getElementById("<%=this.txtOrder6.ClientID %>").value);
            var order7 = setCommaHis_Del(document.getElementById("<%=this.txtOrder7.ClientID %>").value);
            var order8 = setCommaHis_Del(document.getElementById("<%=this.txtOrder8.ClientID %>").value);
            var order9 = setCommaHis_Del(document.getElementById("<%=this.txtOrder9.ClientID %>").value);
            var order10 = setCommaHis_Del(document.getElementById("<%=this.txtOrder10.ClientID %>").value);
            var order11 = setCommaHis_Del(document.getElementById("<%=this.txtOrder11.ClientID %>").value);
            var order12 = setCommaHis_Del(document.getElementById("<%=this.txtOrder12.ClientID %>").value);

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

            // 재고 체크            
            if (jego1 < order1) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder1.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder1.ClientID%>').val('0');
                $('#<%=this.txtOrder1.ClientID%>').focus();
            }
            if (jego2 < order2) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder2.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder2.ClientID%>').val('0');
                $('#<%=this.txtOrder2.ClientID%>').focus();
            }
            if (jego3 < order3) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder3.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder3.ClientID%>').val('0');
                $('#<%=this.txtOrder3.ClientID%>').focus();
            }
            if (jego4 < order4) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder4.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder4.ClientID%>').val('0');
                $('#<%=this.txtOrder4.ClientID%>').focus();
            }
            if (jego5 < order5) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder5.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder5.ClientID%>').val('0');
                $('#<%=this.txtOrder5.ClientID%>').focus();
            }
            if (jego6 < order6) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder6.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder6.ClientID%>').val('0');
                $('#<%=this.txtOrder6.ClientID%>').focus();
            }
            if (jego7 < order7) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder7.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder7.ClientID%>').val('0');
                $('#<%=this.txtOrder7.ClientID%>').focus();
            }
            if (jego8 < order8) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder8.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder8.ClientID%>').val('0');
                $('#<%=this.txtOrder8.ClientID%>').focus();
            }
            if (jego9 < order9) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder9.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder9.ClientID%>').val('0');
                $('#<%=this.txtOrder9.ClientID%>').focus();
            }
            if (jego10 < order10) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder10.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder10.ClientID%>').val('0');
                $('#<%=this.txtOrder10.ClientID%>').focus();
            }
            if (jego11 < order11) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder11.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder11.ClientID%>').val('0');
                $('#<%=this.txtOrder11.ClientID%>').focus();
            }
            if (jego12 < order12) {
                //alert('주문량이 재고 수량보다 많습니다.');
                showMessageToolTip('<%=this.txtOrder12.ClientID%>', '주문량이 재고 수량보다 많습니다.');
                $('#<%=this.txtOrder12.ClientID%>').val('0');
                $('#<%=this.txtOrder12.ClientID%>').focus();
            }

            var orderT = eval(order1) + eval(order2) + eval(order3) + eval(order4) + eval(order5) + eval(order6) + eval(order7) + eval(order8) + eval(order9) + eval(order10) + eval(order11) + eval(order12);
            
            orderT = GetEmptyNumber(orderT);

            //document.getElementById("<%=this.txtOrderTotal.ClientID%>").value = orderT;

            document.getElementById("<%=this.txtOrderTotal.ClientID%>").value = setCommaHisVal(orderT);


            //$('.writebtn').tooltip('hide')
            //.attr('data-original-title', "테스트")
            //.tooltip('show');
        }

        function GetEmptyNumber(val)
        {
            if (isNaN(val) || val == '') {
                val = 0;
            }
            return parseFloat(val);
        }

        function CheckBlju() {

            if (document.getElementById("<%=this.txtProduct.ClientID%>").value == "") {
                showMessageToolTip('<%=this.txtProduct.ClientID%>', '발주스타일을 선택해 주세요.');
                document.getElementById("<%=this.txtProduct.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=this.txtOrderTotal.ClientID%>").value == "") {
                showMessageToolTip('<%=this.txtOrderTotal.ClientID%>', '주문량을 입력해 주세요.');
                document.getElementById("<%=this.txtOrderTotal.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function CheckRefresh() {

            if (document.getElementById("<%=this.txtProduct.ClientID%>").value == "") {
                showMessageToolTip('<%=this.txtProduct.ClientID%>', '발주스타일을 선택해 주세요.');
                document.getElementById("<%=this.txtProduct.ClientID%>").focus();
                return false;
            }

            return true;
        }

        function CheckProduct(obj) {
            if (document.getElementById("<%=this.txtProduct.ClientID%>").value == "") {
                showMessageToolTip('<%=this.txtProduct.ClientID%>', '발주스타일을 선택해 주세요.');

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

        function CheckComplete()
        {
            if ($('#<%=this.hidBaeSong.ClientID%>').val() == "" || $('#<%=this.txtBaeSongName.ClientID%>').val() == "") {
                showMessageToolTip('<%=this.btnBaeSong.ClientID%>', '배송지를 선택해 주세요.');
                return false;
            }
            
            bljumin = eval($('#<%=this.hidBljuMin.ClientID%>').val());
            bljutotal = eval($('#<%=this.hidBljuTotal.ClientID%>').val());
            if (GetEmptyNumber(bljumin) > GetEmptyNumber(bljutotal))
            {
                alert("최소주문량이 " + eval($('#<%=this.hidBljuMin.ClientID%>').val()) + "개 이상이어야 합니다.");
                return false;
            }

            if (confirm('[주문완료]가 되어야 본사에서 배송준비를 합니다. 완료하시겠습니까?'))
            {
                return true;
            } else {
                return false;
            }
        }

        function GetProductPopup() {
            var searchValue = $("#<%=this.txtProduct.ClientID%>").val();

            var paramDate = $("#<%=this.txtDate.ClientID%>").val();
            var paramTime = $("#<%=this.txtTime.ClientID%>").val();
            var paramKure = $("#<%=this.hidKureCode.ClientID%>").val();

            OpenModal("/Page/StyleSelect.aspx?searchValue=" + searchValue + "&paramDate=" + paramDate + "&paramTime=" + paramTime + "&paramKure=" + paramKure, '스타일 선택창', '', '1100', '700');

            return false;
        }

        function DeleteCheckLine()
        {
            if ($(".orderTable tr").length == 3) {
                return confirm("해당 라인의 스타일을 '삭제'하시겠습니까?\n아울러, 발주 의뢰한 자료가 1건으로 '발주의뢰서' 자체도 삭제됩니다.");
            }
            else {
                return confirm("해당 라인의 스타일을 '삭제'하시겠습니까?");
            }
        }

        function OpenConfirmPage() {
            var today = new Date();
            var yesterday = new Date(today.valueOf() - (24 * 60 * 60 * 1000));
            var yyyy = yesterday.getFullYear().toString();
            var mm = (yesterday.getMonth() + 1).toString();
            var dd = yesterday.getDate().toString();

            var resultDate = yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);

            if (confirm("전(前) 일자로 '발주 중'인 것이 있습니다.\n계속하시겠습니까? 확인(계속), 취소(아니오)")) {

                location.href = "Order_tbl.aspx?mode=add&param_date=" + resultDate;
            }
            else {
                location.href = "Order_tbl.aspx?param_date=" + resultDate;
            }
        }

        //]]>
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="isSizeNum" Visible="false" runat="server"></asp:Label>
    
    <% if (isSizeNum.Text == "10") { %>
    <style>
        .sizeDiv {float: left;width: 9%;}
        .sizeHideDiv {display: none;}
        .sizeTotalDiv {float: left;width: 10%;}
        .sizeTdHide {display: none;}
    </style>
    <% } else { %>
    <style>
        .sizeDiv {float: left;width: 7.5%;}
        .sizeHideDiv {float: left;width: 7.5%;}
        .sizeTotalDiv {float: left;width: 10%;}
    </style>
    <% } %>
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hidKind" runat="server" />

            <asp:HiddenField ID="hidLine" runat="server" />
            <asp:HiddenField ID="hidTotal" runat="server" />
            <asp:HiddenField ID="hidTotalV" runat="server" />

            <asp:HiddenField ID="hidBljuMin" runat="server" />
            <asp:HiddenField ID="hidBljuTotal" runat="server" />

            <div class="container-fluid">

                <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
                    <asp:View ID="vwList" runat="server">

                        <div class="row">
                            <div class="card-body">
                                <asp:Panel runat="server" ID="Panel1" GroupingText="상세검색">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" width="100%" cellspacing="0">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="tdtitleW150">발주의뢰일자</th>
                                                        <td>
                                                            <cal:CalendarDate ID="ucBljuday" runat="server" />
                                                            ~
                                                            <cal:CalendarDate ID="ucBljuday1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="styleDiv">
                                                        <th>
                                                            <asp:Button ID="btnSelectProductList" runat="server" Text="발주 스타일" Width="110" CssClass="btn btn-info" OnClientClick="return GetProductList('nMo')" UseSubmitBehavior="false" />
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtProductList" runat="server" CssClass="form-control" Width="100" OnKeyDown="if(event.keyCode == 13){return GetProductList('Mo');}"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:TextBox ID="txtProductDetailList" runat="server" CssClass="form-control" Width="500px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div style="text-align: center">
                                            <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnClear" runat="server" Visible="false" Text="초기화" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="height: 10px"></div>
                                <asp:Panel runat="server" ID="Panel2" GroupingText="발주의뢰 현황">
                                    <div>
                                        <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvList_ItemDataBound" OnItemCommand="lvList_ItemCommand" OnPagePropertiesChanging="lvList_PagePropertiesChanging" >
                                            <LayoutTemplate>
                                                <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                                    <tr class="defaultTitleTR">
                                                        <td width="40">NO</td>
                                                        <td>발주의뢰일자</td>
                                                        <td>스타일</td>
                                                        <!--<td>발주업체</td>-->
                                                        <td>공급가액</td>
                                                        <td>부가세</td>
                                                        <td>합계금액</td>
                                                        <td>기타사항</td>
                                                        <td>최초등록일자</td>
                                                        <td>최종수정일자</td>
                                                        <td>배송방법</td>
                                                        <td>받는분</td>
                                                        <td>발주상태</td>
                                                        <td>메시지</td>
                                                        <td width="40">조회</td>
                                                        <td width="40">수정</td>
                                                        <td width="40">삭제</td>
                                                    </tr>
                                                    <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                                    <tr>
                                                        <td colspan="17" align="center" class="tdbottom">
                                                            <asp:DataPager ID="dpList" runat="server" PagedControlID="lvList" PageSize="15">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField
                                                                        ShowFirstPageButton="true"
                                                                        ShowNextPageButton="false"
                                                                        ShowPreviousPageButton="false"
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
                                                                        ShowLastPageButton="true"
                                                                        ShowNextPageButton="false"
                                                                        ShowPreviousPageButton="false"
                                                                        ButtonCssClass="PrevNext"
                                                                        LastPageText="마지막" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td <asp:Literal ID="ltlNumberStyle" runat="server"></asp:Literal> <asp:Literal ID="ltlOpenChat1" runat="server"></asp:Literal>>
                                                        <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                                                    </td>
                                                    <td <asp:Literal ID="ltlOpenChat2" runat="server"></asp:Literal>><%# Eval("Bjhd_Date") %> <%# Eval("Bjhd_Times") %></td>
                                                    <td <asp:Literal ID="ltlOpenChat3" runat="server"></asp:Literal>><asp:Literal ID="ltlStyleNox" runat="server"></asp:Literal></td>
                                                    <!--<td <asp:Literal ID="ltlOpenChat4" runat="server"></asp:Literal>><%# Eval("Bjhd_MainBuyer") %>(<%# Eval("Bjhd_MainBuyerNm") %>)</td>-->
                                                    <td style="text-align: right; padding-right: 5px;" <asp:Literal ID="ltlOpenChat5" runat="server"></asp:Literal>><%# GetAmountFormat(Eval("Bjhd_NetAmount")) %></td>
                                                    <td style="text-align: right; padding-right: 5px;" <asp:Literal ID="ltlOpenChat6" runat="server"></asp:Literal>><%# GetAmountFormat(Eval("Bjhd_VatAmount")) %></td>
                                                    <td style="text-align: right; padding-right: 5px;" <asp:Literal ID="ltlOpenChat7" runat="server"></asp:Literal>><%# GetAmountFormat(Eval("Bjhd_HapAmount")) %></td>
                                                    <td style="text-align: left; padding-left: 5px;" <asp:Literal ID="ltlOpenChat8" runat="server"></asp:Literal>><%# Eval("Bjhd_Remark") %></td>
                                                    <td <asp:Literal ID="ltlOpenChat9" runat="server"></asp:Literal>><%# Eval("Bjhd_CreateDate") %> <%# Eval("Bjhd_CreateSawon") %></td>
                                                    <td <asp:Literal ID="ltlOpenChat10" runat="server"></asp:Literal>><%# Eval("Bjhd_ModifyDate") %> <%# Eval("Bjhd_ModifySaWon") %></td>
                                                    <td <asp:Literal ID="ltlOpenChat11" runat="server"></asp:Literal>>
                                                        <asp:Literal ID="ltlBaesong" runat="server"></asp:Literal>
                                                    </td>
                                                    <td <asp:Literal ID="ltlOpenChat12" runat="server"></asp:Literal>>
                                                        <asp:Literal ID="ltlBaesongSend" runat="server"></asp:Literal>
                                                    </td>
                                                    <td <asp:Literal ID="ltlOpenChat13" runat="server"></asp:Literal>>
                                                        <asp:Literal ID="ltlBonsaCheck" runat="server"></asp:Literal>
                                                    </td>
                                                    <td style="cursor:pointer" <asp:Literal ID="ltlOpenChat14" runat="server"></asp:Literal>>
                                                        <asp:LinkButton runat="server" ID="lnkMessenger" CssClass="btn btn-primary btn-sm" style="color:#858796" BackColor="White" BorderStyle="None" BorderWidth="0">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkSubView" CssClass="btn btn-success btn-circle btn-sm" CommandName="subView">
                                                            <i class="fas fa-search"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkSubModify" CssClass="btn btn-success btn-circle btn-sm" CommandName="subModify">
                                                            <i class="fas fa-check"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkSubDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandName="subDelete" OnClientClick="return confirm('삭제된 자료는 복구되지 않습니다. 삭제 하시겠습니까 ?');">
                                                            <i class="fas fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                                    <tr class="defaultDataTR">
                                                        <td class="tdbottom" valign="middle" align="center">발주의뢰 내역이 없습니다.</td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vwWrite" runat="server">

                        <div style="float: right; padding-right: 15px; padding-top: 10px;">
                            <asp:LinkButton runat="server" ID="lnkList" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" OnClick="lnkList_Click">
                                <i class="fas fa-align-justify fa-sm text-white-50"></i> 리스트로 돌아가기
                            </asp:LinkButton>
                        </div>

                        <div style="clear: both"></div>

                        <div class="row">
                            <div class="card-body">
                                <asp:Panel runat="server" ID="pnInput" GroupingText="관리번호">
                                    <div class="row" style="display: flex;">
                                    <div class="col-md-8">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" width="100%" cellspacing="0">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="tdtitleW150">발주의뢰일자</th>
                                                        <td colspan="2" class="no-gutters">
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <th class="tdtitleW100">발주업체</th>
                                                        <td colspan="2">
                                                            <!--<uc1:Trader ID="ucTrader" runat="server" />//-->
                                                            <asp:TextBox ID="txtKure" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField ID="hidKureCode" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">
                                                            <asp:Button ID="btnBaeSong" runat="server" Text="배송지 선택" Width="110" CssClass="btn btn-info" />
                                                        </th>
                                                        <td class="no-gutters" colspan="5">
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtBaeSongOpt" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                                <asp:HiddenField ID="hidBaeSong" runat="server" />
                                                            </div>
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="txtBaeSongName" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">기타사항</th>
                                                        <td colspan="5">
                                                            <asp:TextBox ID="txtEtc" runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">공급가액</th>
                                                        <td style="width:200px">
                                                            <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <th class="tdtitleW100">부 가 세</th>
                                                        <td style="width:200px">
                                                            <asp:TextBox ID="txtVatAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <th class="tdtitleW100">합계금액</th>
                                                        <td>
                                                            <asp:TextBox ID="txtHapAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="height: 10px;"></div>
                                        <div class="table-responsive">
                                            <table class="table table-bordered" width="100%" cellspacing="0">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="tdtitleW150">
                                                            <asp:Button ID="btnSelectProduct" runat="server" Text="발주 스타일" Width="110" CssClass="btn btn-info" OnClientClick="return GetProductPopup()" UseSubmitBehavior="false" />
                                                            <div runat="server" id="divProductTitle"><asp:Literal ID="ltlProductTitle" runat="server" Text="발주 스타일"></asp:Literal></div>
                                                        </th>
                                                        <td class="no-gutters">
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control inputProduct" Width="95%" MaxLength="30" OnKeyDown="if(event.keyCode == 13){return GetProductPopup();}"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtProductDetail" runat="server" CssClass="form-control" Width="98%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">사이즈명</th>
                                                        <td class="no-gutters">
                                                            <div class="col-md-12 no-gutters">
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName1">
                                                                        <asp:Literal ID="ltlSize1" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName2">
                                                                        <asp:Literal ID="ltlSize2" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName3">
                                                                        <asp:Literal ID="ltlSize3" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName4">
                                                                        <asp:Literal ID="ltlSize4" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName5">
                                                                        <asp:Literal ID="ltlSize5" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName6">
                                                                        <asp:Literal ID="ltlSize6" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName7">
                                                                        <asp:Literal ID="ltlSize7" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName8">
                                                                        <asp:Literal ID="ltlSize8" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName9">
                                                                        <asp:Literal ID="ltlSize9" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <div class="sizeTitle sizeName10">
                                                                        <asp:Literal ID="ltlSize10" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeHideDiv">
                                                                    <div class="sizeTitle sizeName11">
                                                                        <asp:Literal ID="ltlSize11" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeHideDiv">
                                                                    <div class="sizeTitle sizeName12">
                                                                        <asp:Literal ID="ltlSize12" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="sizeTotalDiv">
                                                                    <div class="totalTitle">
                                                                        <asp:Literal ID="ltlSizeTotal" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">본사재고</th>
                                                        <td class="no-gutters">
                                                            <div class="col-md-12 no-gutters">
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego1" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego2" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego3" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego4" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego5" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego6" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego7" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego8" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego9" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv">
                                                                    <asp:TextBox ID="txtJego10" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeHideDiv">
                                                                    <asp:TextBox ID="txtJego11" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeHideDiv">
                                                                    <asp:TextBox ID="txtJego12" runat="server" CssClass="form-control align-right" Width="95%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeTotalDiv">
                                                                  <asp:TextBox ID="txtJegoTotal" runat="server" CssClass="form-control align-right btn-red" Width="100%" Enabled="false"></asp:TextBox>  
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">주 문 량</th>
                                                        <td class="no-gutters">
                                                            <div class="col-md-12 no-gutters ">
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder1" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder2" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder3" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder4" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder5" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder6" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder7" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder8" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder9" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder10" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeHideDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder11" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeHideDiv orderInput">
                                                                    <asp:TextBox ID="txtOrder12" runat="server" CssClass="form-control align-right" Width="95%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox>
                                                                </div>
                                                                <div class="sizeTotalDiv">
                                                                    <asp:TextBox ID="txtOrderTotal" runat="server" CssClass="form-control align-right btn-red" Width="100%" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:HiddenField ID="hidJustPrice" runat="server" />
                                                            <asp:HiddenField ID="hidJustAmount" runat="server" />
                                                            <asp:HiddenField ID="hidDnga1" runat="server" />
                                                            <asp:HiddenField ID="hidDnga2" runat="server" />
                                                            <asp:HiddenField ID="hidDnga3" runat="server" />
                                                            <asp:HiddenField ID="hidDnga4" runat="server" />
                                                            <asp:HiddenField ID="hidDnga5" runat="server" />
                                                            <asp:HiddenField ID="hidDnga6" runat="server" />
                                                            <asp:HiddenField ID="hidDnga7" runat="server" />
                                                            <asp:HiddenField ID="hidDnga8" runat="server" />
                                                            <asp:HiddenField ID="hidDnga9" runat="server" />
                                                            <asp:HiddenField ID="hidDnga10" runat="server" />
                                                            <asp:HiddenField ID="hidDnga11" runat="server" />
                                                            <asp:HiddenField ID="hidDnga12" runat="server" />
                                                            <asp:HiddenField ID="hidDngaLowPrice" runat="server" />
                                                            <asp:HiddenField ID="hidDngaJustPrice" runat="server" />
                                                            <asp:HiddenField ID="hidDngaBigSizePrice" runat="server" />
                                                            <asp:HiddenField ID="hidDngaSizeBoxQty" runat="server" />
                                                            <asp:HiddenField ID="hidDngaBoxQty" runat="server" />
                                                            <asp:HiddenField ID="hidDngaIpgoPrice" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="height: 10px;"></div>
                                        <div>
                                            <div style="float: left">
                                                <asp:Button ID="btnNewWrite" runat="server" Text="신 규" Width="110" CssClass="btn btn-info" OnClick="btnNewWrite_Click" />
                                                <asp:Button ID="btnRefresh" runat="server" Text="새로고침" Width="110" CssClass="btn btn-info" OnClick="btnRefresh_Click" OnClientClick="return CheckRefresh();" />
                                            </div>
                                            <div style="float: right">
                                                <asp:Button ID="btnWrite" runat="server" Text="저 장" Width="100" CssClass="btn btn-primary btn-sm writebtn" OnClick="btnWrite_Click" OnClientClick="return CheckBlju();" />
                                                <asp:Button ID="btnModify" runat="server" Text="수 정" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnModify_Click" OnClientClick="return CheckBlju();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="table-responsive" style="height:100%">
                                            <table class="table table-bordered" width="100%" cellspacing="0">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="tdtitleW150">최초등록일자</th>
                                                        <td>
                                                            <asp:TextBox ID="txtFirstDate" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">최종수정일자</th>
                                                        <td>
                                                            <asp:TextBox ID="txtLastDate" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <!-- 상품 이미지 업로드 테스트용
                                                      <asp:FileUpload ID="fu" runat="server" />
                                                      <asp:Button ID="btnUpload" runat="server" Text="업로드" OnClick="btnUpload_Click" />
                                                      -->
                                                            <div style="overflow: auto; overflow-x: hidden; height: 335px;">
                                                                <asp:Image runat="server" ID="imgProduct" Width="300" style="cursor:pointer" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                            <div style="position: absolute; bottom: 0px; right:12px;">
                                                <asp:LinkButton runat="server" ID="lnkChating" CssClass="d-none d-sm-inline-block btn btn-sm btn-info shadow-sm">
                                                    <i class="fas fa-comment-dots fa-sm text-white-50"></i> Q & A 대화방
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="lnkComplete" CssClass="d-none d-sm-inline-block btn btn-sm btn-success shadow-sm" OnClick="lnkComplete_Click" OnClientClick="return CheckComplete();">
                                                    <i class="fas fa-download fa-sm text-white-50"></i> 주문완료 후 종료
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </asp:Panel>
                                <div style="height: 10px"></div>
                                <asp:Panel runat="server" ID="pnList" GroupingText="발주의뢰 내역">
                                    <div>
                                        <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph"
                                            OnItemDataBound="lvMain_ItemDataBound" OnItemCommand="lvMain_ItemCommand" OnLayoutCreated="lvMain_LayoutCreated">
                                            <LayoutTemplate>
                                                <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable orderTable">
                                                    <tr class="defaultTitleTR">
                                                        <td width="40">NO</td>
                                                        <td style="width: 100px;">STYLE</td>
                                                        <td>S/T NAME</td>
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
                                                        <td class="sizeTdHide"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                                        <td class="sizeTdHide"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                                        <td style="width: 80px;">TOTAL</td>
                                                        <td>단가</td>
                                                        <td>금액</td>
                                                        <td width="40">수정</td>
                                                        <td width="40">삭제</td>
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
                                                        <td class="tdMoney sizeTdHide"><asp:Literal ID="ltlSum11" runat="server"></asp:Literal></td>
                                                        <td class="tdMoney sizeTdHide"><asp:Literal ID="ltlSum12" runat="server"></asp:Literal></td>
                                                        <td class="tdMoney"><asp:Literal ID="ltlSumTotal" runat="server"></asp:Literal></td>
                                                        <td></td>
                                                        <td class="tdMoney"><asp:Literal ID="ltlSumMoney" runat="server"></asp:Literal></td>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td><%# Eval("Blju_Line") %></td>
                                                    <td><%# Eval("Blju_StyleNox") %></td>
                                                    <td style="text-align: left; padding-left: 5px;"><%# Eval("Blju_StyleName") %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty01")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty02")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty03")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty04")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty05")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty06")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty07")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty08")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty09")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty10")) %></td>
                                                    <td class="tdMoney sizeTdHide"><%# GetAmountFormat(Eval("Blju_Qty11")) %></td>
                                                    <td class="tdMoney sizeTdHide"><%# GetAmountFormat(Eval("Blju_Qty12")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_QtyTotal")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_JustPrice")) %></td>
                                                    <td class="tdMoney">
                                                        <%# GetAmountFormat(Eval("Blju_JustAmount")) %>
                                                        <asp:Literal ID="ltlJustAmount" runat="server" Text='<%# Eval("Blju_JustAmount") %>' Visible="false"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkSubModify" CssClass="btn btn-success btn-circle btn-sm" CommandName="subModify">
                                                    <i class="fas fa-check"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkSubDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandName="subDelete" OnClientClick="return DeleteCheckLine();">
                                                    <i class="fas fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </asp:View>
                    <asp:View ID="vwView" runat="server">

                        <div style="float: right; padding-right: 15px; padding-top: 10px;">
                            <asp:LinkButton runat="server" ID="lnkList2" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" OnClick="lnkList_Click">
                                <i class="fas fa-align-justify fa-sm text-white-50"></i> 리스트로 돌아가기
                            </asp:LinkButton>

                            <asp:LinkButton runat="server" ID="lnkList3" Visible="false" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" OnClientClick="location.href='/Page/Order_tbl.aspx'; return false;">
                                <i class="fas fa-align-justify fa-sm text-white-50"></i> 발주의뢰 현황조회 리스트
                            </asp:LinkButton>

                            <asp:LinkButton runat="server" ID="lnkList4" Visible="false" CssClass="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm" OnClientClick="location.href='/Page/KureSpecList.aspx?hgubun=back'; return false;">
                                <i class="fas fa-align-justify fa-sm text-white-50"></i> 거래명세서 현황조회 리스트
                            </asp:LinkButton>
                        </div>

                        <div style="clear: both"></div>

                        <div class="row">
                            <div class="card-body">
                                <asp:Panel runat="server" ID="Panel3" GroupingText="관리번호">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-bordered" width="100%" cellspacing="0">
                                                <thead>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th class="tdtitleW150">발주의뢰일자</th>
                                                        <td colspan="2" class="no-gutters" style="min-width: 200px;">
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtDateV" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtTimeV" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <th class="tdtitleW150">발주업체</th>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="txtKureV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField ID="hidKureCodeV" runat="server" />
                                                        </td>
                                                        <th class="tdtitleW150">최초등록일자</th>
                                                        <td style="min-width: 200px;">
                                                            <asp:TextBox ID="txtFirstDateV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">
                                                            배송지
                                                        </th>
                                                        <td class="no-gutters" colspan="4">
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtBaeSongOptV" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-10">
                                                                <asp:TextBox ID="txtBaeSongNameV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <th class="tdtitleW150">
                                                            운송장번호
                                                        </th>
                                                        <td class="no-gutters" colspan="2">
                                                            <asp:TextBox ID="txtSongJangNoxV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">기타사항</th>
                                                        <td colspan="5">
                                                            <asp:TextBox ID="txtEtcV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <th class="tdtitleW150">최종수정일자</th>
                                                        <td>
                                                            <asp:TextBox ID="txtLastDateV" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th class="tdtitleW150">공급가액</th>
                                                        <td style="min-width: 120px;">
                                                            <asp:TextBox ID="txtNetAmountV" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <th class="tdtitleW150">부 가 세</th>
                                                        <td style="min-width: 120px;">
                                                            <asp:TextBox ID="txtVatAmountV" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <th class="tdtitleW150">합계금액</th>
                                                        <td style="min-width: 120px;">
                                                            <asp:TextBox ID="txtHapAmountV" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:LinkButton runat="server" ID="lnkKureSpecPrintV" CssClass="d-none d-sm-inline-block btn btn-sm btn-info shadow-sm">
                                                                <i class="fas fa-print fa-sm text-white-50"></i> 거래명세표 출력
                                                            </asp:LinkButton>
                                                            &nbsp;&nbsp;
                                                            <asp:LinkButton runat="server" ID="lnkChatingV" CssClass="d-none d-sm-inline-block btn btn-sm btn-info shadow-sm">
                                                                <i class="fas fa-comment-dots fa-sm text-white-50"></i> Q & A 대화방
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="height: 10px"></div>
                                <asp:Panel runat="server" ID="Panel4" GroupingText="발주의뢰 내역">
                                    <div>
                                        <asp:ListView ID="lvView" runat="server" ItemPlaceholderID="iph" OnLayoutCreated="lvView_LayoutCreated">
                                            <LayoutTemplate>
                                                <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                                    <tr class="defaultTitleTR">
                                                        <td width="40">NO</td>
                                                        <td style="width: 100px;">STYLE</td>
                                                        <td>S/T NAME</td>
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
                                                        <td class="sizeTdHide"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                                        <td class="sizeTdHide"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                                        <td style="width: 80px;">TOTAL</td>
                                                        <td>단가</td>
                                                        <td>금액</td>
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
                                                        <td class="tdMoney sizeTdHide"><asp:Literal ID="ltlSum11" runat="server"></asp:Literal></td>
                                                        <td class="tdMoney sizeTdHide"><asp:Literal ID="ltlSum12" runat="server"></asp:Literal></td>
                                                        <td class="tdMoney"><asp:Literal ID="ltlSumTotal" runat="server"></asp:Literal></td>
                                                        <td></td>
                                                        <td class="tdMoney"><asp:Literal ID="ltlSumMoney" runat="server"></asp:Literal></td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td><%# Eval("Blju_Line") %></td>
                                                    <td><%# Eval("Blju_StyleNox") %></td>
                                                    <td style="text-align: left; padding-left: 5px;"><%# Eval("Blju_StyleName") %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty01")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty02")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty03")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty04")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty05")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty06")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty07")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty08")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty09")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_Qty10")) %></td>
                                                    <td class="tdMoney sizeTdHide"><%# GetAmountFormat(Eval("Blju_Qty11")) %></td>
                                                    <td class="tdMoney sizeTdHide"><%# GetAmountFormat(Eval("Blju_Qty12")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_QtyTotal")) %></td>
                                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_JustPrice")) %></td>
                                                    <td class="tdMoney">
                                                        <%# GetAmountFormat(Eval("Blju_JustAmount")) %>
                                                        <asp:Literal ID="ltlJustAmount" runat="server" Text='<%# Eval("Blju_JustAmount") %>' Visible="false"></asp:Literal>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>

            </div>

            <div>
                <lp:LayerProductList ID="LayerProductList" runat="server" />
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
