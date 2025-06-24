<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmApplicationVerifyMst.aspx.cs"
    Inherits="AnganwadiWebApp.Transaction.FrmApplicationVerifyMst" %>

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
                    <td align="left">&nbsp;
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
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
                    <td align="left">&nbsp;
                        <asp:DropDownList ID="ddlporition" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
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
                    <td align="left">&nbsp;<asp:TextBox ID="txtAge" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Document Verified<asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:RadioButtonList ID="rdbDocVerify" runat="server" RepeatDirection="Horizontal" Style="margin-top: -10px;">
                        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td align="left">Application Approved<asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:RadioButtonList ID="rdbAppliApproved" runat="server" RepeatDirection="Horizontal" Style="margin-top: -10px;">
                        <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="N" style="margin-left: 15px;"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                    <td align="left">Remark<asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox" TextMode="MultiLine" Width="230px" Height="50px" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:GridView ID="grdDoc" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                            Width="100%" AutoGenerateColumns="false" OnRowCommand="grdDoc_RowCommand"
                            AllowPaging="True" PageSize="10" OnRowDataBound="grdDoc_RowDataBound"
                            OnSelectedIndexChanged="grdDoc_SelectedIndexChanged" ShowFooter="true">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:BoundField DataField="Docid" HeaderText="DocumentID" />
                                <asp:TemplateField HeaderText="Document Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocNames" Text='<%#Eval("Docname")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server"
                                            CssClass="btn btn-secondary" CommandName="PreviewFile" CommandArgument='<%# Eval("Docid") %>' Text="View"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMarks" runat="server" Text="0" CssClass="form-control" OnTextChanged="txtMarks_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblToTMarks" runat="server" Text="Total Marks : " Font-Bold="true" ForeColor="DarkBlue"></asp:Label>
                                        <asp:Label ID="lblTotalMarks" runat="server" Font-Bold="true" Text="0" ForeColor="DarkBlue"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
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
                                <asp:PostBackTrigger ControlID="grdDoc" />
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
