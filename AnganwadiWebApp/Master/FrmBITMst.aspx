<%@ Page Title="BIT Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmBITMst.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmBITMst" %>

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
    </script>
    <style type="text/css">
        .style2
        {
            width: 110px;
        }
        .style3
        {
            width: 120px;
        }
        .style4
        {
            width: 270px;
        }
    </style>
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
        <table width="80%">
            <tr>
                <td style="width: 100px">
                    <%--State<span style="color: Red"> * </span>--%>
                </td>
                <td style="width: 10px">
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="DropDown" AutoPostBack="true"
                        Visible="false" Width="210px" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style2">
                    <%--Division<span style="color: Red"> * </span>--%>
                </td>
                <td style="width: 10px">
                </td>
                <td class="style4">
                    <asp:DropDownList ID="ddlDiv" runat="server" CssClass="DropDown" Width="210px" AutoPostBack="true"
                        Visible="false" OnSelectedIndexChanged="ddlDiv_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <%-- District<span style="color: Red"> * </span>--%>
                </td>
                <td style="width: 10px">
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlDist" runat="server" CssClass="DropDown" Width="210px" Visible="false"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlDist_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style2">
                    <%-- CDPO<span style="color: Red"> * </span>--%>
                </td>
                <td style="width: 10px">
                </td>
                <td class="style4">
                    <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" Width="210px" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="130px">
                    State Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    Division Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtDiv" runat="server" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="130px">
                    District Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtDist" runat="server" CssClass="TextBox"></asp:TextBox>
                </td>
                <td>
                    CDPO Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtCDPO" runat="server" CssClass="TextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    BIT Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                </td>
                <td class="style2">
                    BIT Code <span style="color: Red">* </span>
                </td>
                <td>
                    :
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtCode" runat="server" CssClass="TextBox" MaxLength="9"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtCode" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Supervisor Name
                </td>
                <td valign="top">
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="TextBox" MaxLength="100"
                        Width="210px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtSupervisor" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td valign="top">
                    Address<span style="color: Red"> * </span>
                </td>
                <td valign="top">
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" Height="51px" MaxLength="100"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Phone Number
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtPhoneno" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtPhoneno" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td>
                    Email ID
                </td>
                <td valign="top">
                    :
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="TextBox" MaxLength="40"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                        ControlToValidate="txtEmailId" ErrorMessage="Invalid" Style="color: Red" ValidationExpression="(([_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+))"
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
                <td>
                </td>
                <td style="width: 10px">
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtIFSC" runat="server" CssClass="TextBox" Width="210px" Visible="false"></asp:TextBox>&nbsp;
                    <asp:LinkButton ID="LinkSerchBankBranch" runat="server" OnClick="LinkSerchBankBranch_OnClick"
                        Visible="false">Validate IFSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="DropDown"
                        Width="210px" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlBankBranch" runat="server" CssClass="DropDown" Width="210px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
                <td class="style2">
                </td>
                <td>
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" MaxLength="20" Width="210px"
                        Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="DropDown" Width="210px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table width="500px">
            <tr>
                <td align="right">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                        OnClick="BtnSubmit_Click" />
                </td>
                <td width="5px">
                </td>
                <td align="left">
                    <asp:Button ID="BtnBack" runat="server" Text="Back" CssClass="Button" ValidationGroup="A"
                        OnClick="BtnBack_Click" />
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
