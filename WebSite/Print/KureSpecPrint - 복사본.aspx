<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KureSpecPrint - 복사본.aspx.cs" Inherits="Print_KureSpecPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" moznomarginboxes="" mozdisallowselectionprint="">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    
    <script type="text/javascript" src="/js/jquery-1.9.1.min.js"></script>    
    <script type="text/javascript" src="/js/common-custom.js"></script>
    <link href="/css/print.css" rel="stylesheet" />

    <script language="JavaScript" type="text/JavaScript">
    <!--
        function Installed() {
            try {
                return (new ActiveXObject('IEPageSetupX.IEPageSetup'));
            }
            catch (e) {
                return false;
            }
        }

        function doSetup() {
            //컴포넌트를 설치했다면 창을 닫을때 설정값을 롤백시킴
            if (Installed())
                IEPageSetupX.RollBack();
        }

        var initBody;

        function beforePrint() {
            initBody = document.body.innerHTML;
            document.body.innerHTML = inPrint.innerHTML;
        }

        function afterPrint() {
            document.body.innerHTML = initBody;
        }

        function PrintPage() {
            DivMenu.style.display = "none";
            window.print();
            DivMenu.style.display = "";

            window.onbeforeprint = beforePrint;
            window.onafterprint = afterPrint;

            window.focus();	//포커스를 이 프레임으로

            IEPageSetupX.Orientation = 1;//인쇄 방향 설정 - 가로0, 세로1
            IEPageSetupX.paper = "A4";
            IEPageSetupX.header = '';//머리글
            IEPageSetupX.footer = '';//바닥글
            IEPageSetupX.PrintBackground = true;
            IEPageSetupX.ShrinkToFit = false;
            IEPageSetupX.Preview();

            var paramValue = "Y";
            var param_mainbuyer = $("#<%=this.hidMainBuyer.ClientID%>").val();
            var param_date = $("#<%=this.hidDate.ClientID%>").val();
            var param_times = $("#<%=this.hidTimes.ClientID%>").val();
            var param_sample = $("#<%=this.hidSample.ClientID%>").val();

            $.ajax({ //검색결과 바인딩
                type: "POST",
                url: "/Handler/WebService_common.asmx/UpdateKureMyungPrint",
                data: "{'param_mainbuyer':'" + param_mainbuyer + "','param_date':'" + param_date + "','param_times':'" + param_times + "','param_sample':'" + param_sample + "','paramValue':'" + paramValue + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    //$.mobile.loading('show');
                },
                error: function () {
                    $.mobile.loading('show', { text: 'Loading e ...', textVisible: true });
                },
                success: function (msg) {
                    var totCnt = $.parseJSON(msg.d);
                    if (totCnt > 0) {
                    }
                    //$.mobile.loading('hide');
                }
            });

            return false;
        }

        function CloseWindow() {
            self.close();
            return false;
        }

        function Installed() {
            try {
                return (new ActiveXObject('IEPageSetupX.IEPageSetup'));
            }
            catch (e) {
                return false;
            }
        }

        function printMe() {
            if (Installed()) {
                IEPageSetupX.header = "";
                IEPageSetupX.footer = "";
                IEPageSetupX.PrintBackground = true;
                IEPageSetupX.ShrinkToFit = false;
                IEPageSetupX.Preview();
            }
            else
                alert("컨트롤을 설치하지 않았네요.. 정상적으로 인쇄되지 않을 수 있습니다.");
        }
    //-->
    </script>

