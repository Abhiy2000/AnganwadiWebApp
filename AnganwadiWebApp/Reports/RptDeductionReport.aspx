<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RptDeductionReport.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptDeductionReport" %>

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
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 97%; height: 468px; overflow: auto;">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td style="width: 90px">Division<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">District
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td>CDPO
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_SelectedIndexChanged" />
                    </td>
                    <td>BIT
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_SelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <div>
                <table align="left">
                    <tr>
                        <td style="width: 90px">Month Date
                        </td>
                        <td style="width: 10px">:</td>
                        <td width="350px">
                            <uc1:Date ID="dtSalDate" runat="server" />

                        </td>
                        <td>
                            <asp:RadioButton ID="rbdSummery" runat="server" Text="Summery" GroupName="SevikaType" Checked="true" />
                            &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdDetail" runat="server" Text="Detail" GroupName="SevikaType" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="BtnSubmit" runat="server" Text="Search" CssClass="Button"
                                OnClick="BtnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                                OnClick="btnExport_Click" Width="120px" />
                        </td>

                    </tr>
                    <tr style="height: 20px">
                    </tr>
                    <tr>
                        <td colspan="5">
                            <div id="divDetails" runat="server">
                                <asp:GridView ID="GrdDeductionReport" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="true" PageSize="10" OnPageIndexChanging="GrdDeductionReport_PageIndexChanging">
                                    <RowStyle CssClass="GrdRow"></RowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="divname" HeaderText="Division"></asp:BoundField>
                                        <asp:BoundField DataField="distname" HeaderText="District"></asp:BoundField>
                                        <asp:BoundField DataField="cdponame" HeaderText="CDPO Name"></asp:BoundField>
                                        <asp:BoundField DataField="cdpocode" HeaderText="CDPO Code"></asp:BoundField>
                                        <asp:BoundField DataField="Beneficiary" HeaderText="Beneficiary"></asp:BoundField>
                                        <asp:BoundField DataField="aadharno" HeaderText="Aadhar No"></asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                                </asp:GridView>
                            </div>
                            <div id="divSummery" runat="server">
                                <asp:GridView ID="GrdSummery" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                                    AllowPaging="true" PageSize="10">
                                    <RowStyle CssClass="GrdRow"></RowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="divname" HeaderText="Division"></asp:BoundField>
                                        <asp:BoundField DataField="distname" HeaderText="District"></asp:BoundField>
                                        <asp:BoundField DataField="cdponame" HeaderText="CDPO Name"></asp:BoundField>
                                        <asp:BoundField DataField="cdpocode" HeaderText="CDPO Code"></asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
                                    </Columns>
                                    <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:PostBackTrigger ControlID="GrdDeductionReport" />
                    <asp:PostBackTrigger ControlID="GrdSummery" />
                </Triggers>
            </asp:UpdatePanel>
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
</asp:Content>
