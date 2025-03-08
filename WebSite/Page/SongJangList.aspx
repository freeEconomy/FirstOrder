<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MainPop.master" AutoEventWireup="true" CodeFile="SongJangList.aspx.cs" Inherits="Page_SongJangList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

    <div style="padding:5px;">
        <div style="height:5px;"></div>
        
        <asp:ListView ID="lvList" runat="server" ItemPlaceholderID="iph">
            <LayoutTemplate>
		<table width="100%" cellpadding="1" cellspacing="1" border="0" class="defaultTable" id="zonelistProductTable">
            <tr align="center" class="defaultTitleTR1">
                <td width="40">NO.</td>
                <td width="200">송장번호</td>
            </tr>
            <asp:PlaceHolder ID="iph" runat="server"></asp:PlaceHolder>
		</table>
            </LayoutTemplate>
            <ItemTemplate>
            <tr class="defaultDataTR">
                <td><%# Eval("Bjhd3_Seqx") %></td>
                <td><%# Eval("Bjhd3_SongJangNox") %></td>
            </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
            <table border="0" width="100%" cellspacing="1" cellpadding="1" class="defaultTable">
                <tr id="trNotData" runat="server">
				    <td style="background-color:White; text-align:center; font-weight:bold; height:30px">송장번호가 존재하지 않습니다.</td>
                </tr>
            </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>

</asp:Content>