</head>
<BODY OnLoad="setTimeout('printMe();', 500);" OnUnload="if (Installed()) IEPageSetupX.RollBack();">
    <form id="form1" runat="server">
        
        <div id="DivMenu" style="width: 100%;">

            <asp:HiddenField ID="hidMainBuyer" runat="server" />
            <asp:HiddenField ID="hidDate" runat="server" />
            <asp:HiddenField ID="hidTimes" runat="server" />
            <asp:HiddenField ID="hidSample" runat="server" />

            <div style="text-align: center;">
                <input type="button" id="btnPrint" name="btnPrint" value="인쇄" class="button gray large" onclick="PrintPage();" />
                <!--<asp:Button ID="btnPrint2" runat="server" CssClass="button gray large" Text="인쇄" />-->
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" id="btnClose" name="btnClose" value="닫기" class="button gray large" onclick="CloseWindow();" />
            </div>

            <OBJECT id=IEPageSetupX classid="clsid:41C5BC45-1BE8-42C5-AD9F-495D6C8D7586" codebase="./IEPageSetupX.cab#version=1,4,0,3" style="width:0;height:0">	
	            <param name="copyright" value="http://isulnara.com">
	            <div style="position:absolute;top:276;left:320;width:300;height:68;border:solid 1 #99B3A0;background:#D8D7C4;overflow:hidden;z-index:1;visibility:visible;"><FONT style='font-family: "굴림", "Verdana"; font-size: 9pt; font-style: normal;'><BR>&nbsp;&nbsp;인쇄 여백제어 컨트롤이 설치되지 않았습니다.&nbsp;&nbsp;<BR>&nbsp;&nbsp;<a href="./IEPageSetupX.exe"><font color=red>이곳</font></a>을 클릭하여 수동으로 설치하시기 바랍니다.&nbsp;&nbsp;</FONT></div>
            </OBJECT>
        </div>

        <div id="inPrint">
            <div style="height:10px"></div>
            <div class="font12">
                <asp:Literal ID="ltlSerial" runat="server"></asp:Literal>
            </div>
            <div>
                <table border="0" width="100%" cellspacing="0" cellpadding="0" class="tablePrint">
                    <tr>
                        <td style="width:50%">
                            <table border="0" width="100%" cellspacing="0" cellpadding="3" class="tableSubPrint">
                                <tr>
                                    <td colspan="4" style="height: 80px;" class="font35 bold fontColor1 noBorderRight">
                                        <div style="float:left;padding-left: 10px;">
                                            거 래 명 세 서
                                        </div>
                                        <div style="float:left">
                                            〔
                                        </div>
                                        <div style="float:left;margin-top: -10px;margin-bottom: -10px;">
                                            <div style="font-size: 11px; height: 14px; margin-bottom: 6px;">공급자</div>
                                            <div style="font-size: 11px;">보관용</div>
                                        </div>
                                        <div style="float:left">
                                            〕
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aligncenter fontColor1" style="width: 10%;">
                                        거래<br />일자
                                    </td>
                                    <td class="bold aligncenter font14" colspan="2" style="width: 40%;">
                                        <asp:Literal ID="ltlNapmDate" runat="server"></asp:Literal>
                                    </td>
                                    <td class="alignright noBorderRight" style="width: 50%;">
                                        <span class="bold font14"><asp:Literal ID="ltlMainBuyerNm" runat="server"></asp:Literal></span> <span class="fontColor1">귀하</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="aligncenter fontColor1" colspan="2">합계금액 : <br />(공급가액+부가세)</td>
                                    <td class="bold alignright font14 noBorderRight" colspan="2"><asp:Literal ID="ltlHapAmount" runat="server"></asp:Literal> 원</td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:50%">
                            <table border="0" width="100%" cellspacing="0" cellpadding="3" class="tableSubPrint">
                                <tr>
                                    <td rowspan="5" class="bold aligncenter fontColor1" style="width:30px;">공<br /><br />급<br /><br />자</td>
                                    <td class="aligncenter fontColor1" style="width:50px">사업자<br />번호</td>
                                    <td colspan="3" class="bold aligncenter font18 fontColor1">
                                        <asp:Literal ID="ltlComInfo1" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aligncenter fontColor1">상호<br />(법인명)</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo2" runat="server"></asp:Literal>
                                    </td>
                                    <td class="aligncenter fontColor1" style="width:50px">성명<br />(대표자)</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo3" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aligncenter fontColor1">사업장<br />주소</td>
                                    <td colspan="3" class="bold fontColor1" style="width: 250px; word-break: break-all;">
                                        <asp:Literal ID="ltlComInfo4" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aligncenter fontColor1">업태</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo5" runat="server"></asp:Literal>
                                    </td>
                                    <td class="aligncenter fontColor1">종목</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo6" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aligncenter fontColor1">Tel</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo7" runat="server"></asp:Literal>
                                    </td>
                                    <td class="aligncenter fontColor1">Fax</td>
                                    <td class="bold fontColor1">
                                        <asp:Literal ID="ltlComInfo8" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" width="100%" cellspacing="0" cellpadding="3" class="tableSubPrint">
                                <tr>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">NO</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">품&nbsp;&nbsp;&nbsp;&nbsp;목</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">규&nbsp;&nbsp;&nbsp;&nbsp;격</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">단위</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">수&nbsp;&nbsp;&nbsp;&nbsp;량</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">단&nbsp;&nbsp;&nbsp;&nbsp;가</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">공급가액</td>
                                    <td class="bold aligncenter font14 fontColor1 noBorderTop">비&nbsp;&nbsp;&nbsp;&nbsp;고</td>
                                </tr>

                        <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph">
                            <ItemTemplate>
                                <tr>
                                    <td class="aligncenter">
                                        <%# Eval("num") %>
                                    </td>
                                    <td>
                                        <%# Eval("style") %>
                                    </td>
                                    <td>
                                        <%# Eval("size") %>
                                    </td>                            
                                    <td class="aligncenter">
                                        <%# Eval("unit") %>
                                    </td>
                                    <td class="alignright">
                                        <%# Eval("qty") %>
                                    </td>
                                    <td class="alignright">
                                        <%# Eval("dnga") %>
                                    </td>
                                    <td class="alignright">
                                        <%# Eval("dngasum") %>
                                    </td>
                                    <td></td>
                                </tr>
                                <!--<tr>
                                    <td class="aligncenter">1</td>
                                    <td>MK-102</td>
                                    <td>L(95)</td>
                                    <td class="aligncenter">PCS</td>
                                    <td class="alignright">1</td>
                                    <td class="alignright">11,000</td>
                                    <td class="alignright">11,000</td>
                                    <td></td>
                                </tr>
                                -->
                            </ItemTemplate>
                        </asp:ListView>

                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td class="aligncenter">--- 이 하 여 백 ---</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>

                        <asp:ListView ID="lvEmpty" runat="server" ItemPlaceholderID="iph">
                            <ItemTemplate>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                                <tr>
                                    <td colspan="4" class="aligncenter fontColor1">계</td>
                                    <td class="alignright"><asp:Literal ID="ltlQtyTotal" runat="server"></asp:Literal></td>
                                    <td class="alignright"></td>
                                    <td class="alignright"><asp:Literal ID="ltlNetAmount" runat="server"></asp:Literal></td>
                                    <td></td>
                                </tr>
                                <tr style="height:50px;">
                                    <td colspan="2" class="aligncenter fontColor1">기타사항</td>
                                    <td colspan="6">
                                        <asp:Literal ID="ltlEtc" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="alignright font12">
                <asp:Literal ID="ltlPrintDay" runat="server"></asp:Literal>
            </div>

        </div>

    </form>
</body>
</html>
