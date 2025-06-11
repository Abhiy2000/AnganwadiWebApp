<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmPromotionList.aspx.cs" Inherits="AnganwadiWebApp.Master.FrmPromotionList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tblIntFormat
        {
            height: 75px;
            width: 500px;
        }
    </style>
    <script  type="text/javascript">
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
    <div class="Panel">
       <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <hr />
        <div class="panel-body">
            <table class="tblIntFormat">
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnAddNew" Text="Add New" class="Button" OnClick="btnAddNew_Click" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Aadhar No
                    </td>
                    <td colspan="4">
                        &nbsp;:
                        <asp:TextBox  CssClass="TextBox" runat="server" ID="txtAdharNo" placeholder="Input Aadhar Number" Width="205px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnSearch" class="Button" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="lblError" runat="server" Style="color: Red;"></asp:Label>
            <asp:GridView runat="server" AutoGenerateColumns="false" ID="grdList" AllowPaging="true"
                PageSize="10" CssClass="Grid" Width="85%" 
                OnSelectedIndexChanged="grdList_SelectedIndexChanged" OnRowDataBound="grdList_OnRowDataBound">
                <Columns>
                    <asp:CommandField SelectText="Promote" ShowSelectButton="true" HeaderText="Select" />
                    <asp:BoundField DataField="divname" HeaderText="Division" />
                    <asp:BoundField DataField="distname" HeaderText="District" />
                    <asp:BoundField DataField="cdponame" HeaderText="CDPO" />
                    <asp:BoundField DataField="bitbitname" HeaderText="BIT" />
                    <asp:BoundField DataField="savikaName" HeaderText="Name" />
                    <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MM/yyyy}" />                    
                    <asp:BoundField DataField="Exp" HeaderText="Experience" />
                    <asp:BoundField DataField="desig" HeaderText="Designation" />
                    <asp:BoundField DataField="educname" HeaderText="Education" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="payscal" HeaderText="Payscale" />
                    <asp:BoundField DataField="eduid" HeaderText="EduId" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div>
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
    </div>
</asp:Content>
