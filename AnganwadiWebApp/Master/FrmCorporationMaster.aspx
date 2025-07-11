﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmCorporationMaster.aspx.cs" Inherits="ProjectManagement.Master.FrmCorporationMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function closeMsgPopupnew() {
            $find("mpeMsg1").hide();
            debugger;
            var el = document.getElementById('ContentPlaceHolder1_lblredirect');
            var aa = el.innerHTML;
            if (aa != "") {
                window.location.href = aa;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <b>
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label></b>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table align="left">
            <tr>
                <td>
                    Parent Id<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlParentId" runat="server" AutoPostBack="true" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Corporation Name<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtCorporationName" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Class<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        <table align="left">
            <tr>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                        OnClick="BtnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 430px;
        height: 160px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="imgClose2" ToolTip="Close" runat="server" Style="z-index: -1; float: right;
            margin-top: -15px; margin-right: -15px;" onclick="closeMsgPopupNew();" ImageUrl="~/Images/closebtn.png" />
        <center>
            <br />
            <table width="100%">
                <tr>
                    <td align="left" colspan="3" style="color: #094791; font-weight: bold;">
                        &nbsp;&nbsp;&nbsp;Message
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                    </td>
                </tr>
            </table>
            <table width="90%">
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="lblPopupResponse" runat="server" Font-Bold="true" Text=""></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <%-- <asp:Button ID="btnClodeMsg" runat="server" CssClass="Button" Text="Close" />--%>
                        <input id="btnClodeMsg" class="Button" runat="server" type="button" value="OK" style="width: 100px;"
                            onclick="closeMsgPopupnew();" />
                        <asp:Label ID="lblredirect" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
