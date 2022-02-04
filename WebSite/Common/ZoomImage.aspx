<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="ZoomImage.aspx.cs" Inherits="Common_ZoomImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="Javascript">
    <!--
        function zoomIn()
        {
            var img = $("#<%=this.imgFile.ClientID %>");
            var width = img.width() * 1.1;
            var height = img.height() * 1.1;
            img.width(width);
            img.height(height);
            return false;
        }

        function zoomOut()
        {
            var img = $("#<%=this.imgFile.ClientID %>");
            var width = img.width() * 0.9;
            var height = img.height() * 0.9;
            img.width(width);
            img.height(height);
            return false;
        }
    
        // 이미지 마우스 끌기 기능
        var pre_x = 0, pre_y = 0;
        function set_value() {
            pre_x = event.x;
            pre_y = event.y;
        }

        function move() {
            var delta_x = pre_x - event.x;
            var delta_y = pre_y - event.y;

            pre_x = event.x;
            pre_y = event.y

            window.scrollBy(delta_x, delta_y);
        }

        function SetReSize(w, h)
        {
            var w2 = w;
            var h2 = h;
            if (w2 > 1200) {
                w2 = 1200;
            }
            if (h2 > 800) {
                h2 = 800;
            }
            console.log(h2);
            var width = parseInt(w2) + 50;
            var height = parseInt(h2) + 140;
            console.log(height);
            if (width < 260)
            {
                width = 260;
            }

            window.resizeTo(width, height);
        }

    // -->
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" Runat="Server">

<div style="height:50px;">
    <asp:Button ID="btnZoomIn" runat="server" Text="확 대" Width="110" CssClass="btn btn-info" OnClientClick="return zoomIn();" />
    <asp:Button ID="btnZoomOut" runat="server" Text="축 소" Width="110" CssClass="btn btn-info" OnClientClick="return zoomOut();" />
</div>
<div style="padding:0px 0px 0px 0px;">
	<asp:Image runat="server" ID="imgFile" ImageAlign="AbsMiddle" CssClass="imgCursor" BorderStyle="Solid" BorderColor="Gray" BorderWidth="0" style="position:absolute; cursor:pointer" onMouseOver="this.style.cursor='move';" onmouseout="this.style.cursor='auto';"  Ondrag="move();"  onMouseDown="set_value();" />
</div>
</asp:Content>
