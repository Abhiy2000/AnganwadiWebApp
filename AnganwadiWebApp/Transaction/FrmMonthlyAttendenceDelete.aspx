<%@ Page Title="Monthly Attendence" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmMonthlyAttendenceDelete.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmMonthlyAttendenceDelete" %>

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
    <style type="text/css">
        .notclickable {
            cursor: text;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function InProgress() {
            document.getElementById("imgrefresh").style.visibility = 'visible';
        }
        function onComplete() {
            document.getElementById("imgrefresh").style.visibility = 'hidden';
        }


        function GetSelectedRow(lnk) {
            debugger;

            var row = lnk.parentNode.parentNode;

            var rowIndex = row.rowIndex - 1;

            var lblTotalDays = row.cells[4].getElementsByTagName("span")[0];

            var lblPrsntDays = row.cells[6].getElementsByTagName("span")[0];
            var txtAbsentDays = row.cells[5].getElementsByTagName("input")[0];

            if (txtAbsentDays != "") {
                var totaldays = lblTotalDays.innerHTML;
                var AbsentDays = txtAbsentDays.value;

                if (parseInt(AbsentDays) > parseInt(totaldays)) {
                    alert('|| Absent Days can not be greater than Total Days ||');
                    txtAbsentDays.value = "";
                    lblPrsntDays.innerHTML = "";
                    return false;
                }

                lblPrsntDays.innerHTML = parseInt(totaldays) - parseInt(AbsentDays);

                if (document.getElementById("hdnpresentday").value == "") {
                    document.getElementById("hdnpresentday").value = rowIndex + "~" + lblPrsntDays.innerHTML;
                }
                else {
                    document.getElementById("hdnpresentday").value = document.getElementById("hdnpresentday").value + "$" + rowIndex + "~" + lblPrsntDays.innerHTML;
                }
            }
        }


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            alert('asass');
            var gvDrv = document.getElementById("<%=grdMonAttendance.ClientID %>");
            for (i = 0; i < gvDrv.rows.length; i++) {

                var lblPrsntDays = row.cells[6].getElementsByTagName("span")[0];
                lblPrsntDays.innerHTML = "10";

            }
        });
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

        .style3 {
            width: 121px;
        }

        .style4 {
            width: 396px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <asp:HiddenField ID="hdnpresentday" runat="server" ClientIDMode="Static" />
    <div class="Panel" style="width: 97%; height:550px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table>
                <tr>
                    <td class="style3">Division<span style="color: Red"> * </span>
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 260px">
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged" />
                    </td>
                    <td style="width: 70px">District<span style="color: Red"> * </span>
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td style="width: 240px">
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">CDPO<span style="color: Red"> * </span>
                    </td>
                    <td>:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged" />
                    </td>
                    <td>BIT<span style="color: Red"> * </span>
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
            <table align="left">
                <tr>
                    <td>
                        <%-- Project Type--%>
                    </td>
                    <td style="width: 10px"></td>
                    <td class="style4">
                        <asp:DropDownList ID="ddlprjType" runat="server" CssClass="DropDown" OnSelectedIndexChanged="ddlprjType_SelectedIndexChanged"
                            Visible="false" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--Anganwadi<span style="color: Red"> * </span>--%>
                    </td>
                    <td style="width: 10px"></td>
                    <td class="style4">
                        <asp:DropDownList ID="ddlAngan" runat="server" CssClass="DropDown" AutoPostBack="true"
                            Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--Salary Date--%>
                        Attendance Month<span style="color: Red"> * </span>
                    </td>
                    <td style="width: 10px">:
                    </td>
                    <td class="style4">
                        <%-- <uc1:Date ID="dtSalDate" class="dtSalDate" runat="server" />--%>
                        <asp:DropDownList runat="server" ID="ddlMonth" CssClass="DropDown" Width="110px">
                        </asp:DropDownList>
                        <asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDown" Width="100px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="10px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td class="style4">
                        <asp:Button ID="Submit" runat="server" CssClass="Button" Text="Search" OnClick="Submit_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="Button" Text="Back" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
            <table align="right">
                <tr>
                    <td>
                        <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblrowCount" runat="server" Text=""></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblUnauthSevika" runat="server" Text="Unauthorized Sevika :" Visible="false"></asp:Label>
                        <asp:Label ID="lblUnauthCount" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <div style="max-height: 250px; overflow: auto;">
                            <asp:GridView ID="grdMonAttendance" runat="server" AutoGenerateColumns="false" CssClass="Grid" Enabled="false"
                                Width="100%">
                                <RowStyle CssClass="GrdRow"></RowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sevika Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSevikaId" Text='<%# Eval("sevikaid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <HeaderStyle Width="70px" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BIT Name" HeaderStyle-Width="120px" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBITNM" Text='<%# Eval("BITNM")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Anganwadi Name" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAnganNM" Text='<%# Eval("anganNM")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sevika Name" HeaderStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSevkiaNm" Text='<%# Eval("sevikaNM")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="250px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desgination">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDesg" Text='<%# Eval("desgNM")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Days">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblTotalDays" Text='<%# Eval("Days")%>' ClientIDMode="Static"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Absent Days">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAbsentDays" runat="server" Width="80px" ClientIDMode="Static" MaxLength="2"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                FilterMode="ValidChars" TargetControlID="txtAbsentDays" ValidChars="0123456789">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Present Days">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPrsntDays" ClientIDMode="Static"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allowence">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAllow" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                FilterMode="ValidChars" TargetControlID="txtAllow" ValidChars="0123456789">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deduction">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDeduct" runat="server" Width="80px"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                FilterMode="ValidChars" TargetControlID="txtDeduct" ValidChars="0123456789">
                                            </ajaxToolkit:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="20px"></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="padding-left:400px;">
                        <asp:Button ID="btnInsert" runat="server" CssClass="Button" Width="160px" Text="Delete Attendance" Visible="false"
                            OnClick="btnInsert_Click" />
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
    <cc1:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 500px; height: 200px; display: none;">
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
