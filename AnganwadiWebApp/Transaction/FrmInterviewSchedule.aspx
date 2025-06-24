<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="FrmInterviewSchedule.aspx.cs" Inherits="ANCL_MRRGWEB.Transaction.FrmInterviewSchedule" %>

<%@ Register Src="../WebUserControls/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />

    <div class="Panel" style="width: 100%; height: 490px; overflow: auto">
        <div>
            <table width="100%">
                <tr>
                    <td align="left">Application No<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtApplino" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                    <td align="left">&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Name of Applicant
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtNameApp" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="230px" Height="50px" />
                    </td>
                    <td align="left">Address of Applicant
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="230px" Height="50px" />
                    </td>
                    <td align="left">Division
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td align="left">District
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Anganwadi
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Position
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlporition" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
                    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                        <td align="left">Birth Date
                        </td>
                        <td align="center" class="style4">:
                        </td>
                        <td align="left">&nbsp;<uc1:Date ID="birthdt" runat="server" />
                        </td>
                        <td align="left">Age
                        </td>
                        <td align="center" class="style4">:
                        </td>
                        <td align="left">&nbsp;<asp:TextBox ID="txtAge" runat="server" CssClass="TextBox" />
                        </td>
                    </asp:Panel>
                    <td align="left">Interview Date<asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<uc1:Date ID="DtInterviewDt" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdUserLevel" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                            Width="100%" AutoGenerateColumns="False" AutoPostBack="true">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="निवडा">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSlot" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="40px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Slot" HeaderText="वेळ (टप्पा )">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="time" HeaderText="वेळ" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btnsubmit" />
                                <asp:PostBackTrigger ControlID="btnClose" />
                                <asp:PostBackTrigger ControlID="GrdUserLevel" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button ID="Btnsubmit" CssClass="btn btn-primary" runat="server" Text="Submit"
                                    ValidationGroup="A" Style="margin-top: 15px;" OnClick="Btnsubmit_Click" />
                                <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Close" CssClass="btn btn-white"
                                    OnClick="BtnClose_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
