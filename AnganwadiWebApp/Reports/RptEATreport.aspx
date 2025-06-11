<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RptEATreport.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptEATreport" %>

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
        <asp:Panel ID="PnlDropdowns" runat="server" Width="80%">
            <table>
                <tr>
                    <td style="width: 115px">Division
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
                        <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlSerch" runat="server">
            <table align="left" width="55%">
                <tr>
                    <td>Designation Type
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td height="40px">
                        <asp:RadioButton ID="rbdWorker" runat="server" Text="Anganwadi Worker" AutoPostBack="true"
                            GroupName="desgFlag" OnCheckedChanged="rbdWorker_CheckedChanged" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdHelper" runat="server" Text="Anganwadi Hepler" AutoPostBack="true"
                            GroupName="desgFlag" OnCheckedChanged="rbdHelper_CheckedChanged" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdMini" runat="server" Text="Mini Anganwadi" AutoPostBack="true"
                            GroupName="desgFlag" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdOther" runat="server" Text="Anganwadi Other" AutoPostBack="true"
                            Visible="false" GroupName="desgFlag" OnCheckedChanged="rbdOther_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--Designation--%>
                    </td>
                    <td style="width: 10px"></td>
                    <td>
                        <asp:DropDownList ID="ddldesg" runat="server" CssClass="DropDown" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Sevika
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td>
                        <asp:RadioButton ID="rbdRegistered" runat="server" Text="Registered" GroupName="SevikaType"
                            Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdUnregistered" runat="server" Text="Unregistered" GroupName="SevikaType" />
                        <%-- &nbsp;&nbsp;
                        <asp:RadioButton ID="rbdMaternity" runat="server" Text="Maternity" GroupName="SevikaType" />--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        Salary<span style="width: 10px;margin-left:40px">:</span>
                        <asp:RadioButton ID="RbtnGross" runat="server" Text="Gross" GroupName="sal"
                            Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="RbtnNet" runat="server" Text="Net" GroupName="sal" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 89px">Salary Date
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td width="350px">
                        <%--<uc1:Date ID="dtSalDate" runat="server" />--%>
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="110px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" Width="100px"
                            OnClick="BtnSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                            Width="116px" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <div>
                <asp:Panel ID="PnlBillNo" runat="server" Style="overflow: auto; border: none;">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="grdBillno" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                                    Width="100%" OnSelectedIndexChanged="grdBillno_SelectedIndexChanged">
                                    <RowStyle CssClass="GrdRow"></RowStyle>
                                    <Columns>
                                        <asp:CommandField HeaderText="Select" ShowSelectButton="True" SelectText="Select">
                                            <HeaderStyle Width="60px" />
                                        </asp:CommandField>
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
            <br />
            <table>
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdPaymnt" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10" OnSelectedIndexChanged="GrdPaymnt_SelectedIndexChanged"
                            OnRowDataBound="GrdPaymnt_RowDataBound" OnPageIndexChanging="GrdPaymnt_PageIndexChanging">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="sevikaid" HeaderText="Sevika Id"></asp:BoundField>
                                <asp:BoundField DataField="cpsmscode" HeaderText="CPSMS Beneficiary Code"></asp:BoundField>
                                <asp:BoundField DataField="SchemeSpeciId" HeaderText="Scheme Specific Id"></asp:BoundField>
                                <asp:BoundField DataField="BenifisheryName" HeaderText="Beneficiary Name"></asp:BoundField>
                                <asp:BoundField DataField="Purpose" HeaderText="Purpose"></asp:BoundField>
                                <asp:BoundField DataField="centralShare" HeaderText="Centre Share Payment Amount"></asp:BoundField>
                                <asp:BoundField DataField="StateShare" HeaderText="State Share Payment Amount"></asp:BoundField>
                                <asp:BoundField DataField="FrmDate" HeaderText="Payment From Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="ToDate" HeaderText="Payment To Date" DataFormatString="{0:dd-MMM-yyyy}"></asp:BoundField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExport" />
                    <asp:PostBackTrigger ControlID="grdBillno" />
                </Triggers>
            </asp:UpdatePanel>
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
