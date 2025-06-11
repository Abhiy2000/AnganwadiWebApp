<%@ Page Title="User Creation" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmUserCreation.aspx.cs" Inherits="ProjectManagement.User.FrmUserCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }
        function checkPassStrength() {
            var value = $('#<%=txtPassword.ClientID %>').val();
            var score = 0;
            var status = "";
            var specialChars = "<>@!#$%^&*()_+[]{}?:;|'\"\\,./~`-="
            if (value.toString().length >= 8) {

                if (/[a-z]/.test(value)) {
                    score += 1;
                }
                if (/[A-Z]/.test(value)) {
                    score += 1;
                }
                if (/\d/.test(value)) {
                    score += 1;
                }
                for (i = 0; i < specialChars.length; i++) {
                    if (value.indexOf(specialChars[i]) > -1) {
                        score += 1;
                    }
                }
            }
            else {
                score = 1;
            }

            if (score == 2) {
                status = status = "<span style='color:#CCCC00'>Medium</span>";
            }
            else if (score == 3) {
                status = "<span style='color:#0DFF5B'>Strong</span>";
            }
            else if (score >= 4) {
                status = "<span style='color:#009933'>Very Strong</span>";
            }
            else {

                status = "<span style='color:red'>Week</span>";
            }
            if (value.toString().length > 0) {
                $('#<%=lblPasswordStrength.ClientID %>').html("Status :<span> " + status + "</span>");
            }
            else {
                $('#<%=lblPasswordStrength.ClientID %>').html("");
            }
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
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table>
            <tr>
                <td style="width: 110px">
                    User ID<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtUserID" CssClass="TextBox" runat="server" Style="color: #808080;
                        background-color: #f6f6f6" />
                </td>
            </tr>
            <tr>
                <td>
                    User Name<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtUserName" CssClass="TextBox" runat="server" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtUserName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.-/ ">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td>
                    Designation<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesg" runat="server" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Password
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" CssClass="TextBox" runat="server" TextMode="Password" MaxLength="20" onKeyUp="checkPassStrength()" oncopy="return false" onpaste="return false" oncut="return false"/>                                  
                </td>
                <td>
                    Confirm Password
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" CssClass="TextBox" runat="server" TextMode="Password" MaxLength="20" oncopy="return false" onpaste="return false" oncut="return false"/>
                </td>
            </tr>
            <tr>
                <td class="txt">
                </td>
                <td class="style1">
                </td>
                <td>
                    <asp:Label ID="lblPasswordStrength" runat="server"></asp:Label>
                </td>
                <td class="txt">
                </td>
                <td class="style1">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Active From<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td class="style1">
                    <uc1:Date ID="ActiveFrmDt" runat="server" />
                </td>
                <td>
                    Active Till<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td class="style2">
                    <uc1:Date ID="ActiveTillDt" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Department<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table align="left" style="margin-left: 120px;">
            <tr>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" OnClick="BtnSubmit_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnCancel" runat="server" Text="Close" CssClass="Button" OnClick="BtnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%-- Loader--%>
            <div id="imgrefresh" class="loader transparent">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.GIF" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSubmit" />
        </Triggers>
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
