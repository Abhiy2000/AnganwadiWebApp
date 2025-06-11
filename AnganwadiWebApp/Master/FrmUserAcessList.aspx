<%@ Page Title="User Access Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmUserAcessList.aspx.cs" Inherits="ProjectManagement.Master.FrmUserAcessList" %>

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
    <%--   <h1 class="PageTitle">
        User Access List</h1>
    <br />
    <br />
    <br />--%>
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">
                        Division
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">
                        District
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>
                        CDPO
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged" />
                    </td>
                    <td>
                        BIT
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <hr />
            <asp:Button ID="btnSearch" runat="server" CssClass="Button" Text="Search" 
                onclick="btnSearch_Click" />
            <hr />
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GrdUserList" runat="server" CssClass="Grid" Width="95%" AutoGenerateColumns="False"
                            OnRowDataBound="GrdUserList_RowDataBound" OnSelectedIndexChanged="GrdUserList_SelectedIndexChanged">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:CommandField HeaderText="Modify" ShowSelectButton="True" SelectText="Modify">
                                    <HeaderStyle Width="60px" />
                                </asp:CommandField>
                                <asp:BoundField DataField="branchname" HeaderText="Branch">
                                    <HeaderStyle Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="userid" HeaderText="User Id">
                                    <HeaderStyle Width="220px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="var_usermst_userfullname" HeaderText="User Name">
                                    <HeaderStyle Width="350px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BrId" HeaderText="BrId">
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
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
