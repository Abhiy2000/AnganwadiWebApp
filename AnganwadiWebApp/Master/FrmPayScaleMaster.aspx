<%@ Page Title="PayScale Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmPayScaleMaster.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmPayScaleMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 300px;
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
        function change() {
            $("#<%=btnTemp.ClientID%>").click();
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
                </td>
                <td style="width: 10px">
                </td>
                <td>
                    <asp:TextBox ID="txtPayCode" CssClass="TextBox" runat="server" Visible="false" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Type<span style="color: Red">* </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjname" runat="server" Width="100%" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    Education <span style="color: Red">* </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlEduName" runat="server" Width="100%" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Payscale<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtPayscale" CssClass="TextBox" runat="server" MaxLength="30" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtPayscale" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    Designation <span style="color: Red">* </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesg" runat="server" Width="100%" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Wages<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtWages" CssClass="TextBox" runat="server" MaxLength="5" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtWages" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                   From Effective Date<span style="color: Red"> *</span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                   <uc1:Date ID="dtFrom" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Central<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtCentral" CssClass="TextBox" runat="server" MaxLength="5" Width="250px"
                        onchange="change()" />%
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtCentral" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    State<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtState" CssClass="TextBox" runat="server" MaxLength="5" Width="250px"
                        ReadOnly="true" />%
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtState" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Central Amount
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtCentralAmt" CssClass="TextBox" runat="server" MaxLength="5" Width="250px"
                        ReadOnly="true" />
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    State Amount
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtStateAmt" CssClass="TextBox" runat="server" MaxLength="5" Width="250px"
                        ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td>
                    Fixed<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtFixed" CssClass="TextBox" runat="server" MaxLength="5" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtFixed" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    Status<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:RadioButton ID="rbdActive" runat="server" GroupName="ActiveInactive" Text="Active"
                        Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbdInactive" runat="server" GroupName="ActiveInactive" Text="Inactive" />
                </td>
            </tr>
            <tr>
                <td>
                    Experience From<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtExpFrm" CssClass="TextBox digit" runat="server" MaxLength="5"
                        Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtExpFrm" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 60px">
                </td>
                <td>
                    Experience To<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtExpTo" CssClass="TextBox" runat="server" MaxLength="5" Width="250px" />
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtExpTo" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
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
        <br />
        <br />
        <br />
        <table width="210px" style="margin-left:120px">
            <tr>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                        OnClick="BtnSubmit_Click" />
                    <asp:Button runat="server" ID="btnTemp" Style="display: none;" OnClick="btnTemp_Click" />
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
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 490px;
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
                        <asp:Label ID="lblPopupResponse1" runat="server" Font-Bold="true" Text="Do you want to save PayScale in InActive Mode ?"></asp:Label>
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
