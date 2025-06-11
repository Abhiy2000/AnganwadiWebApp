<%@ Page Title="Master Search" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmMstSearch.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmMstSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Src="~/WebUserControls/GridViewMst.ascx" TagName="GridViewMst" TagPrefix="uc2" %>
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
        <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; border: none;">
            <table width="80%">
                <tr>
                    <td>
                        Division<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDiv" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDiv_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        District
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDist" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDist_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        CDPO
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        BIT
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBIT" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBIT_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Anganwadi
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Report Type<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRptType" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRptType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Project Type
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPrjType" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblsub" runat="server"> Sub-Type<span style="color: Red">&nbsp;* </span></asp:Label>
                    </td>
                    <td style="width: 10px">
                        <asp:Label ID="Label1" runat="server">:</asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdAuth" runat="server" GroupName="AuthType" Text="Authorzied"
                            Checked="true" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnauth" runat="server" GroupName="AuthType" Text="Unauthorzied"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:Label ID="lblaadhar" runat="server">Aadhar No</asp:Label>
                    </td>
                    <td style="width: 10px">
                        <asp:Label ID="Label2" runat="server">:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAadharNo" runat="server" CssClass="TextBox" MaxLength="12" Width="230px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtAadharNo" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdReg" runat="server" GroupName="RegType" Text="Registered"
                            Checked="true" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnreg" runat="server" GroupName="RegType" Text="Unregistered"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdWrker" runat="server" GroupName="DesgType" Text="Worker"
                            Checked="true" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdHelper" runat="server" GroupName="DesgType" Text="Helper"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="Button" Text="Search" OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                            Visible="false" Width="150px" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="PnlGrid" runat="server" Style="overflow: auto; border: none;width:999px;">
            <table width="100%">
                <tr>
                    <td>
                        <uc2:GridViewMst ID="GrdList" runat="server" CssClass="Grid" AutoGenerateColumns="true" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
