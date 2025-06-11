<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridViewMst.ascx.cs" Inherits="GridUserControl.WebUserControls.GridViewMst" %>
<asp:Label ID="lblPageNumber" runat="server" Text="Page Number : "></asp:Label> 
<asp:DropDownList ID="ddlPageNo" runat="server" Width="60px" AutoPostBack="true" OnSelectedIndexChanged="ddlPageNo_OnSelectedIndexChanged"></asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="lblTotalRecords" runat="server" Text=""></asp:Label> 
<asp:GridView ID="GridView1" runat="server" Width="100%" style="margin-top:8px"></asp:GridView>

