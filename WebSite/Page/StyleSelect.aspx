<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="StyleSelect.aspx.cs" Inherits="Page_StyleSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
        
        // 스타일 정보 넘기기
        function SetProduct(StyleNox, MainName, SubName, SpecColor, JegoQty01, JegoQty02, JegoQty03, JegoQty04, JegoQty05, JegoQty06, JegoQty07, JegoQty08, JegoQty09, JegoQty10, JegoQty11, JegoQty12, JegoQty13, JegoQty14, JegoQty15, JegoQty16, JegoQty17, JegoTotal, IsDuple, Msg, imgWidth, imgHeight, sizeName1, sizeName2, sizeName3, sizeName4, sizeName5, sizeName6, sizeName7, sizeName8, sizeName9, sizeName10, sizeName11, sizeName12, sizeName13, sizeName14, sizeName15, sizeName16, sizeName17, Kind) {
            if (IsDuple != "")
            {
                alert(IsDuple + "번 라인에 해당스타일이 이미 등록되어 있습니다!");
            }
            else
            {
                parent.$('#ctl00_cph1_txtJego1').val(JegoQty01);
                parent.$('#ctl00_cph1_txtJego2').val(JegoQty02);
                parent.$('#ctl00_cph1_txtJego3').val(JegoQty03);
                parent.$('#ctl00_cph1_txtJego4').val(JegoQty04);
                parent.$('#ctl00_cph1_txtJego5').val(JegoQty05);
                parent.$('#ctl00_cph1_txtJego6').val(JegoQty06);
                parent.$('#ctl00_cph1_txtJego7').val(JegoQty07);
                parent.$('#ctl00_cph1_txtJego8').val(JegoQty08);
                parent.$('#ctl00_cph1_txtJego9').val(JegoQty09);
                parent.$('#ctl00_cph1_txtJego10').val(JegoQty10);
                parent.$('#ctl00_cph1_txtJego11').val(JegoQty11);
                parent.$('#ctl00_cph1_txtJego12').val(JegoQty12);
                parent.$('#ctl00_cph1_txtJego13').val(JegoQty13);
                parent.$('#ctl00_cph1_txtJego14').val(JegoQty14);
                parent.$('#ctl00_cph1_txtJego15').val(JegoQty15);
                parent.$('#ctl00_cph1_txtJego16').val(JegoQty16);
                parent.$('#ctl00_cph1_txtJego17').val(JegoQty17);
                parent.$('#ctl00_cph1_txtJegoTotal').val(JegoTotal);
        
                parent.$('#ctl00_cph1_txtOrder1').val('');
                parent.$('#ctl00_cph1_txtOrder2').val('');
                parent.$('#ctl00_cph1_txtOrder3').val('');
                parent.$('#ctl00_cph1_txtOrder4').val('');
                parent.$('#ctl00_cph1_txtOrder5').val('');
                parent.$('#ctl00_cph1_txtOrder6').val('');
                parent.$('#ctl00_cph1_txtOrder7').val('');
                parent.$('#ctl00_cph1_txtOrder8').val('');
                parent.$('#ctl00_cph1_txtOrder9').val('');
                parent.$('#ctl00_cph1_txtOrder10').val('');
                parent.$('#ctl00_cph1_txtOrder11').val('');
                parent.$('#ctl00_cph1_txtOrder12').val('');
                parent.$('#ctl00_cph1_txtOrder13').val('');
                parent.$('#ctl00_cph1_txtOrder14').val('');
                parent.$('#ctl00_cph1_txtOrder15').val('');
                parent.$('#ctl00_cph1_txtOrder16').val('');
                parent.$('#ctl00_cph1_txtOrder17').val('');
                parent.$('#ctl00_cph1_txtOrderTotal').val('');

                // 버튼 비활성화
                parent.$('#ctl00_cph1_btnSelectProduct').attr('style', 'display:none');
                parent.$('#ctl00_cph1_divProductTitle').html('발주 스타일');
                parent.$('#ctl00_cph1_txtProduct').attr('disabled', true);

                // 버튼 활성화
                parent.$('#ctl00_cph1_btnRefresh').attr('disabled', false);
                parent.$('#ctl00_cph1_btnNewWrite').attr('disabled', false);
                parent.$('#ctl00_cph1_btnWrite').attr('disabled', false);
                parent.$('#ctl00_cph1_btnModify').attr('disabled', false);
                if (setCommaHis_Del(JegoQty01) > 0)
                    parent.$('#ctl00_cph1_txtOrder1').attr('disabled', false);
                if (setCommaHis_Del(JegoQty02) > 0)
                    parent.$('#ctl00_cph1_txtOrder2').attr('disabled', false);
                if (setCommaHis_Del(JegoQty03) > 0)
                    parent.$('#ctl00_cph1_txtOrder3').attr('disabled', false);
                if (setCommaHis_Del(JegoQty04) > 0)
                    parent.$('#ctl00_cph1_txtOrder4').attr('disabled', false);
                if (setCommaHis_Del(JegoQty05) > 0)
                    parent.$('#ctl00_cph1_txtOrder5').attr('disabled', false);
                if (setCommaHis_Del(JegoQty06) > 0)
                    parent.$('#ctl00_cph1_txtOrder6').attr('disabled', false);
                if (setCommaHis_Del(JegoQty07) > 0)
                    parent.$('#ctl00_cph1_txtOrder7').attr('disabled', false);
                if (setCommaHis_Del(JegoQty08) > 0)
                    parent.$('#ctl00_cph1_txtOrder8').attr('disabled', false);
                if (setCommaHis_Del(JegoQty09) > 0)
                    parent.$('#ctl00_cph1_txtOrder9').attr('disabled', false);
                if (setCommaHis_Del(JegoQty10) > 0)
                    parent.$('#ctl00_cph1_txtOrder10').attr('disabled', false);
                if (setCommaHis_Del(JegoQty11) > 0)
                    parent.$('#ctl00_cph1_txtOrder11').attr('disabled', false);
                if (setCommaHis_Del(JegoQty12) > 0)
                    parent.$('#ctl00_cph1_txtOrder12').attr('disabled', false);
                if (setCommaHis_Del(JegoQty13) > 0)
                    parent.$('#ctl00_cph1_txtOrder13').attr('disabled', false);
                if (setCommaHis_Del(JegoQty14) > 0)
                    parent.$('#ctl00_cph1_txtOrder14').attr('disabled', false);
                if (setCommaHis_Del(JegoQty15) > 0)
                    parent.$('#ctl00_cph1_txtOrder15').attr('disabled', false);
                if (setCommaHis_Del(JegoQty16) > 0)
                    parent.$('#ctl00_cph1_txtOrder16').attr('disabled', false);
                if (setCommaHis_Del(JegoQty17) > 0)
                    parent.$('#ctl00_cph1_txtOrder17').attr('disabled', false);
            
                parent.$("#ctl00_cph1_txtProduct").val(StyleNox);
                parent.$("#ctl00_cph1_txtProductDetail").val(MainName + ", " + SubName + ", " + SpecColor);
            
                parent.$("#ctl00_cph1_txtMsg").val(Msg);

                parent.$("#ctl00_cph1_imgProduct").attr("src", "/Handler/DisplayImage.ashx?code=" + StyleNox);

                /*
                if (imgWidth > 0)
                {
                    parent.$("#ctl00_cph1_imgProduct").attr("onclick", "zoomViewImage('style','" + StyleNox + "','" + imgWidth + "','" + imgHeight + "')");
                }
                */

                parent.$(".sizeName1").html(sizeName1);
                parent.$(".sizeName2").html(sizeName2);
                parent.$(".sizeName3").html(sizeName3);
                parent.$(".sizeName4").html(sizeName4);
                parent.$(".sizeName5").html(sizeName5);
                parent.$(".sizeName6").html(sizeName6);
                parent.$(".sizeName7").html(sizeName7);
                parent.$(".sizeName8").html(sizeName8);
                parent.$(".sizeName9").html(sizeName9);
                parent.$(".sizeName10").html(sizeName10);
                parent.$(".sizeName11").html(sizeName11);
                parent.$(".sizeName12").html(sizeName12);
                parent.$(".sizeName13").html(sizeName13);
                parent.$(".sizeName14").html(sizeName14);
                parent.$(".sizeName15").html(sizeName15);
                parent.$(".sizeName16").html(sizeName16);
                parent.$(".sizeName17").html(sizeName17);

                parent.$("#ctl00_cph1_hidKind").val(Kind);
            }

            parent.$('#dialog').dialog('close');
        }

        function Search_Check(chk) {

            if (document.getElementById("<%=this.txtProduct.ClientID%>").value == "")
            {
                alert("스타일 이름을 입력해주세요.");
                return false;
            }

            if (chk == "GO")
            {
                <%= Page.GetPostBackEventReference(btnSearch) %>;
            }

            return true;
        }

        //]]>
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div style="padding:5px;">
        <div style="height:5px;"></div>
	    
        <div class="table-responsive">
            <table class="table table-bordered" width="100%" cellspacing="0">
                <thead>
                </thead>
                <tbody>
                    <tr>
                        <th class="tdtitleW150" style="padding:3px;">스타일 이름</th>
                        <td style="padding:3px;">
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control" Width="100" OnKeyDown="if(event.keyCode == 13){return Search_Check('GO');}"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" OnClientClick="return Search_Check('')" />
                        </td>
                    </tr>
                </tbody>                                        
            </table>
        </div>
        <div style="height: 10px"></div>

	    <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvList_ItemDataBound" OnPagePropertiesChanging="lvList_PagePropertiesChanging">
            <LayoutTemplate>
		<table width="100%" cellpadding="1" cellspacing="1" border="0" class="defaultTable" id="zonelistProductTable">
            <tr align="center" class="defaultTitleTR1">
                <td width="40">NO</td>
                <td width="120">스타일번호</td>
                <td width="150">분류명</td>
                <td width="200">제품명</td>
                <td width="200">규격(칼라)</td>
                <td width="150">낱장단가</td>
                <td width="150">박스단가</td>
                <td width="200">BigSize 추가금</td>
                <td width="200">Size별 박스단가<br />수량</td>
                <td width="200">총수량 박스단가<br />수량</td>
            </tr>
            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
			<tr>
				<td colspan="10" align="center" class="tdbottom">
					<asp:DataPager ID="dpList" runat="server" PagedControlID="lvList" PageSize="20" >
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
							LastPageText="마지막"
								/>
						</Fields>
					</asp:DataPager>							
				</td>
			</tr>
		</table>
            </LayoutTemplate>
            <ItemTemplate>
            <tr class="defaultDataTR" style="cursor:pointer" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'" <asp:Literal ID="ltlSelectScript" runat="server"></asp:Literal>>
                <td><asp:Literal ID="ltlNumber" runat="server"></asp:Literal></td>
                <td><%# Eval("Jego_StyleNox") %></td>
                <td><asp:Literal ID="ltlMainName" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlSubName" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlSpecColor" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlJustPrice" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlLowPrice" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlBigSizePrice" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlSizeBoxQty" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ltlBoxQty" runat="server"></asp:Literal></td>
            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
            <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                <tr id="trNotData" runat="server">
				    <td style="background-color:White; text-align:center; font-weight:bold; height:30px">해당 발주 스타일이 존재하지 않습니다.</td>
                </tr>
            </table>
            </EmptyDataTemplate>
        </asp:ListView>
    
    </div>

</asp:Content>
