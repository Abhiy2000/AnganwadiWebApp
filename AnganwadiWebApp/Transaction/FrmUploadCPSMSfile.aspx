<%@ Page Title="Upload PFMS Code" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="FrmUploadCPSMSfile.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmUploadCPSMSfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <div class="Panel" style="width: 1100px; height: 500px; overflow: auto;">
        <table align="left" width="60%">
            <tr>
                <td width="90px">
                    File Upload
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td width="160px">
                    <asp:FileUpload ID="upldCPSMS" runat="server" />
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="upldCPSMS"
                        ErrorMessage="Invalid File" ValidationExpression="(.*\.([Tt][Xx][Tt])$)" ValidationGroup="A">
                    </asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-left: 20px">
                    <br />
                    <asp:UpdatePanel ID="updtpanl" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="lnkDownload" />
                        </Triggers>
                        <ContentTemplate>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="Button" ValidationGroup="A"
                                OnClick="BtnUpload_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">Download Template</asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Style="overflow: auto; border: none;">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GrdList" runat="server" CssClass="Grid" AutoGenerateColumns="false"
                            Width="100%" OnRowDataBound="GrdList_RowDataBound">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="PFMSCODE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCPSMS" Text='<%# Eval("CPSMS_Beneficiary_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SCHEMESPECIID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblSevikaCode" Text='<%# Eval("Scheme_Specific_Id")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Beneficiary_Name" HeaderText="FULLNAME" />
                                <asp:BoundField DataField="Beneficiary_Regional_Name" HeaderText="REGIONAL_NAME" />
                                <asp:BoundField DataField="Bank_Name" HeaderText="BANK" />
                                <asp:BoundField DataField="Aadhaar_Number" HeaderText="AADHARNO" />
                                <asp:BoundField DataField="Account_Number" HeaderText="Account_No" />
                                <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC" />
                                <asp:BoundField DataField="State" HeaderText="STATE" />
                                <asp:BoundField DataField="District" HeaderText="DISTRICT" />
                                <asp:BoundField DataField="Address_Line_1" HeaderText="ADDRESS1" />
                                <asp:BoundField DataField="Address_Line2" HeaderText="ADDRESS2" />
                                <asp:BoundField DataField="Address_Line3" HeaderText="ADDRESS3" />
                                <asp:BoundField DataField="Centre_Share_Payment_Amount" HeaderText="CENTERSHAREAMT" />
                                <asp:BoundField DataField="State_Share_Payment_Amount" HeaderText="STATESHAREAMT" />
                                <asp:BoundField DataField="Total_Amount" HeaderText="TOTAL_AMOUNT" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" OnClick="btnUpdate_Click" />
                </td>
            </tr>
        </table>
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
    <ajaxToolkit:ModalPopupExtender ID="popMsg" runat="server" BehaviorID="mpeMsg1" TargetControlID="hdnPop5"
        PopupControlID="pnlMessage1" CancelControlID="imgClose2">
    </ajaxToolkit:ModalPopupExtender>
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
