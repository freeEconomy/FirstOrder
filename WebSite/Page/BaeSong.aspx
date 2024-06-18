<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="BaeSong.aspx.cs" Inherits="Page_BaeSong" %>

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
                    showMessageToolTipPop('<%=this.txtBaeSongName1.ClientID%>', '화물 지점명을 입력해야 합니다.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName1.ClientID%>').val().length < 4)
                {
                    showMessageToolTipPop('<%=this.txtBaeSongName1.ClientID%>', '화물 지점명을 4자리 이상으로 입력해야 합니다.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName1Send.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtBaeSongName1Send.ClientID%>', '대리점의 배송지명을 입력해야 합니다.');
                    return false;
                }

                parent.$('#' + baeSongOptID + '').val('화물 발송');
                parent.$('#' + baeSongID + '').val('0');
                parent.$('#' + baeSongNameID + '').val($('#<%=this.txtBaeSongName1.ClientID%>').val());

                return true;
            }
            else if (optID == "rbBaeSong2" || optID == "rbBaeSong3")
            {
                if ($('#<%=this.txtBaeSongName2.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtBaeSongName2.ClientID%>', '받으실 분을 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtZipcode.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtZipcode.ClientID%>', '받는곳 우편번호를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress1.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtAddress1.ClientID%>', '받는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress2.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtAddress2.ClientID%>', '받는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtTel1.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtTel1.ClientID%>', '받는곳 연락처를 입력해 주세요.');
                    return false;
                }

                if ($('#<%=this.txtBaeSongName2Send.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtBaeSongName2Send.ClientID%>', '보내는 분을 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtZipcodeSend.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtZipcodeSend.ClientID%>', '보내는곳 우편번호를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress1Send.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtAddress1Send.ClientID%>', '보내는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtAddress2Send.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtAddress2Send.ClientID%>', '보내는곳 주소를 입력해 주세요.');
                    return false;
                }
                if ($('#<%=this.txtTel1Send.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtTel1Send.ClientID%>', '보내는곳 연락처를 입력해 주세요.');
                    return false;
                }

                if (optID == "rbBaeSong2")
                {
                    parent.$('#' + baeSongOptID + '').val('로젠 택배');
                    parent.$('#' + baeSongID + '').val('1');
                }
                else if (optID == "rbBaeSong3")
                {
                    parent.$('#' + baeSongOptID + '').val('화물 택배');
                    parent.$('#' + baeSongID + '').val('2');
                }
                
                parent.$('#' + baeSongNameID + '').val($('#<%=this.txtBaeSongName2.ClientID%>').val());

                return true;
            }
            else if (optID == "rbBaeSong4") {
                if ($('#<%=this.txtBaeSongName4.ClientID%>').val() == "") {
                    showMessageToolTipPop('<%=this.txtBaeSongName4.ClientID%>', '기타배송 내용을 입력해야 합니다.');
                    return false;
                }

                parent.$('#' + baeSongOptID + '').val('기타 발송');
                parent.$('#' + baeSongID + '').val('3');
                parent.$('#' + baeSongNameID + '').val($('#<%=this.txtBaeSongName4.ClientID%>').val());

            }
        }

        //]]>
    </script>

    <style>
        .form-control {
            height: 1.8rem;
        }
        .btn {
            vertical-align: top;
            padding: 0.275rem 0.75rem;
            margin-top: 0.3rem;
        }
        label {
            margin-bottom: 0.4rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div style="padding:5px;">
        <div style="height:5px;"></div>
        
        <div>
            <div style="float:left">
                <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong1" runat="server" Text="화물 발송" AutoPostBack="true" OnCheckedChanged="rbBaeSong1_CheckedChanged" Checked="true" /> : 
            </div>
            <div style="float:left; padding-left:5px;">
                <div style="font-size:14px;text-align: right;">
                    화물 지점명 : <asp:TextBox ID="txtBaeSongName1" runat="server" CssClass="form-control" Width="414px" MaxLength="50"></asp:TextBox>
                </div>
                <div style="font-size:14px;text-align: right; padding-top: 5px;">
                    받는 분/ 연락처 : <asp:TextBox ID="txtBaeSongName1Send" runat="server" CssClass="form-control" Width="240px" MaxLength="50"></asp:TextBox> <asp:TextBox ID="txtBaeSongTel1Send" runat="server" CssClass="form-control" Width="170px" MaxLength="30"></asp:TextBox>
                </div>
            </div>
        </div>

        <div style="clear:both; height:5px;"></div>
        <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong2" runat="server" Text="로젠택배" AutoPostBack="true" OnCheckedChanged="rbBaeSong2_CheckedChanged" />
        <asp:RadioButton GroupName="rbBaeSongOpt" ID="rbBaeSong3" runat="server" Text="화물택배" AutoPostBack="true" OnCheckedChanged="rbBaeSong3_CheckedChanged" />
        <asp:Panel runat="server" ID="pnBaeSong">
            <div class="table-responsive">
                <table class="table table-bordered baesongTable" width="100%" cellspacing="0">
                    <thead>
                    </thead>
                    <tbody>
                        <tr>
                            <th class="tdtitleW150Left">배송지 선택</th>
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
                        <tr>
                            <th class="tdtitleW150Left">받으실 분</th>
                            <td>
                                <asp:TextBox ID="txtBaeSongName2" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Right">주소</th>
                            <td>
                                <div style="line-height:300%">
                                    <div>
                                        <div style="float:left">
                                            <asp:TextBox ID="txtZipcode" runat="server" Width="60" MaxLength="10" CssClass="form-control txtZipcode" />
                                            <asp:Button ID="btnSearchAddr" runat="server" Text="주소찾기" Width="110" CssClass="btn btn-info" OnClientClick="return popupAddress();" />
                                        </div>
                                        <div style="float:right; height:35px;">
                                            <asp:CheckBox ID="cbSaveAddr" runat="server" Text=" 배송지 목록에 추가" />
                                        </div>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtAddress1" runat="server" Width="100%" MaxLength="100" CssClass="form-control txtAddress1" /><br />
                                        <asp:TextBox ID="txtAddress2" runat="server" Width="100%" MaxLength="50" CssClass="form-control txtAddress2" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Right">연락처</th>
                            <td>
                                <asp:TextBox ID="txtTel1" runat="server" CssClass="form-control" Width="228px" MaxLength="20"></asp:TextBox>
                                <asp:TextBox ID="txtTel2" runat="server" CssClass="form-control" Width="228px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Right">배송 요청사항</th>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Width="100%" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Left">보내는 분</th>
                            <td>
                                <asp:RadioButtonList ID="rblBaeSongSendOption" runat="server" RepeatDirection="Horizontal" CssClass="form-control radiolist" AutoPostBack="true" OnSelectedIndexChanged="rblBaeSongSendOption_SelectedIndexChanged">
                                    <asp:ListItem Text="발주처" Value="발주처"></asp:ListItem>
                                    <asp:ListItem Text="마커본사" Value="본사"></asp:ListItem>
                                    <asp:ListItem Text="새로입력" Value="새로"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txtBaeSongName2Send" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Right">주소</th>
                            <td>
                                <div style="line-height:300%">
                                    <asp:TextBox ID="txtZipcodeSend" runat="server" Width="60" MaxLength="50" CssClass="form-control txtZipcodeSend" />
                                    <asp:Button ID="btnSearchAddrSend" runat="server" Text="주소찾기" Width="110" CssClass="btn btn-info" OnClientClick="return popupAddressSend();" />
                                    <asp:TextBox ID="txtAddress1Send" runat="server" Width="100%" MaxLength="100" CssClass="form-control txtAddress1Send" /><br />
                                    <asp:TextBox ID="txtAddress2Send" runat="server" Width="100%" MaxLength="50" CssClass="form-control txtAddress2Send" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="tdtitleW150Right">연락처</th>
                            <td>
                                <asp:TextBox ID="txtTel1Send" runat="server" CssClass="form-control" Width="228px" MaxLength="20"></asp:TextBox>
                                <asp:TextBox ID="txtTel2Send" runat="server" CssClass="form-control" Width="228px" MaxLength="20"></asp:TextBox>
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
            <div style="float:left">
                <asp:CheckBox ID="chkSunbulPay" runat="server" />
            </div>
            <div style="float:right">
                <asp:Button ID="btnConfirm" runat="server" Text="확 인" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnConfirm_Click" />
                <asp:Button ID="btnClose" runat="server" Text="닫 기" Width="100" CssClass="btn btn-primary btn-sm" OnClientClick="self.close(); return false;" Visible="false" />
            </div>
        </div>
    </div>

</asp:Content>
