<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RptLICReportDownload.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptLICReportDownload" %>

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
        function closeMsgPopupnew1() {
            $find("mpeMsg2").hide();
            debugger;
            return false;
        }

    </script>
    <style type="text/css">
        .style1 {
            width: 317px;
        }

        .tab-content {
            padding-bottom: 23px !important;
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
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"> LIC Reports Download </asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 97%; height: 500px; overflow: auto">
        <table align="left" cellpadding="20" cellspacing="20">
            <tr>
                <td>Division
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="padding-left: 30px;">District
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>CDPO
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCDPO_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="padding-left: 30px;">BIT
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBeat_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>From Date<span style="color: Red">&nbsp;* </span>
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <uc1:Date ID="DtFromDate" runat="server" />
                </td>
                <td style="padding-left: 30px;">To Date <span style="color: Red">&nbsp;* </span>
                </td>
                <td>:
                </td>
                <td>
                    <uc1:Date ID="DtToDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Sevika 
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbSevika" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Text="Internal" style="margin-right: 20px" Value="I" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="External" style="margin-right: 20px;" Value="E"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>

            <tr>
                <td>Status
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbstatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Death" style="margin-right: 20px" Value="DT" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Other" style="margin-right: 20px; margin-left: 15px;" Value="OT"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>
            <tr>
                <td>Export To
                </td>
                <td style="width: 10px">:
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbdownload" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="PDF" style="margin-right: 20px" Value="P" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Excel" style="margin-right: 20px; margin-left: 28px;" Value="E"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>

            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Style="margin-top: 8px;"
                                CssClass="Button" OnClick="btnSubmit_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
    &nbsp;&nbsp;&nbsp;
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
                        <input id="btnClodeMsg" class="Button" runat="server" type="button" value="OK" style="width: 100px;"
                            onclick="closeMsgPopupnew();" />
                        <asp:Label ID="lblredirect" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
