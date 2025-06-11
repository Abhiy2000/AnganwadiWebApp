<%@ Page Title="Bank Branch Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmBranchMst.aspx.cs" Inherits="AnganwadiLib.Masters.FrmBranchMst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <table align="left" cellspacing="5px">
            <tr>
                <td>
                    <asp:Label ID="lblBankName" runat="server" Text="*" ForeColor="white" align="left"></asp:Label>Bank
                    Name
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlBankname" runat="server" Width="100%" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <asp:Label ID="lblBranch1" runat="server" Text="*" ForeColor="white" align="left"></asp:Label>शाखेचे नाव
                </td>
                <td style="width:10px">:</td>
                <td>
                     <asp:DropDownList ID="ddlBranch" runat="server" Width="100%" CssClass="DropDown"></asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="lblBranch" runat="server" Text="*" ForeColor="white" align="left"></asp:Label>Branch
                    Name
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtBranch" runat="server" Width="200px" MaxLength="100" CssClass="TextBox"></asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                        FilterMode="ValidChars" TargetControlID="txtBranch" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="*" ForeColor="white" align="left"></asp:Label>IFSC
                    Code
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtIFSC" runat="server" Width="200px" MaxLength="11" CssClass="TextBox"></asp:TextBox>
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
        <table width="380px" cellspacing="7px">
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Cilck" />
                </td>
                <td width="5px">
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="Button" OnClick="btnCancel_Cilck" />
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
