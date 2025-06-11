<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmSevikaEdtReportScrn.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmSevikaEdtReportScrn" %>

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
        p {
            margin: 0 0 1px;
        }

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
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium" Visible="true"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 568px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">Division
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">District
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
                    <td>BIT
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
                    <td>From Date
                    </td>
                    <td>:
                    </td>
                    <td>
                        <uc1:Date ID="DtFromDate" runat="server" width="350px" />
                    </td>
                    <td>To Date
                    </td>
                    <td>:
                    </td>
                    <td>
                        <uc1:Date ID="DtToDate" runat="server" width="350px" />
                    </td>
                </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="Button" Text="Search" runat="server" OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnExport" CssClass="Button" Text="Export  Excel" runat="server" Visible="false"
                            OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <hr />
        <table>
            <tr>

                <asp:GridView ID="GrdSevikaEdtStatusRpt" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found." EmptyDataRowStyle-HorizontalAlign="Center"
                    FooterStyle-HorizontalAlign="Right">

                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No.">
                            <HeaderStyle Width="70px" />
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="divname" HeaderText="Division" />
                        <asp:BoundField DataField="distname" HeaderText="District" />
                        <asp:BoundField DataField="cdponame" HeaderText="CDPO" />
                        <asp:BoundField DataField="bitbitname" HeaderText="BIT" />

                        <asp:BoundField DataField="sevikaname" HeaderText="Sevika Details" />
                        <asp:TemplateField HeaderText="Old value">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <p>DOB:<asp:Label ID="lblOldDob" runat="server" Text='<%# Eval("old_dob") %>' /></p>
                                <p>Join Date:<asp:Label ID="lblOldDoJ" runat="server" Text='<%# Eval("old_joindate") %>' /></p>
                                <p>Aadhar No:<asp:Label ID="lblOldAadharNo" runat="server" Text='<%# Eval("old_aadharno") %>' /></p>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Change Value">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <p>
                                    <asp:Label ID="lblNewDob" runat="server" Text='<%# Eval("new_dob") %>' />
                                </p>
                                <p>
                                    <asp:Label ID="lblNewDoJ" runat="server" Text='<%# Eval("new_joindate") %>' />
                                </p>
                                <p>
                                    <asp:Label ID="lblNewAadharNo" runat="server" Text='<%# Eval("new_aadharno") %>' />
                                </p>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>

            </tr>
        </table>
    </div>
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
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
