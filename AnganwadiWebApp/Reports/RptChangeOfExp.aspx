<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="RptChangeOfExp.aspx.cs" Inherits="AnganwadiWebApp.Reports.RptChangeOfExp" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <table align="left">
            <tr>
                <td>
                    Division
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    District
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDistrict_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    CDPO
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlCDPO" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCDPO_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    BIT
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <asp:DropDownList ID="ddlBeat" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlBeat_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    As on Date
                </td>
                <td style="width: 10px">
                    :
                </td>
                <td>
                    <uc1:Date ID="dtAsonDt" runat="server" />
                </td>
                <td style="width: 30px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 10px">
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="Button"
                        Visible="false" Width="150px" OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="PnlSerch" runat="server">
            <br />
            <br />
            <br />
            <table>
                <tr>
                    <td style="height: 30px;">
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdChngofExp" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="10" 
                            onrowdatabound="GrdChngofExp_RowDataBound">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="Division" HeaderText="Division"></asp:BoundField>
                                <asp:BoundField DataField="District" HeaderText="District"></asp:BoundField>
                                <asp:BoundField DataField="CDPO" HeaderText="CDPO" HeaderStyle-Width="120px"></asp:BoundField>
                                <asp:BoundField DataField="BIT" HeaderText="BIT" HeaderStyle-Width="50px"></asp:BoundField>
                                <asp:BoundField DataField="Sevika_Name" HeaderText="Sevika Name" HeaderStyle-Width="250px"></asp:BoundField>
                                <asp:BoundField DataField="Aadhar_No" HeaderText="Aadhar No"></asp:BoundField>
                                <%--<asp:BoundField DataField="DOJ" HeaderText="Joining Date" DataFormatString="{0:dd-MMM-yyyy}">
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Joining Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOJ" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DOJ","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CurrentExp" HeaderText="Current Experienece"></asp:BoundField>
                                <%--<asp:BoundField DataField="ExpectedExp" HeaderText="Expected Experienece"></asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Expected Experienece" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpectedExp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
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
</asp:Content>
