<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RptDiffAbstractReport.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptDiffAbstractReport" %>


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
                        <asp:DropDownList ID="ddlDiv" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Designation<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddldesg" runat="server" CssClass="DropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Salary Month<span style="color: Red">&nbsp;* </span>
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="110px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Report Type
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:RadioButton ID="rbd40" runat="server" Text="40%" GroupName="rptType" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbd60" runat="server" Text="60%" GroupName="rptType" />
                       
                    </td>
                </tr>
                 <tr>
                    <td>
                        Sevika
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdRegistered" runat="server" Text="Registered" GroupName="SevikaType"
                            Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnregistered" runat="server" Text="Unregistered" GroupName="SevikaType" />
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
                        <asp:Button ID="btnSearch" runat="server" CssClass="Button" Text="Search" Visible="false"
                            OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" Width="100px"
                            OnClick="BtnSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                            Visible="true" Width="150px" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlBillNo" runat="server" Style="overflow: auto; border: none;">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="grdBillno" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                Width="100%">
                                <RowStyle CssClass="GrdRow"></RowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPDF" runat="server" OnClick="lnkPDF_Click">PDF</asp:LinkButton>
                                            <%--   <asp:Label ID="lblEmpId" runat="server" Text='Select'></asp:Label>--%>
                                            <asp:HiddenField ID="hdnCentral" runat="server" Value='<%# Bind("Central") %>' />
                                            <asp:HiddenField ID="hdnState" runat="server" Value='<%# Bind("State") %>' />
                                            <asp:HiddenField ID="hdnFixed" runat="server" Value='<%# Bind("Fixed") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkExl" runat="server" OnClick="lnkExl_Click">Excel</asp:LinkButton>
                                            <%--   <asp:Label ID="lblEmpId" runat="server" Text='Select'></asp:Label>--%>
                                            <asp:HiddenField ID="hdnCentral1" runat="server" Value='<%# Bind("Central") %>' />
                                            <asp:HiddenField ID="hdnState2" runat="server" Value='<%# Bind("State") %>' />
                                            <asp:HiddenField ID="hdnFixed3" runat="server" Value='<%# Bind("Fixed") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <%--     <asp:CommandField HeaderText="Export To PDF" ShowSelectButton="True" SelectText="Select">
                                        <HeaderStyle Width="60px" />
                                    </asp:CommandField>--%>
                                    <%-- <asp:CommandField HeaderText="Export To Excel" ShowSelectButton="True" SelectText="Select">
                                        <HeaderStyle Width="60px" />
                                    </asp:CommandField>--%>
                                    <asp:BoundField DataField="Central" HeaderText="Central Bill No">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="State" HeaderText="State Bill No">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fixed" HeaderText="Fixed Bill No">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <asp:Panel ID="PnlGrid" runat="server" Style="overflow: auto; border: none;">
            <table width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="GrdAbstrct" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            Visible="false" AllowPaging="true" PageSize="10">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="Division" HeaderText="Division"></asp:BoundField>
                                <asp:BoundField DataField="District" HeaderText="District"></asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation"></asp:BoundField>
                                <asp:BoundField DataField="PayScale" HeaderText="PayScale"></asp:BoundField>
                                <asp:BoundField DataField="Wage_Rate_Per_Day" HeaderText="Wage Rate per day"></asp:BoundField>
                                <asp:BoundField DataField="No_of_Present_Days" HeaderText="No. of Present days">
                                </asp:BoundField>
                                <asp:BoundField DataField="No_of_Beneficiaries" HeaderText="No. of Beneficiaries">
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total Amount"></asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
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
            <asp:PostBackTrigger ControlID="grdBillno" />
            <asp:PostBackTrigger ControlID="BtnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>