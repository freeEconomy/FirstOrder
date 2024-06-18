<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="Notice.aspx.cs" Inherits="Page_Notice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/CalendarDate.ascx" TagName="CalendarDate" TagPrefix="cal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
	        function iFrame_OnUploadComplete(upFileName, upFileSize) {
		        document.getElementById("<%=this.hidUpFileName.ClientID %>").value = upFileName;
		        document.getElementById("<%=this.hidUpFileSize.ClientID %>").value = upFileSize;

		        if (document.getElementById("<%=this.btnWrite.ClientID%>")) {
			        <%= Page.GetPostBackEventReference(btnWrite) %>
		        } else {
			        <%= Page.GetPostBackEventReference(btnModify) %>
		        }
	        }

            function WriteCheck() {
                if (document.getElementById("<%=this.txtTitle.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtTitle.ClientID%>', '팝업 제목을 입력해 주세요.');
                    document.getElementById("<%=this.txtTitle.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtLeftMargin.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtLeftMargin.ClientID%>', '왼쪽여백을 입력해 주세요.');
                    document.getElementById("<%=this.txtLeftMargin.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtPWidth.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtPWidth.ClientID%>', '팝업 가로값을 입력해 주세요.');
                    document.getElementById("<%=this.txtPWidth.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtPHeight.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtPHeight.ClientID%>', '팝업 세로값을 입력해 주세요.');
                    document.getElementById("<%=this.txtPHeight.ClientID%>").focus();
                    return false;
                }

                document.getElementById("<%=this.hidUpFileName.ClientID %>").value = "";
                document.getElementById("<%=this.hidUpFileSize.ClientID %>").value = "";

                var fileUp = document.getElementById("ifrFile").contentWindow.document.getElementById("fuFile");

                if (fileUp.value == "") {
                    return true;
                } else {
                    var fileBtn = document.getElementById("ifrFile").contentWindow.document.getElementById("btnUpload");
                    fileBtn.click();

                    return false;
                }
	        }

	        function ModifyCheck() {
                if (document.getElementById("<%=this.txtTitle.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtTitle.ClientID%>', '팝업 제목을 입력해 주세요.');
                    document.getElementById("<%=this.txtTitle.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtTopMargin.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtTopMargin.ClientID%>', '상단여백을 입력해 주세요.');
                    document.getElementById("<%=this.txtTopMargin.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtLeftMargin.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtLeftMargin.ClientID%>', '왼쪽여백을 입력해 주세요.');
                    document.getElementById("<%=this.txtLeftMargin.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtPWidth.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtPWidth.ClientID%>', '팝업 가로값을 입력해 주세요.');
                    document.getElementById("<%=this.txtPWidth.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=this.txtPHeight.ClientID%>").value == "") {
                    showMessageToolTipPop('<%=this.txtPHeight.ClientID%>', '팝업 세로값을 입력해 주세요.');
                    document.getElementById("<%=this.txtPHeight.ClientID%>").focus();
                    return false;
                }

		        document.getElementById("<%=this.hidUpFileName.ClientID %>").value = "";
		        document.getElementById("<%=this.hidUpFileSize.ClientID %>").value = "";

		        var fileUp = document.getElementById("ifrFile").contentWindow.document.getElementById("fuFile");

		        if (fileUp.value == "") {
			        return true;
		        } else {
			        var fileBtn = document.getElementById("ifrFile").contentWindow.document.getElementById("btnUpload");
			        fileBtn.click();
			
			        return false;
		        }
	        }

	        function DeleteCheck() {
		        if (confirm("삭제된 자료는 복구되지 않습니다. 삭제하시겠습니까?")) {
			        return true;
		        } else {
			        return false;
		        }
	        }
	
	        function FileDownLoad(tFileName) {
    
	            var tDir = "BoardFilePath";
		
		        document.getElementById("ifrDown").src = "/Common/FileDown.aspx?tDir=" + escape(tDir) + "&tFileName=" + escape(tFileName);
            }

            function OnlyNumbers(event) {
                event.target.value = event.target.value.replace(/[^0-9]/g, "");
            }
        //]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div class="container-fluid">

        <!-- Content Row -->
        <div class="row">

            <div class="card-body">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hidIdx" runat="server" />
                        <asp:HiddenField ID="hidUpFileName" runat="server" />
                        <asp:HiddenField ID="hidUpFileSize" runat="server" />
                        <asp:HiddenField ID="hidOrgFileName" runat="server" />
                        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
                            <asp:View ID="vwList" runat="server">
                                <div class="table-responsive">
                                    <table class="table table-bordered" width="100%" cellspacing="0">
                                        <thead>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th class="tdtitleW150">작성일자</th>
                                                <td>
                                                    <cal:CalendarDate ID="ucMemoDaySSch" runat="server" />
                                                    ~
								                    <cal:CalendarDate ID="ucMemoDayESch" runat="server" />
                                                </td>
                                                <th>내용</th>
                                                <td>
                                                    <asp:TextBox ID="txtMemoSch" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="height: 30px">
                                                    <asp:Button ID="btnSearch" runat="server" Text="검 색" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnAdd" runat="server" Text="추 가" Width="100" CssClass="btn btn-primary btn-sm" OnClick="btnAdd_Click" />
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
                                    <asp:ListView ID="lvMain" runat="server" ItemPlaceholderID="iph"
                                        OnPagePropertiesChanging="lvMain_PagePropertiesChanging" OnItemDataBound="lvMain_ItemDataBound" OnItemCommand="lvMain_ItemCommand">
                                        <LayoutTemplate>
                                            <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                                <tr class="defaultTitleTR">
                                                    <td width="50">번호</td>
                                                    <td width="180">공지기간</td>
                                                    <td>내용</td>
                                                    <td width="40">파일</td>
                                                    <td width="80">등록일자</td>
                                                    <td width="40">수정</td>
                                                    <td width="40">삭제</td>
                                                </tr>
                                                <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
                                                <tr>
                                                    <td colspan="7" align="center" class="tdbottom">
                                                        <asp:DataPager ID="dpMain" runat="server" PagedControlID="lvMain" PageSize="10">
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
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="defaultDataTR" onmouseover="this.style.backgroundColor='#FFF0F5'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                <td>
                                                    <asp:Literal ID="ltlNumber" runat="server"></asp:Literal></td>
                                                <td><%# Eval("noticeday") %> ~ <%# Eval("noticeday2") %></td>
                                                <td style="text-align: left; padding: 3px 3px 3px 3px;">
                                                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgFile" runat="server" />
                                                </td>
                                                <td><%# Eval("memoday") %></td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lnkSubModify" CssClass="btn btn-success btn-circle btn-sm" CommandName="subModify" CommandArgument='<%# Eval("u") %>'>
                                                        <i class="fas fa-check"></i>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="lnkSubDelete" CssClass="btn btn-danger btn-circle btn-sm" CommandName="subDelete" CommandArgument='<%# Eval("u") %>' OnClientClick="return confirm('삭제된 자료는 복구되지 않습니다. 삭제 하시겠습니까 ?');">
                                                        <i class="fas fa-trash"></i>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                                                <tr class="defaultTitleTR">
                                                    <td width="50">번호</td>
                                                    <td width="180">공지기간</td>
                                                    <td>내용</td>
                                                    <td width="40">파일</td>
                                                    <td width="80">등록일자</td>
                                                    <td width="40">수정</td>
                                                    <td width="40">삭제</td>
                                                </tr>
                                                <tr class="defaultDataTR">
                                                    <td colspan="7" class="tdbottom" valign="middle" align="center">등록된 자료가 존재하지 않습니다.</td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:View>

                            <asp:View ID="vwWrite" runat="server">
                                <div class="table-responsive">
                                    <table class="table table-bordered" width="100%" cellspacing="0">
                                        <thead>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th>팝업 제목</th>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Width="400" MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>공지기간</th>
                                                <td colspan="3">
                                                    <cal:CalendarDate ID="ucNoticeDay" runat="server" />
                                                    ~ <cal:CalendarDate ID="ucNoticeDay2" runat="server" />
                                                    * 메인에 띄우는 공지기간입니다.(앞 날짜만 입력하시면 하루만 표시됩니다.)
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>리스트상단표시</th>
                                                <td colspan="3">
                                                    <asp:CheckBox runat="server" ID="cbIsNotice" Text="상단표시" />
                                                    (* 리스트에서 상단에 표시합니다.)
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>내용</th>
                                                <td colspan="3">
                                                    <ckeditor:ckeditorcontrol id="ckContent" runat="server" height="300px" width="100%"></ckeditor:ckeditorcontrol>
                                                    <div><asp:Label ID="lblFile" runat="server"></asp:Label></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>첨부파일</th>
                                                <td colspan="3">
                                                    <iframe id="ifrFile" name="ifrFile" scrolling="no" frameborder="0" style="text-align: center; vertical-align: middle; border-style: none; margin: 0px; width: 100%; height: 30px" src="/Common/FileUpload.aspx?tDir=BoardFilePath"></iframe>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>팝업 위치</th>
                                                <td colspan="3">
                                                    상단여백 : 
                                                    <asp:TextBox ID="txtTopMargin" runat="server" CssClass="form-control" Width="100" OnKeyUp="OnlyNumbers(event)"></asp:TextBox>
                                                    왼쪽여백 : 
                                                    <asp:TextBox ID="txtLeftMargin" runat="server" CssClass="form-control" Width="100" OnKeyUp="OnlyNumbers(event)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>팝업 크기</th>
                                                <td colspan="3">
                                                    가로 : 
                                                    <asp:TextBox ID="txtPWidth" runat="server" CssClass="form-control" Width="100" OnKeyUp="OnlyNumbers(event)"></asp:TextBox>
                                                    세로 : 
                                                    <asp:TextBox ID="txtPHeight" runat="server" CssClass="form-control" Width="100" OnKeyUp="OnlyNumbers(event)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="height: 30px">
                                                    <asp:Button ID="btnWrite" runat="server" Text="저 장" Width="110" CssClass="btn btn-info" OnClientClick="return WriteCheck();" OnClick="btnWrite_Click" />
                                                    <asp:Button ID="btnModify" runat="server" Text="수 정" Width="110" CssClass="btn btn-info" OnClientClick="return ModifyCheck();" OnClick="btnModify_Click"/>
                                                    <asp:Button ID="btnDelete" runat="server" Text="삭 제" Width="110" CssClass="btn btn-info" OnClientClick="return DeleteCheck();" OnClick="btnDelete_Click" />
                                                    <asp:Button ID="btnList" runat="server" Text="리스트" Width="110" CssClass="btn btn-info" OnClick="btnList_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnWrite" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
    
    <iframe id="ifrDown" name="ifrDown" scrolling="no" frameborder="0" style="text-align: center; vertical-align: middle; border-style: none; margin: 0px; width: 0px; height: 0px"></iframe>

</asp:Content>
