<%@ Page Title="Monthly Attendence Report" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="RptMonthlyAttendence.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptMonthlyAttendence" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <%-- <asp:Button ID="btnNew" runat="server" Text="New User" CssClass="Button" OnClick="btnNew_Click" />
            <hr />--%>
            <table>
                <tr>
                    <td>
                        Salary Date :
                    </td>
                    <td style="padding-left:10px">
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="10px">
                    </td>
                </tr>
                <tr>
                    <td>
                        Report Type :
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdAll" runat="server" Text="All" GroupName="RptType" Checked="true" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdAuth" runat="server" Text="Authorized" GroupName="RptType" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnAuth" runat="server" Text="Unauthorized" GroupName="RptType" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        Present Days :
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdAllDays" runat="server" Text="All" GroupName="RptPDay" Checked="true" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rbdZero" runat="server" Text="Zero Day" GroupName="RptPDay" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="Button" Text="Search" runat="server" OnClick="btnSearch_Click" />
                    </td>
                    <td style="padding-left:10px">
                        <asp:Button ID="btnExport" CssClass="Button" Text="Export To Excel" runat="server" Visible="false" width="150px"
                            OnClick="btnExport_Click" />

                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblrowCount" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <table width="80%">
            <tr>
                <td class="style1">
                    <asp:GridView ID="grdMonAttendRpt" runat="server" CssClass="Grid" Width="100%" 
                    AllowPaging="True" PageSize="10"
                        AutoGenerateColumns="False" onrowdatabound="grdMonAttendRpt_RowDataBound" 
                        onpageindexchanging="grdMonAttendRpt_PageIndexChanging">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <%--<asp:CommandField HeaderText="Select" ShowSelectButton="True">
                                <HeaderStyle Width="60px" />
                            </asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Sr. No.">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:BoundField DataField="sevikacode" HeaderText="Sevika Code">
                                <HeaderStyle Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sevikaname" HeaderText="Sevika Name">
                                <HeaderStyle Width="600px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="designation" HeaderText="Designation">
                                <HeaderStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="presentdays" HeaderText="Present Days">
                                <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="absentdays" HeaderText="Absent Days">
                                <HeaderStyle Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="centralAmt" HeaderText="Central" Visible="false">
                                <HeaderStyle Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="stateAmt" HeaderText="State" Visible="false">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fixedAmt" HeaderText="Fixed" Visible="false">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status" Visible="true">
                                <HeaderStyle Width="90px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
