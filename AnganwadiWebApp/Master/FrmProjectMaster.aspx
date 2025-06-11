<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmProjectMaster.aspx.cs" Inherits="ProjectManagement.Master.FrmProjectMaster" %>

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
            <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table align="center">
            <tr>
                <td>
                    Project Id<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtProjId" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
                <td>
                    Reference No<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtReferenceNo" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Name<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtProjName" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Owner<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjOwner" runat="server" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtDate" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
                <td>
                    Duration to complete<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDurComplete" runat="server" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Monitor By<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtMonitor" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
                <td>
                    User Id<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserId" runat="server" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    No of stages<span style="color: Red"> * </span>
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtNoStages" CssClass="TextBox" runat="server" MaxLength="50" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table align="center">
            <tr>
                <td>
                    <asp:GridView ID="GrdBranch" runat="server" CssClass="Grid" AutoGenerateColumns="False"
                        Width="500px" OnRowDataBound="GrdBranch_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkBranch" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="branchname" HeaderText="Branch Name">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="branchcode" HeaderText="Branch Code">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="brid" HeaderText="BRID">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <AlternatingRowStyle CssClass="GrdAltRow" />
                        <RowStyle CssClass="GrdRow" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="Button" ValidationGroup="A"
                        OnClick="BtnSubmit_Click" />
                </td>
            </tr>
        </table>
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
