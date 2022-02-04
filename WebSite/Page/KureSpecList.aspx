<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="KureSpecList.aspx.cs" Inherits="Page_KureSpecList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/CalendarDate.ascx" TagName="CalendarDate" TagPrefix="cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	      
        function Search_CHK() {

            if (document.getElementById("<%=this.ucNapmDateS.ClientID%>_txtDate").value == "" && document.getElementById("<%=this.ucNapmDateE.ClientID%>_txtDate").value == "")
            {
                alert("검색할 거래일자를 입력해주세요.");
                return false;
            }

            return true;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div class="container-fluid">

        <!-- Content Row -->
        <div class="row">

            <div class="card-body">

                <div class="table-responsive">
                    <table class="table table-bordered" width="100%" cellspacing="0">
                        <thead>
                        </thead>
                        <tbody>
                            <tr>
                                <th class="tdtitleW150">거래일자</th>
                                <td>
                                    <cal:CalendarDate ID="ucNapmDateS" runat="server" />
                                    ~
								    <cal:CalendarDate ID="ucNapmDateE" runat="server" />
                                    <asp:CheckBox ID="cbSizeDetail" runat="server" Text="사이즈별 세부내역 표시" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 30px">
                                    <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" OnClientClick="return Search_CHK()" />
                                    &nbsp;&nbsp;<asp:Button ID="btnExcel" runat="server" Text="엑셀변환" Width="100" CssClass="btn btn-success btn-sm" OnClick="btnExcel_Click" />
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
                    <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                    <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvMain_ItemDataBound">
                        <LayoutTemplate>
                                <tr class="defaultTitleTR">
                                    <td width="50">번호</td>
                                    <td width="100">거래일자</td>
                                    <td width="180">품목</td>
                                    <td width="80">거래구분</td>
                                    <td width="100">공급가액</td>
                                    <td width="100">부가세</td>
                                    <td width="100">합계금액</td>
                                    <td>받는분</td>
                                    <td>비고</td>
                                    <td width="80">명세서</td>
                                    <td width="80">메시지</td>
                                    <td width="50">조회</td>
                                </tr>
                                <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                <tr class="defaultSumTR">
                                    <td colspan="4">합 계</td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumNet" runat="server"></asp:Literal></td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumVat" runat="server"></asp:Literal></td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumHap" runat="server"></asp:Literal></td>
                                    <td colspan="5"></td>
                                </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                                <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td <asp:Literal ID="ltlNumberStyle" runat="server"></asp:Literal> <asp:Literal ID="ltlOpenChat1" runat="server"></asp:Literal>>
                                        <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                                    </td>
                                    <td <asp:Literal ID="ltlOpenChat2" runat="server"></asp:Literal>><%# Eval("Bjhd_NapmDate") %></td>
                                    <td style="text-align:left; padding-left:5px;" <asp:Literal ID="ltlOpenChat3" runat="server"></asp:Literal>><asp:Literal ID="ltlStyleNox" runat="server"></asp:Literal></td>
                                    <td <asp:Literal ID="ltlOpenChat4" runat="server"></asp:Literal>><asp:Literal ID="ltlSample" runat="server"></asp:Literal></td>
                                    <td <asp:Literal ID="ltlOpenChat5" runat="server"></asp:Literal> class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_NetAmount")) %></td>
                                    <td <asp:Literal ID="ltlOpenChat6" runat="server"></asp:Literal> class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_VatAmount")) %></td>
                                    <td <asp:Literal ID="ltlOpenChat7" runat="server"></asp:Literal> class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_HapAmount")) %></td>
                                    <td style="text-align:left; padding-left:5px;" <asp:Literal ID="ltlOpenChat8" runat="server"></asp:Literal>>
                                        <asp:Literal ID="ltlBaesongSend" runat="server"></asp:Literal>
                                    </td>
                                    <td style="text-align:left; padding-left:5px;" <asp:Literal ID="ltlOpenChat9" runat="server"></asp:Literal>><%# Eval("Bjhd_Remark") %></td>
                                    <td <asp:Literal ID="ltlOpenChat10" runat="server"></asp:Literal>><%# Eval("Bjhd_KureMyung_Print") %></td>
                                    <td style="cursor:pointer" <asp:Literal ID="ltlOpenChat11" runat="server"></asp:Literal>>
                                        <asp:LinkButton runat="server" ID="lnkMessenger" CssClass="btn btn-primary btn-sm" style="color:#858796" BackColor="White" BorderStyle="None" BorderWidth="0">
                                        </asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lnkSubView" CssClass="btn btn-success btn-circle btn-sm" CommandName="subView">
                                            <i class="fas fa-search"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                                        
                                <asp:ListView ID="lvDetail" runat="server" ItemPlaceholderID="iph">
                                    <LayoutTemplate>
                                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                            <tr class="defaultSumTR2">
                                                <td></td>
                                                <td colspan="3" style="text-align:center">계</td>
                                                <td class="tdMoney"><asp:Literal ID="ltlSumCnt" runat="server"></asp:Literal></td>
                                                <td class="tdMoney"></td>
                                                <td class="tdMoney"><asp:Literal ID="ltlSumNet" runat="server"></asp:Literal></td>
                                                <td colspan="5"></td>
                                            </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                            <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td></td>
                                                <td style="text-align:left; padding-left:5px;"><%# Eval("StyleNox") %></td>
                                                <td style="text-align:left; padding-left:5px;"><%# Eval("StyleSize") %></td>
                                                <td style="text-align:left; padding-left:5px;">PCS</td>
                                                <td class="tdMoney"><%# GetAmountFormat(Eval("SizeQty")) %></td>
                                                <td></td>
                                                <td class="tdMoney"><%# GetAmountFormat(Eval("SumNet")) %></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                        </ItemTemplate>
                        <EmptyDataTemplate>
                                <tr class="defaultDataTR">
                                    <td class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                                </tr>
                        </EmptyDataTemplate>
                    </asp:ListView>

                    <asp:ListView ID="lvExcel" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvMain_ItemDataBound" Visible="false">
                        <LayoutTemplate>
                                <tr class="defaultTitleTR">
                                    <td width="50">번호</td>
                                    <td>거래일자</td>
                                    <td>품목</td>
                                    <td>거래구분</td>
                                    <td>공급가액</td>
                                    <td>부가세</td>
                                    <td>합계금액</td>
                                    <td>받는분</td>
                                    <td>비고</td>
                                    <td>명세서</td>
                                </tr>
                                <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                <tr class="defaultSumTR">
                                    <td colspan="4">합 계</td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumNet" runat="server"></asp:Literal></td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumVat" runat="server"></asp:Literal></td>
                                    <td class="tdMoney"><asp:Literal ID="ltlSumHap" runat="server"></asp:Literal></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                                <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td><asp:Literal ID="ltlNumber" runat="server"></asp:Literal></td>
                                    <td><%# Eval("Bjhd_NapmDate") %></td>
                                    <td style="text-align:left; padding-left:5px;"><asp:Literal ID="ltlStyleNox" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltlSample" runat="server"></asp:Literal></td>
                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_NetAmount")) %></td>
                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_VatAmount")) %></td>
                                    <td class="tdMoney"><%# GetAmountFormat(Eval("Bjhd_HapAmount")) %></td>
                                    <td><asp:Literal ID="ltlBaesongSend" runat="server"></asp:Literal></td>
                                    <td style="text-align:left; padding-left:5px;"><%# Eval("Bjhd_Remark") %></td>
                                    <td><%# Eval("Bjhd_KureMyung_Print") %></td>
                                </tr>
                                        
                                <asp:ListView ID="lvDetail" runat="server" ItemPlaceholderID="iph">
                                    <LayoutTemplate>
                                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                            <tr class="defaultSumTR2">
                                                <td></td>
                                                <td colspan="3" style="text-align:center">계</td>
                                                <td class="tdMoney"><asp:Literal ID="ltlSumCnt" runat="server"></asp:Literal></td>
                                                <td class="tdMoney"></td>
                                                <td class="tdMoney"><asp:Literal ID="ltlSumNet" runat="server"></asp:Literal></td>
                                                <td colspan="3"></td>
                                            </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                            <tr class="defaultDataTR">
                                                <td></td>
                                                <td style="text-align:left; padding-left:5px;"><%# Eval("StyleNox") %></td>
                                                <td style="text-align:left; padding-left:5px;"><%# Eval("StyleSize") %></td>
                                                <td style="text-align:left; padding-left:5px;">PCS</td>
                                                <td class="tdMoney"><%# GetAmountFormat(Eval("SizeQty")) %></td>
                                                <td></td>
                                                <td class="tdMoney"><%# GetAmountFormat(Eval("SumNet")) %></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                                <tr class="defaultDataTR">
                                    <td class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                                </tr>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    </table>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
