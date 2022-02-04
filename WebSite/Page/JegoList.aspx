<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="JegoList.aspx.cs" Inherits="Page_JegoList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/Trader.ascx" TagName="Trader" TagPrefix="uc1" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	      
        function Search_CHK() {

            if (document.getElementById("<%=this.ucTrader.ClientID%>_ddlTrader").value == "" && document.getElementById("<%=this.ucProduct.ClientID%>_ddlProduct").value == "")
            {
                //alert("대리점 혹은 스타일을 선택해주세요.");
                //return false;
            }

            return true;
        }

        function ViewMainBuyer(KureDaePyo, KureAddr, KureTel, KurePhone, rowIndex) {
            
            $('#mainbuyerview').html('');

            var items = [];
            var html = '';

            items.push('<table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">');
            items.push('<tr class="defaultTitleTR">');
            items.push('<td style="text-align:center;">대표자</td>');
            items.push('<td style="padding-left:5px;text-align:left; background-color: #FFFFFF;">' + KureDaePyo + '</td>');
            items.push('</tr>');
            items.push('<tr class="defaultTitleTR">');
            items.push('<td style="text-align:center;">주소</td>');
            items.push('<td style="padding-left:5px;text-align:left; background-color: #FFFFFF;">' + KureAddr + '</td>');
            items.push('</tr>');
            items.push('<tr class="defaultTitleTR">');
            items.push('<td style="text-align:center;">연락처</td>');
            items.push('<td style="padding-left:5px;text-align:left; background-color: #FFFFFF;">' + KureTel + '</td>');
            items.push('</tr>');
            items.push('<tr class="defaultTitleTR">');
            items.push('<td style="text-align:center;">담당휴대폰</td>');
            items.push('<td style="padding-left:5px;text-align:left; background-color: #FFFFFF;">' + KurePhone + '</td>');
            items.push('</tr>');
            items.push('</table>');

            html = items.join('');
            $('#mainbuyerview').html($(html));

            var rowHeight = 388 + (22 * rowIndex);

            rowHeight = rowHeight + "px";

            $("#draggable").css({ top: rowHeight, left: '310px', width: '550px', height: '220px', padding: '0.4em 0.5em 0.5em 0.5em' });
            
            $("#draggable").show();
            $("#draggable").draggable();
            return false;
        }

        function layerClose() {
            $("#draggable").hide();
            return false;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="valPreVal" Visible="false" runat="server"></asp:Label>
    <asp:Label ID="isSizeNum" Visible="false" runat="server"></asp:Label>
    
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

    <div id="draggable" class="ui-widget-content" style="position:absolute;z-index:999; display:none">
        <div id="layerSearch" style="vertical-align:top;">
            <div id="divClose" style="float:right; vertical-align:top; height:23px;">
                <asp:ImageButton ID="imgClose" runat="server" Width="13px" AlternateText="닫기" ImageUrl="~/images/btnpopClose.gif" OnClientClick="return layerClose();" />
            </div>
            <table border="0" id="mainbuyerview" width="100%" cellspacing="0" class="tablelineno" cellpadding="0" bgcolor="#FFFFFF">
		    </table>
        </div>
        <div style="padding:5px;">
        </div>
    </div>

    <div class="container-fluid">

        <!-- Content Row -->
        <div class="row">

            <div class="card-body">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <table class="table table-bordered" width="100%" cellspacing="0">
                                <thead>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th class="tdtitleW150">대리점</th>
                                        <td>
                                            <uc1:Trader ID="ucTrader" runat="server" />
                                        </td>
                                        <th class="tdtitleW150">스타일</th>
                                        <td>
                                            <uc2:Product ID="ucProduct" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" style="height: 30px">
                                            <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" OnClientClick="return Search_CHK()" />
                                            <br /><br />
                                            <asp:HyperLink ID="hylPdf" runat="server" NavigateUrl="/pdfjs-dist/web/viewer.html" Target="_blank">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="/images/JegoPdf.jpg" />
                                            </asp:HyperLink>
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
                            <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvMain_ItemDataBound" OnLayoutCreated="lvMain_LayoutCreated">
                                <LayoutTemplate>
                                    <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                        <tr class="defaultTitleTR">
                                            <td width="50">번호</td>
                                            <td>대리점</td>
                                            <td>스타일번호</td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize1" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize2" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize3" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize4" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize5" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize6" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize7" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize8" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize9" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;"><asp:Literal ID="ltlSize10" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd sizeHideDiv"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideTd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">재고수량</td>
                                            <td style="width: 80px;">재고일자</td>
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
                                            <td></td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                        <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkMainBuyer" runat="server"></asp:LinkButton>
                                            </td>
                                            <td><%# Eval("Jego_StyleNox") %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty01")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty02")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty03")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty04")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty05")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty06")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty07")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty08")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty09")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_Qty10")) %></td>
                                            <td class="tdMoney sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty11")) %></td>
                                            <td class="tdMoney sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty12")) %></td>
                                            <td class="tdMoney sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty13")) %></td>
                                            <td class="tdMoney sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty14")) %></td>
                                            <td class="tdMoney sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty15")) %></td>
                                            <td class="tdMoney sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty16")) %></td>
                                            <td class="tdMoney sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty17")) %></td>
                                            <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_QtyTotal")) %></td>
                                            <td><%# Eval("JEGO_CreateDate") %></td>
                                        </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                        <tr class="defaultDataTR">
                                            <td class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    
</asp:Content>
