<%@ Page Title="Pay Sheet List" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmAngWisePaySheetList.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmAngWisePaySheetList" %>

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
    <div class="Panel" style="width: 1125px; height: 500px; overflow: auto">
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
            <table>
                <%--<tr>
                <td>
                     Date :
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="100px">
                    </asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown"  Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>--%>
                <tr>
                    <td style="width: 90px">
                        For the Month
                    </td>
                    <td style="width: 10px">
                        :
                    </td>
                    <td class="style1">
                        <%--<uc1:Date ID="txtFromdate" runat="server" width="350px" />--%>
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
                    </td>
                    <td width="10px">
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="Button" Text="Search" runat="server" OnClick="btnSearch_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExport" CssClass="Button" Text="Export To Excel" runat="server"
                            Visible="false" Width="150px" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="PnlGrd" runat="server" Width="90%">
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:GridView ID="grdPaySheetRpt" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10" ShowFooter="True" FooterStyle-BackColor="#428BCA"
                            FooterStyle-ForeColor="white" OnRowDataBound="grdPaySheetRpt_RowDataBound" FooterStyle-HorizontalAlign="Right"
                            OnPageIndexChanging="grdPaySheetRpt_PageIndexChanging">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Division" HeaderText="Division" Visible="false">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="District" HeaderText="District" Visible="false">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CDPO" HeaderText="CDPO" Visible="false">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BIT" HeaderText="BIT" Visible="false">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Anganwadi" HeaderText="Anganwadi">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <%-- <asp:BoundField DataField="sevikaid" HeaderText="Sevika Id">
                                <HeaderStyle  Width="150px" />
                            </asp:BoundField>--%>
                                <asp:BoundField DataField="SevikaName" HeaderText="Sevika Name">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Aadhar" HeaderText="Aadhar No">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PFMS_Code" HeaderText="PFMS Code">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SchemeSpecificCode" HeaderText="Scheme Specific Id">
                                    <HeaderStyle Width="150px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Designation" HeaderText="Designation">
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDays" HeaderText="Total Days">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Present" HeaderText="Present">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="absent" HeaderText="Absent" FooterText="Total">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Central" HeaderText="Central" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="State" HeaderText="State" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fixed" HeaderText="Fixed" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Allowance1" HeaderText="Allowance1" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="allowance2" HeaderText="Allowance2" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deduction1" HeaderText="Deduction1" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deduction2" HeaderText="Deduction2" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="PayTribal" HeaderText="Pay Tribal" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="AddCenter" HeaderText="Additional Center" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="AddState" HeaderText="Additional State" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalWages" HeaderText="TotalWages" DataFormatString="{0:0}">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Width="60px" />
                                </asp:BoundField>
                            </Columns>
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
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
