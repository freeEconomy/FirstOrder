<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="Common_FileUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
.visibleFalse {
    visibility: hidden;
    width: 0px;
    height: 0px;
}
</style>
</head>
<body style="margin:0px 0px 0px 0px;">
    <form id="form1" runat="server">
    <div style="vertical-align:top; padding-top:5px;">
    	<asp:FileUpload ID="fuFile" runat="server" CssClass="inputfile" Width="400" />
		<asp:Button ID="btnUpload" runat="server" Text="파일저장" CssClass="visibleFalse" onclick="btnUpload_Click" />
    </div>
    </form>
</body>
</html>
