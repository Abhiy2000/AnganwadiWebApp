<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmLICPaymentList.aspx.cs" Inherits="AnganwadiWebApp.Transaction.FrmLICPaymentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
       
        function redirectToNewWindow() {

            window.open("../Transaction/FrmLICPaymentMst.aspx", "_blank");
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
<div>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- Loader --%>
                    <div id="imgrefresh" class="loader transparent">
                        <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Images/loader.GIF" AlternateText="Loading ..."
                            ToolTip="Loading ..." Style="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 1125px; height: 500px; overflow: auto">
       
      
       <%-- <asp:Panel ID="PnlSerch" runat="server" Style="width: 1125px; height: 500px;">--%>
            <div>
                <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="grdLICDEF" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="grdLICDEF" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            PageSize="10" CssClass="Grid" Width="100%" OnSelectedIndexChanged="grdLICDEF_SelectedIndexChanged"
                            OnRowDataBound="grdLICDEF_OnRowDataBound">
                            <Columns>
                                <asp:CommandField SelectText="Select" ShowSelectButton="true" HeaderText="Select" />
                                <asp:BoundField DataField="SevikaID" HeaderText="SevikaID" />
                                <asp:BoundField DataField="SevikaName" HeaderText="Sevika Name" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="ExitReason" HeaderText="Reason" />
                                <asp:BoundField DataField="ClaimAmount" HeaderText="Elegiable Amount" />
                                <asp:BoundField DataField="divname" HeaderText="Division" />
                                <asp:BoundField DataField="distname" HeaderText="District" />
                                <asp:BoundField DataField="cdponame" HeaderText="CDPO" />
                                <asp:BoundField DataField="bitbitname" HeaderText="BIT" />
                             </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
           
       <%-- </asp:Panel>--%>
    </div>
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
