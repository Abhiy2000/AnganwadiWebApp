<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RptPolicyUpdateRpt.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptPolicyUpdateRpt" %>

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
    <style type="text/css">
        .style1 {
            width: 72px;
        }

        .style2 {
            width: 85px;
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
        .transparent {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }

        .loader {
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

            .loader img {
                padding: 10px;
                position: fixed;
                top: 45%;
                left: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" CssClass="Button" Text="Search" OnClick="btnSearch_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                        Width="116px" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <br />--%>
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 1100px; height: 468px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">Division<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td class="style2">District
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>CDPO
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged" />
                    </td>
                    <td class="style2">BIT
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>Anganwadi
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAnganID" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAnganID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="style2">Project Type
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPrjType" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <%--   <hr />
        <asp:Button ID="btnNew" runat="server" Text="Add New" CssClass="Button" OnClick="btnNew_Click" />
        <hr />--%>
            <table>
                <tr>
                    <td>
                        <br />
                        <asp:UpdatePanel ID="UpdatePan" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="search" />
                            </Triggers>
                        </asp:UpdatePanel>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="search" runat="server" Text="Search" CssClass="Button" OnClick="search_Click"
                            Width="105px" />
                    </td>
                    <td width="7px"></td>
                    <td align="center">
                        <br />
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                            Width="116px" OnClick="btnExport_Click1" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel ID="Panel1" runat="server" Width="1050px" Style="overflow: auto">
                <table width="80%">
                    <tr>
                        <td class="style1">
                            <uc2:GridViewMst ID="GrdSevikaList" runat="server" CssClass="Grid" AutoGenerateColumns="true" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
        <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
            PopupControlID="pnlMessage1" CancelControlID="imgClose2">
        </ajaxToolkit:ModalPopupExtender>
        <asp:HiddenField ID="hdnPop5" runat="server" />
        <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 430px; height: 160px; display: none;">
            <%-- display: none; --%>
            <asp:Image ID="imgClose2" ToolTip="Close" runat="server" Style="z-index: -1; float: right; margin-top: -15px; margin-right: -15px;"
                onclick="closeMsgPopupNew();" ImageUrl="~/Images/closebtn.png" />
            <center>
                <br />
                <table width="100%">
                    <tr>
                        <td align="left" colspan="3" style="color: #094791; font-weight: bold;">&nbsp;&nbsp;&nbsp;Message
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
</asp:Content>
