<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarDate.ascx.cs" Inherits="Control_CalendarDate" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<script type="text/javascript" language="javascript">
	jQuery(function ($) {
		$("#<%=this.txtDate.ClientID%>").mask("9999-99-99");
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {

        $("#<%=this.txtDate.ClientID%>").mask("9999-99-99");

    });
</script>
<asp:TextBox ID="txtDate" runat="server" Width="80" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" />
<cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
	Enabled="True" TargetControlID="txtDate" 
	PopupButtonID="imgCal">
</cc1:CalendarExtender>
