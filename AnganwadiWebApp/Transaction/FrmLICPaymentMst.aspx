<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmLICPaymentMst.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmLICPaymentMst" %>

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
        .style1
        {
            width: 317px;
        }
        .tab-content
        {
            padding-bottom: 23px !important;
        }
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
    <div class="Panel" style="width: 97%; height: 500px; overflow: auto">
        <div class="nav-tabs-custom">
            <div class="active tab-pane" id="tab-1">
                <table align="center">
                    <tr>
                        <td height="30px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Sevika Id
                            <asp:HiddenField runat="server" ID="hdnSevikaId" />
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtSevikaId" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Reason
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtReason" CssClass="TextBox" runat="server" ReadOnly="True" Width="230px" />
                        </td>
                        <td>
                            Elegiable Amount
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TxtClaimAmount" CssClass="TextBox" runat="server" ReadOnly="true"
                                Width="230px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Account No <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="txtAccountNo" CssClass="TextBox" runat="server" Width="230px" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            UTR No <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtUtrNo" CssClass="TextBox" runat="server" Width="230px" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Payment Date <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td>
                            <uc1:Date ID="DtPaymentDate" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Amount <span style="color: Red">*</span>
                        </td>
                        <td style="width: 10px">
                            :
                        </td>
                        <td class="style1">
                            <asp:TextBox ID="TxtAmount" runat="server" CssClass="TextBox" Width="230px" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnAddList" runat="server" Text="Add to List" CssClass="Button" ValidationGroup="A"
                                OnClick="BtnAddList_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <div align="center">
                    <asp:GridView ID="grdLICDEF" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        PageSize="10" CssClass="Grid" Width="75%" OnRowDeleting="grdLICDEF_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AccountNo" HeaderText="Account No" />
                            <asp:BoundField DataField="UtrNo" HeaderText="UTR No" />
                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                            <%--<asp:CommandField SelectText="Delete Row" ShowSelectButton="true" HeaderText="" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete">Delete Row</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <table align="left" style="margin-left: 450px;">
                    <tr>
                        <td>
                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                                OnClick="BtnSubmit_Click" />
                        </td>
                        <td width="10px">
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <asp:ImageButton ID="BigDocImg" runat="server" Height="300px" Width="300px" BorderWidth="1px"
            BorderColor="Black" BorderStyle="Solid" Style="display: none; float: right;" />
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
    &nbsp;&nbsp;&nbsp;
    <asp:HiddenField ID="hdnPop5" runat="server" />
    <asp:Panel ID="pnlMessage1" runat="server" CssClass="Popup" Style="width: 500px;
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
    <cc1:ModalPopupExtender ID="popMsg1" runat="server" BehaviorID="mpeMsg2" TargetControlID="hdnPop6"
        PopupControlID="pnlMessage2" CancelControlID="imgClose3">
    </cc1:ModalPopupExtender>
    <asp:HiddenField ID="hdnPop6" runat="server" />
    <asp:Panel ID="pnlMessage2" runat="server" CssClass="Popup" Style="width: 460px;
        height: 200px; display: none;">
        <%-- display: none; --%>
        <asp:Image ID="imgClose3" ToolTip="Close" runat="server" Style="z-index: -1; float: right;
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
                        <asp:Label ID="lblPopupResponse1" runat="server" Font-Bold="true" Text="Do you want to save Sevika in InActive Mode ?"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnYes" runat="server" CssClass="Button" Text="Yes" />
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
