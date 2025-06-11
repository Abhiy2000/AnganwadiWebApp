<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmCompanyList.aspx.cs" Inherits="ProjectManagement.MstListPages.FrmCompanyList" %>
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
       <b>
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label></b>
    </div>
    <br />
  
    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <asp:GridView ID="GrdCompanyList" runat="server" Width="60%" CssClass="Grid" ShowFooter="True"
            AutoGenerateColumns="False" OnRowDataBound="GrdCompanyList_RowDataBound" OnSelectedIndexChanged="GrdCompanyList_SelectedIndexChanged">
            <RowStyle CssClass="GrdRow" />
            <AlternatingRowStyle CssClass="GrdAltRow" />
            <Columns>
                <asp:CommandField ShowSelectButton="true" SelectText="Select" HeaderText="Select">
                    <HeaderStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField HeaderText="CompanyId" DataField="CompanyId">
                    <HeaderStyle Width="0px" />
                </asp:BoundField>
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name">
                    <HeaderStyle Width="330px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle CssClass="GridFooter" />
        </asp:GridView>
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
