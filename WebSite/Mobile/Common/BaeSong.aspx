<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MobilePop.master" AutoEventWireup="true" CodeFile="BaeSong.aspx.cs" Inherits="Mobile_Common_BaeSong" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	        
        function CheckBaeSong(baeSongOptID, baeSongID, baeSongNameID)
        {
            var optID = "";
            $("[name*=rbBaeSongOpt]").each(function () { // 라디오 버튼
                if (this.checked) {
                    optID = $(this).val();
                }
            });

            if (optID == "rbBaeSong1")
            {
                if ($('#<%=this.txtBaeSongName1.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtBaeSongName1.ClientID%>', '화물 지점명을 입력해야 합니다.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName1.ClientID%>').val().length < 5)
                {
                    showMessageToolTip('<%=this.txtBaeSongName1.ClientID%>', '화물 지점명을 5자리 이상으로 입력해야 합니다.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName1Send.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtBaeSongName1Send.ClientID%>', '대리점의 배송지명을 입력해야 합니다.');
                    return false;
                }

                $('#' + baeSongOptID + '', opener.document).val("화물 발송");
                $('#' + baeSongID + '', opener.document).val("0");
                $('#' + baeSongNameID + '', opener.document).val($('#<%=this.txtBaeSongName1.ClientID%>').val());

                return true;
            }
            else if (optID == "rbBaeSong2" || optID == "rbBaeSong3")
            {
                if ($('#<%=this.txtBaeSongName2.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtBaeSongName2.ClientID%>', '받으실 분을 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtZipcode.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtZipcode.ClientID%>', '받는곳 우편번호를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress1.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtAddress1.ClientID%>', '받는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress2.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtAddress2.ClientID%>', '받는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtTel1.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtTel1.ClientID%>', '받는곳 연락처를 입력해 주세요.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName2Send.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtBaeSongName2Send.ClientID%>', '보내는 분을 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtZipcodeSend.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtZipcodeSend.ClientID%>', '보내는곳 우편번호를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress1Send.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtAddress1Send.ClientID%>', '보내는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress2Send.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtAddress2Send.ClientID%>', '보내는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtTel1Send.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtTel1Send.ClientID%>', '보내는곳 연락처를 입력해 주세요.');
                    return false;
                }

                if (optID == "rbBaeSong2")
                {
                    $('#' + baeSongOptID + '', opener.document).val("CJ 택배");
                    $('#' + baeSongID + '', opener.document).val("1");
                }
                else if (optID == "rbBaeSong3")
                {
                    $('#' + baeSongOptID + '', opener.document).val("화물 택배");
                    $('#' + baeSongID + '', opener.document).val("2");
                }
                
                $('#' + baeSongNameID + '', opener.document).val($('#<%=this.txtBaeSongName2.ClientID%>').val());

                return true;
            }
            else if (optID == "rbBaeSong4") {
                if ($('#<%=this.txtBaeSongName4.ClientID%>').val() == "") {
                    showMessageToolTip('<%=this.txtBaeSongName4.ClientID%>', '기타배송 내용을 입력해야 합니다.');
                    return false;
                }

                $('#' + baeSongOptID + '', opener.document).val("기타 발송");
                $('#' + baeSongID + '', opener.document).val("3");
                $('#' + baeSongNameID + '', opener.document).val($('#<%=this.txtBaeSongName4.ClientID%>').val());
            }
        }

        //]]>
    </script>

    <style>
        .form-control {
            height: 1.8rem;
        }
        .ui-input-text input {
            background-color: #f9f9f9;
            border-color: #aaa;
            color: #585858;
            text-shadow: 0 1px 0 #fff;
            font-weight: bold;
        }
        .btn {
            vertical-align: top;
            padding: 0.275rem 0.75rem;
            margin-top: 0.3rem;
        }
        label {
            margin-bottom: 0.4rem;
            font-size: 14px !important;
        }
        .radiolist td {
            padding: 0px 5px 0px 5px;
            float: left;
        }
        .ui-radio .ui-btn.ui-radio-on:after {
            width: 18px;
            height: 18px;
        }
        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: 0.875rem;
            line-height: 1.5;
            border-radius: 0.2rem;
        }
        .ui-btn {
            font-size: 14px;
            margin: 0.25em 0em 0.25em 0em;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div style="padding:5px;">
        <div style="float:right;">                
            <asp:Button ID="btnClose" runat="server" Text="창 닫기" Width="100" CssClass="btn btn-primary btn-sm" OnClientClick="self.close(); return false;" />
        </div>
        <div style="clear:both;"></div>
        <div>
            <div>
                <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong1" runat="server" Text="화물 발송" AutoPostBack="true" OnCheckedChanged="rbBaeSong1_CheckedChanged" Checked="true" />
            </div>
            <div style="padding-left:5px;">
                <table class="reportTable">
                    <tr>
                        <th class="reportTh" style="min-width: 95px;">
                            화물 지점명
                        </th>
                        <td class="reportTd" style="padding-left: 5px;">
                            <asp:TextBox ID="txtBaeSongName1" runat="server" CssClass="form-control" Width="100%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th class="reportTh">
                            받는 분/ 연락처
                        </th>
                        <td class="reportTd" style="padding-left: 5px;">
                            <div class="ui-grid-a">
                                <div class="ui-block-a">
                                    <asp:TextBox ID="txtBaeSongName1Send" runat="server" CssClass="form-control" Width="100%" MaxLength="50"></asp:TextBox>
                                </div>
                                <div class="ui-block-b">
                                    <asp:TextBox ID="txtBaeSongTel1Send" runat="server" CssClass="form-control" Width="100%" MaxLength="30"></asp:TextBox>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div style="clear:both; height:5px;"></div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong2" runat="server" Text="CJ택배" AutoPostBack="true" OnCheckedChanged="rbBaeSong2_CheckedChanged" />
            </div>
            <div class="ui-block-b">
                <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong3" runat="server" Text="화물택배" AutoPostBack="true" OnCheckedChanged="rbBaeSong3_CheckedChanged" />
            </div>
        </div>

        <asp:Panel runat="server" ID="pnBaeSong">
            <div class="table-responsive">
                <table class="table table-bordered baesongTable" width="100%" cellspacing="0">
                    <thead>
                    </thead>
                    <tbody>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left">배송지 선택</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rblBaeSongOption" runat="server" RepeatDirection="Horizontal" CssClass="form-control radiolist" AutoPostBack="true" OnSelectedIndexChanged="rblBaeSongOption_SelectedIndexChanged">
                                    <asp:ListItem Text="기본 배송지" Value="기본"></asp:ListItem>
                                    <asp:ListItem Text="최근 배송지" Value="최근"></asp:ListItem>
                                    <asp:ListItem Text="배송지 목록" Value="배송지"></asp:ListItem>
                                    <asp:ListItem Text="새로입력" Value="새로"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:DropDownList ID="ddlAddrList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddrList_SelectedIndexChanged" Width="505">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left">받으실 분</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBaeSongName2" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left">주소</th>
                        </tr>
                        <tr>
                            <td>
                                <div style="line-height:300%">
                                    <asp:CheckBox ID="cbSaveAddr" runat="server" Text=" 배송지 목록에 추가" data-mini="true" />
                                    <div>
                                        <div class="ui-grid-a">
                                            <div class="ui-block-a">
                                                <asp:TextBox ID="txtZipcode" runat="server" MaxLength="10" CssClass="form-control txtZipcode" />
                                            </div>
                                            <div class="ui-block-b">
                                                <asp:Button ID="btnSearchAddr" runat="server" Text="주소찾기" CssClass="btn-sm btn-info" OnClientClick="return popupAddress();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtAddress1" runat="server" Width="100%" MaxLength="100" CssClass="form-control txtAddress1" />
                                        <asp:TextBox ID="txtAddress2" runat="server" Width="100%" MaxLength="50" CssClass="form-control txtAddress2" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left">연락처</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTel1" runat="server" CssClass="form-control" Width="100%" MaxLength="20"></asp:TextBox>
                                <asp:TextBox ID="txtTel2" runat="server" CssClass="form-control" Width="100%" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left">배송 요청사항</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Width="100%" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left2">보내는 분</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rblBaeSongSendOption" runat="server" RepeatDirection="Horizontal" CssClass="form-control radiolist" AutoPostBack="true" OnSelectedIndexChanged="rblBaeSongSendOption_SelectedIndexChanged">
                                    <asp:ListItem Text="발주처" Value="발주처"></asp:ListItem>
                                    <asp:ListItem Text="마커본사" Value="본사"></asp:ListItem>
                                    <asp:ListItem Text="새로입력" Value="새로"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txtBaeSongName2Send" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left2">주소</th>
                        </tr>
                        <tr>
                            <td>
                                <div style="line-height:300%">
                                    <div>
                                        <div class="ui-grid-a">
                                            <div class="ui-block-a">
                                                <asp:TextBox ID="txtZipcodeSend" runat="server" MaxLength="50" CssClass="form-control txtZipcodeSend" />
                                            </div>
                                            <div class="ui-block-b">
                                                <asp:Button ID="btnSearchAddrSend" runat="server" Text="주소찾기" CssClass="btn btn-info" OnClientClick="return popupAddressSend();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtAddress1Send" runat="server" Width="100%" MaxLength="100" CssClass="form-control txtAddress1Send" />
                                        <asp:TextBox ID="txtAddress2Send" runat="server" Width="100%" MaxLength="50" CssClass="form-control txtAddress2Send" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr style="height: 40px;">
                            <th class="tdtitleW150Left2">연락처</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTel1Send" runat="server" CssClass="form-control" Width="100%" MaxLength="20"></asp:TextBox>
                                <asp:TextBox ID="txtTel2Send" runat="server" CssClass="form-control" Width="100%" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
        
        <div style="clear:both; height:5px;"></div>
        <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong4" runat="server" Text="기타배송" AutoPostBack="true" OnCheckedChanged="rbBaeSong4_CheckedChanged" /> :
        <asp:TextBox ID="txtBaeSongName4" runat="server" CssClass="form-control" Width="500px" MaxLength="50"></asp:TextBox>

        <div style="clear:both; height:5px;"></div>

        <div>
            <div style="float:left; margin-top: -4px;">
                <asp:CheckBox ID="chkSunbulPay" runat="server" />
            </div>
            <div style="float:right;">
                <asp:Button ID="btnConfirm" runat="server" Text="확 인" CssClass="btn btn-primary btn-sm" OnClick="btnConfirm_Click" />
            </div>
        </div>
    </div>

</asp:Content>
