<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="ReturnProduct.aspx.cs" Inherits="Page_ReturnProduct" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/Product.ascx" TagName="Product" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	      
        function GetProductPopup() {
            var searchValue = $("#<%=this.txtProduct.ClientID%>").val();

            var paramDate = "";
            var paramTime = "";
            var paramKure = "";

            OpenModal("/Page/ReturnSelect.aspx?searchValue=" + searchValue + "&paramDate=" + paramDate + "&paramTime=" + paramTime + "&paramKure=" + paramKure, '발주내역 선택창', '', '800', '680');

            return false;
        }

        function SearchProduct()
        {
            <%= Page.GetPostBackEventReference(btnSearch2) %>
        }

        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:Label ID="valPreVal" Visible="false" runat="server"></asp:Label>
    
    <asp:HiddenField ID="hidDate" runat="server" />
    <asp:HiddenField ID="hidTimes" runat="server" />
    <asp:HiddenField ID="hidSample" runat="server" />
    <asp:HiddenField ID="hidLine" runat="server" />

    <% if (valPreVal.Text == "tbl") { %>
    <style>
        .sizeHideTd {display: none;}
        .leftPadd {
            padding-left:50px !important;
        }
        .leftPaddTotal {
            padding-left:40px !important;
        }
    </style>
    <% } else { %>
    <style>
        .leftPadd {
            padding-left:25px !important;
        }
        .leftPaddTotal {
            padding-left:15px !important;
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
                                <th class="tdtitleW150">AS요청스타일</th>
                                <td>
                                    <asp:TextBox ID="txtProduct" runat="server" CssClass="form-control inputProduct" Width="100" MaxLength="30" OnKeyDown="if(event.keyCode == 13){return GetProductPopup();}"></asp:TextBox>

                                    <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClientClick="return GetProductPopup()" />

                                    <asp:Button ID="btnSearch2" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch2_Click" Visible="false" />
                                </td>
                            </tr>
                        </tbody>                                        
                    </table>
                </div>
                <div style="height: 10px"></div>
                <div>
                    <table class="table table-bordered" width="100%" cellspacing="0">
                        <thead>
                        </thead>
                        <tbody>
                            <tr>
                                <th class="tdtitleW150">발주의뢰일자</th>
                                <td class="no-gutters" style="min-width: 200px;">
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" Width="95%" Enabled="false"></asp:TextBox>
                                    </div>
                                </td>
                                <th class="tdtitleW150">공급가액</th>
                                <td style="min-width: 120px;">
                                    <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th class="tdtitleW150">부 가 세</th>
                                <td style="min-width: 120px;">
                                    <asp:TextBox ID="txtVatAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                </td>
                                <th class="tdtitleW150">합계금액</th>
                                <td style="min-width: 120px;">
                                    <asp:TextBox ID="txtHapAmount" runat="server" CssClass="form-control inputMoney" Width="100%" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="tdtitleW150">기타사항</th>
                                <td colspan="7">
                                    <asp:TextBox ID="txtEtc" runat="server" CssClass="form-control" Width="100%" Enabled="false"></asp:TextBox>
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
                                <th style="width:200px;"><div class="th-inner" style="padding-left:75px;">반품사유</div></th>
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
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize11" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize12" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize13" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize14" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize15" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize16" runat="server"></asp:Literal></div></th>
                                <th class="sizeHideTd"><div class="th-inner leftPadd"><asp:Literal ID="ltlSize17" runat="server"></asp:Literal></div></th>
                                <th><div class="th-inner leftPaddTotal">TOTAL</div></th>
                            </tr>
                            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                            <tr class="defaultSumTR">
                                <td >합 계</td>
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
                            </tr>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td style="width:120px;">
                                    사이즈 표기
                                </td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder1" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder2" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder3" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder4" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder5" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder6" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder7" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder8" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder9" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrder10" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder11" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder12" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder13" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder14" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder15" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder16" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth sizeHideTd"><asp:TextBox ID="txtOrder17" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
                                <td class="tdMoney minWidth"><asp:TextBox ID="txtOrderTotal" runat="server" CssClass="form-control align-right" Width="98%" OnKeyUp="CalQty(); setCommaHis(this); if(event.keyCode == 13){ChangeEnterTab(this);}"></asp:TextBox></td>
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
