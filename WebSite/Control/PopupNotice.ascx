<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupNotice.ascx.cs" Inherits="Control_PopupNotice" %>
<script type="text/javascript" language="javascript">
	//<![CDATA[

    function fnPopupTodayClose(cookie) {
        setCookie(cookie, 'OK', 1);
        $('#divPopupNotice', parent.document).hide();

        return false;
    }

    function fnPopupNoticeClose() {
        $('#divPopupNotice', parent.document).hide();
        return false;
    }

    function PopupNotice() {
        $('#divPopupNotice', parent.document).show();
        $('#divPopupNotice', parent.document).draggable({ handle: "h2" });
    }

    function FileDownLoad(tFileName) {

        var tDir = "BoardFilePath";

        document.getElementById("ifrDown").src = "/Common/FileDown.aspx?tDir=" + escape(tDir) + "&tFileName=" + escape(tFileName);

        return false;
    }

    //]]>
</script>

<div style="height:50px; padding-top:5px; padding-left:490px; font-weight:bold; font-size:20px;">
    공지사항
</div>

<asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvList_ItemDataBound" OnPagePropertiesChanging="lvList_PagePropertiesChanging" >
    <LayoutTemplate>
        <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
        <div style="text-align: center; padding-right: 50px; padding-top: 10px;">
            <asp:DataPager ID="dpList" runat="server" PagedControlID="lvList" PageSize="1">
                <Fields>
                    <asp:NumericPagerField
                        PreviousPageText="&lt;"
                        NextPageText="&gt;"
                        ButtonCount="10"
                        NextPreviousButtonCssClass="PrevNext"
                        CurrentPageLabelCssClass="CurrentPage"
                        NumericButtonCssClass="PageNumber" />
                </Fields>
            </asp:DataPager>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div style="padding: 15px; height: 630px; overflow-y: auto; border: solid 1px #bdbdbd;">
            <asp:Literal ID="ltlNotice" runat="server"></asp:Literal>
        </div>
        <div style="float:left; padding-left:15px; padding-top: 10px; height: 30px;">
            <asp:LinkButton ID="lnkFile" runat="server"></asp:LinkButton>
        </div>
    </ItemTemplate>
</asp:ListView>

<div style="padding-top:10px; padding-left:365px;">
    <asp:Button ID="btnTodayClose" runat="server" Text="오늘 하루 표시안함" CssClass="btn btn-primary btn-sm" Width="180" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnClose" runat="server" Text="닫 기" CssClass="btn btn-primary btn-sm" Width="100" OnClientClick="return fnPopupNoticeClose();" />
</div>

<iframe id="ifrDown" name="ifrDown" scrolling="no" frameborder="0" style="text-align: center; vertical-align: middle; border-style: none; margin: 0px; width: 0px; height: 0px"></iframe>
