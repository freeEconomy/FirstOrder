<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MobilePop.master" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="Mobile_Common_Chat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" type="text/css" rel="stylesheet" />
    
    <script type="text/javascript">

        $(function () {

            $('textarea').on('keydown', function (event) {
                if (event.keyCode == 13)
                    if (!event.shiftKey) {
                        //<%= Page.GetPostBackEventReference(btnSend) %>;
                    }
            });

            $(".msg_history").scrollTop($(".msg_history")[0].scrollHeight);

            $('#<%=this.txtMsg.ClientID%>').focus();

            // 인터벌 MessageUpdate 처리 5초
            setInterval(function() {
                MessageUpdate();
            }, 5000);
        });

        function MessageUpdate() {

            var paramDate = $('#<%=this.hidDate.ClientID%>').val();
            var paramTimes = $('#<%=this.hidTimes.ClientID%>').val();
            var paramKure = $('#<%=this.hidMainbuyer.ClientID%>').val();
            var paramSample = $('#<%=this.hidSample.ClientID%>').val();

            //$('.msg_history').html('');

            $.ajax({ //검색결과 바인딩
                type: "POST",
                url: "/Handler/WebService_common.asmx/MessageUpdate",
                data: "{'paramDate':'" + paramDate + "','paramTimes':'" + paramTimes + "','paramKure':'" + paramKure + "','paramSample':'" + paramSample + "'}",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    
                },
                error: function () {
                    
                },
                success: function (msg) {
                    var items = [];
                    var html = '';
                    var data = $.parseJSON(msg.d);
                    var totCnt = data.length;

                    if (totCnt > 0) {
                        $.each(data, function (key, val) {

                            if (val.mesg_bonsa_daeri == "0") // 본사 메시지
                            {
                                items.push("<div class=\"incoming_msg\">");
                                items.push("    <div class=\"received_msg\">");
                                items.push("        <p>");
                                items.push("            " + val.mesg_message + "");
                                items.push("        </p>");
                                items.push("        <span class=\"time_date\">");
                                items.push("             " + val.createDate + "");
                                items.push("        </span>");
                                items.push("    </div>");
                                items.push("</div>");
                            }
                            else  // 대리점 메시지
                            {
                                items.push("<div class=\"outgoing_msg\">");
                                items.push("    <div class=\"sent_msg\">");
                                items.push("        <p>");
                                items.push("            " + val.mesg_message + "");
                                items.push("        </p>");
                                items.push("        <span class=\"time_date\">");
                                items.push("             " + val.createDate + "");
                                if (val.isBonsaRead == "1") // 본사 확인여부
                                {
                                    items.push("             <span style='color:red;'>&nbsp;1</span>");
                                }
                                items.push("        </span>");
                                items.push("    </div>");
                                items.push("</div>");
                            }

                        });
                    }

                    html = items.join('');

                    $(".msg_history").append($(html));

                    $(".msg_history").scrollTop($(".msg_history")[0].scrollHeight);
                }
            });
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <asp:HiddenField ID="hidDate" runat="server" />
    <asp:HiddenField ID="hidTimes" runat="server" />
    <asp:HiddenField ID="hidMainbuyer" runat="server" />
    <asp:HiddenField ID="hidSample" runat="server" />

    <asp:Button ID="btnSend" runat="server" Visible="false" OnClick="sendMessage" />

    <div style="padding:5px;">
    <div style="float:right;">                
        <asp:Button ID="btnClose" runat="server" Text="창 닫기" Width="100" CssClass="btn btn-primary btn-sm" OnClientClick="self.close(); return false;" />
    </div>
    <div style="clear:both;"></div>

    <div>
        <div class="messaging">
            <div class="inbox_msg">
                <div>
                    <div class="bonsaTop">본사</div>
                    <div class="daeriTop">대리점</div>
                </div>
                <div class="mesgs">
                    <div class="msg_history">

                        <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph" onitemdatabound="lvMain_ItemDataBound" >
                            <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="iph"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <asp:Literal ID="ltlItem" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                    <div class="type_msg">
                        <div class="input_msg_write">
                            <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" CssClass="write_msg" placeholder="내용을 입력하세요." Width="90%"></asp:TextBox>
                            
                            <asp:LinkButton runat="server" ID="lnkSend" CssClass="msg_send_btn" OnClick="sendMessage" >
                                <i class="fa fa-paper-plane-o" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                        <div style="display:none">
                            <asp:LinkButton runat="server" ID="lnkSend2" OnClick="sendMessage2" >
                                <i class="fa fa-paper-plane-o" aria-hidden="true"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    </div>

</asp:Content>
