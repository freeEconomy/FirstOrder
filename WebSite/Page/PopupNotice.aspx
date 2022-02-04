<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="PopupNotice.aspx.cs" Inherits="Page_PopupNotice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Control/PopupNotice.ascx" TagName="PopupNotice" TagPrefix="pn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        //<![CDATA[
        
        //]]>
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <pn:PopupNotice ID="PopupNotice" runat="server" />

</asp:Content>
