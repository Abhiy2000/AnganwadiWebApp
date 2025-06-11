<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmAdditionalPayMst.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmAdditionalPayMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 923px;
            height: 269px;
        }
        .rbl input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }
    </style>
    <script type="text/javascript">
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
    <div class="Panel">
        <div class="boxHead">
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
        </div>
        <br />
        <div class="panel-body">
            <table class="style1">
                <tr>
                    <td>
                        Aadhar No
                    </td>
                    <td colspan="6">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtAdharNo" placeholder="Input Aadhar Number"
                            Width="205px"></asp:TextBox>
                        &nbsp; &nbsp;<asp:Button runat="server" ID="btnSearch" class="Button" Text="Search"
                            OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Division
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtDivision" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        District
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtDistrict" Width="205px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        CDPO
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtCDPO" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        BIT
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtBitCompId" Width="205px"></asp:TextBox>
                        <asp:TextBox CssClass="TextBox" runat="server" ID="txtCompId" Width="205px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtName" Width="205px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        Scheme Specific Id
                    </td>
                    <td colspan="2">
                        :&nbsp; &nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtSchemeSpecificId"
                            Width="205px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Type
                    </td>
                    <td colspan="6">
                        :&nbsp; &nbsp;
                        <asp:CheckBox runat="server" Text="Maternity" Value="M" ID="chkMaternity" OnCheckedChanged="chkMaternity_CheckedChanged"
                            AutoPostBack="true"></asp:CheckBox>
                        &nbsp;
                        <asp:CheckBox runat="server" Text="Additional Pay" ID="chkAdditionalPay" Value="A"
                            OnCheckedChanged="chkAdditionalPay_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                        &nbsp;
                        <asp:CheckBox Text="Tribal" ID="chkTribal" runat="server" Value="T" OnCheckedChanged="chkTribal_CheckedChanged"
                            AutoPostBack="true"></asp:CheckBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        From Date
                    </td>
                    <td colspan="2">
                        <uc1:Date ID="dtFrom" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        To Date
                    </td>
                    <td colspan="2">
                        <uc1:Date ID="dtTo" runat="server" />
                    </td>
                </tr>
            </table>
            <center>
                <br />
                <asp:HiddenField ID="hdnSavikaId" runat="server" />
                <asp:Button runat="server" ID="btnDelete" class="Button" Text="Delete" OnClick="btnDelete_Click"
                    Style="margin-right: 15px;" />
                <asp:Button runat="server" ID="btnSubmit" class="Button" Text="Submit" OnClick="btnSubmit_Click"
                    Style="margin-right: 15px;" />
                <asp:Button runat="server" ID="btnCancel" class="Button" Text="Cancel" OnClick="btnCancel_Click" />
            </center>
        </div>
    </div>
    <div>
        <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
            PopupControlID="pnlMessage1" CancelControlID="imgClose2">
        </cc1:ModalPopupExtender>
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
    </div>
</asp:Content>
