<%@ Page Title="Retirement Report" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmPromotionRpt.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmPromotionRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
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
        <table align="left" cellpadding="20" cellspacing="20">
            <tr>
                <td>
                    Division
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    District
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged">
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
                        OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    BIT
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    From Date<span style="color: Red">&nbsp;* </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <uc1:Date ID="dtFrmDate" runat="server" />
                </td>
                <td>
                    To Date<span style="color: Red">&nbsp;* </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <uc1:Date ID="dtToDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 10px">
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="Button" Text="Search" 
                        onclick="btnSearch_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                        Visible="false" Width="150px" onclick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="PnlSerch" runat="server">
            <table>
                <tr>
                    <td style="height:30px;"></td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdRetirement" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="Division" HeaderText="Division"></asp:BoundField>
                                <asp:BoundField DataField="District" HeaderText="District"></asp:BoundField>
                                <asp:BoundField DataField="DDOCode" HeaderText="DDO Code"></asp:BoundField>
                                <asp:BoundField DataField="Anganwadi" HeaderText="Anganwadi"></asp:BoundField>
                                <asp:BoundField DataField="SevikaCode" HeaderText="Sevika Code"></asp:BoundField>
                                <asp:BoundField DataField="Sevika_Name" HeaderText="Sevika Name"></asp:BoundField>
                                <asp:BoundField DataField="DOJ" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}">
                                </asp:BoundField>
                                <asp:BoundField DataField="oldpayscele" HeaderText="Old Payscale" ItemStyle-Font-Bold="true"></asp:BoundField>
                                <asp:BoundField DataField="PromoteDate" HeaderText="Promote Date" DataFormatString="{0:dd-MMM-yyyy}">
                                </asp:BoundField>                                
                                <asp:BoundField DataField="newQualification" HeaderText="Qualification"></asp:BoundField>
                                <asp:BoundField DataField="newdesig" HeaderText="Designation"></asp:BoundField>
                                <asp:BoundField DataField="newpayscele" HeaderText="New Payscale"></asp:BoundField>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
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
</asp:Content>
