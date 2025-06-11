<%@ Page Title="Anganwadi Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmAnganwadiMst.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmAnganwadiMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        function closeMsgPopupnew1() {
            $find("mpeMsg2").hide();
            debugger;
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
    </script>
    <style type="text/css">
        .transparent
        {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }
        .loader
        {
            position: fixed;
            text-align: center;
            height: 100%;
            width: 100%;
            top: 0;
            right: 0;
            left: 0;
            z-index: 9999999;
            background-color: #FFFFFF;
            opacity: 0.3;
            visibility: hidden;
        }
        .loader img
        {
            padding: 10px;
            position: fixed;
            top: 45%;
            left: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table align="left">
            <tr>
                <td>
                    Anganwadi Name<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td width="350px">
                    <asp:TextBox ID="txtAngnName" CssClass="TextBox" runat="server" MaxLength="50" />
                    <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtAngnName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                    </ajaxToolkit:FilteredTextBoxExtender>--%>
                </td>
                <td>
                    Anganwadi Code<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtAngnCode" CssClass="TextBox" runat="server" MaxLength="11" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtAngnCode" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Address<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td width="350px">
                    <asp:TextBox ID="txtAddress" CssClass="TextBox" runat="server" TextMode="MultiLine"
                        Height="50px" />
                </td>
                <td>
                    Email
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" CssClass="TextBox" runat="server" MaxLength="40" />
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="txtEmail" ErrorMessage="Invalid" Style="color: Red" ValidationExpression="(([_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+))"
                        ValidationGroup="A"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    PIN Code
                </td>
                <td>
                    :
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="TextBox" MaxLength="6"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtPinCode" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Mobile No
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td width="350px">
                    <asp:TextBox ID="txtMobNo" CssClass="TextBox" runat="server" MaxLength="10" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtMobNo" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td>
                    Phone No
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneNo" CssClass="TextBox" runat="server" MaxLength="10" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtPhoneNo" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Status<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:RadioButton ID="rbdActive" runat="server" GroupName="type" Text="Active" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbdInActive" runat="server" GroupName="type" Text="InActive" />
                </td>
                <td>
                </td>
                <td style="width: 10px">
                </td>
                <td width="350px">
                    <asp:DropDownList ID="ddlprjType" runat="server" CssClass="DropDown" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table width="450px">
            <tr>
                <td align="right">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" OnClick="BtnSubmit_Click" />
                </td>
                <td width="7px">
                </td>
                <td>
                    <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="Button" OnClick="BtnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Loader --%>
            <div id="imgrefresh" class="loader transparent">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.GIF" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server"
        TargetControlID="UpdatePanel1">
        <Animations>
        <OnUpdating>
               <Parallel duration="0">
                    <ScriptAction Script="InProgress();" /> 
               </Parallel>
            </OnUpdating>
            <OnUpdated>
               <Parallel duration="0">
                   <ScriptAction Script="onComplete();" /> 
               </Parallel>
            </OnUpdated>
        </Animations>
    </cc1:UpdatePanelAnimationExtender>
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 460px;
        height: 200px; display: none;">
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
    <cc1:ModalPopupExtender ID="popMsg1" runat="server" BehaviorID="mpeMsg2" TargetControlID="hdnPop6"
        PopupControlID="pnlMessage2" CancelControlID="imgClose3">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop6" runat="server" />
    <asp:Panel ID="pnlMessage2" runat="server" CssClass="Popup" Style="width: 460px;
        height: 200px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="imgClose3" ToolTip="Close" runat="server" Style="z-index: -1; float: right;
            margin-top: -15px; margin-right: -15px;" onclick="closeMsgPopupNew1();" ImageUrl="~/Images/closebtn.png" />
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
                        <asp:Label ID="lblPopupResponse1" runat="server" Font-Bold="true" Text="Do you want to save Anganwadi in InActive Mode ?"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" CssClass="Button" Text="Yes" OnClick="btnYes_Click" />
                        <%-- <asp:Button ID="btnClodeMsg" runat="server" CssClass="Button" Text="Close" />--%>
                        <input id="btnClodeMsg1" class="Button" runat="server" type="button" value="No" style="width: 100px;"
                            onclick="closeMsgPopupnew1();" />
                        <asp:Label ID="lblredirect1" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
