<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="DefaultError.aspx.cs" Inherits="AnganwadiWebApp.DefaultError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Panel" style="width: 97%; height: 490px; overflow: auto; text-align: center;">
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="Small" ForeColor="#FF6600"
            Text="Something Bad happened, Please contact Administrator!!!!"></asp:Label>
        <br />
        <br />
        <asp:Button ID="BtnOK" CssClass="Button" runat="server" Text="OK" OnClick="BtnOK_Click"
            Width="147px" />
    </div>
</asp:Content>
