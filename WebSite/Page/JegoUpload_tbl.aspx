<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="JegoUpload_tbl.aspx.cs" Inherits="Page_JegoUpload_tbl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/CalendarDate.ascx" TagName="CalendarDate" TagPrefix="cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[

        function ExcelUploadCheck() {

            if (document.getElementById("<%=this.fileExcel.ClientID %>").value == "") {
                alert("파일을 선택해 주세요.");
                return false;
            }

            return true;
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <asp:Label ID="isSizeNum" Visible="false" runat="server"></asp:Label>
    <% if (isSizeNum.Text == "12") { %>
    <style>
        .sizeHideDiv {display: table-cell;}
    </style>
    <% } %>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hidGubun" runat="server" />

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
                                        <td style="width: 400px;">
                                            <asp:Button ID="btnSample1" runat="server" Text="샘플양식(가로용)다운" Width="150" CssClass="btn btn-info" OnClientClick="location.href='/Upload/Sample/대리점_재고업로드_의류(가로)_7XL.xlsx'; return false;" />
                                            <asp:Button ID="btnSample2" runat="server" Text="샘플양식(세로용)다운" Width="150" CssClass="btn btn-info" OnClientClick="location.href='/Upload/Sample/대리점_재고업로드_의류(세로).xlsx'; return false;" />
                                        </td>
                                        <th class="tdtitleW150">파일선택</th>
                                        <td>
                                            <div style="float: left; padding-right: 10px;">
                                                <asp:FileUpload ID="fileExcel" runat="server" Width="350" CssClass="inputfile" /></div>
                                            <div style="float: left;">
                                                <asp:Button ID="btnOther" runat="server" Text="다른 파일 선택" Width="150" CssClass="btn btn-dark" OnClick="btnOther_Click" />
                                                <asp:Button ID="btnExcel" runat="server" Text="엑셀 업로드" Width="150" CssClass="btn btn-primary" OnClientClick="return ExcelUploadCheck();" OnClick="btnExcel_Click" />
                                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="저장하기" Width="150" CssClass="btn btn-danger" OnClick="btnSave_Click" OnClientClick="return confirm('저장 하시겠습니까 ?');" />
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2000" DynamicLayout="true">
                                                    <ProgressTemplate>
                                                        <center>
                                                    <img src="/images/ajax-loader.gif" runat="server" title="로딩중..." />
                                                </center>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
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
                            <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" OnLayoutCreated="lvMain_LayoutCreated">
                                <LayoutTemplate>
                                    <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                        <tr class="defaultTitleTR">
                                            <td width="50">번호</td>
                                            <td>스타일번호</td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize1" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize2" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize3" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize4" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize5" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize6" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize7" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize8" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize9" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">
                                                <asp:Literal ID="ltlSize10" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideDiv">
                                                <asp:Literal ID="ltlSize11" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;" class="sizeHideDiv">
                                                <asp:Literal ID="ltlSize12" runat="server"></asp:Literal></td>
                                            <td style="width: 80px;">재고수량</td>
                                        </tr>
                                        <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td><%# Eval("Jego_Num") %></td>
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
                                        <td class="tdMoney sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty11")) %></td>
                                        <td class="tdMoney sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty12")) %></td>
                                        <td class="tdMoney"><%# GetAmountFormat(Eval("Jego_QtyTotal")) %></td>
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

                            <asp:ListView ID="lvMain2" runat="server" ItemPlaceholderID="iph">
                                <LayoutTemplate>
                                    <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                        <tr class="defaultTitleTR">
                                            <td width="50">번호</td>
                                            <td>스타일번호</td>
                                            <td>사이즈명</td>
                                            <td>재고수량</td>
                                        </tr>
                                        <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td><%# Eval("Jego_Num") %></td>
                                        <td><%# Eval("Jego_StyleNox") %></td>
                                        <td><%# Eval("Jego_Size") %></td>
                                        <td><%# GetAmountFormat(Eval("Jego_QtyTotal")) %></td>
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

                    </div>
                </div>
            </div>

        </ContentTemplate>
	    <Triggers>
		    <asp:PostBackTrigger ControlID="btnExcel" />
	    </Triggers>
    </asp:UpdatePanel>

</asp:Content>
