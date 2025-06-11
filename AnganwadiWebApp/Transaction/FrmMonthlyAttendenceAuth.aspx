<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmMonthlyAttendenceAuth.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmMonthlyAttendenceAuth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
    <script type="text/javascript">

        function calculate() {
            var a1, a2, a3, a4, a5, central, state, fixed, total, total1, total2, total3;

            a1 = document.getElementById('#lblPrsntDays').html();
            a2 = document.getElementById('lblPrsntDays').innertext;
            a3 = document.getElementById('txtAllow').innerHTML;
            a4 = document.getElementById('txtDeduct').innerHTML;
            a5 = document.getElementById('txtTotalwages').innerHTML;
            central = document.getElementById("<%=HiddenField1.ClientID %>");
            state = document.getElementById("<%=HiddenField2.ClientID %>");
            fixed = document.getElementById("<%=HiddenField3.ClientID %>");

            total = parseInt(a5) / parseInt(a1) * parseInt(a2);
            total1 = parseInt(central) / parseInt(a1) * parseInt(a2);
            total2 = parseInt(state) / parseInt(a1) * parseInt(a2);
            total3 = parseInt(fixed) / parseInt(a1) * parseInt(a2);

            a5 = parseInt(total1) + parseInt(total2) + parseInt(total3) + parseInt(a3) - parseInt(a4)
            document.getElementById("txtTotalwages").value = a5;
        }
    </script>
    <style type="text/css">
        .notclickable
        {
            cursor: text;
        }
        .style1
        {
            width: 124px;
        }
    </style>
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
        <table align="center" width="100%">
            <tr>
                <td class="style1">
                    <%--Project Type--%>
                </td>
                <td style="width: 10px">
                </td>
                <td>
                    <asp:DropDownList ID="ddlprjType" runat="server" CssClass="DropDown" OnSelectedIndexChanged="ddlprjType_SelectedIndexChanged"
                        Visible="false" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <%--Salary Date--%>
                    Attendance Month<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <uc1:Date ID="dtSalDate" class="dtSalDate" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <table align="center" width="100%">
            <tr>
                <td>
                    <asp:Button ID="Submit" runat="server" CssClass="Button" Text="Submit" OnClick="Submit_Click" />
                </td>
                <td width="10px">
                </td>
                <td>
                    <asp:Button ID="btnBack" runat="server" CssClass="Button" Text="Back" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table align="center" width="100%">
            <tr>
                <td>
                    <asp:GridView ID="grdMonAttendance" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                        OnRowUpdating="grdMonAttendance_RowUpdating" Width="100%" Visible="true">
                        <RowStyle CssClass="GrdRow"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="chkAll" Text="Select All" AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkCompany" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sevika Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSevikaId" Text='<%# Eval("sevikaid")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sevika Name" HeaderStyle-Width="250px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblSevkiaNm" Text='<%# Eval("sevikaNM")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="350px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Beat" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblbeat" Text='<%# Eval("brname")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Anganwadi" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblanganwadi" Text='<%# Eval("angnname")%>'></asp:Label>
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
                                    <asp:Label runat="server" ID="lblTotalDays" Text='<%# Eval("Days")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Absent Days">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAbsentDays" runat="server" Width="40px" OnTextChanged="txtAbsentDays_TextChanged"
                                        AutoPostBack="true" MaxLength="2"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        FilterMode="ValidChars" TargetControlID="txtAbsentDays" ValidChars="0123456789">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Present Days">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPrsntDays"></asp:Label>
                                    <%--<asp:TextBox ID="txtPrsntDays" runat="server" Width="80px"></asp:TextBox>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Allowence">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAllow" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtAllow_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                        FilterMode="ValidChars" TargetControlID="txtAllow" ValidChars="0123456789">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deduction">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDeduct" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txtDeduct_TextChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                        FilterMode="ValidChars" TargetControlID="txtDeduct" ValidChars="0123456789">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Wages">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalwages" runat="server" Width="50px" ReadOnly="true"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Update">
                                <ItemTemplate>
                                    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" Width="60px" CommandName="Update" />
                                    <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="Label3" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="Label4" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="Label5" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="values" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblcentral" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblstate" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblfixed" Visible="false"></asp:Label>

                                    <asp:Label runat="server" ID="lblPayTribal" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblAddCenter" Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblAddState" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnInsert" runat="server" CssClass="Button" Text="Authorize" Visible="false"
                        OnClick="btnInsert_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="HiddenField4" runat="server"></asp:HiddenField>
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

      <cc1:ModalPopupExtender ID="popMsg1" runat="server" BehaviorID="mpeMsg2" TargetControlID="HiddenField5"
        PopupControlID="Panel1" CancelControlID="Image1">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="HiddenField5" runat="server" />
    <asp:Panel ID="Panel1" runat="server" CssClass="Popup" Style="width: 460px;
        height: 200px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="Image1" ToolTip="Close" runat="server" Style="z-index: -1; float: right;
            margin-top: -15px; margin-right: -15px;" onclick="closeMsgPopupNew1();" ImageUrl="~/Images/closebtn.png" />
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
                        <asp:Label ID="lblPopupResponse1" runat="server" Font-Bold="true" Text=" तुम्ही काही सेविकांची हजेरी शून्य दिवस ऑथोराइज्ड करत आहात. जर बरोबर असल्यास 'YES' वर Click करा अन्यथा 'NO' वर Click करा."></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" CssClass="Button" Text="Yes" OnClick="btnYes_Click" />
                        <%-- <asp:Button ID="btnClodeMsg" runat="server" CssClass="Button" Text="Close" />--%>
                        <input id="btnClodeMsg1" class="Button" runat="server" type="button" value="No" style="width: 100px;"
                            onclick="closeMsgPopupnew1();" />
                        <asp:Label ID="lblredirect1" runat="server" Style="display: none"></asp:Label>
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
