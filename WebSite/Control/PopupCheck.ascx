<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupCheck.ascx.cs" Inherits="Control_PopupCheck" %>

<script type="text/javascript" src="/js/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="/js/jquery-ui-1.13.2/jquery-ui.js"></script>

<style type="text/css">
	#wrapper_popup div[id ^= 'layerPop'] {width:100%;margin:10px;position:absolute; padding:20px 30px; background:#f2f0ff; z-index:100000;border-radius:5px;overflow:auto;display:none}
	#wrapper_popup div[id ^= 'layerPop'] h2 {display:block;margin:5px 0 0;padding-bottom:15px;border-bottom:1px #d8d8d8 solid;font-size:1.1em;font-weight:bold}
	#wrapper_popup div[id ^= 'layerPop'] p {display:block;padding:20px 0 0;margin:0;font-size:1.0em;line-height:1.5em}
	#wrapper_popup div[id ^= 'layerPop'] .hour-close {display:inline-block;padding:8px 20px;background:#f2f2f2;border:1px #d8d8d8 solid;border-radius:5px;margin-top:15px;font-size: 0.80rem;color:#fff;background-color: #4e73df;border-color: #4e73df;}
	#wrapper_popup div[id ^= 'layerPop'] .hour-close:hover {background:#8aa8ff;color:#fff;text-decoration:none}
	#wrapper_popup div[id ^= 'layerPop'] img.cancel {position:absolute; right:20px;top:20px;zoom:1.2;filter: alpha(opacity=50);opacity: 0.7}
	#wrapper_popup div[id ^= 'layerPop'] img.cancel:hover {zoom:1.2;filter: alpha(opacity=100);opacity: 1.0}
	#wrapper_popup .b-area {width:100%;text-align:right;margin-top:20px;border-top:1px #ccc solid}
	#wrapper_popup ul {list-style-type:disc;margin:0 0 20px;line-height:1.5em}
	#wrapper_popup .c-green {color:green !important}
	#wrapper_popup .c-blue {color:blue !important}
	#wrapper_popup .c-red {color:red !important}
	.layer-shadow {box-shadow: 0px 3px 15px 0px rgba(0,0,0,0.74);-webkit-box-shadow: 0px 3px 15px 0px rgba(0,0,0,0.74);-moz-box-shadow: 0px 3px 15px 0px rgba(0,0,0,0.74);}
    .popup-content {
        overflow-y: auto;
        margin-top: 10px;
    }
</style>
<script>

    $(document).ready(function() {
        $(".layer-shadow").draggable();
    });

    function closeLayer(id){
	    $("#" + id).hide();
    }
  
    function openLayer(id){
	    $("#" + id).show();
    }

    function closeNoOpen(IdName) {
	    setCookie(IdName, "N", 1);
	    closeLayer(IdName);
    }

    function setCookie(IdName, value, expiredays) {
		var date = new Date();
        date.setDate(date.getDate() + expiredays);
        document.cookie = escape(IdName) + "=" + escape(value) + "; expires=" + date.toUTCString();
    }

    function expireCookie(cookieName) {
        var cookies = document.cookie.split(';');
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i].trim();
            if (cookie.indexOf(cookieName + '=') === 0) {
                return true;
            }
        }
        return false
    }

    function checkCookie(cookieName) {
        var isCookieSet = expireCookie(cookieName);
        if (isCookieSet) {
            $("#" + cookieName).hide();
        } else {
            $("#" + cookieName).show();
        }
    }

    function FileDownLoad(tFileName) {

        var tDir = "BoardFilePath";

        document.getElementById("ifrDown").src = "/Common/FileDown.aspx?tDir=" + escape(tDir) + "&tFileName=" + escape(tFileName);

        return false;
    }

</script>

<div id="wrapper_popup">

    <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph" OnItemDataBound="lvList_ItemDataBound">
        <LayoutTemplate>
            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div id='layerPop<%# Eval("U") %>' class="layer-shadow" style='margin-top:<%# Eval("topMargin") %>px; margin-left:<%# Eval("leftMargin") %>px; width:<%# Eval("PWidth") %>px; height:<%# Eval("PHeight") %>px;'>
			    <h2><%# Eval("Title") %></h2>
			    <div class="popup-content" style='height: <%# GetPopupContentHeight(Eval("PHeight")) %>px;'>
				    
                    <div style="padding: 15px; border: solid 1px #bdbdbd;">
                        <asp:Literal ID="ltlNotice" runat="server"></asp:Literal>
                    </div>
                    <div style="float:left; padding-left:15px; padding-top: 10px; height: 30px;">
                        <asp:Literal ID="ltlFilePre" runat="server"></asp:Literal><a runat="server" id="aFile"><asp:Literal ID="ltlFile" runat="server"></asp:Literal></a>
                    </div>

			    </div>
		        <div class="b-area"><a href="#" onclick="closeNoOpen('layerPop<%# Eval("U") %>')" class="hour-close">오늘 하루 열지 않기</a></div>
		        <a href="#" onclick="closeLayer('layerPop<%# Eval("U") %>')"><img class="cancel" src="/images/icon_delete.gif" alt="X"></a>
	        </div>
        </ItemTemplate>
    </asp:ListView>
</div>

<iframe id="ifrDown" name="ifrDown" scrolling="no" frameborder="0" style="text-align: center; vertical-align: middle; border-style: none; margin: 0px; width: 0px; height: 0px"></iframe>