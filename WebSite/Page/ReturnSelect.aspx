<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="ReturnSelect.aspx.cs" Inherits="Page_ReturnSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
        
        // 스타일 정보 넘기기
        function SetProduct(StyleNox, Date, Times, MainBuyer, Sample, Line) {
            parent.$("#ctl00_cph1_txtProduct").val(StyleNox);
            parent.$("#ctl00_cph1_hidDate").val(Date);
            parent.$("#ctl00_cph1_hidTimes").val(Times);
            parent.$("#ctl00_cph1_hidSample").val(Sample);
            parent.$("#ctl00_cph1_hidLine").val(Line);

            parent.$('#dialog').dialog('close');

            parent.SearchProduct();
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
                <td width="200">발주의뢰일자</td>
                <td style="width: 80px;">TOTAL</td>
                <td>단가</td>
                <td>금액</td>
            </tr>
            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
			<tr>
				<td colspan="6" align="center" class="tdbottom">
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
                <td><%# Eval("Blju_StyleNox") %></td>
                <td><%# Eval("Blju_Date") %> <%# Eval("Blju_Times") %></td>
                <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_QtyTotal")) %></td>
                <td class="tdMoney"><%# GetAmountFormat(Eval("Blju_JustPrice")) %></td>
                <td class="tdMoney">
                    <%# GetAmountFormat(Eval("Blju_JustAmount")) %>
                </td>
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
