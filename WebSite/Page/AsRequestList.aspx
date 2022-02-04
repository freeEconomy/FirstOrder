<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="AsRequestList.aspx.cs" Inherits="Page_AsRequestList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/CalendarDate.ascx" TagName="CalendarDate" TagPrefix="cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	      
        function Search_CHK() {

            if (document.getElementById("<%=this.ucBnpmdateS.ClientID%>_txtDate").value == "" && document.getElementById("<%=this.ucBnpmdateE.ClientID%>_txtDate").value == "" && document.getElementById("<%=this.ucBljudateS.ClientID%>_txtDate").value == "" && document.getElementById("<%=this.ucBljudateE.ClientID%>_txtDate").value == "")
            {
                alert("검색할 AS접수일자나 발주의뢰일자를 입력해주세요.");
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
                                <th class="tdtitleW150">AS접수일자</th>
                                <td>
                                    <cal:CalendarDate ID="ucBnpmdateS" runat="server" />
                                    ~
								    <cal:CalendarDate ID="ucBnpmdateE" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th class="tdtitleW150">발주의뢰일자</th>
                                <td>
                                    <cal:CalendarDate ID="ucBljudateS" runat="server" />
                                    ~
                                    <cal:CalendarDate ID="ucBljudateE" runat="server" />
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
                <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvList_ItemDataBound" OnItemCommand="lvList_ItemCommand" OnPagePropertiesChanging="lvList_PagePropertiesChanging">
                    <LayoutTemplate>
                        <tr class="defaultTitleTR">
                            <td width="40">NO</td>
                            <td>AS접수일자</td>
                            <td>발주의뢰일자</td>
                            <td>스타일</td>
                            <td>AS수량</td>
                            <td>최초등록일자</td>
                            <td>최종수정일자</td>
                            <td>진행상태</td>
                            <td width="40">조회</td>
                            <td width="40">수정</td>
                            <td width="40">삭제</td>
                        </tr>
                        <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                        <tr>
                            <td colspan="11" align="center" class="tdbottom">
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
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                            </td>
                            <td><%# Eval("BnpmH_Date") %> <%# Eval("BnpmH_Times") %></td>
                            <td><%# Eval("BnpmH_Bjhd_Date") %> <%# Eval("BnpmH_Bjhd_Times") %></td>
                            <td><%# Eval("BnpmH_StyleNox") %></td>
                            <td class="tdMoney"><%# GetAmountFormat(Eval("BnpmH_QtyTotal")) %></td>
                            <td><%# Eval("BnpmH_CreateDate") %> <%# Eval("BnpmH_CreateSawon") %></td>
                            <td><%# Eval("BnpmH_ModifyDate") %> <%# Eval("BnpmH_ModifySaWon") %></td>
                            <td>
                                <asp:Literal ID="ltlBonsaCheck" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:LinkButton CommandArgument="" runat="server" ID="lnkSubView" CssClass="btn btn-success btn-circle btn-sm" CommandName="subView">
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
                        <tr class="defaultDataTR">
                            <td class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                        </tr>
                    </EmptyDataTemplate>
                </asp:ListView>

                <asp:ListView ID="lvExcel" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvExcel_ItemDataBound" Visible="false">
                    <LayoutTemplate>
                        <tr class="defaultTitleTR">
                            <td width="40">NO</td>
                            <td>AS접수일자</td>
                            <td>발주의뢰일자</td>
                            <td>스타일</td>
                            <td>AS수량</td>
                            <td>최초등록일자</td>
                            <td>최종수정일자</td>
                            <td>진행상태</td>
                        </tr>
                        <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                            <td>
                                <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                            </td>
                            <td><%# Eval("BnpmH_Date") %> <%# Eval("BnpmH_Times") %></td>
                            <td><%# Eval("BnpmH_Bjhd_Date") %> <%# Eval("BnpmH_Bjhd_Times") %></td>
                            <td><%# Eval("BnpmH_StyleNox") %></td>
                            <td class="tdMoney"><%# GetAmountFormat(Eval("BnpmH_QtyTotal")) %></td>
                            <td><%# Eval("BnpmH_CreateDate") %> <%# Eval("BnpmH_CreateSawon") %></td>
                            <td><%# Eval("BnpmH_ModifyDate") %> <%# Eval("BnpmH_ModifySaWon") %></td>
                            <td>
                                <asp:Literal ID="ltlBonsaCheck" runat="server"></asp:Literal>
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
                </div>
                    

            </div>
        </div>
    </div>
    
</asp:Content>
