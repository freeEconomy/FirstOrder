<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="MarkJegoList.aspx.cs" Inherits="Page_MarkJegoList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	      
        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="valPreVal" Visible="false" runat="server"></asp:Label>
    <asp:Label ID="isSizeNum" Visible="false" runat="server"></asp:Label>
    
    <% if (valPreVal.Text == "tbl") { %>
    <style>
        .sizeHideTd {display: none;}
        .leftPadd {
            padding-left: 45px !important;
        }
    </style>
        <% if (isSizeNum.Text == "12") { %>
        <style>
            .sizeHideDiv {display: table-cell;}
        </style>
        <% } %>
    <% } else { %>
    <style>
        .leftPadd {
            padding-left:30px !important;
        }
    </style>
    <% } %>

    <style>
        .minWidth {
            min-width:60px;
        }
    </style>

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
                                <th class="tdtitleW150">스타일</th>
                                <td>
                                    <uc2:Product ID="ucProduct" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="height: 30px">
                                    <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" />
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
                <div class="fixed-table-container" style="height:620px;">
                    <div class="header-background"> </div>
                    <div class="fixed-table-container-inner">
                        <table cellspacing="0">
                    <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvMain_ItemDataBound" OnLayoutCreated="lvMain_LayoutCreated">
                        <LayoutTemplate>
                            <tr>
                                <th class="first" style="min-width: 35px;"><div class="th-inner" style="padding-left:15px;">번호</div></th>
                                <th><div class="th-inner" style="padding-left:25px;">스타일번호</div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize1" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize2" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize3" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize4" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize5" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize6" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize7" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize8" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize9" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd"><asp:Literal ID="ltlSize10" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd sizeHideDiv"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd sizeHideDiv"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPadd">TOTAL</div></th>
                            </tr>
                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                            <tr class="defaultSumTR">
                                <td colspan="2">합 계</td>
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
                            </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td>
                                    <asp:Literal ID="ltlNumber" runat="server"></asp:Literal>
                                </td>
                                <td><%# Eval("Jego_StyleNox") %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty01")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty02")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty03")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty04")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty05")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty06")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty07")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty08")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty09")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_Qty10")) %></td>
                                <td class="tdMoney minWidth sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty11")) %></td>
                                <td class="tdMoney minWidth sizeHideTd sizeHideDiv"><%# GetAmountFormat(Eval("Jego_Qty12")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty13")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty14")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty15")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty16")) %></td>
                                <td class="tdMoney minWidth sizeHideTd"><%# GetAmountFormat(Eval("Jego_Qty17")) %></td>
                                <td class="tdMoney minWidth"><%# GetAmountFormat(Eval("Jego_QtyTotal")) %></td>
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
    </div>
    
</asp:Content>
