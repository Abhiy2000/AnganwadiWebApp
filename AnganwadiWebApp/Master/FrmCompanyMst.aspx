<%@ Page Title="State Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmCompanyMst.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmCompanyMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 286px;
        }
    </style>
    <script type="text/javascript" language="javascript">
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
        <table>
            <asp:Panel ID="pnl1" runat="server" Visible="false">
                <tr>
                    <td style="width: 110px">
                        Category
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td colspan="4">
                        <asp:RadioButton ID="RdoOrg" runat="server" AutoPostBack="True" GroupName="A" OnCheckedChanged="RdoOrg_CheckedChanged"
                            Text="State" Visible="true" />
                        &nbsp;<asp:RadioButton ID="RdoDiv" runat="server" AutoPostBack="True" GroupName="A"
                            OnCheckedChanged="RdoDiv_CheckedChanged" Text="Division" />
                        &nbsp;
                        <asp:RadioButton ID="RdoDistrict" runat="server" AutoPostBack="True" GroupName="A"
                            OnCheckedChanged="RdoDistrict_CheckedChanged" Text="District" />
                        &nbsp;
                        <asp:RadioButton ID="RdoCDPO" runat="server" AutoPostBack="True" GroupName="A" OnCheckedChanged="RdoCDPO_CheckedChanged"
                            Text="CDPO/Project" />
                        &nbsp;
                        <asp:RadioButton ID="RdoBeat" runat="server" AutoPostBack="True" GroupName="A" OnCheckedChanged="RdoBeat_CheckedChanged"
                            Text="Beat" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        Parent
                    </td>
                    <td>
                        <br />
                        :
                    </td>
                    <td class="style1">
                        <br />
                        <br />
                        <asp:DropDownList ID="ddlParent" runat="server" AutoPostBack="True" CssClass="DropDown"
                            OnSelectedIndexChanged="ddlParent_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
            </asp:Panel>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    State Name<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtName" runat="server" CssClass="TextBox" MaxLength="100"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtName" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 10px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Branch Name<span style="color: Red"> * </span>
                    <%--ICDS Name<span style="color: Red"> * </span>--%>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="TextBox" MaxLength="20"></asp:TextBox>
                </td>
                <td>
                    State Code <span style="color: Red">* </span>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="TextBox" MaxLength="2"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtCode" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td height="5px">
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Address<span style="color: Red"> * </span>
                </td>
                <td valign="top">
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" Height="51px" MaxLength="100"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
                <td valign="top">
                    Phone Number
                </td>
                <td valign="top">
                    :
                </td>
                <td valign="top">
                    <asp:TextBox ID="txtPhoneno" runat="server" CssClass="TextBox" MaxLength="10"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtPhoneno" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    Email ID<span style="color: Red"> * </span>
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtEmailId" runat="server" CssClass="TextBox" MaxLength="40"></asp:TextBox>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                        ControlToValidate="txtEmailId" ErrorMessage="Invalid" Style="color: Red" ValidationExpression="(([_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+))"
                        ValidationGroup="A"></asp:RegularExpressionValidator>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Bank
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="DropDown"
                        Width="210px" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    IFSC
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td width="350px">
                    <asp:TextBox ID="txtIFSC" runat="server" CssClass="TextBox" Width="210px"></asp:TextBox>&nbsp;
                    <asp:LinkButton ID="LinkSerchBankBranch" runat="server" OnClick="LinkSerchBankBranch_OnClick">Validate IFSC</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    Bank Branch
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlBankBranch" runat="server" CssClass="DropDown" Width="210px" Visible="false">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBankBranch" runat="server" CssClass="TextBox" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    Account No.
                </td>
                <td>
                    :
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="TextBox" MaxLength="20" Width="210px"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtAccountNo" ValidChars="0123456789">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="DropDown" Width="210px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table width="380px">
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
