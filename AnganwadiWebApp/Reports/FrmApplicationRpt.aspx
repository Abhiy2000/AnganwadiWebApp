<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmApplicationRpt.aspx.cs"
    Inherits="AnganwadiWebApp.Reports.FrmApplicationRpt" %>

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
                    <td align="left">Division
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDivision" runat="server" CssClass="DropDown" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" />
                    </td>
                    <td align="left">District
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Position
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlproj" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Anganwadi
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlAnganwadi" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Religion
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtReligion" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                    <td align="left">Educational Qualification
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlEduQuali" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Cast
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtcast" runat="server" CssClass="TextBox" Width="230px" />
                    </td>
                    <td align="left">Marital Status
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlMarStatus" runat="server" CssClass="DropDown" />
                    </td>
                    <td align="left">Disability %
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:TextBox ID="txtDisability" runat="server" CssClass="TextBox" Width="230px"/>
                    </td>
                </tr>
                <tr>
                    <td align="left">Status
                    </td>
                    <td align="center" class="style4">:
                    </td>
                    <td align="left">&nbsp;<asp:DropDownList ID="ddlStatus" runat="server" CssClass="DropDown" />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Btnsubmit" />
                                <asp:PostBackTrigger ControlID="BtnExportToPdf" />
                                <asp:PostBackTrigger ControlID="BtnExportToExcel" />
                                <asp:PostBackTrigger ControlID="btnClose" />
                                <asp:PostBackTrigger ControlID="GrdUserLevel" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button ID="Btnsubmit" CssClass="btn btn-primary" runat="server" Text="Search"
                                    ValidationGroup="A" Style="margin-top: 15px;" OnClick="Btnsubmit_Click" />
                                <asp:Button ID="BtnExportToPdf" CssClass="btn btn-primary" runat="server" Text="Export To Pdf"
                                    ValidationGroup="A" Style="margin-top: 15px;" OnClick="BtnExportToPdf_Click" Visible="false" />
                                <asp:Button ID="BtnExportToExcel" CssClass="btn btn-primary" runat="server" Text="Export To Excel"
                                    ValidationGroup="A" Style="margin-top: 15px;" OnClick="BtnExportToExcel_Click" Visible="false" />
                                <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Close" CssClass="btn btn-white"
                                    OnClick="BtnClose_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <hr />
            <table width="100%">
                <tr>
                    <td class="style1">
                        <asp:GridView ID="GrdUserLevel" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                            Width="100%" AutoGenerateColumns="False" AutoPostBack="true" OnRowDataBound="GrdUserLevel_RowDataBound">
                            <RowStyle CssClass="GrdRow"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Sr. No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="APPLIID" HeaderText="Application Id"></asp:BoundField>
                                <asp:BoundField DataField="APPLINO" HeaderText="Application No"></asp:BoundField>
                                <asp:BoundField DataField="APPNAME" HeaderText="Name of Applicant"></asp:BoundField>
                                <asp:BoundField DataField="ADDRESS" HeaderText="Address"></asp:BoundField>
                                <asp:BoundField DataField="DOB" HeaderText="Date of Birth" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="AGE" HeaderText="Age"></asp:BoundField>
                                <asp:BoundField DataField="AADHARNO" HeaderText="Aadhar No"></asp:BoundField>
                                <asp:BoundField DataField="PANNO" HeaderText="Pan No"></asp:BoundField>
                                <asp:BoundField DataField="SUBCAST" HeaderText="Subcaste"></asp:BoundField>
                                <asp:BoundField DataField="AUTHREMARK" HeaderText="Approved / Rejected Remark"></asp:BoundField>
                                <asp:BoundField DataField="AUTHBY" HeaderText="Approved / Rejected By"></asp:BoundField>
                                <asp:BoundField DataField="AUTHDT" HeaderText="Approved / Rejected Date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
