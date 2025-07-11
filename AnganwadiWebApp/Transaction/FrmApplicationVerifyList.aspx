﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="FrmApplicationVerifyList.aspx.cs"
    Inherits="AnganwadiWebApp.Transaction.FrmApplicationVerifyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="boxHead">
        <asp:Label ID="LblGrdHead" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />

    <div class="Panel" style="width: 100%; height: 468px; overflow: auto">
        <asp:Panel ID="PnlDropdowns" runat="server">
            <table width="100%">
                <tr>
                    <td class="style1">
                        <center>
                            <asp:GridView ID="GrdApplication" runat="server" CssClass="table table-striped table-bordered table-hover dataTables3-example"
                                Width="100%" AutoGenerateColumns="false"
                                AllowPaging="False" PageSize="10" OnRowDataBound="GrdApplication_RowDataBound"
                                OnSelectedIndexChanged="GrdApplication_SelectedIndexChanged">
                                <RowStyle CssClass="GrdRow"></RowStyle>
                                <Columns>
                                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" SelectText="Select"></asp:CommandField>
                                    <asp:TemplateField HeaderText="Sr. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="APPLIID" HeaderText="Application Id"></asp:BoundField>
                                    <asp:BoundField DataField="APPLINO" HeaderText="Application No"></asp:BoundField>
                                    <asp:BoundField DataField="APPNAME" HeaderText="Name of Applicant"></asp:BoundField>
                                    <asp:BoundField DataField="PORTID" HeaderText="Position Id"></asp:BoundField>
                                    <asp:BoundField DataField="POSITION" HeaderText="Applied Position"></asp:BoundField>
                                    <asp:BoundField DataField="ANGNID" HeaderText="Anganwadi Id"></asp:BoundField>
                                    <asp:BoundField DataField="ANGNNAME" HeaderText="Anganwadi"></asp:BoundField>
                                </Columns>
                                <AlternatingRowStyle CssClass="GrdAltRow"></AlternatingRowStyle>
                            </asp:GridView>
                        </center>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr align="center">
                    <td>
                        <asp:Button ID="BtnClose" runat="server" Style="margin-top: 15px;" Text="Back" CssClass="btn btn-white"
                            OnClick="BtnClose_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
