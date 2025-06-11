<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPagePayDetails.Master"
    AutoEventWireup="true" CodeBehind="FrmPayDetailReport.aspx.cs" Inherits="AnganwadiWebApp.Reports.FrmPayDetailReport" %>

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
        .style1
        {
            width: 480px;
            height: 250px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //alert('page loaded');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"> Payment Detail Report </asp:Label>
    </div>
    <br />
    <div class="Panel" style="overflow-y: scroll; height: 500px;">
        <div style="margin-left: 300px;">
            <table class="style1">
                <tr>
                    <td>
                        Enter Aadhar Number
                    </td>
                    <td colspan="3">
                        :&nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtAdharNumber" MaxLength="12"
                            Width="130px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtAdharNumber" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Aadhar Number
                    </td>
                    <td colspan="3">
                        :&nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtConfirmAdharNumber"
                            MaxLength="12" Width="130px"></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                            FilterMode="ValidChars" TargetControlID="txtConfirmAdharNumber" ValidChars="0123456789">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Image ID="imgPayConfCaptcha" runat="server" Height="35px" Width="130px" Style="margin-left: 10px;" />
                                <asp:ImageButton ID="btnPayConfRefresh" runat="server" ToolTip="Refresh" ImageUrl="~/Images/btnRefresh.png"
                                    Width="30px" Height="30px" OnClick="btnPayConfRefresh_Click" />
                                <%-- OnClick="btnPayConfRefresh_Click"--%>
                                <%--<asp:LinkButton ID="BtnRefresh" runat="server" Visible="false" OnClick="BtnRefresh_Click">Refresh</asp:LinkButton>--%>
                                <%--OnClick="BtnRefresh_Click"--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkBtnDownload" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        Enter Captcha
                    </td>
                    <td colspan="3">
                        :&nbsp;<asp:TextBox CssClass="TextBox" runat="server" ID="txtPayConfCaptcha" MaxLength="16"
                            Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Style="margin-top: 8px; margin-left: 10px;"
                            CssClass="Button" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkBtnDownload" runat="server" Text="Download PDF" OnClick="lnkBtnDownload_Click" />
                    </td>
                </tr>
                <tr id="trSevika" runat="server">
                    <td>
                        <asp:Label runat="server" ID="lblSavikaName">Sevika Name</asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="Label1">:</asp:Label>&nbsp;<asp:TextBox CssClass="TextBox"
                            runat="server" ID="txtSavikaName" MaxLength="16" Width="275px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <center>
                <asp:GridView ID="grdMonAttendance" runat="server" AutoGenerateColumns="false" CssClass="Grid"
                    Width="70%" Visible="true" OnRowDataBound="grdMonAttendance_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="salary_for" HeaderText="Salary For" />
                        <asp:BoundField DataField="credited_date" HeaderText="Credited Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="cpsmscode" HeaderText="Scheme Specific ID" />
                        <asp:BoundField DataField="bank_name" HeaderText="Bank Name" />
                        <asp:BoundField DataField="accno" HeaderText="Acc No" />
                        <asp:BoundField DataField="totalpaid" HeaderText="Amount" />
                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </center>
        </div>
    </div>
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
    &nbsp;&nbsp;&nbsp;
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
</asp:Content>
